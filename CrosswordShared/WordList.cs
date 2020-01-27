using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Neverer.UtilityClass
{
    class WordList
    {
        public ObservableCollection<String> Words { get; set; }
        public bool Readonly { get; set; }
        public String Filename { get; set; }
        public bool Autosave { get; set; }

        public WordList()
        {
            this.Readonly = true;
            this.Autosave = false;
            this.Filename = null;
            this.Words = new ObservableCollection<String>();
            this.Words.CollectionChanged += Words_CollectionChanged;
        }
        public WordList(String filename, bool Readonly = false, bool Autosave = false)
        {
            this.Readonly = Readonly;
            this.Autosave = Autosave;
            this.Filename = filename;
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
            this.Filename = null;
            this.Words = Words;
            this.Words.CollectionChanged += Words_CollectionChanged;
        }


        private void Words_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
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
            if (this.Filename == null)
            {
                throw new Exception("Cannot save without a filename being set");
            }
            SaveToFile(this.Filename);
        }

        public void SaveToFile(String filename, bool persistFilename = true)
        {
            // Ensure folder exists
            FileInfo fi = new FileInfo(filename);
            Directory.CreateDirectory(fi.DirectoryName);
            // If persistFilename is true, this is our new filename for the wordlist, even if we already have one
            if (persistFilename) { this.Filename = filename; }
            TextWriter tw = new StreamWriter(filename);
            IEnumerable<String> sorted = (from String s in Words orderby s select s);
            foreach (String s in sorted)
            {
                tw.WriteLine(s);
            }
            tw.Close();
        }

        public void LoadFromFile(String filename, bool persistFilename = true, bool Autosave = false)
        {
            if (!File.Exists(filename))
            {
                throw new Exception(String.Format("File {0} does not exist", filename));
            }
            // Set autosave to false so we don't overwrite any existing linked wordlist on load
            this.Autosave = false;
            // Do not add handler for CollectionChanged event, as it should already be present from constructor
            this.Words.Clear();
            this.Words = new ObservableCollection<String>(File.ReadAllLines(filename));
            // If persistFilename is true, this is our new filename for the wordlist, even if we already have one
            if (persistFilename) { this.Filename = filename; }
            this.Autosave = Autosave;
        }
    }
}
