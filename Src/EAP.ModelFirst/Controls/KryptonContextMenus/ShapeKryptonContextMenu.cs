using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComponentFactory.Krypton.Toolkit;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using System.Drawing;

namespace EAP.ModelFirst.Controls.KryptonContextMenus
{
    internal sealed class ShapeKryptonContextMenu : DiagramKryptonContextMenu
    {
		static ShapeKryptonContextMenu _default = new ShapeKryptonContextMenu();

        bool locked;
		KryptonContextMenuItem itemAlign;
		KryptonContextMenuItem itemAlignTop, itemAlignLeft, itemAlignBottom, itemAlignRight;
		KryptonContextMenuItem itemAlignHorizontal, itemAlignVertical;
		KryptonContextMenuItem itemMakeSameSize;
        KryptonContextMenuItem itemSameWidth, itemSameHeight, itemSameSize;

        KryptonContextMenuItem itemBackColor;
        KryptonContextMenuColorColumns itemBackColorColumns;
        KryptonContextMenuColorColumns itemStanderBackColorColumns;
        KryptonContextMenuItem itemNoBackColor;

        KryptonContextMenuItem itemForeColor;
        KryptonContextMenuColorColumns itemForeColorColumns;
        KryptonContextMenuColorColumns itemStanderForeColorColumns;
        KryptonContextMenuItem itemNoForeColor;

        private ShapeKryptonContextMenu()
		{
			InitMenuItems();
		}

        public static ShapeKryptonContextMenu Default
		{
			get { return _default; }
		}

		private void UpdateTexts()
		{
			itemAlign.Text = Strings.MenuAlign;
			itemAlignTop.Text = Strings.MenuAlignTop;
			itemAlignLeft.Text = Strings.MenuAlignLeft;
			itemAlignBottom.Text = Strings.MenuAlignBottom;
			itemAlignRight.Text = Strings.MenuAlignRight;
			itemAlignHorizontal.Text = Strings.MenuAlignHorizontal;
			itemAlignVertical.Text = Strings.MenuAlignVertical;
			itemMakeSameSize.Text = Strings.MenuMakeSameSize;
			itemSameWidth.Text = Strings.MenuSameWidth;
			itemSameHeight.Text = Strings.MenuSameHeight;
			itemSameSize.Text = Strings.MenuSameSize;
		}

		public override void ValidateMenuItems(Diagram diagram)
		{
			base.ValidateMenuItems(diagram);
			GeneralKryptonContextMenu.Default.ValidateMenuItems(diagram);

			bool multiSelection = (diagram.SelectedElementCount >= 2);
			itemAlign.Enabled = multiSelection;
			itemAlignTop.Enabled = multiSelection;
			itemAlignLeft.Enabled = multiSelection;
			itemAlignBottom.Enabled = multiSelection;
			itemAlignRight.Enabled = multiSelection;
			itemAlignHorizontal.Enabled = multiSelection;
			itemAlignVertical.Enabled = multiSelection;
			itemMakeSameSize.Enabled = multiSelection;
			itemSameWidth.Enabled = multiSelection;
			itemSameHeight.Enabled = multiSelection;
			itemSameSize.Enabled = multiSelection;

            locked = true;
            if (diagram.SelectedShapeCount == 1)
            {
                var s = diagram.GetSelectedShapes().First();
                itemBackColorColumns.SelectedColor = s.BackColor;
                itemStanderBackColorColumns.SelectedColor = s.BackColor;
                itemNoBackColor.Checked = s.BackColor.IsEmpty;
                itemForeColorColumns.SelectedColor = s.ForeColor;
                itemStanderForeColorColumns.SelectedColor = s.ForeColor;
                itemNoForeColor.Checked = s.ForeColor.IsEmpty;
            }
            else
            {
                itemBackColorColumns.SelectedColor = Color.Empty;
                itemStanderBackColorColumns.SelectedColor = Color.Empty;
                itemNoBackColor.Checked = false;
                itemForeColorColumns.SelectedColor = Color.Empty;
                itemStanderForeColorColumns.SelectedColor = Color.Empty;
                itemNoForeColor.Checked = false;
            }
            locked = false;
            UpdateTexts();
		}

