using System;
using System.Collections.Generic;
using System.IO;
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
            var filter = "Library files (*.lib.xml)|*.lib.xml|All files (*.*)|*.*";
            saveLibraryDialog.Filter = filter;
            openLibraryDialog.Filter = filter;

            TextsSelected = false;
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

        public bool TextsSelected { get; set; }
        // the label is now "Select" which makes more sense
        // TODO: make sure all selectors use the same verbiage
        private void btnExit_Click(object sender, EventArgs e)
        {
            // TODO: if exiting from form-close, texts are auto-selected and appended to source
            // we should NOT do this, to emulate a CANCEL event
            TextsSelected = true;
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

        // TODO: cache, and only recreate if "dirty"
        public List<Text> SelectedTexts
        {
            get
            {
                var st = new List<Text>();
                st = LibrarySelector.SelectedItems.Cast<Text>().ToList();
                return st;
            }
            set { LibrarySelector.SelectedItems = value.Cast<Object>().ToList(); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            saveLibraryDialog.Title = "Save Selected Texts";

            saveLibraryDialog.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var name = saveLibraryDialog.FileName;

            var xml = SelectedTexts.ToXml();

            File.WriteAllText(name, xml);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadFromFile();
        }

        private void LoadFromFile()
        {
            DialogResult dr = openLibraryDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                var xml = string.Empty;
                using (var sr = new StreamReader(openLibraryDialog.FileName))
                {
                    xml = sr.ReadToEnd();
                    sr.Close();
                }

                var texts = new List<Text>().FromXML(xml);
                SelectedTexts = texts;
            }
        }
    }
}