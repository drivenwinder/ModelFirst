using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EAP.Collections
{
    [Serializable]
    [DataContract]
    public class ValueTextPair : ICloneable, IComparable<ValueTextPair>, IEquatable<ValueTextPair>
    {
        [DataMember]
        public object Value { get; set; }
        [DataMember]
        public string Text { get; set; }

        public ValueTextPair() { }

        public ValueTextPair(object value, string text)
        {
            Value = value;
            Text = text;
        }

        public override string ToString()
        {
            if (Text == null)
                return string.Empty;
            return Text;
        }

        public override bool Equals(object obj)
        {
            ValueTextPair o = obj as ValueTextPair;
            if (object.Equals(o, null))
                return false;
            return Equals(o);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public ValueTextPair Clone()
        {
            return new ValueTextPair(Value, Text);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public int CompareTo(ValueTextPair other)
        {
            return ToString().CompareTo(other.Text);
        }

        public bool Equals(ValueTextPair other)
        {
            return Value == other.Value && Text == other.Text;
        }

        public static bool operator ==(ValueTextPair a, ValueTextPair b)
        {
            bool isNull1 = object.Equals(a, null);
            bool isNull2 = object.Equals(b, null);
            if (isNull1 && isNull2)
                return true;
            if (isNull1 || isNull2)
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(ValueTextPair a, ValueTextPair b)
        {
            return !(a == b);
        }
    }
}
