using System;
using System.Windows.Forms;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Controls.Editors.TypeEditors
{
    public partial class ListEditor : UserControl, ILocalizable
    {
        public event EditValueChangedEventHandler ValueChanged;

        public bool Changed { get; private set; }

        protected ListEditorHandler EditorHandler { get; private set; }

        private ListEditor()
        {
            InitializeComponent();
        }

        public ListEditor(ListEditorHandler handler)
            : this()
        {
            EditorHandler = handler;
            EditorHandler.ListEditor = this;
            lstItems.SmallImageList = Icons.IconList;
        }

        private void OnValueChanged(Control control)
        {
            if (ValueChanged != null)
                ValueChanged.Invoke(this, new EditValueChangedEventArgs(new EditorInfo(control)));
        }

        protected virtual void MoveUpItem(ListViewItem item)
        {
            if (item != null)
            {
                int index = item.Index;

                if (index > 0)
                {
                    ListViewItem item2 = lstItems.Items[index - 1];

                    SwapListItems(item, item2);
                    item2.Focused = true;
                    item2.Selected = true;
                    Changed = true;
                    OnValueChanged(lstItems);
                }
            }
        }

        protected virtual void MoveDownItem(ListViewItem item)
        {
            if (item != null)
            {
                int index = item.Index;

                if (index < lstItems.Items.Count - 1)
                {
                    ListViewItem item2 = lstItems.Items[index + 1];

                    SwapListItems(item, item2);
                    item2.Focused = true;
                    item2.Selected = true;
                    Changed = true;
                    OnValueChanged(lstItems);
                }
            }
        }

        protected virtual void Remove(ListViewItem item)
        {
            lstItems.Items.Remove(item);
            Changed = true;
            OnValueChanged(lstItems);
        }

        private void SwapListItems(ListViewItem item1, ListViewItem item2)
        {
            string text = item1.Text;
            item1.Text = item2.Text;
            item2.Text = text;

            object tag = item1.Tag;
            item1.Tag = item2.Tag;
            item2.Tag = tag;

            Changed = true;
        }

        private void Accept()
        {
            try
            {
                if (lstItems.SelectedItems.Count == 0)
                    EditorHandler.AddToList(txtItem.Text);
                else
                    EditorHandler.Modify(lstItems.SelectedItems[0], txtItem.Text);

                ClearInput();
                Changed = true;
                OnValueChanged(lstItems);
                txtItem.Focus();
            }
            catch (BadSyntaxException ex)
            {
                errorProvider.SetIconPadding(toolStrip, -(lblItemCaption.Width + txtItem.Width));
                errorProvider.SetError(toolStrip, ex.Message);
            }
        }

        private void ItemSelected()
        {
            toolMoveUp.Enabled = true;
            toolMoveDown.Enabled = true;
            toolDelete.Enabled = true;
            btnAccept.Text = Strings.ButtonModify;
            lblItemCaption.Text = Strings.ModifyItem;
            txtItem.Text = lstItems.SelectedItems[0].Text;
            errorProvider.SetError(toolStrip, null);
        }

        private void ClearInput()
        {
            toolMoveUp.Enabled = false;
            toolMoveDown.Enabled = false;
            toolDelete.Enabled = false;
            btnAccept.Text = Strings.ButtonAddItem;
            lblItemCaption.Text = Strings.AddNewItem;
            txtItem.Text = null;
            errorProvider.SetError(toolStrip, null);
            if (lstItems.SelectedItems.Count > 0)
                lstItems.SelectedItems[0].Selected = false;
        }

        private void DeleteSelectedItem()
        {
            if (lstItems.SelectedItems.Count > 0)
            {
                int index = lstItems.SelectedItems[0].Index;

                Remove(lstItems.SelectedItems[0]);
                OnValueChanged(lstItems);
                int count = lstItems.Items.Count;
                if (count > 0)
                {
                    if (index >= count)
                        index = count - 1;
                    lstItems.Items[index].Selected = true;
                }
                else
                {
                    txtItem.Focus();
                }
            }
        }

        public void UpdateTexts()
        {
            btnAccept.Text = Strings.ButtonAddItem;
            lblItemCaption.Text = Strings.AddNewItem;
            toolMoveUp.Text = Strings.MoveUp;
            toolMoveDown.Text = Strings.MoveDown;
            toolDelete.Text = Strings.Delete;
            lstItems.Columns[0].Text = Strings.Item;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateTexts();
            ClearInput();
            txtItem.Select();
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Accept();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
                ClearInput();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            Accept();
        }

        private void lstItems_ItemSelectionChanged(object sender,
            ListViewItemSelectionChangedEventArgs e)
        {
            if (lstItems.SelectedItems.Count == 0)
                ClearInput();
            else
                ItemSelected();
        }

        private void lstItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteSelectedItem();
        }

        private void toolMoveUp_Click(object sender, EventArgs e)
        {
            if (lstItems.SelectedItems.Count > 0)
                MoveUpItem(lstItems.SelectedItems[0]);
        }

        private void toolMoveDown_Click(object sender, EventArgs e)
        {
            if (lstItems.SelectedItems.Count > 0)
                MoveDownItem(lstItems.SelectedItems[0]);
        }

        private void toolDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedItem();
        }
    }
}
