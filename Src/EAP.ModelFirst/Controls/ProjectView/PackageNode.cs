using System;
using System.Windows.Forms;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;
using EAP.ModelFirst.Controls.Dialogs;
using EAP.ModelFirst.Controls.Explorers;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Controls.ProjectView
{
    public sealed class PackageNode : ContainerNode, IProjectItemNode
    {
        static ContextMenuStrip contextMenu = new ContextMenuStrip();

        static ToolStripMenuItem mnuClass = new ToolStripMenuItem("Class", Resources.Class, newClass_Click);
        static ToolStripMenuItem mnuStructure = new ToolStripMenuItem("Structure", Resources.Struct, newStructure_Click);
        static ToolStripMenuItem mnuInterface = new ToolStripMenuItem("Interface", Resources.Interface24, newInterface_Click);
        static ToolStripMenuItem mnuEnum = new ToolStripMenuItem("Enum", Resources.Enum, newEnum_Click);
        static ToolStripMenuItem mnuDelegate = new ToolStripMenuItem("Delegate", Resources.Delegate, newDelegate_Click);
        static PackageNode()
        {
            var mnuAddNew = new ToolStripMenuItem(Strings.MenuAddNew, Resources.NewEntity,
                    new ToolStripMenuItem("Diagram", Resources.Diagram, newDiagram_Click),
                    new ToolStripMenuItem("Package", Resources.Folder, newPackage_Click),
                    new ToolStripSeparator(),
                    mnuClass,
                    mnuStructure,
                    mnuInterface,
                    mnuEnum,
                    mnuDelegate
                );
            mnuAddNew.DropDownOpening += new EventHandler(mnuAddNew_DropDownOpening);
            contextMenu.Items.AddRange(new ToolStripItem[] {
				mnuAddNew,
				new ToolStripSeparator(),
                new ToolStripMenuItem(Strings.MenuGenerateCode, Resources.CodeGenerator, genCode_Click),
                new ToolStripSeparator(),
				new ToolStripMenuItem(Strings.MenuRename, Resources.Rename, rename_Click, Keys.F2),
				new ToolStripMenuItem(Strings.MenuDeleteProjectItem, Resources.Delete, delete_Click)
			});
        }

        static void mnuAddNew_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            PackageNode node = (PackageNode)menuItem.Owner.Tag;
            mnuDelegate.Visible = node.Package.ProjectInfo.Language.SupportsDelegates;
            mnuEnum.Visible = node.Package.ProjectInfo.Language.SupportsEnums;
            mnuInterface.Visible = node.Package.ProjectInfo.Language.SupportsInterfaces;
            mnuStructure.Visible = node.Package.ProjectInfo.Language.SupportsStructures;
        }

        public Package Package { get; private set; }

        public override PackageBase PackageBase { get { return Package; } }

        public PackageNode(Package package)
        {
            Text = package.Name;
            Package = package;
            ImageKey = "folder";
            SelectedImageKey = "folder";
            Package.ItemAdded += new ProjectItemEventHandler(Package_ItemAdded);
            Package.ItemRemoved += new ProjectItemEventHandler(Package_ItemRemoved);
            Package.Renamed += new EventHandler(Package_Renamed);
        }

        void Package_Renamed(object sender, EventArgs e)
        {
            Text = Package.Name;
            if (TreeView != null)
                TreeView.Sort();
        }

        private void Package_ItemAdded(object sender, ProjectItemEventArgs e)
        {
            AddProjectItemNode(e.ProjectItem);
        }

        private void Package_ItemRemoved(object sender, ProjectItemEventArgs e)
        {
            RemoveProjectItemNode(e.ProjectItem);
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

        public IProjectItem ProjectItem
        {
            get { return Package; }
        }

        public override void StateChanged()
        {
            Package.Collapsed = !IsExpanded;
        }

        public override void BeforeDelete()
        {
            Package.ItemAdded -= new ProjectItemEventHandler(Package_ItemAdded);
            Package.ItemRemoved -= new ProjectItemEventHandler(Package_ItemRemoved);
            base.BeforeDelete();
        }

        public override void LabelModified(NodeLabelEditEventArgs e)
        {
            Package.Name = e.Label;
            if (Package.Name != e.Label)
                e.CancelEdit = true;
        }

        #region Menu Click

        private static void genCode_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            PackageNode node = (PackageNode)menuItem.Owner.Tag;
            Generator.Show(node.Package.GetTypes());
        }

        private static void newPackage_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            Package package = ((PackageNode)menuItem.OwnerItem.Owner.Tag).Package;
            Package subPackage = Package.Create(Project.GetName(package, ProjectItemType.Package, "Package"));
            subPackage.IsUntitled = true;
            package.Add(subPackage);
        }

        private static void newClass_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            Package package = ((PackageNode)menuItem.OwnerItem.Owner.Tag).Package;

            var c = package.ProjectInfo.Language.CreateClass(Project.GetName(package,ProjectItemType.Type, "Class"));
            c.IsUntitled = true;
            package.Add(c);
        }

        private static void newStructure_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            Package package = ((PackageNode)menuItem.OwnerItem.Owner.Tag).Package;

            var c = package.ProjectInfo.Language.CreateStructure(Project.GetName(package, ProjectItemType.Type, "Structure"));
            c.IsUntitled = true;
            package.Add(c);
        }

        private static void newInterface_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            Package package = ((PackageNode)menuItem.OwnerItem.Owner.Tag).Package;

            var c = package.ProjectInfo.Language.CreateInterface(Project.GetName(package, ProjectItemType.Type, "Interface"));
            c.IsUntitled = true;
            package.Add(c);
        }

        private static void newEnum_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            Package package = ((PackageNode)menuItem.OwnerItem.Owner.Tag).Package;

            var c = package.ProjectInfo.Language.CreateEnum(Project.GetName(package, ProjectItemType.Type, "Enum"));
            c.IsUntitled = true;
            package.Add(c);
        }

        private static void newDelegate_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            Package package = ((PackageNode)menuItem.OwnerItem.Owner.Tag).Package;

            var c = package.ProjectInfo.Language.CreateDelegate(Project.GetName(package, ProjectItemType.Type, "Delegate"));
            c.IsUntitled = true;
            package.Add(c);
        }
        
        private static void newDiagram_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            Package package = ((PackageNode)menuItem.OwnerItem.Owner.Tag).Package;

            Diagram diagram = Diagram.Create(Project.GetName(package, ProjectItemType.Model, package.Name));
            diagram.IsUntitled = true;
            package.Add(diagram);
        }

        private static void rename_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            PackageNode node = (PackageNode)menuItem.Owner.Tag;

            node.EditLabel();
        }

        private static void delete_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            IProjectItem package = ((PackageNode)menuItem.Owner.Tag).Package;

            DialogResult result = Client.ShowConfirm(string.Format(Strings.DeleteItemConfirmation, package.Name));

            if (result == DialogResult.OK)
                package.Package.Remove(package);
        }

        #endregion
    }
}
