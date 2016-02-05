using System;
using System.Linq;
using System.Windows.Forms;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Controls.Explorers;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;

namespace EAP.ModelFirst.Controls.ProjectView
{
	public sealed class DiagramNode : ItemNode, IProjectItemNode
	{
		Diagram diagram;

		static ContextMenuStrip contextMenu = new ContextMenuStrip();

        static DiagramNode()
        {
            contextMenu.Items.AddRange(new ToolStripItem[] {
				new ToolStripMenuItem(Strings.MenuOpen, Resources.View, open_Click),
				new ToolStripSeparator(),
                new ToolStripMenuItem(Strings.MenuGenerateCode, Resources.CodeGenerator, genCode_Click),
                new ToolStripSeparator(),
				new ToolStripMenuItem(Strings.MenuRename, Resources.Rename, rename_Click, Keys.F2),
				new ToolStripMenuItem(Strings.MenuDeleteProjectItem, Resources.Delete, delete_Click)
			});
        }

		public DiagramNode(Diagram diagram)
		{
			if (diagram == null)
				throw new ArgumentNullException("diagram");

			this.diagram = diagram;
			this.Text = diagram.Name;
			this.ImageKey = "diagram";
			this.SelectedImageKey = "diagram";

            Diagram.EntityAdded += new EntityEventHandler(diagram_EntityAdded);
            Diagram.Renamed += new EventHandler(Diagram_Renamed);
		}

        void Diagram_Renamed(object sender, EventArgs e)
        {
            Text = Diagram.Name;
            if (TreeView != null)
                TreeView.Sort();
        }

        void diagram_EntityAdded(object sender, EntityEventArgs e)
        {
            if (e.Entity is TypeBase)
            {
                if (((TypeBase)e.Entity).ProjectInfo == null)
                {
                    if (Parent is ProjectNode)
                        ((ProjectNode)(Parent)).Project.Add((TypeBase)e.Entity);
                    else if (Parent is PackageNode)
                        ((PackageNode)(Parent)).Package.Add((TypeBase)e.Entity);
                }
            }
        }

		public Diagram Diagram
		{
			get { return diagram; }
		}

		public IProjectItem ProjectItem
		{
			get { return diagram; }
		}

        public override ContextMenuStrip ContextMenuStrip
        {
            get
            {
                contextMenu.Tag = this;
                return contextMenu;
            }
            set { base.ContextMenuStrip = value; }
        }

        public override void BeforeDelete()
        {
            diagram.EntityAdded -= new EntityEventHandler(diagram_EntityAdded);
            //关闭编辑窗口
            ((IDocumentItem)Diagram).Close();
            base.BeforeDelete();
        }

		public override void LabelModified(NodeLabelEditEventArgs e)
		{
			diagram.Name = e.Label;
		}

		public override void DoubleClick()
        {
            ItemView.DockForm.OpenDocument(diagram);
		}

		public override void EnterPressed()
		{
            if (ItemView != null)
                ItemView.DockForm.OpenDocument(diagram);
		}

		private static void open_Click(object sender, EventArgs e)
		{
			ToolStripItem menuItem = (ToolStripItem) sender;
			DiagramNode node = (DiagramNode) menuItem.Owner.Tag;
            node.ItemView.DockForm.OpenDocument(node.Diagram);		
		}

		private static void rename_Click(object sender, EventArgs e)
		{
			ToolStripItem menuItem = (ToolStripItem) sender;
			DiagramNode node = (DiagramNode) menuItem.Owner.Tag;
			node.EditLabel();
		}

        private static void delete_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            IProjectItem diagram = ((DiagramNode)menuItem.Owner.Tag).Diagram;

            DialogResult result = Client.ShowConfirm(string.Format(Strings.DeleteItemConfirmation, diagram.Name));

            if (result == DialogResult.OK)
                diagram.Package.Remove(diagram);
        }

        private static void genCode_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            DiagramNode node = (DiagramNode)menuItem.Owner.Tag;
            var types = node.Diagram.Entities.OfType<TypeBase>();
            Generator.Show(types);
        }
	}
}
