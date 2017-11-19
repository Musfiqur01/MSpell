﻿/* Copyright (c) 2017 Musfiqur Rahman */
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSpell
{
    /// <summary>
    /// Spellchecker class
    /// </summary>
    public class SpellChecker : ISpellChecker
    {
        /// <summary>
        /// The head of the trie
        /// </summary>
        private TrieNode head;

        /// <summary>
        /// Creates a new instance of the <see cref="SpellChecker"/> class.
        /// </summary>
        public SpellChecker()
        {
            head = new TrieNode();
        }

        /// <summary>
        /// Determines if the spelling of the word is correct
        /// </summary>
        /// <param name="word">The input word</param>
        /// <returns>True if the spelling is correct</returns>
        public bool IsSpellingCorrect(string word)
        {
            var node = this.head;
            int index = 0;
            while (index < word.Length)
            {
                if (node.Next.ContainsKey(word[index]))
                {
                    node = node.Next[word[index++]];
                }
                else
                {
                    return false;
                }
            }

            return index == word.Length && node.IsWord;
        }

        /// <summary>
        /// Adds the words to the internal dictionary
        /// </summary>
        /// <param name="dictionary">The dictionary</param>
        public void Train(List<string> dictionary)
        {
            // train each word in the dictionary
            dictionary.ForEach(head.Insert);
        }

        /// <summary>
        /// Adds the word to the internal dictionary
        /// </summary>
        /// <param name="word"></param>
        public void AddWord(string word)
        {
            // train original word
            head.Insert(word);
        }

        /// <summary>
        /// Returns all the word in the dictionary with specified edit distance
        /// </summary>
        /// <param name="word">The word</param>
        /// <param name="editDistance">The distance</param>
        /// <returns>The list of candidate</returns>
        public List<Candidate> GetSuggestedWords(string word, int editDistance)
        {
            var allCandidates = new List<Candidate>();
            var input = new Candidate(word, 0, this.head, 0);
            var candidates = input.GenerateCandidate(editDistance);
            var validWords = new List<Candidate>();
            var queue = new Queue<Candidate>();
            foreach (var c in candidates)
            {
                queue.Enqueue(c);
            }

            while (queue.Count > 0)
            {
                var candidate = queue.Dequeue();
                if (IsSpellingCorrect(candidate.Word))
                {
                    validWords.Add(candidate);
                }

                if (candidate.EditDistance < editDistance)
                {
                    candidates = candidate.GenerateCandidate(editDistance);
                    foreach (var c in candidates)
                    {
                        queue.Enqueue(c);
                    }
                }
            }

            var suggestedCandidate = validWords.Distinct().ToList();
            suggestedCandidate.Sort();

            return suggestedCandidate;
        }
    }
}