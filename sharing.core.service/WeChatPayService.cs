
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
		private readonly IDatabaseFactory databaseFactory;
        public WeChatPayService(IWeChatUserService wxUserService, 
			IRandomGenerator generator,
			IDatabaseFactory databaseFactory)
        {
            this.wxUserService = wxUserService;
            this.generator = generator;
			this.databaseFactory = databaseFactory;
        }

        public Trade PrepareUnifiedorder(TopupContext context, out WxPayAttach attach)
        {
            
            var queryString = $@"
    INSERT INTO 
    [dbo].[Trade]([MerchantId],[WxUserId],[TradeCode],[WxOrderId],[TradeId],[TradeType],
[TradeState],[Money],[RealMoney],[CreatedDateTime],[Attach], [Strategy])
    VALUES( 
    @pMerhantId,
    (SELECT TOP 1 Id FROM [dbo].[WxUser] WHERE AppId=@pAppId AND OpenId=@pOpenId ) AS WxUserId,
    (SELECT COUNT(Id) + 1 FROM [dbo].[Trade] WHERE CreatedDateTime / 86400 * 86400 = CONVERT(BIGINT,DATEDIFF(S,'1970-01-01',SYSDATETIME())) / 86400 * 86400) AS [TradeCode],
    @pWxOrderId AS WxOrderId,
    @pTradeId AS TradeId,
    {(int)TradeTypes.Recharge} AS TradeType,
    {(int)TradeStates.HavePay} AS TradeState,
    @pMoney AS Money,
    @pRealMoney AS RealMoney,
    DATEDIFF(S,'1970-01-01',SYSUTCDATETIME()) AS CreatedDateTime,
    @pAttach AS Attach,
    @pStrategy AS Strategy);
    UPDATE [dbo].[Trade] SET TradeId=CONCAT(@prefix , [dbo].[funLpad](Id,10,'0')) WHERE WxOrderId = @pWxOrderId;
    SELECT TOP 1 *  FROM [dbo].[Trade] WHERE WxOrderId = @pWxOrderId;
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
            using (var database = this.databaseFactory.GenerateDatabase(isWriteOnly:true))
            {
                return database.SqlQuerySingleOrDefaultTransaction<Trade>(queryString, new
                {
                    @pAppId = context.AppId,
					@pMerhantId = context.MerchantId,
                    @pOpenId = context.OpenId,
                    @pWxOrderId = Guid.NewGuid().ToString().Replace("-", string.Empty),
                    @pTradeId = Guid.NewGuid().ToString().Replace("-", string.Empty),
                    @pMoney = context.Money,
                    @pRealMoney = context.Money + (context.Money * 0.2),                    
                    @pStrategy = "{}",
                    @pAttach = attach.SerializeToJson(),
                    @prefix = string.Format("T{0}", DateTime.UtcNow.ToString("yyyyMMdd"))
                });
            }
        }


        public Payment GetPayment(string appid)
        {
            var queryString = "SELECT [Payment] FROM [dbo].[MWeChatApp]  (NOLOCK) WHERE AppId=@AppId;";
            using (var database = this.databaseFactory.GenerateDatabase(isWriteOnly:false))
            {
                var app = database.SqlQuerySingleOrDefault<MWeChatApp>(queryString, new { AppId = appid });
                return app.Payment.DeserializeToObject<Payment>();
            }
        }

        public Trade GetTradeByTradeId(string tradeId)
        {
            var queryString = "SELECT * FROM [dbo].[Trade]  (NOLOCK) WHERE [TradeId] =@tradeId";
            using (var database = this.databaseFactory.GenerateDatabase(isWriteOnly: false) )
            {
                return database.SqlQuerySingleOrDefault<Trade>(queryString, new { tradeId = tradeId });
            }
        }

        public Trade PrepareUnifiedorder(OrderContext context, out WxPayAttach attach)
        {
            var queryString = $@"
    INSERT INTO 
    [dbo].[Trade]([MerchantId],[WxUserId],[TradeCode],[WxOrderId],[TradeId],[TradeType],[TradeState],[Money],[RealMoney],[CreatedDateTime],[Attach], [Strategy])
    SELECT  
    @pMchId,
    (SELECT TOP 1 Id FROM [dbo].[WxUser] WHERE AppId=@pAppId AND OpenId=@pOpenId ) AS WxUserId,
    (SELECT COUNT(Id) + 1 FROM [dbo].[Trade] WHERE CreatedDateTime / 86400 * 86400 = CONVERT(BIGINT,DATEDIFF(S,'1970-01-01',SYSDATETIME())) / 86400 * 86400) AS [Code],
    @pWxOrderId AS WxOrderId,
    @pTradeId AS TradeId,
    {(int)TradeTypes.Consume} AS TradeType,
    {(int)TradeStates.Newly} AS TradeState,
    @pMoney AS Money,
    @pRealMoney AS RealMoney,
    DATEDIFF(S,'1970-01-01',SYSUTCDATETIME())  AS CreatedDateTime,
    @pAttach AS Attach,
    @pStrategy AS Strategy;
    UPDATE [dbo].[Trade] SET TradeId=CONCAT(@prefix , [dbo].[funLpad](Id,10,'0')) WHERE WxOrderId = @pWxOrderId;
    SELECT TOP 1 * FROM [dbo].[Trade] WHERE WxOrderId = @pWxOrderId ;
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
            attach.Sign(context.Money );
            
            using (var database = this.databaseFactory.GenerateDatabase(isWriteOnly: true) )
            {
                return database.SqlQuerySingleOrDefaultTransaction<Trade>(queryString, new
                {
                    @pAppId = context.AppId,
                    @pOpenId = context.OpenId,
                    @pMchId = context.MerchantId,
                    @pWxOrderId = Guid.NewGuid().ToString().Replace("-", string.Empty),
                    @pTradeId = Guid.NewGuid().ToString().Replace("-", string.Empty),
                    @pMoney = context.Money,
                    @pRealMoney = context.Money,                    
                    @pStrategy = "{}",					
					@pAttach = context.SerializeToJson(),
                    @prefix = string.Format("T{0}", DateTime.UtcNow.ToString("yyyyMMdd"))
                });
            }
        }
    }
}
