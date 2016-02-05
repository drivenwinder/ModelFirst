using System;
using System.Windows.Forms;
using EAP.ModelFirst.Core.Template;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;

namespace EAP.ModelFirst.Controls.TemplateView
{
    public class FileNode : ItemNode
    {
        TemplateFile templateFile;
        
        static ContextMenuStrip contextMenu = new ContextMenuStrip();

        static FileNode()
		{
			contextMenu.Items.AddRange(new ToolStripItem[] {
				new ToolStripMenuItem(Strings.MenuOpen, Resources.View, open_Click),
				new ToolStripMenuItem(Strings.MenuOpenFileFolder, Resources.Open, lookup_Click),
                new ToolStripSeparator(),
				new ToolStripMenuItem(Strings.MenuRename, Resources.Rename, rename_Click, Keys.F2),
				new ToolStripMenuItem(Strings.MenuDelete, Resources.Delete, delete_Click)
			});
		}

        public FileNode(TemplateFile file)
        {
            templateFile = file;
            Text = templateFile.Name;
            ImageKey = "template";
            SelectedImageKey = "template";
            templateFile.Renamed += new EventHandler(templateFile_Renamed);
        }

        void templateFile_Renamed(object sender, EventArgs e)
        {
            Text = templateFile.Name;
            if (TreeView != null)
                TreeView.Sort();
        }

        public TemplateFile TemplateFile { get { return templateFile; } }

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

        private static void lookup_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            FileNode node = (FileNode)menuItem.Owner.Tag;

            node.TemplateFile.LookupFile();
        }

        private static void rename_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            FileNode node = (FileNode)menuItem.Owner.Tag;

            node.EditLabel();
        }

        private static void open_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            FileNode node = (FileNode)menuItem.Owner.Tag;

            node.ItemView.DockForm.OpenDocument(node.TemplateFile);
        }

        private static void delete_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            var node = ((FileNode)menuItem.Owner.Tag);

            DialogResult result = Client.ShowConfirm(Strings.DeleteItemConfirmation.FormatArgs(node.TemplateFile.Name));

            if (result == DialogResult.OK)
            {
                node.TemplateFile.Delete();
                node.Delete();
            }
        }

        #endregion

        public override void DoubleClick()
        {
            if (ItemView != null)
                ItemView.DockForm.OpenDocument(templateFile);
        }

        public override void BeforeDelete()
        {
            templateFile.Close();
            base.BeforeDelete();
        }

        public override void LabelModified(NodeLabelEditEventArgs e)
        {
            TemplateFile.Name =  e.Label;

            if (TemplateFile.Name != e.Label)
                e.CancelEdit = true;
        }
    }
}
