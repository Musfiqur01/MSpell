/* Copyright (c) 2017 Musfiqur Rahman */
using System;
using System.Collections.Generic;
using System.Text;

namespace MSpell
{
    /// <summary>
    /// Candidate for a replacement for a word
    /// </summary>
    public class Candidate : IComparable<Candidate>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Candidate"/> class.
        /// </summary>
        /// <param name="word">The word</param>
        /// <param name="index">The index where other candidate generation should begin</param>
        /// <param name="node">The node of the trie</param>
        /// <param name="editDistance">The distance of the candidate from the original </param>
        internal Candidate(string word, int index, TrieNode node, int editDistance)
        {
            this.Word = word.ToLowerInvariant();
            this.Index = index;
            this.Node = node;
            this.EditDistance = editDistance;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Candidate"/> class.
        /// </summary>
        /// <param name="word">The word</param>
        /// <param name="editDistance">The distance of the candidate from the original </param>
        public Candidate(string word, int editDistance)
        {
            this.Word = word.ToLowerInvariant();
            this.EditDistance = editDistance;
        }

        /// <summary>
        /// Gets the edit distance of the candidate from the original word
        /// </summary>
        public int EditDistance { get; }

        /// <summary>
        /// Gets the node of the trie where the candidate generation should start
        /// </summary>
        private TrieNode Node { get; }

        /// <summary>
        /// Gets the index of the character where canddiate generation should start
        /// </summary>
        private int Index { get; }

        /// <summary>
        /// Gets the word of the current candidate
        /// </summary>
        public string Word { get; }

        /// <summary>
        /// Generates a hashset of candidate with the max edit distance
        /// </summary>
        /// <param name="maxEditDistance">The maximum edit distance</param>
        /// <returns>A hashset of candidates</returns>
        public HashSet<Candidate> GenerateCandidate(int maxEditDistance)
        {
            var candidates = new HashSet<Candidate>();
            if (EditDistance == maxEditDistance || Index > Word.Length) return candidates;

            // If the present index is contained candidates add it.
            if (Index < Word.Length && this.Node.Next.ContainsKey(Word[Index]))
            {
                candidates.Add(new Candidate(this.Word, this.Index + 1, this.Node.Next[Word[Index]], this.EditDistance));
            }

            // generate the delete candidate
            if (Index <= Word.Length - 1)
            {
                var letter = Word[Index];
                var deleteWord = new StringBuilder(Word);
                deleteWord.Remove(Index, 1);

                // delete word should begin from the first index, from the same node 
                var deleteCandidate = new Candidate(deleteWord.ToString(), Index, Node, EditDistance + 1);
                if (!candidates.Contains(deleteCandidate)) candidates.Add(deleteCandidate);
            }

            // generate the transpose candidates
            if (Index < Word.Length - 1)
            {
                var substitutionWord = new StringBuilder(Word);
                var c = substitutionWord[Index];
                substitutionWord[Index] = substitutionWord[Index + 1];
                substitutionWord[Index + 1] = c;
                var substitutionCandidate = new Candidate(substitutionWord.ToString(), Index, Node, EditDistance + 1);
                if (!candidates.Contains(substitutionCandidate)) candidates.Add(substitutionCandidate);
            }

            foreach (var c in Node.Next.Keys)
            {
                if (Index < Word.Length)
                {
                    if (c == Word[Index]) continue;

                    // generate the replace candidates
                    var replacedWord = new StringBuilder(Word);
                    replacedWord[Index] = c;
                    var replaceCandidate = new Candidate(replacedWord.ToString(), Index, Node, EditDistance + 1);
                    if (!candidates.Contains(replaceCandidate)) candidates.Add(replaceCandidate);
                }

                // generate the insert candidates
                var insertedWord = new StringBuilder(Word);
                insertedWord.Insert(Index, c);
                if(Word.Equals("mon") && c.Equals('o'))
                {

                }
                var insertCandidate = new Candidate(insertedWord.ToString(), Index, Node, EditDistance + 1);
                if (!candidates.Contains(insertCandidate)) candidates.Add(insertCandidate);
            }

            return candidates;
        }

        /// <summary>
        /// Determines if the candidates are equal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var other = obj as Candidate;
            return other != null && other.Word.Equals(this.Word, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Returns the hashcode of the current candidate
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return this.Word.GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of the current candidate
        /// </summary>
        /// <returns>A string representation which contains the word and its editdistance</returns>
        public override string ToString()
        {
            return $"<{this.Word},D:{this.EditDistance}";
        }

        /// <summary>
        /// Used for comparison with another candidate. The comparison is done on edit distance first , then the word.
        /// </summary>
        /// <param name="other">The other candidate</param>
        /// <returns>A comparison score based on edit distance and the word</returns>        
        public int CompareTo(Candidate other)
        {
            return this.EditDistance==other.EditDistance ? this.Word.CompareTo(other.Word) : this.EditDistance.CompareTo(other.EditDistance);
        }
    }
}
