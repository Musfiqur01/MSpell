﻿/* Copyright (c) 2017 Musfiqur Rahman */
namespace MSpell
{
    /// <summary>
    /// Interface for spellchecker factory
    /// </summary>
    public interface ISpellCheckerFactory
    {
        /// <summary>
        /// Adds a spell checker to the factory
        /// </summary>
        /// <param name="language">The language</param>
        /// <param name="spellchecker">The spellchecker</param>
        void AddSpellChecker(string language, ISpellChecker spellchecker);

        /// <summary>
        /// Determines if the langauge is supported
        /// </summary>
        /// <param name="language">The language</param>
        /// <returns>True if the language is supported</returns>
        bool IsLanguageSupported(string language);

        /// <summary>
        /// The indexer for the Spellchecker 
        /// </summary>
        /// <param name="language">The language</param>
        /// <returns>The spellchecker for the language</returns>
        ISpellChecker this[string language] { get; }
    }
}
