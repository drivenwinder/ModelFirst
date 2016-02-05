using System;
using System.Windows.Forms;
using EAP.ModelFirst.Controls.Dialogs;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Members;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Controls.Editors.TypeEditors
{
    public partial class MemberListEditor : UserControl, ILocalizable
    {
        CompositeType parent = null;
        bool locked = false;
        int attributeCount = 0;
        Member currentMember = null;
        public MemberType NewMemberType = MemberType.Field;

        protected internal bool Error
        {
            get { return memberEditor.Error || schemaEditor.Error; }
        }

        public event EditValueChangedEventHandler ValueChanged;

        public MemberListEditor()
        {
            InitializeComponent();
            UpdateTexts();
            lstMembers.SmallImageList = Icons.IconList;
            memberEditor.ValueChanged += new EditValueChangedEventHandler(memberValue_ValueChanged);
            schemaEditor.ValueChanged += new EditValueChangedEventHandler(memberValue_ValueChanged);
        }

        private void OnValueChanged(Control control)
        {
            if (ValueChanged != null)
            {
                ListViewItem item = GetFocusedItem();
                ListEditorInfo info = new ListEditorInfo(lstMembers, item == null ? 0 : item.Index, control);
                ValueChanged.Invoke(this, new EditValueChangedEventArgs(info));
            }
        }

        ListViewItem GetFocusedItem()
        {
            ListViewItem item = null;

            if (lstMembers.FocusedItem != null)
                item = lstMembers.FocusedItem;
            else if (lstMembers.SelectedItems.Count > 0)
                item = lstMembers.SelectedItems[0];

            return item;
        }

        void memberValue_ValueChanged(object sender, EditValueChangedEventArgs e)
        {
            SetValue();
            OnValueChanged(e.Editor.Control);
        }

        void SetValue()
        {
            ListViewItem item = GetFocusedItem();
            if (item == null) return;
            var member = item.Tag as Member;
            if (member == null) return;
            item.ImageIndex = Icons.GetImageIndex(member);
            item.SubItems[1].Text = member.Name;
            item.SubItems[2].Text = member.Type;
            item.SubItems[3].Text = member.Language.GetAccessString(member.AccessModifier);
            if (member is Field)
            {
                var field = (Field)member;
                item.SubItems[4].Text = member.Language.GetFieldModifierString(field.Modifier);
                item.SubItems[5].Text = field.GenerateDbColumn.ToString();
                item.SubItems[6].Text = field.DbSchema.Name;
                item.SubItems[7].Text = field.DbSchema.DbType.ToSafeString();
                item.SubItems[8].Text = field.DbSchema.Length;
                item.SubItems[9].Text = field.DbSchema.NotNull.ToString();
                item.SubItems[10].Text = field.DbSchema.IsPrimaryKey.ToString();
                item.SubItems[11].Text = field.DbSchema.Index.ToString();
                item.SubItems[12].Text = field.DbSchema.AutoIncrement.ToString();
                item.SubItems[13].Text = field.DbSchema.DefaultValue;
                item.SubItems[14].Text = field.Comments;
            }
            else if (member is Operation)
            {
                item.SubItems[4].Text = member.Language.GetOperationModifierString(((Operation)member).Modifier);
                item.SubItems[5].Text = "";
                item.SubItems[6].Text = "";
                item.SubItems[7].Text = "";
                item.SubItems[8].Text = "";
                item.SubItems[9].Text = "";
                item.SubItems[10].Text = "";
                item.SubItems[11].Text = "";
                item.SubItems[12].Text = "";
                item.SubItems[13].Text = "";
                item.SubItems[14].Text = member.Comments;
            }
            RefreshMember(member);
        }

        public void SetCompositeType(CompositeType type)
        {
            parent = type;
            FillMembersList();
            if (lstMembers.Items.Count == 0)
            {
                RefreshMember(null);
            }
            else if (lstMembers.FocusedItem == null)
            {
                try
                {
                    //lstMembers.Focus();
                    lstMembers.Items[0].Focused = true;
                    lstMembers.Items[0].Selected = true;
                }
                catch { }
            }
            
            toolNewField.Visible = parent.SupportsFields;
            toolNewConstructor.Visible = parent.SupportsConstuctors;
            toolNewDestructor.Visible = parent.SupportsDestructors;
            toolNewProperty.Visible = parent.SupportsProperties;
            toolNewEvent.Visible = parent.SupportsEvents;
            toolOverrideList.Visible = parent is SingleInharitanceType;
            toolImplementList.Visible = parent is IInterfaceImplementer;
            toolImplementList.Enabled = (parent is IInterfaceImplementer) &&
                ((IInterfaceImplementer)parent).ImplementsInterface;
            toolSepAddNew.Visible = parent is SingleInharitanceType ||
                parent is IInterfaceImplementer;
        }

        void RefreshMember(Member actualMember)
        {
            if (locked)
                return;
            memberEditor.SetMember(actualMember);
            schemaEditor.SetMember(actualMember);

        }

        void SetFocus()
        {
            if (currentMember == null) return;
            foreach (ListViewItem i in lstMembers.Items)
            {
                if (i.Tag == currentMember)
                {
                    lstMembers.Focus();
                    i.Focused = true;
                    i.Selected = true;
                    break;
                }
            }
        }

        void DeleteSelectedMember()
        {
            if (lstMembers.SelectedItems.Count > 0)
            {
                ListViewItem item = lstMembers.SelectedItems[0];
                int index = item.Index;

                if (item.Tag is Field)
                    attributeCount--;
                parent.RemoveMember(item.Tag as Member);
                lstMembers.Items.Remove(item);

                int count = lstMembers.Items.Count;
                if (count > 0)
                {
                    if (index >= count)
                        index = count - 1;
                    try
                    {
                        //lstMembers.Focus();
                        lstMembers.Items[index].Selected = true;
                    }
                    catch { }
                }
                else
                {
                    RefreshMember(null);
                }
            }
        }

        void FillMembersList()
        {
            ListViewItem item = GetFocusedItem();
            int index = 0;
            if (item != null)
                index = item.Index;
            lstMembers.Items.Clear();
            attributeCount = 0;

            foreach (Field field in parent.Fields)
                AddFieldToList(field);

            foreach (Operation operation in parent.Operations)
                AddOperationToList(operation);

            if (index < lstMembers.Items.Count)
            {
                lstMembers.Items[index].Focused = true;
                try
                {
                    //lstMembers.Focus();
                    lstMembers.Items[index].Selected = true;
                }
                catch { }
            }
        }

        public void AddNewMember()
        {
            switch (NewMemberType)
            {
                case MemberType.Constructor:
                    toolNewField_Click(toolNewField, EventArgs.Empty);
                    break;
                case MemberType.Destructor:
                    toolNewDestructor_Click(toolNewConstructor, EventArgs.Empty);
                    break;
                case MemberType.Event:
                    toolNewEvent_Click(toolNewEvent, EventArgs.Empty);
                    break;
                case MemberType.Field:
                    toolNewField_Click(toolNewField, EventArgs.Empty);
                    break;
                case MemberType.Method:
                    toolNewMethod_Click(toolNewMethod, EventArgs.Empty);
                    break;
                case MemberType.Property:
                    toolNewProperty_Click(toolNewProperty, EventArgs.Empty);
                    break;
            }
        }

        //void AddNewItem(ListViewItem item)
        //{
        //    try
        //    {
        //        item.Focused = true;
        //        item.Selected = true;
        //    }
        //    catch { }
        //}

        //void AddNewField(Field field)
        //{
        //    ListViewItem item = AddFieldToList(field);
        //    NewMemberType = MemberType.Field;
        //    AddNewItem(item);
        //}

        //void AddNewOperation(Operation operation)
        //{
        //    ListViewItem item = AddOperationToList(operation);
        //    NewMemberType = operation.MemberType;
        //    AddNewItem(item);
        //}

        ListViewItem AddFieldToList(Field field)
        {
            if (field == null)
                throw new ArgumentNullException("field");

            ListViewItem item = lstMembers.Items.Insert(attributeCount, "");

            item.Tag = field;
            item.ImageIndex = Icons.GetImageIndex(field);
            item.SubItems.Add(field.Name);
            item.SubItems.Add(field.Type);
            item.SubItems.Add(field.Language.GetAccessString(field.AccessModifier));
            item.SubItems.Add(field.Language.GetFieldModifierString(field.Modifier));
            item.SubItems.Add(field.GenerateDbColumn.ToString());
            item.SubItems.Add(field.DbSchema.Name);
            item.SubItems.Add(field.DbSchema.DbType.ToSafeString());
            item.SubItems.Add(field.DbSchema.Length);
            item.SubItems.Add(field.DbSchema.NotNull.ToString());
            item.SubItems.Add(field.DbSchema.IsPrimaryKey.ToString());
            item.SubItems.Add(field.DbSchema.Index.ToString());
            item.SubItems.Add(field.DbSchema.AutoIncrement.ToString());
            item.SubItems.Add(field.DbSchema.DefaultValue);
            item.SubItems.Add(field.Comments);
            attributeCount++;

            return item;
        }

        ListViewItem AddOperationToList(Operation operation)
        {
            if (operation == null)
                throw new ArgumentNullException("operation");

            ListViewItem item = lstMembers.Items.Add("");

            item.Tag = operation;
            item.ImageIndex = Icons.GetImageIndex(operation);
            item.SubItems.Add(operation.Name);
            item.SubItems.Add(operation.Type);
            item.SubItems.Add(parent.Language.GetAccessString(operation.AccessModifier));
            item.SubItems.Add(parent.Language.GetOperationModifierString(operation.Modifier));
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add(operation.Comments);
            return item;
        }

        void SwapListItems(ListViewItem item1, ListViewItem item2)
        {
            int image = item1.ImageIndex;
            item1.ImageIndex = item2.ImageIndex;
            item2.ImageIndex = image;

            object tag = item1.Tag;
            item1.Tag = item2.Tag;
            item2.Tag = tag;

            for (int i = 0; i < item1.SubItems.Count; i++)
            {
                string text = item1.SubItems[i].Text;
                item1.SubItems[i].Text = item2.SubItems[i].Text;
                item2.SubItems[i].Text = text;
            }
        }

        private void toolNewField_Click(object sender, EventArgs e)
        {
            if (parent.SupportsFields && Validate())
            {
                currentMember = parent.AddField();                
                OnValueChanged(lstMembers);
                SetFocus();
            }
        }

        private void toolNewMethod_Click(object sender, EventArgs e)
        {
            if (!Validate()) return;
            currentMember = parent.AddMethod();
            OnValueChanged(lstMembers);
            SetFocus();
        }

        private void toolNewConstructor_Click(object sender, EventArgs e)
        {
            if (parent.SupportsConstuctors && Validate())
            {
                currentMember = parent.AddConstructor();
                OnValueChanged(lstMembers);
                SetFocus();
            }
        }

        private void toolNewDestructor_Click(object sender, EventArgs e)
        {
            if (parent.SupportsDestructors && Validate())
            {
                currentMember = parent.AddDestructor();
                OnValueChanged(lstMembers);
                SetFocus();
            }
        }

        private void toolNewProperty_Click(object sender, EventArgs e)
        {
            if (parent.SupportsProperties && Validate())
            {
                currentMember = parent.AddProperty();
                OnValueChanged(lstMembers);
                SetFocus();
            }
        }

        private void toolNewEvent_Click(object sender, EventArgs e)
        {
            if (parent.SupportsEvents && Validate())
            {
                currentMember = parent.AddEvent();
                OnValueChanged(lstMembers);
                SetFocus();
            }
        }

        private void toolOverrideList_Click(object sender, EventArgs e)
        {
            if (parent is SingleInharitanceType && Validate())
            {
                SingleInharitanceType derivedType = (SingleInharitanceType)parent;
                using (OverrideDialog dialog = new OverrideDialog())
                {
                    if (dialog.ShowDialog(derivedType) == DialogResult.OK)
                    {
                        foreach (Operation operation in dialog.GetSelectedOperations())
                        {
                            Operation overridden = (derivedType).Override(operation);
                            AddOperationToList(overridden);
                        }
                        OnValueChanged(lstMembers);
                    }
                }
            }
        }

        private void toolImplementList_Click(object sender, EventArgs e)
        {
            if (parent is IInterfaceImplementer && Validate())
            {
                using (ImplementDialog dialog = new ImplementDialog())
                {
                    if (dialog.ShowDialog(parent as IInterfaceImplementer) == DialogResult.OK)
                    {
                        foreach (Operation operation in dialog.GetSelectedOperations())
                        {
                            Implement((IInterfaceImplementer)parent, operation,
                                dialog.ImplementExplicitly);
                        }
                        OnValueChanged(lstMembers);
                    }
                }
            }
        }
        private void Implement(IInterfaceImplementer parent, Operation operation, bool mustExplicit)
        {
            Operation defined = parent.GetDefinedOperation(operation);
            if (!operation.Language.SupportsExplicitImplementation)
                mustExplicit = false;

            if (defined == null)
            {
                Operation implemented = parent.Implement(operation, mustExplicit);
                AddOperationToList(implemented);
            }
            else if (defined.Type != operation.Type)
            {
                Operation implemented = parent.Implement(operation, true);
                AddOperationToList(implemented);
            }
        }

        private void toolSortByKind_Click(object sender, EventArgs e)
        {
            if (!Validate()) return;
            parent.SortMembers(SortingMode.ByKind);
            FillMembersList();
            OnValueChanged(lstMembers);
        }

        private void toolSortByAccess_Click(object sender, EventArgs e)
        {
            if (!Validate()) return;
            parent.SortMembers(SortingMode.ByAccess);
            FillMembersList();
            OnValueChanged(lstMembers);
        }

        private void toolSortByName_Click(object sender, EventArgs e)
        {
            if (!Validate()) return;
            parent.SortMembers(SortingMode.ByName);
            FillMembersList();
            OnValueChanged(lstMembers);
        }

        private void toolMoveUp_Click(object sender, EventArgs e)
        {
            if (!Validate()) return;
            if (lstMembers.SelectedItems.Count > 0)
            {
                ListViewItem item1 = lstMembers.SelectedItems[0];
                int index = item1.Index;

                if (index > 0)
                {
                    ListViewItem item2 = lstMembers.Items[index - 1];

                    if (item1.Tag is Field && item2.Tag is Field ||
                        item1.Tag is Operation && item2.Tag is Operation)
                    {
                        locked = true;
                        parent.MoveUpItem(item1.Tag);
                        SwapListItems(item1, item2);
                        item2.Focused = true;
                        item2.Selected = true;
                        locked = false;
                        OnValueChanged(lstMembers);
                    }
                }
            }
        }

        private void toolMoveDown_Click(object sender, EventArgs e)
        {
            if (!Validate()) return;
            if (lstMembers.SelectedItems.Count > 0)
            {
                ListViewItem item1 = lstMembers.SelectedItems[0];
                int index = item1.Index;

                if (index < lstMembers.Items.Count - 1)
                {
                    ListViewItem item2 = lstMembers.Items[index + 1];

                    if (item1.Tag is Field && item2.Tag is Field ||
                        item1.Tag is Operation && item2.Tag is Operation)
                    {
                        locked = true;
                        parent.MoveDownItem(item1.Tag);
                        SwapListItems(item1, item2);
                        item2.Focused = true;
                        item2.Selected = true;
                        locked = false;
                        OnValueChanged(lstMembers);
                    }
                }
            }
        }

        private void toolDelete_Click(object sender, EventArgs e)
        {
            if (!Validate()) return;
            DeleteSelectedMember();
            OnValueChanged(lstMembers);
        }

        private void lstMembers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!Validate()) return;
            if (e.IsSelected && e.Item.Tag is Member)
            {
                RefreshMember((Member)e.Item.Tag);

                toolDelete.Enabled = true;
                if (e.ItemIndex < attributeCount)
                {
                    toolMoveUp.Enabled = (e.ItemIndex > 0);
                    toolMoveDown.Enabled = (e.ItemIndex < attributeCount - 1);
                }
                else
                {
                    toolMoveUp.Enabled = (e.ItemIndex > attributeCount);
                    toolMoveDown.Enabled = (e.ItemIndex < lstMembers.Items.Count - 1);
                }
            }
            else
            {
                toolMoveUp.Enabled = false;
                toolMoveDown.Enabled = false;
                toolDelete.Enabled = false;
            }
        }

        private void lstMembers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteSelectedMember();
        }

        public void UpdateTexts()
        {
            toolNewField.Text = Strings.NewField;
            toolNewMethod.Text = Strings.NewMethod;
            toolNewConstructor.Text = Strings.NewConstructor;
            toolNewDestructor.Text = Strings.NewDestructor;
            toolNewProperty.Text = Strings.NewProperty;
            toolNewEvent.Text = Strings.NewEvent;
            toolOverrideList.Text = Strings.OverrideMembers;
            toolImplementList.Text = Strings.Implementing;
            toolSortByKind.Text = Strings.SortByKind;
            toolSortByAccess.Text = Strings.SortByAccess;
            toolSortByName.Text = Strings.SortByName;
            toolMoveUp.Text = Strings.MoveUp;
            toolMoveDown.Text = Strings.MoveDown;
            toolDelete.Text = Strings.Delete;
            lstMembers.Columns[1].Text = Strings.Name;
            lstMembers.Columns[2].Text = Strings.Type;
            lstMembers.Columns[3].Text = Strings.Access;
            lstMembers.Columns[4].Text = Strings.Modifiers;
        }
    }
}
