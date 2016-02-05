using System;
using System.Windows.Forms;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Controls.Explorers;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Controls.ProjectView
{
    public sealed class ProjectNode : ContainerNode
    {
        static ContextMenuStrip contextMenu = new ContextMenuStrip();

        static ToolStripMenuItem mnuClass = new ToolStripMenuItem("Class", Resources.Class, newClass_Click);
        static ToolStripMenuItem mnuStructure = new ToolStripMenuItem("Structure", Resources.Struct, newStructure_Click);
        static ToolStripMenuItem mnuInterface = new ToolStripMenuItem("Interface", Resources.Interface24, newInterface_Click);
        static ToolStripMenuItem mnuEnum = new ToolStripMenuItem("Enum", Resources.Enum, newEnum_Click);
        static ToolStripMenuItem mnuDelegate = new ToolStripMenuItem("Delegate", Resources.Delegate, newDelegate_Click);

        static ProjectNode()
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
				new ToolStripMenuItem(Strings.MenuSave, Resources.Save, save_Click),
				new ToolStripMenuItem(Strings.MenuSaveAs, null, saveAs_Click),
				new ToolStripSeparator(),
				new ToolStripMenuItem(Strings.MenuRename, Resources.Rename, rename_Click, Keys.F2),
				new ToolStripMenuItem(Strings.MenuCloseProject, Resources.CloseProject, close_Click)
			});
        }

        static void mnuAddNew_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            ProjectNode node = (ProjectNode)menuItem.Owner.Tag;
            mnuDelegate.Visible = node.Project.Language.SupportsDelegates;
            mnuEnum.Visible = node.Project.Language.SupportsEnums;
            mnuInterface.Visible = node.Project.Language.SupportsInterfaces;
            mnuStructure.Visible = node.Project.Language.SupportsStructures;
        }

        public ProjectNode(Project project)
        {
            if (project == null)
                throw new ArgumentNullException("project");

            Project = project;
            Text = project.Name;

            AddProjectItems(Project);
            Project.ItemAdded += new ProjectItemEventHandler(Project_ItemAdded);
            Project.ItemRemoved += new ProjectItemEventHandler(Project_ItemRemoved);
            Project.Renamed += new EventHandler(Project_Renamed);
        }

        void Project_Renamed(object sender, EventArgs e)
        {
            Text = Project.Name;
            if (TreeView != null)
                TreeView.Sort();
        }

        public Project Project { get; private set; }

        public override ContextMenuStrip ContextMenuStrip
        {
            get
            {
                contextMenu.Tag = this;
                return contextMenu;
            }
            set { base.ContextMenuStrip = value; }
        }

        void AddProjectItems(Project project)
        {
            if (project.IsEmpty)
            {
                ItemNode node = new EmptyProjectNode(project);
                Nodes.Add(node);
                if (TreeView != null)
                    node.AfterInitialized();
            }
            else
            {
                foreach (IProjectItem projectItem in project.Items)
                    AddProjectItemNode(projectItem);
            }
        }

        void Project_ItemAdded(object sender, ProjectItemEventArgs e)
        {
            AddProjectItemNode(e.ProjectItem);
        }

        void Project_ItemRemoved(object sender, ProjectItemEventArgs e)
        {
            RemoveProjectItemNode(e.ProjectItem);
            if (Project.IsEmpty)
            {
                ItemNode node = new EmptyProjectNode(Project);
                Nodes.Add(node);
                if (TreeView != null)
                    node.AfterInitialized();
            }
        }

        public override void LabelModified(NodeLabelEditEventArgs e)
        {
            Project.Name = e.Label;

            if (Project.Name != e.Label)
                e.CancelEdit = true;
        }

        public override void BeforeDelete()
        {
            Project.ItemAdded -= new ProjectItemEventHandler(Project_ItemAdded);
            Project.ItemRemoved -= new ProjectItemEventHandler(Project_ItemRemoved);
            base.BeforeDelete();
        }

        public override void StateChanged()
        {
            Project.Collapsed = !IsExpanded;
        }

        #region Menu Click

        private static void newPackage_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            Project project = ((ProjectNode)menuItem.OwnerItem.Owner.Tag).Project;

            Package package = Package.Create(Project.GetName(project, ProjectItemType.Package, "Package"));
            package.IsUntitled = true;
            project.Add(package);
        }

        private static void newClass_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            Project project = ((ProjectNode)menuItem.OwnerItem.Owner.Tag).Project;

            var c = project.Language.CreateClass(Project.GetName(project, ProjectItemType.Type, "Class"));
            c.IsUntitled = true;
            project.Add(c);
        }

        private static void newStructure_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            Project project = ((ProjectNode)menuItem.OwnerItem.Owner.Tag).Project;

            var c = project.Language.CreateStructure(Project.GetName(project, ProjectItemType.Type, "Structure"));
            c.IsUntitled = true;
            project.Add(c);
        }

        private static void newInterface_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            Project project = ((ProjectNode)menuItem.OwnerItem.Owner.Tag).Project;

            var c = project.Language.CreateInterface(Project.GetName(project, ProjectItemType.Type, "Interface"));
            c.IsUntitled = true;
            project.Add(c);
        }

        private static void newEnum_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            Project project = ((ProjectNode)menuItem.OwnerItem.Owner.Tag).Project;

            var c = project.Language.CreateEnum(Project.GetName(project, ProjectItemType.Type, "Enum"));
            c.IsUntitled = true;
            project.Add(c);
        }

        private static void newDelegate_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            Project project = ((ProjectNode)menuItem.OwnerItem.Owner.Tag).Project;

            var c = project.Language.CreateDelegate(Project.GetName(project, ProjectItemType.Type, "Delegate"));
            c.IsUntitled = true;
            project.Add(c);
        }

        private static void newDiagram_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            Project project = ((ProjectNode)menuItem.OwnerItem.Owner.Tag).Project;

            Diagram diagram = Diagram.Create(Project.GetName(project, ProjectItemType.Model, project.Name));
            diagram.IsUntitled = true;
            project.Add(diagram);
        }

        private static void rename_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            ProjectNode node = (ProjectNode)menuItem.Owner.Tag;

            node.EditLabel();
        }

        private static void save_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            var node = (ProjectNode)menuItem.Owner.Tag;

            node.ItemView.DockForm.Workspace.SaveProject(node.Project);
        }

        private static void saveAs_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            var node = (ProjectNode)menuItem.Owner.Tag;

            node.ItemView.DockForm.Workspace.SaveProjectAs(node.Project);
        }

        private static void close_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            var node = (ProjectNode)menuItem.Owner.Tag;

            node.ItemView.DockForm.Workspace.RemoveProject(node.Project);
        }

        private static void genCode_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            ProjectNode node = (ProjectNode)menuItem.Owner.Tag;
            Generator.Show(node.Project.GetTypes());
        }

        #endregion

        public override PackageBase PackageBase
        {
            get { return Project; }
        }
    }
}
