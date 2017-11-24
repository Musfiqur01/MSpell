Install the nuget package to get started.

The usage is:

var spellChecker = new MSpell.SpellChecker();

var dictionary = new List<string> {"moon","soon","mono","windows"};

spellChecker.Train(dictionary);

Inorder to get all word within 1 edit distance:            
var suggestion = spellChecker.GetSuggestedWords("mon", 1);

The output should be mono, moon. The output is sorted by editdistance, then by largest prefix match, then by length of the word.

Use any dictionary you like in any language.