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
        private List<RuleSet> _activeEditors;
        private MultipleSelectionControl _control;

        public RuleSetEditor()
        {
            InitializeComponent();
        }

        public RuleSetEditor(RuleSet set, MultipleSelectionControl control, List<RuleSet> activeEditors)
            : this()
        {
            RuleSet = set;

            // TODO: populate Available list
            // TODO: oooooh! we need a Transformer factory, that will give us all transformers of a particular granularity
            // ??????

            _control = control;
            _activeEditors = activeEditors;
            FormClosing += FormCloser;

            // TODO: available items should be ALL (from a factory)
            // SELECTED should be the below....
            Editor.AvailableItems = set.Rules.Cast<object>().ToList();

            //Editor.SelectedItems = set.Rules.Cast<object>().ToList();
        }

        // TODO: we also need to create the Transformer editor
        // so that can be added as the double-click handler

        public void FormCloser(object sender, FormClosingEventArgs e)
        {
            try
            {
                _activeEditors.Remove(RuleSet);
                RuleSet.Rules = Editor.SelectedItems.Cast<ITransformer>().ToList();
                _control.UpdateSelected(RuleSet); // so count can change
            }
            catch (Exception ex)
            {
                // TODO: logging....
                MessageBox.Show(ex.ToString());
            }
        }

        // this is distinct from the values contained in the control
        // so when they are edited, this set never gets reset.....
        // TODO: don't store this independently....
        // or, update on close, so that RuleSet := Editor.SelectedItems
        public RuleSet RuleSet { get; set; }

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