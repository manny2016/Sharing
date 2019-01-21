
namespace Sharing.WeChat.Models
{
    using Sharing.Core;
    using System.Xml.Serialization;

    [XmlRoot("xml")]
    public class WeChatEncryptMsg : IWeChatEncryptMsg
    {
        public string ToUserName { get; set; }

        public string Encrypt { get; set; }        
    }
}
