using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Entities
{
    public class Trade
    {
        /// <summary>
        /// 交易流水号
        /// </summary>
        public virtual long Id { get; set; }
        /// <summary>
        /// 微信用户编号
        /// </summary>
        public virtual long WxUserId { get; set; }
        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public virtual string WxOrderId { get; set; }
        /// <summary>
        /// 系统交易号
        /// </summary>
        public virtual string TradeId { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public virtual TradeTypes TradeType { get; set; }
        /// <summary>
        /// 交易状态
        /// </summary>
        public virtual TradeStates TradeState { get; set; }
        /// <summary>
        /// 交易金额 单位分
        /// </summary>
        public virtual int Money { get; set; }
        /// <summary>
        /// 实际获得金额
        /// </summary>
        public virtual int RealMoney { get; set; }
        /// <summary>
        /// 交易创建时间
        /// </summary>
        public virtual long CreatedTime { get; set; }
        /// <summary>
        /// 交易确认时间
        /// </summary>
        public virtual long? ConfirmTime { get; set; }
        public virtual string Attach { get; set; }
        /// <summary>
        /// 使用的营销策略
        /// </summary>
        public virtual string Strategy { get; set; }
    }
}
