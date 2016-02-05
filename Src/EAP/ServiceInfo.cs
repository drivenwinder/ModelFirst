using System;
using System.Runtime.Serialization;

namespace EAP
{
    [DataContract]
    public class ServiceInfo
    {
        [DataMember]
        public string Version { get; set; }

        [DataMember]
        public string ServiceType { get; set; }

        [DataMember]
        public DateTime ServerTime { get; set; }
    }
}
