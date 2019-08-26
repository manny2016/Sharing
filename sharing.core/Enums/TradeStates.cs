using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    [Flags]
    public enum TradeStates
    {
        /// <summary>
        /// 新的交易
        /// </summary>
        Newly = 1,
        /// <summary>
        /// 已取消
        /// </summary>
        Canceled = 2,
        /// <summary>
        /// 已支付
        /// </summary>
        HavePay = 4,
        /// <summary>
        /// 支付确认
        /// </summary>
        AckPay = 8,
        /// <summary>
        /// 制作中
        /// </summary>
        Marking = 16,
        /// <summary>
        /// 制作完成
        /// </summary>
        Ready = 32,
        /// <summary>
        /// 已交付
        /// </summary>        
        Delivered = 64,

    }
}
