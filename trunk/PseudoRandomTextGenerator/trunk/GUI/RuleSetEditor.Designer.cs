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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuleSetEditor));
            this.Editor = new CustomSelectControl.MultipleSelectionControl();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUseSet = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // Editor
            // 
            this.Editor.AllowMultipleItems = true;
            this.Editor.Location = new System.Drawing.Point(-3, 0);
            this.Editor.Name = "Editor";
            this.Editor.SelectedItems = ((System.Collections.Generic.List<object>)(resources.GetObject("Editor.SelectedItems")));
            this.Editor.Size = new System.Drawing.Size(389, 255);
            this.Editor.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(168, 261);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUseSet
            // 
            this.btnUseSet.Location = new System.Drawing.Point(296, 261);
            this.btnUseSet.Name = "btnUseSet";
            this.btnUseSet.Size = new System.Drawing.Size(75, 23);
            this.btnUseSet.TabIndex = 2;
            this.btnUseSet.Text = "&Use";
            this.btnUseSet.UseVisualStyleBackColor = true;
            this.btnUseSet.Click += new System.EventHandler(this.btnUseSet_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(87, 261);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "&Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // RuleSetEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 292);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnUseSet);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.Editor);
            this.Name = "RuleSetEditor";
            this.Text = "RuleSetEditor";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomSelectControl.MultipleSelectionControl Editor;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUseSet;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}