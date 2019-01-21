
namespace Sharing.Core.Entities
{
    public class MWeChatApp : IWxApp
    {
        public virtual long MerchantId { get; set; }
        public virtual AppTypes AppType { get; set; }
        public virtual string AppId { get; set; }
        public virtual string Secret { get; set; }
        public virtual string Payment { get; set; }

        public string OriginalId { get; set; }
    }
}
