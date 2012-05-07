using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TextTransformer
{
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
                // TODO: implements

                var tokens = new List<string>();

                switch (Granularity)
                {
                    case Granularity.Character:
                        tokens = new Regex(@".").Split(Source).ToList();
                        break;
                    case Granularity.Word:
                        tokens = new Regex(@"\s+").Split(Source).ToList();
                        break;
                    case Granularity.Sentence:
                        break;
                    case Granularity.Paragraph:
                        tokens = Regex.Split(Source, "\r\n\r\n|\r\r|\n\n").ToList();
                        break;
                    case Granularity.Line:
                        tokens = Regex.Split(Source, "\r\n|\r|\n").ToList();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return tokens;
            }
        }
    }
}