

namespace Sharing.Agent.Delivery.Components
{
    using Sharing.Agent.Delivery.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    public partial class MemoComponent : UserControl
    {
        public MemoComponent(string code, string prodName, string option, int index, int total)
        {
            InitializeComponent();
            this.lab_code.Text = string.Format(this.lab_code.Text, code);
            this.lab_productName.Text = prodName;
            this.lab_counter.Text = string.Format(this.lab_counter.Text, index, total);
           // this.lab_createdTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            this.lab_option.Text = option;
        }
        public IEnumerable<PrintItem> GenernatePrintItems()
        {
            foreach (var item in this.Controls)
            {
                foreach (var printItem in GenernatePrintItemfromLabel(item as Label))
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
    }
}
