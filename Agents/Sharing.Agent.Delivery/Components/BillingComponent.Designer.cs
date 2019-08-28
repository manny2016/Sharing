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
            this.SuspendLayout();
            // 
            // lab_title
            // 
            this.lab_title.AutoSize = true;
            this.lab_title.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_title.Location = new System.Drawing.Point(75, 22);
            this.lab_title.Name = "lab_title";
            this.lab_title.Size = new System.Drawing.Size(138, 22);
            this.lab_title.TabIndex = 0;
            this.lab_title.Text = "柠檬工坊东坡里店";
            this.lab_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lab_subtitle
            // 
            this.lab_subtitle.AutoSize = true;
            this.lab_subtitle.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_subtitle.Location = new System.Drawing.Point(115, 57);
            this.lab_subtitle.Name = "lab_subtitle";
            this.lab_subtitle.Size = new System.Drawing.Size(48, 19);
            this.lab_subtitle.TabIndex = 2;
            this.lab_subtitle.Text = "结帐单";
            this.lab_subtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lab_code
            // 
            this.lab_code.AutoSize = true;
            this.lab_code.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_code.Location = new System.Drawing.Point(20, 101);
            this.lab_code.Name = "lab_code";
            this.lab_code.Size = new System.Drawing.Size(66, 17);
            this.lab_code.TabIndex = 3;
            this.lab_code.Text = "POS号: {0}";
            // 
            // lab_DeliveryType
            // 
            this.lab_DeliveryType.AutoSize = true;
            this.lab_DeliveryType.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_DeliveryType.Location = new System.Drawing.Point(152, 101);
            this.lab_DeliveryType.Name = "lab_DeliveryType";
            this.lab_DeliveryType.Size = new System.Drawing.Size(78, 17);
            this.lab_DeliveryType.TabIndex = 3;
            this.lab_DeliveryType.Text = "支付方式: {0}";
            // 
            // lab_createdTime
            // 
            this.lab_createdTime.AutoSize = true;
            this.lab_createdTime.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_createdTime.Location = new System.Drawing.Point(20, 129);
            this.lab_createdTime.Name = "lab_createdTime";
            this.lab_createdTime.Size = new System.Drawing.Size(86, 17);
            this.lab_createdTime.TabIndex = 3;
            this.lab_createdTime.Text = "时        间: {0}";
            // 
            // lab_id
            // 
            this.lab_id.AutoSize = true;
            this.lab_id.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_id.Location = new System.Drawing.Point(20, 158);
            this.lab_id.Name = "lab_id";
            this.lab_id.Size = new System.Drawing.Size(82, 17);
            this.lab_id.TabIndex = 3;
            this.lab_id.Text = "账  单  号: {0}";
            // 
            // list_items
            // 
            this.list_items.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.list_items.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.list_items.FormattingEnabled = true;
            this.list_items.ItemHeight = 17;
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
            this.list_items.Location = new System.Drawing.Point(23, 247);
            this.list_items.MultiColumn = true;
            this.list_items.Name = "list_items";
            this.list_items.Size = new System.Drawing.Size(272, 102);
            this.list_items.TabIndex = 4;
            // 
            // BillingComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.list_items);
            this.Controls.Add(this.lab_DeliveryType);
            this.Controls.Add(this.lab_id);
            this.Controls.Add(this.lab_createdTime);
            this.Controls.Add(this.lab_code);
            this.Controls.Add(this.lab_subtitle);
            this.Controls.Add(this.lab_title);
            this.Name = "BillingComponent";
            this.Size = new System.Drawing.Size(317, 470);
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
    }
}
