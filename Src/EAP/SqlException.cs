using System;
using System.Runtime.Serialization;

namespace EAP
{
    [DataContract]
    public class SqlException : Exception
    {
        string _Sql;
        string _innerStackTrace;
        [DataMember]
        public string Sql
        {
            get { return _Sql; }
            set { _Sql = value; }
        }
        [DataMember]
        public override string StackTrace
        {
            get
            {
                return String.Format("{0}{1}{2}",
                    _innerStackTrace, Environment.NewLine, base.StackTrace);
            }
        }

        public SqlException(string message, Exception ex, ISqlSection sql)
            : base(message, ex)
        {
            _innerStackTrace = ex.StackTrace;
            if (sql != null)
                _Sql = sql.ToDbCommandText();
        }

        public SqlException(string message, Exception ex, string sqlText)
            : base(message, ex)
        {
            _innerStackTrace = ex.StackTrace;
            _Sql = sqlText;
        }

        public SqlException(Exception ex, string sqlText)
            : base(ex.Message, ex)
        {
            _innerStackTrace = ex.StackTrace;
            _Sql = sqlText;
        }

        public SqlException(Exception ex) : this(ex.Message, ex, "") { }

        public SqlException(string message, Exception ex) : this(message, ex, "") { }

        public SqlException(Exception ex, ISqlSection sql) : this(ex.Message, ex, sql) { }
    }
}
