using System;
using System.Linq;
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
            InitializeComponent();
        }

        public MarkovEditor(MarkovGenerator mg)
        {
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
    }
}