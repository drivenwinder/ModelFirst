using System;
using System.Linq;

namespace EAP.ModelFirst.Controls.Documents
{
    public partial class DocumentBase : DockContent
    {
        public DocumentBase()
        {
            Handler = new DocumentHandler();
        }

        public DocumentHandler Handler { get; private set; }

        public IDockForm DockForm { get; set; }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void closeOtherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var d in DockHandler.DockPanel.Documents.ToList())
                if(d != this)
                    d.DockHandler.Close();
        }

        public virtual IDynamicMenu GetDynamicMenu()
        {
            return null;
        }
    }
}
