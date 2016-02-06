using System;
using System.Linq;
using System.Windows.Forms;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Controls.ProjectView;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Controls.Explorers
{
    public partial class ProjectExplorer : ExplorerBase, IPropertyConfigurable
    {
        static ProjectExplorer instance;

        static ProjectExplorer Instance
        {
            get
            {
                if (instance == null)
                    instance = new ProjectExplorer();
                return instance;
            }
        }

        public static void Show(IDockForm dockForm)
        {
            LoadProjects(dockForm);
            Instance.Show(dockForm.DockPanel);
        }

        public override IDockForm DockForm
        {
            get { return base.DockForm; }
            set
            {
                view.DockForm = value;
                base.DockForm = value;
            }
        }

        protected ProjectExplorer()
        {
            InitializeComponent();
            UpdateTexts();
        }

        bool isLoaded;

        public static void LoadProjects(IDockForm dockForm)
        {
            Instance.DockForm = dockForm;
            if (Instance.isLoaded) return;
            Instance.view.LoadProjects(dockForm.Workspace.Projects);
            dockForm.Workspace.ProjectAdded += Instance.workspace_ProjectAdded;
            dockForm.Workspace.ProjectRemoved += Instance.workspace_ProjectRemoved;
            Instance.isLoaded = true;
        }

        private void workspace_ProjectAdded(object sender, ProjectEventArgs e)
        {
            view.AddProject(e.Project);
        }

        private void workspace_ProjectRemoved(object sender, ProjectEventArgs e)
        {
            view.RemoveProject(e.Project);
        }

        private void ProjectExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public override void UpdateTexts()
        {
            Text = Strings.ProjectExplorer;
            TabText = Strings.ProjectExplorer;
            base.UpdateTexts();
        }

        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            view.LoadProjects(DockForm.Workspace.Projects);
        }

        private void view_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is ContainerNode)
            {
                btnEdit.Visible = false;
                btnView.Visible = false;
            }
            else if (e.Node is TypeNode)
            {
                btnEdit.Visible = true;
                btnView.Visible = false;
            }
            else if (e.Node is DiagramNode)
            {
                btnEdit.Visible = false;
                btnView.Visible = true;
            }
            PropertyWindow.SetPropertyObject(PropertyObject);
        }

        private void btnGenerate_Click(object sender, System.EventArgs e)
        {
            if (view.SelectedNode is ContainerNode)
            {
                var node = (ContainerNode)view.SelectedNode;
                Generator.Show(node.PackageBase.GetTypes());
            }
            else if (view.SelectedNode is TypeNode)
            {
                var node = (TypeNode)view.SelectedNode;
                Generator.Show(new[] { node.TypeBase });
            }
            else if (view.SelectedNode is DiagramNode)
            {
                var node = (DiagramNode)view.SelectedNode;
                var types = node.Diagram.Entities.OfType<TypeBase>();
                Generator.Show(types);
            }
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            if (view.SelectedNode is TypeNode)
            {
                var node = (TypeNode)view.SelectedNode;
                DockForm.OpenDocument(node.TypeBase);
            }
        }

        private void btnView_Click(object sender, System.EventArgs e)
        {
            if (view.SelectedNode is DiagramNode)
            {
                var node = (DiagramNode)view.SelectedNode;
                DockForm.OpenDocument(node.Diagram);
            }
        }

        public object PropertyObject
        {
            get
            {
                if (view.SelectedNode is ContainerNode)
                {
                    var node = (ContainerNode)view.SelectedNode;
                    return node.PackageBase;
                }
                else if (view.SelectedNode is TypeNode)
                {
                    var node = (TypeNode)view.SelectedNode;
                    return node.TypeBase;
                }
                else if (view.SelectedNode is DiagramNode)
                {
                    var node = (DiagramNode)view.SelectedNode;
                    return node.Diagram;
                }
                return null;
            }
        }
    }
}
