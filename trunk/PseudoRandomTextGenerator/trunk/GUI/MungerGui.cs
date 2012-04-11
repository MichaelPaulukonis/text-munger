using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TextSourcers;
using TextTransformer;

namespace GUI
{
    public partial class MungerGui : Form
    {
        private readonly List<RuleSet> _activeEditors = new List<RuleSet>();
        private string _output;
        private List<Text> _previouslySelected;

        public MungerGui()
        {
            InitializeComponent();

            InitializeOpenFileDialog();

            // erase dev-guides
            Output.Text = string.Empty;
            Snippets.Text = string.Empty;
            btnSave.Enabled = false; // this should automatically be called due to other

            var rules = new List<object> { new RuleSet(Granularity.All),
                    new RuleSet(Granularity.Sentence),
                    new RuleSet(Granularity.Word)
            };

            RuleSetSelector.AvailableItems = rules;

            RuleSetSelector.AddDoubleClickHandler(DisplayRuleSetEditor);

            // TODO: we could pre-fill the text with a file is some sort of parameter is passed in
            // TODO: separate out the logic and the GUI as much as possible
            // that would make this whole thing scriptable.... ?
        }

        public void DisplayRuleSetEditor(object sender, EventArgs e)
        {
            // DAMMIT! we still have implementation details exposed. WTF!!!!
            // TODO: fix this
            var rs = (RuleSet)((ListBox)sender).SelectedItem;
            if (!_activeEditors.Contains(rs))
            {
                _activeEditors.Add(rs);
                var ed = new RuleSetEditor(rs, RuleSetSelector, _activeEditors);
                ed.Show();
            }
        }

        // TODO: throw some logging on here
        // what's taking so much time ?!?!?
        // TODO: optionally enable visual logging of what's going on....
        // and maybe even incrementally update the display after each (entire-text) pass...
        private void btnApply_Click(object sender, EventArgs e)
        {
            using (new HourGlass())
            {
                _output = Source.Text;

                // go through words, apply granulars, based on percentage
                // this will be tweaked, so that each rule has it's own percentage.
                // or something.....

                // TODO: instead of pulling from the form-control, keep this stored locally
                // where "locally" probably means inside of some-other class?
                // hrm. No, probably here in the gui
                // the "TextMunger" class will apply rules to text
                // everything should be supplied, not stored
                var sets = RuleSetSelector.SelectedItems.Cast<RuleSet>().ToList();

                if (sets.Count > 0)
                {
                    foreach (var ruleset in sets)
                    {
                        // TODO: hunh. if...else. There's got to be a smarter way.....
                        if (ruleset.Granularity == Granularity.All)
                        {
                            ApplyGlobals(ruleset.Rules);
                        }
                        else
                        {
                            ApplyGranularRules(ruleset.Rules);
                        }
                    }
                }

                Output.Text = _output;
            }
        }

        private void ApplyGlobals(List<TransformerBase> globalRules)
        {
            foreach (var rule in globalRules)
            {
                rule.Source = _output;
                _output = rule.Munged;
            }
        }

        private void ApplyGranularRules(List<TransformerBase> granulars)
        {
            const int threshold = 100; // 20% chance of applying the rule
            var sb = new StringBuilder();

            var rnd = new Random();

            var regex = new Regex(@"\s+"); // original regex
            var words = regex.Split(_output).ToList();

            foreach (var word in words)
            {
                string outword;

                if (granulars.Any() && rnd.Next(0, 100) < threshold)
                {
                    var rule = granulars[(rnd.Next(0, granulars.Count))];
                    rule.Source = word;
                    outword = rule.Munged;
                }
                else
                {
                    outword = word;
                }
                // TODO: spaces don't work so well, here.....
                sb.Append(outword + " ");
            }

            _output = sb.ToString();
        }

        private void Source_TextChanged(object sender, EventArgs e)
        {
            // enable/disable RuleApplication based on existence of source text
            // NOTE: you can edit rules in the absence of a text, and pick out text afterwards
            // but you can't apply rules to nothing.....
            btnApply.Enabled = (Source.Text.Length != 0);
        }

