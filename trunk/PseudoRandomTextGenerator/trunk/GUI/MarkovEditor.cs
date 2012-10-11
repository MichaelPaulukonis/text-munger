using System;
using System.Windows.Forms;
using TextTransformer;

namespace GUI
{
    public partial class MarkovEditor : Form
    {
        private readonly MarkovGenerator _m;

        public MarkovEditor()
            : this(new MarkovGenerator())
        {
        }

        public MarkovEditor(MarkovGenerator mg)
        {
            this.StartPosition = FormStartPosition.CenterParent;

            InitializeComponent();
            _m = mg;
            PopulateForm();
        }

        public MarkovGenerator MarkovGenerator
        {
            get { return _m; }
        }

        private void PopulateForm()
        {
            txtKeySize.Text = MarkovGenerator.KeySize.ToString();
            txtLengthMax.Text = MarkovGenerator.LengthMax.ToString();
            txtLengthMin.Text = MarkovGenerator.LengthMin.ToString();
            txtStarterSeed.Text = MarkovGenerator.StarterSeed;

            cbRules.DataSource = Enum.GetValues(typeof(MarkovRuleType));

            // TODO: pre-select the correct Type
            // TODO: I don't think that the Markov Editor exposes that.
            // it exposes the Tokenizer, but not the enum... we need to convert around.....
            cbRules.SelectedIndex = (int)MarkovGenerator.MarkovRuleType;

            chkCaseSensitive.Checked = MarkovGenerator.CaseSensitive;
        }

        private void txtKeySize_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MarkovGenerator.KeySize = int.Parse(txtKeySize.Text);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                txtKeySize.Focus();
            }
        }

        private void txtLengthMin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MarkovGenerator.LengthMin = int.Parse(txtLengthMin.Text);

                // if min > max, increase max (use the form so events percolate and run any checks that might exist)
                if (MarkovGenerator.LengthMin > MarkovGenerator.LengthMax)
                {
                    txtLengthMax.Text = txtLengthMin.Text;
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                txtLengthMin.Focus();
            }
        }

        private void txtLengthMax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MarkovGenerator.LengthMax = int.Parse(txtLengthMax.Text);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                txtLengthMax.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MarkovGenerator.TokenizerRule = GetMarkovRuleFromForm();
            this.Close();
        }

        private IMarkovRule GetMarkovRuleFromForm()
        {
            MarkovRuleType sel;
            Enum.TryParse(cbRules.SelectedItem.ToString(), true, out sel);
            var mr = new MarkovRuleFactory(sel).GetRule();
            return mr;
        }

        private void chkCaseSensitive_CheckedChanged(object sender, EventArgs e)
        {
            MarkovGenerator.CaseSensitive = chkCaseSensitive.Checked;
        }

        private void txtStarterSeed_TextChanged(object sender, EventArgs e)
        {
            MarkovGenerator.StarterSeed = txtStarterSeed.Text;
        }

        // TODO: this runs when we pre-populate the list
        // so the saved selection is OVERWRITTEN
        // SOLUTION: drop this method, and only populte the MarkovGenerator on save
        //private void cbRules_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    MarkovRuleType sel;
        //    Enum.TryParse(cbRules.SelectedItem.ToString(), true, out sel);
        //    var mr = new MarkovRuleFactory(sel).GetRule();
        //    MarkovGenerator.TokenizerRule = mr;
        //}
    }
}