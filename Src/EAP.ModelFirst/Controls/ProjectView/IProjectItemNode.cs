using System.Windows.Forms;
using EAP.ModelFirst.Core.Project;

namespace EAP.ModelFirst.Controls.ProjectView
{
    public interface IProjectItemNode 
    {
        IProjectItem ProjectItem { get; }

        void LabelModified(NodeLabelEditEventArgs e);
    }
}
