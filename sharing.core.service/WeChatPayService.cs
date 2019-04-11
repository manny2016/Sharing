
namespace Sharing.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Sharing.Core.Entities;
    using Sharing.Core.Models;
    using Sharing.WeChat.Models;
    using Microsoft.Extensions.DependencyInjection;
    using Sharing.Core;
    public class WeChatPayService : IWeChatPayService
    {
        private readonly IWeChatUserService wxUserService;
        private readonly IRandomGenerator generator;
        public WeChatPayService(IWeChatUserService wxUserService, IRandomGenerator generator)
        {
            this.wxUserService = wxUserService;
            this.generator = generator;
        }

        public Trade PrepareUnifiedorder(TopupContext context, out WxPayAttach attach)
        {

            var queryString = @"
    INSERT INTO 
    `sharing_trade`(WxUserId,WxOrderId,TradeId,TradeType,TradeState,Money,RealMoney,CreatedTime,Attach, Strategy)
    SELECT 
    (SELECT WxUserId FROM `sharing_wxuser_identity` WHERE AppId=@pAppId AND OpenId=@pOpenId LIMIT 1) AS WxUserId,
    @pWxOrderId AS WxOrderId,
    @pTradeId AS TradeId,
    'Recharge' AS TradeType,
    'Waiting' AS TradeState,
    @pMoney AS Money,
    @pRealMoney AS RealMoney,
    @pCreatedTime AS CreatedTime,
    @pAttach AS Attach,
    @pStrategy AS Strategy;
    UPDATE `sharing_trade` SET TradeId=CONCAT(@prefix , LPAD(Id,10,'0')) WHERE WxOrderId = @pWxOrderId;
    SELECT * FROM `sharing_trade` WHERE WxOrderId = @pWxOrderId LIMIT 1;
";
            //var pyramid = this.wxUserService.GetSharedContext(context as IWxUserKey)
            //    .BuildSharedPyramid(context as IWxUserKey, out long basicWxUserId);
            attach = new WxPayAttach()
            {
                CardId = context.CardId,
                NonceStr = this.generator.Genernate(),
                TimeStamp = DateTime.UtcNow.ToUnixStampDateTime(),
                UserCode = context.UserCode,
                MCode = context.MCode
            };
            context.Money = context.Money * 100;
            attach.Sign(context.Money);
            using (var database = SharingConfigurations.GenerateDatabase(true))
            {
                return database.SqlQuerySingleOrDefaultTransaction<Trade>(queryString, new
                {
                    pAppId = context.AppId,
                    pOpenId = context.OpenId,
                    pWxOrderId = Guid.NewGuid().ToString().Replace("-", string.Empty),
                    pTradeId = Guid.NewGuid().ToString().Replace("-", string.Empty),
                    pMoney = context.Money,
                    pRealMoney = context.Money + (context.Money * 0.2),
                    pCreatedTime = DateTime.UtcNow.ToUnixStampDateTime(),
                    pStrategy = "{}",
                    pAttach = attach.SerializeToJson(),
                    prefix = string.Format("T{0}", DateTime.UtcNow.ToString("yyyyMMdd"))
                });
            }
        }


        public Payment GetPayment(string appid)
        {
            var queryString = "SELECT `Payment` FROM `sharing_mwechatapp` WHERE AppId=@AppId;";
            using (var database = SharingConfigurations.GenerateDatabase(false))
            {
                var app = database.SqlQuerySingleOrDefault<MWeChatApp>(queryString, new { AppId = appid });
                return app.Payment.DeserializeToObject<Payment>();
            }
        }

        public Trade GetTradeByTradeId(string tradeId)
        {
            var queryString = "SELECT * FROM `sharing_trade` WHERE `TradeId` =@tradeId";
            using (var database = SharingConfigurations.GenerateDatabase(false))
            {
                return database.SqlQuerySingleOrDefault<Trade>(queryString, new { tradeId = tradeId });
            }
        }

        public Trade PrepareUnifiedorder(OrderContext context, out WxPayAttach attach)
        {
            var queryString = @"
    INSERT INTO 
    `sharing_trade`(WxUserId,WxOrderId,TradeId,TradeType,TradeState,Money,RealMoney,CreatedTime,Attach, Strategy)
    SELECT 
    (SELECT WxUserId FROM `sharing_wxuser_identity` WHERE AppId=@pAppId AND OpenId=@pOpenId LIMIT 1) AS WxUserId,
    @pWxOrderId AS WxOrderId,
    @pTradeId AS TradeId,
    'Consume' AS TradeType,
    'Waiting' AS TradeState,
    @pMoney AS Money,
    @pRealMoney AS RealMoney,
    @pCreatedTime AS CreatedTime,
    @pAttach AS Attach,
    @pStrategy AS Strategy;
    UPDATE `sharing_trade` SET TradeId=CONCAT(@prefix , LPAD(Id,10,'0')) WHERE WxOrderId = @pWxOrderId;
    SELECT * FROM `sharing_trade` WHERE WxOrderId = @pWxOrderId LIMIT 1;
";
            //var pyramid = this.wxUserService.GetSharedContext(context as IWxUserKey)
            //    .BuildSharedPyramid(context as IWxUserKey, out long basicWxUserId);
            attach = new WxPayAttach()
            {
                //CardId = context.CardId,
                NonceStr = this.generator.Genernate(),
                TimeStamp = DateTime.UtcNow.ToUnixStampDateTime(),
                //UserCode = context.UserCode,
                //MCode = context.MCode
            };
            //context.Money = context.Totalfee.ToString();
            attach.Sign(context.Totalfee ?? 0);
            using (var database = SharingConfigurations.GenerateDatabase(true))
            {
                return database.SqlQuerySingleOrDefaultTransaction<Trade>(queryString, new
                {
                    pAppId = context.AppId,
                    pOpenId = context.OpenId,
                    pWxOrderId = Guid.NewGuid().ToString().Replace("-", string.Empty),
                    pTradeId = Guid.NewGuid().ToString().Replace("-", string.Empty),
                    pMoney = context.Totalfee,
                    pRealMoney = context.Totalfee,
                    pCreatedTime = DateTime.UtcNow.ToUnixStampDateTime(),
                    pStrategy = "{}",
                    pAttach = context.SerializeToJson(),
                    prefix = string.Format("T{0}", DateTime.UtcNow.ToString("yyyyMMdd"))
                });
            }
        }
    }
}
