using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Serial = System.Xml.Serialization;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using Neverer.UtilityClass;

namespace Neverer
{
    [Serializable]
    public class SettingValue
    {
        public bool Serialized { get; set; }
        public String Value { get; set; }

        // NB Structure below allows for ValueType to be serializable
        // Normally, as Type is based on System.RuntimeType (not public) this will
        //  fail with "System.RuntimeType is inaccessible due to its protection level".
        // ValueType is essentially a wrapper for __ValueType, which also updates
        //  ValueTypeName as needed
        [Serial.XmlIgnore]
        private Type __ValueType;
        [Serial.XmlIgnore]
        public Type ValueType
        {
            get { return __ValueType; }
            set
            {
                __ValueType = value;
                ValueTypeName = value.AssemblyQualifiedName;
            }
        }
        public string ValueTypeName
        {
            get { return __ValueType.AssemblyQualifiedName; }
            set { __ValueType = Type.GetType(value); }
        }

        [Serial.XmlIgnore]
        public Exception LastError { get; set; }

        public SettingValue()
        {
            Serialized = false;
            Value = "";
            ValueType = typeof(String);
            LastError = null;
        }

        public SettingValue(bool Serialized, String Value, Type ValueType)
        {
            this.Serialized = Serialized;
            this.Value = Value;
            this.ValueType = ValueType;
        }

        public SettingValue(Object Value)
        {
            switch (Type.GetTypeCode(Value.GetType()))
            {
                case TypeCode.String:
                    this.Value = (String)Value;
                    this.Serialized = false;
                    this.ValueType = typeof(String);
                    break;
                case TypeCode.Int32:
                    this.Value = Value.ToString();
                    this.Serialized = false;
                    this.ValueType = typeof(Int32);
                    break;
                case TypeCode.Double:
                    this.Value = Value.ToString();
                    this.Serialized = false;
                    this.ValueType = typeof(Double);
                    break;
                default:
                    try
                    {
                        //this.Value = Value.ToString();
                        //Serial.XmlSerializer xs = new Serial.XmlSerializer(Value.GetType());
                        //xs.Serialize(, Value);
                        this.Serialized = true;
                        this.ValueType = Value.GetType();
                        this.Value = Convert.ChangeType(Value, this.ValueType).SerializeObject();
                    }
                    catch (Exception ex)
                    {
                        LastError = ex;
                    }
                    break;
            }
        }
    }

    [Serializable]
    public class Settings : SerializableDictionary<String, SettingValue>
    {
        /*[System.Xml.Serialization.XmlElement]
        public FlexStack<String> RecentFiles { get; set; }*/
        public const String keyRecentFiles = "RecentFiles";

        public Settings()
        {
            this.DictionaryFiles = new SerializableDictionary<DictType, List<String>>();
            this.DictionaryFiles.Add(DictType.Default, new List<String>());
            this.Set(Settings.keyRecentFiles, new FlexStack<String>());
        }

        [DllImport("shell32.dll", CharSet = CharSet.Ansi)]
        private static extern void SHAddToRecentDocs(int flag, [In] string path);

        public event EventHandler FileListChanged;

        public T Get<T>(String key)
        {
            if (this.ContainsKey(key))
            {
                SettingValue sv = this[key];
                if (typeof(T) != sv.ValueType)
                {
                    throw new Exception(String.Format("Could not retrieve Setting \"{0}\" (type {1}) as type {2}", key, sv.ValueType.ToString(), typeof(T).ToString()));
                }
                if (sv.Serialized)
                {
                    return (T)sv.Value.DeserializeObject<T>();
                }
                else
                {
                    return (T)Convert.ChangeType(sv.Value, typeof(T));
                }
            }
            else
            {
                return default(T);
            }
        }

        public SettingValue Set(String key, Object value)
        {
            if (this.ContainsKey(key))
            {
                // Edit
                this[key] = new SettingValue(value);
            }
            else
            {
                // Add
                this.Add(key, new SettingValue(value));
            }
            return this[key];
        }

        [Serial.XmlElement("DictionaryFiles")]
        public SerializableDictionary<DictType,List<String>> DictionaryFiles { get; set; }

        public FlexStack<String> RecentFiles
        {
            get
            {
                return this.Get<FlexStack<String>>(Settings.keyRecentFiles);
            }
            private set
            {
                this.Set(Settings.keyRecentFiles, value);
                FileListChanged(this, new EventArgs());
            }
        }

        public String DefaultFolder
        {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                    , Assembly.GetExecutingAssembly().GetName().Name
                );
            }
        }

        private String DefaultFilename
        {
            get
            {
                String filePath = DefaultFolder;
                String fileName = Assembly.GetExecutingAssembly().GetName().Name + ".settings.xml";
                return Path.Combine(filePath, fileName);
            }
        }

        public Exception Save(String fileName = null)
        {
            if (fileName == null) { fileName = DefaultFilename; }
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
        public static Settings Load(String fileName = null)
        {
            if (fileName == null) { fileName = (new Settings()).DefaultFilename; }
            if (!File.Exists(fileName))
            {
                Settings ss = new Settings();
                ss.Save();
            }
            Settings s = ExtensionMethods.LoadFromXML<Settings>(fileName);
            return s;
        }

        public void AddToFileList(String path)
        {
            FlexStack<String> rfTmp = RecentFiles;
            if (rfTmp.Contains(path))
            {
                rfTmp.Remove(path);
            }
            rfTmp.Push(path);
            RecentFiles = rfTmp;
            this.Save();
            FileListChanged(this, new EventArgs());
            // First argument is a UINT flag from "SHARD enumeration"
            // Value 2 indicates null-terminated ANSI string of full path incl filename
            SHAddToRecentDocs(2, path);
        }

        // TODO - autosave of currently edited crossword, populated on timer and removed every time crossword is saved, autoloaded on load when present
    }
}
