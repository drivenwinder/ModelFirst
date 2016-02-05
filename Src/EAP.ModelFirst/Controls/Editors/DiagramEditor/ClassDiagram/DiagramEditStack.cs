using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram
{
    public class DiagramEditStack
    {
        int editLevel = -1;

        List<XmlElement> track = new List<XmlElement>();

        XmlElement init;

        public event EventHandler StackChanged;

        public bool CanUndo { get { return editLevel >= 0; } }

        public bool CanRedo { get { return editLevel < track.Count - 1; } }

        public void Init(XmlElement value)
        {
            init = value;
        }

        public void Push(XmlElement value)
        {
            if (editLevel < track.Count - 1)
                track.RemoveRange(editLevel + 1, track.Count - editLevel - 1);
            track.Add(value);
            editLevel++;
            OnStackChanged();
        }

        public XmlElement Undo()
        {
            if (!CanUndo)
                throw new InvalidOperationException("Can't undo");
            if (--editLevel < 0 && init != null)
                return init;
            return track[editLevel];
        }

        public XmlElement Redo()
        {
            if (!CanRedo)
                throw new InvalidOperationException("Can't redo");
            return track[++editLevel];
        }

        public void Clear()
        {
            editLevel = -1;
            track.Clear();
            OnStackChanged();
        }

        protected void OnStackChanged()
        {
            if (StackChanged != null)
                StackChanged.Invoke(this, new EventArgs());
        }

        public XmlElement Serialize(Diagram diagram)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?><configuration></configuration>");
            XmlElement node = doc.CreateElement("root");
            diagram.Serialize(node);
            return node;
        }
    }
}
