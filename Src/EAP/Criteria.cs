using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;

namespace EAP
{
    [Serializable]
    public enum QueryType
    {
        Equal,
        NotEqual,
        Greater,
        Less,
        GreaterOrEqual,
        LessOrEqual,
        Like,
        NotLike,
        EqualCI,//Case Insensitive
        NotEqualCI,//Case Insensitive
    }
    [Serializable]
    public enum QueryGroup { And, Or }

    public interface ICriteriaVisitor
    {
        object Visit(BinaryOperator theOperator);
        object Visit(InOperator theOperator);
        object Visit(GroupOperator theOperator);
        object Visit(OperandValue theOperand);
    }
    public interface IClientCriteriaVisitor : ICriteriaVisitor
    {
        object Visit(OperandProperty theOperand);
    }
    [Serializable]
    public sealed class QueryCriteriaCollection : List<QueryCriteria>
    {
        public QueryCriteriaCollection() { }
        public override bool Equals(object obj)
        {
            QueryCriteriaCollection another = obj as QueryCriteriaCollection;
            if (another == null)
                return false;
            if (this.Count != another.Count)
                return false;
            for (int i = 0; i < this.Count; ++i)
            {
                if (!Equals(this[i], another[i]))
                    return false;
            }
            return true;
        }
        public override int GetHashCode()
        {
            int result = 0;
            foreach (object o in this)
            {
                result ^= o.GetHashCode();
            }
            return result;
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (QueryCriteria op in this)
            {
                if (result.Length > 0)
                    result.Append("; ");
                result.Append(op.ToString());
            }
            return result.ToString();
        }
    }
    [Serializable]
    [XmlInclude(typeof(BinaryOperator))]
    [XmlInclude(typeof(InOperator))]
    [XmlInclude(typeof(GroupOperator))]
    [XmlInclude(typeof(OperandValue))]
    [XmlInclude(typeof(OperandProperty))]
    public abstract class QueryCriteria : ICloneable
    {
        public static QueryCriteria Empty { get { return new GroupOperator(QueryGroup.Or); } }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public abstract object Accept(ICriteriaVisitor visitor);

