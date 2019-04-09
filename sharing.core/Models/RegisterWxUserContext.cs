

namespace Sharing.Core.Models
{
    using Sharing.WeChat.Models;
    public class RegisterWxUserContext
    {        
        //public string InvitedBy { get; set; }
        public IWxApp WxApp { get; set; }
        public AppTypes AppType { get; set; }
        public WeChatUserInfo Info { get; set; }
    }
}
