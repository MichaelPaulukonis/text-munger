using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TextTransformer;

namespace GUI
{
    public partial class MungerGui : Form
    {
        private readonly List<RuleSet> _activeEditors = new List<RuleSet>();
        private string _output;

        public MungerGui()
        {
            InitializeComponent();

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
            _output = Source.Text;

            // go through words, apply granulars, based on percentage
            // this will be tweaked, so that each rule has it's own percentage.
            // or something.....

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

            txtOutput.Text = _output;
            btnSave.Visible = true;
        }

        private void ApplyGlobals(List<ITransformer> globalRules)
        {
            foreach (var rule in globalRules)
            {
                rule.Source = _output;
                _output = rule.Munged;
            }
        }

        private void ApplyGranularRules(List<ITransformer> granulars)
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

        private void rtbSource_TextChanged(object sender, EventArgs e)
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
            MessageBox.Show("Not yet implemented!");
            // TODO: file-save dialog
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
            if (rbInternet.Checked)
            {
                // TODO: implement
                // form for a URL, and then g-d knows what....
                MessageBox.Show("Not Yet Implemented.");
            }
            else if (rbLibrary.Checked)
            {
                var libaryPath = ConfigurationManager.AppSettings["LibraryPath"];
                var l = new frmLibraryPicker(libaryPath);
                l.ShowDialog();

                Source.AppendText(l.Source);
                // TODO: implement
                // read from the app.config setting for library path
            }
            else if (rbFile.Checked)
            {
                // load up a file-dialog
                // store the last-used path in app.config?
                MessageBox.Show("Not Yet Implemented.");
            }
        }

        private void btnClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(txtOutput.Text, true);
        }
    }
}