using EAP.ModelFirst.Core;
using EAP.Win.UI;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EAP.ModelFirst.Controls
{
    public interface IDocument : IDockContent
    {
        IDocumentItem DocumentItem { get; }

        string GetStatus();

        DocumentHandler Handler { get; }

        IDynamicMenu GetDynamicMenu();

        void Save();

        void SaveAs();

        bool IsDirty { get; }
    }
}
