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

            cbRules.DataSource = Enum.GetValues(typeof(MarkovRule));
            // TODO: pre-select the correct Rule
            // TODO: I don't think that the Markov Editor exposes that.
            // it exposes the Tokenizer, but not the enum... we need to convert around.....
            cbRules.SelectedIndex = (int)MarkovGenerator.MarkovRule;
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
            this.Close();
        }

        // TODO: this runs when we pre-populate the list
        // so the saved selection is OVERWRITTEN
        private void cbRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            MarkovRule sel;
            Enum.TryParse(cbRules.SelectedItem.ToString(), true, out sel);
            var mr = new MarkovRuleFactory(sel).GetRule();
            MarkovGenerator.TokenizerRule = mr;
        }
    }
}