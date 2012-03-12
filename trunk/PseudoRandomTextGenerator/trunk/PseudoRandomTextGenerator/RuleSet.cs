using System;
using System.Collections.Generic;
using System.Linq;

namespace TextTransformer
{
    [Serializable]
    public class GranularityViolationException : Exception
    {
        public GranularityViolationException(string message)
            : base(message)
        { }
    }

    public class RuleSet : ICloneable
    {
        private string _name;

        public RuleSet(string name)
        {
            ForcedGranularity = false;
            _name = name;
            Rules = new List<ITransformer>();
        }

        public RuleSet(string name, Granularity granularity)
            : this(name)
        {
            _g = granularity;
            ForcedGranularity = true; // ahhhh, not sure where to go with this
            // the idea being, if we create a "Globals" ruleset, or, "ALL", don't reset it to "Unknown"
            // as we want to process on the "ALL" level, no matter what.....
        }

        public override string ToString()
        {
            return string.Format("{0} : {1}", _name, Rules.Count);
        }

        public bool ForcedGranularity { get; private set; }

        // if Granularity was supplied on construction, do not allow a reset
        private Granularity _g = Granularity.Unknown;

        public Granularity Granularity
        {
            get { return _g; }
            set
            {
                if (!ForcedGranularity)
                {
                    _g = value;
                }
            }
        }

        public object Clone()
        {
            var rs = new RuleSet(_name);

            // TODO: uh-oh, this is not a deep copy!
            // we have to make a COPY of each rule
            // so that they can be edited independently.....
            // RABBIT HOLES!!!!!
            rs.Rules = Rules;
            rs.Granularity = Granularity;
            rs.ForcedGranularity = ForcedGranularity;
            return rs;
        }

        private List<ITransformer> _rules = new List<ITransformer>();

        public List<ITransformer> Rules
        {
            get { return _rules; }
            set
            {
                var g = Granularity.Unknown; // if empty ruleset, reset granularity

                if (value.Count > 0)
                {
                    // rules must have <= granularity, right?
                    // can't apply a global on a word-level [ie, Markov on a word can't happen]
                    // but can apply word-level on a global basis [SHOUTY for ALL is possible]
                    // my granularity assessments and rules are incorrect
                    // homophonic works on word and above
                    // Markov works on sentence and above
                    // so, RuleSet granularity should be of GREATEST granularity
                    // and not draw exceptions for what we add, yeah?
                    // with the knowledge that we will process on the greatest level
                    // BUT WE CAN FORCE A LEVEL PROCESS
                    // by manually setting the granularity....

                    // capture greatest level of granularity

                    foreach (var rule in value.Where(rule => rule.Granularity > g))
                    {
                        g = rule.Granularity;
                    }
                }

                Granularity = g;
                _rules = value;
            }
        }

        public RuleSet AddRule(ITransformer rule)
        {
            if (Granularity == Granularity.Unknown)
            {
                Granularity = rule.Granularity;
            }
            else
            {
                if (rule.Granularity > Granularity)
                {
                    Granularity = rule.Granularity;
                    // TODO: better exception reporting
                    //throw new GranularityViolationException(string.Format("Rule {0} cannot be added to RuleSet", rule.ToString()));
                }
            }
            Rules.Add(rule);
            return this;
        }

        private Granularity CalculateGranularity()
        {
            var g = Granularity.Unknown;
            foreach (var transformer in Rules.Where(transformer => transformer.Granularity > g))
            {
                g = transformer.Granularity;
            }
            return g;
        }

        public RuleSet DeleteRule(ITransformer rule)
        {
            for (var i = 0; i < Rules.Count; ++i)
            {
                if (Rules[i] == rule)
                {
                    Rules.Remove(rule);

                    break;
                }
            }

            // reset granularity to max granularity
            Granularity = CalculateGranularity();

            return this;
        }
    }
}