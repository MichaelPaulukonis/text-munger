namespace GUI
{
    partial class RuleSetEditor
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
            this.Editor = new CustomSelectControl.MultipleSelectionControl();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Editor
            // 
            this.Editor.AllowMultipleItems = true;
            this.Editor.Location = new System.Drawing.Point(-3, 0);
            this.Editor.Name = "Editor";
            this.Editor.Size = new System.Drawing.Size(389, 255);
            this.Editor.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(154, 261);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // RuleSetEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 292);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.Editor);
            this.Name = "RuleSetEditor";
            this.Text = "RuleSetEditor";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomSelectControl.MultipleSelectionControl Editor;
        private System.Windows.Forms.Button btnSave;
    }
}