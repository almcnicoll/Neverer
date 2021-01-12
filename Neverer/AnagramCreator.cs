using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CrosswordShared.UtilityClass;
using Neverer.UtilityClass;

namespace Neverer
{
    public partial class AnagramCreator : Form
    {
        Creator __callingInstance;
        HashSet<String> __allWords;
        int minWordSize = 1;

        SortedWordDictionary swdDict = new SortedWordDictionary();

        HashSet<String> allWords
        {
            get
            {
                if (allWords == null)
                {
                    populateAllWords();
                }
                return __allWords;
            }
        }

        public AnagramCreator(Creator callingInstance)
        {
            __callingInstance = callingInstance;
            __allWords = null;
            InitializeComponent();
        }

        /// <summary>
        /// Populates the local cache with all words from all dictionaries and word lists
        /// </summary>
        void populateAllWords()
        {
            __allWords = new HashSet<String>();
            foreach (DictType dt in __callingInstance.AllDictionaries.Keys)
            {
                if (__callingInstance.AllDictionaries[dt] != null)
                {
                    foreach (IWordSource ws in __callingInstance.AllDictionaries[dt])
                    {
                        __allWords.UnionWith(ws.keys);
                    }
                }
            }
        }

        /// <summary>
        /// Generates a list of possible anagrams from the given text
        /// </summary>
        /// <param name="Source">The source text</param>
        /// <param name="MaxEntries">The maximum number of entries to produce</param>
        private void generateAnagrams(String Source, int MaxEntries)
        {
            // TODO - Anagram generation code
            HashSet<String> dict = this.allWords;
            {

            }
        }

        // recursive function to find all the anagrams for charInventory characters
        // starting with the word at dictionaryIndex in dictionary keyList
        private HashSet<HashSet<String>> findAnagrams(int dictionaryIndex, char[] charInventory, List<String> keyList)
        {
            // terminating condition if no words are found
            if (dictionaryIndex >= keyList.Count || charInventory.Length < minWordSize)
            {
                return null;
            }

            String searchWord = keyList[dictionaryIndex];
            char[] searchWordChars = searchWord.ToCharArray();
            // This is where you find the anagrams for whole word
            //if (AnagramSolverHelper.isEquivalent(searchWordChars, charInventory)){
            if (searchWordChars.isEquivalent(charInventory))

            {
                HashSet<HashSet<String>> anagramsSet = new HashSet<HashSet<String>>();
                HashSet<String> anagramSet = new HashSet<String>();
                anagramSet.Add(searchWord);
                anagramsSet.Add(anagramSet);

                return anagramsSet;
            }

            // This is where you find the anagrams with multiple words
            //if (AnagramSolverHelper.isSubset(searchWordChars, charInventory))
            if (searchWordChars.isSubset(charInventory))
            {
                // update charInventory by removing the characters of the search
                // word as it is subset of characters for the anagram search word
                char[] newCharInventory = charInventory.setDifference(searchWordChars);
                if (newCharInventory.Length >= minWordSize)
                {
                    HashSet<HashSet<String>> anagramsSet = new HashSet<HashSet<String>>();
                    for (int index = dictionaryIndex + 1; index < keyList.Count; index++)
                    {
                        HashSet<HashSet<String>> searchWordAnagramsKeysSet = findAnagrams(index, newCharInventory, keyList);
                        if (searchWordAnagramsKeysSet != null)
                        {
                            HashSet<HashSet<String>> mergedSets = mergeWordToSets(searchWord, searchWordAnagramsKeysSet);
                            anagramsSet.UnionWith(mergedSets);
                        }
                    }
                    return ((anagramsSet.Count == 0) ? null : anagramsSet);
                }
            }

            // no anagrams found for current word
            return null;
        }

        /// <summary>
        /// This function will merge the real dictionary words found under the sorted key word
        /// for e.g. if the set of words to be merged are [elt, aet]
        /// and the real dictionary words for 'elt' are [let, tel]
        /// and the real dictionary words for 'aet' are [eat, tea]
        /// then the merged set would be [[let, eat], [let, tea], [tel, eat], [tel, tea]]
        /// </summary>
        /// <param name="anagramKeySet">The sorted key word</param>
        /// <returns>The merged set of matching words</returns>
        private HashSet<HashSet<String>> mergeAnagramKeyWords(
                HashSet<String> anagramKeySet)
        {
            if (anagramKeySet == null)
            {
                throw new ArgumentNullException("Anagram keys set cannot be null");
            }
            HashSet<HashSet<String>> anagramsSet = new HashSet<HashSet<String>>();
            foreach (String word in anagramKeySet)
            {
                HashSet<String> anagramWordSet = swdDict.findSingleWordAnagrams(word);
                anagramsSet.Add(anagramWordSet);
            }

            //HashSet<String>[] anagramsSetArray = anagramsSet.toArray(new Set[0]);
            HashSet<String>[] anagramsSetArray = new HashSet<string>[anagramsSet.Count];
            anagramsSet.CopyTo(anagramsSetArray);

            return AnagramFunctions.setMultiplication(anagramsSetArray);
        }

        // add word to all the sets
        private HashSet<HashSet<String>> mergeWordToSets(String word, HashSet<HashSet<String>> sets)
        {
            if (word.isEmpty())
            {
                throw new ArgumentOutOfRangeException("Word cannot be null");
            }
            if (sets == null)
            {
                return null;
            }
            HashSet<HashSet<String>> mergedSets = new HashSet<HashSet<String>>();
            foreach (HashSet<String> set in sets)
            {
                if (set == null)
                {
                    throw new ArgumentNullException("Anagram keys set cannot be null");
                }
                set.Add(word);
                mergedSets.Add(set);
            }

            return mergedSets;
        }

        /*
         * prints usage instructions
        private static void usage()
        {

            System.out.println("Usage:");
            System.out.println("\tjava -cp AnagramSolver.jar com.parthparekh.algorithms.AnagramSolver " +
                                        "<absolute_path_to_wordlist_file> <min_word_length> <words_for_anagram_search>");
            System.out.println("");
        }
        */

        /*
        public static void main(String[] args) throws IOException
        {
        if (args.length< 3) {
            usage();
        System.exit(1);
        }
    String wordlistPath = args[0];
    assert !wordlistPath.isEmpty();
        Integer minWordLength = Integer.parseInt(args[1]);
        if (minWordLength <= 0) {
            // defaulting it to 3
            minWordLength = 3;
        }
        
AnagramSolver anagramSolver = new AnagramSolver(minWordLength, wordlistPath);
String anagramWords = "";
for (int index = 2; index < args.length; index++)
{
    anagramWords += args[index];
    anagramWords += " ";
}
System.out.println("All the anagrams for \"" + anagramWords + "\" are: ");
System.out.println("");

HashSet<HashSet<String>> anagrams = anagramSolver.findAllAnagrams(anagramWords);
System.out.println("");
if (anagrams == null || anagrams.isEmpty())
{
    System.out.println("no anagrams found..");
}
else
{
    System.out.println("Total " + anagrams.size() + " anagrams found");
}
    }
}
        */
    }
}
