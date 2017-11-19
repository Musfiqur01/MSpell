using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MSpellTest
{
    [TestClass]
    public class SpellCheckerFactoryTest
    {
        [TestMethod]
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

    }
}