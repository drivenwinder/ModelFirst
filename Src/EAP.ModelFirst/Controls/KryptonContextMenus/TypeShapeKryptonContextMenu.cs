using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using ComponentFactory.Krypton.Toolkit;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Controls.Dialogs;

namespace EAP.ModelFirst.Controls.KryptonContextMenus
{
	internal sealed class TypeShapeKryptonContextMenu : DiagramKryptonContextMenu
	{
		static TypeShapeKryptonContextMenu _default = new TypeShapeKryptonContextMenu();

		KryptonContextMenuItem mnuSize, mnuAutoSize, mnuAutoWidth, mnuAutoHeight;
		KryptonContextMenuItem mnuCollapseAllSelected, mnuExpandAllSelected;
		KryptonContextMenuItem mnuEdit;

		private TypeShapeKryptonContextMenu()
		{
			InitMenuItems();
		}

		public static TypeShapeKryptonContextMenu Default
		{
			get { return _default; }
		}

		private void UpdateTexts()
		{
			mnuSize.Text = Strings.MenuSize;
			mnuAutoSize.Text = Strings.MenuAutoSize;
			mnuAutoWidth.Text = Strings.MenuAutoWidth;
			mnuAutoHeight.Text = Strings.MenuAutoHeight;
			mnuCollapseAllSelected.Text = Strings.MenuCollapseAllSelected;
			mnuExpandAllSelected.Text = Strings.MenuExpandAllSelected;
            mnuEdit.Text = Strings.MenuEditDetial;
		}

		public override void ValidateMenuItems(Diagram diagram)
		{
			base.ValidateMenuItems(diagram);
			ShapeKryptonContextMenu.Default.ValidateMenuItems(diagram);
			mnuEdit.Enabled = (diagram.SelectedElementCount == 1);
		}

        private void InitMenuItems()
        {
            mnuEdit = new KryptonContextMenuItem(Strings.MenuEditDetial, Resources.EditMembers, mnuEdit_Click);
            mnuAutoSize = new KryptonContextMenuItem(Strings.MenuAutoSize, mnuAutoSize_Click);
            mnuAutoWidth = new KryptonContextMenuItem(Strings.MenuAutoWidth, mnuAutoWidth_Click);
            mnuAutoHeight = new KryptonContextMenuItem(Strings.MenuAutoHeight,  mnuAutoHeight_Click);
            mnuCollapseAllSelected = new KryptonContextMenuItem(Strings.MenuCollapseAllSelected, mnuCollapseAllSelected_Click);
            mnuExpandAllSelected = new KryptonContextMenuItem(Strings.MenuExpandAllSelected, mnuExpandAllSelected_Click);
            mnuSize = new KryptonContextMenuItem(Strings.MenuSize);
            mnuSize.Items.Add(new KryptonContextMenuItems(new KryptonContextMenuItemBase[] {
                mnuAutoSize,
                mnuAutoWidth,
                mnuAutoHeight,
                new KryptonContextMenuSeparator(),
                mnuCollapseAllSelected,
                mnuExpandAllSelected
            }));

            MenuList.AddRange(ShapeKryptonContextMenu.Default.MenuItems);
            MenuList.AddRange(new KryptonContextMenuItemBase[] {
				mnuSize,
				mnuEdit,
			});
        }

		private void mnuAutoSize_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.AutoSizeOfShapes();
		}

		private void mnuAutoWidth_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.AutoWidthOfShapes();
		}

		private void mnuAutoHeight_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.AutoHeightOfShapes();
		}

		private void mnuCollapseAllSelected_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.CollapseAll(true);
		}

		private void mnuExpandAllSelected_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.ExpandAll(true);
		}

		private void mnuEdit_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
			{
				TypeShape typeShape = Diagram.TopSelectedElement as TypeShape;
                if (typeShape != null)
                {
                    typeShape.IsActive = false;
                    using (TypeBaseDialog dialog = new TypeBaseDialog())
                    {
                        dialog.ShowDialog(typeShape.TypeBase);
                    }
                }
			}
		}
	}
}