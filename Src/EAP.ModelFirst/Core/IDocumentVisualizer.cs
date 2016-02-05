using System;
using System.Drawing;
using System.Windows.Forms;
using EAP.ModelFirst.Controls.Editors.DiagramEditor;

namespace EAP.ModelFirst.Core
{
	public interface IDocumentVisualizer : IZoomable
	{
		event EventHandler DocumentRedrawed;
		event EventHandler VisibleAreaChanged;

		bool HasDocument { get; }

		//IDiagram Diagram { get; }

		Point Offset { get; set; }

		Size DocumentSize { get; }

		Rectangle VisibleArea { get; }

		void DrawDocument(Graphics g);
	}
}
