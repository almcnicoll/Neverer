using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Neverer.UtilityClass
{
    public static class AnagramFunctions
    {

        /// <summary>
        /// Returns a string of the letters in the source word, ordered alphabetically
        /// </summary>
        /// <param name="word">The source word</param>
        /// <param name="excludeDuplicates">Whether to return only unique letters</param>
        /// <returns>An ordered string of the characters in the source word</returns>
        public static String getOrderedLetterString(String word, Boolean excludeDuplicates = false)
        {
            char[] characters = word.ToLower().ToCharArray();
            if (excludeDuplicates)
            {
                char[] unique = characters.Distinct().ToArray();
                Array.Sort(unique);
                return new String(unique); //unique.ToString();
            }
            else
            {
                Array.Sort(characters);
                return new string(characters);
            }
        }

        /// <summary>
        /// Returns the initial string, minus the letters specified in <paramref name="subtractionLetters"/>
        /// </summary>
        /// <param name="initialString">The starting string</param>
        /// <param name="subtractionLetters">The letters to remove</param>
        /// <returns></returns>
        public static String subtractSortedString(String initialString, String subtractionLetters)
        {
            char[] allChars = subtractionLetters.ToCharArray();
            foreach (char c in allChars)
            {
                if (initialString.Contains(c.ToString()))
                {
                    var regex = new Regex(Regex.Escape(c.ToString()));
                    initialString = regex.Replace(initialString, "", 1);
                }
                else
                {
                    throw new Exception("Could not find all the subtractionLetters in initialString");
                }
            }
            return initialString;
        }


        /// <summary>
        /// Adds a value to a <see cref="HashSet{U}"/> within a <see cref="Dictionary{T, HashSet<U>}"/>, whether or not the key already exists
        /// </summary>
        /// <typeparam name="T">The key type of the Dictionary</typeparam>
        /// <typeparam name="U">The value type within the Dictionary's HashSets</typeparam>
        /// <param name="dict">The dictionary to update</param>
        /// <param name="key">The key to update or create</param>
        /// <param name="value">The value to add to the HashSet</param>
        public static void addToHashSetInDictionary<T, U>(ref Dictionary<T, HashSet<U>> dict, T key, U value)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, new HashSet<U>());
            }
            dict[key].Add(value);
        }

    }
}
