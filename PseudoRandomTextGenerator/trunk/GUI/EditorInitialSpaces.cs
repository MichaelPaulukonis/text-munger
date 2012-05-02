using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TextTransformer;

namespace GUI
{
    public partial class EditorInitialSpaces : Form
    {
        private readonly InitialSpaces _ins;

        public EditorInitialSpaces()
            : this(new InitialSpaces())
        { }

        public EditorInitialSpaces(InitialSpaces ins)
        {
            _ins = ins;

            InitializeComponent();

            PopulateForm();
        }

        public InitialSpaces InitialSpaces
        {
            get { return _ins; }
        }

        private void PopulateForm()
        {
            txtOffset.Text = _ins.Offset.ToString();
            txtProbability.Text = _ins.ProbabilityOffset.ToString();
            txtVariance.Text = _ins.OffsetVariance.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtProbability_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _ins.ProbabilityOffset = int.Parse(txtProbability.Text);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                txtProbability.Focus();
            }
        }

        private void txtVariance_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // not sure if this is the way to go about it....
                // if empty, probably in the middle of an edit
                // so ignore it
                // HOWEVER, if we save without typing more
                // the LAST DIGIT present, now invisible, will be the value
                // yarg
                if (txtVariance.Text.Length > 0)
                {
                    _ins.OffsetVariance = int.Parse(txtVariance.Text);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                txtVariance.Focus();
            }
        }

        private void txtOffset_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _ins.Offset = int.Parse(txtOffset.Text);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                txtOffset.Focus();
            }
        }
    }
}