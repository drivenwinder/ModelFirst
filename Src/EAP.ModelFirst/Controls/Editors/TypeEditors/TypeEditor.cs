using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;

namespace EAP.ModelFirst.Controls.Editors.TypeEditors
{
    public partial class TypeEditor : UserControl, IEditable
    {
        TypeBase type;
        bool locked = false;
        bool isEditing = false;
        EditStack editStack = new EditStack();
        XmlElement restorePoint = null; 

        public event EventHandler EditStateChanged;

        protected virtual bool Error { get; set; }

        protected virtual bool IsDirty
        {
            get { return type != null && type.IsDirty; }
        }

        public TypeEditor()
        {
            InitializeComponent();
        }

        protected void SetTypeBase(TypeBase typeBase)
        {
            type = typeBase;
            type.Clean();
            type.Modified += new EventHandler(type_Modified);
            type.ProjectInfo.Saved += new EventHandler(Project_Saved);
        }

        void Project_Saved(object sender, EventArgs e)
        {
            restorePoint = Serializer.Serialize(type);
        }

        void type_Modified(object sender, EventArgs e)
        {
            //对于单个类，在其它地方修改后，将加入编辑堆栈中。并记录还原点
            if (!Focused && !ContainsFocus)
            {
                OnValueChanged(new EditorInfo(this));
                restorePoint = Serializer.Serialize(type);
            }
        }

        protected virtual void UpdateValues()
        {

        }

        protected virtual void OnValueChanged(EditorInfo editor)
        {
            if (!IsEditing || locked)
                return;
            UpdateValues();
            editStack.Push(editor, Serializer.Serialize(type));
            OnEditStateChanged(EventArgs.Empty);
        }

        protected virtual void InitEditStack()
        {
            UpdateValues();
            restorePoint = Serializer.Serialize(type);
            editStack.Init(new EditorInfo(this), restorePoint);
            editStack.Clear();
            OnEditStateChanged(EventArgs.Empty);
        }

        protected virtual void OnEditStateChanged(EventArgs e)
        {
            if (EditStateChanged != null)
                EditStateChanged(this, e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Error && keyData == Keys.Escape)
            {
                UpdateValues();
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }

        #region

        public bool IsEmpty
        {
            get { return false; }
        }

        public bool CanUndo
        {
            get { return editStack.CanUndo; }
        }

        public bool CanRedo
        {
            get { return editStack.CanRedo; }
        }

        public bool CanDelete
        {
            get { return false; }
        }

        public bool CanCutToClipboard
        {
            get { return true; }
        }

        public bool CanCopyToClipboard
        {
            get { return true; }
        }

        public bool CanPasteFromClipboard
        {
            get { return true; }
        }

        public void Undo()
        {
            var v = editStack.Undo();
            type.Deserialize(v.Value);
            UpdateValues();
            OnEditStateChanged(EventArgs.Empty);
            locked = true;
            if (v.Editor != null)
                v.Editor.Focus();
            locked = false;
        }

        public void Redo()
        {
            var v = editStack.Redo();
            type.Deserialize(v.Value);
            UpdateValues();
            OnEditStateChanged(EventArgs.Empty);
            locked = true;
            if (v.Editor != null)
                v.Editor.Focus();
            locked = false;
        }

        public void Cut()
        {
            if (ContainsFocus)
            {
                var c = Client.GetFocusedControl();
                if (c != null && c is TextBoxBase)
                    ((TextBoxBase)c).Cut();
            }
        }

        public void Copy()
        {
            if (ContainsFocus)
            {
                var c = Client.GetFocusedControl();
                if (c != null && c is TextBoxBase)
                    ((TextBoxBase)c).Copy();
            }
        }

        public void Paste()
        {
            if (ContainsFocus)
            {
                var c = Client.GetFocusedControl();
                if (c != null && c is TextBoxBase)
                    ((TextBoxBase)c).Paste();
            }
        }

        public void SelectAll()
        {
            if (ContainsFocus)
            {
                var c = Client.GetFocusedControl();
                if (c != null && c is TextBoxBase)
                    ((TextBoxBase)c).SelectAll();
            }
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            BeginEdit();
        }

        public virtual void OnClosing(CancelEventArgs e)
        {
            if (Error && e.Cancel)
            {
                var result = Client.ShowConfirm(Strings.CancelEditAndCloseConfirmation, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.OK)
                    e.Cancel = false;
            }
            if (!e.Cancel && IsDirty)
            {
                DialogResult result = Client.Show(Strings.DiscardChanges, Strings.Confirmation,
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
                if (result == DialogResult.Yes)
                    CancelEdit();
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        public virtual void OnClosed(EventArgs e)
        {
            type.Modified -= new EventHandler(type_Modified);
            type.ProjectInfo.Saved -= new EventHandler(Project_Saved);
            EndEdit();
            editStack = null;
        }

        public bool IsEditing
        {
            get { return isEditing; }
        }

        public void BeginEdit()
        {
            isEditing = true;
        }

        public void EndEdit()
        {
            isEditing = false;
        }

        public void CancelEdit()
        {
            type.Deserialize(restorePoint);
        }

        public TypeBase TypeBase
        {
            get { return type; }
        }
    }
}
