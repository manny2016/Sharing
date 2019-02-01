
namespace Sharing.WeChat.Models
{
    using Sharing.Core;
    public class WxPayAttach : IWxMCardId, IWxCardCode, IMCode
    {

        public long TimeStamp { get; set; }

        public string CardId { get; set; }

        public string UserCode { get; set; }

        public string NonceStr { get; set; }
        /// <summary>
        /// 支付签名
        /// </summary>
        public string Paysign { get; set; }

        public string MCode { get; set; }
    }
}
