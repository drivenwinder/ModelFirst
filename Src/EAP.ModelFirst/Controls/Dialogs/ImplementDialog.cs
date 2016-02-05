using System;
using System.Drawing;
using System.Windows.Forms;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Members;

namespace EAP.ModelFirst.Controls.Dialogs
{
	public partial class ImplementDialog : TreeDialog
	{
		CheckBox chkImplementExplicitly = new CheckBox();

		public ImplementDialog()
		{
			chkImplementExplicitly.AutoSize = true;
			chkImplementExplicitly.Location = new Point(12, 284);
			chkImplementExplicitly.Text = Strings.ImplementExplicitly;
			chkImplementExplicitly.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
			this.Controls.Add(chkImplementExplicitly);
		}

		public bool ImplementExplicitly
		{
			get { return chkImplementExplicitly.Checked; }
		}

		public override void UpdateTexts()
		{
			this.Text = Strings.Implementing;
			base.UpdateTexts();
		}

		private TreeNode CreateInterfaceNode(string interfaceName)
		{
			TreeNode node = OperationTree.Nodes.Add(interfaceName);
			node.SelectedImageIndex = Icons.InterfaceImageIndex;
			node.ImageIndex = Icons.InterfaceImageIndex;

			return node;
		}

		private void AddOperations(IInterfaceImplementer implementer,
			InterfaceType _interface, TreeNode node)
		{
			if (implementer == null || _interface == null || node == null)
				return;

			foreach (InterfaceType baseInterface in _interface.Bases)
				AddOperations(implementer, baseInterface, node);

			foreach (Operation operation in _interface.Operations) {
				Operation defined = implementer.GetDefinedOperation(operation);

				if (defined == null) {
					CreateOperationNode(node, operation);
				}
				else if (defined.Type != operation.Type &&
                    _interface.Language.SupportsExplicitImplementation)
				{
					TreeNode operationNode = CreateOperationNode(node, operation);
					operationNode.ForeColor = Color.Gray;
				}
			}
		}

		private void AddInterface(IInterfaceImplementer implementer, InterfaceType _interface)
		{
			if (implementer == null || _interface == null)
				return;

			TreeNode node = CreateInterfaceNode(_interface.Name);
			AddOperations(implementer, _interface, node);
		}

		public DialogResult ShowDialog(IInterfaceImplementer implementer)
		{
			if (implementer == null)
				return DialogResult.None;

			chkImplementExplicitly.Checked = false;
            chkImplementExplicitly.Visible = (implementer.Language.SupportsExplicitImplementation);

			OperationTree.Nodes.Clear();
			foreach (InterfaceType _interface in implementer.Interfaces)
				AddInterface(implementer, _interface);
			RemoveEmptyNodes();

			return ShowDialog();
		}
	}
}