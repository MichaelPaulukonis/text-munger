using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TextTransformer
{
    // TODO: one flaw - no room for the XRML word-tokenizer (working or non-working)
    // it's not defined solely by the granularity.
    // hrm.....
    public class TextTokenizer
    {
        // parameterless-contstructor defaults to word-level
        public TextTokenizer()
            : this(Granularity.Word)
        {
        }

        public TextTokenizer(Granularity granularity)
        {
            this.Granularity = granularity;
        }

        public TextTokenizer(Granularity granularity, string source)
            : this(granularity)
        {
            this.Source = source;
        }

        public string Source { get; set; }

        public Granularity Granularity { get; set; }

        public IList<string> Tokens
        {
            get
            {
                var tokens = new List<string>();

                switch (Granularity)
                {
                    case Granularity.Character:
                        tokens = Source.ToCharArray().Select(c => c.ToString()).ToList();
                        break;
                    case Granularity.Word:
                        tokens = new Regex(@"\s+").Split(Source).ToList();
                        break;
                    case Granularity.Sentence:
                        break;
                    case Granularity.Paragraph:
                        // TODO: better way to write this
                        tokens = Regex.Split(Source, "\r\n\r\n|\r\r|\n\n").ToList();
                        break;
                    case Granularity.Line:
                        tokens = Regex.Split(Source, "\r\n|\r|\n").ToList();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(string.Format("{0} is not a supported Granularity for tokenization.", Granularity));
                }

                return tokens;
            }
        }
    }
}