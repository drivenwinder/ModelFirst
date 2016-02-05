using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EAP.Collections
{
    [Serializable]
    public class ValueTextList : List<ValueTextPair>
    {
        public void Add(object key, string text)
        {
            Add(new ValueTextPair(key, text));
        }

        public ValueTextList()
        {
        }

        public ValueTextList(IEnumerable<ValueTextPair> collection)
            : base(collection)
        {
        }

        public ValueTextList(int capacity)
            : base(capacity)
        {
        }
    }
}
