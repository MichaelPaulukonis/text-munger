using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace TextSourcers
{
    [DataContract]
    public class Library : IEnumerable<IText>
    {
        public Library()
        { }

        public Library(string title)
        {
            Title = title;
        }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Path { get; set; }

        private Dictionary<string, IText> _contents = new Dictionary<string, IText>();

        //[DataMember]
        // TODO: failing to serialize
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

        [DataMember]
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

    [DataContract]
    public class Text : IText
    {
        public Text() { }

        public Text(string title, string path)
        {
            Title = title;
            Location = path;
        }

        public Text(string json)
        {
            var text = this.FromJSON(json);
            this.Title = text.Title;
            this.Location = text.Location;
        }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
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
            // trap for null Parents (ie, standalone texts)
            var parent = string.Empty;
            if (Parent != null)
            {
                parent = ":" + parent;
            }
            return String.Format("{0}{1}", parent, Title);
        }
    }

    // TODO: serialization model is incomplete for de-serialization
    [DataContract]
    public class InternetText : Text
    {
        public InternetText(string title, string url) : base(title: title, path: url) { }

        public InternetText(string title, string url, IExtractor ext)
            : base(title: title, path: url)
        {
            Extractor = ext;
        }

        // TODO: we lack an extractor
        // so this will fail
        // need to provide explicitly?
        // BUT THEN WE CAN'T SERIALIZE
        public InternetText(string json)
        {
            var text = this.FromJSON(json);
            this.Title = text.Title;
            this.Location = text.Location;
        }

        private IExtractor _extractor;

        private string _contents;

        public override string Contents
        {
            get
            {
                if (_contents == null)
                {
                    _contents = Extractor.Extract(this.Location);
                }
                return _contents;
            }
            set { _contents = value; }
        }

        public IExtractor Extractor
        {
            get { return _extractor; }
            set { _extractor = value; }
        }
    }
}