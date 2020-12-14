using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Neverer
{
    [Serializable]
    public class WordSources
    {
        List<String> Dictionaries { get; set; }
        List<String> WordLists { get; set; }
        int DefaultDictionaryIndex { get; set; } = 0;
        int DefaultWordListIndex { get; set; } = 0;


        /// <summary>
        /// Returns the default folder to which this object should be serialized
        /// </summary>
        public static String DefaultFolder
        {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                    , Assembly.GetExecutingAssembly().GetName().Name
                );
            }
        }
        /// <summary>
        /// Returns the default filename to which this object should be serialized
        /// </summary>
        private static String DefaultFilename
        {
            get
            {
                String filePath = DefaultFolder;
                String fileName = Assembly.GetExecutingAssembly().GetName().Name + ".wordsources.xml";
                return Path.Combine(filePath, fileName);
            }
        }
        /// <summary>
        /// Serializes this object to an XML file. If filename is not specified, target is DefaultFolder + DefaultFilename
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Exception Save(String fileName = null)
        {
            if (fileName == null) { fileName = Path.Combine(WordSources.DefaultFolder, WordSources.DefaultFilename); }
            try
            {
                this.SaveToXML(fileName);
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        /// <summary>
        /// Loads object from an XML file. If filename is not specified, source is DefaultFolder + DefaultFilename
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static WordSources Load(String fileName = null)
        {
            if (fileName == null) { fileName = Path.Combine(WordSources.DefaultFolder, WordSources.DefaultFilename); }
            if (!File.Exists(fileName))
            {
                Settings ss = new Settings();
                ss.Save();
            }
            WordSources s = ExtensionMethods.LoadFromXML<WordSources>(fileName);
            return s;
        }

    }
}
