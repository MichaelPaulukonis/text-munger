namespace TextTransformer
{
    internal class TransformationFactory
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
    }
}