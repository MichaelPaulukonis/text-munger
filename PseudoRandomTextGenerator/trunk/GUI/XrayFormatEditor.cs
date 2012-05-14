﻿using System.Windows.Forms;
using TextTransformer;

namespace GUI
{
    public partial class XrayFormatEditor : Form
    {
        private XrmlFormat _xray;

        public XrayFormatEditor() : this(new XrmlFormat()) { }

        public XrayFormatEditor(XrmlFormat x)
        {
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
            //rbFillIn.Checked = _xray.RetainAlignment;
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void txtLineLength_TextChanged(object sender, System.EventArgs e)
        {
            _xray.LineLengthLimit = int.Parse(txtLineLength.Text);
        }

        private void txtDensity_TextChanged(object sender, System.EventArgs e)
        {
            _xray.Density.Percentage = int.Parse(txtDensity.Text);
        }

        private void txtYaw_TextChanged(object sender, System.EventArgs e)
        {
            _xray.Density.RandomWalker.Yaw = int.Parse(txtYaw.Text);
        }

        private void txtWarble_TextChanged(object sender, System.EventArgs e)
        {
            _xray.Density.RandomWalker.Warble = int.Parse(txtWarble.Text);
        }

        private void txtTenacity_TextChanged(object sender, System.EventArgs e)
        {
            _xray.Density.RandomWalker.Tenacity = int.Parse(txtTenacity.Text);
        }

        private void rbDensity_CheckedChanged(object sender, System.EventArgs e)
        {
            _xray.RetainAlignment = !rbDensity.Checked;
            panelDensity.Enabled = rbDensity.Checked;
        }

        // TODO: form elemements:
        // punctuation mark (default: period )
    }
}