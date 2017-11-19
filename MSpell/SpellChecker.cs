/* Copyright (c) 2017 Musfiqur Rahman */
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
            var validWords = new Dictionary<Candidate, Candidate>();
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
                    if (!validWords.ContainsKey(candidate)){
                        validWords.Add(candidate,candidate);
                    }
                    else
                    {
                        var prevCandidate = validWords[candidate];
                        if (prevCandidate.EditDistance > candidate.EditDistance)
                        {
                            validWords[candidate] = candidate;
                        }
                    }
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

            var suggestedCandidate = new List<Candidate>();

            // seperate candidates acccording to edit distance
            var editDistanceToCandidate = new Dictionary<int, List<Candidate>>();
            foreach(var candidate in validWords.Values.ToList())
            {
                if (!editDistanceToCandidate.ContainsKey(candidate.EditDistance))
                {
                    editDistanceToCandidate.Add(candidate.EditDistance, new List<Candidate>());
                }

                editDistanceToCandidate[candidate.EditDistance].Add(candidate);
            }
            
            // Add the candidates in suggested candidate
            for(int i = 1; i <= editDistance; i++)
            {
                if (editDistanceToCandidate.ContainsKey(i))
                {
                    suggestedCandidate.AddRange(PrefixBasedSort(word, editDistanceToCandidate[i]));
                }                
            }

            return suggestedCandidate;
        }

        /// <summary>
        /// Sorts a list based on prefix match with an input word
        /// </summary>
        /// <param name="word">The word</param>
        /// <param name="candidates">The candidates</param>
        /// <returns></returns>
        public static List<Candidate> PrefixBasedSort(string word, List<Candidate> candidates)
        {
            var sortedList = new List<Tuple<int,Candidate>>();
            foreach(var candidate in candidates)
            {
                int matchedCount = 0;
                while(matchedCount < candidate.Word.Length && matchedCount < word.Length && word[matchedCount] == candidate.Word[matchedCount])
                {
                    matchedCount++;
                }

                sortedList.Add(new Tuple<int, Candidate>(matchedCount, candidate));
            }

            sortedList.Sort((s, t) => {
                    if (s.Item1 == t.Item1) return t.Item2.Word.Length.CompareTo(s.Item2.Word.Length);
                    return t.Item1.CompareTo(s.Item1);
                });

            return sortedList.Select(t => t.Item2).ToList();
        }
    }
}