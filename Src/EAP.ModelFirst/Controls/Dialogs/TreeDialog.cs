using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project.Members;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Controls.Dialogs
{
	public partial class TreeDialog : DialogForm, ILocalizable
	{
		bool checkingLocked = false;

		public TreeDialog()
		{
            InitializeComponent();
            var font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            treOperations.StateNormal.Node.Content.ShortText.Font = font;
            treOperations.StateCommon.Node.Content.ShortText.Font = font;
            treOperations.StateDisabled.Node.Content.ShortText.Font = font;
            treOperations.StatePressed.Node.Content.ShortText.Font = font;
            treOperations.StatePressed.Node.Content.ShortText.Font = font;
			treOperations.ImageList = Icons.IconList;
		}

		protected TreeView OperationTree
		{
			get { return treOperations.TreeView; }
		}

        public IEnumerable<Operation> GetSelectedOperations()
        {
            for (int parent = 0; parent < treOperations.Nodes.Count; parent++) {
                TreeNode parentNode = treOperations.Nodes[parent];

                for (int child = 0; child < parentNode.Nodes.Count; child++) {
                    if (parentNode.Nodes[child].Tag is Operation &&
                        parentNode.Nodes[child].Checked) {
                        yield return (Operation) parentNode.Nodes[child].Tag;
                    }
                }
            }
        }

        /// <exception cref="ArgumentNullException">
		/// <paramref name="parentNode"/> is null.-or-
		/// <paramref name="operation"/> is null.
		/// </exception>
		protected TreeNode CreateOperationNode(TreeNode parentNode, Operation operation)
		{
			if (parentNode == null)
				throw new ArgumentNullException("parentNode");
			if (operation == null)
				throw new ArgumentNullException("operation");

			TreeNode child = parentNode.Nodes.Add(operation.GetUmlDescription());
			int imageIndex = Icons.GetImageIndex(operation);

			child.Tag = operation;
			child.ImageIndex = imageIndex;
			child.SelectedImageIndex = imageIndex;
			child.ToolTipText = operation.ToString();

			return child;
		}

		protected void RemoveEmptyNodes()
		{
			for (int i = 0; i < OperationTree.Nodes.Count; i++) {
				if (OperationTree.Nodes[i].Nodes.Count == 0)
					OperationTree.Nodes.RemoveAt(i--);
			}
		}

		public virtual void UpdateTexts()
		{
			btnOK.Text = Strings.ButtonOK;
			btnCancel.Text = Strings.ButtonCancel;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			UpdateTexts();
		}

		private void treMembers_AfterCheck(object sender, TreeViewEventArgs e)
		{
			if (!checkingLocked) {
				checkingLocked = true;

				TreeNode node = e.Node;
				TreeNode parentNode = e.Node.Parent;

				if (parentNode != null) {
					if (!node.Checked) {
						parentNode.Checked = false;
					}
					else {
						bool allChecked = true;

						foreach (TreeNode neighbour in parentNode.Nodes) {
							if (!neighbour.Checked) {
								allChecked = false;
								break;
							}
						}
						parentNode.Checked = allChecked;
					}
				}

				foreach (TreeNode child in node.Nodes)
					child.Checked = node.Checked;

				checkingLocked = false;
			}
		}
	}
}