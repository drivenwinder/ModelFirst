using System;
using System.ComponentModel;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Properties;
using System.Windows.Forms;
using EAP.ModelFirst.Core.Project.Members;

namespace EAP.ModelFirst.Controls.Editors.TypeEditors
{
    public partial class CompositeTypeEditor : TypeEditor, ILocalizable
    {
        CompositeType compositeType;
        bool locked = false;
        bool error = false;

        protected override bool Error
        {
            get { return error || memberListEditor.Error; }
            set { base.Error = value; }
        }

        public CompositeTypeEditor()
        {
            InitializeComponent();
            UpdateTexts();
        }

        public bool MemberOnly
        {
            get { return !kryptonHeaderGroupTypeInfo.Visible; }
            set { kryptonHeaderGroupTypeInfo.Visible = !value; }
        }

        public CompositeTypeEditor SetCompositeType(CompositeType type, bool memberOnly = false)
        {
            MemberOnly = memberOnly;
            SetTypeBase(type);
            compositeType = type;
            LanguageSpecificInitialization();
            InitEditStack();
            memberListEditor.ValueChanged += new EditValueChangedEventHandler(memberEditor_ValueChanged);
            return this;
        }

        void memberEditor_ValueChanged(object sender, EditValueChangedEventArgs e)
        {
            if (locked)
                return;
            OnValueChanged(e.Editor);
        }

        void LanguageSpecificInitialization()
        {
            cbxAccess.Items.Clear();
            // Public
            if (compositeType.Language.ValidAccessModifiers.ContainsKey(AccessModifier.Public))
                cbxAccess.Items.Add(compositeType.Language.ValidAccessModifiers[AccessModifier.Public]);
            // Protected Internal
            if (compositeType.IsNested && compositeType.Language.ValidAccessModifiers.ContainsKey(AccessModifier.ProtectedInternal))
                cbxAccess.Items.Add(compositeType.Language.ValidAccessModifiers[AccessModifier.ProtectedInternal]);
            // Internal
            if (compositeType.Language.ValidAccessModifiers.ContainsKey(AccessModifier.Internal))
                cbxAccess.Items.Add(compositeType.Language.ValidAccessModifiers[AccessModifier.Internal]);
            // Protected
            if (compositeType.IsNested && compositeType.Language.ValidAccessModifiers.ContainsKey(AccessModifier.Protected))
                cbxAccess.Items.Add(compositeType.Language.ValidAccessModifiers[AccessModifier.Protected]);
            // Private
            if (compositeType.IsNested && compositeType.Language.ValidAccessModifiers.ContainsKey(AccessModifier.Private))
                cbxAccess.Items.Add(compositeType.Language.ValidAccessModifiers[AccessModifier.Private]);
            // Default
            if (compositeType.Language.ValidAccessModifiers.ContainsKey(AccessModifier.Default))
                cbxAccess.Items.Add(compositeType.Language.ValidAccessModifiers[AccessModifier.Default]);

            if (compositeType is ClassType)
            {
                lblModifier.Visible = true;
                cbxModifier.Visible = true;
                cbxModifier.Items.Clear();
                foreach (string modifier in compositeType.Language.ValidClassModifiers.Values)
                    cbxModifier.Items.Add(modifier);
            }
            else
            {
                lblModifier.Visible = false;
                cbxModifier.Visible = false;
            }
            
            if (compositeType is SingleInharitanceType)
            {
                lblTableName.Visible = true;
                txtTableName.Visible = true;
            }
            else
            {
                lblTableName.Visible = false;
                txtTableName.Visible = false;
            }
            if (compositeType.SupportsFields)
                memberListEditor.NewMemberType = MemberType.Field;
            else if (compositeType.SupportsProperties)
                memberListEditor.NewMemberType = MemberType.Property;
            else
                memberListEditor.NewMemberType = MemberType.Method;
        }

        public void UpdateTexts()
        {
            lblName.Text = Strings.Name;
            lblAccess.Text = Strings.Access;
            lblModifier.Text = Strings.Modifier;
        }

