using System;
using System.Drawing;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using EAP.ModelFirst.Controls.Editors.FloatingEditors;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Relationships;

namespace EAP.ModelFirst.Controls.Editors.DiagramEditor
{
    public interface IDiagram : IProjectDocument, IEditable, IPrintable, ILayoutable
	{
		event EventHandler OffsetChanged;
		event EventHandler SizeChanged;
		event EventHandler ZoomChanged;
		event EventHandler StatusChanged;
		event EventHandler NeedsRedraw;
		event PopupWindowEventHandler ShowingWindow;
		event PopupWindowEventHandler HidingWindow;
        
		Point Offset { get; set; }

		Size Size { get; }

		float Zoom { get; set; }

		Color BackColor { get; }

		bool HasSelectedElement { get; }

		void Display(Graphics g);

		void Redraw();

		void CloseWindows();

		string GetShortDescription();

		string GetSelectedElementName();

		void MouseDown(AbsoluteMouseEventArgs e);

		void MouseMove(AbsoluteMouseEventArgs e);

		void MouseUp(AbsoluteMouseEventArgs e);

        void DragOver(AbsoluteMouseEventArgs e);

        void DragDrop(AbsoluteMouseEventArgs e);

		void DoubleClick(AbsoluteMouseEventArgs e);

		void KeyDown(KeyEventArgs e);

        KryptonContextMenu GetKryptonContextMenu(AbsoluteMouseEventArgs e);

        void CancelCreating();

        void CreateShape(EntityType type);

        void CreateShape(IEntity entity);

        void CreateConnection(RelationshipType type);

        void AcceptEdit();

        string GetStatus();
	}
}
