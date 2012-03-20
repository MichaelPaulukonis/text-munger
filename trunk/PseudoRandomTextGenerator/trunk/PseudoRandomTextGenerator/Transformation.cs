using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TextTransformer
{
    // TODO: indicates the minimum level for the rule to run on
    //       ie, an "All" should not run on a char
    // I've commented out unused granularities, for now.
    public enum Granularity
    {
        Unknown = -1,
        //Any = 0,      I don't think we need this; all existing uses have been moved to Word
        //Character = 1,
        Word = 2,
        Sentence = 3,   // yeah, maybe
        //Paragraph = 4,  // yeah. okay. so what?
        //Page = 5,       // no idea how this would be defined
        All = 100       // leaving room for weird expansion. although refactoring should take care of that. except for serialization...
    }

    // do NOT implement ICloneable on interface
    // as only those Transformers that have distinct settings require it
    public interface ITransformer
    {
        string Source { get; set; }

        string Munged { get; }

        Granularity Granularity { get; }
    }

    internal static class TransformerTools
    {
        // this is ganked from MarkovGenerator.cs:public class DefaultRule : IMarkovRule
        // which suggests it is common code....
        internal static IList<string> SplitToWords(string subject)
        {
            var regex = new Regex(@"\s+");

            var splitted = regex.Split(subject).ToList();

            return splitted;
        }

        internal static IList<string> SplitToChars(string subject)
        {
            var regex = new Regex(@".");

            var splitted = regex.Split(subject).ToList();

            return splitted;
        }
    }

    public class Shouty : ITransformer
    {
        public string Source { get; set; }

        private string _m = null;

        public string Munged
        {
            get { return Munge(Source); }
        }

        private string Munge(string source)
        {
            return source.ToUpper();
        }

        public Granularity Granularity { get { return Granularity.Word; } }

        public override string ToString()
        {
            return Munge("shouty");
        }
    }

    public class RandomCaps : ITransformer
    {
        public string Source { get; set; }

        private string _m = null;

        public string Munged
        {
            //get { return _m ?? (_m = Munge()); }
            get { return Munge(Source); }
        }

        private string Munge(string source)
        {
            var rnd = new Random();

            var c = source.ToLower().ToCharArray();
            for (var i = 0; i < c.Length; ++i)
            {
                if (rnd.Next(0, 100) > 50)
                {
                    c[i] = c[i].ToString().ToUpper().ToCharArray()[0];
                }
            }

            return new string(c);
        }

        public Granularity Granularity { get { return Granularity.Word; } }

        public override string ToString()
        {
            return Munge("RandomCaps");
        }
    }

    public class Disemconsonant : ITransformer
    {
        public string Source { get; set; }

        private string _m = null;

        public string Munged
        {
            get { return Munge(); }
        }

        private string Munge()
        {
            var regex = new Regex(@"[bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ]+");
            var munged = regex.Replace(Source, "");

            return munged;
        }

        public Granularity Granularity { get { return Granularity.Word; } }

        public override string ToString()
        {
            // honestly, removing the consonsants makes this impenetrable
            return "Disemconsonant";
        }
    }

    public class Disemvowell : ITransformer
    {
        public string Source { get; set; }

        public string Munged
        {
            get { return Munge(Source); }
        }

        private string Munge(string source)
        {
            var regex = new Regex(@"[aeiouAEIOU]+");
            var munged = regex.Replace(source, "");

            return munged;
        }

        public Granularity Granularity { get { return Granularity.Word; } }

        public override string ToString()
        {
            return Munge("Disemvowell");
        }
    }

    // clone, as the Mark can be set independently
    public class PunctuizeWhitespace : ITransformer, ICloneable
    {
        public PunctuizeWhitespace()
        {
            Mark = "."; // default
        }

        public string Mark { get; set; }

        public string Source { get; set; }

        private string _m = null;

        public string Munged
        {
            get { return Munge(Source); }
        }

        private string Munge(string source)
        {
            // TODO: this is a naive implementation.
            // however, it doesn't replace multiple spaces with a single punct, that's something....

            var output = Regex.Replace(source, @"\s", Mark);

            return output;
        }

        public Granularity Granularity { get { return Granularity.Sentence; } }

        public object Clone()
        {
            var c = new PunctuizeWhitespace();

            c.Mark = this.Mark;

            return c;
        }

        public override string ToString()
        {
            return Munge("Punctuize Whitespace");
        }
    }

    public class VowellToPunct : ITransformer
    {
        public VowellToPunct()
        {
            Mark = "."; // default
        }

        public string Source { get; set; }

        public string Mark { get; set; }

        private string _m = null;

        public string Munged
        {
            get { return Munge(Source); }
        }

        private string Munge(string source)
        {
            var regex = new Regex(@"[aeiouAEIOU]+");
            var munged = regex.Replace(source, Mark);

            return munged;
        }

        public Granularity Granularity { get { return Granularity.Word; } }

        public override string ToString()
        {
            return Munge("VowellToPunct");
        }
    }

    public class Reverse : ITransformer
    {
        public string Source { get; set; }

        private string _m = null;

        public string Munged
        {
            get { return Munge(); }
        }

        private string Munge()
        {
            return new string(Source.Reverse().ToArray());
        }

        public Granularity Granularity { get { return Granularity.Word; } }

        public override string ToString()
        {
            return "reserveR";
        }
    }

    // TODO: instead of just homophones, this would lend itself to ANY word-level, list-based transformation
    //       ie, mis-spellings, etc.
    //       NOT, however, leet-speak or other regex-level transformers
    public class Homophonic : ITransformer
    {
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
                if (Homophones.ContainsKey(word))
                {
                    var index = rnd.Next(0, Homophones[word].Count); // random.next range := 0..(Count-1)
                    replace = Homophones[word][index];
                }
                sb.Append(replace + padding);
            }

            return sb.ToString();
        }

        public Granularity Granularity { get { return Granularity.Word; } }

        private Dictionary<string, List<string>> _homophones = null;

        private Dictionary<string, List<string>> Homophones
        {
            get { return _homophones ?? (_homophones = GetHomophones()); }
        }

        private Dictionary<string, List<string>> GetHomophones()
        {
            var homophones = new Dictionary<string, List<string>>();

            const string path = @"D:\Dropbox\projects\TextMunger\homophone_list.txt";
            using (var reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("#")) { continue; } // comment-character

                    var pieces = line.Split(',');

                    var variants = Permutations(pieces);
                    foreach (var variant in variants)
                    {
                        var key = variant.ElementAt(0).Trim();
                        if (!homophones.ContainsKey(key))
                        {
                            var vals = variant.Skip(1).Take(variant.Count() - 1).ToList();
                            homophones.Add(key, vals);
                        }
                    }
                }
            }

            return homophones;
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

        public override string ToString()
        {
            return "Homphonerize";
        }
    }

    //       shuffle -- problem: depends upon atomicity
    //       for now, let's assume word-level, that is, shuffle the chars in a word
    public class Shuffle : ITransformer
    {
        public string Source { get; set; }

        public string Munged
        {
            get { return Munge(); }
        }

        public Granularity Granularity { get { return Granularity.Word; } }

        private string Munge()
        {
            // http://stackoverflow.com/a/5383519/41153
            var s = Source.ToCharArray();
            var rnd = new Random();
            var result = s.OrderBy(item => rnd.Next());

            return string.Join(string.Empty, result.ToArray());
        }

        public override string ToString()
        {
            return "Shuffle";
        }
    }

    public class PigLatin : ITransformer
    {
        public string Source { get; set; }

        public string Munged
        {
            get { return Munge(Source); }
        }

        public Granularity Granularity
        {
            get { return Granularity.Word; }
        }

        // based on code @ http://stackoverflow.com/questions/4098178/c-sharp-translator-from-pig-latin-to-english
        // TODO: final punctuation should come at the end of the word, not in the middle...
        private static string Munge(string word)
        {
            string pigLatinOut = string.Empty;
            if (word.Length > 0)
            {
                const string vowel = "AEIOUaeiou";

                string afterFirst = word.Substring(1);
                string firstLetter = word.Substring(0, 1);
                var x = firstLetter.IndexOf(vowel);

                if (x == -1)
                {
                    pigLatinOut = (afterFirst + firstLetter + "ay ");
                }
                else
                {
                    pigLatinOut = (firstLetter + afterFirst + "way ");
                }
            }
            return pigLatinOut;
        }

        public override string ToString()
        {
            return "PigLatin";
        }
    }

    public class Leet : ITransformer
    {
        // transform code based on http://stackoverflow.com/a/3216008/41153

        private Dictionary<string, List<string>> _rules;

        private Random _rnd;

        public Leet(Random rnd)
        {
            _rnd = rnd;

            _rules = new Dictionary<string, List<string>>();

            // TODO: read these from a file
            // then, we have another class that can be sub-classed based on the rule-file

            // TODO: aaaargh, bitten by case-sensitivity, again.... !!!

            AddRule("A", "4");
            AddRule("A", @"/\");
            AddRule("A", "@");
            AddRule("A", "^");

            AddRule("B", "13");
            AddRule("B", "/3");
            AddRule("B", "|3");
            AddRule("B", "8");

            AddRule("X", "><");

            AddRule("C", "<");
            AddRule("C", "(");

            AddRule("D", "|)");
            AddRule("D", "|>");

            AddRule("E", "3");

            AddRule("G", "6");

            AddRule("H", "/-/");
            AddRule("H", "[-]");
            AddRule("H", "]-[");

            AddRule("I", "!");

            AddRule("L", "|_");

            AddRule("J", "_/");
            AddRule("J", "_|");

            AddRule("L", "1");

            AddRule("O", "0");

            AddRule("S", "5");

            AddRule("T", "7");

            AddRule("W", @"\/\/");
            AddRule("V", @"\/");

            AddRule("Z", "2");
        }

        public Leet() : this(new Random()) { }

        private void AddRule(string plain, string leet)
        {
            if (!_rules.ContainsKey(plain))
            {
                var rule = new List<string> { leet };
                _rules.Add(plain, rule);
            }
            else
            {
                _rules[plain].Add(leet);
            }
        }

        public string Source { get; set; }

        public string Munged
        {
            get { return Munge(); }
        }

        public Granularity Granularity { get { return Granularity.Word; } }

        // as always, we might want to not always replace every single letter
        // so, think about an internal randomizer, that we can configure.....
        private string Munge()
        {
            var munged = Source;

            foreach (var rule in _rules)
            {
                // http://stackoverflow.com/questions/3993826/case-insensitive-replace-without-using-regular-expression-in-c
                var i = munged.IndexOf(rule.Key, StringComparison.OrdinalIgnoreCase);
                if (i > 0) // short-circuit all evals if rule-char not in word
                {
                    string leet;
                    // rule may have more than one replacer....
                    if (rule.Value.Count() == 1)
                    {
                        leet = rule.Value[0];
                    }
                    else
                    {
                        var index = _rnd.Next(0, rule.Value.Count());
                        leet = rule.Value[index];
                    }

                    // currently, our keys are all length := 1
                    // but who knows....
                    var len = rule.Key.Length;
                    munged = munged.Replace(munged.Substring(i, len), leet);
                }
            }

            return munged;
        }

        public override string ToString()
        {
            return "L33t";
        }
    }
}