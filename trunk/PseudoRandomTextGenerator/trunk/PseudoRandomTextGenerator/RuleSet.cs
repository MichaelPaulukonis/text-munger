using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextMunger
{
    public class RuleSet : ICloneable
    {
        private string _name;

        public RuleSet(string name)
        {
            _name = name;
            Rules = new List<ITransformer>();
        }

        public override string ToString()
        {
            return string.Format("{0} : {1}", _name, Rules.Count);
        }

        public object Clone()
        {
            var rs = new RuleSet(_name);

            // TODO: uh-oh, this is not a deep copy!
            // we have to make a COPY of each rule
            // so that they can be edited independently.....
            // RABBIT HOLES!!!!!
            rs.Rules = Rules;

            return rs;
        }

        public List<ITransformer> Rules { get; set; }

        public RuleSet AddRule(ITransformer rule)
        {
            Rules.Add(rule);
            return this;
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
            return this;
        }
    }
}