using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TextTransformer
{
    public class Library
    {
        public Library()
        {
        }

        public Library(string title)
        {
            Title = title;
        }

        public string Title { get; set; }

        public string Path { get; set; }

        // TODO: we need key:value pairs on Title:Text
        private Dictionary<string, Text> _contents = new Dictionary<string, Text>();

        public Dictionary<string, Text> Contents
        {
            get { return _contents; }
            set { _contents = value; }
        }

        public Library AddText(Text text)
        {
            text.Parent = this;
            Contents.Add(text.Title, text);
            return this;
        }
    }

    public class Text
    {
        public Text() { }

        public Text(string title, string path)
        {
            Title = title;
            Path = path;
        }

        public string Title { get; set; }

        public string Path { get; set; }

        private string _contents;

        // allow for setting, so we can have arbitrary texts that are not file-based
        // not sure where to go with that, though....
        public string Contents
        {
            get
            {
                if (_contents == null)
                {
                    if (Path == null)
                    {
                        throw new MissingFieldException("Either set Path or first add contents before retrieving contents");
                    }
                    using (var sr = new StreamReader(Path))
                    {
                        _contents = sr.ReadToEnd();
                        sr.Close();
                    }
                }

                return _contents;
            }
            set { _contents = value; }
        }

        public Library Parent { get; set; }
    }
}