using System;
using System.ComponentModel;
using System.Windows.Forms;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Members;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;

namespace EAP.ModelFirst.Controls.Editors.TypeEditors
{
    public partial class MemberEditor : UserControl, ILocalizable
    {
        Member member;
        bool locked = false;

        protected internal bool Error { get; private set; }

        public event EditValueChangedEventHandler ValueChanged;

        public MemberEditor()
        {
            InitializeComponent();
            UpdateTexts();
        }

        public void SetMember(Member m)
        {
            member = m;
            if (member != null)
                LanguageSpecificInitialization();
            UpdateValues();
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

        void DisableFields()
        {
            member = null;

            locked = true;

            txtSyntax.Clear();
            txtName.Clear();
            cbxType.Text = null;
            cbxAccess.Text = null;
            txtInitValue.Clear();
            txtComments.Clear();
            txtSyntax.Enabled = false;
            txtName.Enabled = false;
            cbxType.Enabled = false;
            cbxAccess.Enabled = false;
            txtInitValue.Enabled = false;
            txtComments.Enabled = false;
            gbFieldModifiers.Enabled = false;
            gbOperationModifiers.Enabled = false;

            ckbGetter.Checked = false;
            ckbSetter.Checked = false;
            tableLayoutPanelGetterSetter.Visible = false;

            locked = false;
        }

        void UpdateValues()
        {
            errorProvider.Clear();
            Error = false;
            if (member == null)
            {
                DisableFields();
                return;
            }

            locked = true;
            txtSyntax.Enabled = true;
            txtName.Enabled = true;
            txtSyntax.ReadOnly = (member is Destructor);
            txtName.ReadOnly = (member == null || member.IsNameReadonly);
            cbxType.Enabled = (member != null && !member.IsTypeReadonly);
            cbxAccess.Enabled = (member != null && member.IsAccessModifiable);
            txtInitValue.Enabled = (member is Field);
            txtComments.Enabled = true;

            txtSyntax.Text = member.ToString();
            txtName.Text = member.Name;
            cbxType.Text = member.Type;
            txtComments.Text = member.Comments;
            cbxAccess.SelectedItem = member.Language.ValidAccessModifiers[member.AccessModifier];

            if (member is Field)
            {
                Field field = (Field)member;

                gbFieldModifiers.Enabled = true;
                gbFieldModifiers.Visible = true;
                gbOperationModifiers.Visible = false;
                tableLayoutPanelGetterSetter.Visible = true;

                ckbFieldStatic.Checked = field.IsStatic;
                ckbFieldReadonly.Checked = field.IsReadonly;
                ckbFieldConst.Checked = field.IsConstant;
                ckbFieldNew.Checked = field.IsHider;
                ckbFieldVolatile.Checked = field.IsVolatile;
                txtInitValue.Text = field.InitialValue;

                ckbGetter.Checked = field.Getter;
                ckbSetter.Checked = field.Setter;
            }
            else if (member is Operation)
            {
                Operation operation = (Operation)member;

                gbOperationModifiers.Enabled = true;
                gbOperationModifiers.Visible = true;
                gbFieldModifiers.Visible = false;
                tableLayoutPanelGetterSetter.Visible = false;

                ckbOperationStatic.Checked = operation.IsStatic;
                ckbOperationVirtual.Checked = operation.IsVirtual;
                ckbOperationAbstract.Checked = operation.IsAbstract;
                ckbOperationOverride.Checked = operation.IsOverride;
                ckbOperationSealed.Checked = operation.IsSealed;
                ckbOperationNew.Checked = operation.IsHider;
                txtInitValue.Text = string.Empty;
            }
            locked = false;
        }

        private void OnValueChanged(Control control)
        {
            if (ValueChanged != null)
                ValueChanged.Invoke(this, new EditValueChangedEventArgs(new EditorInfo(control)));
        }

        private void LanguageSpecificInitialization()
        {
            Language language = member.Language;
            cbxAccess.Items.Clear();
            foreach (string modifier in language.ValidAccessModifiers.Values)
                cbxAccess.Items.Add(modifier);

            if (language.IsValidModifier(FieldModifier.Static))
            {
                ckbFieldStatic.Enabled = true;
                ckbFieldStatic.Text = language.ValidFieldModifiers[FieldModifier.Static];
            }
            else
            {
                ckbFieldStatic.Enabled = false;
                ckbFieldStatic.Text = "Static";
            }

            if (language.IsValidModifier(FieldModifier.Readonly))
            {
                ckbFieldReadonly.Enabled = true;
                ckbFieldReadonly.Text = language.ValidFieldModifiers[FieldModifier.Readonly];
            }
            else
            {
                ckbFieldReadonly.Enabled = false;
                ckbFieldReadonly.Text = "Readonly";
            }

            if (language.IsValidModifier(FieldModifier.Constant))
            {
                ckbFieldConst.Enabled = true;
                ckbFieldConst.Text = language.ValidFieldModifiers[FieldModifier.Constant];
            }
            else
            {
                ckbFieldConst.Enabled = false;
                ckbFieldConst.Text = "Constant";
            }

            if (language.IsValidModifier(FieldModifier.Hider))
            {
                ckbFieldNew.Enabled = true;
                ckbFieldNew.Text = language.ValidFieldModifiers[FieldModifier.Hider];
            }
            else
            {
                ckbFieldNew.Enabled = false;
                ckbFieldNew.Text = "Hider";
            }

            if (language.IsValidModifier(FieldModifier.Volatile))
            {
                ckbFieldVolatile.Enabled = true;
                ckbFieldVolatile.Text = language.ValidFieldModifiers[FieldModifier.Volatile];
            }
            else
            {
                ckbFieldVolatile.Enabled = false;
                ckbFieldVolatile.Text = "Volatile";
            }

            if (language.IsValidModifier(OperationModifier.Static))
            {
                ckbOperationStatic.Enabled = true;
                ckbOperationStatic.Text = language.ValidOperationModifiers[OperationModifier.Static];
            }
            else
            {
                ckbOperationStatic.Enabled = false;
                ckbOperationStatic.Text = "Static";
            }

            if (language.IsValidModifier(OperationModifier.Virtual))
            {
                ckbOperationVirtual.Enabled = true;
                ckbOperationVirtual.Text = language.ValidOperationModifiers[OperationModifier.Virtual];
            }
            else
            {
                ckbOperationVirtual.Enabled = false;
                ckbOperationVirtual.Text = "Virtual";
            }

            if (language.IsValidModifier(OperationModifier.Abstract))
            {
                ckbOperationAbstract.Enabled = true;
                ckbOperationAbstract.Text = language.ValidOperationModifiers[OperationModifier.Abstract];
            }
            else
            {
                ckbOperationAbstract.Enabled = false;
                ckbOperationAbstract.Text = "Abstract";
            }

            if (language.IsValidModifier(OperationModifier.Override))
            {
                ckbOperationOverride.Enabled = true;
                ckbOperationOverride.Text = language.ValidOperationModifiers[OperationModifier.Override];
            }
            else
            {
                ckbOperationOverride.Enabled = false;
                ckbOperationOverride.Text = "Override";
            }

            if (language.IsValidModifier(OperationModifier.Sealed))
            {
                ckbOperationSealed.Enabled = true;
                ckbOperationSealed.Text = language.ValidOperationModifiers[OperationModifier.Sealed];
            }
            else
            {
                ckbOperationSealed.Enabled = false;
                ckbOperationSealed.Text = "Sealed";
            }

            if (language.IsValidModifier(OperationModifier.Hider))
            {
                ckbOperationNew.Enabled = true;
                ckbOperationNew.Text = language.ValidOperationModifiers[OperationModifier.Hider];
            }
            else
            {
                ckbOperationNew.Enabled = false;
                ckbOperationNew.Text = "Hider";
            }
        }

        private void txtSyntax_Validating(object sender, CancelEventArgs e)
        {
            if (locked || member == null)
                return;
            try
            {
                errorProvider.SetError(txtSyntax, null);
                Error = false;
                var changed = txtSyntax.Text != member.ToString();
                if (changed)
                {
                    member.InitFromString(txtSyntax.Text);
                    UpdateValues();
                    OnValueChanged(txtSyntax);
                }
            }
            catch (BadSyntaxException ex)
            {
                e.Cancel = true;
                errorProvider.SetError(txtSyntax, ex.Message);
                Error = true;
            }
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (locked || member == null)
                return;
            try
            {
                errorProvider.SetError(txtName, null);
                Error = false;
                var changed = member.Name != txtName.Text;
                if (changed)
                {
                    member.Name = txtName.Text;
                    UpdateValues();
                    OnValueChanged(txtSyntax);
                }
            }
            catch (BadSyntaxException ex)
            {
                e.Cancel = true;
                errorProvider.SetError(txtName, ex.Message);
                Error = true;
            }
        }

        private void cbxType_Validating(object sender, CancelEventArgs e)
        {
            if (locked || member == null)
                return;
            try
            {
                errorProvider.SetError(cbxType, null);
                Error = false;
                var changed = member.Type != cbxType.Text;
                if (!cbxType.Items.Contains(cbxType.Text))
                    cbxType.Items.Add(cbxType.Text);
                member.Type = cbxType.Text;
                cbxType.Select(0, 0);
                if (changed)
                {
                    UpdateValues();
                    OnValueChanged(txtSyntax);
                }
            }
            catch (BadSyntaxException ex)
            {
                e.Cancel = true;
                errorProvider.SetError(cbxType, ex.Message);
                Error = true;
            }
        }

        private void cbxAccess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locked || member == null)
                return;
            try
            {
                errorProvider.SetError(cbxAccess, null);
                Error = false;
                string selectedModifierString = cbxAccess.SelectedItem.ToString();

                foreach (AccessModifier modifier in member.Language.ValidAccessModifiers.Keys)
                {
                    if (member.Language.ValidAccessModifiers[modifier] == selectedModifierString)
                    {
                        var changed = member.AccessModifier != modifier;
                        member.AccessModifier = modifier;
                        if (changed)
                        {
                            UpdateValues();
                            OnValueChanged(txtSyntax);
                        }
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

        private void ckbFieldStatic_CheckedChanged(object sender, EventArgs e)
        {
            if (locked || !(member is Field))
                return;
            var field = (Field)member;
            try
            {
                errorProvider.SetError(gbFieldModifiers, null);
                Error = false;
                var changed = field.IsStatic != ckbFieldStatic.Checked;
                field.IsStatic = ckbFieldStatic.Checked;
                if (changed)
                {
                    UpdateValues();
                    OnValueChanged(txtSyntax);
                }
            }
            catch (BadSyntaxException ex)
            {
                locked = true;
                ckbFieldStatic.Checked = field.IsStatic;
                locked = false;
                errorProvider.SetError(gbFieldModifiers, ex.Message);
                Error = true;
            }
        }

        private void ckbFieldReadonly_CheckedChanged(object sender, EventArgs e)
        {
            if (locked || !(member is Field))
                return;
            var field = (Field)member;
            try
            {
                errorProvider.SetError(gbFieldModifiers, null);
                Error = false;
                var changed = field.IsReadonly != ckbFieldReadonly.Checked;
                field.IsReadonly = ckbFieldReadonly.Checked;
                if (changed)
                {
                    UpdateValues();
                    OnValueChanged(txtSyntax);
                }
            }
            catch (BadSyntaxException ex)
            {
                locked = true;
                ckbFieldReadonly.Checked = field.IsReadonly;
                locked = false;
                errorProvider.SetError(gbFieldModifiers, ex.Message);
                Error = true;
            }
        }

        private void ckbFieldConst_CheckedChanged(object sender, EventArgs e)
        {
            if (locked || !(member is Field))
                return;
            var field = (Field)member;
            try
            {
                errorProvider.SetError(gbFieldModifiers, null);
                Error = false;
                var changed = field.IsConstant != ckbFieldConst.Checked;
                field.IsConstant = ckbFieldConst.Checked;
                if (changed)
                {
                    UpdateValues();
                    OnValueChanged(txtSyntax);
                }
            }
            catch (BadSyntaxException ex)
            {
                locked = true;
                ckbFieldConst.Checked = field.IsConstant;
                locked = false;
                errorProvider.SetError(gbFieldModifiers, ex.Message);
                Error = true;
            }
        }

        private void ckbFieldNew_CheckedChanged(object sender, EventArgs e)
        {
            if (locked || !(member is Field))
                return;
            var field = (Field)member;
            try
            {
                errorProvider.SetError(gbFieldModifiers, null);
                Error = false;
                var changed = field.IsHider != ckbFieldNew.Checked;
                field.IsHider = ckbFieldNew.Checked;
                if (changed)
                {
                    UpdateValues();
                    OnValueChanged(txtSyntax);
                }
            }
            catch (BadSyntaxException ex)
            {
                locked = true;
                ckbFieldNew.Checked = field.IsHider;
                locked = false;
                errorProvider.SetError(gbFieldModifiers, ex.Message);
                Error = true;
            }
        }

        private void ckbFieldVolatile_CheckedChanged(object sender, EventArgs e)
        {
            if (locked || !(member is Field))
                return;
            var field = (Field)member;
            try
            {
                errorProvider.SetError(gbFieldModifiers, null);
                Error = false;
                var changed = field.IsVolatile != ckbFieldVolatile.Checked;
                field.IsVolatile = ckbFieldVolatile.Checked;
                if (changed)
                {
                    UpdateValues();
                    OnValueChanged(txtSyntax);
                }
            }
            catch (BadSyntaxException ex)
            {
                locked = true;
                ckbFieldVolatile.Checked = field.IsVolatile;
                locked = false;
                errorProvider.SetError(gbFieldModifiers, ex.Message);
                Error = true;
            }
        }

        private void txtInitValue_Validating(object sender, CancelEventArgs e)
        {
            if (locked || !(member is Field))
                return;
            var field = (Field)member;
            if (txtInitValue.Text.Length > 0 && txtInitValue.Text[0] == '"' &&
                !txtInitValue.Text.EndsWith("\""))
            {
                txtInitValue.Text += '"';
            }
            var changed = field.InitialValue != txtInitValue.Text;
            field.InitialValue = txtInitValue.Text;
            if (changed)
            {
                UpdateValues();
                OnValueChanged(txtSyntax);
            }
        }

        private void ckbOperationStatic_CheckedChanged(object sender, EventArgs e)
        {
            if (locked || !(member is Operation))
                return;
            var op = (Operation)member;
            try
            {
                errorProvider.SetError(gbOperationModifiers, null);
                Error = false;
                var changed = op.IsStatic != ckbOperationStatic.Checked;
                op.IsStatic = ckbOperationStatic.Checked;
                if (changed)
                {
                    UpdateValues();
                    OnValueChanged(txtSyntax);
                }
            }
            catch (BadSyntaxException ex)
            {
                locked = true;
                ckbOperationStatic.Checked = op.IsStatic;
                locked = false;
                errorProvider.SetError(gbOperationModifiers, ex.Message);
                Error = true;
            }
        }

        private void ckbOperationVirtual_CheckedChanged(object sender, EventArgs e)
        {
            if (locked || !(member is Operation))
                return;
            var op = (Operation)member;
            try
            {
                errorProvider.SetError(gbOperationModifiers, null);
                Error = false;
                var changed = op.IsVirtual != ckbOperationVirtual.Checked;
                op.IsVirtual = ckbOperationVirtual.Checked;
                if (changed)
                {
                    UpdateValues();
                    OnValueChanged(txtSyntax);
                }
            }
            catch (BadSyntaxException ex)
            {
                locked = true;
                ckbOperationVirtual.Checked = op.IsVirtual;
                locked = false;
                errorProvider.SetError(gbOperationModifiers, ex.Message);
                Error = true;
            }
        }

        private void ckbOperationAbstract_CheckedChanged(object sender, EventArgs e)
        {
            if (locked || !(member is Operation))
                return;
            var op = (Operation)member;
            try
            {
                errorProvider.SetError(gbOperationModifiers, null);
                Error = false;
                if (member.Parent is ClassType &&
                    ((ClassType)member.Parent).Modifier != ClassModifier.Abstract)
                {
                    DialogResult result = Client.ShowConfirm(Strings.ChangingToAbstractConfirmation);
                    if (result == DialogResult.Cancel)
                    {
                        locked = true;
                        ckbOperationAbstract.Checked = op.IsAbstract;
                        locked = false;
                        return;
                    }
                }
                var changed = op.IsAbstract != ckbOperationAbstract.Checked;
                op.IsAbstract = ckbOperationAbstract.Checked;
                if (changed)
                {
                    UpdateValues();
                    OnValueChanged(txtSyntax);
                }
            }
            catch (BadSyntaxException ex)
            {
                locked = true;
                ckbOperationAbstract.Checked = op.IsAbstract;
                locked = false;
                errorProvider.SetError(gbOperationModifiers, ex.Message);
                Error = true;
            }
        }

        private void ckbOperationNew_CheckedChanged(object sender, EventArgs e)
        {
            if (locked || !(member is Operation))
                return;
            var op = (Operation)member;
            try
            {
                errorProvider.SetError(gbOperationModifiers, null);
                Error = false;
                var changed = op.IsHider != ckbOperationNew.Checked;
                op.IsHider = ckbOperationNew.Checked;
                if (changed)
                {
                    UpdateValues();
                    OnValueChanged(txtSyntax);
                }
            }
            catch (BadSyntaxException ex)
            {
                locked = true;
                ckbOperationNew.Checked = op.IsHider;
                locked = false;
                errorProvider.SetError(gbOperationModifiers, ex.Message);
                Error = true;
            }
        }

        private void ckbOperationOverride_CheckedChanged(object sender, EventArgs e)
        {
            if (locked || !(member is Operation))
                return;
            var op = (Operation)member;
            try
            {
                errorProvider.SetError(gbOperationModifiers, null);
                Error = false;
                var changed = op.IsOverride != ckbOperationOverride.Checked;
                op.IsOverride = ckbOperationOverride.Checked;
                if (changed)
                {
                    UpdateValues();
                    OnValueChanged(txtSyntax);
                }
            }
            catch (BadSyntaxException ex)
            {
                locked = true;
                ckbOperationOverride.Checked = op.IsOverride;
                locked = false;
                errorProvider.SetError(gbOperationModifiers, ex.Message);
                Error = true;
            }
        }

        private void ckbOperationSealed_CheckedChanged(object sender, EventArgs e)
        {
            if (locked || !(member is Operation))
                return;
            var op = (Operation)member;
            try
            {
                errorProvider.SetError(gbOperationModifiers, null);
                Error = false;
                var changed = op.IsSealed != ckbOperationSealed.Checked;
                op.IsSealed = ckbOperationSealed.Checked;
                if (changed)
                {
                    UpdateValues();
                    OnValueChanged(txtSyntax);
                }
            }
            catch (BadSyntaxException ex)
            {
                locked = true;
                ckbOperationSealed.Checked = op.IsSealed;
                locked = false;
                errorProvider.SetError(gbOperationModifiers, ex.Message);
                Error = true;
            }
        }

        private void cbxAccess_Validated(object sender, EventArgs e)
        {
            if (member != null && errorProvider.GetError(cbxAccess).IsNotEmpty())
            {
                errorProvider.SetError(cbxAccess, null);
                locked = true;
                cbxAccess.SelectedItem = member.Language.ValidAccessModifiers[member.AccessModifier];
                locked = false;
                Error = false;
            }
        }

        public void UpdateTexts()
        {
            lblSyntax.Text = Strings.Syntax;
            lblName.Text = Strings.Name;
            lblType.Text = Strings.Type;
            lblAccess.Text = Strings.Access;
            lblInitValue.Text = Strings.InitialValue;
            gbOperationModifiers.Text = Strings.Modifiers;
            gbFieldModifiers.Text = Strings.Modifiers;
        }

        private void ckbGetter_CheckedChanged(object sender, EventArgs e)
        {
            if (locked || !(member is Field))
                return;
            var field = (Field)member;
            try
            {
                var changed = field.Getter != ckbGetter.Checked;
                field.Getter = ckbGetter.Checked;
                errorProvider.SetError(ckbGetter, null);
                Error = false;
                if (changed)
                {
                    UpdateValues();
                    OnValueChanged(ckbGetter);
                }
            }
            catch (BadSyntaxException ex)
            {
                locked = true;
                ckbGetter.Checked = field.Getter;
                locked = false;
                errorProvider.SetError(ckbGetter, ex.Message);
                Error = true;
            }
        }

        private void ckbSetter_CheckedChanged(object sender, EventArgs e)
        {
            if (locked || !(member is Field))
                return;
            var field = (Field)member;
            try
            {
                var changed = field.Setter != ckbSetter.Checked;
                field.Setter = ckbSetter.Checked;
                errorProvider.SetError(ckbSetter, null);
                Error = false;
                if (changed)
                {
                    UpdateValues();
                    OnValueChanged(ckbSetter);
                }
            }
            catch (BadSyntaxException ex)
            {
                locked = true;
                ckbSetter.Checked = field.Setter;
                locked = false;
                errorProvider.SetError(ckbSetter, ex.Message);
                Error = true;
            }
        }

        private void txtComments_Validating(object sender, CancelEventArgs e)
        {
            if (locked || member == null)
                return;
            bool changed = member.Comments != txtComments.Text;
            member.Comments = txtComments.Text;
            if (changed)
                OnValueChanged(txtComments);
        }
    }
}
