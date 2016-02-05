using System;
using System.ComponentModel;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Controls.Editors.TypeEditors
{
    public partial class DelegateTypeEditor : TypeEditor, ILocalizable
    {
        DelegateType delegateType;
        bool locked;

        public DelegateTypeEditor()
        {
            InitializeComponent();
            UpdateTexts();
        }

        public DelegateTypeEditor SetDelegateType(DelegateType type, bool memberOnly = false)
        {
            MemberOnly = memberOnly;
            SetTypeBase(type);
            delegateType = type;
            LanguageSpecificInitialization();
            InitEditStack();
            delegateParameterEditor.ValueChanged += new EditValueChangedEventHandler(delegateParameterEditor_ValueChanged);
            return this;
        }

        void delegateParameterEditor_ValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (locked)
                return;
            OnValueChanged(e.Editor);
        }

        public bool MemberOnly
        {
            get { return !kryptonHeaderGroupDelegateInfo.Visible; }
            set { kryptonHeaderGroupDelegateInfo.Visible = !value; }
        }

        public void UpdateTexts()
        {
            lblName.Text = Strings.Name;
            lblAccess.Text = Strings.Access;
            lblReturnType.Text = Strings.ReturnType;
        }

        protected override void UpdateValues()
        {
            locked = true;

            txtReturnType.Text = delegateType.ReturnType;
            txtName.Text = delegateType.Name;
            txtComments.Text = delegateType.Comments;
            cbxAccess.SelectedItem = delegateType.Language.ValidAccessModifiers[delegateType.AccessModifier];
            delegateParameterEditor.SetDelegateType(delegateType);

            locked = false;
            errorProvider.Clear();
            Error = false;
        }

        void LanguageSpecificInitialization()
        {
            cbxAccess.Items.Clear();
            // Public
            if (delegateType.Language.ValidAccessModifiers.ContainsKey(AccessModifier.Public))
                cbxAccess.Items.Add(delegateType.Language.ValidAccessModifiers[AccessModifier.Public]);
            // Protected Internal
            if (delegateType.IsNested && delegateType.Language.ValidAccessModifiers.ContainsKey(AccessModifier.ProtectedInternal))
                cbxAccess.Items.Add(delegateType.Language.ValidAccessModifiers[AccessModifier.ProtectedInternal]);
            // Internal
            if (delegateType.Language.ValidAccessModifiers.ContainsKey(AccessModifier.Internal))
                cbxAccess.Items.Add(delegateType.Language.ValidAccessModifiers[AccessModifier.Internal]);
            // Protected
            if (delegateType.IsNested && delegateType.Language.ValidAccessModifiers.ContainsKey(AccessModifier.Protected))
                cbxAccess.Items.Add(delegateType.Language.ValidAccessModifiers[AccessModifier.Protected]);
            // Private
            if (delegateType.IsNested && delegateType.Language.ValidAccessModifiers.ContainsKey(AccessModifier.Private))
                cbxAccess.Items.Add(delegateType.Language.ValidAccessModifiers[AccessModifier.Private]);
            // Default
            if (delegateType.Language.ValidAccessModifiers.ContainsKey(AccessModifier.Default))
                cbxAccess.Items.Add(delegateType.Language.ValidAccessModifiers[AccessModifier.Default]);
        }

        private void cbxAccess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locked || delegateType == null)
                return;
            try
            {
                string selectedModifierString = cbxAccess.SelectedItem.ToString();

                foreach (AccessModifier modifier in delegateType.Language.ValidAccessModifiers.Keys)
                {
                    if (delegateType.Language.ValidAccessModifiers[modifier] == selectedModifierString)
                    {
                        var changed = delegateType.AccessModifier != modifier;
                        delegateType.AccessModifier = modifier;
                        errorProvider.SetError(cbxAccess, null);
                        Error = false;
                        if (changed)
                            OnValueChanged(new EditorInfo(cbxAccess));
                        break;
                    }
                }
            }
            catch (BadSyntaxException ex)
            {
                errorProvider.SetError(cbxAccess, ex.Message);
                Error = true;
            }
        }

        private void cbxAccess_Validated(object sender, EventArgs e)
        {
            if (delegateType != null && errorProvider.GetError(cbxAccess).IsNotEmpty())
            {
                errorProvider.SetError(cbxAccess, null);
                locked = true;
                cbxAccess.SelectedItem = delegateType.Language.ValidAccessModifiers[delegateType.AccessModifier];
                locked = false;
                Error = false;
            }
        }

        private void txtReturnType_Validating(object sender, CancelEventArgs e)
        {
            if (locked || delegateType == null) return;
            try
            {
                var changed = delegateType.ReturnType != txtReturnType.Text;
                delegateType.ReturnType = txtReturnType.Text;
                errorProvider.SetError(txtReturnType, null);
                Error = false;
                if (changed)
                    OnValueChanged(new EditorInfo(txtReturnType));
            }
            catch (BadSyntaxException ex)
            {
                errorProvider.SetError(txtReturnType, ex.Message);
                Error = true;
                e.Cancel = true;
            }
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (locked || delegateType == null) return;
            try
            {
                var changed = delegateType.Name != txtName.Text;
                delegateType.Name = txtName.Text;
                errorProvider.SetError(txtName, null);
                Error = false;
                if (changed)
                    OnValueChanged(new EditorInfo(txtName));
            }
            catch (BadSyntaxException ex)
            {
                errorProvider.SetError(txtName, ex.Message);
                Error = true;
                e.Cancel = true;
            }
        }

        private void txtComments_Validating(object sender, CancelEventArgs e)
        {
            if (locked || delegateType == null) return;
            bool changed = delegateType.Comments != txtComments.Text;
            delegateType.Comments = txtComments.Text;
            if (changed)
                OnValueChanged(new EditorInfo(txtComments));
        }
    }
}
