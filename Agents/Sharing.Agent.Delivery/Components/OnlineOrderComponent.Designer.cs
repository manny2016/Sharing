namespace Sharing.Agent.Delivery.Components
{
    partial class OnlineOrderComponent
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cms_action = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cms_accept = new System.Windows.Forms.ToolStripMenuItem();
            this.重新打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lab_Code = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lab_delivery = new System.Windows.Forms.Label();
            this.lab_des = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lab_name = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lab_phone = new System.Windows.Forms.Label();
            this.lab_state = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lab_money = new System.Windows.Forms.Label();
            this.cms_action.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cms_action
            // 
            this.cms_action.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cms_accept,
            this.重新打印ToolStripMenuItem});
            this.cms_action.Name = "cms_action";
            this.cms_action.Size = new System.Drawing.Size(123, 48);
            // 
            // cms_accept
            // 
            this.cms_accept.Name = "cms_accept";
            this.cms_accept.Size = new System.Drawing.Size(122, 22);
            this.cms_accept.Text = "接单";
            // 
            // 重新打印ToolStripMenuItem
            // 
            this.重新打印ToolStripMenuItem.Name = "重新打印ToolStripMenuItem";
            this.重新打印ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.重新打印ToolStripMenuItem.Text = "重新打印";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lab_money);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lab_state);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lab_phone);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lab_name);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lab_des);
            this.panel1.Controls.Add(this.lab_delivery);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lab_Code);
            this.panel1.Location = new System.Drawing.Point(21, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(322, 169);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "单号:";
            // 
            // lab_Code
            // 
            this.lab_Code.AutoSize = true;
            this.lab_Code.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_Code.ForeColor = System.Drawing.Color.Green;
            this.lab_Code.Location = new System.Drawing.Point(60, 13);
            this.lab_Code.Name = "lab_Code";
            this.lab_Code.Size = new System.Drawing.Size(62, 19);
            this.lab_Code.TabIndex = 0;
            this.lab_Code.Text = "单号:006";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(151, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "类型:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "描述:";
            // 
            // lab_delivery
            // 
            this.lab_delivery.AutoSize = true;
            this.lab_delivery.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_delivery.ForeColor = System.Drawing.Color.Green;
            this.lab_delivery.Location = new System.Drawing.Point(195, 13);
            this.lab_delivery.Name = "lab_delivery";
            this.lab_delivery.Size = new System.Drawing.Size(35, 19);
            this.lab_delivery.TabIndex = 7;
            this.lab_delivery.Text = "外送";
            // 
            // lab_des
            // 
            this.lab_des.AutoSize = true;
            this.lab_des.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_des.ForeColor = System.Drawing.Color.Green;
            this.lab_des.Location = new System.Drawing.Point(62, 103);
            this.lab_des.Name = "lab_des";
            this.lab_des.Size = new System.Drawing.Size(108, 19);
            this.lab_des.TabIndex = 8;
            this.lab_des.Text = "奶茶3姐妹等多件";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "姓名:";
            // 
            // lab_name
            // 
            this.lab_name.AutoSize = true;
            this.lab_name.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_name.ForeColor = System.Drawing.Color.Green;
            this.lab_name.Location = new System.Drawing.Point(63, 43);
            this.lab_name.Name = "lab_name";
            this.lab_name.Size = new System.Drawing.Size(62, 19);
            this.lab_name.TabIndex = 10;
            this.lab_name.Text = "单号:006";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(153, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 19);
            this.label6.TabIndex = 11;
            this.label6.Text = "电话:";
            // 
            // lab_phone
            // 
            this.lab_phone.AutoSize = true;
            this.lab_phone.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_phone.ForeColor = System.Drawing.Color.Green;
            this.lab_phone.Location = new System.Drawing.Point(195, 46);
            this.lab_phone.Name = "lab_phone";
            this.lab_phone.Size = new System.Drawing.Size(35, 19);
            this.lab_phone.TabIndex = 12;
            this.lab_phone.Text = "电话";
            // 
            // lab_state
            // 
            this.lab_state.AutoSize = true;
            this.lab_state.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_state.ForeColor = System.Drawing.Color.Green;
            this.lab_state.Location = new System.Drawing.Point(63, 72);
            this.lab_state.Name = "lab_state";
            this.lab_state.Size = new System.Drawing.Size(48, 19);
            this.lab_state.TabIndex = 14;
            this.lab_state.Text = "已支付";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(19, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 19);
            this.label7.TabIndex = 13;
            this.label7.Text = "状态:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(151, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 19);
            this.label8.TabIndex = 15;
            this.label8.Text = "金额:";
            // 
            // lab_money
            // 
            this.lab_money.AutoSize = true;
            this.lab_money.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_money.ForeColor = System.Drawing.Color.Green;
            this.lab_money.Location = new System.Drawing.Point(195, 73);
            this.lab_money.Name = "lab_money";
            this.lab_money.Size = new System.Drawing.Size(48, 19);
            this.lab_money.TabIndex = 16;
            this.lab_money.Text = "已支付";
            // 
            // OnlineOrderComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.Controls.Add(this.panel1);
            this.Name = "OnlineOrderComponent";
            this.Size = new System.Drawing.Size(343, 169);
            this.cms_action.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip cms_action;
        private System.Windows.Forms.ToolStripMenuItem cms_accept;
        private System.Windows.Forms.ToolStripMenuItem 重新打印ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lab_Code;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lab_des;
        private System.Windows.Forms.Label lab_delivery;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lab_phone;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lab_name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lab_money;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lab_state;
        private System.Windows.Forms.Label label7;
    }
}
