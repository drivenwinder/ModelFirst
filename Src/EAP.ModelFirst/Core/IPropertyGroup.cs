using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace EAP.ModelFirst.Core
{
    public interface IPropertyGroup
    {
        event EventHandler SelectionChanged;

        void Select(object obj);

        IEnumerable ObjectList { get; }

        object[] SelectedObjects { get; }
    }
}
