using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAP.ModelFirst.Core
{
    public interface INamedObject
    {
        event EventHandler Renamed;

        string Name { get; set; }
    }
}
