

namespace Sharing.Agent.Delivery
{
    using Sharing.Agent.Delivery.Components;
    using Sharing.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    public static class OnlineOrderExtension
    {
        //public static string ToStringforPrintingDetails(this OnlineOrder order)
        //{
        //    var delivery = order.Delivery == Sharing.Core.DeliveryTypes.Takeout ? "外送" : "自取";
        //    var address = order.Delivery == Sharing.Core.DeliveryTypes.BySelf ? "店内" : order.Address;
        //    return string.Concat(
        //        $"单号:{order.Code}\r\n",
        //        $"姓名:{order.Name}\r\n",
        //        $"电话:{order.Mobile}\r\n",
        //        string.Join("\t\t", new string[] { "名称", "规格", "单价", "数量" }),
        //        "\r\n",
        //        string.Join("\r\n", order.Items.Select(o =>
        //        {
        //            return $"{o.Product}\t\t{o.Option}\t{o.Price}元\t{o.Count}";
        //        })),
        //        $"\r\n类型:{delivery}\r\n",
        //        $"地址:{address}");
        //}
        //public static string ToStringforPrintingOrderCode(this OnlineOrder order)
        //{
        //    return order.Code;
        //}
        //public static string ToStringforTakeout(this OnlineOrder order)
        //{
        //    return string.Empty;
        //}
        public static IEnumerable<ListViewGroup> Where(this ListViewGroupCollection collection, Func<ListViewGroup, bool> func)
        {
            foreach (var group in collection)
            {
                var listViewGroup = group as ListViewGroup;
                if (func(listViewGroup))
                {
                    yield return listViewGroup;
                }
            }
        }
        public static IEnumerable<ListViewItem> Select(this ListView.ListViewItemCollection collection, Func<ListViewItem, ListViewItem> func)
        {
            foreach (var item in collection)
            {
                yield return func(item as ListViewItem);
            }
        }
        public static void Remove(this IEnumerable<ListViewGroup> target, ListView source)
        {
            var array = target.ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                source.Groups.Remove(array[i]);
            }
        }
        public static void Remove(this IEnumerable<ListViewItem> target, ListView source)
        {
            var array = target.ToArray();

            for (int i = 0; i < array.Length; i++)
            {
                source.Items.Remove(array[i]);
            }
        }

        private static bool PrintBilling(this OnlineOrder order, string printer)
        {
            var billing = new BillingComponent(order);
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = printer;
            pd.PrinterSettings.DefaultPageSettings.PaperSize = new PaperSize("Bill", 317, 470);
            pd.PrintPage += (sender, ev) =>
            {
                foreach (var item in billing.GenernatePrintItems())
                {
                    ev.Graphics.DrawString(item.Text, item.Font, Brushes.Black, new PointF(item.Point.X, item.Point.Y));
                }
            };
            pd.Print();
            return true;
        }
        private static bool PrintOrderCode(this OnlineOrder order, string printer)
        {
            var index = 1;
            var count = order.Items.Sum(o => o.Count);
            foreach (var item in order.Items)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    var memo = new MemoComponent(order.TradeCode,item.Product,item.Option,index, count);
                    PrintDocument doc = new PrintDocument();
                    doc.PrintPage += (sender, ev) =>
                    {
                        foreach (var print in memo.GenernatePrintItems())
                        {
                            ev.Graphics.DrawString(print.Text, print.Font, Brushes.Black, new PointF(print.Point.X, print.Point.Y));
                        }
                    };
                    index++;
                }
            }
            return true;
        }
        public async static void PrintAsync(this OnlineOrder order)
        {
            order.PrintBilling(Settings.Create().BillingPrinter);
            order.PrintOrderCode(Settings.Create().OrderCodePrinter);
        }
    }
}
