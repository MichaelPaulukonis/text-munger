using System.Collections.Generic;

namespace TextTransformer
{
    public class TransformationFactory
    {
        //public enum Granularity
        //{
        //    Unknown = -1,
        //    Any = 0,
        //    Character = 1,
        //    Word = 2,
        //    Sentence = 3,   // yeah, maybe
        //    Paragraph = 4,  // yeah. okay. so what?
        //    Page = 5,       // no idea how this would be defined
        //    All = 100       // leaving room for weird expansion. although refactoring should take care of that. except for serialization...
        //}

        // TODO: what do I want?
        // a factory that returns all possible transformers of a given granularity (and smaller)
        // ie, word-transforer is all word, character, and any
        // All == all, I guess
        // do we have any any

        public TransformationFactory(Granularity maxGranularity)
        {
            Granularity = maxGranularity;
        }

        public Granularity Granularity { get; private set; }

        // we will return ALL transformers that are up to and including this level
        public List<ITransformer> GetTransformers()
        {
            var ts = new List<ITransformer>();

            // cascade through, so the highest granularity (all)
            // can list everything
            // although not all lower-level granularities work well on a higher-level
            // like PigLatin. That just fails at anything other than word
            // so maybe we need to introduce a Granularity RANGE
            // Granularity.min and Granularity.max ???
            switch (Granularity)
            {
                case Granularity.All:
                    ts.AddRange(GetGranularityAll());
                    goto case Granularity.Sentence;

                case Granularity.Sentence:
                    ts.AddRange(GetGranularitySentence());
                    goto case Granularity.Word;

                case Granularity.Word:
                    ts.AddRange(GetGranularityWord());
                    break;
            }

            return ts;
        }

        private List<ITransformer> GetGranularitySentence()
        {
            return new List<ITransformer> { new PunctuizeWhitespace()
                //, new Density()
            };
        }

        // TODO: some of these are sentence based -- pull them out
        private List<ITransformer> GetGranularityWord()
        {
            return new List<ITransformer>
            {new Leet(),
                new PigLatin(),
                new Shuffle(),
                new Disemconsonant(),
                new Disemvowell(),
                new RandomCaps(),
                new Reverse(),
                new Shouty(),
                new VowellToPunct(),
                //new Homophonic()
                new TransformerFromFile(@"D:\Dropbox\projects\TextMunger\homophone_list.txt")
            };
        }

        private List<ITransformer> GetGranularityAll()
        {
            return new List<ITransformer> { new MarkovGenerator(), new XrmlFormat() };
        }
    }
}