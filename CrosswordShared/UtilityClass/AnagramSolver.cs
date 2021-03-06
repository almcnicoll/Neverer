using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Neverer.UtilityClass
{
    public class AnagramSolver
    {
        public HashSet<String> workingList;
        public static Dictionary<String, AnagramTree> globalLookup = null;

        public AnagramSolver(List<IWordSource> sourceLists)
        {
            if (globalLookup == null) { globalLookup = new Dictionary<String, AnagramTree>(); }
            workingList = new HashSet<String>();
            foreach (IWordSource ws in sourceLists)
            {
                workingList.UnionWith(ws.keys);
            }
        }

        /// <summary>
        /// Solve all anagrams for the given string
        /// </summary>
        /// <param name="input">The string to anagram</param>
        /// <returns>A HashSet of anagram strings</returns>
        public HashSet<String> solve(String input)
        {
            // Clear cache
            globalLookup.Clear();

            // Output vars and working vars
            HashSet<String> output = new HashSet<String>();
            String sortedInput = AnagramFunctions.getOrderedLetterString(input);

            // Create a parent AnagramTree object
            AnagramTree root = new AnagramTree(sortedInput, ref workingList, ref globalLookup);

            if (root.solve(false))
            {
                output.UnionWith(root.getAllAnagrams());
            }

            // Return results
            return output;
        }
    }
}
