using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Controls.Editors.DiagramEditor;

namespace EAP.ModelFirst.Controls.KryptonContextMenus
{
    public abstract class DiagramKryptonContextMenu : KryptonContextMenuBase
    {
        Diagram diagram = null;

        protected sealed override IDiagram Document
        {
            get { return diagram; }
        }

        protected Diagram Diagram
        {
            get { return diagram; }
        }

        public sealed override void ValidateMenuItems(IDiagram document)
        {
            ValidateMenuItems(document as Diagram);
        }

        public virtual void ValidateMenuItems(Diagram diagram)
        {
            this.diagram = diagram;
        }
    }
}
