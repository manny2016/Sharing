
namespace Sharing.Core.Services
{
    using System;
    using System.Collections.Generic;
    using Sharing.Core.Models;
    using System.Linq;
    using Microsoft.Extensions.Caching.Memory;
    using Sharing.Core.Entities;

    public class SharingHostService : ISharingHostService
    {
        //private readonly IMerchantService MerchantService;
        private readonly IMemoryCache MemoryCache;
        public SharingHostService(IMemoryCache memoryCache)
        {

            this.MemoryCache = memoryCache;
        }
        public IList<MerchantDetails> MerchantDetails
        {
            get
            {
                string cacheKey = "Sharing.MerchantDetails";
                return this.MemoryCache.GetOrCreate<IList<MerchantDetails>>(cacheKey, (entity) =>
                  {
                      var queryString = @"
    SELECT  
    merchant.Id,
    merchant.MCode,
    merchant.BrandName,
    (
	    SELECT CONCAT(
        '[',
		    GROUP_CONCAT(JSON_OBJECT('appid',wxapp.AppId,'OriginalId',wxapp.OriginalId,'secret',wxapp.Secret,'appType',wxapp.`AppType`)),
        ']') FROM  
        `sharing_mwechatapp` AS wxapp WHERE wxapp.MerchantId = merchant.Id
    ) AS WxApps
    FROM `sharing_merchant` AS merchant;
    ";
                      var result = new List<MerchantDetails>();
                      using (var database = SharingConfigurations.GenerateDatabase(false))
                      {
                          result = database.SqlQuery<MerchantDetails>(queryString).ToList();
                      }
                      entity.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                      return result;
                  });
            }
        }
        public IList<ProductTreeNodeModel> Products
        {
            get
            {
                string cacheKey = "Sharing.Products";
                return this.MemoryCache.GetOrCreate<IList<ProductTreeNodeModel>>(cacheKey, (entity) =>
                {
                    var result = new List<ProductTreeNodeModel>();
                    using (var database = SharingConfigurations.GenerateDatabase(false))
                    {
                        var queryString = @"
SET group_concat_max_len := @@max_allowed_packet;
SELECT Id,`Name`,`MchId`,
(
	SELECT CONCAT(
    '[',
		GROUP_CONCAT(JSON_OBJECT(
        'id',products.`Id`,
        'mchid',products.`MchId`,
        'name',products.`Name`,
        'price',products.`Price`,
        'salesVol',products.`SalesVol`,
        'sortNo',products.`SortNo`,
        'imageUrl',products.`ImageUrl`
       )),
    ']') FROM  
    `sharing_product` AS products WHERE  products.CategoryId = categories.Id AND Enabled = 1
) AS JsonString
FROM `sharing_category` AS categories
WHERE  Enabled = 1
";
                        result = database.SqlQuery<Category>(queryString).Select((category) =>
                        {
                            return new ProductTreeNodeModel()
                            {
                                CategoryId = category.Id,
                                CategoryName = category.Name,
                                MchId = category.MchId,
                                Products = (category.JsonString ?? "[]").DeserializeToObject<ProductModel[]>()
                            };
                        }).ToList();

                    }
                    entity.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                    return result;
                });
            }

        }
        public IEnumerable<ProductModel> GetHotSaleProducts(long mchid, int top = 10)
        {
            return this.Products
                .Where(o => o.MchId.Equals(mchid))
                .SelectMany(o => o.Products)
                .OrderByDescending(o => o.SalesVol)
                .Take(top);
        }
        public ProductTreeNodeModel[] GetProductTree(long mchid)
        {
            return this.Products
                .Where(o => o.MchId.Equals(mchid))
                .ToArray();
        }
    }
}
