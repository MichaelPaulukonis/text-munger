﻿using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace TextTransformer
{
    [DataContract]
    public class XrmlFormat : TransformerBase
    {
        public XrmlFormat()
        {
            RetainAlignment = true;
            this.Density = new Density();
            LineLengthLimit = 80;
        }

        [DataMember]
        public Density Density { get; set; }

        [DataMember]
        public int LineLengthLimit { get; set; }

        /// <summary>
        /// Retain original spacing, or fill in with Density
        /// </summary>
        [DataMember]
        public Boolean RetainAlignment { get; set; }

        [DataMember]
        public Boolean FlushRight { get; set; }

        [DataMember]
        public Boolean FlushLeft { get; set; }

        public override string Source { get; set; }

        public override string Munged
        {
            get { return Munge(); }
        }

        private string Munge()
        {
            var mod = Source;

            // TODO: we need to analyze the incoming, better
            // if multiple lines, we need to retain that,
            //    BUT enforce the line-length
            // if "single" line, we need to split it

            // this is a pre-line-length operation
            if (!RetainAlignment)
            {
                // process density first, as that removes line-breaks
                Density.Source = mod;
                mod = Density.Munged;
            }

            var sb = new StringBuilder();
            var lines = new TextTokenizer(Granularity.Line, mod).Tokens;
            // TODO: loop through lines

            foreach (var line in lines)
            {
                // if too long, cut it off
                // if too short, pad it out
                // also, flush-left, flush-right it

                var newline = string.Empty;

                if (line.Length > LineLengthLimit)
                {
                    // wait. if eveything comes in AS ONE LINE
                    // this is f****D
                    // we need more analysis, here....
                    newline = line.Substring(0, LineLengthLimit);
                }
                else
                {
                    newline = line.PadRight(LineLengthLimit);
                }

                if (FlushLeft)
                {
                    newline = newline.PadLeft(LineLengthLimit);
                }
                else if (FlushRight)
                {
                    newline = newline.Trim().PadRight(LineLengthLimit);
                }

                if (RetainAlignment)
                {
                    newline = newline.Replace(" ", ".");
                }
                sb.AppendLine(newline);
            }

            //mod = sb.ToString();

            // sadly, does NOT pad with spaces at the end. waaaaah!
            // "(.{1,80})"
            //var regex = string.Format("(.{{1,{0}}})", LineLengthLimit);
            //mod = Regex.Replace(mod, regex, "$1\n");
            //mod = PadItOut(mod);

            // this is a post-line-length operation
            //if (RetainAlignment)
            //{
            //    // TODO: what if we get tabs, etc?
            //    mod = mod.Replace(" ", ".");
            //}

            return sb.ToString();
        }

        private string PadItOut(string source)
        {
            var lines = new TextTokenizer(Granularity.Line, source).Tokens;
            var sb = new StringBuilder();

            foreach (var line in lines)
            {
                var append = string.Empty;
                var l = line.Length;
                if (l < LineLengthLimit)
                {
                    append = new String(' ', LineLengthLimit - l);
                }
                // TODO: aargh, string concatenation inside of string-buildering!
                sb.AppendLine(line + append);
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
            return "XRML format";
        }

        // TODO: make a better description, sheesh!
        public override string Description
        {
            get { return "Formats Source to column-width and punctuated as per XraysMonaLisa."; }
        }
    }

    [DataContract]
    public class Density : TransformerBase
    {
        private static Random _rnd = new Random();

        public Density() : this(percentage: 97) { }

        public Density(int percentage)
        {
            Percentage = percentage;
            this.RandomWalker = new RandomWalker(30, 10, 10);
        }

        [DataMember]
        public RandomWalker RandomWalker { get; set; }

        public override string Source { get; set; }

        private int _p = 97; // default

        [DataMember]
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

        public override string Munged
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
            var words = new TextTokenizer(Granularity.Word, Source).Tokens;

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

        public override Granularity Granularity
        {
            get { return Granularity.Sentence; }
            set { return; }
        }

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

        [DataMember]
        public double Boundary
        {
            get { return _boundary; }
            set { _boundary = value; }
        }

        // TODO: I suppose randomization should be able to be turned on/off....
        // http://stackoverflow.com/questions/706952/smooth-movement-to-ascend-through-the-atmosphere/707035#707035
        public int GetPunctAmount(int density)
        {
            Double RangeMin = 0;
            Double RangeMax = 1840;

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
                var curPuncts = (correctedValue * (RangeMax - RangeMin) + RangeMin);

                var flatPuncts = (int)Math.Round(curPuncts);

                var offset = RandomOffset(flatPuncts);
                // uh.... WAAAAY off, and goes negative, and pretty much stays there... so, needs tweaking
                //var offset = RandomWalker.Next();

                flatPuncts += offset;

                if (flatPuncts > RangeMax) flatPuncts = (int)RangeMax;
                if (flatPuncts < RangeMin) flatPuncts = (int)RangeMin;

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
            // not sure what this line is figuring out
            // ugh.
            var normal = Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2);
            var offset = ((normal * deviation) + mean) / 2;

            if (offset > density) offset = density;
            if (offset < -density) offset = -density;

            return (int)Math.Round(offset);
        }

        public override string ToString()
        {
            return String.Format("Density {0}%", Percentage);
        }

        public override string Description
        {
            get { return "Punctuizes the Source semi-randomly around the given density."; }
        }
    }

    [DataContract]
    public class RandomWalker
    {
        private static Random _rnd = new Random();
        private int _yaw;
        private int _tenacity;

        public RandomWalker(int yaw, int warble, int tenacity)
        {
            //RangeMin = rangeMin;
            //RangeMax = rangeMax;
            Yaw = yaw;
            Warble = warble;
            Tenacity = tenacity;
            _tenacity = tenacity;
            _yaw = yaw;
            //Density = density;
        }

        // major-deviation walk
        [DataMember]
        public int Yaw
        {
            get;
            set;
        }

        // minor deviation walk around Yaw-point
        [DataMember]
        public int Warble
        {
            get;
            set;
        }

        // tendancy to warble around Yaw point
        [DataMember]
        public int Tenacity { get; set; }

        public int Next()
        {
            _tenacity--;
            if (_tenacity <= 0) // reset yaw and tenacity
            {
                _tenacity = _rnd.Next(Tenacity);
                _yaw += _rnd.Next(-Yaw, Yaw);
            }

            // new warble each retrieval
            var warble = _rnd.Next(-Warble, Warble);
            return _yaw + warble;
        }
    }
}