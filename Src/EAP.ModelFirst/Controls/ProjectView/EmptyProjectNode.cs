using System;
using System.Drawing;
using System.Windows.Forms;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Controls.ProjectView
{
	public sealed class EmptyProjectNode : ItemNode
	{
		Project project;

		/// <exception cref="ArgumentNullException">
		/// <paramref name="project"/> is null.
		/// </exception>
		public EmptyProjectNode(Project project)
		{
			if (project == null)
				throw new ArgumentNullException("project");

			this.project = project;
			project.ItemAdded += new ProjectItemEventHandler(project_ItemAdded);

			this.Text = Strings.DoubleClickToAddDiagram;
			this.ImageKey = "diagram";
			this.SelectedImageKey = "diagram";
		}

		protected internal override void AfterInitialized()
		{
			base.AfterInitialized();
			NodeFont = new Font(TreeView.Font, FontStyle.Italic);
		}

		private void project_ItemAdded(object sender, ProjectItemEventArgs e)
		{
			this.Delete();
		}

		public override void LabelModified(NodeLabelEditEventArgs e)
		{
			e.CancelEdit = true;
		}

		private void AddEmptyDiagram()
		{
			TreeNode parent = Parent;

			this.Delete();
            Diagram diagram = Diagram.Create(Project.GetName(project, ProjectItemType.Model, "Diagram"));
			project.Add(diagram);
		}

		public override void DoubleClick()
		{
			AddEmptyDiagram();
		}

		public override void EnterPressed()
		{
			AddEmptyDiagram();
		}

		public override void BeforeDelete()
		{
			project.ItemAdded -= new ProjectItemEventHandler(project_ItemAdded);
			NodeFont.Dispose();
			base.BeforeDelete();
		}
	}
}
