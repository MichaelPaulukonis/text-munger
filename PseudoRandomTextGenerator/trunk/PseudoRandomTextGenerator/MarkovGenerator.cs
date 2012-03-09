using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextTransformer
{
    public interface IMarkovRules
    {
        IList<string> Split(string subject);

        string Delimiter { get; }

        string Clean(string dirty);
    }

    public class DefaultRule : IMarkovRules
    {
        public virtual IList<string> Split(string subject)
        {
            var regex = new Regex(@"\s+"); // original regex

            subject = this.Clean(subject);

            var splitted = regex.Split(subject).ToList();

            return splitted;
        }

        public virtual string Delimiter { get { return " "; } }

        public string Clean(string dirty)
        {
            var clean = dirty;

            clean = clean.Replace("\r", "");
            clean = clean.Replace("\n", "");

            // TODO: replace HTML character entities

            return clean;
        }
    }

    public class XrayWordRule : DefaultRule
    {
        public override string Delimiter { get { return string.Empty; } }

        public override IList<string> Split(string subject)
        {
            // this is NOT tokenizing punctuation the way I would have expected
            // blocks of "....." ".." are kept contiguous, instead of being split. ugh.
            var regex = new Regex(@"(\W+)"); // tokenize punctuation

            //var regex = new Regex(@"\W+"); // remove all punctuation

            //var regex = new Regex(@"."); // every char for itself

            // http://blog.figmentengine.com/2008/10/markov-chain-code.html
            // suggests using
            // One of the interesting side-effects of tokenizing using a simple regex
            // is that if the input stream is HTML the Markov chain will treat HTML
            // as words as well and therefore not only generate text that looks like
            // the input, but is also formatted like the input - this also works for punctuation.

            // actually, this has somewhat awful results, at least as far as spacing is concerned
            // perhaps the original source has better output handling
            //var regex = new Regex(@"(\W+)");

            // okay, now I see.
            /* the \W+ method makes punctuation, spaces, etc as individual tokens
             * the use of the keylength allows the engine to select the likelihood of tokens to be followed by other tokens
             * which in THIS case would be words, whitespace, or punctuation.
             *
             * the \s+ method built-in to PseudoRandomTextGenerator has a lot of space-insertion going on
             * which is basically required to be NOT present in the \W+ method
             *
             * Not sure how to synthesize a switch between the two at the moment...
             *
             */

            // we ALSO need to split the punctuation. aaaargh.

            subject = this.Clean(subject);

            // instead of regex.split, let's go through it char-by-char

            var splitted = new List<string>();
            string token = string.Empty;
            var inWord = false;
            foreach (char c in subject)
            {
                // if we're processing letters, and we find a letter, append to word
                inWord = (char.IsLetter(c));
                if (inWord)
                {
                    token += c;
                }
                else
                {
                    if (token != string.Empty)
                    {
                        splitted.Add(token);
                        token = string.Empty;
                    }
                    splitted.Add(c.ToString());
                }
            }

            return splitted;
        }
    }

    public class XrayCharRule : XrayWordRule
    {
        public override IList<string> Split(string subject)
        {
            var regex = new Regex(@"."); // every char for itself

            subject = this.Clean(subject);

            var splitted = subject.ToCharArray().Select(c => c.ToString()).ToList();

            return splitted;
        }
    }

    public class Key
    {
        public string Word { get; set; }

        public string Current { get; set; }

        public string Previous { get; set; }
    }

    public class MarkovGenerator : ITransformer, ICloneable
    {
        private readonly int _keySize;
        private Dictionary<string, List<string>> _chain;
        private Random _random;
        private readonly IMarkovRules _rule;
        private const int FirstWord = 1;
        private string _wordDelim = Convert.ToChar(6).ToString(); // this is for the storage model only, not for output.

        public MarkovGenerator(int keySize = 2)
        {
            if (keySize < 1 || keySize > 5) throw new ArgumentOutOfRangeException("keySize", "Can be 1-5");

            _keySize = keySize;
            _chain = new Dictionary<string, List<string>>();
            _random = new Random();

            //_rule = new DefaultRule();
            _rule = new XrayWordRule();
            //_rule = new XrayCharRule();

            MinLength = 100;
            MaxLength = 100;
        }

        public MarkovGenerator(IMarkovRules rules, int keySize = 2)
            : this(keySize)
        {
            _rule = rules;
        }

        // TODO: should there be more params exposed?
        public object Clone()
        {
            var c = new MarkovGenerator(_rule, _keySize);

            return c;
        }

        public void ReadTextFile(string filePath)
        {
            ReadStreamAndInitialize(new StreamReader(filePath).BaseStream);
        }

        public void ReadStream(Stream stream)
        {
            ReadStreamAndInitialize(stream);
        }

        public void ReadText(string thisIsMySeedData)
        {
            this.Source = thisIsMySeedData;
        }

        private void ReadStreamAndInitialize(Stream stream)
        {
            TextReader reader = new StreamReader(stream);
            var text = reader.ReadToEnd();
            reader.Close();
            reader.Dispose();

            this.Source = text;
        }

        private void InitializeChain()
        {
            var tokens = _rule.Split(Source);

            if (tokens.Count < _keySize) throw new ArgumentException("The text is smaller than the key size");

            AddTokensToChain(tokens);
        }

        private void AddTokensToChain(IList<string> tokens)
        {
            // TODO: ooooh, Trim() -- that removes any change of significant whitespace!!!!!
            for (var i = _keySize; i < tokens.Count; i++)
            {
                var nonModifiedClosurei = i;
                //var keyTokens = Enumerable.Range(0, _keySize).Select(k => tokens[nonModifiedClosurei - (_keySize - k)].Trim());

                //AddToken(keyTokens, tokens[nonModifiedClosurei].Trim());

                var keyTokens = Enumerable.Range(0, _keySize).Select(k => tokens[nonModifiedClosurei - (_keySize - k)]);

                AddToken(keyTokens, tokens[nonModifiedClosurei]);
            }
        }

        private void AddToken(IEnumerable<string> keyTokens, string value)
        {
            // do we need to remove the space for the alternate method?
            var chainKey = string.Join(_wordDelim, keyTokens); // original
            //var chainKey = string.Join("", keyTokens); // tokenized punctuation and whitespace

            if (_chain.ContainsKey(chainKey))
            {
                if (!_chain[chainKey].Contains(value)) _chain[chainKey].Add(value);
            }
            else _chain.Add(chainKey, new List<string>() { value });
        }

        public int MinLength { get; set; }

        public int MaxLength { get; set; }

        private string _source;

        public string Source
        {
            get { return _source; }
            set
            {
                _source = value;
                InitializeChain();
            }
        }

        private string _m = null;

        // doh! this means it never re-processes what we've got!
        // ugh. not what I want...
        public string Munged
        {
            //get { return _m ?? (_m = Munge()); }
            get { return Munge(); }
        }

        public string Munge()
        {
            return this.Write(MinLength, MaxLength);
        }

        // running Markov on a word-level is a bit silly, but for long words I guess you could do it
        // should we just bite-the-bullet and insist on sentence-level?
        // what about the hundred-letter thunderwords? or a punctuation-and-whitespace-free blob?
        public Granularity Granularity { get { return Granularity.All; } }

        // TODO: overload, to accept a designated seed-string
        // if seed-string does not exist, also use random
        // also, length, and some other factors to come from the _rule
        public string Write(int minLength, int maxLength)
        {
            // once we get a string longer than this we are done
            // NOTE: minLength is a hard barrier, while maxLength is soft.
            var length = _random.Next(minLength, maxLength);

            // get the first key AT RANDOM
            var key = new Key { Current = GetRandomKey(), Previous = string.Empty };

            var keyRepCount = 0;
            const int keyRepLimit = 5;

            var sentenceLength = 0;
            // keep the words in a list, so we aren't doing more string manipulation than necessary
            var words = new List<string>();
            var ruleDelimLength = _rule.Delimiter.Length; // cache this.

            // while we haven't reached the length
            // get the next word
            // add it to the list
            // increment the sentence length, add one for the rule-delim we will add in the join
            // update the current key by getting the tail and appending the nextWord
            while (sentenceLength < length)
            {
                key = GetWord(key);
                words.Add(key.Word);
                sentenceLength += (key.Word.Length + ruleDelimLength);
                // Q: do we need to remove the space for the alternate method?
                // A: not now, all methods use the space as a delimiter IN THE STORAGE MODEL
                //    not in the output. this allows us to split the key-length "words" into components

                key.Previous = key.Current;
                // skip the first word of the existing key, add a space and the next word
                // update the current key by getting the tail and appending the nextWord
                key.Current = string.Join(_wordDelim, key.Current.Split(Convert.ToChar(_wordDelim)).Skip(FirstWord)) + _wordDelim + key.Word;

                // keep from "infinite" repetition where newKey will only point to itself
                if (key.Previous == key.Current)
                {
                    keyRepCount++;
                    if (keyRepCount >= keyRepLimit)
                    {
                        key.Current = GetRandomKey();
                    }
                }
            }

            return string.Join(_rule.Delimiter, words);
        }

        // since it's an object, we could just make it by Ref and stop worrying about return values...
        private Key GetWord(Key k)
        {
            if (_chain.ContainsKey(k.Current))
            {
                // get a random (ie, unweighted) entry
                //var index = _random.Next(_chain[key].Count - 1); // original, seems to always return 0 for length 2
                var index = _random.Next(0, _chain[k.Current].Count);
                k.Word = _chain[k.Current][index];
            }
            else
            { // since key does not exist, get any existing word at random
                // ie, unweighted, no basis on existing key. hrm.

                // any OTHER need for the old method?
                k.Word = GetWord(GetRandomKey());
            }

            return k;
        }

        private string GetWord(string key)
        {
            string word;

            if (_chain.ContainsKey(key))
            {
                // get a random (ie, unweighted) entry
                //var index = _random.Next(_chain[key].Count - 1); // original, seems to always return 0 for length 2
                var index = _random.Next(0, _chain[key].Count);
                word = _chain[key][index];
            }
            else
            { // since key does not exist, get any existing word at random
                // ie, unweighted, no basis on existing key. hrm.

                // aaaaand, this is also crap, because now we have a key that never existed, and we're going to keep randomizing.
                // ugh. we're in bad letter territory, now
                // WTF?
                // TODO: curr, prev and word shound be members of some dataStructure
                word = GetWord(GetRandomKey());
            }

            return word;
        }

        private string GetRandomKey()
        {
            var keyIndex = _random.Next(_chain.Count);
            return _chain.Keys.ToArray()[keyIndex];
        }
    }
}