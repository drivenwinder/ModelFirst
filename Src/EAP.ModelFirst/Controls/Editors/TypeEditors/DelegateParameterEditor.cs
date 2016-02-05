using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Properties;
using System.Windows.Forms;
using EAP.ModelFirst.Core.Project.Parameters;

namespace EAP.ModelFirst.Controls.Editors.TypeEditors
{
    public class DelegateParameterEditor : ListEditor
    {
        DelegateType parent = null;

        class DelegateParameterEditorHandler : ListEditorHandler
        {
            DelegateParameterEditor editor { get { return ListEditor as DelegateParameterEditor; } }
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

        public DelegateParameterEditor()
            : base(new DelegateParameterEditorHandler())
        {
        }

        void FillList()
        {
            lstItems.Items.Clear();
            foreach (Parameter value in parent.Arguments)
            {
                ListViewItem item = lstItems.Items.Add(value.ToString());

                item.Tag = value;
                item.ImageIndex = Icons.ParameterImageIndex;
            }
        }

        void AddToList(string text)
        {
            Parameter value = parent.AddParameter(text);
            ListViewItem item = lstItems.Items.Add(value.ToString());

            item.Tag = value;
            item.ImageIndex = Icons.ParameterImageIndex;
        }

        void Modify(ListViewItem item, string text)
        {
            if (item.Tag is Parameter)
            {
                Parameter parameter = parent.ModifyParameter((Parameter)item.Tag, text);
                item.Tag = parameter;
                item.Text = parameter.ToString();
            }
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
            if (item != null && item.Tag is Parameter)
                parent.RemoveParameter((Parameter)item.Tag);
            base.Remove(item);
        }

        public void SetDelegateType(DelegateType parent)
        {
            if (parent != null)
            {
                this.parent = parent;
                this.Text = string.Format(Strings.ItemsOfType, parent.Name);
                FillList();
            }
        }
    }
}
