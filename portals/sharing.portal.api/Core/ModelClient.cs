

namespace Sharing.Portal.Api
{
    using Sharing.Core;
    using Sharing.Portal.Api.Models;
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Sharing.WeChat.Models;
    using Sharing.Core.Entities;
    using Sharing.Core.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ModelClient
    {
        private readonly IServiceProvider provider = SharingConfigurations.CreateServiceProvider(null);

        public WeChatUserModel Register(RegisterWeChatUserContext context)
        {
            var weChatApi = provider.GetService<IWeChatApi>();
            var wxUserService = provider.GetService<IWxUserService>();
            var info = weChatApi.Decrypt<WeChatUserInfo>(context.Data, context.IV, context.SessionKey);
            var membership = wxUserService.Register(new Core.Models.RegisterWxUserContext()
            {
                AppType = AppTypes.Miniprogram,
                Info = info,
                InvitedBy = context.InvitedBy,
                WxApp = new WxApp()
                {
                    AppId = context.AppId
                }
            });
            return new WeChatUserModel()
            {
                Mobile = membership.Mobile,
                OpenId = membership.OpenId,
                UnionId = info.UnionId
            };
        }


        public SessionWxResponse GetSession(JSCodeApiToken token)
        {
            var service = provider.GetService<IWeChatApi>();
            return service.GetSession(token);
        }

        public IList<MCardModel> GetMCardModels(string mcode)
        {
            var queryString = @"
SELECT 
    CardId,
	merchant.BrandName,
	mcard.Title,
	mcard.Prerogative,
	mcard.LogoUrl
FROM sharing_mcard AS mcard
	INNER JOIN sharing_merchant AS merchant 
		ON mcard.MerchantId=merchant.Id
WHERE merchant.MCode=mcode";
            using (var database = SharingConfigurations.GenerateDatabase(false))
            {
                return database.SqlQuery<MCardModel>(queryString, new { mcode = mcode }).ToList();
            }
        }


        public MCardDetails GetMCardDetails(string appid, string openid, string cardid)
        {
            var queryString = @"
SELECT 
	mcard.BrandName,
    mcard.Title,
    IFNULL(ucard.WxUserId,0) AS Ready,
    mcard.CardId,
    mcard.Prerogative,
    ucard.UserCode,
    IFNULL(ucard.Money,0)/100,
    IFNULL(ucard.RewardMoney,0)/100,
    (
		SELECT Address FROM  `sharing_merchant` AS merchant WHERE merchant.Id = mcard.MerchantId
        LIMIT 1
    ) AS Address
FROM `sharing_wxusercard` AS ucard
	RIGHT JOIN `sharing_mcard` AS mcard
		ON ucard.CardId = mcard.CardId AND mcard.CardId =@pCardid
	LEFT JOIN  `sharing_wxuser` AS wxuser
		ON ucard.WxUserId = wxuser.Id AND wxuser.AppId=@pAppid AND wxuser.OpenId=@pOpenId
WHERE mcard.CardId =@pCardid  
";
            using (var database = SharingConfigurations.GenerateDatabase(false))
            {
                return database.SqlQuerySingleOrDefault<MCardDetails>(queryString, new
                {
                    pAppid = appid,
                    pOpenId = openid,
                    pCardid = cardid
                });
            }
        }
        public List<MCardDetails> GetMCardDetails(string appid, string openid)
        {
            var queryString = @"
SELECT 
	mcard.BrandName,
    mcard.Title,
    IFNULL(ucard.WxUserId,0) AS Ready,
    mcard.CardId,
    mcard.Prerogative,
    ucard.UserCode,
    IFNULL(ucard.Money,0) / 100,
    IFNULL(ucard.RewardMoney,0) / 100,
    (
		SELECT Address FROM  `sharing_merchant` AS merchant WHERE merchant.Id = mcard.MerchantId
        LIMIT 1
    ) AS Address
FROM `sharing_wxusercard` AS ucard
	RIGHT JOIN `sharing_mcard` AS mcard
		ON ucard.CardId = mcard.CardId 
	LEFT JOIN  `sharing_wxuser` AS wxuser
		ON ucard.WxUserId = wxuser.Id AND wxuser.AppId=@pAppId AND wxuser.OpenId=@pOpenId
WHERE wxuser.AppId =@pAppId  AND ucard.UserCode IS NOT NULL;
";
            using (var database = SharingConfigurations.GenerateDatabase(false))
            {
                return database.SqlQuery<MCardDetails>(queryString, new
                {
                    pAppId = appid,
                    pOpenId = openid
                }).ToList();
            }
        }
        public PullWxPayData GenerateUnifiedorder(TopupContext context)
        {
            var service = this.provider.GetService<IWeChatPayService>();
            var api = this.provider.GetService<IWeChatApi>();
            var generator = this.provider.GetService<IRandomGenerator>();
            ////P3 Need to query from cache.
            var payment = service.GetPayment(context.AppId);
            var trade = service.PrepareUnifiedorder(context);
            var data = context.GenerateUnifiedWxPayData(payment.MchId.ToString(), trade.TradeId, payment.PayKey, generator.Genernate());
            var parameter = api.Unifiedorder(data, payment.MchId.ToString());
            parameter.PaySign = parameter.MakeSign(payment.PayKey);

            return new PullWxPayData()
            {
                nonceStr = parameter.NonceStr,
                package = parameter.Package,
                paySign = parameter.PaySign,
                signType = WxPayData.SIGN_TYPE_HMAC_SHA256,
                timeStamp = parameter.TimeStamp.ToString()
            };
        }
    }
}
