using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Core.Project
{
    public class DeserializeContext
    {
        List<KeyValuePair<Model, XmlElement>> models = new List<KeyValuePair<Model, XmlElement>>();
        
        public void Add(Model model, XmlElement node)
        {
            models.Add(new KeyValuePair<Model, XmlElement>(model, node));
        }

        public IEnumerable<KeyValuePair<Model, XmlElement>> Models
        {
            get { return models; }
        }
    }
}
