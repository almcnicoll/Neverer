using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neverer.UtilityClass
{
    public static class AnagramFunctions
    {
        public static String Alphabetise(this String original)
        {
            char[] letters = original.ToCharArray();
            Array.Sort(letters);
            return new String(letters);
        }

        public static Dictionary<String, HashSet<String>> GetOrderedDictionary(this IEnumerable<String> original)
        {
            Dictionary<String, HashSet<String>> output = new Dictionary<String, HashSet<String>>();
            foreach (String word in original)
            {
                String a = Alphabetise(word);
                if (output.ContainsKey(a))
                {
                    //output[a] += "," + word;
                    output[a].Add(word);
                }
                else
                {
                    output.Add(a, new HashSet<String>());
                    output[a].Add(word);
                }
            }
            return output;
        }

        /// <summary>
        /// Swaps two elements by index in a character array
        /// </summary>
        /// <param name="letters">The array of characters</param>
        /// <param name="pos1">The position of the first character to swap</param>
        /// <param name="pos2">The position of the second character to swap</param>
        /// <returns>The new array of characters</returns>
        public static char[] Swap(this char[] letters, int pos1, int pos2)
        {
            char[] output = new char[letters.Length];
            letters.CopyTo(output, 0);
            output[pos1] = letters[pos2];
            output[pos2] = letters[pos1];
            return output;
        }
        /// <summary>
        /// Swaps two elements by index in a string
        /// </summary>
        /// <param name="letterString">The string of characters</param>
        /// <param name="pos1">The position of the first character to swap</param>
        /// <param name="pos2">The position of the second character to swap</param>
        /// <returns>The new array of characters</returns>
        public static char[] Swap(this String letterString, int pos1, int pos2)
        {
            char[] letters = letterString.ToCharArray();
            return letters.Swap(pos1, pos2);
        }

        /// <summary>
        /// Returns all valid permutations of <paramref name="original"/> within <paramref name="dictionary"/>
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="original"></param>
        /// <returns>A <see cref="HashSet"/> of Strings</returns>
        /// <seealso cref="https://www.researchgate.net/publication/10779787_Generating_anagrams_from_multiple_core_strings_employing_user-defined_vocabularies_and_orthographic_parameters?enrichId=rgreq-fa2796d62326869bf4a0cda7e6089347-XXX&enrichSource=Y292ZXJQYWdlOzEwNzc5Nzg3O0FTOjEwMjM2NDM0NjMyMjk0NUAxNDAxNDE2OTcwNjEz&el=1_x_2&_esc=publicationCoverPdf"/>
        public static HashSet<String> GetPermutations(this String original, IEnumerable<String> dictionary = null)
        {
            HashSet<String> output = new HashSet<String>();
            if ((dictionary == null) || (dictionary.Contains(original))) { output.Add(original); }

            char[] letters = original.ToCharArray();

            for (int k = 0; k < letters.Length; k++)
            {
                for (int n = 0; n < k; n++)
                {
                    String[] looper = new String[output.Count];
                    output.CopyTo(looper);
                    foreach (String existingPerm in looper)
                    {
                        //char[] permLetters = letters.Swap(k, n);
                        char[] permLetters = existingPerm.Swap(k, n);
                        String permString = new String(permLetters);
                        if ((dictionary == null) || (dictionary.Contains(permString)))
                        {
                            output.Add(permString);
                        }
                    }
                }
            }

            // Dictionary-filtering
            if (dictionary == null)
            {
                output.IntersectWith(dictionary);
            }

            return output;
        }
    }
}