        protected override void UpdateValues()
        {
            locked = true;
            txtName.Text = compositeType.Name;
            txtComments.Text = compositeType.Comments;
            cbxAccess.SelectedItem = compositeType.Language.ValidAccessModifiers[compositeType.AccessModifier];
            if(compositeType is SingleInharitanceType)
            	txtTableName.Text = ((SingleInharitanceType)compositeType).TableName;
            if (compositeType is ClassType)
                cbxModifier.SelectedItem = compositeType.Language.ValidClassModifiers[(compositeType as ClassType).Modifier];
            memberListEditor.SetCompositeType(compositeType);
            locked = false;
            errorProvider.Clear();
            error = false;
        }

        private void cbxAccess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locked || compositeType == null)
                return;
            try
            {
                string selectedModifierString = cbxAccess.SelectedItem.ToString();

                foreach (AccessModifier modifier in compositeType.Language.ValidAccessModifiers.Keys)
                {
                    if (compositeType.Language.ValidAccessModifiers[modifier] == selectedModifierString)
                    {
                        var changed = compositeType.AccessModifier != modifier;
                        compositeType.AccessModifier = modifier;
                        errorProvider.SetError(cbxAccess, null);
                        error = false;
                        if (changed)
                            OnValueChanged(new EditorInfo(cbxAccess));
                        break;
                    }
                }
            }
            catch (BadSyntaxException ex)
            {
                errorProvider.SetError(cbxAccess, ex.Message);
                error = true;
            }
        }

        private void cbxModifier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locked || !(compositeType is ClassType))
                return;
            try
            {
                string selectedModifierString = cbxModifier.SelectedItem.ToString();
                var type = compositeType as ClassType;
                foreach (ClassModifier modifier in type.Language.ValidClassModifiers.Keys)
                {
                    if (type.Language.ValidClassModifiers[modifier] == selectedModifierString)
                    {
                        var changed = type.Modifier != modifier;
                        type.Modifier = modifier;
                        errorProvider.SetError(cbxModifier, null);
                        error = false;
                        if (changed)
                            OnValueChanged(new EditorInfo(cbxModifier));
                        break;
                    }
                }
            }
            catch (BadSyntaxException ex)
            {
                errorProvider.SetError(cbxModifier, ex.Message);
                error = true;
            }
        }

        private void cbxModifier_Validated(object sender, EventArgs e)
        {
            if ((compositeType is ClassType) && errorProvider.GetError(cbxModifier).IsNotEmpty())
            {
                var type = compositeType as ClassType;
                errorProvider.SetError(cbxModifier, null);
                locked = true;
                cbxModifier.SelectedItem = type.Language.ValidClassModifiers[type.Modifier];
                locked = false;
                error = false;
            }
        }

        private void cbxAccess_Validated(object sender, EventArgs e)
        {
            if (compositeType != null && errorProvider.GetError(cbxAccess).IsNotEmpty())
            {
                errorProvider.SetError(cbxAccess, null);
                locked = true;
                cbxAccess.SelectedItem = compositeType.Language.ValidAccessModifiers[compositeType.AccessModifier];
                locked = false;
                error = false;
            }
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (locked || compositeType == null)
                return;
            try
            {
                var changed = compositeType.Name != txtName.Text;
                compositeType.Name = txtName.Text;
                errorProvider.SetError(txtName, null);
                error = false;
                if(changed)
                    OnValueChanged(new EditorInfo(txtName));
            }
            catch (BadSyntaxException ex)
            {
                e.Cancel = true;
                errorProvider.SetError(txtName, ex.Message);
                error = true;
            }
        }
        
        void TxtTableNameValidating(object sender, CancelEventArgs e)
        {
            if (locked || !(compositeType is SingleInharitanceType))
                return;
            try
            {
            	var type = compositeType as SingleInharitanceType;
                var changed = type.TableName != txtTableName.Text;
                type.TableName = txtTableName.Text;
                errorProvider.SetError(txtTableName, null);
                error = false;
                if(changed)
                    OnValueChanged(new EditorInfo(txtTableName));
            }
            catch (BadSyntaxException ex)
            {
                e.Cancel = true;
                errorProvider.SetError(txtTableName, ex.Message);
                error = true;
            }
        }

        private void txtComments_Validating(object sender, CancelEventArgs e)
        {
            if (locked || compositeType == null)
                return;
            bool changed = compositeType.Comments != txtComments.Text;
            compositeType.Comments = txtComments.Text;
            if (changed)
                OnValueChanged(new EditorInfo(txtComments));
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Insert)
            {
                if(memberListEditor.Validate())
                    memberListEditor.AddNewMember();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
