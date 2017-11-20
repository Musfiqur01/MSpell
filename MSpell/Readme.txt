Install the nuget package to get started.

The usage is:

var spellChecker = new MSpell.SpellChecker();

var dictionary = new List<string> {"moon","soon","mono","windows"};

spellChecker.Train(dictionary);
            
var suggestion = spellChecker.GetSuggestedWords("mon", 1);

The output should be mono,moon.


Use any dictionary you like in any language.