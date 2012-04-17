using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Serialization;
using HtmlAgilityPack;
using TextSourcers;
using TextTransformer;

namespace Runner
{
    public class ConsoleRunner
    {
        private MarkovGenerator _textGenerator;
        private static string _textCache = string.Empty;

        public void SetUp()
        {
            // no parm = default keysize of 2
            //_textGenerator = new MarkovGenerator();
            _textGenerator = new MarkovGenerator(2);
        }

        private static void Main(string[] args)
        {
            var c = new ConsoleRunner();

            c.TestAlphaSpacing();

            //c.TestRuleSerialization();

            //var mg = new ConsoleRunner();
            //mg.MungeIt();
        }

        public void TestAlphaSpacing()
        {
            var source = "apple\nbaker\nzebra\n!whatever\n\n....;'hello";
            var lines = Regex.Split(source, "\r\n|\r|\n");
            foreach (var line in lines)
            {
                if (line.Length > 0)
                {
                    var firstLetter = line.ToLower()[0];
                    const int aAsInt = 97;
                    const int zAsInt = 123;
                    var offset = ((firstLetter >= aAsInt && firstLetter <= zAsInt) ? firstLetter - aAsInt : 0);
                }
            }
        }

        // testing Markov Rules, to start
        // had to make changes to the Markov Generator.
        // but now we can serialize/deserialize
        // plus, some names make a tiny bit more sense
        // I'm rethinking the whole "MarkovRuleType" necessity.
        // it's almos all params EXCEPT for the XrayWord type -- that has words and puncts as tokens.
        // can that be done with a regex?
        public void TestRuleSerialization()
        {
            // List<ITransformer> globalRules

            //// THIS WORKS
            //var l = new Leet();
            //var lx = l.ToXml();
            //var ll = new Leet().FromXML(lx);

            // THIS does NOT work
            var tf = new TransformationFactory(Granularity.Word);
            var rules = tf.GetTransformers();
            // cannot deserialize Leet....
            var rx = rules.ToXml();
            var rr = new List<TransformerBase>().FromXML(rx);
        }

        public void TestXrmlSerialization()
        {
            var x = new XrmlFormat();
            x.LineLength = 2;
            x.Density.Percentage = 50;

            var xx = x.ToXml();
            var x2 = new XrmlFormat().FromXML(xx);

            x2.Source =
                "This and that are some. More of the. Oh, whatever the that, then. You know this? This is what I mean. That or that and or of yes.";
            Console.WriteLine(x2.Munged);

            Console.ReadKey();
        }

        public void TestMarkovSerialization()
        {
            var m = new MarkovGenerator(MarkovRuleType.XrayChar, 3);

            var xm = m.ToXml();

            var mm = new MarkovGenerator().FromXML(xm);

            mm.Source =
                "This and that are some. More of the. Oh, whatever the that, then. You know this? This is what I mean. That or that and or of yes.";
            Console.WriteLine(mm.Munged);

            Console.ReadKey();
        }

        //http://www.joe-stevens.com/2009/12/29/json-serialization-using-the-datacontractjsonserializer-and-c/
        public void TestLibrarySerialization()
        {
            var path = @"D:\Dropbox\projects\TextMunger\Library\scripts\SWSECOND.TXT";
            var path2 = @"D:\Dropbox\projects\TextMunger\Library\scripts\alien_3_fasano_planet_wood.TXT";

            var t = new Text("The Star Wars", path);
            var t2 = new Text("Alien3", path2);

            var aa = t.ToXml_nonworking();
            var bb = new Text().FromXMLnonWorking(aa);

            //Console.WriteLine(aa);

            var l = new Library("Test");
            l.Add(t);
            l.Add(t2);

            // we're getting an array of Text back, not an actual Library
            // the Title and Path are not included in the XML
            var ll = l.ToXml();

            Console.WriteLine(ll);

            var mm = new Library().FromXML(ll);

            Console.WriteLine(mm);

            // the output has no reference to the class type, nor the Extractor (of course -- it's not serialized)
            // so, what CAN we serialize? some sort of enum that is passed to a factory?
            // worry about this later.
            // serialize the text-based library
            // which is the most important
            //var it = new InternetText("Title", "http://www.xradiograph.com/XraysMonaLisa/BlindHag", new XrmlTextExtractor());
            //var j2 = it.ToJSON();

            Console.ReadKey();
        }

