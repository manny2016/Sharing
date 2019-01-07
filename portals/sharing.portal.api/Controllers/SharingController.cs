


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

    [Produces("application/json")]
    [ApiController]
    public class SharingController : ControllerBase
    {

        private readonly ModelClient client = new ModelClient();

        [Route("api/sharing/Register")]
        [HttpPost]
        public WeChatUserModel Register(RegisterWeChatUserContext context)
        {
            return client.Register(context);
        }

        [Route("api/sharing/GetSession")]
        [HttpPost]
        public SessionWxResponse GetSession(JSCodeApiToken token)
        {
            return client.GetSession(token);
        }


        [Route("api/sharing/QueryMCards")]
        [HttpPost]
        public IList<MCardModel> QueryMCards(MerchantKey key)
        {
            return client.GetMCardModels(key.MCode);
        }


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
        [Route("api/sharing/QueryMyMCardDetails")]
        [HttpPost]
        public IList<MCardDetails> QueryMyMCardDetails(QueryMyMCardContext context)
        {
            Guard.ArgumentNotNull(context, "context");
            Guard.ArgumentNotNullOrEmpty(
                new string[] { context.AppID, context.OpenId },
                new string[] { "AppId", "OpenId" });
            return client.GetMCardDetails(context.AppID, context.OpenId);
        }

        [Route("api/sharing/WxBizMsg")]
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
            if (this.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {
                this.HttpContext.Response.Body.Write(echostr.ToBytes());
            }
            else
            {

            }
        }



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

        [Route("api/enjoy/PayNotify")]
        [HttpGet]
        [HttpPost]
        public void PayNotify()
        {

        }

    }
}
