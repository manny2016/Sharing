using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sharing.Agent.Delivery
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
            this.Load += FrmSettings_Load;
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            foreach (var printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                var printnerName = printer.ToString();
                var ridobtn1 = new RadioButton() { Text = printer.ToString() };
                if (printnerName.Equals(Settings.Create().BillingPrinter))
                {
                    ridobtn1.Checked = true;
                }

                var ridobtn2 = new RadioButton() { Text = printer.ToString() };
                if (printnerName.Equals(Settings.Create().OrderCodePrinter))
                {
                    ridobtn2.Checked = true;
                }

                this.flowLayoutPanel1.Controls.Add(ridobtn1);
                this.flowLayoutPanel2.Controls.Add(ridobtn2);
            }
            this.chb_autoprint.Checked = Settings.Create().Autoprint;
            this.tb_restApi.Text = Settings.Create().API;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            var settings = Settings.Create();
            settings.API = this.tb_restApi.Text;
            settings.Autoprint = this.chb_autoprint.Checked;
            foreach (var control in this.flowLayoutPanel1.Controls)
            {
                var rdio = control as RadioButton;
                if (rdio != null && rdio.Checked)
                {
                    settings.BillingPrinter = rdio.Text;
                    break;
                }
            }
            foreach (var control in this.flowLayoutPanel2.Controls)
            {
                var rdio = control as RadioButton;
                if (rdio != null && rdio.Checked)
                {
                    settings.OrderCodePrinter = rdio.Text;
                    break;
                }
            }
            settings.Save();
            this.Close();
        }
    }
}
