using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Runner;
using TextTransformer;

namespace GUI
{
    public partial class MungerGui : Form
    {
        private List<RuleSet> _activeEditors = new List<RuleSet>();
        private string _source;
        private string _output;

        public MungerGui()
        {
            InitializeComponent();

            // TODO: these can start empty, or with defaults
            //       name will reflect the quantity within
            //       and empty-sets can be curated with the editor

            // these are all examples of how to add rules

            // TODO: we need to have an editor for some of these
            // or the factory will be giving us defaults I don't like.
            // or, make what I like the defaults... yeah, that makes sense....

            var globals = new RuleSet("Global Rules", Granularity.All);

            var markov = new MarkovGenerator { MinLength = 8000, MaxLength = 10000 };
            globals.AddRule(markov).AddRule(new Density { Percentage = 97 }).AddRule(new XrmlFormat());

            // oop, I need a RuleSet populated
            var granular = new RuleSet("Granular Rules", Granularity.Word) { Rules = new TransformationFactory().GetTransformers(Granularity.Word) };
            //var granular = new TransformationFactory().GetTransformers(Granularity.Word);
            //var granular = new RuleSet("Granular Rules", Granularity.Word)
            //                   {
            //                       Rules = new List<ITransformer>
            //                                   {
            //                                       new Leet(),
            //                                       new PigLatin(),
            //                                       new Shuffle(),
            //                                       new Disemconsonant(),
            //                                       new RandomCaps(),
            //                                       new VowellToPunct(),
            //                                       new Reverse(),
            //                                       new Shouty(),
            //                                       new VowellToPunct(),
            //                                       new Homophonic(),
            //                                       new Disemvowell(),
            //                                   }
            //                   };

            //granular.AddRule(new Leet()).AddRule(new PigLatin());

            //var grs = new List<ITransformer> {new Leet(), new PigLatin(), new Shuffle(),
            //new Disemconsonant(),new RandomCaps(),new VowellToPunct(),
            //new Reverse(),new Shouty(),new VowellToPunct(),
            //new Homophonic(),new Disemvowell(),};

            var rules = new List<object> { globals, granular };

            // TODO: better name. It's not just an adder
            RuleSetAdder.AvailableItems = rules;

            RuleSetAdder.AddDoubleClickHandler(DisplayRuleSetEditor);

            // TODO: need to build editors for the RuleSets
            //       which would be another CustomSelectControl
            //       and then editors for the varios transformer rules
            //       which will be interesting. Wonder if it can be abstracted.....
            var cs = new Runner.ConsoleRunner();

            _source = new ConsoleRunner().GetSource();
        }

        public void DisplayRuleSetEditor(object sender, EventArgs e)
        {
            // DAMMIT! we still have implementation details exposed. WTF!!!!
            // TODO: fix this
            var rs = (RuleSet)((ListBox)sender).SelectedItem;
            if (!_activeEditors.Contains(rs))
            {
                _activeEditors.Add(rs);
                var ed = new RuleSetEditor(rs, RuleSetAdder, _activeEditors);
                ed.Show();
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            _output = _source;
            // TODO: apply rules
            // apply globals one by one
            // go through words, apply granulars, based on percentage
            // this will be tweaked, so that each rule has it's own percentage.
            // or something.....

            // global application
            // retrieve rule
            // set rule.source = _source
            // Munger
            // TODO:dump into a modified car, not _source
            // ALSo: string builder each time? eh. who knows.

            // returns List<Object> -- how to cast to List<RuleSet>
            // RuleSet.Rules = Editor.SelectedItems.Cast<ITransformer>().ToList();
            var sets = RuleSetAdder.SelectedItems.Cast<RuleSet>().ToList();
            // uh, not quite. we need to distinguish between the granularity of the sets.

            // TODO: Markov is a pre-applied global, yeah?
            // but xray format should be a POST-applied global
            // so, we can't make these assumptions
            // ah, whatever....

            if (sets.Count > 0)
            {
                foreach (var ruleset in sets)
                {
                    if (ruleset.Granularity == Granularity.All)
                    {
                        ApplyGlobals(ruleset.Rules);
                    }
                    else
                    {
                        ApplyGranularRules(ruleset.Rules);
                    }
                }
            }

            txtOutput.Text = _output;
        }

        private void ApplyGlobals(List<ITransformer> globalRules)
        {
            foreach (var rule in globalRules)
            {
                rule.Source = _output;
                _output = rule.Munged;
            }
        }

        private void ApplyGranularRules(List<ITransformer> granulars)
        {
            const int threshold = 100; // 20% chance of applying the rule
            var sb = new StringBuilder();

            var rnd = new Random();

            var regex = new Regex(@"\s+"); // original regex
            var words = regex.Split(_output).ToList();

            foreach (var word in words)
            {
                string outword;

                if (granulars.Any() && rnd.Next(0, 100) < threshold)
                {
                    var rule = granulars[(rnd.Next(0, granulars.Count))];
                    rule.Source = word;
                    outword = rule.Munged;
                }
                else
                {
                    outword = word;
                }
                // TODO: spaces don't work so well, here.....
                sb.Append(outword + " ");
            }

            _output = sb.ToString();
        }
    }
}