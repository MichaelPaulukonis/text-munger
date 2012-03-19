using System.Windows.Forms;
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
        }

        // TODO: form elemements:
        // punctuation mark (default: period )
        // density percentage
        // cols to cut-at (default: 80)
        // eventually, random deviation - how much swing
        // and maybe some different density algorithms
    }
}