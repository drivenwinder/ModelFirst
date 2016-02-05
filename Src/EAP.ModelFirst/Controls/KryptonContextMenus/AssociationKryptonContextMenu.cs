using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComponentFactory.Krypton.Toolkit;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Connections;
using EAP.ModelFirst.Core.Project.Relationships;

namespace EAP.ModelFirst.Controls.KryptonContextMenus
{
    internal sealed class AssociationKryptonContextMenu : DiagramKryptonContextMenu
    {
		static AssociationKryptonContextMenu _default = new AssociationKryptonContextMenu();

		KryptonContextMenuItem mnuDirection, mnuUnidirectional, mnuBidirectional;
		KryptonContextMenuItem mnuType, mnuAssociation, mnuComposition, mnuAggregation;
		KryptonContextMenuItem mnuReverse;
		KryptonContextMenuItem mnuEdit;

        private AssociationKryptonContextMenu()
		{
			InitMenuItems();
		}

		public static AssociationKryptonContextMenu Default
		{
			get { return _default; }
		}

		private void UpdateTexts()
		{
			mnuDirection.Text = Strings.MenuDirection;
			mnuUnidirectional.Text = Strings.MenuUnidirectional;
			mnuBidirectional.Text = Strings.MenuBidirectional;
			mnuType.Text = Strings.MenuType;
			mnuAssociation.Text = Strings.MenuAssociation;
			mnuComposition.Text = Strings.MenuComposition;
			mnuAggregation.Text = Strings.MenuAggregation;
			mnuReverse.Text = Strings.MenuReverse;
			mnuEdit.Text = Strings.MenuProperties;
		}

		public override void ValidateMenuItems(Diagram diagram)
		{
			base.ValidateMenuItems(diagram);
			ConnectionKryptonContextMenu.Default.ValidateMenuItems(diagram);
			mnuEdit.Enabled = (diagram.SelectedElementCount == 1);
            UpdateTexts();
		}

        private void InitMenuItems()
        {
            mnuUnidirectional = new KryptonContextMenuItem(Strings.MenuUnidirectional,
                Resources.Unidirectional, mnuUnidirectional_Click);
            mnuBidirectional = new KryptonContextMenuItem(Strings.MenuBidirectional,
                Resources.Bidirectional, mnuBidirectional_Click);
            mnuDirection = new KryptonContextMenuItem(Strings.MenuDirection);
            mnuDirection.Items.Add(new KryptonContextMenuItems(new[]{
				mnuUnidirectional,
				mnuBidirectional
            }));

            mnuAssociation = new KryptonContextMenuItem(Strings.MenuAssociation, Resources.Association, mnuAssociation_Click);
            mnuComposition = new KryptonContextMenuItem(Strings.MenuComposition, Resources.Composition, mnuComposition_Click);
            mnuAggregation = new KryptonContextMenuItem(Strings.MenuAggregation, Resources.Aggregation, mnuAggregation_Click);
            mnuType = new KryptonContextMenuItem(Strings.MenuType);
            mnuType.Items.Add(new KryptonContextMenuItems(new[]{
				mnuAssociation,
				mnuComposition,
				mnuAggregation
			}));

            mnuReverse = new KryptonContextMenuItem(Strings.MenuReverse, mnuReverse_Click);
            mnuEdit = new KryptonContextMenuItem(Strings.MenuEditConnection, Resources.Property, mnuEdit_Click);

            MenuList.AddRange(ConnectionKryptonContextMenu.Default.MenuItems);
            MenuList.Insert(10, mnuDirection);
            MenuList.Insert(11, mnuType);
            MenuList.Insert(12, mnuReverse);
            MenuList.Insert(13, new KryptonContextMenuSeparator());
            MenuList.Add(mnuEdit);
        }

		private void mnuUnidirectional_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
			{
				foreach (Association association in Diagram.GetSelectedConnections())
					association.AssociationRelationship.Direction = Direction.Unidirectional;
			}
		}

		private void mnuBidirectional_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
			{
				foreach (Association association in Diagram.GetSelectedConnections())
					association.AssociationRelationship.Direction = Direction.Bidirectional;
			}
		}

		private void mnuAssociation_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
			{
				foreach (Association association in Diagram.GetSelectedConnections())
				{
					association.AssociationRelationship.AssociationType = AssociationType.Association;
				}
			}
		}

		private void mnuComposition_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
			{
				foreach (Association association in Diagram.GetSelectedConnections())
					association.AssociationRelationship.AssociationType = AssociationType.Composition;
			}
		}

		private void mnuAggregation_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
			{
				foreach (Association association in Diagram.GetSelectedConnections())
					association.AssociationRelationship.AssociationType = AssociationType.Aggregation;
			}
		}

		private void mnuReverse_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
			{
				Association association = Diagram.TopSelectedElement as Association;
				if (association != null)
					association.AssociationRelationship.Reverse();
			}
		}

		private void mnuEdit_Click(object sender, EventArgs e)
		{
			if (Diagram != null)
			{
				Association association = Diagram.TopSelectedElement as Association;
				if (association != null)
					association.ShowEditDialog();
			}
		}
	}
}
