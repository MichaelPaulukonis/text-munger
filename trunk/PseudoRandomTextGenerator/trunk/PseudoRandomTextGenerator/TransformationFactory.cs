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

        public TransformationFactory()
        {
        }

        // we will return ALL transformers that are up to and including this level
        public List<ITransformer> GetTransformers(Granularity maxGranularity)
        {
            var ts = new List<ITransformer>();

            // TODO: other cases
            // and.. fall-through? things should be added up.....
            // and actually, "ALL" should include... everything, right?!??!
            switch (maxGranularity)
            {
                case Granularity.All:
                    ts.AddRange(GetGranularityAll());
                    goto case Granularity.Sentence;

                case Granularity.Sentence:
                    // TODO: implement
                    goto case Granularity.Word;

                case Granularity.Word:
                    ts.AddRange(GetGranularityWord());
                    break;
            }

            return ts;
        }

        // TODO: some of these are sentence based -- pull them out
        private List<ITransformer> GetGranularityWord()
        {
            return new List<ITransformer>
            {new Leet(),
                new PigLatin(),
                new Shuffle(),
                new Disemconsonant(),
                new RandomCaps(),
                new VowellToPunct(),
                new Reverse(),
                new Shouty(),
                new VowellToPunct(),
                new Homophonic(),
                new Disemvowell()
            };
        }

        private List<ITransformer> GetGranularityAll()
        {
            return new List<ITransformer> { new MarkovGenerator(), new XrmlFormat(), new Density() };
        }
    }
}