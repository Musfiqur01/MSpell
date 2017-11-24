Install the nuget package to get started.

The usage is:

var spellChecker = new MSpell.SpellChecker();

var dictionary = new List<string> {"moon","soon","mono","windows"};

spellChecker.Train(dictionary);

// To get all word in the dictionry within 1 edit distance
var suggestion = spellChecker.GetSuggestedWords("mon", 1);

// The output is sorted by editdistance, prefixmatch, wordlength in order
The output should be mono,moon.

Download any dictionary use SpellChecker Factory to make the libarary support multiple languages.
var factory = new SpellCheckerFactory();
factory.Add("en-us", spellChecker);