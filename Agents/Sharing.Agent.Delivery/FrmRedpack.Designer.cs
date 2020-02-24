namespace Sharing.Agent.Delivery {
	partial class FrmRedpack {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if ( disposing && (components != null) ) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
			this.lv_OrderDetals = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.SuspendLayout();
			// 
			// lv_OrderDetals
			// 
			this.lv_OrderDetals.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17,
            this.columnHeader18});
			this.lv_OrderDetals.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lv_OrderDetals.FullRowSelect = true;
			this.lv_OrderDetals.GridLines = true;
			listViewGroup1.Header = "ListViewGroup";
			listViewGroup1.Name = "listViewGroup1";
			this.lv_OrderDetals.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
			this.lv_OrderDetals.HideSelection = false;
			this.lv_OrderDetals.Location = new System.Drawing.Point(0, 0);
			this.lv_OrderDetals.Name = "lv_OrderDetals";
			this.lv_OrderDetals.Size = new System.Drawing.Size(800, 450);
			this.lv_OrderDetals.TabIndex = 2;
			this.lv_OrderDetals.UseCompatibleStateImageBehavior = false;
			this.lv_OrderDetals.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "名称";
			this.columnHeader1.Width = 120;
			// 
			// columnHeader15
			// 
			this.columnHeader15.Text = "鼓励金";
			this.columnHeader15.Width = 120;
			// 
			// columnHeader16
			// 
			this.columnHeader16.Text = "交易金额";
			this.columnHeader16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader16.Width = 97;
			// 
			// columnHeader17
			// 
			this.columnHeader17.Text = "关联交易";
			this.columnHeader17.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader17.Width = 173;
			// 
			// columnHeader18
			// 
			this.columnHeader18.Text = "产生日期";
			this.columnHeader18.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader18.Width = 80;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 428);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(800, 22);
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// FrmRedpack
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.lv_OrderDetals);
			this.Name = "FrmRedpack";
			this.ShowInTaskbar = false;
			this.Text = "鼓励金发放预览";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lv_OrderDetals;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.ColumnHeader columnHeader17;
		private System.Windows.Forms.ColumnHeader columnHeader18;
		private System.Windows.Forms.StatusStrip statusStrip1;
	}
}