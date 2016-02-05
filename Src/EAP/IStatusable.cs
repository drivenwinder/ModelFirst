using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EAP
{
    public interface IStatusable
    {
        string StatusText { get; set; }

        Cursor Cursor { get; set; }
    }
}
