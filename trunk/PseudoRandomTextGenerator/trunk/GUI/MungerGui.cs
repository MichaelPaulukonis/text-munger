using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TextMunger;

namespace GUI
{
    public partial class MungerGui : Form
    {
        private List<RuleSet> _activeEditors = new List<RuleSet>();

        public MungerGui()
        {
            InitializeComponent();

            // TODO: these can start empty, or with defaults
            //       name will reflect the quantity within
            //       and empty-sets can be curated with the editor

            // these are all examples of how to add rules

            var globals = new RuleSet("Global Rules");
            globals.AddRule(new Density { Percentage = 97 }).AddRule(new XrmlFormat());

            var granular = new RuleSet("Granular Rules")
                               {
                                   Rules = new List<ITransformer>
                                               {
                                                   new Leet(),
                                                   new PigLatin(),
                                                   new Shuffle(),
                                                   new Disemconsonant(),
                                                   new RandomCaps(),
                                                   new VowellToPunct(),
                                                   new Reverse(),
                                                   new Shouty(),
                                                   new VowellToPunct(),
                                                   new Homophonic(),
                                                   new Disemvowell(),
                                               }
                               };

            //granular.AddRule(new Leet()).AddRule(new PigLatin());

            //var grs = new List<ITransformer> {new Leet(), new PigLatin(), new Shuffle(),
            //new Disemconsonant(),new RandomCaps(),new VowellToPunct(),
            //new Reverse(),new Shouty(),new VowellToPunct(),
            //new Homophonic(),new Disemvowell(),};

            var rules = new List<object> { globals, granular };

            RuleSetAdder.AvailableItems = rules;

            RuleSetAdder.AddDoubleClickHandler(DisplayRuleSetEditor);

            // TODO: need to build editors for the RuleSets
            //       which would be another CustomSelectControl
            //       and then editors for the varios transformer rules
            //       which will be interesting. Wonder if it can be abstracted.....
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
    }
}