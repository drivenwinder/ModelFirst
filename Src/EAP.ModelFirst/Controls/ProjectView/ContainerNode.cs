using System.Windows.Forms;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Controls.ProjectView
{
    public abstract class ContainerNode : ItemNode
    {
        public abstract PackageBase PackageBase { get; }
        
        protected ItemNode AddProjectItemNode(IProjectItem projectItem)
        {
            ItemNode node = null;

            if (projectItem is Diagram)
            {
                var diagram = (Diagram)projectItem;
                node = new DiagramNode(diagram);
                if (ItemView != null)
                    ItemView.DockForm.OpenDocument(diagram);
            }
            else if (projectItem is Package)
            {
                var package = (Package)projectItem;
                node = new PackageNode(package);
                foreach (IProjectItem i in package.Items)
                {
                    ((PackageNode)node).AddProjectItemNode(i);
                }
                if (!package.Collapsed)
                    node.Expand();
            }
            else if (projectItem is TypeBase)
            {
                var type = (TypeBase)projectItem;
                node = new TypeNode(type);
            }
            // More kind of items might be possible later...

            if (node != null)
            {
                Nodes.Add(node);
                if (TreeView != null)
                {
                    node.AfterInitialized();
                    TreeView.SelectedNode = node;
                }
                if (projectItem.IsUntitled)
                    node.EditLabel();
            }
            return node;
        }

        protected void RemoveProjectItemNode(IProjectItem projectItem)
        {
            foreach (ItemNode node in Nodes)
            {
                if (node is IProjectItemNode && ((IProjectItemNode)node).ProjectItem == projectItem)
                {
                    node.Delete();
                    return;
                }
            }
        }
    }
}
