using Neverer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordShared.UtilityClass
{
    /// <summary>
    /// Data structure to store the dictionary of words with sorted keys
    /// </summary>
    public class SortedWordDictionary
    {
        // below map will store string with sorted characters as key and all the anagrams of that string as value
        private SortedDictionary<String, HashSet<String>> sortedStringMap = new SortedDictionary<String, HashSet<String>>();
        private Boolean isDictionaryLoaded { get; set; } = true; // TODO check if this needs initialising as false

        /*
          * loads the words from wordlist file into map; it assumes the wordlist file contains words delimited by newline
          * i.e. \n
          *
          * @param filePath absolute file path of the wordlist (assuming it's in the classpath)
          
        public void loadDictionary(String filePath)
        {

            loadDictionaryWithSubsets(filePath, null, 0);
        }
        */

        /*
          * loads only the words that are subsets of wordString from wordlist file into map;
          * it assumes the wordlist file contains words delimited by newline i.e. \n
          *
          * @param filePath absolute file path of the wordlist (assuming it's in the classpath)
          *
          * @param wordString string to check for subsets
          *
          * @param minWordSize minimum word size to load from dictionary
          
        public void loadDictionaryWithSubsets(String filePath, String wordString,
                                              int minWordSize)
        {

        if (filePath == null || filePath.isEmpty()) {
            throw new ArgumentException("file path invalid");
    }

        try {
            File file = new File(filePath);
    BufferedReader reader = new BufferedReader(new InputStreamReader(
            new FileInputStream(file)));
    String word;
            while ((word = reader.readLine()) != null)
            {
                assert word != null;
                word = word.trim().toLowerCase();
    String sortedWord = AnagramSolverHelper.sortWord(word);
                if (sortedWord == null
                        || sortedWord.isEmpty()
                        || (wordString != null && !wordString.isEmpty() && (sortedWord
                        .length() < minWordSize || !AnagramSolverHelper
                        .isSubset(sortedWord.toCharArray(), wordString
                                .replaceAll("\\s", "").toLowerCase()
                                .toCharArray()))))
                {
                    // don't add the word to dictionary if word is empty or if
                    // word from word-list is not a subset of wordString or word
                    // is less than minWordSize
                    continue;
                }
HashSet<String> wordSet = sortedStringMap.get(sortedWord);
if (wordSet != null)
{
    // add word to the existing wordset
    wordSet.add(word);
}
else
{
    wordSet = new TreeHashSet<String>();
    wordSet.add(word);
    sortedStringMap.put(sortedWord, wordSet);
}
            }

            reader.close();
isDictionaryLoaded = true;
        } catch (IOException ioException)
{
    throw ioException;
}
    }
        */

        /// <summary>
        /// Add word to dictionary
        /// </summary>
        /// <param name="wordString">The string to add</param>
        /// <returns>Returns true if the word is successfully added, otherwise false</returns>
        public Boolean addWord(String wordString)
        {

            if (wordString.isEmpty())
            {
                return false;
            }

            String sortedWord = wordString.sortWord();
            //HashSet<String> wordSet = sortedStringMap.get(sortedWord);
            HashSet<String> wordSet;
            sortedStringMap.TryGetValue(sortedWord, out wordSet);
            if (wordSet != null)
            {
                // add word to the existing words set
                wordSet.Add(wordString);
            }
            else
            {
                // add create new words set
                //wordSet = new TreeHashSet<String>();
                wordSet = new HashSet<String>();
                wordSet.Add(wordString);
                sortedStringMap[sortedWord] = wordSet;
            }

            return true;
        }

        /// <summary>
        /// Finds all anagrams of the word in the dictionary
        /// </summary>
        /// <param name="wordString">Word for which anagrams are to be found</param>
        /// <returns>Set of single word anagrams for the given string</returns>
        public HashSet<String> findSingleWordAnagrams(String wordString)
        {

            if (!isDictionaryLoaded)
            {
                throw new Exception("Dictionary not loaded");
            }
            else
            {

                if (wordString == null || wordString.isEmpty())
                {
                    throw new ArgumentException("Word string cannot be empty");
                }
                HashSet<String> returnVal;
                sortedStringMap.TryGetValue(wordString.sortWord(), out returnVal);
                return returnVal;
            }
        }

        /// <summary>
        /// Get all keys in the dictionary
        /// </summary>
        /// <returns>A list of all keys</returns>
        public List<String> getDictionaryKeyList()
        {
            if (sortedStringMap == null) { throw new ArgumentNullException("Dictionary not initialised"); }
            return sortedStringMap.Keys.ToList();
        }

        /// <summary>
        /// Overrides the default toString() display
        /// </summary>
        /// <returns></returns>
        public String toString()
        {
            return "isDictionaryLoaded?: " + isDictionaryLoaded + "\nDictionary: " + sortedStringMap.Keys.Count + " entries";
        }



    }

    public static class AnagramFunctions
    {

        #region "Anagram functions"
        /// <summary>
        /// Sort the characters in a word string
        /// </summary>
        /// <param name="wordString">String to sort</param>
        /// <returns>String with sorted characters</returns>
        public static String sortWord(this String wordString)
        {
            if (wordString.isEmpty())
            {
                return null;
            }
            /*byte[] charBytes = wordString.getBytesUTF8();
            Array.Sort(charBytes);
            return charBytes.makeStringUTF8();*/
            char[] chars = wordString.ToCharArray();
            Array.Sort(chars);
            return new String(chars);
        }

        /// <summary>
        /// Checks if the first character array is subset of second character array
        /// </summary>
        /// <param name="charArr1">character array charArr1 to check for subset</param>
        /// <param name="charArr2">checking for subset against character array charArr2</param>
        /// <returns>true if charArray1 is subset of charArray2, false otherwise</returns>
        public static Boolean isSubset(this char[] charArr1, char[] charArr2)
        {
            if (charArr1.Length > charArr2.Length)
            {
                return false;
            }
            List<Char> charList1 = new List<Char>(charArr1);
            List<Char> charList2 = new List<Char>(charArr2);
            // cannot do containsAll as there can be duplicate characters
            foreach (Char charValue in charList1)
            {
                if (charList2.Contains(charValue))
                {
                    charList2.Remove(charValue);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /*
        public static List<Char> toList(this char[] charArr)
        {
            assert charArr != null;
            List<Char> charList = new ArrayList<Char>();
            for (char ch : charArr)
            {
                charList.add(ch);
            }
            return charList;
        }

        */

        /*
        /// <summary>
        /// Converts character list to character array
        /// </summary>
        /// <param name="charList">The list to convert</param>
        /// <returns>The array of <see cref="Char"/>s</returns>
        public static char[] toCharArray(this List<Char> charList)
        {
            if (charList == null || charList.isEmpty())
            {
                return new char[0];
            }

            char[] charArr = new char[charList.Count];
            for (int index = 0; index < charList.Count; index++)
            {
                charArr[index] = charList[index];
            }
            return charArr;
        }*/

        /// <summary>
        /// checks if two character arrays are equivalent;
        /// char arrays are equivalent if:
        /// 1. the number of elements in them are equal, and
        /// 2. all the elements are same(not necessarily in same order)
        /// complexity should be O(n)
        /// </summary>
        /// <param name="charArr1">first character array for equivalence check</param>
        /// <param name="charArr2">second character array for equivalence check</param>
        /// <returns>true if charArr1 is equivalent to charArr2, false otherwise</returns>
        public static Boolean isEquivalent(this char[] charArr1, char[] charArr2)
        {
            if (charArr1.Length != charArr2.Length)
            {
                return false;
            }
            int sum1 = 0;
            int sum2 = 0;
            for (int index = 0; index < charArr1.Length; index++)
            {
                sum1 += charArr1[index];
                sum2 += charArr2[index];
            }
            // in most cases it would return from here
            if (sum1 != sum2)
            {
                return false;
            }
            List<Char> charList1 = new List<Char>(charArr1);
            List<Char> charList2 = new List<Char>(charArr2);
            foreach (Char charValue in charList1)
            {
                charList2.Remove(charValue);
            }
            return charList2.isEmpty();
        }

        /// <summary>
        /// calculates set difference for 2 character arrays i.e. charArr1 - charArr2 removes all charArr2 elements from charArr1
        /// complexity should be O(n)
        /// </summary>
        /// <param name="charArr1">first character array for set difference</param>
        /// <param name="charArr2">second character array for set difference</param>
        /// <returns>Resultant character array of set difference between charArr1 and charArr2</returns>
        public static char[] setDifference(this char[] charArr1, char[] charArr2)
        {
            List<Char> list1 = new List<Char>(charArr1);
            List<Char> list2 = new List<Char>(charArr2);
            foreach (Char charObj in list2)
            {
                list1.Remove(charObj);
            }
            return list1.ToArray(); //list1.toCharArray();
        }

        /// <summary>
        /// Function to perform set multiplication of all the sets of strings passed
        /// </summary>
        /// <param name="setsArray">Multiple sets to multiply (can be a set of strings array)</param>
        /// <returns>Returns set consisting of set of strings after cartesian product is applied</returns>
        public static HashSet<HashSet<String>> setMultiplication(params HashSet<String>[] setsArray)
        {
            if (setsArray == null || setsArray.Length == 0)
            {
                return null;
            }
            return setMultiplication(0, setsArray);
        }

        /// <summary>
        /// Recursive function to calculate the cartesian product of all the sets of strings passed
        /// </summary>
        /// <param name="index">Recursion depth - I think?</param>
        /// <param name="setsArray">Multiple sets to multiply (can be a set of strings array)</param>
        /// <returns>Returns set consisting of set of strings after cartesian product is applied</returns>
        public static HashSet<HashSet<String>> setMultiplication(int index, params HashSet<String>[] setsArray)
        {
            HashSet<HashSet<String>> setsMultiplied = new HashSet<HashSet<String>>();
            if (index == setsArray.Length)
            {
                setsMultiplied.Add(new HashSet<String>());
            }
            else
            {
                foreach (String obj in setsArray[index])
                {
                    foreach (HashSet<String> set in setMultiplication(index + 1, setsArray))
                    {
                        set.Add(obj);
                        setsMultiplied.Add(set);
                    }
                }
            }

            return setsMultiplied;
        }
        #endregion
    }
}
