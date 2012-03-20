using System;
using System.Text;
using System.Text.RegularExpressions;

namespace TextTransformer
{
    public class XrmlFormat : ITransformer
    {
        public XrmlFormat()
        {
            this.Density = new Density();
            LineLength = 80;
        }

        public Density Density { get; set; }

        public int LineLength { get; set; }

        public string Source { get; set; }

        public string Munged
        {
            get { return Munge(); }
        }

        private string Munge()
        {
            // process density first, as that removes line-breaks
            Density.Source = Source;
            var mod = Density.Munged;

            // "(.{1,81})"
            var regex = string.Format("(.{{1,{0}}})", LineLength + 1);
            mod = Regex.Replace(mod, regex, "$1\n");

            return mod;
        }

        public Granularity Granularity { get { return Granularity.All; } }

        public override string ToString()
        {
            return "XRML format";
        }
    }

    public class Density : ITransformer
    {
        private Random _rnd;
        private RandomWalker _walker;

        public Density() : this(percentage: 97) { }

        public Density(int percentage)
        {
            _rnd = new Random();
            Percentage = percentage;
            _walker = new RandomWalker(30, 10, 10);
        }

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

                // TODO: THIS is where RandomWalker goes....
                var offset = RandomOffset(flatPuncts);
                //var offset = _walker.Next();

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
            // not sure what this line is figureing out
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
    }

    internal class RandomWalker
    {
        private Random _rnd = new Random();

        public RandomWalker(int yaw, int warble, int tenacity)
        {
            //RangeMin = rangeMin;
            //RangeMax = rangeMax;
            Yaw = yaw;
            Warble = warble;
            Tenacity = tenacity;
            //Density = density;
        }

        public int RangeMin { get; set; }

        public int RangeMax { get; set; }

        // major-deviation walk
        public int Yaw { get; set; }

        // minor deviation walk around Yaw-point
        public int Warble { get; set; }

        // tendancy to warble around Yaw point
        public int Tenacity { get; set; }

        public int Density { get; set; }

        public int Next()
        {
            //xMajor += randomInt(-10, 10);
            //for (i = 0; i < randomInt(1, n); ++i)
            //{
            //    xMinor = randomInt(-3, 3);
            //    xChange = xMajor + xMinor;
            //}

            while (true)
            {
                var major = _rnd.Next(-Yaw, Yaw);
                var steps = _rnd.Next(0, Tenacity);
                for (int i = 0; i <= steps; ++i)
                {
                    var minor = _rnd.Next(-Warble, Warble);
                    return major + minor;
                }
            }
        }

        // TODO: don't need this....
        private int DensityHoverPoint(int density)
        {
            Int32 numberSteps = 101;

            // Positive values produce ascending functions.
            // Negative values produce descending functions.
            // Values with smaller magnitude produce more linear functions.
            // Values with larger magnitude produce more step like functions.
            // Zero causes an error.
            // Try for example +1.0, +6.0, +20.0 and -1.0, -6.0, -20.0
            //Double boundary = +6.0;
            var boundary = 6.0;

            {
                Double t = -boundary + 2.0 * boundary * density / (numberSteps - 1);
                Double correction = 1.0 / (1.0 + Math.Exp(Math.Abs(boundary)));
                Double value = 1.0 / (1.0 + Math.Exp(-t));
                Double correctedValue = (value - correction) / (1.0 - 2.0 * correction);

                // TODO: remove the puncts refs
                var curPuncts = (correctedValue * (RangeMax - RangeMin) + RangeMin);

                var flatPuncts = (int)Math.Round(curPuncts);

                // for whatever reason, it's not always within the range
                if (flatPuncts > RangeMax) flatPuncts = (int)RangeMax;
                if (flatPuncts < RangeMin) flatPuncts = (int)RangeMin;

                return flatPuncts;
            }
        }
    }
}