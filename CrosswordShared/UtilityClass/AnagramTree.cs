using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Neverer.UtilityClass
{
    public class AnagramTree
    {
        public String OriginalWord;
        private String OrderedString;
        public HashSet<String> words;
        public Dictionary<String, AnagramTree> children;
        public Boolean childrenPopulated = false;
        private String lastEvaluatedPartial = null;

        public HashSet<String> workingList;
        public Dictionary<String, AnagramTree> globalLookup;

        // TODO - set this to false
        public const Boolean DebugMode = true;

        public AnagramTree()
        {
            OrderedString = "";
            words = new HashSet<String>();
            children = new Dictionary<String, AnagramTree>();
        }

        public AnagramTree(String characters, ref HashSet<String> masterWordList, ref Dictionary<String, AnagramTree> globalLookup)
        {
            this.OriginalWord = characters;
            OrderedString = AnagramFunctions.getOrderedLetterString(characters);
            this.workingList = masterWordList;
            this.globalLookup = globalLookup;
            words = new HashSet<String>();
            children = new Dictionary<String, AnagramTree>();
        }

        public HashSet<String> getAllAnagrams(String prefix = "")
        {
            if (words.Count + children.Count == 0) { return null; }
            HashSet<String> output = new HashSet<String>();
            // Add whole-word anagrams if top-level
            if (true) //if (prefix == "")
            {
                {
                    foreach (String word in words)
                    {
                        output.Add(prefix + word);
                    }
                }
            }
            // Add sub-anagrams
            foreach (String partial in children.Keys)
            {
                AnagramTree remainder = children[partial];
                AnagramTree child = globalLookup[partial];
                //output.UnionWith(child.getAllAnagrams(prefix + " " + child.OriginalWord + " "));
                foreach (String word in child.words)
                {
                    output.UnionWith(remainder.getAllAnagrams(prefix + word + " "));
                }
            }
            return output;
        }

        /// <summary>
        /// Attempts to solve anagrams for this AnagramTree
        /// </summary>
        /// <param name="stopAtFirstSolution">Whether to return true at the first successful anagram, or whether to find all possible solutions</param>
        /// <param name="nestLevel">The recursion level of the function call</param>
        /// <returns>True if at least one valid anagram was found</returns>
        public bool solve(Boolean stopAtFirstSolution, int nestLevel = 0)
        {
            Dictionary<String, HashSet<String>> lookup = new Dictionary<String, HashSet<String>>();

            // Prepare initial word list - only words with the right letters
            String validLetters = AnagramFunctions.getOrderedLetterString(OrderedString, true);
            Regex reLimit = new Regex("^[" + validLetters + "]{1," + OrderedString.Length.ToString() + "}$", RegexOptions.IgnoreCase);
            HashSet<String> sourceWords = new HashSet<String>(
                            from String s in workingList
                            where reLimit.IsMatch(Regex.Replace(s, Clue.NonCountingChars_Regex, ""))
                            select s
                            );

            Log("Initial triage produced " + sourceWords.Count + " words", nestLevel);

            // Create a further filter - max number of each letter
            Dictionary<char, int> letterCounts = new Dictionary<char, int>();
            foreach (char c in OrderedString.ToCharArray())
            {
                if (!letterCounts.ContainsKey(c)) { letterCounts.Add(c, 0); }
                letterCounts[c]++;
            }
            String strMaxCountFilter = "";
            foreach (char c in letterCounts.Keys.OrderBy(c => c))
            {
                strMaxCountFilter += c.ToString() + "{0," + letterCounts[c] + "}";
            }
            Regex reMaxCountFilter = new Regex("^" + strMaxCountFilter + "$", RegexOptions.IgnoreCase);

            // Put candidate words into a dictionary with the sorted string as the key
            foreach (String word in sourceWords)
            {
                String ow = AnagramFunctions.getOrderedLetterString(word);
                // Filter on max-counts
                if (reMaxCountFilter.IsMatch(ow))
                {
                    AnagramFunctions.addToHashSetInDictionary(ref lookup, ow, word);
                    if (!globalLookup.ContainsKey(ow)) { globalLookup.Add(ow, new AnagramTree()); }
                    globalLookup[ow].words.Add(word);
                }
            }
            Log("Second filter produced " + sourceWords.Count + " possible partials or anagrams", nestLevel);

            // Retrieve whole-word anagrams
            if (lookup.ContainsKey(OrderedString))
            {
                this.words.UnionWith(lookup[OrderedString]);
            }
            if ((nestLevel == 0) && (this.words.Contains(this.OriginalWord)))
            {
                // Don't count original string as an option
                this.words.Remove(this.OriginalWord);
            }
            if (stopAtFirstSolution && (this.words.Count > 0))
            {
                return true;
            }

            // TODO - Perhaps raise an event here so we can update the interface after this initial step?

            // Start looking through candidate partial words, starting with the longest
            IEnumerable<String> partials = (from String k in lookup.Keys
                                            where (k.Length < OrderedString.Length)
                                            orderby k.Length descending, k
                                            select k);
            foreach (String partial in partials)
            {
                lastEvaluatedPartial = partial;
                // Get remainder string if this partial is used
                String remainder = AnagramFunctions.subtractSortedString(OrderedString, partial);
                Log("Breaking " + OriginalWord.ToUpper() + " into " + partial.ToUpper() + " and " + remainder.ToUpper(), nestLevel);

                // See what can be done with the remainder
                if (globalLookup.ContainsKey(remainder) && globalLookup[remainder].childrenPopulated)
                {
                    // We've already calculated this substring - just link to it
                    Log("We have already calculated anagrams for " + remainder.ToUpper(), nestLevel);
                    this.children.Add(remainder, globalLookup[remainder]);
                }
                else
                {
                    AnagramTree partialTree = new AnagramTree(remainder, ref this.workingList, ref globalLookup);
                    if (partialTree.solve(false, nestLevel + 1))
                    {
                        // At least one solution
                        this.children.Add(partial, partialTree);
                    }
                    else
                    {
                        // No solutions - scrap this partial
                    }
                }
            }

            this.childrenPopulated = true;
            return ((this.words.Count + this.children.Count) > 0);
        }

        public static void Log(String message, int nestLevel = 0)
        {
            if (DebugMode)
            {
                String prefix = new String('\t', nestLevel);
                Console.WriteLine(prefix + message);
            }
        }
    }
}
