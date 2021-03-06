﻿namespace System.Collections.Generic
{
    using System;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Linq;

    // This code copyright (c) Dacris Software Inc. under MIT license

    [Serializable()]
    //[XmlInclude(typeof(Neverer.UtilityClass.CrosswordDictionary))]
    public class SerializableDictionary<TKey, TVal> : Dictionary<TKey, TVal>, IXmlSerializable, ISerializable //where TVal : class
    {
        #region Constants
        private const string DictionaryNodeName = "Dictionary";
        private const string ItemNodeName = "Item";
        private const string KeyNodeName = "Key";
        private const string ValueNodeName = "Value";
        #endregion

        #region Constructors
        public SerializableDictionary()
        {

        }
        public SerializableDictionary(bool Readonly)
        {
            this.Readonly = Readonly;
        }
        public SerializableDictionary(IDictionary<TKey, TVal> dictionary, bool Readonly = false)
                    : base(dictionary)
        {
            this.Readonly = Readonly;
        }
        public SerializableDictionary(IEqualityComparer<TKey> comparer, bool Readonly = false)
            : base(comparer)
        {
            this.Readonly = Readonly;
        }
        public SerializableDictionary(int capacity, bool Readonly = false)
            : base(capacity)
        {
            this.Readonly = Readonly;
        }
        public SerializableDictionary(IDictionary<TKey, TVal> dictionary, IEqualityComparer<TKey> comparer, bool Readonly = false)
            : base(dictionary, comparer)
        {
            this.Readonly = Readonly;
        }
        public SerializableDictionary(int capacity, IEqualityComparer<TKey> comparer, bool Readonly = false)
            : base(capacity, comparer)
        {
            this.Readonly = Readonly;
        }
        #endregion

        #region ISerializable Members
        protected SerializableDictionary(SerializationInfo info, StreamingContext context)
        {
            int itemCount = info.GetInt32("ItemCount");
            for (int i = 0; i < itemCount; i++)
            {
                KeyValuePair<TKey, TVal> kvp = (KeyValuePair<TKey, TVal>)info.GetValue(String.Format("Item{0}", i), typeof(KeyValuePair<TKey, TVal>));
                this.Add(kvp.Key, kvp.Value);
            }
        }
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ItemCount", this.Count);
            int itemIdx = 0;
            foreach (KeyValuePair<TKey, TVal> kvp in this)
            {
                info.AddValue(String.Format("Item{0}", itemIdx), kvp, typeof(KeyValuePair<TKey, TVal>));
                itemIdx++;
            }
        }
        #endregion

        #region IXmlSerializable Members
        void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
        {
            //writer.WriteStartElement(DictionaryNodeName);
            foreach (KeyValuePair<TKey, TVal> kvp in this)
            {
                writer.WriteStartElement(ItemNodeName);
                writer.WriteStartElement(KeyNodeName);
                KeySerializer.Serialize(writer, kvp.Key);
                writer.WriteEndElement();
                writer.WriteStartElement(ValueNodeName);
                ValueSerializer.Serialize(writer, kvp.Value);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
            //writer.WriteEndElement();
        }
        void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.IsEmptyElement)
            {
                return;
            }

            // Move past container
            if (!reader.Read())
            {
                throw new XmlException("Error in Deserialization of Dictionary");
            }

            //reader.ReadStartElement(DictionaryNodeName);
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement(ItemNodeName);
                reader.ReadStartElement(KeyNodeName);
                TKey key = (TKey)KeySerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement(ValueNodeName);
                TVal value = (TVal)ValueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadEndElement();
                if (this.ContainsKey(key))
                {
                    this[key] = value;
                }
                else
                {
                    this.Add(key, value);
                }
                reader.MoveToContent();
            }
            //reader.ReadEndElement();

            reader.ReadEndElement(); // Read End Element to close Read of containing node
        }
        System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }
        #endregion

        #region Public properties
        public bool Readonly { get; set; }
        #endregion

        #region Private Properties
        protected XmlSerializer ValueSerializer
        {
            get
            {
                if (valueSerializer == null)
                {
                    try
                    {
                        valueSerializer = new XmlSerializer(typeof(TVal));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return valueSerializer;
            }
        }
        private XmlSerializer KeySerializer
        {
            get
            {
                if (keySerializer == null)
                {
                    keySerializer = new XmlSerializer(typeof(TKey));
                }
                return keySerializer;
            }
        }
        #endregion

        #region Private Members
        private XmlSerializer keySerializer = null;
        private XmlSerializer valueSerializer = null;
        #endregion

        #region Al-Extensions
        public void AddIfNotExists(TKey key, TVal valueIfNotExists)
        {
            if (this.ContainsKey(key))
            {
                return;
            }
            this.Add(key, valueIfNotExists);
        }
        public TVal RetrieveIfKeyExists(TKey key, TVal valueIfNotExists = default(TVal))
        {
            if (this.ContainsKey(key))
            {
                return this[key];
            }
            else
            {
                return valueIfNotExists;
            }
        }
        #endregion
    }
}
