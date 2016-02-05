using System;
using System.Runtime.Serialization;

namespace EAP
{
    public class AppException : InvalidException
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        protected AppException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public AppException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class InvalidException : Exception
    {
        string _innerStackTrace;

        /// <summary>
        /// 获取调用堆栈上直接帧的字符串表示形式。
        /// </summary>
        public override string StackTrace
        {
            get
            {
                return String.Format("{0}{1}{2}",
                    _innerStackTrace, Environment.NewLine, base.StackTrace);
            }
        }

        public InvalidException() { }

        public InvalidException(string msg)
            : base(msg)
        {
        }

        public InvalidException(string message, Exception ex)
            : base(message, ex)
        {
        }

        protected InvalidException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            _innerStackTrace = info.GetString("_innerStackTrace");
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.SerializationFormatter)]
        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand, Flags = System.Security.Permissions.SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("_innerStackTrace", _innerStackTrace);
        }
    }
}
