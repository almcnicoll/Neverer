using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using Serial = System.Xml.Serialization;

namespace Neverer.UtilityClass
{
    public enum DictType
    {
        Default = 0,
        Custom = 1
    }

    public enum DictFileType
    {
        XML = 0,
        Plaintext = 1
    }

    class CrosswordDictionary : SerializableDictionary<String, List<String>>
    {
        public String fileName { get; set; }

        public CrosswordDictionary()
        {

        }
        public CrosswordDictionary(String fileName)
        {
            this.fileName = fileName;
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
            CrosswordDictionary cd = new CrosswordDictionary(fileName);
            try
            {
                String[] allWords = File.ReadAllLines(fileName);
                foreach (String word in allWords)
                {
                    cd.Add(word, new List<String>());
                }
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
                string xmlString = xmlDoc.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(Crossword);

                    Serial.XmlSerializer serializer = new Serial.XmlSerializer(outType);
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
                return null;
            }
        }
    }
}
