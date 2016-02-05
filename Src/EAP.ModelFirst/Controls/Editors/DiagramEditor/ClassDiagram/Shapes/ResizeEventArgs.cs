using System;
using System.Drawing;

namespace EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes
{
	public delegate void ResizeEventHandler(object sender, ResizeEventArgs e);

	public class ResizeEventArgs : EventArgs
	{
		Size change;

		public ResizeEventArgs(Size change)
		{
			this.change = change;
		}

		public Size Change
		{
			get { return change; }
			set { change = value; }
		}
	}
}
