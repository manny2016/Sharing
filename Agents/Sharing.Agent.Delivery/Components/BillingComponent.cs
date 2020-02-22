


namespace Sharing.Agent.Delivery.Components
{
    using System.Windows.Forms;
    using Sharing.Agent.Delivery.Models;
    using Sharing.Core.Models;
    using System.Linq;
    using System.Collections.Generic;
    using Sharing.Agent.Delivery.Core.Models;
    using System.Drawing;

    public partial class BillingComponent : UserControl
    {
        private readonly OnlineOrder OnlineOrderContext;
        public BillingComponent(OnlineOrder order)
        {
            this.OnlineOrderContext = order;
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            this.lab_title.Text = "柠檬工坊东坡里店";
            this.lab_subtitle.Text = "结帐单";
            this.lab_id.Text = string.Format(this.lab_id.Text, this.OnlineOrderContext.TradeId);
            ;
            Point? lastPoint;
            foreach (var label in this.SplitAddressAndDisplay(string.Format("交付地址:{0}", this.OnlineOrderContext.Delivery == Sharing.Core.DeliveryTypes.BySelf
                ? "本店"
                : this.OnlineOrderContext.Address), out lastPoint))
            {
                this.Controls.Add(label);
            }

            this.list_items.Top = lastPoint == null ? this.list_items.Top : lastPoint.Value.Y + 30;
            this.lab_code.Text = string.Format(this.lab_code.Text, this.OnlineOrderContext.TradeCode);

            this.lab_DeliveryType.Text = string.Format(this.lab_DeliveryType.Text,
                this.OnlineOrderContext.Delivery == Sharing.Core.DeliveryTypes.BySelf ? "自提" : "外送");

            this.list_items.ItemHeight = 25;
            this.list_items.Items.Clear();

            this.list_items.Items.Add(string.Join("\t", new string[] { "名称", "数量", "单价", "总额" }));
            this.list_items.Items.Add("- - - - - - - - - - - - - - - - - - - - - - - - ");
            foreach (var item in this.OnlineOrderContext.Items)
            {
                this.list_items.Items.Add($"{item.Product}({item.Option.TrimEnd(',')})");
                var text = string.Join("\t", new string[] { item.Count.ToString(), item.Price.ToString("0.00"), item.Money.ToString("0.00") });
                this.list_items.Items.Add($"\t{text}");
            }
            this.list_items.Height = this.list_items.Items.Count * this.list_items.ItemHeight + 10;

            var yValueNextList = this.list_items.Location.Y + this.list_items.Height + 10;

            this.lab_CopeWith.Text = string.Format(this.lab_CopeWith.Text, this.OnlineOrderContext.Total?.ToString("0.00"));
            this.lab_CopeWith.Location = new Point(this.lab_CopeWith.Left, yValueNextList);

            this.lab_disfromfriend.Text = string.Format(this.lab_disfromfriend.Text, "0.00");
            this.lab_disfromfriend.Location = new Point(this.lab_disfromfriend.Left, yValueNextList);

            this.lab_paid.Text = string.Format(this.lab_paid.Text, this.OnlineOrderContext.Total?.ToString("0.00"));
            this.lab_paid.Location = new Point(this.lab_paid.Left, this.lab_disfromfriend.Top + 20);

            this.lab_paytype.Text = string.Format(this.lab_paid.Text, "线上支付");
            this.lab_paytype.Location = new Point(this.lab_paytype.Left, this.lab_disfromfriend.Top + 20);

            this.lab_createdTime.Text = string.Format(this.lab_createdTime.Text, this.OnlineOrderContext.CreatedDateTime?.ToString("yyyy-MM-dd HH:mm:ss"));
            this.lab_createdTime.Location = new Point(this.lab_createdTime.Location.X, this.lab_paid.Location.Y + this.lab_createdTime.Height + 10);
        }

        public IEnumerable<PrintItem> GenernatePrintItems()
        {
            foreach (var item in this.Controls)
            {
                foreach (var printItem in GenernatePrintItemfromLabel(item as Label))
                {
                    yield return printItem;
                }
                foreach (var printItem in GenernatePrintItemfromList(item as ListBox))
                {
                    yield return printItem;
                }
            }
        }
        private IEnumerable<PrintItem> GenernatePrintItemfromLabel(Label label)
        {
            if (label != null)
            {
                yield return new PrintItem()
                {
                    Font = label.Font,
                    Point = label.Location,
                    Text = label.Text
                };
            }
        }
        private IEnumerable<PrintItem> GenernatePrintItemfromList(ListBox list)
        {
            if (list != null)
            {
                var point = list.Location;
                var index = 0;
                foreach (var item in list.Items)
                {
                    yield return new PrintItem()
                    {
                        Font = list.Font,
                        Point = new System.Drawing.Point(list.Left, list.ItemHeight * (index++) + list.Top),
                        Text = item.ToString()
                    };
                }
            }
        }

        public Image GenernateImageforPrinting()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            var memoryImage = new Bitmap(this.Width, this.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
            return memoryImage;
        }
        public List<Label> SplitAddressAndDisplay(string address, out Point? lastPoint)
        {
            var list = new List<Label>();
            lastPoint = null;
            var location = new Point(20, 155);
            var font = new Font("Microsoft YaHei", 9);
            var size = address.ToCharArray()[0].Measure(font);
            var maxWordsInSingleLine = (int)((this.Width - 20) / size.Width);
            var line = 0;
            foreach (var array in address.ToCharArray().Split<char>(maxWordsInSingleLine))
            {
                lastPoint = new Point(20, (int)(size.Height * (line++) + location.Y));
                list.Add(new Label()
                {
                    Font = font,
                    Location = lastPoint ?? new Point(0, 0),
                    Text = string.Join("", array),
                });
            }
            return list;
        }

		private void lab_id_Click(object sender, System.EventArgs e) {

		}
	}
}
