using System;
using System.Windows.Forms;
using EAP.ModelFirst.Core.Template;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;

namespace EAP.ModelFirst.Controls.TemplateView
{
    public class FolderNode : ItemNode
    {
        TemplateFolder templateFolder;

        static ContextMenuStrip contextMenu = new ContextMenuStrip();

        static FolderNode()
        {
            contextMenu.Items.AddRange(new ToolStripItem[] {
				new ToolStripMenuItem(Strings.MenuAddNew, Resources.NewDocument,
					new ToolStripMenuItem(Strings.MenuTemplateFile, Resources.Template, newFile_Click),
				    new ToolStripMenuItem(Strings.MenuFolder, Resources.Folder, newFolder_Click)
				),
				new ToolStripMenuItem(Strings.MenuLocationFolder, Resources.Open, lookup_Click),
				new ToolStripSeparator(),
				new ToolStripMenuItem(Strings.MenuRename, Resources.Rename, rename_Click, Keys.F2),
				new ToolStripMenuItem(Strings.MenuDelete, Resources.Delete, delete_Click)
			});
        }

        public FolderNode(TemplateFolder folder)
        {
            templateFolder = folder;
            Text = templateFolder.Name;
            ImageKey = "folder";
            SelectedImageKey = "folder";
            templateFolder.Renamed += new EventHandler(templateFolder_Renamed);
        }

        void templateFolder_Renamed(object sender, EventArgs e)
        {
            Text = templateFolder.Name;
            if (TreeView != null)
                TreeView.Sort();
        }

        public override void LabelModified(NodeLabelEditEventArgs e)
        {
            TemplateFolder.Name = e.Label;

            if (TemplateFolder.Name != e.Label)
                e.CancelEdit = true;
        }

        public TemplateFolder TemplateFolder { get { return templateFolder; } }

        public override ContextMenuStrip ContextMenuStrip
        {
            get
            {
                contextMenu.Tag = this;
                return contextMenu;
            }
            set { base.ContextMenuStrip = value; }
        }

        #region Menu Click

        private static void newFile_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            var node = (FolderNode)menuItem.OwnerItem.Owner.Tag;
            var dir = node.TemplateFolder.AddNewTemplate();
            var newNode = new FileNode(dir);
            node.Nodes.Add(newNode);
            node.Expand();
            newNode.EditLabel();
        }
        private static void newFolder_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            var node = (FolderNode)menuItem.OwnerItem.Owner.Tag;
            var dir = node.TemplateFolder.AddNewFolder();
            var newNode = new FolderNode(dir);
            node.Nodes.Add(newNode);
            node.Expand();
            newNode.EditLabel();
        }

        private static void rename_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            FolderNode node = (FolderNode)menuItem.Owner.Tag;

            node.EditLabel();
        }

        private static void lookup_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            FolderNode node = (FolderNode)menuItem.Owner.Tag;

            node.TemplateFolder.LookupFolder();
        }

        private static void delete_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            var node = (FolderNode)menuItem.Owner.Tag;

            DialogResult result = Client.ShowConfirm(string.Format(Strings.DeleteItemConfirmation, node.TemplateFolder.Name));

            if (result == DialogResult.OK)
            {
                node.TemplateFolder.Delete();
                node.Delete();
            }
        }

        #endregion
    }
}
