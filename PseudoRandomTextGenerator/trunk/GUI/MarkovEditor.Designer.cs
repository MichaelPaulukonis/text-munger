namespace GUI
{
    partial class MarkovEditor
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtKeySize = new System.Windows.Forms.TextBox();
            this.txtLengthMin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLengthMax = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbRules = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chkCaseSensitive = new System.Windows.Forms.CheckBox();
            this.txtStarterSeed = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Split rule";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "key size";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtKeySize
            // 
            this.txtKeySize.Location = new System.Drawing.Point(80, 37);
            this.txtKeySize.Name = "txtKeySize";
            this.txtKeySize.Size = new System.Drawing.Size(68, 20);
            this.txtKeySize.TabIndex = 3;
            this.txtKeySize.TextChanged += new System.EventHandler(this.txtKeySize_TextChanged);
            // 
            // txtLengthMin
            // 
            this.txtLengthMin.Location = new System.Drawing.Point(80, 64);
            this.txtLengthMin.Name = "txtLengthMin";
            this.txtLengthMin.Size = new System.Drawing.Size(68, 20);
            this.txtLengthMin.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtLengthMin, "char size of output");
            this.txtLengthMin.TextChanged += new System.EventHandler(this.txtLengthMin_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "min Length";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLengthMax
            // 
            this.txtLengthMax.Location = new System.Drawing.Point(80, 91);
            this.txtLengthMax.Name = "txtLengthMax";
            this.txtLengthMax.Size = new System.Drawing.Size(68, 20);
            this.txtLengthMax.TabIndex = 7;
            this.toolTip1.SetToolTip(this.txtLengthMax, "char size of output");
            this.txtLengthMax.TextChanged += new System.EventHandler(this.txtLengthMax_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "max Length";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(73, 179);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbRules
            // 
            this.cbRules.FormattingEnabled = true;
            this.cbRules.Items.AddRange(new object[] {
            "Generic WordRule",
            "Xray Word Type",
            "Xray Char Type"});
            this.cbRules.Location = new System.Drawing.Point(80, 10);
            this.cbRules.Name = "cbRules";
            this.cbRules.Size = new System.Drawing.Size(121, 21);
            this.cbRules.TabIndex = 9;
            // 
            // chkCaseSensitive
            // 
            this.chkCaseSensitive.AutoSize = true;
            this.chkCaseSensitive.Location = new System.Drawing.Point(80, 143);
            this.chkCaseSensitive.Name = "chkCaseSensitive";
            this.chkCaseSensitive.Size = new System.Drawing.Size(96, 17);
            this.chkCaseSensitive.TabIndex = 10;
            this.chkCaseSensitive.Text = "Case Sensitive";
            this.chkCaseSensitive.UseVisualStyleBackColor = true;
            this.chkCaseSensitive.CheckedChanged += new System.EventHandler(this.chkCaseSensitive_CheckedChanged);
            // 
            // txtStarterSeed
            // 
            this.txtStarterSeed.Location = new System.Drawing.Point(80, 117);
            this.txtStarterSeed.Name = "txtStarterSeed";
            this.txtStarterSeed.Size = new System.Drawing.Size(68, 20);
            this.txtStarterSeed.TabIndex = 12;
            this.toolTip1.SetToolTip(this.txtStarterSeed, "char size of output");
            this.txtStarterSeed.TextChanged += new System.EventHandler(this.txtStarterSeed_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "starter";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MarkovEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 216);
            this.Controls.Add(this.txtStarterSeed);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkCaseSensitive);
            this.Controls.Add(this.cbRules);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtLengthMax);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtLengthMin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtKeySize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MarkovEditor";
            this.Text = "MarkovEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKeySize;
        private System.Windows.Forms.TextBox txtLengthMin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLengthMax;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbRules;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chkCaseSensitive;
        private System.Windows.Forms.TextBox txtStarterSeed;
        private System.Windows.Forms.Label label5;
    }
}