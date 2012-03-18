namespace GUI
{
    partial class MungerGui
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MungerGui));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.SourceBox = new System.Windows.Forms.GroupBox();
            this.Source = new System.Windows.Forms.TextBox();
            this.rbEdit = new System.Windows.Forms.RadioButton();
            this.btnSourceRetrieve = new System.Windows.Forms.Button();
            this.btnClearSource = new System.Windows.Forms.Button();
            this.rbFile = new System.Windows.Forms.RadioButton();
            this.rbInternet = new System.Windows.Forms.RadioButton();
            this.rbLibrary = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RuleSetSelector = new CustomSelectControl.MultipleSelectionControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnClipboard = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SourceBox.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(699, 700);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.SourceBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(691, 674);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Get Sources";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // SourceBox
            // 
            this.SourceBox.Controls.Add(this.Source);
            this.SourceBox.Controls.Add(this.rbEdit);
            this.SourceBox.Controls.Add(this.btnSourceRetrieve);
            this.SourceBox.Controls.Add(this.btnClearSource);
            this.SourceBox.Controls.Add(this.rbFile);
            this.SourceBox.Controls.Add(this.rbInternet);
            this.SourceBox.Controls.Add(this.rbLibrary);
            this.SourceBox.Location = new System.Drawing.Point(6, 5);
            this.SourceBox.Name = "SourceBox";
            this.SourceBox.Size = new System.Drawing.Size(680, 675);
            this.SourceBox.TabIndex = 5;
            this.SourceBox.TabStop = false;
            this.SourceBox.Text = "Sources";
            // 
            // Source
            // 
            this.Source.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Source.Location = new System.Drawing.Point(15, 121);
            this.Source.Multiline = true;
            this.Source.Name = "Source";
            this.Source.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Source.Size = new System.Drawing.Size(665, 545);
            this.Source.TabIndex = 11;
            this.Source.TextChanged += new System.EventHandler(this.Source_TextChanged);
            // 
            // rbEdit
            // 
            this.rbEdit.AutoSize = true;
            this.rbEdit.Checked = true;
            this.rbEdit.Location = new System.Drawing.Point(15, 88);
            this.rbEdit.Name = "rbEdit";
            this.rbEdit.Size = new System.Drawing.Size(43, 17);
            this.rbEdit.TabIndex = 10;
            this.rbEdit.TabStop = true;
            this.rbEdit.Text = "Edit";
            this.rbEdit.UseVisualStyleBackColor = true;
            this.rbEdit.CheckedChanged += new System.EventHandler(this.rbSource_CheckedChanged);
            // 
            // btnSourceRetrieve
            // 
            this.btnSourceRetrieve.Location = new System.Drawing.Point(224, 20);
            this.btnSourceRetrieve.Name = "btnSourceRetrieve";
            this.btnSourceRetrieve.Size = new System.Drawing.Size(104, 23);
            this.btnSourceRetrieve.TabIndex = 9;
            this.btnSourceRetrieve.Text = "Select Sources";
            this.btnSourceRetrieve.UseVisualStyleBackColor = true;
            this.btnSourceRetrieve.Visible = false;
            this.btnSourceRetrieve.Click += new System.EventHandler(this.btnSourceRetrieve_Click);
            // 
            // btnClearSource
            // 
            this.btnClearSource.Location = new System.Drawing.Point(253, 89);
            this.btnClearSource.Name = "btnClearSource";
            this.btnClearSource.Size = new System.Drawing.Size(75, 23);
            this.btnClearSource.TabIndex = 8;
            this.btnClearSource.Text = "Clear";
            this.btnClearSource.UseVisualStyleBackColor = true;
            this.btnClearSource.Click += new System.EventHandler(this.btnClearSource_Click);
            // 
            // rbFile
            // 
            this.rbFile.AutoSize = true;
            this.rbFile.Location = new System.Drawing.Point(15, 65);
            this.rbFile.Name = "rbFile";
            this.rbFile.Size = new System.Drawing.Size(41, 17);
            this.rbFile.TabIndex = 6;
            this.rbFile.Text = "File";
            this.rbFile.UseVisualStyleBackColor = true;
            this.rbFile.CheckedChanged += new System.EventHandler(this.rbSource_CheckedChanged);
            // 
            // rbInternet
            // 
            this.rbInternet.AutoSize = true;
            this.rbInternet.Location = new System.Drawing.Point(15, 42);
            this.rbInternet.Name = "rbInternet";
            this.rbInternet.Size = new System.Drawing.Size(98, 17);
            this.rbInternet.TabIndex = 5;
            this.rbInternet.Text = "Internet Source";
            this.rbInternet.UseVisualStyleBackColor = true;
            this.rbInternet.CheckedChanged += new System.EventHandler(this.rbSource_CheckedChanged);
            // 
            // rbLibrary
            // 
            this.rbLibrary.AutoSize = true;
            this.rbLibrary.Location = new System.Drawing.Point(15, 19);
            this.rbLibrary.Name = "rbLibrary";
            this.rbLibrary.Size = new System.Drawing.Size(56, 17);
            this.rbLibrary.TabIndex = 4;
            this.rbLibrary.Text = "Library";
            this.rbLibrary.UseVisualStyleBackColor = true;
            this.rbLibrary.CheckedChanged += new System.EventHandler(this.rbSource_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(691, 674);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Select Transforms";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RuleSetSelector);
            this.groupBox2.Location = new System.Drawing.Point(121, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(414, 345);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "addRuleSet";
            // 
            // RuleSetSelector
            // 
            this.RuleSetSelector.AllowMultipleItems = true;
            this.RuleSetSelector.Location = new System.Drawing.Point(6, 20);
            this.RuleSetSelector.Name = "RuleSetSelector";
            this.RuleSetSelector.SelectedItems = ((System.Collections.Generic.List<object>)(resources.GetObject("RuleSetSelector.SelectedItems")));
            this.RuleSetSelector.Size = new System.Drawing.Size(389, 255);
            this.RuleSetSelector.TabIndex = 8;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnClipboard);
            this.tabPage3.Controls.Add(this.txtOutput);
            this.tabPage3.Controls.Add(this.btnSave);
            this.tabPage3.Controls.Add(this.btnApply);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(691, 674);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Munged Output";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnClipboard
            // 
            this.btnClipboard.Location = new System.Drawing.Point(95, 14);
            this.btnClipboard.Name = "btnClipboard";
            this.btnClipboard.Size = new System.Drawing.Size(99, 23);
            this.btnClipboard.TabIndex = 18;
            this.btnClipboard.Text = "Copy to Clipboard";
            this.btnClipboard.UseVisualStyleBackColor = true;
            this.btnClipboard.Click += new System.EventHandler(this.btnClipboard_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(3, 43);
            this.txtOutput.MaxLength = 429496729;
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(685, 630);
            this.txtOutput.TabIndex = 17;
            this.txtOutput.Text = "         1         2         3         4         5         6         7         8\r" +
    "\n1234567890123456789012345678901234567890123456789012345678901234567890123456789" +
    "0";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(200, 14);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save Output";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(13, 14);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 15;
            this.btnApply.Text = "ApplyRules";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // MungerGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 712);
            this.Controls.Add(this.tabControl1);
            this.Name = "MungerGui";
            this.Text = "Text Munger";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.SourceBox.ResumeLayout(false);
            this.SourceBox.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox SourceBox;
        private System.Windows.Forms.TextBox Source;
        private System.Windows.Forms.RadioButton rbEdit;
        private System.Windows.Forms.Button btnSourceRetrieve;
        private System.Windows.Forms.Button btnClearSource;
        private System.Windows.Forms.RadioButton rbFile;
        private System.Windows.Forms.RadioButton rbInternet;
        private System.Windows.Forms.RadioButton rbLibrary;
        private System.Windows.Forms.GroupBox groupBox2;
        private CustomSelectControl.MultipleSelectionControl RuleSetSelector;
        private System.Windows.Forms.Button btnClipboard;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnApply;
    }
}