using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAP.Win.UI
{
    public delegate void FilterControlColumnEventHandler(object sender, FilterColumnEventArgs e);

    public class FilterColumnEventArgs : EventArgs
    {
        public FilterColumn Column { get; set; }
    }
}
