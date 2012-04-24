using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TextSourcers;

namespace TextSourcers
{
    public class FileLibraryFetch : ILibraryFetch
    {
        public FileLibraryFetch(string source)
        {
            this.LibrarySource = source;
        }

        public string LibrarySource { get; set; }

        // ganked from frmLibraryPicker
        // wrapper for Recursive GetLibrary() method
        public Dictionary<string, Library> GetLibrary()
        {
            var libs = new Dictionary<string, Library>();

            return GetLibrary(LibrarySource, libs, null);
        }

        // ganked from frmLibraryPicker
        private Dictionary<string, Library> GetLibrary(string libraryPath, Dictionary<string, Library> libs, Library parent)
        {
            // add all files from this path (if any)
            var libname = Path.GetFileName(libraryPath);
            var lib = parent; // only re-assign if there are files
            var files = Directory.GetFiles(libraryPath);
            if (files.Length > 0)
            {
                var tempLib = lib;
                tempLib = new Library(libname) { Parent = parent };

                foreach (var f in files)
                {
                    if (Path.GetExtension(f).ToLower() == ".txt")
                    {
                        var tx = new Text(Path.GetFileNameWithoutExtension(f), f);
                        tempLib.Add(tx);
                    }
                }

                // if a non-.txt file is in files, we will create a lib, but it will be empty
                // disregard
                if (tempLib.Count() > 0)
                {
                    libs.Add(libname, tempLib);
                    lib = tempLib;
                }
            }

            var dirs = Directory.GetDirectories(libraryPath);
            foreach (var d in dirs)
            {
                // TODO: this is where we want a parent reference
                GetLibrary(d, libs, lib);
            }

            return libs;
        }
    }
}