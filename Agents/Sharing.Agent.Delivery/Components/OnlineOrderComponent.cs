
using System.Windows.Forms;
using Sharing.Core.Models;
using System.Linq;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Sharing.Agent.Delivery.Components
{
    public partial class OnlineOrderComponent : UserControl
    {
        public OnlineOrder OrderContext { get; private set; }
        public bool Selected  { get; private set; }
        public OnlineOrderComponent(OnlineOrder context)
            : this()
        {
            this.OrderContext = context;
        }
        public OnlineOrderComponent()
        {
            InitializeComponent();
            this.Selected = false;            
            this.Click += OnlineOrderComponent_Click;
            this.panel1.Click += OnlineOrderComponent_Click;
            this.Load += OnlineOrderComponent_Load;
        }

        private void OnlineOrderComponent_Click(object sender, System.EventArgs e)
        {
            this.Selected = !this.Selected;
            this.BackColor = this.Selected ? Color.Red : Color.Green;
            this.BorderStyle = this.Selected ? BorderStyle.Fixed3D : BorderStyle.None;         
        }

        private void OnlineOrderComponent_Load(object sender, System.EventArgs e)
        {
            if (this.OrderContext != null)
            {
                this.lab_delivery.Text = this.OrderContext.Delivery == Core.DeliveryTypes.BySelf ? "店内消费" : "外送";
                this.lab_name.Text = string.IsNullOrEmpty(this.OrderContext.Name) ? "" : $"{this.OrderContext.Name.Substring(0, 1)}**";
                this.lab_phone.Text = string.IsNullOrEmpty(this.OrderContext.Mobile)
                    ? ""
                    : $"{this.OrderContext.Mobile.Substring(0, 3)}****{this.OrderContext.Mobile.Substring(7, 4)}";
                this.lab_des.Text = $"{string.Join(",", this.OrderContext.Items.Select(o => o.Product))}";
                this.lab_money.Text = this.OrderContext.Items.Sum(o =>
                {
                    return o.Money;
                }).ToString("0.00元");
            }
        }
    }
}
