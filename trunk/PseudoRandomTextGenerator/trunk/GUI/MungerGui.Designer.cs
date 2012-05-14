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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MungerGui));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToSnippetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnLoadRules = new System.Windows.Forms.Button();
            this.btnSaveRules = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RuleSetSelector = new CustomSelectControl.MultipleSelectionControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabOutputs = new System.Windows.Forms.TabControl();
            this.tabOutput = new System.Windows.Forms.TabPage();
            this.Output = new System.Windows.Forms.TextBox();
            this.tabSnippets = new System.Windows.Forms.TabPage();
            this.Snippets = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.txtDiagnostic = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.rbLibrary = new System.Windows.Forms.RadioButton();
            this.rbInternet = new System.Windows.Forms.RadioButton();
            this.rbFile = new System.Windows.Forms.RadioButton();
            this.btnClearSource = new System.Windows.Forms.Button();
            this.btnSourceRetrieve = new System.Windows.Forms.Button();
            this.rbEdit = new System.Windows.Forms.RadioButton();
            this.Source = new System.Windows.Forms.TextBox();
            this.tableSourceLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tableSelectors = new System.Windows.Forms.TableLayoutPanel();
            this.panelRadioButtons = new System.Windows.Forms.Panel();
            this.tableButtons = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabOutputs.SuspendLayout();
            this.tabOutput.SuspendLayout();
            this.tabSnippets.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableSourceLayout.SuspendLayout();
            this.tableSelectors.SuspendLayout();
            this.panelRadioButtons.SuspendLayout();
            this.tableButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(724, 712);
            this.tabControl1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.copyToSnippetsToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.selectAllToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(165, 158);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // copyToSnippetsToolStripMenuItem
            // 
            this.copyToSnippetsToolStripMenuItem.Name = "copyToSnippetsToolStripMenuItem";
            this.copyToSnippetsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.copyToSnippetsToolStripMenuItem.Text = "Copy to Snippets";
            this.copyToSnippetsToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableSourceLayout);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(716, 686);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Source";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnLoadRules);
            this.tabPage2.Controls.Add(this.btnSaveRules);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(716, 686);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Select Transforms";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnLoadRules
            // 
            this.btnLoadRules.Location = new System.Drawing.Point(541, 279);
            this.btnLoadRules.Name = "btnLoadRules";
            this.btnLoadRules.Size = new System.Drawing.Size(119, 23);
            this.btnLoadRules.TabIndex = 12;
            this.btnLoadRules.Text = "Load Rules";
            this.btnLoadRules.UseVisualStyleBackColor = true;
            this.btnLoadRules.Click += new System.EventHandler(this.btnLoadRules_Click);
            // 
            // btnSaveRules
            // 
            this.btnSaveRules.Location = new System.Drawing.Point(541, 250);
            this.btnSaveRules.Name = "btnSaveRules";
            this.btnSaveRules.Size = new System.Drawing.Size(119, 23);
            this.btnSaveRules.TabIndex = 11;
            this.btnSaveRules.Text = "Save Rules";
            this.btnSaveRules.UseVisualStyleBackColor = true;
            this.btnSaveRules.Click += new System.EventHandler(this.btnSaveRules_Click);
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
            this.RuleSetSelector.DisplayFilter = true;
            this.RuleSetSelector.Location = new System.Drawing.Point(6, 20);
            this.RuleSetSelector.Name = "RuleSetSelector";
            this.RuleSetSelector.SelectedItems = ((System.Collections.Generic.List<object>)(resources.GetObject("RuleSetSelector.SelectedItems")));
            this.RuleSetSelector.Size = new System.Drawing.Size(389, 255);
            this.RuleSetSelector.TabIndex = 8;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(716, 686);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Munged Output";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabOutputs
            // 
            this.tabOutputs.Controls.Add(this.tabOutput);
            this.tabOutputs.Controls.Add(this.tabSnippets);
            this.tabOutputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOutputs.Location = new System.Drawing.Point(0, 25);
            this.tabOutputs.Margin = new System.Windows.Forms.Padding(0);
            this.tabOutputs.Name = "tabOutputs";
            this.tabOutputs.SelectedIndex = 0;
            this.tabOutputs.Size = new System.Drawing.Size(716, 661);
            this.tabOutputs.TabIndex = 17;
            this.tabOutputs.SelectedIndexChanged += new System.EventHandler(this.tabOutputs_SelectedIndexChanged);
            // 
            // tabOutput
            // 
            this.tabOutput.Controls.Add(this.Output);
            this.tabOutput.Location = new System.Drawing.Point(4, 22);
            this.tabOutput.Margin = new System.Windows.Forms.Padding(0);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabOutput.Size = new System.Drawing.Size(708, 635);
            this.tabOutput.TabIndex = 0;
            this.tabOutput.Text = "Output";
            this.tabOutput.UseVisualStyleBackColor = true;
            // 
            // Output
            // 
            this.Output.ContextMenuStrip = this.contextMenuStrip1;
            this.Output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Output.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Output.Location = new System.Drawing.Point(3, 3);
            this.Output.Margin = new System.Windows.Forms.Padding(0);
            this.Output.MaxLength = 429496729;
            this.Output.Multiline = true;
            this.Output.Name = "Output";
            this.Output.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Output.Size = new System.Drawing.Size(702, 629);
            this.Output.TabIndex = 18;
            this.Output.Text = "         1         2         3         4         5         6         7         8\r" +
    "\n1234567890123456789012345678901234567890123456789012345678901234567890123456789" +
    "0";
            this.Output.TextChanged += new System.EventHandler(this.Output_TextChanged);
            // 
            // tabSnippets
            // 
            this.tabSnippets.Controls.Add(this.Snippets);
            this.tabSnippets.Location = new System.Drawing.Point(4, 22);
            this.tabSnippets.Margin = new System.Windows.Forms.Padding(0);
            this.tabSnippets.Name = "tabSnippets";
            this.tabSnippets.Padding = new System.Windows.Forms.Padding(3);
            this.tabSnippets.Size = new System.Drawing.Size(708, 635);
            this.tabSnippets.TabIndex = 1;
            this.tabSnippets.Text = "Snippets";
            this.tabSnippets.UseVisualStyleBackColor = true;
            // 
            // Snippets
            // 
            this.Snippets.ContextMenuStrip = this.contextMenuStrip1;
            this.Snippets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Snippets.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Snippets.Location = new System.Drawing.Point(3, 3);
            this.Snippets.Margin = new System.Windows.Forms.Padding(0);
            this.Snippets.MaxLength = 429496729;
            this.Snippets.Multiline = true;
            this.Snippets.Name = "Snippets";
            this.Snippets.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Snippets.Size = new System.Drawing.Size(702, 629);
            this.Snippets.TabIndex = 19;
            this.Snippets.Text = "         1         2         3         4         5         6         7         8\r" +
    "\n1234567890123456789012345678901234567890123456789012345678901234567890123456789" +
    "0";
            this.Snippets.TextChanged += new System.EventHandler(this.Snippets_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(75, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(0, 0);
            this.btnApply.Margin = new System.Windows.Forms.Padding(0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 15;
            this.btnApply.Text = "ApplyRules";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.txtDiagnostic);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(716, 686);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Diagnostics";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // txtDiagnostic
            // 
            this.txtDiagnostic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDiagnostic.Location = new System.Drawing.Point(3, 3);
            this.txtDiagnostic.Multiline = true;
            this.txtDiagnostic.Name = "txtDiagnostic";
            this.txtDiagnostic.Size = new System.Drawing.Size(710, 680);
            this.txtDiagnostic.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabOutputs, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(716, 686);
            this.tableLayoutPanel1.TabIndex = 18;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.Controls.Add(this.btnSave, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnApply, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(323, 25);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // rbLibrary
            // 
            this.rbLibrary.AutoSize = true;
            this.rbLibrary.Location = new System.Drawing.Point(3, 3);
            this.rbLibrary.Name = "rbLibrary";
            this.rbLibrary.Size = new System.Drawing.Size(56, 17);
            this.rbLibrary.TabIndex = 4;
            this.rbLibrary.Text = "Library";
            this.rbLibrary.UseVisualStyleBackColor = true;
            this.rbLibrary.CheckedChanged += new System.EventHandler(this.rbSource_CheckedChanged);
            // 
            // rbInternet
            // 
            this.rbInternet.AutoSize = true;
            this.rbInternet.Location = new System.Drawing.Point(3, 26);
            this.rbInternet.Name = "rbInternet";
            this.rbInternet.Size = new System.Drawing.Size(98, 17);
            this.rbInternet.TabIndex = 5;
            this.rbInternet.Text = "Internet Source";
            this.rbInternet.UseVisualStyleBackColor = true;
            this.rbInternet.CheckedChanged += new System.EventHandler(this.rbSource_CheckedChanged);
            // 
            // rbFile
            // 
            this.rbFile.AutoSize = true;
            this.rbFile.Location = new System.Drawing.Point(3, 49);
            this.rbFile.Name = "rbFile";
            this.rbFile.Size = new System.Drawing.Size(41, 17);
            this.rbFile.TabIndex = 6;
            this.rbFile.Text = "File";
            this.rbFile.UseVisualStyleBackColor = true;
            this.rbFile.CheckedChanged += new System.EventHandler(this.rbSource_CheckedChanged);
            // 
            // btnClearSource
            // 
            this.btnClearSource.Location = new System.Drawing.Point(3, 53);
            this.btnClearSource.Name = "btnClearSource";
            this.btnClearSource.Size = new System.Drawing.Size(75, 23);
            this.btnClearSource.TabIndex = 8;
            this.btnClearSource.Text = "Clear";
            this.btnClearSource.UseVisualStyleBackColor = true;
            this.btnClearSource.Click += new System.EventHandler(this.btnClearSource_Click);
            // 
            // btnSourceRetrieve
            // 
            this.btnSourceRetrieve.Location = new System.Drawing.Point(3, 3);
            this.btnSourceRetrieve.Name = "btnSourceRetrieve";
            this.btnSourceRetrieve.Size = new System.Drawing.Size(104, 23);
            this.btnSourceRetrieve.TabIndex = 9;
            this.btnSourceRetrieve.Text = "Select Sources";
            this.btnSourceRetrieve.UseVisualStyleBackColor = true;
            this.btnSourceRetrieve.Visible = false;
            this.btnSourceRetrieve.Click += new System.EventHandler(this.btnSourceRetrieve_Click);
            // 
            // rbEdit
            // 
            this.rbEdit.AutoSize = true;
            this.rbEdit.Checked = true;
            this.rbEdit.Location = new System.Drawing.Point(3, 72);
            this.rbEdit.Name = "rbEdit";
            this.rbEdit.Size = new System.Drawing.Size(43, 17);
            this.rbEdit.TabIndex = 10;
            this.rbEdit.TabStop = true;
            this.rbEdit.Text = "Edit";
            this.rbEdit.UseVisualStyleBackColor = true;
            this.rbEdit.CheckedChanged += new System.EventHandler(this.rbSource_CheckedChanged);
            // 
            // Source
            // 
            this.Source.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Source.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Source.Location = new System.Drawing.Point(0, 100);
            this.Source.Margin = new System.Windows.Forms.Padding(0);
            this.Source.Multiline = true;
            this.Source.Name = "Source";
            this.Source.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Source.Size = new System.Drawing.Size(710, 580);
            this.Source.TabIndex = 11;
            this.Source.TextChanged += new System.EventHandler(this.Source_TextChanged);
            // 
            // tableSourceLayout
            // 
            this.tableSourceLayout.ColumnCount = 1;
            this.tableSourceLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableSourceLayout.Controls.Add(this.Source, 0, 1);
            this.tableSourceLayout.Controls.Add(this.tableSelectors, 0, 0);
            this.tableSourceLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableSourceLayout.Location = new System.Drawing.Point(3, 3);
            this.tableSourceLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tableSourceLayout.Name = "tableSourceLayout";
            this.tableSourceLayout.RowCount = 2;
            this.tableSourceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableSourceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableSourceLayout.Size = new System.Drawing.Size(710, 680);
            this.tableSourceLayout.TabIndex = 12;
            // 
            // tableSelectors
            // 
            this.tableSelectors.ColumnCount = 2;
            this.tableSelectors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableSelectors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableSelectors.Controls.Add(this.panelRadioButtons, 0, 0);
            this.tableSelectors.Controls.Add(this.tableButtons, 1, 0);
            this.tableSelectors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableSelectors.Location = new System.Drawing.Point(0, 0);
            this.tableSelectors.Margin = new System.Windows.Forms.Padding(0);
            this.tableSelectors.Name = "tableSelectors";
            this.tableSelectors.RowCount = 1;
            this.tableSelectors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableSelectors.Size = new System.Drawing.Size(710, 100);
            this.tableSelectors.TabIndex = 12;
            // 
            // panelRadioButtons
            // 
            this.panelRadioButtons.Controls.Add(this.rbLibrary);
            this.panelRadioButtons.Controls.Add(this.rbEdit);
            this.panelRadioButtons.Controls.Add(this.rbInternet);
            this.panelRadioButtons.Controls.Add(this.rbFile);
            this.panelRadioButtons.Location = new System.Drawing.Point(0, 0);
            this.panelRadioButtons.Margin = new System.Windows.Forms.Padding(0);
            this.panelRadioButtons.Name = "panelRadioButtons";
            this.panelRadioButtons.Size = new System.Drawing.Size(126, 99);
            this.panelRadioButtons.TabIndex = 0;
            // 
            // tableButtons
            // 
            this.tableButtons.ColumnCount = 1;
            this.tableButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableButtons.Controls.Add(this.btnSourceRetrieve, 0, 0);
            this.tableButtons.Controls.Add(this.btnClearSource, 0, 1);
            this.tableButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableButtons.Location = new System.Drawing.Point(355, 0);
            this.tableButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tableButtons.Name = "tableButtons";
            this.tableButtons.RowCount = 2;
            this.tableButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableButtons.Size = new System.Drawing.Size(355, 100);
            this.tableButtons.TabIndex = 1;
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
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabOutputs.ResumeLayout(false);
            this.tabOutput.ResumeLayout(false);
            this.tabOutput.PerformLayout();
            this.tabSnippets.ResumeLayout(false);
            this.tabSnippets.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableSourceLayout.ResumeLayout(false);
            this.tableSourceLayout.PerformLayout();
            this.tableSelectors.ResumeLayout(false);
            this.panelRadioButtons.ResumeLayout(false);
            this.panelRadioButtons.PerformLayout();
            this.tableButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox2;
        private CustomSelectControl.MultipleSelectionControl RuleSetSelector;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToSnippetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.TabControl tabOutputs;
        private System.Windows.Forms.TabPage tabOutput;
        private System.Windows.Forms.TextBox Output;
        private System.Windows.Forms.TabPage tabSnippets;
        private System.Windows.Forms.TextBox Snippets;
        private System.Windows.Forms.Button btnSaveRules;
        private System.Windows.Forms.Button btnLoadRules;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtDiagnostic;
        private System.Windows.Forms.TableLayoutPanel tableSourceLayout;
        private System.Windows.Forms.TextBox Source;
        private System.Windows.Forms.TableLayoutPanel tableSelectors;
        private System.Windows.Forms.Panel panelRadioButtons;
        private System.Windows.Forms.RadioButton rbLibrary;
        private System.Windows.Forms.RadioButton rbEdit;
        private System.Windows.Forms.RadioButton rbInternet;
        private System.Windows.Forms.RadioButton rbFile;
        private System.Windows.Forms.TableLayoutPanel tableButtons;
        private System.Windows.Forms.Button btnSourceRetrieve;
        private System.Windows.Forms.Button btnClearSource;
    }
}