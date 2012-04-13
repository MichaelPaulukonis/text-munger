using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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

    [DataContract]
    [KnownType(typeof(Shouty))]
    [KnownType(typeof(RandomCaps))]
    [KnownType(typeof(Disemconsonant))]
    [KnownType(typeof(Disemvowell))]
    [KnownType(typeof(PunctuizeWhitespace))]
    [KnownType(typeof(VowellToPunct))]
    [KnownType(typeof(Reverse))]
    [KnownType(typeof(Shuffle))]
    [KnownType(typeof(PigLatin))]
    [KnownType(typeof(Leet))]
    [KnownType(typeof(TransformerFromFile))]
    [KnownType(typeof(MarkovGenerator))]
    [KnownType(typeof(XrmlFormat))]
    [KnownType(typeof(Density))]
    public abstract class TransformerBase
    {
        public abstract string Source { get; set; }

        public abstract string Munged { get; }

        public abstract Granularity Granularity { get; set; }
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

    [DataContract]
    public class Shouty : TransformerBase
    {
        public override string Source { get; set; }

        public override string Munged
        {
            get { return Munge(Source); }
        }

        private  string Munge(string source)
        {
            return source.ToUpper();
        }

        public override Granularity Granularity { get { return Granularity.Word; } 
            set { return; }
        
        }

        public override string ToString()
        {
            return Munge("shouty");
        }
    }

    [DataContract]
    public class RandomCaps : TransformerBase
    {
        public override string Source { get; set; }

        public override string Munged
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

        public override Granularity Granularity { get { return Granularity.Word; } 
            set { return; }
        }


        public override string ToString()
        {
            return Munge("RandomCaps");
        }
    }

    public class Disemconsonant : TransformerBase
    {
        public override string Source { get; set; }

        public override string Munged
        {
            get { return Munge(); }
        }

        private string Munge()
        {
            var regex = new Regex(@"[bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ]+");
            var munged = regex.Replace(Source, "");

            return munged;
        }

        public override Granularity Granularity
        {
            get { return Granularity.Word; }
            set { return; }

        
        }

        public override string ToString()
        {
            // honestly, removing the consonsants makes this impenetrable
            return "Disemconsonant";
        }
    }

    public class Disemvowell : TransformerBase
    {
        public override string Source { get; set; }

        public override string Munged
        {
            get { return Munge(Source); }
        }

        private string Munge(string source)
        {
            var regex = new Regex(@"[aeiouAEIOU]+");
            var munged = regex.Replace(source, "");

            return munged;
        }

        public override Granularity Granularity { get { return Granularity.Word; }
            set { return; }
        
        }

        public override string ToString()
        {
            return Munge("Disemvowell");
        }
    }

    // clone, as the Mark can be set independently
    [DataContract]
    public class PunctuizeWhitespace : TransformerBase, ICloneable
    {
        public PunctuizeWhitespace()
        {
            Mark = "."; // default
        }

        [DataMember]
        public string Mark { get; set; }

        public override string Source { get; set; }

        public override string Munged
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

        public override Granularity Granularity { get { return Granularity.Sentence; }
            set { return; }
        
        }

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

    [DataContract]
    public class VowellToPunct : TransformerBase
    {
        public VowellToPunct()
        {
            Mark = "."; // default
        }

        public override string Source { get; set; }

        [DataMember]
        public string Mark { get; set; }

        public override string Munged
        {
            get { return Munge(Source); }
        }

        private string Munge(string source)
        {
            var regex = new Regex(@"[aeiouAEIOU]+");
            var munged = regex.Replace(source, Mark);

            return munged;
        }

        public override Granularity Granularity { get { return Granularity.Word; }
            set { return; }
        
        }

        public override string ToString()
        {
            return Munge("VowellToPunct");
        }
    }

    [DataContract]
    public class Reverse : TransformerBase
    {
        public override string Source { get; set; }

        public override string Munged
        {
            get { return Munge(); }
        }

        private string Munge()
        {
            return new string(Source.Reverse().ToArray());
        }

        public override Granularity Granularity { get { return Granularity.Word; }
            set { return; }
        
        }

        public override string ToString()
        {
            return "reserveR";
        }
    }

    [DataContract]
    public class Shuffle : TransformerBase
    {
        public override string Source { get; set; }

        public override string Munged
        {
            get { return Munge(); }
        }

        public override Granularity Granularity { get { return Granularity.Word; }
            set { return; }
        
        }

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

    [DataContract]
    public class PigLatin : TransformerBase
    {
        public override string Source { get; set; }

        public override string Munged
        {
            get { return Munge(Source); }
        }

        public override Granularity Granularity
        {
            get { return Granularity.Word; }
            set { return; }
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

    // TODO: convert to file for TransformerFromFile
    [DataContract]
    public class Leet : TransformerBase
    {
        // transform code based on http://stackoverflow.com/a/3216008/41153

        private Dictionary<string, List<string>> _rules;

        private static Random _rnd = new Random();

        public Leet()
        {
            BuildRuleSet();
        }

        public Leet(Random rnd)
            : this()
        {
            _rnd = rnd;
        }

        [OnDeserializing]
        public void OnDeserializing(StreamingContext ctx)
        {
            BuildRuleSet();
        }

        private void BuildRuleSet()
        {
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

        public override string Source { get; set; }

        public override string Munged
        {
            get { return Munge(); }
        }

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

        [DataMember]
        public override Granularity Granularity
        {
            get { return Granularity.Word; }
            set { return; }
        }

        public override string ToString()
        {
            return "L33t";
        }
    }
}