
namespace Sharing.WeChat.Models
{
    using Sharing.Core;
    public class WxPayAttach
    {
        /// <summary>
        /// 由谁支付
        /// </summary>
        public long? PayBy { get; set; }

        public string CardId { get; set; }

        public string UserCode { get; set; }
        /// <summary>
        /// 支付签名
        /// </summary>
        public string Paysign { get; set; }
        /// <summary>
        /// 分享信息
        /// </summary>
        public ISharedPyramid SharedPyramid { get; set; }
    }
}
