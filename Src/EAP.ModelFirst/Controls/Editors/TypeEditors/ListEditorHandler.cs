using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EAP.ModelFirst.Controls.Editors.TypeEditors
{
    public abstract class ListEditorHandler
    {
        public ListEditor ListEditor { get; set; }

        protected internal abstract void FillList();

        protected internal abstract void AddToList(string text);

        protected internal abstract void Modify(ListViewItem item, string text);
    }
}
