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
    public partial class EditorShortLines : Form
    {
        private ShortLines _sl;

        public EditorShortLines()
            : this(new ShortLines())
        { }

        public EditorShortLines(ShortLines sl)
        {
            this.StartPosition = FormStartPosition.CenterParent;

            InitializeComponent();
            _sl = sl;
            PopulateForm();
        }

        public ShortLines ShortLines
        {
            get { return _sl; }
        }

        private void PopulateForm()
        {
            txtProbMultiple.Text = _sl.ProbabilityMultiple.ToString();
            txtProbNewLine.Text = _sl.ProbabilityNewLine.ToString();
            txtRangeNewLine.Text = _sl.MultipleRange.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // if fields were left blank, set as 0
            if (txtProbMultiple.Text.Length == 0) _sl.ProbabilityMultiple = 0;
            if (txtProbNewLine.Text.Length == 0) _sl.ProbabilityNewLine = 0;
            if (txtRangeNewLine.Text.Length == 0) _sl.MultipleRange = 0;

            this.Close();
        }

        private void txtProbNewLine_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // not sure if this is the way to go about it....
                // if empty, probably in the middle of an edit
                // so ignore it
                // HOWEVER, if we save without typing more
                // the LAST DIGIT present, now invisible, will be the value
                // yarg
                if (txtProbNewLine.Text.Length > 0)
                {
                    _sl.ProbabilityNewLine = int.Parse(txtProbNewLine.Text);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                txtProbNewLine.Focus();
            }
        }

        private void txtProbMultiple_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtProbMultiple.Text.Length > 0)
                {
                    _sl.ProbabilityMultiple = int.Parse(txtProbMultiple.Text);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                txtProbMultiple.Focus();
            }
        }

        private void txtRangeNewLine_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtRangeNewLine.Text.Length > 0)
                {
                    _sl.MultipleRange = int.Parse(txtRangeNewLine.Text);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                txtRangeNewLine.Focus();
            }
        }
    }
}