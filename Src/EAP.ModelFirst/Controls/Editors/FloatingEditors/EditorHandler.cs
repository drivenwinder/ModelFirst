using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;

namespace EAP.ModelFirst.Controls.Editors.FloatingEditors
{
    public abstract class EditorHandler
    {
        public virtual EditorWindow Window { get; set; }

        internal abstract void Init(DiagramElement element);

        internal abstract void Relocate(DiagramElement element);

        public abstract void ValidateData();

        protected internal virtual void OnWindowPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
        }
    }
}
