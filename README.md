# text-munger
Automatically exported from code.google.com/p/text-munger

Text Munger chews up sources via a variety of configurable methods.

It is a work-in-progress. See Roadmap for more details.

See http://www.xradiograph.com/WordSalad.TextMunger for my dis-organized, ranting notes.... 

This will probably be converted to JavaScript/node.js

# Introduction

There is no defined goal for TextMunger, beyond being useful.

For certain limited definitions of the word "useful."
Details
RECENTLY COMPLETED TASKS

    improved display in selection editors 2012.04.16
        Library, rule-selectors and editors. 
    FreeVerse transformation rule implementation 2012.04.16
        which combines standalone ShortLines and InitialSpaces transformations 
    HeijinianAidToMemory transformation implementation 2012.04.17
        indents lines according to alpha offset 
    save and reload text-sources (if in library or local files) 2012.03.28
    basic editing control for SOURCE and OUTPUT 2012.03.27
        This was pretty-much built-on, only now it's accessible via context-menu 
    added SNIPPETS window 2012.03.27
        and copy-to-snippets from OUTPUT via context-menu 
    put density inside of XRML format 2012.03.18
    load SOURCE from file 2012.03.19
    save OUTPUT to file 2012.03.19 

TASKS (in no particular order)

    save and reload configured rule-sets
        partially working as of 2012.04.12, but needs improvement, including 
    named rule-sets
    auto-loading of saved-rulesets (in predefined location)
    some sort of short-cut key to edit a given ruleset(s)
        there's a lot of clicking in this interface. 
    build unit-tests
    added percentage bias to text sources
        although this can be crudely-done via adding the text more than once 
    swap OUTPUT to SOURCE, for re-processing
    allow controls to resize when form is resized
    separation of operation from GUI, so can be scripted
    percentage-bias for word-level Transforms
        i.e., don't apply to EVERY word 
    installation project, so installer can exist in project alongside source
    better movement-controls inside of edit-areas
        i.e., word-jump, etc 
    (better) error-handling. Right now, TM expects loaded data to be valid, etc. This task probably goes hand-in-hand with
    add log4net
    apply rule/ruleset to selected text in OUTPUT
    re-think of rule selection/layout -- too many clicks, currently.
    common tokenizer -- word, char, sentence, other. punctuation-sensitive or not.
    Attempt to delete Gutenberg boilerplate
    portamnteaux list, and other analysis of output (a la charNG) 

Transformation Rules Additions and Enhancements

    vowell-to-punct rule: randomize the punctuation -- a la expletive deleted
    BOWDLERIZER -- configurable regex rules, transforms words or phrases into something else
    more Transformations
        acrostic/mesostic generator
        e-cummings-ification 
    more clustering random-walk for Density transform
    file-based generic replacement "translator" to read from directory
        ie, multiple user-controlled translators are possible
        that will be an issue for serialization....
        wait, I think I did this.... 
    formatting category for rules (ie, XRML, shortlines, Heijinian, etc.)
    more formatters (Howlish paragraphs, right-justify position `n`?)
    custom rules have scripting of some kind
    Markov updates
        drop xray-references in Markov rules
        add explanation of rules to rule-editor (and whatever background code)
        change multiple-Markov rules to one rule, with options
        make Markov-analysis case-insensitive for analysis
            not for output (optional, I suppose)
            optional 

WAAAY Down the road, but a major goal:

    planar-output and processing, not just linear 


# Transformation Rules

There are three basic types of transformation rules available:

    Formatters
    Translators
    Generators 

Generators

    Markov (n-gram) Chains 

Translators

    Leet
    Pig Latin
    Shuffle
    Disemconsonant
    Disemvowell
    Random Caps
    Reverse
    Shouty Caps
    Vowell to Punctuation
    Homphonerizer
    Punctuize Whitespace 

Formatters

    Short Lines
    Initial Spaces
    Free Verse
    Heijinian Aid to Memory
    XraysMonaLisa?-style 




