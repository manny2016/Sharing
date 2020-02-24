


namespace Sharing.Agent.Delivery.Components {
	using System.Windows.Forms;
	using Sharing.Core.Models;
	using System.Linq;
	using System.Drawing;
	using System.Collections.Generic;
	using Sharing.Core;
	using System.Text;
	using Sharing.Agent.Delivery.Models;
	using System;
	using System.Drawing.Printing;

	public delegate void OnlineOrderChangedEventHandler(object sender, OnlineOrder order);

	public partial class OnlineOrderComponent : UserControl {
		public event OnlineOrderChangedEventHandler OnlineOrderChanged;
		public OnlineOrder OrderContext { get; private set; }


		public bool Selected { get; private set; }
		public OnlineOrderComponent(OnlineOrder context)
			: this() {
			this.OrderContext = context;
		}
		public OnlineOrderComponent() {
			InitializeComponent();
			this.Selected = false;
			this.panel1.Click += OnlineOrderComponent_Click;
			this.Load += OnlineOrderComponent_Load;
		}

		private void OnlineOrderComponent_Click(object sender, System.EventArgs e) {
			this.Selected = !this.Selected;
			this.panel_left_side.BackColor = this.Selected ? Color.Red : Color.Green;
			this.BorderStyle = this.Selected ? BorderStyle.Fixed3D : BorderStyle.None;
			this.OnClick(e);
		}

		private void OnlineOrderComponent_Load(object sender, System.EventArgs e) {
			if ( this.OrderContext != null ) {
				this.lab_Code.Text = this.OrderContext.TradeCode;
				this.lab_delivery.Text = this.OrderContext.Delivery == Sharing.Core.DeliveryTypes.BySelf ? "店内消费" : "外送";
				this.lab_name.Text = string.IsNullOrEmpty(this.OrderContext.Name) ? "" : $"{this.OrderContext.Name.Substring(0, 1)}**";
				this.lab_phone.Text = string.IsNullOrEmpty(this.OrderContext.Mobile)
					? ""
					: $"{this.OrderContext.Mobile.Substring(0, 3)}****{this.OrderContext.Mobile.Substring(7, 4)}";
				this.lab_des.Text = $"{string.Join(",", this.OrderContext.Items.Select(o => o.Product))}";
				this.lab_money.Text = this.OrderContext.Items.Sum(o => {
					return o.Money / 100;
				}).ToString("0.00元");
				this.lab_state.Text = this.OrderContext.State.GenernateTradeStateString();
				this.lab_createdtime.Text = this.OrderContext.CreatedDateTime?.ToString("yyyy-MM-dd HH:mm:ss");

			}
		}
		/// <summary>
		/// 开始制作
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tbtn_marking_Click(object sender, System.EventArgs e) {
			this.UpgradeTradeState(TradeStates.Marking);
		}
		/// <summary>
		/// 制作完成,并交付
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsbtn_done_Click(object sender, System.EventArgs e) {
			if ( (this.OrderContext.State & TradeStates.Ready) == TradeStates.Ready ) {
				this.UpgradeTradeState(TradeStates.Delivered);
				
			} else {
				this.UpgradeTradeState(TradeStates.Ready);
			}
			this.OnlineOrderChanged?.Invoke(this, this.OrderContext);
		}
		private void UpgradeTradeState(TradeStates state) {
			var url = string.Format(Sharing.Agent.Delivery.Constants.API, "UpgradeTradeState");
			var result = url.GetUriJsonContent<TradeStateResponse>((http) => {
				var data = new {
					TradeId = this.OrderContext.TradeId,
					TradeState = state
				};
				http.Method = "POST";
				http.ContentType = "application/json";
				using ( var stream = http.GetRequestStream() ) {
					var body = data.SerializeToJson();
					var buffers = UTF8Encoding.UTF8.GetBytes(body);
					stream.Write(buffers, 0, buffers.Length);
					stream.Flush();
				}
				return http;
			});
			this.lab_state.Text = result.Data.GenernateTradeStateString();
			this.OrderContext.State = result.Data;
			
		}
		/// <summary>
		/// 重新打印
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsbtn_print_Click(object sender, System.EventArgs e) {
			this.OrderContext.PrintAsync();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolStripButton2_Click(object sender, System.EventArgs e) {
			if ( (this.OrderContext.State & TradeStates.Marking) != TradeStates.Marking &&
				(this.OrderContext.State & TradeStates.HavePay) == TradeStates.HavePay ) {
				if ( MessageBox.Show("订单已支付,真的要取消吗?", "友情提示",
					MessageBoxButtons.YesNo, MessageBoxIcon.Question)
					== DialogResult.Yes ) {
					this.UpgradeTradeState(TradeStates.Canceled);
					this.Parent.Controls.Remove(this);
				}
			} else if ( ((this.OrderContext.State & TradeStates.Marking) == TradeStates.Marking) ||
				  ((this.OrderContext.State & TradeStates.Ready) == TradeStates.Ready) ) {
				this.UpgradeTradeState(TradeStates.Ready | TradeStates.Delivered);
				this.Parent.Controls.Remove(this);
			}
			this.OnlineOrderChanged?.Invoke(this, this.OrderContext);
		}
	}
}
