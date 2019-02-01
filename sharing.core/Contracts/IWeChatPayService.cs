using Sharing.Core.Entities;
using Sharing.Core.Models;
using Sharing.WeChat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core
{
    public interface IWeChatPayService
    {
        /// <summary>
        /// 为微信支付统一下单生成本地交易
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Trade PrepareUnifiedorder(TopupContext context, out WxPayAttach attach);
        /// <summary>
        /// 获取支付账户
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        Payment GetPayment(string appid);

        Trade GetTradeByTradeId(string tradeId);

        

    }
}
