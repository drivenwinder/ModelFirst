using System.Collections.Generic;
using System.Windows.Forms;

namespace EAP.ModelFirst.Controls
{
    public interface IDynamicMenu
    {
        int PreferredIndex { get; }

        IEnumerable<ToolStripMenuItem> GetMenuItems();

        void Close();
    }
}
