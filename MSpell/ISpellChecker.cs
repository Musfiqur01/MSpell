/* Copyright (c) 2017 Musfiqur Rahman */
using System;
using System.Collections.Generic;
using System.Text;

namespace MSpell
{
    /// <summary>
    /// Checks and corrects spelling error
    /// </summary>
    public interface ISpellChecker
    {
        /// <summary>
        /// Determines if the spelling is correct
        /// </summary>
        /// <param name="word">The input word</param>
        /// <returns>True if the spelling is correct</returns>
        bool IsSpellingCorrect(string word);

        /// <summary>
        /// Adds the words to the internal dictionary
        /// </summary>
        /// <param name="dictionary">The dictionary</param>
        void Train(List<string> dictionary);

        /// <summary>
        /// Adds the word to the internal dictionary
        /// </summary>
        /// <param name="word"></param>
        void AddWord(string word);

        /// <summary>
        /// Returns all the word in the dictionary with specified edit distance
        /// </summary>
        /// <param name="word">The word</param>
        /// <param name="editDistance">The distance</param>
        /// <returns>The list of candidates</returns>
        List<Candidate> GetSuggestedWords(string word, int editDistance = 2);
    }
}