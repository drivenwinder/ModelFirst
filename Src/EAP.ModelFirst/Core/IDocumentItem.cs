using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAP.ModelFirst.Core
{
    public interface IDocumentItem : INamedObject
    {
        event EventHandler StateChanged;

        event EventHandler Closing;

        void Close();
    }
}
