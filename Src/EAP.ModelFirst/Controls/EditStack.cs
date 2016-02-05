using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace EAP.ModelFirst.Controls
{
    public class EditStack
    {
        public class EditValue
        {
            public EditorInfo Editor { get; set; }

            public XmlElement Value { get; set; }
        }

        int editLevel = -1;

        List<EditValue> track = new List<EditValue>();

        EditValue init;

        public event EventHandler StackChanged;

        public bool CanUndo { get { return editLevel >= 0; } }

        public bool CanRedo { get { return editLevel < track.Count - 1; } }

        public void Init(EditorInfo ctl, XmlElement value)
        {
            init = new EditValue() { Editor = ctl, Value = value };
        }

        public void Push(EditorInfo ctl, XmlElement value)
        {
            if (editLevel < track.Count - 1)
                track.RemoveRange(editLevel + 1, track.Count - editLevel - 1);
            track.Add(new EditValue { Editor = ctl, Value = value });
            editLevel++;
            OnStackChanged();
        }

        public EditValue Undo()
        {
            if (!CanUndo)
                throw new InvalidOperationException("Can't undo");
            if (--editLevel < 0 && init != null)
                return init;
            return track[editLevel];
        }

        public EditValue Redo()
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
    }
}

