using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAP.ModelFirst.Core
{
    public interface ILayoutable
    {
        event EventHandler SelectionChanged;

        int SelectedShapeCount { get; }

        void AlignLeft();

        void AlignVertical();

        void AlignRight();

        void AlignTop();

        void AlignHorizontal();

        void AlignBottom();

        void AdjustToSameWidth();

        void AdjustToSameHeight();

        void AdjustToSameSize();
    }
}
