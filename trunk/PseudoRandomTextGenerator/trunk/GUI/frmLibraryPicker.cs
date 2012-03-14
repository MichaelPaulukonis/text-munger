using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TextTransformer;

namespace GUI
{
    public partial class frmLibraryPicker : Form
    {
        private Dictionary<string, Library> _library;

        private frmLibraryPicker()
        {
            InitializeComponent();
        }

        // passed-in externally from app.config
        // let's leave the control ignorant of app.config.
        // for awhile ,at least.
        public frmLibraryPicker(string libraryPath)
            : this()
        {
            _library = GetLibrary(libraryPath);

            var items = _library.Keys.SelectMany(textKey => _library[textKey]).ToList();
            LibrarySelector.AvailableItems = items.Cast<object>().ToList();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // TODO: is this the best button name?
            this.Close();
        }

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

        // TODO: add a parent-ref to a library, if possible
        private Dictionary<string, Library> GetLibrary(string libraryPath, Dictionary<string, Library> libs, Library parent)
        {
            // add all files from this path (if any)
            var libname = Path.GetFileName(libraryPath);
            var lib = parent; // only re-assign if there are files
            var files = Directory.GetFiles(libraryPath);
            if (files.Length > 0)
            {
                lib = new Library(libname) { Parent = parent };

                foreach (var f in files)
                {
                    var tx = new Text(Path.GetFileNameWithoutExtension(f), f);
                    lib.AddText(tx);
                }
                libs.Add(libname, lib);
            }

            var dirs = Directory.GetDirectories(libraryPath);
            foreach (var d in dirs)
            {
                // TODO: this is where we want a parent reference
                GetLibrary(d, libs, lib);
            }

            return libs;
        }

        private Dictionary<string, Library> GetLibrary(string libraryPath)
        {
            var libs = new Dictionary<string, Library>();

            return GetLibrary(libraryPath, libs, null);
        }
    }
}