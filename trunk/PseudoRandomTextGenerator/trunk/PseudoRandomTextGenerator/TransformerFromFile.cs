using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TextTransformer
{
    // TODO: instead of just Replacers, this would lend itself to ANY word-level, list-based transformation
    //       ie, mis-spellings, etc.
    //       NOT, however, leet-speak or other REGEX-level transformers
    //       or.... would it???
    [DataContract]
    public class TransformerFromFile : TransformerBase
    {
        public TransformerFromFile(string sourceFile)
        {
            SourceFile = sourceFile;

            // TODO: Smart/Dumb setting: smart = whole words only, dumb = inner-strings
            //       so we get classic := clbuttic
        }

        [DataMember]
        public string SourceFile { get; set; }

        public override string Source { get; set; }

        public override string Munged
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

            // forget about mixed-caps -- too difficult to replicate with words of different lengths
            // although, if identical lengths, could make a go at it
            // but... not worth it?
            // TODO: look into using a regex for all of this. faster?
            // hunh. who knows. time it.

            // if word is in dictionary
            // replace it with replacement
            // if multiple replacements, select at random
            foreach (var word in words)
            {
                var replace = word;

                // so far, files are lowercase
                // if not, we will have to change code
                if (Replacers.ContainsKey(word.ToLower()))
                {
                    var index = rnd.Next(0, Replacers[word.ToLower()].Count); // random.next range := 0..(Count-1)
                    replace = Replacers[word.ToLower()][index];

                    if (AllCaps(word))
                    {
                        replace = replace.ToUpper();
                    }
                    else if (InitialCap(word))
                    {
                        var first = replace[0].ToString().ToUpper();
                        replace = first + replace.Substring(1);
                    }
                }
                sb.Append(replace + padding);
            }

            return sb.ToString();
        }

        private bool AllCaps(string word)
        {
            return (word.ToUpper() == word);
        }

        private bool InitialCap(string word)
        {
            return (word[0].ToString().ToUpper() == word[0].ToString());
        }

        public override Granularity Granularity { get { return Granularity.Word; }
            set { return; }
        }

        private Dictionary<string, List<string>> _replacers;

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

        [DataMember]
        public string Name { get; set; }

        // TODO: take a name from the file
        public override string ToString()
        {
            return Name ?? SourceFile ?? "TransformerFromFile";
        }
    }
}