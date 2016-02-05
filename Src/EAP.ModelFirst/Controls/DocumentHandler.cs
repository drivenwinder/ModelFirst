using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAP.ModelFirst.Core;

namespace EAP.ModelFirst.Controls
{
    public class DocumentHandler
    {
        public event EventHandler Compile;

        public event EventHandler GenerateCode;

        public IPrintable PrintHandler { get; set; }

        public IEditable EditHandler { get; set; }

        public IZoomable ZoomHandler { get; set; }

        public IDocumentVisualizer VisualizerHandler { get; set; }

        public ILayoutable LayoutHandler { get; set; }

        public ITextFormatable TextFormatHandler { get; set; }

        public bool IsPrintable { get { return PrintHandler != null; } }

        public bool IsEditable { get { return EditHandler != null; } }

        public bool IsZoomable { get { return ZoomHandler != null; } }

        public bool IsVisualizable { get { return VisualizerHandler != null; } }

        public bool IsLayoutable { get { return LayoutHandler != null; } }

        public bool IsTextFormatable { get { return TextFormatHandler != null; } }

        public bool CanCompile { get { return Compile != null; } }

        public bool CanGenerateCode { get { return GenerateCode != null; } }

        public void PerformCompile()
        {
            if (Compile != null)
                Compile(this, EventArgs.Empty);
        }

        public void PerformGenerateCode()
        {
            if (GenerateCode != null)
                GenerateCode(this, EventArgs.Empty);
        }
    }
}