        public override string ToString()
        {
            return CriteriaToBasicStyleParameterlessProcessor.ToString(this);
        }
        public static QueryCriteria And(QueryCriteria left, QueryCriteria right)
        {
            return GroupOperator.Combine(QueryGroup.And, left, right);
        }
        public static QueryCriteria Or(QueryCriteria left, QueryCriteria right)
        {
            return GroupOperator.Combine(QueryGroup.Or, left, right);
        }
        public static QueryCriteria And(params QueryCriteria[] operands)
        {
            return GroupOperator.Combine(QueryGroup.And, operands);
        }
        public static QueryCriteria Or(params QueryCriteria[] operands)
        {
            return GroupOperator.Combine(QueryGroup.Or, operands);
        }
        public static implicit operator QueryCriteria(Boolean val)
        {
            return (OperandValue)val;
        }
        public static implicit operator QueryCriteria(Byte val)
        {
            return (OperandValue)val;
        }
        public static implicit operator QueryCriteria(Char val)
        {
            return (OperandValue)val;
        }
        public static implicit operator QueryCriteria(Decimal val)
        {
            return (OperandValue)val;
        }
        public static implicit operator QueryCriteria(Double val)
        {
            return (OperandValue)val;
        }
        public static implicit operator QueryCriteria(Single val)
        {
            return (OperandValue)val;
        }
        public static implicit operator QueryCriteria(Int16 val)
        {
            return (OperandValue)val;
        }
        public static implicit operator QueryCriteria(Int32 val)
        {
            return (OperandValue)val;
        }
        public static implicit operator QueryCriteria(Int64 val)
        {
            return (OperandValue)val;
        }
        public static implicit operator QueryCriteria(Guid val)
        {
            return (OperandValue)val;
        }
        public static implicit operator QueryCriteria(String val)
        {
            return (OperandValue)val;
        }
        public static implicit operator QueryCriteria(DateTime val)
        {
            return (OperandValue)val;
        }
        public static implicit operator QueryCriteria(TimeSpan val)
        {
            return (OperandValue)val;
        }
        public static implicit operator QueryCriteria(Byte[] val)
        {
            return (OperandValue)val;
        }
        object ICloneable.Clone()
        {
            return CloneCommon();
        }
        protected abstract QueryCriteria CloneCommon();
        public static QueryCriteria Clone(QueryCriteria origin)
        {
            if (ReferenceEquals(origin, null))
                return null;
            return origin.CloneCommon();
        }
        public static OperandProperty Clone(OperandProperty origin)
        {
            if (ReferenceEquals(origin, null))
                return null;
            return origin.Clone();
        }
        protected static ICollection<QueryCriteria> Clone(ICollection origins)
        {
            List<QueryCriteria> result = new List<QueryCriteria>(origins.Count);
            foreach (QueryCriteria op in origins)
                result.Add(Clone(op));
            return result;
        }
    }
    [Serializable]
    public class OperandProperty : QueryCriteria
    {
        string propertyName;
        public override object Accept(ICriteriaVisitor visitor)
        {
            IClientCriteriaVisitor clientVisitor = (IClientCriteriaVisitor)visitor;
            return clientVisitor.Visit(this);
        }
        public OperandProperty() : this(string.Empty) { }
        public OperandProperty(string propertyName)
        {
            this.PropertyName = propertyName;
        }
        [XmlAttribute]
        public string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!object.ReferenceEquals(this.GetType(), obj.GetType()))
                return false;
            return object.Equals(this.PropertyName, ((OperandProperty)obj).PropertyName);
        }
        public override int GetHashCode()
        {
            return PropertyName != null ? PropertyName.GetHashCode() : -1;
        }
        protected override QueryCriteria CloneCommon()
        {
            return Clone();
        }
        public OperandProperty Clone()
        {
            return new OperandProperty(this.PropertyName);
        }
    }
    [XmlType("DBNull")]
    public class NullValue
    {
        public static NullValue Value = new NullValue();
    }
    [Serializable]
    public class OperandValue : QueryCriteria
    {
        object value;
        public OperandValue(object value)
        {
            this.value = value;
        }
        public OperandValue() : this(null) { }
        public override object Accept(ICriteriaVisitor visitor)
        {
            return visitor.Visit(this);
        }
        internal static string FormatString(string value)
        {
            int count = value.Length;
            StringBuilder res = new StringBuilder(value.Length);
            for (int i = 0; i < count; i++)
            {
                char c = value[i];
                switch (c)
                {
                    case '\n': res.Append("\\n"); break;
                    case '\r': res.Append("\\r"); break;
                    case ' ': res.Append("\\s"); break;
                    case '\\': res.Append("\\\\"); break;
                    case '\0': res.Append("\\0"); break;
                    default: res.Append(c); break;
                }
            }
            return res.ToString();
        }
        internal static string ReformatString(string value)
        {
            int count = value.Length;
            StringBuilder res = new StringBuilder(value.Length);
            for (int i = 0; i < count; i++)
            {
                char c = value[i];
                if (c == '\\')
                {
                    i++;
                    if (i < count)
                    {
                        c = value[i];
                        switch (c)
                        {
                            case 'n': res.Append('\n'); break;
                            case 'r': res.Append('\r'); break;
                            case 's': res.Append(' '); break;
                            case '\\': res.Append('\\'); break;
                            case '0': res.Append('\0'); break;
                            default: res.Append(c); break;
                        }
                    }
                }
                else
                {
                    res.Append(c);
                }
            }
            return res.ToString();
        }
        [XmlElement(typeof(System.Boolean))]
        [XmlElement(typeof(System.Byte))]
        [XmlElement(typeof(System.SByte))]
        [XmlElement(typeof(System.Char))]
        [XmlElement(typeof(System.Decimal))]
        [XmlElement(typeof(System.Double))]
        [XmlElement(typeof(System.Single))]
        [XmlElement(typeof(System.Int32))]
        [XmlElement(typeof(System.UInt32))]
        [XmlElement(typeof(System.Int64))]
        [XmlElement(typeof(System.UInt64))]
        [XmlElement(typeof(System.Int16))]
        [XmlElement(typeof(System.UInt16))]
        [XmlElement(typeof(System.Guid))]
        [XmlElement(typeof(String))]
        [XmlElement(typeof(DateTime))]
        [XmlElement(typeof(TimeSpan))]
        [XmlElement(typeof(NullValue))]
        [XmlElement(typeof(System.Byte[]))]
        public object XmlValue
        {
            get
            {
                if (Value == null)
                    return NullValue.Value;
                string stringVal = Value as string;
                return stringVal != null ? FormatString(stringVal) : Value;
            }
            set
            {
                if (value is NullValue)
                    Value = null;
                else
                {
                    string stringVal = value as string;
                    Value = stringVal != null ? ReformatString(stringVal) : value;
                }
            }
        }
        [XmlIgnore]
        public virtual object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!object.ReferenceEquals(this.GetType(), obj.GetType()))
                return false;
            return object.Equals(this.Value, ((OperandValue)obj).Value);
        }
        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : -1;
        }
        public static implicit operator OperandValue(Boolean val)
        {
            return new OperandValue(val);
        }
        public static implicit operator OperandValue(Byte val)
        {
            return new OperandValue(val);
        }
        public static implicit operator OperandValue(Char val)
        {
            return new OperandValue(val);
        }
        public static implicit operator OperandValue(Decimal val)
        {
            return new OperandValue(val);
        }
        public static implicit operator OperandValue(Double val)
        {
            return new OperandValue(val);
        }
        public static implicit operator OperandValue(Single val)
        {
            return new OperandValue(val);
        }
        public static implicit operator OperandValue(Int16 val)
        {
            return new OperandValue(val);
        }
        public static implicit operator OperandValue(Int32 val)
        {
            return new OperandValue(val);
        }
        public static implicit operator OperandValue(Int64 val)
        {
            return new OperandValue(val);
        }
        public static implicit operator OperandValue(Guid val)
        {
            return new OperandValue(val);
        }
        public static implicit operator OperandValue(String val)
        {
            return new OperandValue(val);
        }
        public static implicit operator OperandValue(DateTime val)
        {
            return new OperandValue(val);
        }
        public static implicit operator OperandValue(TimeSpan val)
        {
            return new OperandValue(val);
        }
        public static implicit operator OperandValue(Byte[] val)
        {
            return new OperandValue(val);
        }
        protected override QueryCriteria CloneCommon()
        {
            return Clone();
        }
        public OperandValue Clone()
        {
            ICloneable cloneableValue = Value as ICloneable;
            if (cloneableValue != null)
                return new OperandValue(cloneableValue.Clone());
            return new OperandValue(Value);
        }
    }
    [Serializable]
    public class InOperator : QueryCriteria
    {
        public override object Accept(ICriteriaVisitor visitor)
        {
            return visitor.Visit(this);
        }
        QueryCriteria leftOperand;
        public QueryCriteria LeftOperand
        {
            get
            {
                return leftOperand;
            }
            set
            {
                leftOperand = value;
            }
        }
        QueryCriteriaCollection operands = new QueryCriteriaCollection();
        [XmlArrayItem(typeof(QueryCriteria))]
        public virtual QueryCriteriaCollection Operands { get { return operands; } }
        public InOperator() : this(null) { }
        public InOperator(string propertyName, ICollection values)
            : this(new OperandProperty(propertyName))
        {
            List<QueryCriteria> list = new List<QueryCriteria>();
            foreach (object o in values)
            {
                list.Add(new OperandValue(o));
            }
            operands.AddRange(list);
        }
        public InOperator(QueryCriteria leftOperand, params QueryCriteria[] operands) : this(leftOperand, (IEnumerable<QueryCriteria>)operands) { }
        public InOperator(QueryCriteria leftOperand, IEnumerable<QueryCriteria> operands)
        {
            this.leftOperand = leftOperand;
            if (operands != null)
                this.operands.AddRange(operands);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!object.ReferenceEquals(this.GetType(), obj.GetType()))
                return false;
            if (this.Operands.Count != ((InOperator)obj).Operands.Count)
                return false;
            if (!object.Equals(this.LeftOperand, ((InOperator)obj).LeftOperand))
                return false;
            for (int i = 0; i < this.Operands.Count; ++i)
            {
                if (!object.Equals(this.Operands[i], ((InOperator)obj).Operands[i]))
                    return false;
            }
            return true;
        }
        public override int GetHashCode()
        {
            int hash = ReferenceEquals(LeftOperand, null) ? -1 : LeftOperand.GetHashCode();
            foreach (object obj in Operands)
                if (obj != null)
                    hash ^= obj.GetHashCode();
            return hash;
        }
        protected override QueryCriteria CloneCommon()
        {
            return Clone();
        }
        public InOperator Clone()
        {
            return new InOperator(Clone(LeftOperand), Clone(Operands));
        }
    }
    [Serializable]
    public class BinaryOperator : QueryCriteria
    {
        public override object Accept(ICriteriaVisitor visitor)
        {
            return visitor.Visit(this);
        }
        [XmlAttribute]
        public QueryType OperatorType;
        public BinaryOperator() : this((QueryCriteria)null, (QueryCriteria)null, QueryType.Equal) { }
        public BinaryOperator(QueryCriteria opLeft, QueryCriteria opRight, QueryType type)
        {
            this.LeftOperand = opLeft;
            this.RightOperand = opRight;
            this.OperatorType = type;
        }
        public QueryCriteria LeftOperand;
        public QueryCriteria RightOperand;
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!object.ReferenceEquals(this.GetType(), obj.GetType()))
                return false;
            return object.Equals(this.OperatorType, ((BinaryOperator)obj).OperatorType) &&
                object.Equals(this.LeftOperand, ((BinaryOperator)obj).LeftOperand) &&
                object.Equals(this.RightOperand, ((BinaryOperator)obj).RightOperand);
        }
        public override int GetHashCode()
        {
            return (ReferenceEquals(LeftOperand, null) ? -1 : LeftOperand.GetHashCode()) ^ (ReferenceEquals(RightOperand, null) ? -1 : RightOperand.GetHashCode());
        }
        public BinaryOperator(string propertyName, Boolean value, QueryType type)
            : this(new OperandProperty(propertyName), (OperandValue)value, type) { }
        public BinaryOperator(string propertyName, Byte value, QueryType type)
            : this(new OperandProperty(propertyName), value, type) { }
        public BinaryOperator(string propertyName, Char value, QueryType type)
            : this(new OperandProperty(propertyName), value, type) { }
        public BinaryOperator(string propertyName, Decimal value, QueryType type)
            : this(new OperandProperty(propertyName), value, type) { }
        public BinaryOperator(string propertyName, Double value, QueryType type)
            : this(new OperandProperty(propertyName), value, type) { }
        public BinaryOperator(string propertyName, Single value, QueryType type)
            : this(new OperandProperty(propertyName), value, type) { }
        public BinaryOperator(string propertyName, Int32 value, QueryType type)
            : this(new OperandProperty(propertyName), value, type) { }
        public BinaryOperator(string propertyName, Int64 value, QueryType type)
            : this(new OperandProperty(propertyName), value, type) { }
        public BinaryOperator(string propertyName, Int16 value, QueryType type)
            : this(new OperandProperty(propertyName), value, type) { }
        public BinaryOperator(string propertyName, Guid value, QueryType type)
            : this(new OperandProperty(propertyName), value, type) { }
        public BinaryOperator(string propertyName, String value, QueryType type)
            : this(new OperandProperty(propertyName), value, type) { }
        public BinaryOperator(string propertyName, DateTime value, QueryType type)
            : this(new OperandProperty(propertyName), value, type) { }
        public BinaryOperator(string propertyName, TimeSpan value, QueryType type)
            : this(new OperandProperty(propertyName), value, type) { }
        public BinaryOperator(string propertyName, Byte[] value, QueryType type)
            : this(new OperandProperty(propertyName), value, type) { }
        public BinaryOperator(string propertyName, object value, QueryType type)
            : this(new OperandProperty(propertyName), new OperandValue(value), type) { }
        public BinaryOperator(string propertyName, Boolean value)
            : this(propertyName, value, QueryType.Equal) { }
        public BinaryOperator(string propertyName, Byte value)
            : this(propertyName, value, QueryType.Equal) { }
        public BinaryOperator(string propertyName, Char value)
            : this(propertyName, value, QueryType.Equal) { }
        public BinaryOperator(string propertyName, Decimal value)
            : this(propertyName, value, QueryType.Equal) { }
        public BinaryOperator(string propertyName, Double value)
            : this(propertyName, value, QueryType.Equal) { }
        public BinaryOperator(string propertyName, Single value)
            : this(propertyName, value, QueryType.Equal) { }
        public BinaryOperator(string propertyName, Int32 value)
            : this(propertyName, value, QueryType.Equal) { }
        public BinaryOperator(string propertyName, Int64 value)
            : this(propertyName, value, QueryType.Equal) { }
        public BinaryOperator(string propertyName, Int16 value)
            : this(propertyName, value, QueryType.Equal) { }
        public BinaryOperator(string propertyName, Guid value)
            : this(propertyName, value, QueryType.Equal) { }
        public BinaryOperator(string propertyName, String value)
            : this(propertyName, value, QueryType.Equal) { }
        public BinaryOperator(string propertyName, DateTime value)
            : this(propertyName, value, QueryType.Equal) { }
        public BinaryOperator(string propertyName, TimeSpan value)
            : this(propertyName, value, QueryType.Equal) { }
        public BinaryOperator(string propertyName, Byte[] value)
            : this(propertyName, value, QueryType.Equal) { }
        public BinaryOperator(string propertyName, object value)
            : this(propertyName, value, QueryType.Equal) { }
        protected override QueryCriteria CloneCommon()
        {
            return Clone();
        }
        public BinaryOperator Clone()
        {
            return new BinaryOperator(Clone(LeftOperand), Clone(RightOperand), OperatorType);
        }
    }
    [Serializable]
    public sealed class GroupOperator : QueryCriteria
    {
        QueryCriteriaCollection operands = new QueryCriteriaCollection();
        public override object Accept(ICriteriaVisitor visitor)
        {
            return visitor.Visit(this);
        }
        [XmlArrayItem(typeof(QueryCriteria))]
        public QueryCriteriaCollection Operands { get { return operands; } }
        [XmlAttribute]
        public QueryGroup OperatorType;
        public GroupOperator()
            : this(QueryGroup.And)
        {
        }
        public GroupOperator(QueryGroup type, params QueryCriteria[] operands)
            : this(type, (IEnumerable<QueryCriteria>)operands) { }
        public GroupOperator(QueryGroup type, IEnumerable<QueryCriteria> operands)
        {
            this.OperatorType = type;
            this.operands.AddRange(operands);
        }
        public GroupOperator(params QueryCriteria[] operands)
            : this(QueryGroup.And, operands) { }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!object.ReferenceEquals(this.GetType(), obj.GetType()))
                return false;
            if (this.OperatorType != ((GroupOperator)obj).OperatorType)
                return false;
            if (this.Operands.Count != ((GroupOperator)obj).Operands.Count)
                return false;
            for (int i = 0; i < this.Operands.Count; ++i)
            {
                if (!object.Equals(this.Operands[i], ((GroupOperator)obj).Operands[i]))
                    return false;
            }
            return true;
        }
        public override int GetHashCode()
        {
            int hash = -1;
            foreach (object obj in Operands)
                if (obj != null)
                    hash ^= obj.GetHashCode();
            return hash;
        }
        public static QueryCriteria Combine(QueryGroup opType, QueryCriteria left, QueryCriteria right)
        {
            if (ReferenceEquals(left, null))
                return right;
            if (ReferenceEquals(right, null))
                return left;
            GroupOperator groupLeft = left as GroupOperator;
            if (!ReferenceEquals(groupLeft, null) && groupLeft.OperatorType != opType)
                groupLeft = null;
            GroupOperator groupRight = right as GroupOperator;
            if (!ReferenceEquals(groupRight, null) && groupRight.OperatorType != opType)
                groupRight = null;
            List<QueryCriteria> list = new List<QueryCriteria>();
            if (ReferenceEquals(groupLeft, null))
                list.Add(left);
            else
                list.AddRange(groupLeft.Operands);
            if (ReferenceEquals(groupRight, null))
                list.Add(right);
            else
                list.AddRange(groupRight.Operands);
            GroupOperator result = new GroupOperator(opType, list);
            return result;
        }
        public static QueryCriteria Combine(QueryGroup opType, params QueryCriteria[] operands)
        {
            if (operands == null)
                return null;
            QueryCriteria result = null;
            foreach (QueryCriteria operand in operands)
            {
                result = Combine(opType, result, operand);
            }
            return result;
        }
        protected override QueryCriteria CloneCommon()
        {
            return Clone();
        }
        public GroupOperator Clone()
        {
            return new GroupOperator(OperatorType, Clone(Operands));
        }
    }

    #region
    class CriteriaToStringVisitResult
    {
        const string NullCriteriaResult = "()";
        public readonly string Result;
        public readonly CriteriaPriorityClass Priority;
        public CriteriaToStringVisitResult(string result, CriteriaPriorityClass priorityClass)
        {
            this.Result = result;
            this.Priority = priorityClass;
        }
        public CriteriaToStringVisitResult(string result) : this(result, CriteriaPriorityClass.Atom) { }
        public bool IsNull { get { return Result == NullCriteriaResult; } }
        public static readonly CriteriaToStringVisitResult Null = new CriteriaToStringVisitResult(NullCriteriaResult, CriteriaPriorityClass.Atom);
        public string GetEnclosedResult()
        {
            return '(' + Result + ')';
        }
        public string GetEnclosedResultOnGreaterOrEqual(CriteriaPriorityClass basePriority)
        {
            if (this.Priority >= basePriority)
                return GetEnclosedResult();
            else
                return Result;
        }
        public string GetEnclosedResultOnGreater(CriteriaPriorityClass basePriority)
        {
            if (this.Priority > basePriority)
                return GetEnclosedResult();
            else
                return Result;
        }
    }
    enum CriteriaPriorityClass { Atom, Neg, Mul, Add, BinaryNot, BinaryAnd, BinaryXor, BinaryOr, InBetween, CmpGt, CmpEq, IsNull, Not, And, Or }
    abstract class CriteriaToStringBase : ICriteriaVisitor, IClientCriteriaVisitor
    {
        protected string ProcessToCommaDelimitedList(ICollection operands)
        {
            StringBuilder operandsList = new StringBuilder();
            foreach (QueryCriteria op in operands)
            {
                if (operandsList.Length > 0)
                    operandsList.Append(", ");
                operandsList.Append(Process(op).Result);
            }
            return operandsList.ToString();
        }
        protected virtual string GetBetweenText()
        {
            return "Between";
        }
        public abstract string GetOperatorString(QueryType opType);
        public virtual object Visit(BinaryOperator operand)
        {
            string operatorString = GetOperatorString(operand.OperatorType);
            CriteriaPriorityClass priority;
            switch (operand.OperatorType)
            {
                default:
                    throw new InvalidOperationException();
                case QueryType.Greater:
                case QueryType.GreaterOrEqual:
                case QueryType.Less:
                case QueryType.LessOrEqual:
                    priority = CriteriaPriorityClass.CmpGt;
                    break;
                case QueryType.Equal:
                case QueryType.NotEqual:
                case QueryType.EqualCI:
                case QueryType.NotEqualCI:
                case QueryType.Like:
                    priority = CriteriaPriorityClass.CmpEq;
                    break;
            }
            CriteriaToStringVisitResult leftVisitResult = Process(operand.LeftOperand);
            CriteriaToStringVisitResult rightVisitResult = Process(operand.RightOperand);
            string result = leftVisitResult.GetEnclosedResultOnGreater(priority);
            result += ' ';
            result += operatorString;
            result += ' ';
            result += rightVisitResult.GetEnclosedResultOnGreaterOrEqual(priority);
            return new CriteriaToStringVisitResult(result, priority);
        }
        protected virtual string GetIsNotNullText()
        {
            return "Is Not Null";
        }
        protected virtual string GetNotLikeText()
        {
            return "Not Like";
        }
        CriteriaToStringVisitResult CreateNotLike(BinaryOperator likeOp)
        {
            CriteriaToStringVisitResult leftResult = Process(likeOp.LeftOperand);
            CriteriaToStringVisitResult rightResult = Process(likeOp.RightOperand);
            string result = leftResult.GetEnclosedResultOnGreaterOrEqual(CriteriaPriorityClass.CmpEq);
            result += " " + GetNotLikeText() + " ";
            result += rightResult.GetEnclosedResultOnGreaterOrEqual(CriteriaPriorityClass.CmpEq);
            return new CriteriaToStringVisitResult(result, CriteriaPriorityClass.CmpEq);
        }
        protected virtual string GetIsNullText()
        {
            return "Is Null";
        }
        protected virtual string GetInText()
        {
            return "In";
        }
        public virtual object Visit(InOperator operand)
        {
            CriteriaToStringVisitResult result = Process(operand.LeftOperand);
            string strRes = result.GetEnclosedResultOnGreaterOrEqual(CriteriaPriorityClass.InBetween);
            strRes += " " + GetInText() + " (";
            strRes += ProcessToCommaDelimitedList(operand.Operands);
            strRes += ')';
            return new CriteriaToStringVisitResult(strRes, CriteriaPriorityClass.InBetween);
        }
        public abstract string GetOperatorString(QueryGroup opType);
        public virtual object Visit(GroupOperator operand)
        {
            switch (operand.Operands.Count)
            {
                case 0:
                    return CriteriaToStringVisitResult.Null;
                case 1:
                    return Process((QueryCriteria)operand.Operands[0]);
            }
            string delimiter = ' ' + GetOperatorString(operand.OperatorType) + ' ';
            CriteriaPriorityClass basePriority;
            switch (operand.OperatorType)
            {
                case QueryGroup.And:
                    basePriority = CriteriaPriorityClass.And;
                    break;
                case QueryGroup.Or:
                    basePriority = CriteriaPriorityClass.Or;
                    break;
                default:
                    throw new InvalidOperationException();
            }
            CriteriaToStringVisitResult currentResult = Process((QueryCriteria)operand.Operands[0]);
            StringBuilder result = new StringBuilder(currentResult.GetEnclosedResultOnGreater(basePriority));
            for (int i = 1; i < operand.Operands.Count; ++i)
            {
                result.Append(delimiter);
                currentResult = Process((QueryCriteria)operand.Operands[i]);
                result.Append(currentResult.GetEnclosedResultOnGreater(basePriority));
            }
            return new CriteriaToStringVisitResult(result.ToString(), basePriority);
        }
        public abstract object Visit(OperandValue operand);
        public virtual object Visit(OperandProperty operand)
        {
            string result = operand.PropertyName;
            if (result == null)
                result = string.Empty;
            result = "[" + result.Replace("\\", "\\\\").Replace("]", "\\]").Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t") + "]";
            return new CriteriaToStringVisitResult(result);
        }
        protected CriteriaToStringVisitResult Process(QueryCriteria operand)
        {
            if (ReferenceEquals(operand, null))
                return CriteriaToStringVisitResult.Null;
            else
                return (CriteriaToStringVisitResult)operand.Accept(this);
        }
    }
    abstract class CriteriaToStringParameterlessProcessor : CriteriaToStringBase
    {
        const string nullString = "?";
        public static string ValueToString(object value)
        {
            return ValueToString(value, false);
        }
        public static string ValueToString(object value, bool isLegacy)
        {
            if (value == null)
                return nullString;
            TypeCode tc = Type.GetTypeCode(value.GetType());
            switch (tc)
            {
                case TypeCode.DBNull:
                case TypeCode.Empty:
                    return nullString;
                case TypeCode.Boolean:
                    return ((bool)value) ? "True" : "False";
                case TypeCode.Char:
                    return "'" + (char)value + (isLegacy ? "'" : "'c");
                case TypeCode.DateTime:
                    DateTime datetimeValue = (DateTime)value;
                    string dateTimeFormatPattern;
                    if (datetimeValue.TimeOfDay == TimeSpan.Zero)
                    {
                        dateTimeFormatPattern = "yyyy-MM-dd";
                    }
                    else if (datetimeValue.Millisecond == 0)
                    {
                        dateTimeFormatPattern = "yyyy-MM-dd HH:mm:ss";
                    }
                    else
                    {
                        dateTimeFormatPattern = "yyyy-MM-dd HH:mm:ss.fffff";
                    }
                    return "#" + ((DateTime)value).ToString(dateTimeFormatPattern, CultureInfo.InvariantCulture) + "#";
                case TypeCode.String:
                    return "'" + ((string)value).Replace("'", "''") + "'";
                case TypeCode.Decimal:
                    return FixNonFixedText(((Decimal)value).ToString(CultureInfo.InvariantCulture), isLegacy, tc);
                case TypeCode.Double:
                    return FixNonFixedText(((Double)value).ToString("r", CultureInfo.InvariantCulture), isLegacy, tc);
                case TypeCode.Single:
                    return FixNonFixedText(((Single)value).ToString("r", CultureInfo.InvariantCulture), isLegacy, tc);
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    if (value is Enum)
                        return "'" + value.ToString() + "'";
                    return isLegacy ? value.ToString() : value.ToString() + GetSuffix(tc);
                case TypeCode.Object:
                default:
                    if (value is Guid)
                    {
                        return "{" + ((Guid)value).ToString() + "}";
                    }
                    else if (value is TimeSpan)
                    {
                        return "#" + ((TimeSpan)value).ToString() + "#";
                    }
                    else
                    {
                        return "'" + value.ToString() + "'";
                    }
            }
        }
        static string FixNonFixedText(string toFix, bool isLegacy, TypeCode tc)
        {
            if (toFix.IndexOfAny(new char[] { '.', 'e', 'E' }) < 0)
                toFix += ".0";
            if (!isLegacy)
                toFix += GetSuffix(tc);
            return toFix;
        }
        static string GetSuffix(TypeCode tc)
        {
            switch (tc)
            {
                case TypeCode.Decimal:
                    return "m";
                case TypeCode.Single:
                    return "f";
                case TypeCode.Byte:
                    return "b";
                case TypeCode.SByte:
                    return "sb";
                case TypeCode.Int16:
                    return "s";
                case TypeCode.UInt16:
                    return "us";
                case TypeCode.UInt32:
                    return "u";
                case TypeCode.Int64:
                    return "L";
                case TypeCode.UInt64:
                    return "uL";
                default:
                    return string.Empty;
            }
        }
        public static object ValueToCriteriaToStringVisitResult(OperandValue operand)
        {
            return new CriteriaToStringVisitResult(ValueToString(operand.Value));
        }
        public override object Visit(OperandValue operand)
        {
            return ValueToCriteriaToStringVisitResult(operand);
        }
    }
    class CriteriaToBasicStyleParameterlessProcessor : CriteriaToStringParameterlessProcessor
    {
        protected static CriteriaToBasicStyleParameterlessProcessor Instance = new CriteriaToBasicStyleParameterlessProcessor();
        protected CriteriaToBasicStyleParameterlessProcessor() { }
        public static string GetBasicOperatorString(QueryType opType)
        {
            switch (opType)
            {
                default:
                    throw new InvalidOperationException();
                case QueryType.Equal:
                case QueryType.EqualCI:
                    return "=";
                case QueryType.Greater:
                    return ">";
                case QueryType.GreaterOrEqual:
                    return ">=";
                case QueryType.Less:
                    return "<";
                case QueryType.LessOrEqual:
                    return "<=";
                case QueryType.Like:
                    return "Like";
                case QueryType.NotEqual:
                case QueryType.NotEqualCI:
                    return "<>";
            }
        }
        public static string GetBasicOperatorString(QueryGroup opType)
        {
            switch (opType)
            {
                default:
                    throw new InvalidOperationException();
                case QueryGroup.And:
                    return "And";
                case QueryGroup.Or:
                    return "Or";
            }
        }
        public override string GetOperatorString(QueryType opType)
        {
            return GetBasicOperatorString(opType);
        }
        public override string GetOperatorString(QueryGroup opType)
        {
            return GetBasicOperatorString(opType);
        }
        public static string ToString(QueryCriteria operand)
        {
            if (ReferenceEquals(operand, null))
                return string.Empty;
            return Instance.Process(operand).Result;
        }
    }
    #endregion
}
