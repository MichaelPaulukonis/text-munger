namespace GUI
{
    partial class EditorShortLines
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
            this.txtProbNewLine = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtRangeNewLine = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProbMultiple = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtProbNewLine
            // 
            this.txtProbNewLine.Location = new System.Drawing.Point(114, 9);
            this.txtProbNewLine.Name = "txtProbNewLine";
            this.txtProbNewLine.Size = new System.Drawing.Size(36, 20);
            this.txtProbNewLine.TabIndex = 18;
            this.txtProbNewLine.TextChanged += new System.EventHandler(this.txtProbNewLine_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "probability NewLine";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(62, 89);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtRangeNewLine
            // 
            this.txtRangeNewLine.Location = new System.Drawing.Point(114, 62);
            this.txtRangeNewLine.Name = "txtRangeNewLine";
            this.txtRangeNewLine.Size = new System.Drawing.Size(36, 20);
            this.txtRangeNewLine.TabIndex = 15;
            this.txtRangeNewLine.TextChanged += new System.EventHandler(this.txtRangeNewLine_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "NewLine Range";
            // 
            // txtProbMultiple
            // 
            this.txtProbMultiple.Location = new System.Drawing.Point(114, 36);
            this.txtProbMultiple.Name = "txtProbMultiple";
            this.txtProbMultiple.Size = new System.Drawing.Size(36, 20);
            this.txtProbMultiple.TabIndex = 13;
            this.txtProbMultiple.TextChanged += new System.EventHandler(this.txtProbMultiple_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "probability Multiple";
            // 
            // EditorShortLines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(185, 118);
            this.Controls.Add(this.txtProbNewLine);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtRangeNewLine);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtProbMultiple);
            this.Controls.Add(this.label1);
            this.Name = "EditorShortLines";
            this.Text = "ShortLines";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtProbNewLine;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtRangeNewLine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProbMultiple;
        private System.Windows.Forms.Label label1;
    }
}