        private void btnClearSource_Click(object sender, EventArgs e)
        {
            if (Source.Text.Length > 0)
            {
                var result = MessageBox.Show("Are you sure?", "Delete?", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            Source.Text = string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var title = "Save Output";
            if (tabOutputs.SelectedTab == tabSnippets)
            {
                title = "Save Snippepts";
            }
            saveFileDialog.Title = title;

            saveFileDialog.ShowDialog();
        }

        // can't reuse on sub-forms, unless we can parse sender, and apply a different source to save
        private void saveFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var name = saveFileDialog.FileName;

            var text = string.Empty;
            if (tabOutputs.SelectedTab == tabOutput)
            {
                text = Output.Text;
            }
            else
            {
                text = Snippets.Text;
            }
            File.WriteAllText(name, text);
        }

        // handler for all radio buttons
        // http://www.dotnetperls.com/radiobutton
        private void rbSource_CheckedChanged(object sender, EventArgs e)
        {
            // make it visible for all other options, just not edit
            btnSourceRetrieve.Visible = (!rbEdit.Checked);
        }

        private void btnSourceRetrieve_Click(object sender, EventArgs e)
        {
            string inText = string.Empty;

            if (rbInternet.Checked)
            {
                inText = LoadFromInternet();
            }
            else if (rbLibrary.Checked)
            {
                inText = LoadFromLibrary();
            }
            else if (rbFile.Checked)
            {
                inText = LoadFromFile();
            }

            Source.AppendText(inText);
        }

        // TODO: refactor into non-GUI class and place into GUI
        //       uh, why? The library is a GUI selector.
        //       if we were scripting, we'd be supplying files in some other fashion
        //       stop worrying so much.
        private string LoadFromLibrary()
        {
            var libraryPath = ConfigurationManager.AppSettings["LibraryPath"];
            var lf = new FileLibraryFetch(libraryPath);

            return LoadFromLibraryPicker(lf);
        }

        // TODO: only XRML is available now, and by default.
        // conceptually it works!
        // the implementation leaves something to be desired.
        private string LoadFromInternet()
        {
            var lf = new XrmlFetch();

            return LoadFromLibraryPicker(lf);
        }

        private string LoadFromLibraryPicker(ILibraryFetch fetcher)
        {
            var source = string.Empty;

            var l = new frmLibraryPicker(fetcher);
            l.ShowDialog();

            // TODO: implement some mechanism for re-populating the selections on a load
            if (l.TextsSelected)
            {
                _previouslySelected = l.SelectedTexts;

                // for some reason, this is not working
                // even though it works on the frmLibraryPicker
                this.UseWaitCursor = true;
                Application.DoEvents();

                source = l.Source;

                this.UseWaitCursor = false;
            }

            return source;
        }

        // add to OK-click handler?
        private string LoadFromFile()
        {
            // TODO: open the library path by default?
            // TODO: store the last-used path in app.config?

            var sb = new StringBuilder();

            DialogResult dr = openFileDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                foreach (var file in openFileDialog.FileNames)
                {
                    using (var sr = new StreamReader(file))
                    {
                        sb.Append(sr.ReadToEnd());
                        sr.Close();
                    }
                }
            }

            return sb.ToString();
        }

        private void InitializeOpenFileDialog()
        {
            // Set the file dialog to filter for graphics files.
            this.openFileDialog.Filter =
                "Text (*.txt)|*.TXT|" +
                "All files (*.*)|*.*";

            // Allow the user to select multiple images.
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.Title = "Select Text Sources";
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            //this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            //this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            //this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            //this.copyToSnippetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            //this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            //this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            var somethingSelected = (Output.SelectionLength > 0);

            cutToolStripMenuItem.Enabled = somethingSelected;
            copyToolStripMenuItem.Enabled = somethingSelected;
            copyToSnippetsToolStripMenuItem.Enabled = somethingSelected;
            deleteToolStripMenuItem.Enabled = somethingSelected;
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;

            if (item == undoToolStripMenuItem) { Output.Undo(); }
            if (item == cutToolStripMenuItem) { Output.Cut(); }
            if (item == copyToolStripMenuItem) { Output.Copy(); }
            // I prefer the text bracketed -- suppose this could be optional
            if (item == copyToSnippetsToolStripMenuItem) { Snippets.AppendText("\r\n" + Output.SelectedText + "\r\n"); }
            if (item == pasteToolStripMenuItem) { Output.Paste(); }
            if (item == deleteToolStripMenuItem) { Output.SelectedText = string.Empty; }
            if (item == selectAllToolStripMenuItem) { Output.SelectAll(); }
        }

        // TODO: okay, there's a lot of repetition going on, here....
        private void tabOutputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabOutputs.SelectedIndex == 0)
            {
                btnSave.Enabled = (Output.Text.Length > 0);
            }
            else
            {
                btnSave.Enabled = (Snippets.Text.Length > 0);
            }
        }

        private void Output_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = (Output.Text.Length > 0);
        }

        private void Snippets_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = (Snippets.Text.Length > 0);
        }
    }

    // http://stackoverflow.com/a/302865/41153http://stackoverflow.com/a/302865/41153k
    public class HourGlass : IDisposable
    {
        public HourGlass()
        {
            Enabled = true;
        }

        public void Dispose()
        {
            Enabled = false;
        }

        public static bool Enabled
        {
            get { return Application.UseWaitCursor; }
            set
            {
                if (value == Application.UseWaitCursor) return;
                Application.UseWaitCursor = value;
                Form f = Form.ActiveForm;
                if (f != null && f.Handle != null)   // Send WM_SETCURSOR
                    SendMessage(f.Handle, 0x20, f.Handle, (IntPtr)1);
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
    }
}