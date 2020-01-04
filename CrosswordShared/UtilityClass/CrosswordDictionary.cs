using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Neverer.UtilityClass
{
    [Serializable()]
    public enum DictType
    {
        Default = 0,
        Custom = 1,
        Remote = 2
    }

    public enum DictFileType
    {
        XML = 0,
        Plaintext = 1
    }

    [Serializable()]
    //[XmlType(TypeName = "Neverer.UtilityClass.CrosswordDictionary")]
    public class CrosswordDictionary //: SerializableDictionary<String, List<String>>
    {
        public const String importSuffix = ".imported.dic";

        public String fileName { get; set; }
        public bool enabled { get; set; }
        public SerializableDictionary<String, List<String>> entries { get; set; }
        private DateTime? __lastUpdated = null;
        public String dictionaryName { get; set; }

        public DateTime lastUpdated
        {
            get
            {
                if (!__lastUpdated.HasValue) { __lastUpdated = DateTime.Now; }
                return __lastUpdated.Value;
            }
            set
            {
                __lastUpdated = value;
            }
        }

        public CrosswordDictionary()
        {
            SetDefaultValues();
        }
        public CrosswordDictionary(String fileName, DictFileType dictFileType = DictFileType.XML)
        {
            // NB - this constructor does not load a dictionary - it creates a blank one with a filename
            // To load a dictionary, use the static loader defined below
            this.fileName = fileName;
            SetDefaultValues();
        }
        private void SetDefaultValues()
        {
            enabled = true;
            lastUpdated = DateTime.Now;
            entries = new SerializableDictionary<String, List<String>>();
            if ((fileName != null) && (fileName != ""))
            {
                dictionaryName = Path.GetFileNameWithoutExtension(fileName);
                Save(); // In case there's any new class changes since last save
            }
        }

        public override String ToString()
        {
            return ((dictionaryName==null)? "Untitled" : dictionaryName);
        }

        public static CrosswordDictionary Load(String fileName, DictFileType fileType = DictFileType.XML)
        {
            switch (fileType)
            {
                case DictFileType.XML:
                    return CrosswordDictionary.LoadXML(fileName);
                case DictFileType.Plaintext:
                    return CrosswordDictionary.LoadText(fileName);
                default:
                    // TODO - throw some kind of error here
                    return null;
            }
        }

        private static CrosswordDictionary LoadText(String fileName)
        {
            CrosswordDictionary cd = new CrosswordDictionary(fileName + CrosswordDictionary.importSuffix);
            try
            {
                String[] allWords = File.ReadAllLines(fileName);
                foreach (String word in allWords)
                {
                    cd.entries.Add(word, new List<String>());
                }
                cd.Save();
                return cd;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error opening dictionary {0}: {1}", fileName, ex.Message));
                return null;
            }
        }

        private static CrosswordDictionary LoadXML(String fileName)
        {
            CrosswordDictionary cd;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Path.Combine(fileName));
                String xmlString = xmlDoc.OuterXml;

                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "CrosswordDictionary";
                xRoot.IsNullable = false;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(CrosswordDictionary);

                    XmlSerializer serializer = new XmlSerializer(outType, xRoot);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        cd = (CrosswordDictionary)serializer.Deserialize(reader);
                        cd.fileName = fileName;
                        reader.Close();
                    }

                    read.Close();
                }
                return cd;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error opening dictionary {0}: {1}", fileName, ex.Message));
                MessageBox.Show(String.Format("InnerException: {0}", ex.InnerException.Message));
                return null;
            }
        }

        public void Save()
        {
            if (fileName == null) { throw new Exception("No filename specified when saving dictionary."); }
            __lastUpdated = DateTime.Now;
            this.SaveToXML(fileName);
        }
    }
}
