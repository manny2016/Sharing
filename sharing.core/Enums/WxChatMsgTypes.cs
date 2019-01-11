

namespace Sharing.Core
{
    public enum WxChatMsgTypes
    {
        Nothing = -1,
        /// <summary>
        /// 卡券审核通过
        /// </summary>
        card_pass_check = 1,
        /// <summary>
        /// 卡券审核不通过
        /// </summary>
        card_not_pass_check = 2,
        /// <summary>
        /// 领取
        /// </summary>
        user_get_card = 3,
        /// <summary>
        /// 转赠
        /// </summary>
        user_gifting_card = 4,
        /// <summary>
        /// 用户删除卡券事件
        /// </summary>
        user_del_card = 5,
        /// <summary>
        /// 核销卡券事件
        /// </summary>
        user_consume_card = 6,
        /// <summary>
        /// 买单事件
        /// </summary>
        user_pay_from_pay_cell = 7,
        /// <summary>
        /// 进入会员卡事件
        /// </summary>
        user_view_card = 8,
        /// <summary>
        /// 从卡券进入公众号会话事件推送
        /// </summary>
        user_enter_session_from_card = 9,
        /// <summary>
        /// 会员卡内容更新事件
        /// </summary>
        update_member_card = 10,
        /// <summary>
        /// 库存预警
        /// </summary>
        card_sku_remind = 11,
        /// <summary>
        /// 券点流水详情事件
        /// </summary>
        card_pay_order = 12,
        /// <summary>
        /// 会员卡激活事件推送
        /// </summary>
        submit_membercard_user_info = 13,
        /// <summary>
        /// 子商户审核事件
        /// </summary>
        card_merchant_check_result = 14,
    }
}
