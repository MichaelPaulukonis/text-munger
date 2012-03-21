using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using TextSourcers;

// TODO: what will be the common element?
// look at these things, and build an Interface. or something....
// and SourcerFactory. A SourcerSourcer
namespace TextSourcerers
{
    internal class GutenbergFetch
    {
        // TODO: build interfaces for getting all contents, random contents, and others
        // return as Text sources
        // which will have to be updated to use an optional URL instead of just file

        private String GetGutenberg()
        {
            var urls = GetRecentGutenbergBooks();
            var rnd = new Random();
            var source = string.Empty;

            // since a given text might not actually have a .txt.utf8 version
            // pull another item if we've got an empty one
            // TODO: we're redoing the randomization each time.
            // UGH. however, this should all be temporary, not part of the final product....
            while (source.Length == 0)
            {
                var subUrls = urls.OrderBy(x => rnd.Next()).Take(1).ToArray<string>();
                source = WebFetch.Fetch(subUrls[0]);
            }

            return source; // in this case , it's string data
        }

        private IEnumerable<string> GetRecentGutenbergBooks()
        {
            var outlinks = new List<string>();

            // <div class="pgdbrecent">

            //<h2><a name="a267"></a><a href="/browse/authors/a#a267">Appleton, Victor [pseud.]</a></h2>

            //<ul>
            //  <li class="pgdbetext"><a href="/ebooks/952">Tom Swift and His Air Glider, or Seeking the Platinum Treasure</a> (English) (as Author)</li>
            //  <li class="pgdbetext"><a href="/ebooks/4711">Tom Swift in the City of Gold, or, Marvelous Adventures Underground</a> (English) (as Author)</li>
            //</ul>
            const string url = "http://www.gutenberg.org/browse/recent/last1";
            const string xpath = "//div[@class='pgdbrecent']/ul/li/a";
            var source = WebFetch.Fetch(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(source);

            var links = doc.DocumentNode.SelectNodes(xpath);

            foreach (var l in links)
            {
                // TODO: this does not always exists
                // so we will need to trap for missing files....
                var link = "http://www.gutenberg.org" + l.Attributes["href"].Value + ".txt.utf8";
                outlinks.Add(link);
            }

            return outlinks;
        }
    }
}