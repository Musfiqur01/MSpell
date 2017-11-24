using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MSpellTest
{
    [TestClass]
    public class SpellCheckerFactoryTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddSpellChecker_When_SpellChecker_Null()
        {
            // Arrange
            var spellCheckerFactory = new MSpell.SpellCheckerFactory();

            // Act
            spellCheckerFactory.AddSpellChecker("en", null);

            // Assert            
        }

        [TestMethod]
        [ExpectedException(typeof(System.Collections.Generic.KeyNotFoundException))]
        public void Index_Invalid_Language_Throws_Exception()
        {
            // Arrange
            var spellCheckerFactory = new MSpell.SpellCheckerFactory();

            // Act
            var result = spellCheckerFactory["en"];

            // Assert
        }

        [TestMethod]
        public void IsLanguageSupported_True_If_The_Language_Is_Added()
        {
            // Arrange
            var spellCheckerFactory = new MSpell.SpellCheckerFactory();

            // Act
            spellCheckerFactory.AddSpellChecker("en", new MSpell.SpellChecker());

            // Assert
            Assert.IsTrue(spellCheckerFactory.IsLanguageSupported("en"));
        }

        [TestMethod]
        public void IsLanguageSupported_False_If_The_Language_Is_Not_Added()
        {
            // Arrange
            var spellCheckerFactory = new MSpell.SpellCheckerFactory();

            // Act
            spellCheckerFactory.AddSpellChecker("en", new MSpell.SpellChecker());

            // Assert
            Assert.IsFalse(spellCheckerFactory.IsLanguageSupported("de"));
        }
    }
}