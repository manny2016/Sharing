



namespace Sharing.Core
{
    using Sharing.Core.Models;
    using Sharing.WeChat.Models;
    using System;

    public static class WeChatMessageExtension
    {
        public static RegisterCardCoupon Convert(this IWeChatMsg message, string appid)
        {
            var getcard = message as GetCardCouponWeChatMsg;
            Guard.ArgumentNotNull(getcard, "getcard");
            return new RegisterCardCoupon()
            {
                AppId = appid,
                ActiveTime = DateTime.UtcNow.ToUnixStampDateTime(),
                CardId = getcard.CardId,
                Event = getcard.Event,
                FriendOpenId = getcard.FriendUserName,
                IsGiveByFriend = getcard.IsGiveByFriend,
                IsRestoreMemberCard = getcard.IsRestoreMemberCard,
                MsgType = getcard.MsgType,
                OpenId = getcard.FromUserName,
                UnionId = getcard.UnionId,
                UserCode = getcard.UserCardCode
            };
        }
    }
}
