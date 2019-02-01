

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
    using Newtonsoft.Json.Linq;
    using System.Text;
    using Dapper;

    public class ModelClient
    {
        private readonly IWeChatApi wxapi;
        private readonly IWeChatUserService wxUserService;
        private readonly IRandomGenerator generator;
        private readonly IWeChatPayService weChatPayService;
        private readonly ISharingHostService sharingHostService;
        private readonly IMCardService mCardService;
        private readonly IWeChatMsgHandler handler;
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ModelClient));
        public ModelClient(
            IWeChatApi api,
            IWeChatUserService wxUserService,
            IRandomGenerator generator,
            IWeChatPayService payService,
            IMCardService mCardService,
            ISharingHostService hostService,
            IWeChatMsgHandler weChatMsgHandler)
        {
            this.wxapi = api;
            this.sharingHostService = hostService;
            this.wxUserService = wxUserService;
            this.generator = generator;
            this.weChatPayService = payService;
            this.mCardService = mCardService;
            this.handler = weChatMsgHandler;
        }
        public WeChatUserModel Register(RegisterWeChatUserContext context)
        {

            var info = this.wxapi.Decrypt<WeChatUserInfo>(context.Data, context.IV, context.SessionKey);
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
            return this.wxapi.GetSession(token);

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
    IFNULL(ucard.`WxUserId`,0) AS Ready,
    mcard.CardId,
    mcard.Prerogative,
    ucard.UserCode,
    IFNULL(ucard.Money,0)/100 AS Money,
    IFNULL(ucard.RewardMoney,0)/100 AS RewardMoney,
    (
		SELECT Address FROM  `sharing_merchant` AS merchant WHERE merchant.Id = mcard.MerchantId
        LIMIT 1
    ) AS Address
FROM `sharing_wxusercard` AS ucard
	RIGHT JOIN `sharing_mcard` AS mcard
		ON ucard.CardId = mcard.CardId AND mcard.CardId =@pCardid
	LEFT JOIN  `sharing_wxuser_identity` AS wxuser
		ON ucard.`WxUserId` = wxuser.WxUserId AND wxuser.AppId=@pAppid AND wxuser.OpenId=@pOpenId
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
    IFNULL(ucard.Money,0) / 100 AS Money,
    IFNULL(ucard.RewardMoney,0) / 100 AS RewardMoney,
    (
		SELECT Address FROM  `sharing_merchant` AS merchant WHERE merchant.Id = mcard.MerchantId
        LIMIT 1
    ) AS Address
FROM `sharing_wxusercard` AS ucard
	RIGHT JOIN `sharing_mcard` AS mcard
		ON ucard.CardId = mcard.CardId 
	LEFT JOIN  `sharing_wxuser_identity` AS wxuser
		ON ucard.WxUserId = wxuser.WxUserId AND wxuser.AppId=@pAppId AND wxuser.OpenId=@pOpenId
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
            ////P3 Need to query from cache.
            var payment = this.weChatPayService.GetPayment(context.AppId);
            var trade = this.weChatPayService.PrepareUnifiedorder(context, out WxPayAttach attach);
            var data = context.GenerateUnifiedWxPayData(
                payment.MchId.ToString(),
                trade.TradeId,
                payment.PayKey,
                attach.NonceStr,
                attach.Paysign);

            var xml = data.SerializeToXml();
            var parameter = this.wxapi.Unifiedorder(data, payment.MchId.ToString());
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

        public void TodoPayNotify(PayNotification notification)
        {
            Guard.ArgumentNotNull(notification, "notification");
            if (notification.ResultCode.Value.Equals("SUCCESS"))
            {
                var trade = this.weChatPayService.GetTradeByTradeId(notification.OutTradeNo.Value);
                Guard.ArgumentNotNull(trade, "trade");

                if (trade.Money != notification.TotalFee)////P1 TODO: need change to sign verify.
                    throw new WeChatPayException("There is a error happend on transaction to verify.(pay money)");
                if (trade.TradeState != TradeStates.Waiting)
                    throw new WeChatPayException("Only Waitting status pay order can be modify.");

                var attach = trade.Attach.DeserializeToObject<WxPayAttach>();
                var mchid = sharingHostService.MerchantDetails.First(o => o.MCode.Equals(attach.MCode)).Id;
                var pyarmid = this.GetSharedPyramid(new WxUserKey()
                {
                    Id = trade.WxUserId,
                    MchId = mchid
                });
                var executeSqlString = @"spUpgradeforTopupConfirm";
                using (var database = SharingConfigurations.GenerateDatabase(true))
                {
                    var parameters = new Dapper.DynamicParameters();
                    parameters.Add("p_Id", trade.Id, System.Data.DbType.Int64);
                    parameters.Add("p_CardId", attach.CardId, System.Data.DbType.String);
                    parameters.Add("p_UserCode", attach.UserCode, System.Data.DbType.String);
                    parameters.Add("p_Money", trade.RealMoney, System.Data.DbType.Int32);//充值金额 （赠送后的金额）                             
                    parameters.Add("p_confirmTime", DateTime.UtcNow.ToUnixStampDateTime(), System.Data.DbType.Int64);
                    parameters.Add("p_RewardTo", pyarmid.Parent == null ? (int?)null : (int?)pyarmid.Parent.Id, System.Data.DbType.Int64);
                    parameters.Add("p_WxUserId", pyarmid.Id, System.Data.DbType.Int64);
                    parameters.Add("p_RewardMoney", trade.Money * 0.1, System.Data.DbType.Int32);
                    parameters.Add("p_MchId", pyarmid.MchId, System.Data.DbType.Int32);
                    database.Execute(executeSqlString, parameters, System.Data.CommandType.StoredProcedure, true);
                }

            }
        }

        public int UpgradeSharedPyramid(SharingContext context)
        {
            var executeSqlString = @"spUpgradeSharedPyramid";
            using (var database = SharingConfigurations.GenerateDatabase(true))
            {
                var parameters = new Dapper.DynamicParameters();
                parameters.Add("pSharedByOpenId", context.SharedBy.OpenId, System.Data.DbType.String);
                parameters.Add("pSharedByAppId", context.SharedBy.AppId, System.Data.DbType.String);
                parameters.Add("pCurrentAppId", context.Current.AppId, System.Data.DbType.String);
                parameters.Add("pCurrentOpenId", context.Current.OpenId, System.Data.DbType.String);
                return database.Execute(executeSqlString, parameters, System.Data.CommandType.StoredProcedure, true);
            }
        }
        public ISharedPyramid GetSharedPyramid(IWxUserKey basic)
        {
            return this.wxUserService.GetSharedContext(basic)
                .BuildSharedPyramid(basic as IWxUserKey, out long basicWxUserId);
        }

        public CardExtModel PrepareCardSign(ApplyMCardContext context)
        {

            var timestamp = DateTime.UtcNow.ToUnixStampDateTime();
            //var nonceStr = this.generator.Genernate();// Guid.NewGuid().ToString().Replace("-", string.Empty);
            //var nonceStr = this.generator.Genernate();
            var nonceStr = Guid.NewGuid().ToString().Replace("-", string.Empty);
            var official = this.sharingHostService.MerchantDetails.Where(o => o.MCode == context.MCode).SelectMany(o => o.Apps)
                .FirstOrDefault(o => o.AppType == AppTypes.Official);
            var miniprogram = this.sharingHostService.MerchantDetails.Where(o => o.MCode == context.MCode).SelectMany(o => o.Apps)
                .FirstOrDefault(o => o.AppType == AppTypes.Miniprogram);
            var cardsign = new CardExtModel()
            {
                Signature = this.wxapi.GenerateSignForApplyMCard(official, miniprogram, context.CardId, timestamp, nonceStr),
                TimeStamp = timestamp.ToString(),
                NonceStr = nonceStr,
                //CardId = context.CardId
            };


            return cardsign;
        }

        public void CreateOrUpdateCoupon(IMCode mcode, JObject body)
        {
            Guard.ArgumentNotNull(mcode, "mcode");
            Guard.ArgumentNotNull(body, "body");

            var merchant = sharingHostService.MerchantDetails.FirstOrDefault(o => o.MCode.Equals(mcode.MCode));
            Guard.ArgumentNotNull(merchant, "merchant");

            var official = merchant.Apps.FirstOrDefault(o => o.AppType == AppTypes.Official);

            Guard.ArgumentNotNull(official, "official");
            var result = this.wxapi.SaveOrUpdateCardCoupon(official, body);

            if (result.HasError == false)
            {
                this.mCardService.WriteIntoDatabase(new List<MCard>() { body.ParseMCard(merchant.Id, result.CardId) });
            }
        }
        public void Synchronous()
        {
            this.mCardService.Synchronous();
        }

        public void DeleteAllCoupon()
        {
            foreach (var app in this.sharingHostService.MerchantDetails.SelectMany(o => o.Apps))
            {
                var cards = new string[] {
                    "p18KQ51k6hllvEZwFXAx0cGaiohw",
"p18KQ55QuuWfC9NxcM2yKBGAKUbk",
"p18KQ5zUZpRtl3EN6BN_otBkuiJM" };
                foreach (var cardid in cards)
                {
                    this.wxapi.DeleteCardCoupon(app, new MCardDetails() { CardId = cardid });
                }

            }
        }

        public void RegisterCardCoupon(RegisterCardCouponContext context)
        {
            var strBld = new StringBuilder();
            var details = this.sharingHostService.MerchantDetails;
            foreach (var card in context.CardList)
            {
                if (card.IsSuccess)
                {
                    var response = this.wxapi.DecryptMCardUserCode(details.ChooseOfficial(context)
                    , card.EncryptedCode);

                    if (response.HasError == false)
                    {
                        var cardcoupon = new RegisterCardCoupon()
                        {
                            ActiveTime = DateTime.UtcNow.ToUnixStampDateTime(),
                            AppId = context.AppId,
                            CardId = card.CardId,
                            Event = WeChatEventTypes.user_get_card,
                            MsgType = WeChatMsgTypes.@event,
                            FriendOpenId = string.Empty,
                            IsGiveByFriend = false,
                            IsRestoreMemberCard = false,
                            OpenId = context.OpenId,
                            UserCode = response.Code,
                            UnionId = context.UnionId
                        };
                        this.wxUserService.RegisterCardCoupon(cardcoupon);
                    }
                    else
                    {
                        Logger.Error(response.SerializeToJson());
                    }

                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptMsg"></param>
        public void ProccessWeChatMsg(WxMsgToken token)
        {
            var wxapp = sharingHostService.MerchantDetails.SelectMany(o => o.Apps).Where(o => o.OriginalId == token.OriginalId)
                .FirstOrDefault();
            this.handler.Proccess(token, wxapp.AppId);
        }
    }
}
