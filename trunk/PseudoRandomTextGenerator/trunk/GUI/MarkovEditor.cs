using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class MarkovEditor : Form
    {
        public MarkovEditor()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txtKeySize_TextChanged(object sender, EventArgs e)
        {
            // this is keySize
            // must be 2-5
            // why not more ?!?!?
            // if outside range, Save is disabled
        }
    }
}