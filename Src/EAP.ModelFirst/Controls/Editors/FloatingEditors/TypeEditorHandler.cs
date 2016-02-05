using System;
using System.Drawing;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes;

namespace EAP.ModelFirst.Controls.Editors.FloatingEditors
{
    public abstract class TypeEditorHandler : FloatingEditorHandler
	{
		internal sealed override void Relocate(DiagramElement element)
		{
			Relocate((TypeShape) element);
		}

		internal void Relocate(TypeShape shape)
		{
			Diagram diagram = shape.Diagram;
			if (diagram != null)
			{
				Point absolute = new Point(shape.Right, shape.Top);
				Size relative = new Size(
					(int) (absolute.X * diagram.Zoom) - diagram.Offset.X + MarginSize,
					(int) (absolute.Y * diagram.Zoom) - diagram.Offset.Y);

                Window.Location = Window.ParentLocation + relative;
			}
		}
    }
}
