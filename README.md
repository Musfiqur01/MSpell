# MSpell
Identifies spelling mistakes and correct those.

## Getting Started

Install the nuget package to get started.

The usage is:

var spellChecker = new MSpell.SpellChecker();
var dictionary = new List<string> {"moon","soon","mono","windows"};
spellChecker.Train(dictionary);
            
var suggestion = spellChecker.GetSuggestedWords("mon", 1);

The output should be mono,moon.


## Versioning

We use appveyor for versioning.

## Authors

Musfiqur Rahman

See also the list of [contributors](https://github.com/Musfiqur01/MSpell/graphs/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

