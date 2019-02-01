using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Models
{
    public class RegisterCardCoupon : IWeChatMsg
    {
        public string UnionId { get; set; }
        public string AppId { get; set; }
        public string OpenId { get; set; }
        public string CardId { get; set; }
        public string UserCode { get; set; }
        public bool IsGiveByFriend { get; set; }
        public bool IsRestoreMemberCard { get; set; }
        public string FriendOpenId { get; set; }
        public long ActiveTime { get; set; }

        public WeChatMsgTypes MsgType { get; set; }

        public WeChatEventTypes Event { get; set; }
    }
}
