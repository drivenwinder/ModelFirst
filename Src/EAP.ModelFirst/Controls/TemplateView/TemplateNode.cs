using System;
using System.Windows.Forms;
using EAP.ModelFirst.Core.Template;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Controls.TemplateView
{
    public class TemplateNode : FolderNode
    {
        static ContextMenuStrip contextMenu = new ContextMenuStrip();

        public TemplateInfo Template { get; protected set; }

        static TemplateNode()
        {
            contextMenu.Items.AddRange(new ToolStripItem[] {
				new ToolStripMenuItem(Strings.MenuAddNew, Resources.NewDocument,
					new ToolStripMenuItem(Strings.MenuTemplateFile, Resources.Template, newFile_Click),
				    new ToolStripMenuItem(Strings.MenuFolder, Resources.Folder, newFolder_Click)
				),
				new ToolStripMenuItem(Strings.MenuLocationFolder, Resources.Open, lookup_Click),
				new ToolStripSeparator(),
                new ToolStripMenuItem(Strings.MenuBrowser, null , browser_Click)
            });
        }

        public TemplateNode(TemplateFolder folder)
            : base(folder)
        {
            Text = Strings.Templates;
            ImageKey = "folder";
            SelectedImageKey = ImageKey;
            Template = new TemplateInfo(folder.FullName);
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

        private static void lookup_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            FolderNode node = (FolderNode)menuItem.Owner.Tag;

            node.TemplateFolder.LookupFolder();
        }

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

        private static void browser_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            var node = (TemplateNode)menuItem.Owner.Tag;
            using(FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select Template Folder";
                dialog.SelectedPath = Settings.Default.GetTemplateFolder().FullName;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    node.ToolTipText = dialog.SelectedPath;
                    Settings.Default.TemplateFolder = dialog.SelectedPath;
                }
            }
        }
        
        public class TemplateInfo
        {
            public TemplateInfo(string path)
            {
                Path = path;
            }

            public string Path { get; private set; }

            public override string ToString()
            {
                return Strings.Templates;
            }
        }
    }
}
