using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComponentFactory.Krypton.Toolkit;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Connections;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Controls.KryptonContextMenus
{
    internal sealed class GeneralizationKryptonContextMenu : DiagramKryptonContextMenu
    {
		static GeneralizationKryptonContextMenu _default = new GeneralizationKryptonContextMenu();
        
		KryptonContextMenuItem mnuEdit;

        private GeneralizationKryptonContextMenu()
		{
			InitMenuItems();
		}

        public static GeneralizationKryptonContextMenu Default
		{
			get { return _default; }
		}

		private void UpdateTexts()
		{
			mnuEdit.Text = Strings.MenuProperties;
		}

		public override void ValidateMenuItems(Diagram diagram)
		{
			base.ValidateMenuItems(diagram);
			ConnectionKryptonContextMenu.Default.ValidateMenuItems(diagram);
            Generalization generalization = diagram.TopSelectedElement as Generalization;
            mnuEdit.Enabled = generalization != null && generalization.GeneralizationRelationship.Second is ClassType;
            UpdateTexts();
		}

        private void InitMenuItems()
        {
            mnuEdit = new KryptonContextMenuItem(Strings.MenuEditConnection, Resources.Property, mnuEdit_Click);

            MenuList.AddRange(ConnectionKryptonContextMenu.Default.MenuItems);
            MenuList.Add(mnuEdit);
        }

		private void mnuEdit_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
			{
                Generalization generalization = Diagram.TopSelectedElement as Generalization;
                if (generalization != null)
                    generalization.ShowEditDialog();
			}
		}
	}
}