        // TODO: in XRML, at any rate, this seems to go in reverse and not come back
        public void TestRandomWalker()
        {
            var walker = new RandomWalker(20, 5, 10);
            for (var i = 0; i < 1000; ++i)
            {
                Console.WriteLine(string.Format("{0}", walker.Next()));
            }
        }

        // I want to get some variance for the density padding
        // but I want the varance to range from 0..100%
        // so this is still not quite what I want
        public void TestRandom()
        {
            // http://stackoverflow.com/questions/2751938/random-number-within-a-range-based-on-a-normal-distribution
            Random generator = new Random();

            var density = 1840.0;
            //var density = 5;

            double minOffset = density;
            double maxOffset = 0.0;
            var minNormal = 0.0;
            var maxNormal = 0.0;

            double mean = density;
            double deviation = density * 2;

            double total = 0.0;

            for (int i = 0; i < 100; ++i)
            {
                double u1 = generator.NextDouble();
                double u2 = generator.NextDouble();
                // not sure what this line is figureing out
                // ugh.
                double normal = Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2);
                //double offset = (normal * 8.333) + density;
                double offset = ((normal * deviation) + mean) / 2;

                Console.WriteLine(string.Format("normal: {0} offset: {1}", normal, offset));

                if (offset > density) offset = density;
                if (offset < -density) offset = -density;

                // capture stats
                if (normal > maxNormal) maxNormal = normal;
                if (normal < minNormal) minNormal = normal;
                if (offset > maxOffset) maxOffset = offset;
                if (offset < minOffset) minOffset = offset;

                total += offset;
            }
            // minOffset of 30, maxOffset around 70
            // still not sure how to use these
            Console.WriteLine(string.Format("Average offset: {0}", maxOffset / 100));
            Console.WriteLine(string.Format("\n\nMinOffset: {0} MaxOffset: {1}", minOffset, maxOffset));
            Console.WriteLine(string.Format("\n\nMinNormal: {0} MaxNormal: {1}", minNormal, maxNormal));
        }

        public void MungeIt()
        {
            //TestDensity();

            SetUp();

            // TODO: a large part of my interest is in BUILDING the sourcefile
            // allowing it to be selectable, or semi-randomized, from a given set of inputs
            // these could be source-text, online sources (stripped of HTML, possibly), or.. something

            // TODO: construct function to get a list, and randomly select sources
            // TODO: also allow for other additions, like "." frequency, etc.
            //       add in other sources, but convert the whitespace, etc.
            // this is kind of boring, actually
            // it needs some other text, inter-cut
            // with lots of extra spaces jammed into it.
            // white-space converted to periods
            // random words disemvowelled
            // and other transformations applied

            //_textCache = mg.GetRandomContents(GetXrmlPages(), 60);

            _textCache = GetSource();

            //_textCache = "abcdefghijklmnop.abc.abcd.efgh";

            //_textCache = "Once upon a time there were three bears there, once.";

            var x = new ConsoleKeyInfo();

            while (x.KeyChar != (decimal)ConsoleKey.Escape)
            {
                Console.WriteLine("Generating....\r\n\r\n");

                _textGenerator.Source = _textCache;
                var gen = _textGenerator.Write(minLength: 8000, maxLength: 10000);

                gen = ApplyGranularRules(gen);
                gen = ApplyGlobalRules(gen);

                Console.WriteLine(gen);

                Console.WriteLine("\r\n\r\nPress Any Key to ReGen; ESC to exit\r\n\r\n");

                x = Console.ReadKey(true);

                if (x.KeyChar == (decimal)ConsoleKey.S)
                {
                    var outfile = GetSaveName();
                    using (var sw = new StreamWriter(outfile))
                    {
                        sw.Write(gen);
                        sw.Close();
                    }
                    Console.WriteLine(string.Format("\r\n\r\nWritten to {0}", outfile));
                }
            }
        }

