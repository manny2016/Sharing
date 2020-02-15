using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Entities {
	public class Trade {
		/// <summary>
		/// 交易流水号
		/// </summary>
		public long Id { get; set; }
		public long MerchantId { get;set;}
		/// <summary>
		/// 微信用户编号
		/// </summary>
		public long WxUserId { get; set; }
		/// <summary>
		/// 订单编码
		/// </summary>
		public int TradeCode { get; set; }
		/// <summary>
		/// 微信支付订单号
		/// </summary>
		public string WxOrderId { get; set; }
		/// <summary>
		/// 系统交易号
		/// </summary>
		public string TradeId { get; set; }
		/// <summary>
		/// 交易类型
		/// </summary>
		public TradeTypes TradeType { get; set; }
		/// <summary>
		/// 交易状态
		/// </summary>
		public TradeStates TradeState { get; set; }
		/// <summary>
		/// 交易金额 单位分
		/// </summary>
		public int Money { get; set; }
		/// <summary>
		/// 实际获得金额
		/// </summary>
		public int RealMoney { get; set; }
		/// <summary>
		/// 交易创建时间
		/// </summary>
		public long CreatedTime { get; set; }
		/// <summary>
		/// 交易确认时间
		/// </summary>
		public long? ConfirmTime { get; set; }
		public string Attach { get; set; }
		/// <summary>
		/// 使用的营销策略
		/// </summary>
		public string Strategy { get; set; }
	}
}
