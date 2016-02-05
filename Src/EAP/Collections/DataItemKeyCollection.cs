using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace EAP.Collections
{
    [Serializable]
    public class DataItemKeyCollection : List<DataItemKey>
    {
        public DataItemKeyCollection()
        {
        }

        public DataItemKeyCollection(IEnumerable<DataItemKey> collection)
            : base(collection)
        {
        }

        public DataItemKeyCollection(int capacity)
            : base(capacity)
        {
        }
    }
}