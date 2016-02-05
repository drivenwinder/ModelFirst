using System;
using System.ComponentModel;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Controls.Editors.TypeEditors
{
    public partial class EnumTypeEditor : TypeEditor, ILocalizable
    {
        EnumType enumType;
        bool locked;

        public EnumTypeEditor()
        {
            InitializeComponent();
        }

        public EnumTypeEditor SetEnumType(EnumType type, bool memberOnly = false)
        {
            MemberOnly = memberOnly;
            SetTypeBase(type);
            enumType = type;
            LanguageSpecificInitialization();
            InitEditStack();
            enumValueEditor.ValueChanged += new EditValueChangedEventHandler(enumValueEditor_ValueChanged);
            return this;
        }

        void enumValueEditor_ValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (locked)
                return;
            OnValueChanged(e.Editor);
        }

        protected override void UpdateValues()
        {
            locked = true;
            txtName.Text = enumType.Name;
            txtComments.Text = enumType.Comments;
            cbxAccess.SelectedItem = enumType.Language.ValidAccessModifiers[enumType.AccessModifier];
            enumValueEditor.SetEnumType(enumType);
            locked = false;
            errorProvider.Clear();
            Error = false;
        }

        public bool MemberOnly
        {
            get { return !kryptonHeaderGroupEnumInfo.Visible; }
            set { kryptonHeaderGroupEnumInfo.Visible = !value; }
        }

        public void UpdateTexts()
        {
            lblName.Text = Strings.Name;
            lblAccess.Text = Strings.Access;
        }

        void LanguageSpecificInitialization()
        {
            cbxAccess.Items.Clear();
            // Public
            if (enumType.Language.ValidAccessModifiers.ContainsKey(AccessModifier.Public))
                cbxAccess.Items.Add(enumType.Language.ValidAccessModifiers[AccessModifier.Public]);
            // Protected Internal
            if (enumType.IsNested && enumType.Language.ValidAccessModifiers.ContainsKey(AccessModifier.ProtectedInternal))
                cbxAccess.Items.Add(enumType.Language.ValidAccessModifiers[AccessModifier.ProtectedInternal]);
            // Internal
            if (enumType.Language.ValidAccessModifiers.ContainsKey(AccessModifier.Internal))
                cbxAccess.Items.Add(enumType.Language.ValidAccessModifiers[AccessModifier.Internal]);
            // Protected
            if (enumType.IsNested && enumType.Language.ValidAccessModifiers.ContainsKey(AccessModifier.Protected))
                cbxAccess.Items.Add(enumType.Language.ValidAccessModifiers[AccessModifier.Protected]);
            // Private
            if (enumType.IsNested && enumType.Language.ValidAccessModifiers.ContainsKey(AccessModifier.Private))
                cbxAccess.Items.Add(enumType.Language.ValidAccessModifiers[AccessModifier.Private]);
            // Default
            if (enumType.Language.ValidAccessModifiers.ContainsKey(AccessModifier.Default))
                cbxAccess.Items.Add(enumType.Language.ValidAccessModifiers[AccessModifier.Default]);
        }

        private void cbxAccess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locked || enumType == null)
                return;
            try
            {
                string selectedModifierString = cbxAccess.SelectedItem.ToString();

                foreach (AccessModifier modifier in enumType.Language.ValidAccessModifiers.Keys)
                {
                    if (enumType.Language.ValidAccessModifiers[modifier] == selectedModifierString)
                    {
                        var changed = enumType.AccessModifier != modifier;
                        enumType.AccessModifier = modifier;
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
            if (enumType != null && errorProvider.GetError(cbxAccess).IsNotEmpty())
            {
                errorProvider.SetError(cbxAccess, null);
                locked = true;
                cbxAccess.SelectedItem = enumType.Language.ValidAccessModifiers[enumType.AccessModifier];
                locked = false;
                Error = false;
            }
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (locked || enumType == null) return;
            try
            {
                var changed = enumType.Name != txtName.Text;
                enumType.Name = txtName.Text;
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
            if (locked || enumType == null) return;
            bool changed = enumType.Comments != txtComments.Text;
            enumType.Comments = txtComments.Text;
            if (changed)
                OnValueChanged(new EditorInfo(txtComments));
        }
    }
}
