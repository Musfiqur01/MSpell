using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MSpell;

namespace MSpellTest
{
    [TestClass]
    public class SpellCheckerTest
    {
        [TestMethod]
        public void GetSuggestedWords_Returns_Candidate_With_EditDistance_1()
        {
            // Arrange
            var spellChecker = new MSpell.SpellChecker();
            var dictionary = new List<string> {"moon","soon","mono","windows"};
            spellChecker.Train(dictionary);
            
            // Act
            var suggestion = spellChecker.GetSuggestedWords("mon", 1);

            // Assert
            Assert.AreEqual(2, suggestion.Count);
            Assert.AreEqual("mono", suggestion[0].Word);
            Assert.AreEqual("moon", suggestion[1].Word);
        }

        [TestMethod]
        public void GetSuggestedWords_Returns_Candidate_With_EditDistance_2()
        {
            // Arrange
            var spellChecker = new MSpell.SpellChecker();
            var dictionary = new List<string> { "moon", "soon", "mono", "windows" };
            spellChecker.Train(dictionary);

            // Act
            var suggestion = spellChecker.GetSuggestedWords("windwo", 2);

            // Assert
            Assert.AreEqual(1, suggestion.Count);
            Assert.AreEqual("windows", suggestion[0].Word);
        }

        [TestMethod]
        public void PrefixBasedSort_Prioritizes_Highest_Prefix_Match()
        {
            var candidates = new List<Candidate> {
                { new Candidate("windows" ,1)},
                { new Candidate("window" ,1) },
                { new Candidate("mindow" ,1) }
            };

            var result = SpellChecker.PrefixBasedSort("windwos", candidates);
            Assert.AreEqual("windows", result[0].Word);
            Assert.AreEqual("window", result[1].Word);
            Assert.AreEqual("mindow", result[2].Word);
        }

        [TestMethod]
        public void GetSuggestedWords_Returns_Candidate_With_Sorted_By_EditDistance_Then_by_Word()
        {
            // Arrange
            var spellChecker = new MSpell.SpellChecker();
            var dictionary = new List<string> { "moon", "son", "mono", "money" };
            spellChecker.Train(dictionary);

            // Act
            var suggestion = spellChecker.GetSuggestedWords("mon", 2);

            // Assert
            Assert.AreEqual(4, suggestion.Count);
            Assert.AreEqual(1, suggestion[0].EditDistance);
            Assert.AreEqual("mono", suggestion[0].Word);
            Assert.AreEqual(1, suggestion[1].EditDistance);
            Assert.AreEqual("moon", suggestion[1].Word);
            Assert.AreEqual(1, suggestion[2].EditDistance);
            Assert.AreEqual("son", suggestion[2].Word);
            Assert.AreEqual(2, suggestion[3].EditDistance);
            Assert.AreEqual("money", suggestion[3].Word);
        }

        [TestMethod]
        public void IsSpellingCorrect_Returns_True_For_Valid_Word()
        {
            // Arrange
            var spellChecker = new MSpell.SpellChecker();
            spellChecker.AddWord("Hello");

            // Act
            var IsSpellingCorrect = spellChecker.IsSpellingCorrect("hello");

            // Assert
            Assert.IsTrue(IsSpellingCorrect);
        }

        [TestMethod]
        public void IsSpellingCorrect_Returns_False_For_Invalid_Word()
        {
            // Arrange
            var spellChecker = new MSpell.SpellChecker();
            
            // Act
            var IsSpellingCorrect = spellChecker.IsSpellingCorrect("hello");

            // Assert
            Assert.IsFalse(IsSpellingCorrect);
        }
    }
}