using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace EAP.Collections
{
    [Serializable]
    public class DataItemKey
    {
        public DataItemKey()
        {
            Values = new Dictionary<string, object>();
        }

        public DataItemKey(IOrderedDictionary values) :
            this()
        {
            foreach (string k in values.Keys)
            {
                Values.Add(k, values[k]);
            }
        }

        public Dictionary<string, object> Values { get; set; }

        public object Value
        {
            get
            {
                foreach (KeyValuePair<string, object> p in Values)
                {
                    return p.Value;
                }
                return null;
            }
        }

        public object this[string key]
        {
            get
            {
                return Values[key];
            }
            set
            {
                if (Values.ContainsKey(key))
                    Values[key] = value;
                else
                    Values.Add(key, value);
            }
        }

        public override bool Equals(object obj)
        {
            DataItemKey key = obj as DataItemKey;
            if (key == null || key.Values == null || Values == null) return false;
            foreach (KeyValuePair<string, object> p in key.Values)
            {
                if (!Values.ContainsKey(p.Key) || !object.Equals(Values[p.Key], p.Value))
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return Values.GetHashCode();
        }
    }
}
