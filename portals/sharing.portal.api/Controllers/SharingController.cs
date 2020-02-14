


namespace Sharing.Portal.Api
{
    using Microsoft.AspNetCore.Mvc;
    using Sharing.Portal.Api;
    using Sharing.Portal.Api.Models;
    using Sharing.Core;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using Sharing.WeChat.Models;
    using System.Collections.Generic;
    using Sharing.Core.Models;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore;
    using System.Web;

    [Produces("application/json")]
    [ApiController]
    public class SharingController : ControllerBase
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(SharingController));
        private readonly ModelClient client;

        public SharingController(
            ModelClient client)
        {
            this.client = client;
        }
        /// <summary>
        /// 注册微信用户，提供给小程序的用户注册 API
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("api/sharing/Register")]
        [HttpPost]
        public WeChatUserModel Register(RegisterWeChatUserContext context)
        {
            Logger.Info(context.SerializeToJson());
            return client.Register(context);
        }
        /// <summary>
        /// 更新用户分享信息关联关系信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("api/sharing/UpgradeSharedPyramid")]
        [HttpPost]
        public bool UpgradeSharedPyramid(SharingContext context)
        {
            if (context.Current == null || context.SharedBy == null) return false;

            if (context.SharedBy.AppId != context.Current.AppId)
                throw new ArgumentNullException("Current.AppId must be equals to SharedBy.AppId ");

            if (context.SharedBy.OpenId == context.Current.OpenId)
                return false;
            Logger.Info(context.SerializeToJson());
            return client.UpgradeSharedPyramid(context) > 0;
        }

        /// <summary>
        /// 获取微信session
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [Route("api/sharing/GetSession")]
        [HttpPost]
        public SessionWxResponse GetSession(JSCodeApiToken token)
        {
            return client.GetSession(token);
        }

        /// <summary>
        /// 查询会员卡
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [Route("api/sharing/QueryMCards")]
        [HttpPost]
        public IList<MCardModel> QueryMCards(MerchantKey key)
        {
            return client.GetMCardModels(key.MCode);
        }
        /// <summary>
        /// 获取会员卡详情
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>

        [Route("api/sharing/QueryMCardDetails")]
        [HttpPost]
        public MCardDetails QueryMCardDetails(QueryMyMCardContext context)
        {
            Guard.ArgumentNotNull(context, "context");
            Guard.ArgumentNotNullOrEmpty(
                new string[] { context.AppId, context.OpenId, context.CardId },
                new string[] { "AppId", "OpenId", "CardId" });
            var model = client.GetMCardDetails(context.AppId, context.OpenId, context.CardId);
            return model;
        }

        /// <summary>
        /// 查询用户会员卡详情
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("api/sharing/QueryMyMCardDetails")]
        [HttpPost]
        public IList<MCardDetails> QueryMyMCardDetails(QueryMyMCardContext context)
        {
            Guard.ArgumentNotNull(context, "context");
            Guard.ArgumentNotNullOrEmpty(
                new string[] { context.AppId, context.OpenId },
                new string[] { "AppId", "OpenId" });
            return client.GetMCardDetails(context.AppId, context.OpenId);
        }
        /// <summary>
        /// 微信消息推送 接收 api
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="openid"></param>
        /// <param name="encrypt_type"></param>
        /// <param name="msg_signature"></param>
        /// <param name="echostr"></param>
        /// <remarks>
        /// 用 api/enjoy/WxBizMsg 是个历史遗留问题
        /// </remarks>
        [Route("api/enjoy/WxBizMsg")]
        [HttpPost]
        [HttpGet]
        public void WxBizMsg(string signature = null,
            string timestamp = null,
            string nonce = null,
            string openid = null,
            string encrypt_type = null,
            string msg_signature = null,
            string echostr = null)
        {
            var body = this.HttpContext.Request.Body.ReadAsStringAsync();
            Logger.DebugFormat(this.HttpContext.Request.QueryString.ToUriComponent());
            Logger.Debug(body);

            if (this.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {

                this.HttpContext.Response.Body.Write(echostr.ToBytes());
            }
            else
            {
                var encryptMsg = body.DeserializeFromXml<WeChatEncryptMsg>();
                this.client.ProccessWeChatMsg(new WxMsgToken(msg_signature, timestamp, nonce, body, encryptMsg.ToUserName));

            }
        }

        /// <summary>
        /// 创建支付订单并返回拉起微信支付所需要的参数
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("api/sharing/GenerateUnifiedorderforTopup")]
        [HttpPost]
        public PullWxPayData GenerateUnifiedorderforTopup(TopupContext context)
        {
            Guard.ArgumentNotNull(context, "context");
            Guard.ArgumentNotNullOrEmpty(
                new string[] { context.MCode, context.CardId, context.UserCode },
                new string[] { "MCode", "CardId", "UserCode" }
            );
            return client.GenerateUnifiedorder(context);
        }

        [Route("api/sharing/GenerateUnifiedorderforOrder")]
        [HttpPost]
        public PullWxPayData GenerateUnifiedorderforOrder(OrderContext context)
        {
            Guard.ArgumentNotNull(context, "context");
            Guard.ArgumentNotNullOrZero(context.Details, "content.orders");
            return client.GenerateUnifiedorder(context);

        }
        /// <summary>
        /// 接收支付结果通知
        /// </summary>
        /// <remarks>     
        /// </remarks>
        [Route("api/sharing/PayNotify")]
        [HttpGet]
        [HttpPost]
        public void PayNotify()
        {
            try
            {
                var body = this.Request.Body.ReadAsStringAsync();
                Logger.DebugFormat("Obtain WeChat pay notification:\r\n{0}", body);
                var notify = body.DeserializeFromXml<PayNotification>();
                client.TodoPayNotify(notify);
                var str = @"
<xml>
    <return_code><![CDATA[SUCCESS]]></return_code>
    <return_msg><![CDATA[OK]]></return_msg>
</xml>";
                this.Response.Body.Write(str.ToBytes());
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                var str = @"
<xml>
    <return_code><![CDATA[Fail]]></return_code>
    <return_msg><![CDATA[NO]]></return_msg>
</xml>";
                this.Response.Body.Write(str.ToBytes());
            }
        }


        [Route("api/sharing/Test")]
        [HttpGet]
        [HttpPost]
        public void Test()
        {
			
            //Logger.Info("this is a test");
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            this.Response.Body.Write(
               (string.IsNullOrEmpty(environmentName) ? "environment is null" : environmentName).ToBytes());           
        }

        /// <summary>
        /// 获取特定用户的分享关联关系
        /// </summary>
        /// <param name="basic"></param>
        /// <returns></returns>
        [Route("api/sharing/GetSharedPyramid")]
        [HttpPost]
        public ISharedPyramid GetSharedPyramid(WxUserKey basic)
        {
            var pyrmaid = client.GetSharedPyramid(basic);
            return pyrmaid;
        }

        /// <summary>
        /// 领取会员卡
        /// </summary>
        /// <param name="context"></param>
        [Route("api/sharing/ApplyMCard")]
        [HttpPost]
        public CardExtModel ApplyMCard(ApplyMCardContext context)
        {
            Logger.DebugFormat("context", context.SerializeToJson());
            Guard.ArgumentNotNull(context, "context");
            Guard.ArgumentNotNullOrEmpty(
                new string[] { context.MCode, context.CardId },
                new string[] { "MCode", "CardId" });
            return client.PrepareCardSign(context);
        }

        /// <summary>
        /// 登记卡券
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [Route("api/sharing/RegisterCardCoupon")]
        [HttpPost]
        public bool RegisterCardCoupon(RegisterCardCouponContext context)
        {
            client.RegisterCardCoupon(context);
            return true;
        }

        [Route("api/sharing/CreateOrUpdateCardCoupon")]
        [HttpPost]
        public void CreateOrUpdateCardCoupon(CreateOrUpdateCardCouponContext context)
        {
            client.CreateOrUpdateCoupon(context, context.Body);
        }

        [Route("api/sharing/Synchronous")]
        [HttpPost]
        public void Synchronous()
        {
            client.Synchronous();
        }

        [Route("api/sharing/ClreaAllCardCoupon")]
        [HttpPost]
        public void ClreaAllCardCoupon()
        {
            client.DeleteAllCoupon();
        }

        [Route("api/sharing/GetHotSalesProducts")]
        [HttpPost]
        public IList<List<ProductModel>> GetHotSalesProducts(MerchantKey key)
        {
            Guard.ArgumentNotNull(key, "merchantkey");
            Guard.ArgumentNotNullOrEmpty(key.MCode, "mcode");
            return client.GetHotSalesProducts(key);
        }
        [Route("api/sharing/GetProductTreeNodeModels")]
        [HttpPost]
        public ProductTreeNodeModel[] GetProductTreeNodeModels(MerchantKey key)
        {
            Guard.ArgumentNotNull(key, "merchantkey");
            Guard.ArgumentNotNullOrEmpty(key.MCode, "mcode");
            return client.GetProductTreeNodeModels(key);
        }

        [Route("api/sharing/GetProductDetails")]
        [HttpPost]
        public ProductModel GetProductDetails(SharingPrimaryKey key)
        {
            Guard.ArgumentNotNull(key.Id, "key");
            return client.GetProductDetails(key.Id);
        }
        [Route("api/sharing/QueryOnlineOrders")]
        [HttpPost]
        public OnlineOrder[] QueryOnlineOrders(OnineOrderQueryFilter filter)
        {
            Guard.ArgumentNotNull(filter, "filter");
            if (filter.Keys == null || filter.Keys.Length.Equals(0))
            {
                filter.Start = filter.Start ?? DateTime.Now.Date;
                filter.End = filter.End ?? DateTime.Now.Date.AddDays(1);
            }
            
            filter.Type = filter.Type ?? TradeTypes.Consume;          
            return client.QueryOnlineOrders(filter);
        }

        [Route("api/sharing/UpgradeTradeState")]
        [HttpPost]
        public dynamic UpgradeTradeState(TradeStateContext context)
        {
            return new
            {
                state = client.UpgradeTradeState(context.TradeId, context.TradeState)
            };
        }
    }
}
