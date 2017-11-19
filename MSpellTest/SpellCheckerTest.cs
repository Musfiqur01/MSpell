using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
        public void IsSpellingCorrect_Returns_True_For_Invalid_Word()
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