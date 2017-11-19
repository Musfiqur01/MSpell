/* Copyright (c) 2017 Musfiqur Rahman */
using System;
using System.Collections.Generic;
using System.Text;

namespace MSpell
{
    /// <summary>
    /// Creates a factory for all the spellchecker
    /// </summary>
    public class SpellCheckerFactory : ISpellCheckerFactory
    {
        /// <summary>
        /// Lnaguage to spellchecker mapping
        /// </summary>
        private Dictionary<string, ISpellChecker> languageSpellCheckerMapping;

        /// <summary>
        /// Creates a new instance of SpellChecker factory
        /// </summary>
        public SpellCheckerFactory()
        {
            this.languageSpellCheckerMapping = new Dictionary<string, ISpellChecker>();
        }

        /// <summary>
        /// Returns the SpellChecker for the language
        /// </summary>
        /// <param name="language">The language</param>
        /// <returns></returns>
        public ISpellChecker this[string language] => this.languageSpellCheckerMapping[language];

        /// <summary>
        /// Adds a spellchecker to the factory.
        /// </summary>
        /// <param name="language"></param>
        /// <param name="spellchecker"></param>
        public void AddSpellChecker(string language, ISpellChecker spellchecker)
        {
            this.languageSpellCheckerMapping.Add(language , spellchecker);
        }
    }
}