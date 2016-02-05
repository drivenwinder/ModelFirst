using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EAP.ModelFirst.Controls
{
    public class EditorInfo
    {
        public Control Control { get; set; }

        public EditorInfo(Control c)
        {
            Control = c;
        }

        public virtual void Focus()
        {
            Control.Focus();
        }
    }

    public class ListEditorInfo : EditorInfo
    {
        ListView view;

        int itemIndex;

        public ListEditorInfo(ListView v, int index, Control ctl)
            : base(ctl)
        {
            view = v;
            itemIndex = index;
        }

        public override void Focus()
        {
            if (view.Items.Count > itemIndex)
                view.Items[itemIndex].Selected = true;
            base.Focus();
        }
    }
}