        private void InitMenuItems()
        {
            itemAlignTop = new KryptonContextMenuItem(Strings.MenuAlignTop, Resources.AlignTop, itemAlignTop_Click);
            itemAlignLeft = new KryptonContextMenuItem(Strings.MenuAlignLeft, Resources.AlignLeft, itemAlignLeft_Click);
            itemAlignBottom = new KryptonContextMenuItem(Strings.MenuAlignBottom, Resources.AlignBottom, itemAlignBottom_Click);
            itemAlignRight = new KryptonContextMenuItem(Strings.MenuAlignRight, Resources.AlignRight, itemAlignRight_Click);
            itemAlignHorizontal = new KryptonContextMenuItem(Strings.MenuAlignHorizontal, Resources.AlignHorizontal, itemAlignHorizontal_Click);
            itemAlignVertical = new KryptonContextMenuItem(Strings.MenuAlignVertical, Resources.AlignVertical, itemAlignVertical_Click);
            itemAlign = new KryptonContextMenuItem(Strings.MenuAlign);

            itemBackColor = new KryptonContextMenuItem(Strings.MenuBackColor);
            itemBackColorColumns = new KryptonContextMenuColorColumns();
            itemStanderBackColorColumns = new KryptonContextMenuColorColumns();
            itemStanderBackColorColumns.ColorScheme = ColorScheme.OfficeStandard;
            itemNoBackColor = new KryptonContextMenuItem(Strings.MenuNoColor, /*Resources.ButtonNoColor, */itemNoBackColor_Click);

            itemBackColorColumns.SelectedColorChanged += new EventHandler<ColorEventArgs>(itemBackColorColumns_SelectedColorChanged);
            itemStanderBackColorColumns.SelectedColorChanged += new EventHandler<ColorEventArgs>(itemBackColorColumns_SelectedColorChanged);

            itemForeColor = new KryptonContextMenuItem(Strings.MenuForeColor);
            itemForeColorColumns = new KryptonContextMenuColorColumns();
            itemStanderForeColorColumns = new KryptonContextMenuColorColumns();
            itemStanderForeColorColumns.ColorScheme = ColorScheme.OfficeStandard;
            itemNoForeColor = new KryptonContextMenuItem(Strings.MenuNoColor, /*Resources.ButtonNoColor, */itemNoForeColor_Click);

            itemForeColorColumns.SelectedColorChanged += new EventHandler<ColorEventArgs>(itemForeColorColumns_SelectedColorChanged);
            itemStanderForeColorColumns.SelectedColorChanged += new EventHandler<ColorEventArgs>(itemForeColorColumns_SelectedColorChanged);

            itemBackColor.Items.AddRange(new KryptonContextMenuItemBase[]{ 
                itemBackColorColumns,
                itemStanderBackColorColumns,
                new KryptonContextMenuItems(new[] { itemNoBackColor })
            });

            itemForeColor.Items.AddRange(new KryptonContextMenuItemBase[]{ 
                itemForeColorColumns,
                itemStanderForeColorColumns,
                new KryptonContextMenuItems(new[] { itemNoForeColor })
            });

            itemAlign.Items.Add(new KryptonContextMenuItems(new KryptonContextMenuItemBase[] {
                itemAlignTop,
                itemAlignLeft,
                itemAlignBottom,
                itemAlignRight,
                new KryptonContextMenuSeparator(),
                itemAlignHorizontal,
                itemAlignVertical
            }));

            itemSameWidth = new KryptonContextMenuItem(Strings.MenuSameWidth, itemSameWidth_Click);
            itemSameHeight = new KryptonContextMenuItem(Strings.MenuSameHeight, itemSameHeight_Click);
            itemSameSize = new KryptonContextMenuItem(Strings.MenuSameSize, itemSameSize_Click);
            itemMakeSameSize = new KryptonContextMenuItem(Strings.MenuMakeSameSize);
            itemMakeSameSize.Items.Add(new KryptonContextMenuItems(new KryptonContextMenuItemBase[] {
                itemSameWidth,
                itemSameHeight,
                itemSameSize
            }));

            MenuList.AddRange(GeneralKryptonContextMenu.Default.MenuItems);
            MenuList.AddRange(new KryptonContextMenuItemBase[] {
				new KryptonContextMenuSeparator(),
                itemBackColor,
                itemForeColor,
				itemAlign,
				itemMakeSameSize,
			});
        }

        void itemBackColorColumns_SelectedColorChanged(object sender, ColorEventArgs e)
        {
            if (!locked && Diagram != null)
                Diagram.SetShapeBackColor(e.Color);
        }

        void itemNoBackColor_Click(object sender, EventArgs e)
        {
            if (!locked && Diagram != null)
                Diagram.SetShapeBackColor(Color.Empty);
        }

        void itemForeColorColumns_SelectedColorChanged(object sender, ColorEventArgs e)
        {
            if (!locked && Diagram != null)
                Diagram.SetShapeForeColor(e.Color);
        }

        void itemNoForeColor_Click(object sender, EventArgs e)
        {
            if (!locked && Diagram != null)
                Diagram.SetShapeForeColor(Color.Empty);
        }

		private void itemAlignTop_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.AlignTop();
		}

		private void itemAlignLeft_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.AlignLeft();
		}

		private void itemAlignBottom_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.AlignBottom();
		}

		private void itemAlignRight_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.AlignRight();
		}

		private void itemAlignHorizontal_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.AlignHorizontal();
		}

		private void itemAlignVertical_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.AlignVertical();
		}

		private void itemSameWidth_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.AdjustToSameWidth();
		}

		private void itemSameHeight_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.AdjustToSameHeight();
		}

		private void itemSameSize_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
				Diagram.AdjustToSameSize();
		}
	}
}

