using System;
using System.Runtime.Serialization;

namespace EAP.Logging
{
    [DataContract]
    public class LogEvent
    {
        [DataMember]
        public string AppId { get; set; }
        [DataMember]
        public string Caption { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public LogLevel Level { get; set; }
        [DataMember]
        public string Reporter { get; set; }
        [DataMember]
        public string IPAddress { get; set; }
        [DataMember]
        public string SystemInfo { get; set; }
        [DataMember]
        public DateTime LogTime { get; set; }
    }
}
