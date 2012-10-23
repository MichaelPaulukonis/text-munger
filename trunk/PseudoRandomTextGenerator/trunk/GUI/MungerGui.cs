using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NDesk.Options;
using TextSourcers;
using TextTransformer;

namespace GUI
{
    public partial class MungerGui : Form
    {
        private readonly List<RuleSet> _activeEditors = new List<RuleSet>();
        private string _output;
        private List<Text> _previouslySelected;
        private const string RuleSetSuffix = ".sqn.xml";
        private Diagnostic _diagnostic;

        public MungerGui(IEnumerable<string> args)
            : this()
        {
            var p = new OptionSet() {
              { "r|ruleset=", v => { PreLoadRuleSet(v); } },
                };

            p.Parse(args);

            /* TODO: enable params for the following:
             *
             *       Pre-set source
             *       Apply rules
             *       Save output
             *
             *       default/help for bad params
             */

            //List<string> extra;
            //try
            //{
            //    extra = p.Parse(args);
            //}
            //catch (OptionException e)
            //{
            //    Console.Write("greet: ");
            //    Console.WriteLine(e.Message);
            //    Console.WriteLine("Try `greet --help' for more information.");
            //    return;
            //}
        }

        public MungerGui()
        {
            InitializeComponent();

            InitializeOpenFileDialog();

            _diagnostic = new Diagnostic(txtDiagnostic);
            DiagnosticsEnabled = true;

            // erase dev-guides
            Output.Text = string.Empty;
            Snippets.Text = string.Empty;
            btnSave.Enabled = false; // this should automatically be called due to other

            PopulateAvailableRulesets();

            // TODO: we could pre-fill the text with a file is some sort of parameter is passed in
            // TODO: separate out the logic and the GUI as much as possible
            // that would make this whole thing scriptable.... ?
        }

        private bool DiagnosticsEnabled { get; set; }

        private void PopulateAvailableRulesets()
        {
            var rules = new List<object>
                            {
                                new RuleSet(Granularity.All),
                                new RuleSet(Granularity.Sentence),
                                new RuleSet(Granularity.Word)
                            };

            rules.AddRange(LoadPredefinedRuleSets());

            RuleSetSelector.AvailableItems = rules;
            RuleSetSelector.AddDoubleClickHandler(DisplayRuleSetEditor);

            LoadPredefinedRuleSets();
        }

        private void PreLoadRuleSet(string rulePath)
        {
            var rs = LoadRules(rulePath)[0];

            var si = RuleSetSelector.SelectedItems;

            si.Add(rs);

            // the getter does not expose the actual underlying object, so we need to pass it back
            // until it is fixed
            RuleSetSelector.SelectedItems = si;
        }

        // I'm going to have to think about this.....
        // the sets are not what I thought
        // Also, there's not way to mix from a predefined set, since they were originally based on aparticular level
        // that might not be true in the future?
        public List<RuleSet> LoadPredefinedRuleSets()
        {
            var sets = new List<RuleSet>();

            var sourcePath = ConfigurationManager.AppSettings["RuleSetsPath"];
            var files = Directory.GetFiles(sourcePath);
            var fs = from file in files
                     where file.ToLower().EndsWith(RuleSetSuffix)
                     select file;

            foreach (var file in fs)
            {
                var x = LoadRules(file);
                sets.AddRange(x);
            }

            return sets;
        }

        public void DisplayRuleSetEditor(object sender, EventArgs e)
        {
            // DAMMIT! we still have implementation details exposed. WTF!!!!
            // TODO: fix this
            var rs = (RuleSet)((ListBox)sender).SelectedItem;
            if (!_activeEditors.Contains(rs))
            {
                //_activeEditors.Add(rs);
                AddActiveRuleset(rs);
                var ed = new RuleSetEditor(rs, RuleSetSelector, _activeEditors);
                ed.StartPosition = FormStartPosition.CenterParent;
                ed.Show();
            }
        }

        private void AddActiveRuleset(RuleSet rs)
        {
            _activeEditors.Add(rs);
        }

        // TODO: throw some logging on here
        // what's taking so much time ?!?!?
        // TODO: optionally enable visual logging of what's going on....
        // and maybe even incrementally update the display after each (entire-text) pass...
        private void btnApply_Click(object sender, EventArgs e)
        {
            using (new HourGlass())
            {
                _diagnostic.Write("started rule applicaton....");

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

                _diagnostic.Write("Finished rule applicaton....");

                Output.Text = _output;
            }
        }

        private void ApplyGlobals(IEnumerable<TransformerBase> globalRules)
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

        private void btnSaveRules_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = "Save RuleSets";
            saveFileDialog.Filter = string.Format("RuleSet sequences (*{0})|*{1}|All files (*.*)|*.*",
                                                  RuleSetSuffix, RuleSetSuffix);

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var name = saveFileDialog.FileName;

                var rules = RuleSetSelector.SelectedItems.Cast<RuleSet>().ToList();

                var xrs = rules.ToXml();

                File.WriteAllText(name, xrs);
            }
        }

        private void btnLoadRules_Click(object sender, EventArgs e)
        {
            // we should be calling the load-file dialog in here,
            // but the actual heavy lifting in the other function

            // TODO: prep the dialog for this type of load
            openFileDialog.Title = "Load RuleSets";
            openFileDialog.Filter = "RuleSet sequences (*.sqn.xml)|*.sqn.xml|All files (*.*)|*.*";

            var dr = openFileDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                var rules = LoadRules(openFileDialog.FileName); // externalize, becuase we're going to call this programmtically, as well

                RuleSetSelector.SelectedItems = rules.Cast<object>().ToList();
            }
        }

        // TODO: why does this create LIST, doesn't it only return ONE RuleSet ?!?!?
        private List<RuleSet> LoadRules(string rulesFile)
        {
            string xml;
            using (var sr = new StreamReader(rulesFile))
            {
                xml = sr.ReadToEnd();
                sr.Close();
            }

            var rules = new List<RuleSet>().FromXML(xml);

            return rules;
        }

        // TODO: okay, now I'm going to be using this for the RuleSet lists.
        // aaaargh.
        private void btnSave_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            var title = "Save Output";
            var fileName = "output.";

            if (tabOutputs.SelectedTab == tabSnippets)
            {
                title = "Save Snippepts";
                fileName = "snippets.";
            }
            saveFileDialog.Title = title;

            fileName += DateTime.Now.ToString("yyyy.MM.dd") + ".txt";
            saveFileDialog.FileName = fileName;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
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

        // THIS WORKS
        // so, when to use....
        // from a file-menu, I suppose....
        private void SaveTexts(List<Text> texts)
        {
            // TODO: ah, the name!
            var name = "texts.xml";

            File.WriteAllText(name, texts.ToXml());
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
            this.openFileDialog.Filter = "Text (*.txt)|*.TXT|" + "All files (*.*)|*.*";

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

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }
    }

    internal class Diagnostic
    {
        private readonly TextBox _target;

        // it would be nice if it wasn't so form-centric
        // but this will do for now.
        // oh, why not Log4Net ? Becuase I want a visible diagnostic window
        public Diagnostic(TextBox target)
        {
            _target = target;
        }

        public void Write(string msg)
        {
            _target.AppendText(string.Format("{0}: {1}\r\n", TimeStamp(), msg));
        }

        private static string TimeStamp()
        {
            return DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss.fff");
        }

        public void Clear()
        {
            _target.Text = string.Empty;
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