using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sharing.Agent.Delivery {
	public partial class FrmRedpack : Form {
		public FrmRedpack() {
			InitializeComponent();
			this.Load += FrmRedpack_Load;
			
		}

		private void FrmRedpack_Load(object sender, EventArgs e) {
			this.InitializeRewardLogging();
		}

		private void InitializeRewardLogging() {

		}
	}
}
