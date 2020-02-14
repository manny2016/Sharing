
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
 SELECT [Id],[MCode],[BrandName],
(
	SELECT [AppId],[OriginalId],[Secret],[AppType] FROM [dbo].[MWeChatApp] (NOLOCK) [app] WHERE [app].[MerchantId] = [merchant].[Id]
	FOR JSON PATH
) AS WxApps
FROM [dbo].[Merchant] (NOLOCK) [merchant]";
                      var result = new List<MerchantDetails>();
                      using (var database = SharingConfigurations.GenerateDatabase(isWriteOnly:false))
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
                    using (var database = SharingConfigurations.GenerateDatabase(isWriteOnly:false))
                    {
                        var queryString = @"
SELECT 
[categories].[Id],
[categories].[Name],
[categories].[MerchantId],
(
	SELECT [Id],[MerchantId] AS [MchId],[Name],[Price],[SalesVol],[SortNo],[ImageUrl],[Settings] AS [options] FROM [Product] (NOLOCK) [products] 
	WHERE [products].[CategoryId]= [categories].[Id] AND [products].[Enabled] = 1
	FOR JSON PATH
) AS JsonString
FROM [dbo].[Category] (NOLOCK) [categories] WHERE Enabled = 1
";
                        result = database.SqlQuery<Category>(queryString).Select((category) =>
                        {
							var model =  new ProductTreeNodeModel()
                            {
                                CategoryId = category.Id,
                                CategoryName = category.Name,
                                MchId = category.MerchantId,
                                Products = (category.JsonString ?? "[]").DeserializeToObject<ProductModel[]>()
                            };
							return model;
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
