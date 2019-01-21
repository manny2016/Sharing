

namespace Sharing.WeChat.Models
{
    using Sharing.Core;
    using System.Xml.Serialization;
    [XmlRoot("xml")]
    public class GetCardCouponWeChatMsg : IWeChatMsg
    {
        /// <summary>
        /// 领券方帐号（一个OpenID）
        /// </summary>
        public string FromUserName { get; set; }

        public string ToUserName { get; set; }

        public long CreateTime { get; set; }

        public WeChatMsgTypes MsgType { get; set; }

        public WeChatEventTypes Event { get; set; }

        public string CardId { get; set; }

        public bool IsGiveByFriend { get; set; }

        public string UserCardCode { get; set; }

        public string FriendUserName { get; set; }

        public int OuterId { get; set; }
        /// <summary>
        /// 旧卡Code
        /// </summary>
        public string OldUserCardCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OuterStr { get; set; }
        /// <summary>
        /// 是否是恢复的会员卡
        /// </summary>
        public bool IsRestoreMemberCard { get; set; }
        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。
        /// </summary>
        public string UnionId { get; set; }
    }
}
