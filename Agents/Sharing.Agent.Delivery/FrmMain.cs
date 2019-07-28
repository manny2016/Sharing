

namespace Sharing.Agent.Delivery
{
    using Sharing.Agent.Delivery.Components;
    using Sharing.Core.Models;
    using Sharing.Core;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Text;
    using System.Linq;
    using Sharing.Core.CMQ;
    using System.Threading;
    using System.Drawing;

    public delegate void OnlineOrderComingDelegate(OnlineOrder model);
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            this.Load += FrmMain_Load;

        }

        private async void FrmMain_Load(object sender, EventArgs e)
        {
            this.InitializeNewOnlineOrders();
            this.InitializeMarkingOrders();
            this.InitializeCompletedOrders();

            ThreadPool.QueueUserWorkItem((state) =>
            {
                var client = state as TencentCMQClient<OnlineOrder>;
                client.Initialize();
                client.Monitor((model) =>
                {
                    OnlineOrderComingCallback(model);
                    client.DeleteMessage(model);
                });
            }, TencentCMQClientFactory.Create<OnlineOrder>("lemon"));


        }
        private void OnlineOrderComingCallback(OnlineOrder model)
        {
            if (this.flp_NewOrder.InvokeRequired)
            {
                var callback = new OnlineOrderComingDelegate(this.OnlineOrderComingCallback);
                this.flp_NewOrder.Invoke(callback, model);
            }
            else
            {
                this.flp_NewOrder.Controls.Add(new OnlineOrderComponent(model));
            }
        }
        private async Task<OnlineOrder[]> QueryOnineOrders(TradeStates state)
        {
            var url = $"https://www.yourc.club/api/sharing/QueryOnlineOrders";
            var result = url.GetUriJsonContent<OnlineOrder[]>((http) =>
            {
                var queryFilter = new OnineOrderQueryFilter()
                {
                    MchId = 1,
                    State = state,
                    Type = TradeTypes.Consume,
                    Start = DateTime.Now.AddYears(-10)
                };
                http.Method = "POST";
                http.ContentType = "application/json";
                using (var stream = http.GetRequestStream())
                {
                    var body = queryFilter.SerializeToJson();
                    var buffers = UTF8Encoding.UTF8.GetBytes(body);
                    stream.Write(buffers, 0, buffers.Length);
                    stream.Flush();
                }
                return http;
            });
            return result;
        }
        private async void InitializeNewOnlineOrders()
        {
            var results = await this.QueryOnineOrders(TradeStates.HavePay);
            foreach (var order in results.OrderByDescending(o => o.Code))
            {
                this.flp_NewOrder.Controls.Add(new OnlineOrderComponent(order));
            }
        }
        private async void InitializeMarkingOrders()
        {
            this.lv_marking.Items.Clear();
            this.lv_marking.Groups.Clear();
            this.lv_marking.ItemSelectionChanged += Lv_marking_ItemSelectionChanged;
            this.lv_marking.Click += Lv_marking_Click;
            var results = await this.QueryOnineOrders(TradeStates.HavePay);
            foreach (var order in results.OrderByDescending(o => o.Code))
            {
                this.LoadOnlinOrder(lv_marking, order);
            }
        }

        private void Lv_marking_Click(object sender, EventArgs e)
        {
            //var selected= ((ListView)sender).SelectedItems;
            //foreach(var item in selected)
            //{
            //    var selectedListViewItem = item as ListViewItem;                
            //    var backColor = selectedListViewItem.Selected ? Color.AliceBlue : Color.White;
            //    var forcColor = selectedListViewItem.Selected ? Color.White : Color.Black;
            //    selectedListViewItem.BackColor = backColor;
            //    selectedListViewItem.ForeColor = forcColor;
            //    foreach (var itemInSameGroup in selectedListViewItem.Group.Items)
            //    {
            //        var viewItem = itemInSameGroup as ListViewItem;
            //        if (!selectedListViewItem.Text.Equals(viewItem.Text))
            //        {
            //            viewItem.Selected = selectedListViewItem.Selected;
            //            viewItem.BackColor = backColor;
            //            viewItem.ForeColor = forcColor;
            //        }
            //    }            
            //}            
        }

        private void Lv_marking_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
        
        }

        private async void InitializeCompletedOrders()
        {
            this.lv_completed.Items.Clear();
            this.lv_completed.Groups.Clear();
            var results = await this.QueryOnineOrders(TradeStates.Delivered);
            foreach (var order in results.OrderByDescending(o => o.Code))
            {
                this.LoadOnlinOrder(lv_completed, order);
            }
        }
        private void LoadOnlinOrder(ListView listView, OnlineOrder order)
        {
            var delivery = order.Delivery == DeliveryTypes.BySelf ? "堂食" : "外送";
            var group = new ListViewGroup($"单号:{order.Code};配送方式:{delivery}");
            group.Tag = order.Code;
            listView.Groups.Add(group);
            foreach (var item in order.Items)
            {
                var listItem = new ListViewItem(item.Product);
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.Option));
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.Price.ToString("￥0.00元")));
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.Count.ToString()));
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.Money.ToString("￥0.00元")));
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, "制作中"));
                listItem.Group = group;                
                listView.Items.Add(listItem);
                //group.Items.Add(listItem);
                

            }
            listView.Groups.Add(group);
        }
    }
}
