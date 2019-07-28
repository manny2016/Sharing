using Sharing.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharing.Agent.Delivery
{
    public static class OnlineOrderExtension
    {
        public static string ToStringforPrintingDetails(this OnlineOrder order)
        {
            var delivery = order.Delivery == Core.DeliveryTypes.Takeout ? "外送" : "自取";
            var address = order.Delivery == Core.DeliveryTypes.BySelf ? "店内" : order.Address;
            return string.Concat(
                $"单号:{order.Code}\r\n",
                $"姓名:{order.Name}\r\n",
                $"电话:{order.Mobile}\r\n",
                string.Join("\t\t", new string[] { "名称", "规格", "单价", "数量" }),
                "\r\n",
                string.Join("\r\n", order.Items.Select(o =>
                {
                    return $"{o.Product}\t\t{o.Option}\t{o.Price}元\t{o.Count}";
                })),
                $"\r\n类型:{delivery}\r\n",
                $"地址:{address}");
        }
        public static string ToStringforPrintingOrderCode(this OnlineOrder order)
        {
            return order.Code;
        }
        public static string ToStringforTakeout(this OnlineOrder order)
        {
            return string.Empty;
        }
    }
}
