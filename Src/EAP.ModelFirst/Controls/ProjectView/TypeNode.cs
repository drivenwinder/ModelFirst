using System;
using System.Windows.Forms;
using EAP.ModelFirst.Controls.Explorers;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;

namespace EAP.ModelFirst.Controls.ProjectView
{
    public sealed class TypeNode : ItemNode, IProjectItemNode
    {
        static ContextMenuStrip contextMenu = new ContextMenuStrip();

        static TypeNode()
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

        public TypeNode(TypeBase type)
        {
            ImageKey = type.EntityType.ToString();
            SelectedImageKey = ImageKey;
            TypeBase = type;
            TypeBase.Renamed += new EventHandler(TypeBase_Renamed);
            Text = TypeBase.Name;
        }

        #region Menu Click

        private static void rename_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            TypeNode node = (TypeNode)menuItem.Owner.Tag;

            node.EditLabel();
        }

        private static void open_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            TypeNode node = (TypeNode)menuItem.Owner.Tag;
            node.ItemView.DockForm.OpenDocument(node.TypeBase);
        }

        private static void delete_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            IProjectItem item = ((TypeNode)menuItem.Owner.Tag).TypeBase;

            DialogResult result = Client.ShowConfirm(string.Format(Strings.DeleteItemConfirmation, item.Name));

            if (result == DialogResult.OK)
                item.Package.Remove(item);
        }

        private static void genCode_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            TypeNode node = (TypeNode)menuItem.Owner.Tag;
            Generator.Show(new[] { node.TypeBase });
        }

        #endregion

        void TypeBase_Renamed(object sender, EventArgs e)
        {
            Text = TypeBase.Name;
            if (TreeView != null)
                TreeView.Sort();
        }

        public override void BeforeDelete()
        {
            //从所有Diagram中移除
            TypeBase.ProjectInfo.RemoveEntity(TypeBase);
            //关闭编辑窗口
            TypeBase.Close();
            base.BeforeDelete();
        }

        public override void DoubleClick()
        {
            ItemView.DockForm.OpenDocument(TypeBase);
        }

        public override void LabelModified(NodeLabelEditEventArgs e)
        {
            TypeBase.Name = e.Label;

            if (TypeBase.Name != e.Label)
                e.CancelEdit = true;
        }

        public TypeBase TypeBase { get ; private set; }

        public override ContextMenuStrip ContextMenuStrip
        {
            get
            {
                contextMenu.Tag = this;
                return contextMenu;
            }
            set
            {
                base.ContextMenuStrip = value;
            }
        }

        public IProjectItem ProjectItem
        {
            get { return TypeBase; }
        }
    }
}
