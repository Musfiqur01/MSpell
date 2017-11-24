[![Build status](https://ci.appveyor.com/api/projects/status/eoto2o84wgjysoil?svg=true)](https://ci.appveyor.com/project/Musfiqur01/mspell)

# MSpell
Identifies spelling mistakes and suggests replacements.

## Getting Started

Install the nuget package to get started.

The usage is:

var spellChecker = new MSpell.SpellChecker();

var dictionary = new List<string> {"moon","soon","mono","windows"};

spellChecker.Train(dictionary);

// To get all word in the dictionry within 1 edit distance 
            
var suggestion = spellChecker.GetSuggestedWords("mon", 1);

// The output is sorted by editdistance, prefixmatch, wordlength in order
The output should be mono,moon.


## Versioning

We use appveyor for versioning.

## Authors

Musfiqur Rahman

See also the list of [contributors](https://github.com/Musfiqur01/MSpell/graphs/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

