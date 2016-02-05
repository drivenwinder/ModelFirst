using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;

namespace EAP.ModelFirst.Controls.Dialogs
{
    public class PrintingSettings
    {
        public bool Landscape { get; set; }
        public Margins Margins { get; set; }
        public PaperSize PaperSize { get; set; }
        public PaperSource PaperSource { get; set; }
        public string PrinterName { get; set; }
    }
}
