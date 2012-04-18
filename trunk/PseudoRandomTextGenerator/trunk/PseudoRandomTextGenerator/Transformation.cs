using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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

    [DataContract]
    [KnownType(typeof(FreeVerse))]
    [KnownType(typeof(ShortLines))]
    [KnownType(typeof(InitialSpaces))]
    [KnownType(typeof(HeijinianAidToMemory))]
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

        public abstract string Description { get; }
    }

    internal static class TransformerTools
    {
        private static Random _rnd = new Random();

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

        internal static int GetPercentage()
        {
            return _rnd.Next(0, 101); // EXCLUSIVE upper-bound
        }

        // wrapper for Random-access
        internal static int GetRandom(int lowerBound, int upperExclusive)
        {
            return _rnd.Next(lowerBound, upperExclusive);
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

        private string Munge(string source)
        {
            return source.ToUpper();
        }

        public override Granularity Granularity
        {
            get { return Granularity.Word; }
            set { return; }
        }

        public override string ToString()
        {
            return Munge("shouty");
        }

        public override string Description
        {
            get { return "Converts source to IRRITATING ALL CAPITAL LETTERS."; }
        }
    }

    [DataContract]
    public class ShortLines : TransformerBase
    {
        private static Random _rnd = new Random();

        public ShortLines()
        {
            ProbabilityNewLine = 20;
            ProbabilityMultiple = 30;
            MultipleRange = 3;
        }

        // probability of adding a newline
        // probability of inserting a space
        // number of spaces to insert
        // plus or minues number of spaces to insert
        private int _newLineProb;

        [DataMember]
        public int ProbabilityNewLine
        {
            get { return _newLineProb; }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException(paramName: "NewLineProbability", message: "Probability must be between 0 and 100");
                }

                _newLineProb = value;
            }
        }

        private int _multipleProb;

        [DataMember]
        public int ProbabilityMultiple
        {
            get { return _multipleProb; }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException(paramName: "ProbabilityMultiple", message: "ProbabilityMultiple must be between 0 and 100");
                }
                _multipleProb = value;
            }
        }

        private int _multipleRange;

        [DataMember]
        public int MultipleRange
        {
            get { return _multipleRange; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Range", "Range must be greater than 0");
                }
                _multipleRange = value;
            }
        }

        public override string Source { get; set; }

        public override string Munged
        {
            get { return Munge(Source); }
        }

        private int GetPercentage()
        {
            return _rnd.Next(0, 100);
        }

        private string Munge(string source)
        {
            var mod = source;

            var words = new DefaultRule().Split(source);
            var sb = new StringBuilder();

            foreach (var word in words)
            {
                sb.Append(word);
                var newline = TransformerTools.GetPercentage();
                if (newline <= ProbabilityNewLine)
                {
                    var lines = 1;
                    var mult = TransformerTools.GetPercentage();
                    if (mult <= ProbabilityMultiple)
                    {
                        lines += _rnd.Next(0, MultipleRange);
                    }

                    for (int i = 0; i < lines; i++)
                    {
                        sb.Append(Environment.NewLine);
                    }
                }
                else
                {
                    // random number of spaces ?
                    sb.Append(" ");
                }
            }

            return sb.ToString();
        }

        public override Granularity Granularity
        {
            get { return Granularity.All; }
            set { return; }
        }

        public override string ToString()
        {
            return "ShortLines";
        }

        public override string Description
        {
            get { return "Adds newlines after words, configurable probability for likelihood and number of lines."; }
        }
    }

    [DataContract]
    public class InitialSpaces : TransformerBase
    {
        private static Random _rnd = new Random();

        public InitialSpaces()
        {
            ProbabilityOffset = 20;
            Offset = 5;
            OffsetVariance = 5;
        }

        private int _offsetProb;

        [DataMember]
        public int ProbabilityOffset
        {
            get { return _offsetProb; }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException(paramName: "OffsetProbability", message: "OffsetProbability must be between 0 and 100");
                }
                _offsetProb = value;
            }
        }

        private int _offset;

        [DataMember]
        public int Offset
        {
            get { return _offset; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Offset", "Offset must be greater than 0");
                }
                _offset = value;
            }
        }

        private int _offsetVariance;

        [DataMember]
        public int OffsetVariance
        {
            get { return _offsetVariance; }
            set
            {
                if (value < 0 || value > Offset)
                {
                    throw new ArgumentOutOfRangeException("OffsetVariance",
                                                          "OffsetVariance must be non-zero or less than Offset");
                }

                _offsetVariance = value;
            }
        }

        public override string Source { get; set; }

        public override string Munged
        {
            get { return Munge(Source); }
        }

        private string Munge(string source)
        {
            // split by LINES
            // go through each line, random offset

            var result = Regex.Split(source, "\r\n|\r|\n");
            var sb = new StringBuilder();
            foreach (var line in result)
            {
                if (line.Length > 0) // don't waste energy on empty lines. but do preserve them
                {
                    var op = TransformerTools.GetPercentage();
                    if (op <= ProbabilityOffset)
                    {
                        // TODO: add to TransformerTools ???
                        var variance = _rnd.Next(-OffsetVariance, OffsetVariance);
                        var spaceNbr = Offset + variance;
                        var spaces = new string(' ', spaceNbr);
                        sb.Append(spaces);
                    }
                }
                sb.Append(line).Append(Environment.NewLine); // invariant
            }

            return sb.ToString();
        }

        public override Granularity Granularity
        {
            get { return Granularity.All; }
            set { return; }
        }

        public override string ToString()
        {
            return "InitialSpaces";
        }

        public override string Description
        {
            get { return "Adds randomly configurable spaces to the beginning of lines."; }
        }
    }

    /// <summary>
    /// Composites ShortLines and InitialSpaces for something that doesn't fill the margins...
    /// </summary>
    [DataContract]
    public class FreeVerse : TransformerBase
    {
        // TODO: random # of newlines
        // TODO: move preliminary spaces to new standalone, or Tool function
        // as HejinianMemoryAid will need it
        // to align to the leading alpha-spaces

        public FreeVerse()
        {
            Init();
            ProbabilityNewLine = 30;
            Offset = 10;
            OffsetVariance = 10;
            ProbabilityOffset = 50;
        }

        private InitialSpaces _spacer;
        private ShortLines _liner;

        // this remind me of olf VB6 object workarounds...
        private void Init()
        {
            _spacer = new InitialSpaces();
            _liner = new ShortLines();
        }

        [OnDeserializing]
        public void OnDeserializing(StreamingContext ctx)
        {
            Init();
        }

        [DataMember]
        public int ProbabilityNewLine
        {
            get { return _liner.ProbabilityNewLine; }
            set { _liner.ProbabilityNewLine = value; }
        }

        [DataMember]
        public int ProbabilityOffset
        {
            get { return _spacer.ProbabilityOffset; }
            set { _spacer.ProbabilityOffset = value; }
        }

        [DataMember]
        public int Offset
        {
            get { return _spacer.Offset; }
            set { _spacer.Offset = value; }
        }

        private int _offsetVariance;

        [DataMember]
        public int OffsetVariance
        {
            get { return _spacer.OffsetVariance; }
            set { _spacer.OffsetVariance = value; }
        }

        public override string Source { get; set; }

        public override string Munged
        {
            get { return Munge(Source); }
        }

        private string Munge(string source)
        {
            var mod = source;

            _liner.Source = mod;
            mod = _liner.Munged;
            _spacer.Source = mod;
            mod = _spacer.Munged;

            return mod;
        }

        public override Granularity Granularity
        {
            get { return Granularity.All; }
            set { return; }
        }

        public override string ToString()
        {
            return "FreeVerse";
        }

        public override string Description
        {
            get { return "Offsets and newlines"; }
        }
    }

    [DataContract]
    public class HeijinianAidToMemory : TransformerBase
    {
        public override string Source { get; set; }

        public override string Munged
        {
            get { return Munge(Source); }
        }

        private string Munge(string source)
        {
            // split by LINES
            var result = Regex.Split(source, "\r\n|\r|\n");
            var sb = new StringBuilder();
            foreach (var line in result)
            {
                if (line.Length > 0) // don't waste energy on empty lines. but do preserve them
                {
                    // space-number is based on uncased alpha of first char
                    // a:=0, b:=1..z:=25
                    // punctuation will be considered as a
                    var firstLetter = line.ToLower()[0];

                    const int aAsInt = 97;
                    const int zAsInt = 123;
                    var offset = ((firstLetter >= aAsInt && firstLetter <= zAsInt) ? firstLetter - aAsInt : 0);
                    var spaces = new string(' ', offset);
                    sb.Append(spaces);
                }
                sb.Append(line).Append(Environment.NewLine); // invariant
            }

            return sb.ToString();
        }

        public override Granularity Granularity
        {
            get { return Granularity.All; }
            set { return; }
        }

        public override string ToString()
        {
            return "HeinjinianAidToMemory";
        }

        public override string Description
        {
            get { return "Offsets line based on first-character a..z."; }
        }
    }

    [DataContract]
    public class RandomCaps : TransformerBase
    {
        public override string Source { get; set; }

        public override string Munged
        {
            get { return Munge(Source); }
        }

        private string Munge(string source)
        {
            var c = source.ToLower().ToCharArray();
            for (var i = 0; i < c.Length; ++i)
            {
                if (TransformerTools.GetPercentage() > 50)
                {
                    c[i] = c[i].ToString().ToUpper().ToCharArray()[0];
                }
            }

            return new string(c);
        }

        public override Granularity Granularity
        {
            get { return Granularity.Word; }
            set { return; }
        }

        public override string ToString()
        {
            return Munge("RandomCaps");
        }

        public override string Description
        {
            get { return "Random capitalization (not guaranteed to be different from original)"; }
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

        public override string Description
        {
            get { return "Removes all consonants from Source, leaving onlh vowells behind."; }
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

        public override Granularity Granularity
        {
            get { return Granularity.Word; }
            set { return; }
        }

        public override string ToString()
        {
            return Munge("Disemvowell");
        }

        public override string Description
        {
            get { return "Removes all vowells from source, leaving only consonants behind. Inspired by Teresa Nielsen Hayden."; }
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

        public override Granularity Granularity
        {
            get { return Granularity.Sentence; }
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

        public override string Description
        {
            get { return "Converts all whitespace to a given punctuation mark (configurable)."; }
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

        public override Granularity Granularity
        {
            get { return Granularity.Word; }
            set { return; }
        }

        public override string ToString()
        {
            return Munge("VowellToPunct");
        }

        public override string Description
        {
            get { return "Converts all vowells to a given punctuation mark (configurable)."; }
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

        public override Granularity Granularity
        {
            get { return Granularity.Word; }
            set { return; }
        }

        public override string ToString()
        {
            return "reserveR";
        }

        public override string Description
        {
            get { return "Reverses the Source.ecruoS eht sesreveR"; }
        }
    }

    [DataContract]
    public class Shuffle : TransformerBase
    {
        // TODO: optionally preserve the first and last letters of each word

        public override string Source { get; set; }

        public override string Munged
        {
            get { return Munge(); }
        }

        public override Granularity Granularity
        {
            get { return Granularity.Word; }
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

        public override string Description
        {
            get { return "Randoms the letters in Source."; }
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

        public override string Description
        {
            get { return "Translates Source to PigLatin (anslatesTray ourceSay otay igLatinPay). Naive implementation does not preserve capitalization"; }
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

        public override string Description
        {
            get { return "Translates the source into 1337-5p34k (uses internal rule-set)"; }
        }
    }
}