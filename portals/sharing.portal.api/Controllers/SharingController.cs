


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


        [Route("api/sharing/QueryMyMCard")]
        [HttpPost]
        public MyMCardModel QueryMyMCard(QueryMyMCardContext context)
        {
            var model = client.GetMyMCard(context.AppID, context.OpenId, context.CardId);
            return model;
        }
    }
}
