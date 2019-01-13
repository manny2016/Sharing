

namespace Sharing.Portal.Api.Models
{
    using Sharing.Core.Models;
    using Sharing.Core;
    using Newtonsoft.Json;
    /// <summary>
    /// 卡券签名上下文
    /// </summary>
    public class ApplyMCardContext : IWxMCardId, IMCode
    {

        public string MCode { get; set; }

        public string CardId { get; set; }

        
    }
}
