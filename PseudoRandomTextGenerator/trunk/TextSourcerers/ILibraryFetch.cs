using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextSourcers;

namespace TextSourcers
{
    public interface ILibraryFetch
    {
        // sourceLocation may be a filePath, URL, or ... is there anything else it could be?
        Dictionary<string, Library> GetLibrary();

        string LibrarySource { get; set; }
    }

    public interface IExtractor
    {
        string Extract(string source);
    }
}