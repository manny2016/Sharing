

namespace Sharing.Agent.Delivery {
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
	using System.Collections.Generic;

	public delegate void OnlineOrderComingDelegate(OnlineOrder model);
	public partial class FrmMain : Form {
		public FrmMain() {
			InitializeComponent();
			this.Load += FrmMain_Load;

		}

		private async void FrmMain_Load(object sender, EventArgs e) {

			this.InitializeNewOnlineOrders();
			this.InitializeHistoryOnlineOrders();

			//ThreadPool.QueueUserWorkItem((state) => {
			//	var client = state as TencentCMQClient<OnlineOrder>;
			//	client.Initialize();
			//	client.Monitor((model) => {
			//		OnlineOrderComingCallback(model);
			//		client.DeleteMessage(model);
			//	});
			//}, TencentCMQClientFactory.Create<OnlineOrder>("lemon"));

		}
		private void OnlineOrderComingCallback(OnlineOrder model) {
			if ( this.flp_NewOrder.InvokeRequired ) {
				var callback = new OnlineOrderComingDelegate(this.OnlineOrderComingCallback);
				this.flp_NewOrder.Invoke(callback, model);
			} else {
				this.flp_NewOrder.Controls.Add(new OnlineOrderComponent(model));
				if ( Settings.Create().Autoprint ) {
					model.PrintAsync();
				}
			}
		}
		private async Task<OnlineOrder[]> QueryOnineOrders(TradeStates[] includeStates, TradeStates[] excludeStates) {
			var url = string.Format(Constants.API, "QueryOnlineOrders");
			var result = url.GetUriJsonContent<OnlineOrder[]>((http) => {
				var queryFilter = new OnineOrderQueryFilter() {
					MchId = 1,
					States = includeStates,
					ExcludeStates = excludeStates,
					Type = TradeTypes.Consume,
					Start = DateTime.Now.Date.AddDays(-1)
				};
				http.Method = "POST";
				http.ContentType = "application/json";
				using ( var stream = http.GetRequestStream() ) {
					var body = queryFilter.SerializeToJson();
					var buffers = UTF8Encoding.UTF8.GetBytes(body);
					stream.Write(buffers, 0, buffers.Length);
					stream.Flush();
				}
				return http;
			});
			return result;
		}
		private async void InitializeNewOnlineOrders() {
			var results = await this.QueryOnineOrders(
				new TradeStates[] { TradeStates.AckPay },
				new TradeStates[] { TradeStates.Delivered });
			results = results ?? new OnlineOrder[] { };
			foreach ( var order in results.OrderBy(o => o.TradeCode) ) {
				var component = new OnlineOrderComponent(order);
				component.Click += Component_Click;
				component.OnlineOrderCcompletedCompleted += Component_OnlineOrderCcompletedCompleted;
				this.flp_NewOrder.Controls.Add(component);
			}
			this.lv_OrderDetals.Groups.Clear();
			this.lv_OrderDetals.Items.Clear();
		}

		private void Component_OnlineOrderCcompletedCompleted(object sender, OnlineOrder order) {
			this.Controls.Remove(sender as OnlineOrderComponent);
			this.LoadOnlinOrder(this.lv_histories, order);
		}

		private async void InitializeHistoryOnlineOrders() {
			var results = await this.QueryOnineOrders(
			   new TradeStates[] { TradeStates.Delivered }, null);
			this.lv_histories.Groups.Clear();
			this.lv_histories.Items.Clear();
			results = results ?? new OnlineOrder[] { };
			foreach ( var order in results ) {
				LoadOnlinOrder(lv_histories, order);
			}
		}

		private void Component_Click(object sender, EventArgs e) {
			var component = sender as OnlineOrderComponent;
			if ( component.Selected ) {
				LoadOnlinOrder(this.lv_OrderDetals, component.OrderContext);
			} else {
				LoadOnlinOrder(this.lv_OrderDetals, component.OrderContext, "remove");
			}

		}

		public void LoadOnlinOrder(ListView listView, OnlineOrder order, string action = "show") {
			if ( action == "show" ) {
				var delivery = order.Delivery == DeliveryTypes.BySelf ? "堂食" : "外送";
				var group = new ListViewGroup($"单号:{order.TradeCode};配送方式:{delivery}");
				group.Tag = order.TradeCode;
				listView.Groups.Add(group);
				foreach ( var item in order.Items ) {
					var listItem = new ListViewItem(item.Product);
					listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.Option));
					listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.Price.ToString("￥0.00元")));
					listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.Count.ToString()));
					listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, item.Money.ToString("￥0.00元")));
					listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, "制作中"));
					listItem.Group = group;
					listView.Items.Add(listItem);
				}
				listView.Groups.Add(group);
			} else {
				var groups = listView.Groups.Where(o => o.Tag.ToString().Equals(order.TradeCode));
				groups.SelectMany(o => o.Items.Select(t => t)).Remove(listView);
				groups.Remove(listView);
			}
		}

		/// <summary>
		/// 完成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolStripButton2_Click(object sender, EventArgs e) {
			var selected = this.flp_NewOrder.Controls.OfType<OnlineOrderComponent>().Where(o => o.Selected).ToArray();

		}

		private void tsbtn_settings_Click(object sender, EventArgs e) {
			var settings = new FrmSettings();
			settings.ShowDialog();
		}

		private void toolStripButton1_Click(object sender, EventArgs e) {

		}
	}
}
