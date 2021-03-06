using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;


// TODO - create an Interface that covers this and CrosswordDictionary, so that Creator.AllDictionaries can contain both
namespace Neverer.UtilityClass
{
    public class WordList : IWordSource
    {
        private DateTime? __LastUpdated = null;
        public String dictionaryName { get; set; }

        public ObservableCollection<String> Words { get; set; }
        private SerializableDictionary<String, List<String>> __Definitions = null;
        public bool Readonly { get; set; }
        public String fileName { get; set; }
        public bool Autosave { get; set; }
        public bool enabled { get; set; }

        public WordList()
        {
            this.Readonly = true;
            this.Autosave = false;
            this.fileName = null;
            this.enabled = true;
            this.Words = new ObservableCollection<String>();
            this.Words.CollectionChanged += Words_CollectionChanged;
        }
        public WordList(String filename, bool Enabled = true, bool Readonly = false, bool Autosave = false)
        {
            this.Readonly = Readonly;
            this.Autosave = Autosave;
            this.enabled = Enabled;
            this.fileName = filename;
            try
            {
                this.LoadFromFile(filename);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Could not load from file {0}", filename), ex);
            }
            this.Words.CollectionChanged += Words_CollectionChanged;
        }
        public WordList(ObservableCollection<String> Words)
        {
            this.Readonly = true;
            this.Autosave = false;
            this.enabled = true;
            this.fileName = null;
            this.Words = Words;
            this.Words.CollectionChanged += Words_CollectionChanged;
        }

        public IEnumerable<String> keys
        {
            get
            {
                return Words;
            }
        }

        private void populateDictionaryCache()
        {
            __Definitions = new SerializableDictionary<String, List<String>>();
            foreach (String word in Words)
            {
                __Definitions.Add(word, new List<String>());
            }
        }
        public SerializableDictionary<String, List<String>> entries
        {
            get
            {
                if (__Definitions == null) { populateDictionaryCache(); }
                return __Definitions;
            }
        }
        public DateTime lastUpdated
        {
            get
            {
                if (!__LastUpdated.HasValue) { __LastUpdated = DateTime.Now; }
                return __LastUpdated.Value;
            }
            set
            {
                __LastUpdated = value;
            }
        }


        private void Words_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Invalidate Dictionary Cache
            __Definitions = null;
            // If collection changes and Autosave is true, save now
            if (this.Autosave)
            {
                this.Save();
            }
        }

        public void Save()
        {
            if (this.Readonly)
            {
                throw new Exception("Cannot save to a readonly wordlist");
            }
            if (this.fileName == null)
            {
                throw new Exception("Cannot save without a filename being set");
            }
            SaveToFile(this.fileName);
        }

        public void SaveToFile(String filename, bool persistFilename = true)
        {
            // Ensure folder exists
            FileInfo fi = new FileInfo(filename);
            Directory.CreateDirectory(fi.DirectoryName);
            // If persistFilename is true, this is our new filename for the wordlist, even if we already have one
            if (persistFilename) { this.fileName = filename; }
            TextWriter tw = new StreamWriter(filename);
            IEnumerable<String> sorted = (from String s in Words orderby s select s);
            foreach (String s in sorted)
            {
                tw.WriteLine(s);
            }
            tw.Close();
        }

        public void LoadFromFile(String filename, bool persistFilename = true, bool Enabled = true, bool Autosave = false)
        {
            if (!File.Exists(filename))
            {
                throw new Exception(String.Format("File {0} does not exist", filename));
            }
            // Set autosave to false so we don't overwrite any existing linked wordlist on load
            this.Autosave = false;
            // Do not add handler for CollectionChanged event, as it should already be present from constructor
            if (this.Words == null) { this.Words = new ObservableCollection<String>(); }
            this.Words.Clear();
            this.Words = new ObservableCollection<String>(File.ReadAllLines(filename));
            // If persistFilename is true, this is our new filename for the wordlist, even if we already have one
            if (persistFilename) { this.fileName = filename; }
            this.Autosave = Autosave;
            this.enabled = Enabled;
        }
    }
}
