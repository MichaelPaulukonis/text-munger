using System.Windows.Forms;
using TextTransformer;

namespace GUI
{
    public partial class XrayFormatEditor : Form
    {
        private readonly XrmlFormat _xray;

        public XrayFormatEditor() : this(new XrmlFormat()) { }

        public XrayFormatEditor(XrmlFormat x)
        {
            this.StartPosition = FormStartPosition.CenterParent;

            InitializeComponent();
            _xray = x;
            PopulateForm();
        }

        public XrmlFormat XrmlFormat
        {
            get { return _xray; }
        }

        private void PopulateForm()
        {
            txtLineLength.Text = _xray.LineLengthLimit.ToString();
            txtDensity.Text = _xray.Density.Percentage.ToString();
            txtYaw.Text = _xray.Density.RandomWalker.Yaw.ToString();
            txtWarble.Text = _xray.Density.RandomWalker.Warble.ToString();
            txtTenacity.Text = _xray.Density.RandomWalker.Tenacity.ToString();
            rbDensity.Checked = !_xray.RetainAlignment;
            // RetainAlignment is to allow the fill-in of pre-spaced text
            // a-la free verse, or what-have-you
            // this is set on the form by "FillAround"
            // the opposite and default value is "Density Fill"
            // which ignores whitespace
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void txtLineLength_TextChanged(object sender, System.EventArgs e)
        {
            _xray.LineLengthLimit = CleanNumeric(txtLineLength);
        }

        private void txtDensity_TextChanged(object sender, System.EventArgs e)
        {
            _xray.Density.Percentage = CleanNumeric(txtDensity);
        }

        private void txtYaw_TextChanged(object sender, System.EventArgs e)
        {
            _xray.Density.RandomWalker.Yaw = CleanNumeric(txtYaw);
        }

        private void txtWarble_TextChanged(object sender, System.EventArgs e)
        {
            _xray.Density.RandomWalker.Warble = CleanNumeric(txtWarble);
        }

        private void txtTenacity_TextChanged(object sender, System.EventArgs e)
        {
            _xray.Density.RandomWalker.Tenacity = CleanNumeric(txtTenacity);
        }

        private void rbDensity_CheckedChanged(object sender, System.EventArgs e)
        {
            _xray.RetainAlignment = !rbDensity.Checked;
            panelDensity.Enabled = rbDensity.Checked;
        }

        private int CleanNumeric(Control field)
        {
            int i;
            int.TryParse(field.Text, out i);
            field.Text = i.ToString();

            return i;
        }

        // TODO: form elemements:
        // punctuation mark (default: period )
    }
}