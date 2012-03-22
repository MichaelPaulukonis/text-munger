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
        private Dictionary<string, Library> _libraries;

        private frmLibraryPicker()
        {
            InitializeComponent();
        }

        // passed-in externally from app.config
        // let's leave the control ignorant of app.config.
        // for awhile ,at least.
        // TODO: since we can have an Internet Library, now, alternative pass in the library
        public frmLibraryPicker(string libraryPath)
            : this()
        {
            _libraries = GetLibraries(libraryPath);

            var items = _libraries.Keys.SelectMany(textKey => _libraries[textKey]).ToList();
            LibrarySelector.AvailableItems = items.Cast<object>().ToList();
        }

        // TODO . how to keep from repeating crap. - we don't want to pass in GetLibraries(libraryPath), do we?
        // TODO: call this so we can test
        public frmLibraryPicker(Library lib)
            : this()
        {
            _libraries = new Dictionary<string, Library>();
            _libraries.Add(lib.Title, lib);

            var items = _libraries.Keys.SelectMany(textKey => _libraries[textKey]).ToList();
            LibrarySelector.AvailableItems = items.Cast<object>().ToList();
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
                    if (Path.GetExtension(f).ToLower() == ".txt")
                    {
                        var tx = new Text(Path.GetFileNameWithoutExtension(f), f);
                        lib.AddText(tx);
                    }
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

        private Dictionary<string, Library> GetLibraries(string libraryPath)
        {
            var libs = new Dictionary<string, Library>();

            return GetLibrary(libraryPath, libs, null);
        }
    }
}