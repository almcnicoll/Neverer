using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Structure:
 *   Default:
 *     List of CrosswordDictionaries
 *   Custom:
 *     List of CrosswordDictionaries
 *   Remote:
 *     List of CrosswordDictionaries
 */

namespace Neverer.UtilityClass
{
    public class CrosswordDictionaryCollection : SerializableDictionary<DictType, List<IWordSource>>
    {
        Dictionary<String, List<String>> results = new Dictionary<String, List<String>>();

        public Dictionary<String, List<String>> RegExpLookup(String pattern, int maxResults = 100)
        {
            // Create results structure
            results.Clear();

            // Loop through each dictionary, looking for matches
            foreach (DictType dt in Enum.GetValues(typeof(DictType)))
            {
                foreach (IWordSource ws in this[dt])
                {
                    // Only peruse enabled dictionaries
                    if (!ws.Enabled) { continue; }

                    // Cap results at maximum minus any already retrieved
                    int maxMatches = maxResults - results.Keys.Count;

                    // Retrieve matching keys
                    List<String> keys = (from KeyValuePair<String, List<String>> kvp in ws.Entries
                                         where kvp.Key.MatchesRegex(pattern)
                                         select kvp.Key).Take(maxMatches).ToList();

                    // Put contents of matching keys into results
                    foreach (String k in keys)
                    {
                        if (!results.ContainsKey(k)) { results.Add(k, new List<String>()); }
                        results[k].AddRange(ws.Entries[k]);
                    }

                    // Stop if we've got enough results
                    if (results.Keys.Count >= maxResults) { break; }
                }
                // Stop if we've got enough results
                if (results.Keys.Count >= maxResults) { break; }
            }
            return results;
        }
    }
}
