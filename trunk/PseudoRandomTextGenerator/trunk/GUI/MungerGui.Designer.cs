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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("");
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPaste = new System.Windows.Forms.RadioButton();
            this.rbInternet = new System.Windows.Forms.RadioButton();
            this.rbLibrary = new System.Windows.Forms.RadioButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RuleSetAdder = new CustomSelectControl.MultipleSelectionControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbPaste);
            this.groupBox1.Controls.Add(this.rbInternet);
            this.groupBox1.Controls.Add(this.rbLibrary);
            this.groupBox1.Location = new System.Drawing.Point(12, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 516);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sources";
            // 
            // rbPaste
            // 
            this.rbPaste.AutoSize = true;
            this.rbPaste.Location = new System.Drawing.Point(15, 65);
            this.rbPaste.Name = "rbPaste";
            this.rbPaste.Size = new System.Drawing.Size(52, 17);
            this.rbPaste.TabIndex = 6;
            this.rbPaste.TabStop = true;
            this.rbPaste.Text = "Paste";
            this.rbPaste.UseVisualStyleBackColor = true;
            // 
            // rbInternet
            // 
            this.rbInternet.AutoSize = true;
            this.rbInternet.Location = new System.Drawing.Point(15, 42);
            this.rbInternet.Name = "rbInternet";
            this.rbInternet.Size = new System.Drawing.Size(98, 17);
            this.rbInternet.TabIndex = 5;
            this.rbInternet.TabStop = true;
            this.rbInternet.Text = "Internet Source";
            this.rbInternet.UseVisualStyleBackColor = true;
            // 
            // rbLibrary
            // 
            this.rbLibrary.AutoSize = true;
            this.rbLibrary.Location = new System.Drawing.Point(15, 19);
            this.rbLibrary.Name = "rbLibrary";
            this.rbLibrary.Size = new System.Drawing.Size(56, 17);
            this.rbLibrary.TabIndex = 4;
            this.rbLibrary.TabStop = true;
            this.rbLibrary.Text = "Library";
            this.rbLibrary.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.AllowDrop = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            listViewGroup1.Header = "ListViewGroup";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "ListViewGroup";
            listViewGroup2.Name = "listViewGroup2";
            this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.listView1.Location = new System.Drawing.Point(218, 561);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(281, 121);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // RuleSetAdder
            // 
            this.RuleSetAdder.AllowMultipleItems = true;
            this.RuleSetAdder.Location = new System.Drawing.Point(6, 19);
            this.RuleSetAdder.Name = "RuleSetAdder";
            this.RuleSetAdder.Size = new System.Drawing.Size(389, 255);
            this.RuleSetAdder.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RuleSetAdder);
            this.groupBox2.Location = new System.Drawing.Point(227, 29);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(414, 299);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "addRuleSet";
            // 
            // MungerGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 694);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBox1);
            this.Name = "MungerGui";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbPaste;
        private System.Windows.Forms.RadioButton rbInternet;
        private System.Windows.Forms.RadioButton rbLibrary;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private CustomSelectControl.MultipleSelectionControl RuleSetAdder;
        private System.Windows.Forms.GroupBox groupBox2;

    }
}

