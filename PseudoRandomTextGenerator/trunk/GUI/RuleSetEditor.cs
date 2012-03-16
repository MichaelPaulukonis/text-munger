using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CustomSelectControl;
using TextTransformer;

namespace GUI
{
    public partial class RuleSetEditor : Form
    {
        private List<RuleSet> _activeRuleSetEditors; // wait, what?!??!
        private MultipleSelectionControl _control;

        // TODO: these overloaded constructors need some work.
        public RuleSetEditor()
        {
            InitializeComponent();

            Editor.AddDoubleClickHandler(DisplayRuleEditor);
        }

        private RuleSetEditor(MultipleSelectionControl control, List<RuleSet> activeRuleSetEditors)
            : this()
        {
            _control = control;
            _activeRuleSetEditors = activeRuleSetEditors;
            FormClosing += FormCloser;

            // TODO: trap for empty rule-sets
            if (RuleSet.Rules.Count > 0)
            {
                Editor.AvailableItems = RuleSet.Rules.Cast<object>().ToList();
            }
        }

        public RuleSetEditor(TransformationFactory fac, MultipleSelectionControl control, List<RuleSet> activeRuleSetEditors)
            : this(control, activeRuleSetEditors)
        {
            RuleSet = new RuleSet(fac.Granularity);
            RuleSet.Rules = fac.GetTransformers();
        }

        public RuleSetEditor(RuleSet set, MultipleSelectionControl control, List<RuleSet> activeRuleSetEditors)
            : this()
        {
            RuleSet = set;

            if (set.Rules != null && set.Rules.Count > 0)
            {
                Editor.SelectedItems = RuleSet.Rules.Cast<object>().ToList();
            }

            // available items is from a factory, based on granularity level
            Editor.AvailableItems = new TransformationFactory(set.Granularity).GetTransformers().Cast<object>().ToList();

            _control = control;
            _activeRuleSetEditors = activeRuleSetEditors;
            FormClosing += FormCloser;
        }

        // TODO: we also need to create the Transformer editor
        // so that can be added as the double-click handler
        // f'r instance, ouor MarkovGenerator editor has crappy settings by default, now...

        public void FormCloser(object sender, FormClosingEventArgs e)
        {
            try
            {
                _activeRuleSetEditors.Remove(RuleSet);
                RuleSet.Rules = Editor.SelectedItems.Cast<ITransformer>().ToList();
                _control.UpdateSelected(RuleSet); // so count can change
            }
            catch (Exception ex)
            {
                // TODO: logging....
                MessageBox.Show(ex.ToString());
            }
        }

        // hey, has anybody noticed that I use "Rule" and "Transformer" interchangebly?
        // beucase I use "Rule" and "Transformer" interchangebly.
        public void DisplayRuleEditor(object sender, EventArgs e)
        {
            // DAMMIT! we still have implementation details exposed. WTF!!!!
            // TODO: fix this
            var rule = (ITransformer)((ListBox)sender).SelectedItem;

            // TODO: using the above as a model
            // open the appropriate editor
            // or display a "not available" message

            if (rule.GetType() == typeof(MarkovGenerator))
            {
                var ed = new MarkovEditor((MarkovGenerator)rule);
                ed.ShowDialog();
            }
            else
            {
                MessageBox.Show("Either no setup is required, or Rule Editor is not yet implemented");
            }
        }

        // this is distinct from the values contained in the control
        // so when they are edited, this set never gets reset.....
        // TODO: don't store this independently....
        // or, update on close, so that RuleSet := Editor.SelectedItems
        private RuleSet _rs;

        public RuleSet RuleSet
        {
            get { return _rs; }
            set { _rs = value; }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            this.Close();
        }

        public void AddFormClosingHandler(FormClosingEventHandler h)
        {
            FormClosing += h;
        }
    }
}