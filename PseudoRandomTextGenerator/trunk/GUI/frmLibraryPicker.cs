using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//using TextTransformer;
using TextSourcers;

namespace GUI
{
    public partial class frmLibraryPicker : Form
    {
        private Dictionary<string, Library> _library;

        private frmLibraryPicker()
        {
            InitializeComponent();
        }

        public frmLibraryPicker(ILibraryFetch library)
            : this()
        {
            var origCursor = this.Cursor;
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();

            _library = library.GetLibrary();

            var items = _library.Keys.SelectMany(textKey => _library[textKey]).ToList();
            LibrarySelector.AvailableItems = items.Cast<object>().ToList();

            this.Cursor = origCursor;
        }

        // the label is now "Select" which makes more sense
        // TODO: make sure all selectors use the same verbiage
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // TODO: expost the actual list of library items for re-selection
        // and provide ability to pre-populate Selected items
        public string Source
        {
            get
            {
                var sb = new StringBuilder();

                foreach (var t in LibrarySelector.SelectedItems)
                {
                    var text = (Text)t;
                    sb.Append(text.Contents);
                }

                return sb.ToString();
            }
        }

        public List<Text> SelectedTexts
        {
            get
            {
                var st = new List<Text>();
                st = LibrarySelector.SelectedItems.Cast<Text>().ToList();
                return st;
            }
        }
    }
}