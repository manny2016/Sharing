

namespace Sharing.Portal.Api.Models
{
    using Sharing.Core;
    using Newtonsoft.Json;
    public class QueryMyMCardContext : IWxAppId, IWxMCardId, IWxUserOpenId
    {

        public string AppId { get; set; }



        public string OpenId { get; set; }



        public string CardId { get; set; }
    }
}
