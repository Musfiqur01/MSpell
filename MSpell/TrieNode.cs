/* Copyright (c) 2017 Musfiqur Rahman */
using System;
using System.Collections.Generic;
using System.Text;

namespace MSpell
{
    /// <summary>
    /// A trie node
    /// </summary>
    internal class TrieNode
    {
        /// <summary>
        /// Returns true if this node marks the end of a word
        /// </summary>
        public bool IsWord { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<char, TrieNode> Next = new Dictionary<char, TrieNode>();
        
        /// <summary>
        /// Inserts a word into dictionary
        /// </summary>
        /// <param name="word">The word</param>
        public void Insert(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                this.IsWord = true;
                return;
            }

            word = word.ToLowerInvariant();
            if (!Next.ContainsKey(word[0]))
            {
                Next.Add(word[0], new TrieNode());
            }

            Next[word[0]].Insert(word.Substring(1));
        }
    }
}