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
            this.SourceBox = new System.Windows.Forms.GroupBox();
            this.btnSourceRetrieve = new System.Windows.Forms.Button();
            this.btnClearSource = new System.Windows.Forms.Button();
            this.rtbSource = new System.Windows.Forms.RichTextBox();
            this.rbFile = new System.Windows.Forms.RadioButton();
            this.rbInternet = new System.Windows.Forms.RadioButton();
            this.rbLibrary = new System.Windows.Forms.RadioButton();
            this.RuleSetSelector = new CustomSelectControl.MultipleSelectionControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.rbEdit = new System.Windows.Forms.RadioButton();
            this.SourceBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // SourceBox
            // 
            this.SourceBox.Controls.Add(this.rbEdit);
            this.SourceBox.Controls.Add(this.btnSourceRetrieve);
            this.SourceBox.Controls.Add(this.btnClearSource);
            this.SourceBox.Controls.Add(this.rtbSource);
            this.SourceBox.Controls.Add(this.rbFile);
            this.SourceBox.Controls.Add(this.rbInternet);
            this.SourceBox.Controls.Add(this.rbLibrary);
            this.SourceBox.Location = new System.Drawing.Point(12, 29);
            this.SourceBox.Name = "SourceBox";
            this.SourceBox.Size = new System.Drawing.Size(346, 660);
            this.SourceBox.TabIndex = 4;
            this.SourceBox.TabStop = false;
            this.SourceBox.Text = "Sources";
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
            // rtbSource
            // 
            this.rtbSource.Location = new System.Drawing.Point(15, 118);
            this.rtbSource.Name = "rtbSource";
            this.rtbSource.Size = new System.Drawing.Size(314, 535);
            this.rtbSource.TabIndex = 7;
            this.rtbSource.Text = "";
            this.rtbSource.TextChanged += new System.EventHandler(this.rtbSource_TextChanged);
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
            // RuleSetSelector
            // 
            this.RuleSetSelector.AllowMultipleItems = true;
            this.RuleSetSelector.Location = new System.Drawing.Point(6, 19);
            this.RuleSetSelector.Name = "RuleSetSelector";
            this.RuleSetSelector.SelectedItems = ((System.Collections.Generic.List<object>)(resources.GetObject("RuleSetSelector.SelectedItems")));
            this.RuleSetSelector.Size = new System.Drawing.Size(389, 255);
            this.RuleSetSelector.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RuleSetSelector);
            this.groupBox2.Location = new System.Drawing.Point(384, 29);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(414, 299);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "addRuleSet";
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(385, 334);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 10;
            this.btnApply.Text = "ApplyRules";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(478, 334);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save Output";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(384, 363);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(379, 319);
            this.txtOutput.TabIndex = 13;
            this.txtOutput.Text = "";
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
            // 
            // MungerGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 694);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.SourceBox);
            this.Name = "MungerGui";
            this.Text = "Form1";
            this.SourceBox.ResumeLayout(false);
            this.SourceBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox SourceBox;
        private System.Windows.Forms.RadioButton rbFile;
        private System.Windows.Forms.RadioButton rbInternet;
        private System.Windows.Forms.RadioButton rbLibrary;
        private CustomSelectControl.MultipleSelectionControl RuleSetSelector;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.RichTextBox rtbSource;
        private System.Windows.Forms.Button btnClearSource;
        private System.Windows.Forms.Button btnSourceRetrieve;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.RadioButton rbEdit;

    }
}

