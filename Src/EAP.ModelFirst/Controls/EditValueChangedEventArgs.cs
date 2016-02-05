using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EAP.ModelFirst.Controls
{
    public delegate void EditValueChangedEventHandler(object sender, EditValueChangedEventArgs e);

    public class EditValueChangedEventArgs : EventArgs
    {
        public EditValueChangedEventArgs()
        {
        }

        public EditValueChangedEventArgs(EditorInfo info)
        {
            Editor = info;
        }

        public EditorInfo Editor { get; set; }
    }
}
