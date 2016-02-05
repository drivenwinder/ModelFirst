using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Members;

namespace EAP.ModelFirst.Controls.Dialogs
{
	public partial class OverrideDialog : TreeDialog
	{
		public override void UpdateTexts()
		{
			this.Text = Strings.OverrideMembers;
			base.UpdateTexts();
		}

		private TreeNode CreateClassNode(string className)
		{
			TreeNode node = OperationTree.Nodes.Add(className);
			node.SelectedImageIndex = Icons.ClassImageIndex;
			node.ImageIndex = Icons.ClassImageIndex;

			return node;
		}

		private void RemoveSimilarNode(Operation operation)
		{
			if (operation == null)
				return;

			for (int i = 0; i < OperationTree.Nodes.Count; i++) {
				for (int j = 0; j < OperationTree.Nodes[i].Nodes.Count; j++) {
					if (operation.HasSameSignatureAs(
						OperationTree.Nodes[i].Nodes[j].Tag as Operation))
					{
						OperationTree.Nodes[i].Nodes.RemoveAt(j);
						break;
					}
				}
			}
		}

		private void AddOperations(SingleInharitanceType derivedClass,
			SingleInharitanceType baseClass)
		{
			if (derivedClass == null || baseClass == null)
				return;

			AddOperations(derivedClass, baseClass.Base);

			TreeNode node = CreateClassNode(baseClass.Name);
			foreach (Operation operation in baseClass.OverridableOperations) {
				if (derivedClass.GetDefinedOperation(operation) != null)
					continue;
				RemoveSimilarNode(operation);
				CreateOperationNode(node, operation);
			}
		}

		public DialogResult ShowDialog(SingleInharitanceType inheritedClass)
		{
			if (inheritedClass == null)
				return DialogResult.None;

			OperationTree.Nodes.Clear();
			AddOperations(inheritedClass, inheritedClass.Base);
			RemoveEmptyNodes();

			return ShowDialog();
		}
	}
}