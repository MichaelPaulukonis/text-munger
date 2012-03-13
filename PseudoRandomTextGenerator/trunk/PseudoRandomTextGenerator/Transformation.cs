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
            get { return Munge(); }
        }

        public Granularity Granularity { get { return Granularity.Word; } }

        private string Munge()
        {
            return Source.ToUpper();
        }
    }

    public class RandomCaps : ITransformer
    {
        public string Source { get; set; }

        private string _m = null;

        public string Munged
        {
            //get { return _m ?? (_m = Munge()); }
            get { return Munge(); }
        }

        private string Munge()
        {
            var rnd = new Random();

            var c = Source.ToLower().ToCharArray();
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
    }

    public class Disemvowell : ITransformer
    {
        public string Source { get; set; }

        private string _m = null;

        public string Munged
        {
            get { return Munge(); }
        }

        private string Munge()
        {
            var regex = new Regex(@"[aeiouAEIOU]+");
            var munged = regex.Replace(Source, "");

            return munged;
        }

        public Granularity Granularity { get { return Granularity.Word; } }
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
            get { return Munge(); }
        }

        private string Munge()
        {
            // TODO: this is a naive implementation.
            // however, it doesn't replace multiple spaces with a single punct, that's something....

            var output = Regex.Replace(Source, @"\s", Mark);

            return output;
        }

        public Granularity Granularity { get { return Granularity.Sentence; } }

        public object Clone()
        {
            var c = new PunctuizeWhitespace();

            c.Mark = this.Mark;

            return c;
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
            get { return Munge(); }
        }

        private string Munge()
        {
            var regex = new Regex(@"[aeiouAEIOU]+");
            var munged = regex.Replace(Source, Mark);

            return munged;
        }

        public Granularity Granularity { get { return Granularity.Word; } }
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
    }

    public class XrmlFormat : ITransformer
    {
        public string Source { get; set; }

        private string _m = null;

        public string Munged
        {
            get { return Munge(); }
        }

        private string Munge()
        {
            // 80 character lines. that's about it, so far.....
            var mod = Regex.Replace(Source, "(.{1,81})", "$1\n");

            return mod;
        }

        public Granularity Granularity { get { return Granularity.All; } }
    }

    public class Density : ITransformer
    {
        private Random _rnd = new Random();

        public string Source { get; set; }

        private int _p = 97; // default
        public int Percentage
        {
            get { return _p; }
            set
            {
                if (value < 0 || value > 100)
                {
                    var msg = string.Format("'{0}' is invalid; Percentage must be between 0 and 100", value);
                    throw new ArgumentOutOfRangeException(msg);
                }
                _p = value;
            }
        }

        public string Munged
        {
            get { return Munge(); }
        }

        private string Munge()
        {
            // TODO: density notes...
            // break into words
            // loop through words
            // add punctuation between words
            // depending upon density
            // 0 = all punct, no source
            // 100 = all source, no punct
            var words = TransformerTools.SplitToWords(Source);
            var sb = new StringBuilder();
            foreach (var word in words)
            {
                var t = GetPadding(Percentage);
                if (Percentage > 0)
                {
                    // skip word if density = 0% all punct
                    t += word;
                }
                sb.Append(t);
            }

            return sb.ToString();
        }

        public Granularity Granularity { get { return Granularity.Sentence; } }

        // TODO: eventually, drop the word in here, too
        //       so we can break it apart sometimes for lower density
        private string GetPadding(int textPercent)
        {
            // here we are concerned with the density of the punctuation
            // so, invert the external text value
            var punctPercent = (100 - textPercent);

            // now
            // 100 = ALL PUNCT ALLA TIME
            // 0   = no punct waaaah

            var rnd = new Random();
            const char block = '.';

            // also, is annoying regular -- same amount EVERY SINGLE TIME
            // need to introduce some randomness

            var amt = GetPunctAmount(punctPercent);

            return new string(block, amt);
        }

        private double _boundary = 6.0;

        public double Boundary
        {
            get { return _boundary; }
            set { _boundary = value; }
        }

        // TODO: I suppose randomization should be able to be turned on/off....
        // http://stackoverflow.com/questions/706952/smooth-movement-to-ascend-through-the-atmosphere/707035#707035
        public int GetPunctAmount(int density)
        {
            Double minPuncts = 0;
            Double maxPuncts = 1840;

            Int32 numberSteps = 101;

            // Positive values produce ascending functions.
            // Negative values produce descending functions.
            // Values with smaller magnitude produce more linear functions.
            // Values with larger magnitude produce more step like functions.
            // Zero causes an error.
            // Try for example +1.0, +6.0, +20.0 and -1.0, -6.0, -20.0
            //Double boundary = +6.0;
            var boundary = Boundary;

            //for (Int32 density = 0; density <= numberSteps; density++)
            {
                Double t = -boundary + 2.0 * boundary * density / (numberSteps - 1);
                Double correction = 1.0 / (1.0 + Math.Exp(Math.Abs(boundary)));
                Double value = 1.0 / (1.0 + Math.Exp(-t));
                Double correctedValue = (value - correction) / (1.0 - 2.0 * correction);
                var curPuncts = (correctedValue * (maxPuncts - minPuncts) + minPuncts);

                var flatPuncts = (int)Math.Round(curPuncts);
                var offset = RandomOffset(flatPuncts);
                flatPuncts += offset;

                if (flatPuncts > maxPuncts) flatPuncts = (int)maxPuncts;
                if (flatPuncts < minPuncts) flatPuncts = (int)minPuncts;

                return flatPuncts;
            }
        }

        public int RandomOffset(int density)
        {
            // http://stackoverflow.com/questions/2751938/random-number-within-a-range-based-on-a-normal-distribution

            double mean = density;
            double deviation = density * 2;

            var u1 = _rnd.NextDouble();
            var u2 = _rnd.NextDouble();
            // not sure what this line is figureing out
            // ugh.
            var normal = Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2);
            var offset = ((normal * deviation) + mean) / 2;

            if (offset > density) offset = density;
            if (offset < -density) offset = -density;

            return (int)Math.Round(offset);
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