namespace ReadWriteMSR
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bt_Read = new System.Windows.Forms.Button();
            this.tb_Register = new System.Windows.Forms.TextBox();
            this.lb_Value = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.4234F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.5766F));
            this.tableLayoutPanel1.Controls.Add(this.bt_Read, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_Register, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lb_Value, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(467, 124);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // bt_Read
            // 
            this.bt_Read.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bt_Read.Location = new System.Drawing.Point(346, 14);
            this.bt_Read.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bt_Read.Name = "bt_Read";
            this.bt_Read.Size = new System.Drawing.Size(112, 34);
            this.bt_Read.TabIndex = 0;
            this.bt_Read.Text = "Read";
            this.bt_Read.UseVisualStyleBackColor = true;
            this.bt_Read.Click += new System.EventHandler(this.bt_Read_Click);
            // 
            // tb_Register
            // 
            this.tb_Register.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Register.Location = new System.Drawing.Point(4, 18);
            this.tb_Register.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_Register.Name = "tb_Register";
            this.tb_Register.Size = new System.Drawing.Size(330, 26);
            this.tb_Register.TabIndex = 1;
            this.tb_Register.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lb_Value
            // 
            this.lb_Value.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_Value.AutoSize = true;
            this.lb_Value.Location = new System.Drawing.Point(4, 83);
            this.lb_Value.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_Value.Name = "lb_Value";
            this.lb_Value.Size = new System.Drawing.Size(330, 19);
            this.lb_Value.TabIndex = 2;
            this.lb_Value.Text = "0000000000000000";
            this.lb_Value.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lb_Value.Click += new System.EventHandler(this.lb_Value_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 124);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "MSR Read Write";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button bt_Read;
        private System.Windows.Forms.TextBox tb_Register;
        private System.Windows.Forms.Label lb_Value;
    }
}

