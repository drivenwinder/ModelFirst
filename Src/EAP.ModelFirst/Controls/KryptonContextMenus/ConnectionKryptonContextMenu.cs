using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComponentFactory.Krypton.Toolkit;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Connections;

namespace EAP.ModelFirst.Controls.KryptonContextMenus
{
    internal sealed class ConnectionKryptonContextMenu: DiagramKryptonContextMenu
	{
		static ConnectionKryptonContextMenu _default = new ConnectionKryptonContextMenu();

		KryptonContextMenuItem mnuAutoRouting;

        private ConnectionKryptonContextMenu()
		{
			InitMenuItems();
		}

        public static ConnectionKryptonContextMenu Default
		{
			get { return _default; }
		}

		private void UpdateTexts()
		{
			mnuAutoRouting.Text = Strings.MenuAutoRouting;
		}

		public override void ValidateMenuItems(Diagram diagram)
		{
			base.ValidateMenuItems(diagram);
			GeneralKryptonContextMenu.Default.ValidateMenuItems(diagram);
            UpdateTexts();
		}

		private void InitMenuItems()
		{
			mnuAutoRouting = new KryptonContextMenuItem(Strings.MenuAutoRouting,
				null, mnuAutoRouting_Click);

			MenuList.AddRange(GeneralKryptonContextMenu.Default.MenuItems);
            MenuList.AddRange(new KryptonContextMenuItemBase[] {
				new KryptonContextMenuSeparator(),
				mnuAutoRouting,
			});
		}

		private void mnuAutoRouting_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
			{
				foreach (Connection connection in Diagram.GetSelectedConnections())
					connection.AutoRoute();
			}
		}
	}
}