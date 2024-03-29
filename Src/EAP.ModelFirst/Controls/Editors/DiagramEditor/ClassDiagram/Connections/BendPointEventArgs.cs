using System;
using System.Xml;
using System.Drawing;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes;

namespace EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Connections
{
	public delegate void BendPointEventHandler(object sender, BendPointEventArgs e);

	public class BendPointEventArgs : EventArgs
	{
		BendPoint point;

		public BendPointEventArgs(BendPoint point)
		{
			this.point = point;
		}

		public BendPoint BendPoint
		{
			get { return point; }
		}
	}
}
