namespace GUI
{
    partial class XrayFormatEditor
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLineLength = new System.Windows.Forms.TextBox();
            this.txtDensity = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtYaw = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWarble = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTenacity = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "LineLength";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtLineLength
            // 
            this.txtLineLength.Location = new System.Drawing.Point(70, 10);
            this.txtLineLength.Name = "txtLineLength";
            this.txtLineLength.Size = new System.Drawing.Size(100, 20);
            this.txtLineLength.TabIndex = 1;
            this.txtLineLength.TextChanged += new System.EventHandler(this.txtLineLength_TextChanged);
            // 
            // txtDensity
            // 
            this.txtDensity.Location = new System.Drawing.Point(70, 36);
            this.txtDensity.Name = "txtDensity";
            this.txtDensity.Size = new System.Drawing.Size(100, 20);
            this.txtDensity.TabIndex = 3;
            this.txtDensity.TextChanged += new System.EventHandler(this.txtDensity_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Density";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtYaw
            // 
            this.txtYaw.Location = new System.Drawing.Point(70, 62);
            this.txtYaw.Name = "txtYaw";
            this.txtYaw.Size = new System.Drawing.Size(100, 20);
            this.txtYaw.TabIndex = 5;
            this.txtYaw.TextChanged += new System.EventHandler(this.txtYaw_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Yaw";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtWarble
            // 
            this.txtWarble.Location = new System.Drawing.Point(70, 88);
            this.txtWarble.Name = "txtWarble";
            this.txtWarble.Size = new System.Drawing.Size(100, 20);
            this.txtWarble.TabIndex = 7;
            this.txtWarble.TextChanged += new System.EventHandler(this.txtWarble_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Warble";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTenacity
            // 
            this.txtTenacity.Location = new System.Drawing.Point(70, 114);
            this.txtTenacity.Name = "txtTenacity";
            this.txtTenacity.Size = new System.Drawing.Size(100, 20);
            this.txtTenacity.TabIndex = 9;
            this.txtTenacity.TextChanged += new System.EventHandler(this.txtTenacity_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Tenacity";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(63, 156);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // XrayFormatEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(201, 191);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtTenacity);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtWarble);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtYaw);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDensity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLineLength);
            this.Controls.Add(this.label1);
            this.Name = "XrayFormatEditor";
            this.Text = "XrayFormatEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLineLength;
        private System.Windows.Forms.TextBox txtDensity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtYaw;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWarble;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTenacity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnSave;
    }
}