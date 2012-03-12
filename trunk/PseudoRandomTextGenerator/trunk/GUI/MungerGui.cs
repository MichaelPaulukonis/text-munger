using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Runner;
using TextTransformer;

namespace GUI
{
    public partial class MungerGui : Form
    {
        private List<RuleSet> _activeEditors = new List<RuleSet>();
        private string _output;

        public MungerGui()
        {
            InitializeComponent();

            // TODO: these can start empty, or with defaults
            //       name will reflect the quantity within
            //       and empty-sets can be curated with the editor

            // these are all examples of how to add rules

            // TODO: we need to have an editor for some of these
            // or the factory will be giving us defaults I don't like.
            // or, make what I like the defaults... yeah, that makes sense....

            // argh! I missed this for so long.
            // the RuleSet IS populated. So when the editor is launched, both the Availabled and Selected should be populated
            // OTHERWISE, we should have the editor populate via factory
            // -- or, do we just add ALL rules by default???
            // and delete as we want?
            // that way, we'd only need special button to add "empty" ruleset, or something like that...

            // there is some redundant redundancy, here...
            var g = new RuleSet("temp", Granularity.All);
            g.Rules = new TransformationFactory().GetTransformers(g.Granularity);

            var globals = new RuleSet("Global Rules", Granularity.All);

            var markov = new MarkovGenerator { MinLength = 8000, MaxLength = 10000 };
            globals.AddRule(markov).AddRule(new Density { Percentage = 97 }).AddRule(new XrmlFormat());

            // oop, I need a RuleSet populated
            var granular = new RuleSet("Granular Rules", Granularity.Word) { Rules = new TransformationFactory().GetTransformers(Granularity.Word) };

            var rules = new List<object> { globals, granular };

            // TODO: better name. It's not just an adder
            RuleSetSelector.AvailableItems = rules;

            RuleSetSelector.AddDoubleClickHandler(DisplayRuleSetEditor);

            // Potemkin village presets for testing
            rtbSource.Text = new ConsoleRunner().GetSource();
            btnApply.Enabled = true;
            //rbFile.Checked = true; // don't check it, as we want to keep Edit as default
        }

        public void DisplayRuleSetEditor(object sender, EventArgs e)
        {
            // DAMMIT! we still have implementation details exposed. WTF!!!!
            // TODO: fix this
            var rs = (RuleSet)((ListBox)sender).SelectedItem;
            if (!_activeEditors.Contains(rs))
            {
                _activeEditors.Add(rs);
                // TODO: fix this. we will need a different constructor....
                // if this is created from a non-empty ruleSet
                // the rules should be in the SELECTED box
                // the Available column should be all rules of a given granularity
                // as provided by a factory.....
                var ed = new RuleSetEditor(rs, RuleSetSelector, _activeEditors);
                ed.Show();
            }
        }

        // TODO: throw some logging on here
        // what's taking so much time ?!?!?
        private void btnApply_Click(object sender, EventArgs e)
        {
            _output = rtbSource.Text;
            // TODO: apply rules
            // apply globals one by one
            // go through words, apply granulars, based on percentage
            // this will be tweaked, so that each rule has it's own percentage.
            // or something.....

            var sets = RuleSetSelector.SelectedItems.Cast<RuleSet>().ToList();

            if (sets.Count > 0)
            {
                foreach (var ruleset in sets)
                {
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

            // TODO: the richTextBox changes fonts. aaargh
            // foreign text acting as control-characters?!?!?
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
            btnApply.Enabled = (rtbSource.Text.Length != 0);
        }

        private void btnClearSource_Click(object sender, EventArgs e)
        {
            if (rtbSource.Text.Length > 0)
            {
                var result = MessageBox.Show("Are you sure?", "Delete?", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            rtbSource.Text = string.Empty;
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
            }
            else if (rbLibrary.Checked)
            {
                var libaryPath = ConfigurationManager.AppSettings["LibraryPath"];
                var l = new frmLibraryPicker(libaryPath);
                l.ShowDialog();

                rtbSource.AppendText(l.Source);
                // TODO: implement
                // read from the app.config setting for library path
            }
            else if (rbFile.Checked)
            {
                // load up a file-dialog
                // store the last-used path in app.config?
            }
        }
    }
}