using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Neverer.UtilityClass
{
    public enum ExchangeFormat
    {
        Unspecified = 0,
        SOAP = 1,
        AJAX = 2
    }

    public enum ExistsBehavior
    {
        Append = 0,
        Overwrite = 1
    }

    public class RemoteCrosswordDictionary : CrosswordDictionary
    {
        // TODO - build AJAX methods

        public ExchangeFormat format = ExchangeFormat.AJAX;
        public Boolean canRead = true;
        public Boolean canWrite = true;

        public String endpointUri = "";
        public String addUpdateMethod = "addUpdateWord";
        public String removeMethod = "removeWord";
        public String retrieveMethod = "retrieveWords";
        public String retrieveSinceMethod = "retrieveWordsSince";

        /// <summary>
        /// Adds a word (with or without definitions) to the remote dictionary
        /// </summary>
        /// <param name="word">The word to add</param>
        /// <param name="definitions">An optional list of definitions for the given word</param>
        /// <param name="onExistsBehavior">What to do if the entry already exists</param>
        public void addUpdateWord(String word, List<String> definitions, ExistsBehavior onExistsBehavior = ExistsBehavior.Append)
        {
            if (!canWrite)
            {
                throw new InvalidOperationException("Cannot add or update entries when the dictionary is not writable.");
            }
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Removes a word from the remote dictionary
        /// </summary>
        /// <param name="word">The word to remove</param>
        /// <param name="definitions">Optionally, the definitions to remove</param>
        /// <param name="removeSpecifiedDefinitionsOnly">If true, the word is not removed, but only the definitions listed</param>
        public void removeWord(String word, List<String> definitions, Boolean removeSpecifiedDefinitionsOnly = false)
        {

            if (!canWrite)
            {
                throw new InvalidOperationException("Cannot remove entries when the dictionary is not writable.");
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieve all words from the dictionary
        /// </summary>
        /// <returns>A dictionary of words and definitions</returns>
        public Dictionary<String,List<String>> retrieveWords()
        {
            if (!canRead)
            {
                throw new InvalidOperationException("Cannot read entries when the dictionary is not readable.");
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieve all words added since the given timestamp
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns>A dictionary of words and definitions</returns>
        public Dictionary<String, List<String>> retrieveWordsSince(DateTime timestamp)
        {
            if (!canRead)
            {
                throw new InvalidOperationException("Cannot read entries when the dictionary is not readable.");
            }
            throw new NotImplementedException();
        }
    }
}
