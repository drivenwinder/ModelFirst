using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Members;
using System.Windows.Forms;

namespace EAP.ModelFirst.Controls.Editors.TypeEditors
{
    public class EnumValueEditor : ListEditor
    {
        EnumType parent = null;

        class EnumEditorHandler : ListEditorHandler
        {
            EnumValueEditor editor { get { return ListEditor as EnumValueEditor; } }

            protected internal override void FillList()
            {
                editor.FillList();
            }

            protected internal override void AddToList(string text)
            {
                editor.AddToList(text);
            }

            protected internal override void Modify(ListViewItem item, string text)
            {
                editor.Modify(item, text);
            }
        }

        public EnumValueEditor()
            :base(new EnumEditorHandler())
        {
        }

        protected override void MoveUpItem(ListViewItem item)
        {
            if (item != null)
                parent.MoveUpItem(item.Tag);
            base.MoveUpItem(item);
        }

        protected override void MoveDownItem(ListViewItem item)
        {
            if (item != null)
                parent.MoveDownItem(item.Tag);
            base.MoveDownItem(item);
        }

        protected override void Remove(ListViewItem item)
        {
            if (item != null && item.Tag is EnumValue)
                parent.RemoveValue((EnumValue)item.Tag);
            base.Remove(item);
        }

        void FillList()
        {
            lstItems.Items.Clear();
            foreach (EnumValue value in parent.Values)
            {
                ListViewItem item = lstItems.Items.Add(value.ToString());

                item.Tag = value;
                item.ImageIndex = Icons.EnumItemImageIndex;
            }
        }

        void AddToList(string text)
        {
            EnumValue value = parent.AddValue(text);
            ListViewItem item = lstItems.Items.Add(value.ToString());

            item.Tag = value;
            item.ImageIndex = Icons.EnumItemImageIndex;
        }

        void Modify(ListViewItem item, string text)
        {
            if (item.Tag is EnumValue)
            {
                EnumValue enumItem = parent.ModifyValue((EnumValue)item.Tag, text);
                item.Tag = enumItem;
                item.Text = enumItem.ToString();
            }
        }

        public void SetEnumType(EnumType parent)
        {
            if (parent != null)
            {
                this.parent = parent;
                FillList();
            }
        }
    }
}
