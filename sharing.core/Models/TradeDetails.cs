using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Models
{
    public class TradeDetails
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual long Id { get; set; }
        /// <summary>
        /// 交易单号（本平台）
        /// </summary>
        public virtual string TradeId { get; set; }
        /// <summary>
        /// 微信支付单号
        /// </summary>
        public virtual string OrderId { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public virtual TradeTypes Type { get; set; }
        /// <summary>
        /// 所属Appid
        /// </summary>
        public virtual string AppId { get; set; }
        /// <summary>
        /// 交易相关 用户 OpenId
        /// </summary>
        public virtual string OpenId { get; set; }
        /// <summary>
        /// 会员卡Id
        /// </summary>
        public virtual string CardId { get; set; }
        /// <summary>
        /// 会员卡用户 code
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 微信支付商户号
        /// </summary>
        public virtual string MchId { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public virtual TradeStates State { get; set; }
        /// <summary>
        /// 交易金额 单位分
        /// </summary>
        public virtual int Money { get; set; }
        /// <summary>
        /// 实得金额
        /// </summary>
        public virtual int RealMoeny { get; set; }
        /// <summary>
        /// 交易创建时间
        /// </summary>
        public virtual long CreatedTime { get; set; }
        /// <summary>
        /// 交易确认时间
        /// </summary>
        public virtual long? ConfirmTime { get; set; }
        /// <summary>
        /// 交易描述
        /// </summary>
        public virtual string Description { get; set; }
    }
}
