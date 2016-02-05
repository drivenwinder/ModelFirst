using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAP.Win.UI;
using EAP.ModelFirst.Core;
using System.Windows.Forms;

namespace EAP.ModelFirst
{
    public interface IDockForm : IStatusable, ILocalizable
    {
        DockPanel DockPanel { get; }

        void UpdateProgress(int value);

        void OpenDocument(IDocumentItem item);

        event FormClosingEventHandler FormClosing;

        Workspace Workspace { get; }
    }
}
