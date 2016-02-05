using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAP.ModelFirst.Properties;
using ComponentFactory.Krypton.Toolkit;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes;

namespace EAP.ModelFirst.Controls.KryptonContextMenus
{
    internal sealed class CommentShapeKryptonContextMenu : DiagramKryptonContextMenu
    {
		static CommentShapeKryptonContextMenu _default = new CommentShapeKryptonContextMenu();

		KryptonContextMenuItem mnuEditComment;

        private CommentShapeKryptonContextMenu()
		{
			InitMenuItems();
		}

        public static CommentShapeKryptonContextMenu Default
		{
			get { return _default; }
		}

		private void UpdateTexts()
		{
			mnuEditComment.Text = Strings.MenuEditComment;
		}

		public override void ValidateMenuItems(Diagram diagram)
		{
			base.ValidateMenuItems(diagram);
			ShapeKryptonContextMenu.Default.ValidateMenuItems(diagram);
			mnuEditComment.Enabled = (diagram.SelectedElementCount == 1);
            UpdateTexts();
		}

		private void InitMenuItems()
		{
			mnuEditComment = new KryptonContextMenuItem(Strings.MenuEditComment, Resources.EditComment, mnuEditComment_Click);

			MenuList.AddRange(ShapeKryptonContextMenu.Default.MenuItems);
            MenuList.AddRange(new KryptonContextMenuItemBase[] {
				new KryptonContextMenuSeparator(),
				mnuEditComment,
			});
		}

		private void mnuEditComment_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
			{
				CommentShape commentShape = Diagram.TopSelectedElement as CommentShape;
				if (commentShape != null)
					commentShape.EditText();
			}
		}
	}
}