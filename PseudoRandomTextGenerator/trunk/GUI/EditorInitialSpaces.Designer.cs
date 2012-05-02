namespace GUI
{
    partial class EditorInitialSpaces
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtProbability = new System.Windows.Forms.TextBox();
            this.txtVariance = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtOffset = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "offset probability";
            // 
            // txtProbability
            // 
            this.txtProbability.Location = new System.Drawing.Point(101, 36);
            this.txtProbability.Name = "txtProbability";
            this.txtProbability.Size = new System.Drawing.Size(36, 20);
            this.txtProbability.TabIndex = 1;
            this.txtProbability.TextChanged += new System.EventHandler(this.txtProbability_TextChanged);
            // 
            // txtVariance
            // 
            this.txtVariance.Location = new System.Drawing.Point(101, 62);
            this.txtVariance.Name = "txtVariance";
            this.txtVariance.Size = new System.Drawing.Size(36, 20);
            this.txtVariance.TabIndex = 3;
            this.txtVariance.TextChanged += new System.EventHandler(this.txtVariance_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "offset variance";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(62, 89);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtOffset
            // 
            this.txtOffset.Location = new System.Drawing.Point(101, 9);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new System.Drawing.Size(36, 20);
            this.txtOffset.TabIndex = 11;
            this.txtOffset.TextChanged += new System.EventHandler(this.txtOffset_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "offset";
            // 
            // EditorInitialSpaces
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(192, 168);
            this.Controls.Add(this.txtOffset);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtVariance);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtProbability);
            this.Controls.Add(this.label1);
            this.Name = "EditorInitialSpaces";
            this.Text = "InitialSpaces";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProbability;
        private System.Windows.Forms.TextBox txtVariance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtOffset;
        private System.Windows.Forms.Label label3;
    }
}