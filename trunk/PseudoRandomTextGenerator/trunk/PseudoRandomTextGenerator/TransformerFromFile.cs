using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TextTransformer
{
    // TODO: instead of just Replacers, this would lend itself to ANY word-level, list-based transformation
    //       ie, mis-spellings, etc.
    //       NOT, however, leet-speak or other REGEX-level transformers
    //       or.... would it???
    public class TransformerFromFile : ITransformer
    {
        public TransformerFromFile(string sourceFile)
        {
            // eg  @"D:\Dropbox\projects\TextMunger\homophone_list.txt";
            SourceFile = sourceFile;

            // TODO: Smart/Dumb setting: smart = whole words only, dumb = inner-strings
            //       so we get classic := clbuttic
        }

        public string SourceFile { get; private set; }

        public string Source { get; set; }

        private string _m = null;

        public string Munged
        {
            get { return Munge(); }
        }

        private string Munge()
        {
            // if we process multiple words, add a space as padding
            // if we process a single word, no padding
            // and, yeah, this will only work on space-padded multi-words. c'est la vie.
            var words = TransformerTools.SplitToWords(Source);
            var padding = (words.Any()) ? " " : string.Empty;

            var sb = new StringBuilder();
            var rnd = new Random();

            foreach (var word in words)
            {
                var replace = word;
                // if word is in dictionary
                // replace with a homophone
                // if multiples, random of quantity
                if (Replacers.ContainsKey(word))
                {
                    var index = rnd.Next(0, Replacers[word].Count); // random.next range := 0..(Count-1)
                    replace = Replacers[word][index];
                }
                sb.Append(replace + padding);
            }

            return sb.ToString();
        }

        public Granularity Granularity { get { return Granularity.Word; } }

        private Dictionary<string, List<string>> _replacers = null;

        private Dictionary<string, List<string>> Replacers
        {
            get { return _replacers ?? (_replacers = GetReplacers()); }
        }

        // TODO: replace "Replacers" with something meaningful
        // pairs? ugh. word-tuples? word-pairs?
        private Dictionary<string, List<string>> GetReplacers()
        {
            var Replacers = new Dictionary<string, List<string>>();

            using (var reader = new StreamReader(SourceFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("#")) { continue; } // comment-character

                    // TODO: while this works, it makes no sense
                    // because the file is not read until the rule is used
                    // we need the method that looks for all file-translators
                    // to parse the name and return it to the calling factory
                    if (line.ToUpper().StartsWith("NAME:"))
                    {
                        var ps = line.Split(':');
                        if (ps.Length > 1)
                        {
                            Name = ps[1];
                        }
                        continue;
                    }
                    var pieces = line.Split(',');

                    var variants = Permutations(pieces);
                    foreach (var variant in variants)
                    {
                        var key = variant.ElementAt(0).Trim();
                        if (!Replacers.ContainsKey(key))
                        {
                            var vals = variant.Skip(1).Take(variant.Count() - 1).ToList();
                            Replacers.Add(key, vals);
                        }
                    }
                }
            }

            return Replacers;
        }

        // http://stackoverflow.com/a/5129643/41153
        private static IEnumerable<IEnumerable<T>> Permutations<T>(IEnumerable<T> source)
        {
            var c = source.Count();
            if (c == 1)
                yield return source;
            else
                for (int i = 0; i < c; i++)
                    foreach (var p in Permutations(source.Take(i).Concat(source.Skip(i + 1))))
                        yield return source.Skip(i).Take(1).Concat(p);
        }

        public string Name { get; set; }

        // TODO: take a name from the file
        public override string ToString()
        {
            return Name ?? "Homphonerize";
        }
    }
}