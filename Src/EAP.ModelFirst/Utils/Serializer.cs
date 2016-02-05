using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using EAP.ModelFirst.Core;

namespace EAP.ModelFirst.Utils
{
    public static class Serializer
    {
        public static XmlElement Serialize(ISerializableElement element)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><configuration></configuration>");
            XmlElement node = doc.CreateElement("root");
            element.Serialize(node);
            return node;
        }
    }
}
