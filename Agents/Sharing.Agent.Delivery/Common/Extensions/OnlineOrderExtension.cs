

namespace Sharing.Agent.Delivery
{
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

        private static bool PrintBilling(this OnlineOrder order,string printer)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = printer;
            pd.PrintPage += (sender, ev) =>
            {
                Font FontNormal = new Font("Verdana", 12);
                Graphics g = ev.Graphics;
                g.DrawString("Your string to print", FontNormal, Brushes.Black, 0, 0, new StringFormat());
            };
            pd.Print();
            return true;
        }
        private static bool PrintOrderCode(this OnlineOrder order,string printer)
        {
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += (sender, ev) =>
            {
                Font FontNormal = new Font("Verdana", 12);
                Graphics g = ev.Graphics;
                g.DrawString("Your string to print", FontNormal, Brushes.Black, 0, 0, new StringFormat());
            };
            return true;
        }
        public async static void PrintAsync(this OnlineOrder order)
        {
            order.PrintBilling(Settings.Create().BillingPrinter);
            order.PrintOrderCode(Settings.Create().OrderCodePrinter);          
        }
    }
}
