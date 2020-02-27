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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRedpack));
			this.lv_RewardLogging = new System.Windows.Forms.ListView();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.全部发放ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.发放选中ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.反选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lv_RewardLogging
			// 
			this.lv_RewardLogging.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17,
            this.columnHeader18});
			this.lv_RewardLogging.ContextMenuStrip = this.contextMenuStrip1;
			this.lv_RewardLogging.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lv_RewardLogging.FullRowSelect = true;
			this.lv_RewardLogging.GridLines = true;
			this.lv_RewardLogging.HideSelection = false;
			this.lv_RewardLogging.Location = new System.Drawing.Point(0, 0);
			this.lv_RewardLogging.Name = "lv_RewardLogging";
			this.lv_RewardLogging.Size = new System.Drawing.Size(1084, 450);
			this.lv_RewardLogging.SmallImageList = this.imageList1;
			this.lv_RewardLogging.TabIndex = 2;
			this.lv_RewardLogging.UseCompatibleStateImageBehavior = false;
			this.lv_RewardLogging.View = System.Windows.Forms.View.Details;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 428);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1084, 22);
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "用户昵称";
			this.columnHeader1.Width = 120;
			// 
			// columnHeader18
			// 
			this.columnHeader18.Text = "产生日期";
			this.columnHeader18.Width = 289;
			// 
			// columnHeader17
			// 
			this.columnHeader17.Text = "关联交易";
			this.columnHeader17.Width = 217;
			// 
			// columnHeader16
			// 
			this.columnHeader16.Text = "交易金额";
			this.columnHeader16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader16.Width = 173;
			// 
			// columnHeader15
			// 
			this.columnHeader15.Text = "鼓励金";
			this.columnHeader15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader15.Width = 189;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "里脊肉.jpg");
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全部发放ToolStripMenuItem,
            this.发放选中ToolStripMenuItem,
            this.全选ToolStripMenuItem,
            this.反选ToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(181, 114);
			// 
			// 全部发放ToolStripMenuItem
			// 
			this.全部发放ToolStripMenuItem.Name = "全部发放ToolStripMenuItem";
			this.全部发放ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.全部发放ToolStripMenuItem.Text = "全部发放";
			this.全部发放ToolStripMenuItem.Click += new System.EventHandler(this.全部发放ToolStripMenuItem_Click);
			// 
			// 发放选中ToolStripMenuItem
			// 
			this.发放选中ToolStripMenuItem.Name = "发放选中ToolStripMenuItem";
			this.发放选中ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.发放选中ToolStripMenuItem.Text = "发放选中";
			// 
			// 全选ToolStripMenuItem
			// 
			this.全选ToolStripMenuItem.Name = "全选ToolStripMenuItem";
			this.全选ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.全选ToolStripMenuItem.Text = "全选";
			// 
			// 反选ToolStripMenuItem
			// 
			this.反选ToolStripMenuItem.Name = "反选ToolStripMenuItem";
			this.反选ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.反选ToolStripMenuItem.Text = "反选";
			// 
			// FrmRedpack
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1084, 450);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.lv_RewardLogging);
			this.Name = "FrmRedpack";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "鼓励金发放预览";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lv_RewardLogging;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.ColumnHeader columnHeader17;
		private System.Windows.Forms.ColumnHeader columnHeader18;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem 全部发放ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 发放选中ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 全选ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 反选ToolStripMenuItem;
	}
}