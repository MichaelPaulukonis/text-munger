using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using HtmlAgilityPack;

namespace TextSourcers
{
    [DataContract]
    public class XrmlFetch : ILibraryFetch
    {
        private string _sourceUrl = @"http://www.xradiograph.com/XraysMonaLisa/MappaMundi";

        public Dictionary<string, Library> GetLibrary()
        {
            var lib = GetXrmlWebTexts(GetXrmlPageLinks());

            return new Dictionary<string, Library> { { "XRML", lib } };
        }

        [DataMember]
        public string LibrarySource
        {
            get { return _sourceUrl; }
            set { _sourceUrl = value; } // hunh.
        }

        // sample use: _textCache = mg.GetRandomContents(GetXrmlPages(), 60);
        // unfortunately, that relied on randomization... instead of a mechanism for ALL

        // TODO: build interfaces for getting all contents, random contents, and others
        // return as Text sources
        // which will have to be updated to use an optional URL instead of just file

        private string GetRandomContents(IEnumerable<string> urls, int number)
        {
            // http://stackoverflow.com/questions/48087/select-a-random-n-elements-from-listt-in-c-sharp
            var rnd = new Random();
            var subUrls = urls.OrderBy(x => rnd.Next()).Take(5).ToArray<string>();

            return GetXrmlTextAll(subUrls);
        }

        private static string GetXrmlTextAll(IEnumerable<string> urls)
        {
            const string xpath = @"//div[@style='font-family: monospace;']";
            var sb = new StringBuilder();
            foreach (var url in urls)
            {
                try
                {
                    var source = WebFetch.Fetch(url);
                    var doc = new HtmlDocument();
                    doc.LoadHtml(source);
                    var target = doc.DocumentNode.SelectSingleNode(xpath);
                    var text = HttpUtility.HtmlDecode(target.InnerText);
                    sb.Append(text);
                }
                catch (Exception ex)
                {
                    // TODO: swallow it whole!
                    // yikes. or maybe log the issue, down the road....
                }
            }

            return sb.ToString();
        }

        private static Library GetXrmlWebTexts(IEnumerable<string> urls)
        {
            var lib = new Library("XRML pages");

            foreach (var url in urls)
            {
                var myUri = new Uri(url);
                var title = System.IO.Path.GetFileName(myUri.AbsolutePath);

                lib.Add(new InternetText(title, url, new XrmlTextExtractor()));
            }

            return lib;
        }

        // perhaps cache this list locally, and only regen as required?
        // because it's a pain to pull....
        // that should be true of all of of web-pulls
        // cache locally, and only rebuild as required.
        // hrm.... MORE COMPLICATED THAT I CURRENTLY REQUIRE
        private List<string> GetXrmlPageLinks()
        {
            var source = WebFetch.Fetch(LibrarySource);
            var doc = new HtmlDocument();
            doc.LoadHtml(source);
            var target = doc.DocumentNode.SelectNodes("//div[@class='mainlist']/div/ul/li/a");

            return target.Select(link => link.Attributes["href"].Value).ToList();
        }
    }

    [DataContract]
    public class XrmlTextExtractor : IExtractor
    {
        public string Extract(string url)
        {
            const string xpath = @"//div[@style='font-family: monospace;']";
            var source = WebFetch.Fetch(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(source);
            var target = doc.DocumentNode.SelectSingleNode(xpath);
            var text = HttpUtility.HtmlDecode(target.InnerText);

            return text;
        }
    }
}