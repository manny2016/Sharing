namespace Sharing.Agent.Delivery.Components
{
    partial class BillingComponent
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
			this.lab_title = new System.Windows.Forms.Label();
			this.lab_subtitle = new System.Windows.Forms.Label();
			this.lab_code = new System.Windows.Forms.Label();
			this.lab_DeliveryType = new System.Windows.Forms.Label();
			this.lab_createdTime = new System.Windows.Forms.Label();
			this.lab_id = new System.Windows.Forms.Label();
			this.list_items = new System.Windows.Forms.ListBox();
			this.lab_CopeWith = new System.Windows.Forms.Label();
			this.lab_paid = new System.Windows.Forms.Label();
			this.lab_paytype = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lab_title
			// 
			this.lab_title.AutoSize = true;
			this.lab_title.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lab_title.Location = new System.Drawing.Point(41, 24);
			this.lab_title.Name = "lab_title";
			this.lab_title.Size = new System.Drawing.Size(138, 21);
			this.lab_title.TabIndex = 0;
			this.lab_title.Text = "柠檬工坊东坡里店";
			this.lab_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lab_subtitle
			// 
			this.lab_subtitle.AutoSize = true;
			this.lab_subtitle.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lab_subtitle.Location = new System.Drawing.Point(81, 59);
			this.lab_subtitle.Name = "lab_subtitle";
			this.lab_subtitle.Size = new System.Drawing.Size(58, 21);
			this.lab_subtitle.TabIndex = 2;
			this.lab_subtitle.Text = "结帐单";
			this.lab_subtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lab_code
			// 
			this.lab_code.AutoSize = true;
			this.lab_code.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lab_code.Location = new System.Drawing.Point(19, 101);
			this.lab_code.Name = "lab_code";
			this.lab_code.Size = new System.Drawing.Size(70, 16);
			this.lab_code.TabIndex = 3;
			this.lab_code.Text = "POS机器: 01";
			// 
			// lab_DeliveryType
			// 
			this.lab_DeliveryType.AutoSize = true;
			this.lab_DeliveryType.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lab_DeliveryType.Location = new System.Drawing.Point(140, 101);
			this.lab_DeliveryType.Name = "lab_DeliveryType";
			this.lab_DeliveryType.Size = new System.Drawing.Size(50, 16);
			this.lab_DeliveryType.TabIndex = 3;
			this.lab_DeliveryType.Text = "配送: {0}";
			// 
			// lab_createdTime
			// 
			this.lab_createdTime.AutoSize = true;
			this.lab_createdTime.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lab_createdTime.Location = new System.Drawing.Point(23, 389);
			this.lab_createdTime.Name = "lab_createdTime";
			this.lab_createdTime.Size = new System.Drawing.Size(56, 16);
			this.lab_createdTime.TabIndex = 3;
			this.lab_createdTime.Text = "时间 :  {0}";
			// 
			// lab_id
			// 
			this.lab_id.AutoSize = true;
			this.lab_id.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lab_id.Location = new System.Drawing.Point(19, 127);
			this.lab_id.Name = "lab_id";
			this.lab_id.Size = new System.Drawing.Size(50, 16);
			this.lab_id.TabIndex = 3;
			this.lab_id.Text = "编号: {0}";
			this.lab_id.Click += new System.EventHandler(this.lab_id_Click);
			// 
			// list_items
			// 
			this.list_items.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.list_items.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.list_items.FormattingEnabled = true;
			this.list_items.ItemHeight = 16;
			this.list_items.Items.AddRange(new object[] {
            "单间                   dd",
            "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -",
            "dsfs",
            "sdf",
            "sdf",
            "sdfsdf",
            "sdf",
            "sdf",
            "sdf",
            "sdfsdf"});
			this.list_items.Location = new System.Drawing.Point(22, 210);
			this.list_items.MultiColumn = true;
			this.list_items.Name = "list_items";
			this.list_items.Size = new System.Drawing.Size(243, 96);
			this.list_items.TabIndex = 4;
			// 
			// lab_CopeWith
			// 
			this.lab_CopeWith.AutoSize = true;
			this.lab_CopeWith.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lab_CopeWith.Location = new System.Drawing.Point(22, 337);
			this.lab_CopeWith.Name = "lab_CopeWith";
			this.lab_CopeWith.Size = new System.Drawing.Size(82, 16);
			this.lab_CopeWith.TabIndex = 5;
			this.lab_CopeWith.Text = "应付 :  ¥ {0}  元";
			// 
			// lab_paid
			// 
			this.lab_paid.AutoSize = true;
			this.lab_paid.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lab_paid.Location = new System.Drawing.Point(148, 337);
			this.lab_paid.Name = "lab_paid";
			this.lab_paid.Size = new System.Drawing.Size(82, 16);
			this.lab_paid.TabIndex = 5;
			this.lab_paid.Text = "实付 :  ¥  {0} 元";
			// 
			// lab_paytype
			// 
			this.lab_paytype.AutoSize = true;
			this.lab_paytype.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lab_paytype.Location = new System.Drawing.Point(22, 363);
			this.lab_paytype.Name = "lab_paytype";
			this.lab_paytype.Size = new System.Drawing.Size(75, 16);
			this.lab_paytype.TabIndex = 3;
			this.lab_paytype.Text = "支付 :  已支付";
			// 
			// BillingComponent
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.lab_paid);
			this.Controls.Add(this.lab_CopeWith);
			this.Controls.Add(this.list_items);
			this.Controls.Add(this.lab_DeliveryType);
			this.Controls.Add(this.lab_id);
			this.Controls.Add(this.lab_paytype);
			this.Controls.Add(this.lab_createdTime);
			this.Controls.Add(this.lab_code);
			this.Controls.Add(this.lab_subtitle);
			this.Controls.Add(this.lab_title);
			this.Name = "BillingComponent";
			this.Size = new System.Drawing.Size(286, 435);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_title;
        private System.Windows.Forms.Label lab_subtitle;
        private System.Windows.Forms.Label lab_code;
        private System.Windows.Forms.Label lab_DeliveryType;
        private System.Windows.Forms.Label lab_createdTime;
        private System.Windows.Forms.Label lab_id;
        private System.Windows.Forms.ListBox list_items;
        private System.Windows.Forms.Label lab_CopeWith;
        private System.Windows.Forms.Label lab_paid;
        private System.Windows.Forms.Label lab_paytype;
    }
}
