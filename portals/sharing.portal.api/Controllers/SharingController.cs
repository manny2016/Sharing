


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
                new string[] { context.AppID, context.OpenId, context.CardId },
                new string[] { "AppId", "OpenId", "CardId" });
            var model = client.GetMCardDetails(context.AppID, context.OpenId, context.CardId);
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
            Logger.Info(context.SerializeToJson());
            Guard.ArgumentNotNull(context, "context");
            Guard.ArgumentNotNullOrEmpty(
                new string[] { context.AppID, context.OpenId },
                new string[] { "AppId", "OpenId" });
            return client.GetMCardDetails(context.AppID, context.OpenId);
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
            Logger.DebugFormat("Received message pushed by WeChat:\r\n Url:{0}\r\n{1}",
                this.HttpContext.Request.QueryString.Value,
                body);
            if (this.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {

                this.HttpContext.Response.Body.Write(echostr.ToBytes());
            }
            else
            {

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
                new string[] { context.MCode, context.OpenId, context.AppId, context.CardId, context.Code },
                new string[] { "MCode", "OpenId", "AppId", "CardId", "Code" }
            );
            return client.GenerateUnifiedorder(context);
        }

        /// <summary>
        /// 接收支付结果通知
        /// </summary>
        /// <remarks>
        /// 用  api/enjoy/paynotify 是个历史遗留问题
        /// </remarks>
        [Route("api/enjoy/PayNotify")]
        [HttpGet]
        [HttpPost]
        public void PayNotify()
        {
            try
            {
                var body = this.Request.Body.ReadAsStringAsync();
                Logger.DebugFormat("Obtain WeChat pay notification:\r\n{0}", body);
                var notify = this.Request.Body.ReadAsStringAsync().DeserializeFromXml<PayNotification>();
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
        public void Test()
        {
            Logger.Info("this is a test");
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            this.Response.Body.Write(
               (string.IsNullOrEmpty(environmentName) ? "environment is null" : environmentName).ToBytes());


            //
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
            return client.GetSharedPyramid(basic);
        }
        /// <summary>
        /// 领取会员卡
        /// </summary>
        /// <param name="context"></param>
        [Route("api/enjoy/GenerateCardExtString")]
        [HttpPost]
        public CardExtModel ApplyMCard(ApplyMCardContext context)
        {

            Logger.DebugFormat("context", context.SerializeToJson());
            Guard.ArgumentNotNull(context, "context");
            Guard.ArgumentNotNullOrEmpty(
                new string[] { context.AppId, context.CardId, context.OpenId },
                new string[] { "AppId", "CardId", "OpenId" });

            
            var result = client.PrepareCardSign(context);
            Logger.DebugFormat("resut", result.SerializeToJson());
            return result;
        }
    }
}
