using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Jpp.Common
{
    /// <summary>
    /// Basic implementations of Dictionary that will work with the xml serializer
    /// </summary>
    /// <typeparam name="TKey">Type to act as key</typeparam>
    /// <typeparam name="TValue">Type to act as value</typeparam>
    public class SerializableDictionary<TKey, TValue> : IXmlSerializable, IDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _internal;

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get { return Values; }
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get { return Keys; }
        }

        public Dictionary<TKey, TValue>.KeyCollection Keys
        {
            get
            {
                return _internal.Keys;
            }
        }

        public Dictionary<TKey, TValue>.ValueCollection Values
        {
            get { return _internal.Values; }
        }

        public int Count
        {
            get { return _internal.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public TValue this[TKey t]
        {
            get { return _internal[t]; }
            set { _internal[t] = value; }
        }

        public SerializableDictionary()
        {
            _internal = new Dictionary<TKey, TValue>();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public bool ContainsKey(TKey key)
        {
            return _internal.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            return _internal.Remove(key);
        }

        public void Add(TKey key, TValue value)
        {
            _internal[key] = value;
        }


        public void ReadXml(XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");
                reader.ReadStartElement("key");
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement("value");
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }

            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (TKey key in Keys)
            {
                writer.WriteStartElement("item");
                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _internal.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _internal.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _internal.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _internal.ToArray().CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return _internal.Remove(item.Key);
        }
    }
}