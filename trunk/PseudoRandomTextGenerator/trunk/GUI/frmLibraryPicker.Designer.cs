namespace GUI
{
    partial class frmLibraryPicker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLibraryPicker));
            this.LibrarySelector = new CustomSelectControl.MultipleSelectionControl();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LibrarySelector
            // 
            this.LibrarySelector.AllowMultipleItems = true;
            this.LibrarySelector.Location = new System.Drawing.Point(0, -1);
            this.LibrarySelector.Name = "LibrarySelector";
            this.LibrarySelector.SelectedItems = ((System.Collections.Generic.List<object>)(resources.GetObject("LibrarySelector.SelectedItems")));
            this.LibrarySelector.Size = new System.Drawing.Size(389, 255);
            this.LibrarySelector.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(153, 260);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Select";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmLibraryPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 299);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.LibrarySelector);
            this.Name = "frmLibraryPicker";
            this.Text = "LibrarySelector";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomSelectControl.MultipleSelectionControl LibrarySelector;
        private System.Windows.Forms.Button btnExit;
    }
}