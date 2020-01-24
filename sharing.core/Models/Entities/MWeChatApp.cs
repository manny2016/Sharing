
namespace Sharing.Core.Entities
{
    public class MWeChatApp : IWxApp
    {
        public  long MerchantId { get; set; }
        public  AppTypes AppType { get; set; }
        public  string AppId { get; set; }
        public  string Secret { get; set; }
        public  string Payment { get; set; }

        public string OriginalId { get; set; }
    }
}
