using System.Windows.Forms;
using EAP.ModelFirst.Core;

namespace EAP.ModelFirst.Controls
{
	public abstract class ItemNode : TreeNode
	{
		bool editingLabel = false;
		bool deleted = false;

		protected ItemNode()
		{
		}

        public DocumentItemTreeView ItemView
		{
            get
            {
                if (TreeView == null)
                    return null;
                return TreeView.Tag as DocumentItemTreeView;
            }
		}

        public bool EditingLabel
        {
            get { return editingLabel; }
        }

		public virtual void BeforeDelete()
		{
			foreach (ItemNode node in Nodes)
			{
				node.BeforeDelete();
			}
		}

		public void Delete()
		{
			if (!deleted)
			{
				BeforeDelete();
				Remove();
				deleted = true;
			}
		}

        public void EditLabel()
        {
            if (!editingLabel)
                editingLabel = true;
            this.BeginEdit();
        }

        internal void LabelEdited()
        {
            editingLabel = false;
        }

        public virtual void Click()
        {
            if (IsSelected)
                editingLabel = true;
        }

		public virtual void LabelModified(NodeLabelEditEventArgs e)
		{
		}

		public virtual void DoubleClick()
		{
		}

		public virtual void EnterPressed()
		{
		}

        public virtual void StateChanged()
        {

        }
        
		protected internal virtual void AfterInitialized()
		{
			foreach (ItemNode node in Nodes)
				node.AfterInitialized();
		}
    }
}
