using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace TextSourcers
{
    public class Library : IEnumerable<IText>
    {
        public Library()
        { }

        public Library(string title)
        {
            Title = title;
        }

        public string Title { get; set; }

        public string Path { get; set; }

        // TODO: we need key:value pairs on Title:Text
        private Dictionary<string, IText> _contents = new Dictionary<string, IText>();

        public Dictionary<string, IText> Contents
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

        public Library Parent { get; set; }

        public IEnumerator<IText> GetEnumerator()
        {
            return _contents.Values.GetEnumerator();
        }

        public override string ToString()
        {
            var t = string.Empty;
            if (Parent != null)
            {
                t = Parent.Title + ":";
            }

            t += Title;

            return t;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public interface IText
    {
        string Title { get; set; }

        string Contents { get; set; }

        string Location { get; set; }
    }

    public class Text : IText
    {
        public Text() { }

        public Text(string title, string path)
        {
            Title = title;
            Location = path;
        }

        public string Title { get; set; }

        public string Location { get; set; }

        private string _contents;

        // allow for setting, so we can have arbitrary texts that are not file-based
        // not sure where to go with that, though....
        // this could be if, say, we add all generated texts into the library-pool
        // which would get really, really weird
        // probably better off dumping generated texts into a folder
        // that is then part of the library....
        public virtual string Contents
        {
            get
            {
                if (_contents == null)
                {
                    if (Location == null)
                    {
                        throw new MissingFieldException("Either set Path or first add contents before retrieving contents");
                    }
                    using (var sr = new StreamReader(Location))
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

        public override string ToString()
        {
            return String.Format("{0}:{1}", Parent.ToString(), Title);
        }
    }

    public class InternetText : Text, IText
    {
        public InternetText() : base() { }

        public InternetText(string title, string url) : base(title, url) { }

        public override string Contents { get; set; }
    }
}