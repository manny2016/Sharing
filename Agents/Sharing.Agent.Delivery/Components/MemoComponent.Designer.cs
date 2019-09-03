namespace Sharing.Agent.Delivery.Components
{
    partial class MemoComponent
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
            this.label1 = new System.Windows.Forms.Label();
            this.lab_code = new System.Windows.Forms.Label();
            this.lab_counter = new System.Windows.Forms.Label();
            this.lab_productName = new System.Windows.Forms.Label();
            this.lab_createdTime = new System.Windows.Forms.Label();
            this.lab_option = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "柠檬工坊东坡里店";
            // 
            // lab_code
            // 
            this.lab_code.AutoSize = true;
            this.lab_code.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_code.Location = new System.Drawing.Point(17, 44);
            this.lab_code.Name = "lab_code";
            this.lab_code.Size = new System.Drawing.Size(47, 16);
            this.lab_code.TabIndex = 0;
            this.lab_code.Text = "序号:{0}";
            // 
            // lab_counter
            // 
            this.lab_counter.AutoSize = true;
            this.lab_counter.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_counter.Location = new System.Drawing.Point(140, 43);
            this.lab_counter.Name = "lab_counter";
            this.lab_counter.Size = new System.Drawing.Size(41, 16);
            this.lab_counter.TabIndex = 0;
            this.lab_counter.Text = "{0}/{1}";
            // 
            // lab_productName
            // 
            this.lab_productName.AutoSize = true;
            this.lab_productName.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_productName.Location = new System.Drawing.Point(13, 68);
            this.lab_productName.Name = "lab_productName";
            this.lab_productName.Size = new System.Drawing.Size(107, 26);
            this.lab_productName.TabIndex = 0;
            this.lab_productName.Text = "柠檬西游多";
            // 
            // lab_createdTime
            // 
            this.lab_createdTime.AutoSize = true;
            this.lab_createdTime.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_createdTime.Location = new System.Drawing.Point(16, 120);
            this.lab_createdTime.Name = "lab_createdTime";
            this.lab_createdTime.Size = new System.Drawing.Size(47, 16);
            this.lab_createdTime.TabIndex = 1;
            this.lab_createdTime.Text = "时间:{0}";
            // 
            // lab_option
            // 
            this.lab_option.AutoSize = true;
            this.lab_option.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_option.Location = new System.Drawing.Point(15, 98);
            this.lab_option.Name = "lab_option";
            this.lab_option.Size = new System.Drawing.Size(22, 16);
            this.lab_option.TabIndex = 1;
            this.lab_option.Text = "{0}";
            // 
            // MemoComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lab_option);
            this.Controls.Add(this.lab_createdTime);
            this.Controls.Add(this.lab_counter);
            this.Controls.Add(this.lab_code);
            this.Controls.Add(this.lab_productName);
            this.Controls.Add(this.label1);
            this.Name = "MemoComponent";
            this.Size = new System.Drawing.Size(203, 148);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lab_code;
        private System.Windows.Forms.Label lab_counter;
        private System.Windows.Forms.Label lab_productName;
        private System.Windows.Forms.Label lab_createdTime;
        private System.Windows.Forms.Label lab_option;
    }
}