        private static string GetSaveName()
        {
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["MungedOutput"]);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var name = (string.Format("{0}.txt", DateTime.Now.ToString("yyyyMMddHHmmss")));
            return Path.Combine(dir, name);
        }

        public string GetSource()
        {
            //return GetGutenberg();
            return GetLibraryTexts();
        }

        private static string ApplyGlobalRules(string source)
        {
            var output = source;

            var rules = GetGlobalRules();
            foreach (var rule in rules)
            {
                rule.Source = output;
                output = rule.Munged;
            }

            return output;
        }

        // this is a set list for testing
        private static IEnumerable<TransformerBase> GetGlobalRules()
        {
            var l = new List<TransformerBase> {
                //new PunctuizeWhitespace(), // this conflicts with Density....
                new Density{ Percentage = 97}
                ,new XrmlFormat()
            };

            return l;
        }

        // TODO: now, create something that will apply rules in a weighted, randomized manner
        //       to some granularity of the product
        // http://stackoverflow.com/questions/56692/random-weighted-choice
        private static string ApplyGranularRules(string source)
        {
            const int threshold = 20; // 20% chance of applying the rule
            var sb = new StringBuilder();
            var rules = GetRulesGranular();

            var rnd = new Random();

            var regex = new Regex(@"\s+"); // original regex
            var words = regex.Split(source).ToList();

            foreach (var word in words)
            {
                string outword;

                if (rules.Any() && rnd.Next(0, 100) < threshold)
                {
                    var rule = rules[(rnd.Next(0, rules.Count))];
                    rule.Source = word;
                    outword = rule.Munged;
                }
                else
                {
                    outword = word;
                }

                sb.Append(outword + " ");
            }

            return sb.ToString();
        }

        // in order to get this to work, the rule output-cache was eliminated
        // this means we can't retrieve multiple times,
        // but we can run multiple times and always get new material
        private static List<TransformerBase> GetRulesGranular()
        {
            var l = new List<TransformerBase>
            {
                new Leet()
                ,new PigLatin()
                ,new Shuffle()
                ,new Disemvowell()
                ,new Disemconsonant()
                ,new RandomCaps()
                ,new VowellToPunct()
                ,new Reverse()
                ,new Shouty()
                ,new VowellToPunct()
            };

            return l;
        }

        private static string GetXrmlWebText(IEnumerable<string> urls)
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

        private string GetRandomContents(IEnumerable<string> urls, int number)
        {
            // http://stackoverflow.com/questions/48087/select-a-random-n-elements-from-listt-in-c-sharp
            var rnd = new Random();
            var subUrls = urls.OrderBy(x => rnd.Next()).Take(5).ToArray<string>();

            return GetXrmlWebText(subUrls);
        }

        // perhaps cache this list locally, and only regen as required?
        // because it's a pain to pull....
        // that should be true of all of of web-pulls
        // cache locally, and only rebuild as required.
        // hrm.... MORE COMPLICATED THAT I CURRENTLY REQUIRE
        private static List<string> GetXrmlPages()
        {
            var source = WebFetch.Fetch("http://www.xradiograph.com/XraysMonaLisa/MappaMundi");
            var doc = new HtmlDocument();
            doc.LoadHtml(source);
            var target = doc.DocumentNode.SelectNodes("//div[@class='mainlist']/div/ul/li/a");

            return target.Select(link => link.Attributes["href"].Value).ToList();
        }

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

        // for now, just getting one set text
        // in future, this will be selectable via GUI
        // may also want method to search all libs by name
        public string GetLibraryTexts()
        {
            var l = GetLibrary();

            // pick some texts at random?
            // or just decide here and now.....
            //var m = l["Manifestos"];
            var m = l["miscellaneous"];

            // yeah, this is awkward. Or is it????
            var t = m.Contents.ElementAt(0).Value.Contents;

            return t;
        }

        public Dictionary<string, Library> GetLibrary()
        {
            var libraryPath = @"D:\Dropbox\projects\TextMunger\Library";

            var libs = new Dictionary<string, Library>();

            var dirs = Directory.GetDirectories(libraryPath);
            foreach (var d in dirs)
            {
                var libname = Path.GetFileName(d);
                var lib = new Library();

                var files = Directory.GetFiles(d);
                foreach (var f in files)
                {
                    var tx = new Text(Path.GetFileNameWithoutExtension(f), f);
                    lib.Add(tx);
                }
                libs.Add(libname, lib);
            }

            return libs;
        }

        public void TestDensity()
        {
            var d = new Density { Boundary = 6.0 };

            Console.Write(string.Format("Boundary: {0}\n\n", d.Boundary));

            for (var i = 1; i <= 100; ++i)
            {
                var tot = 0;
                for (var j = 0; j <= 100; ++j)
                {
                    var x = d.GetPunctAmount(i);
                    tot += x;
                    Console.WriteLine(string.Format("Percentage: {0} puncts: {1}", i, x));
                }
                Console.WriteLine(string.Format("Average puncts: {0}", tot / 100));
                Console.ReadKey(); // pause to view before next iteration
            }

            Console.ReadLine();
        }
    }
}