using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EAP.ModelFirst.Core.Project.Members;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.CodeGenerator;

namespace EAP.ModelFirst.Controls.Editors.TypeEditors
{
    public partial class SchemaEditor : UserControl
    {
        Field field;
        string memberName;
        string memberType;
        bool locked = false;

        protected internal bool Error { get; private set; }

        public event EditValueChangedEventHandler ValueChanged;

        public SchemaEditor()
        {
            InitializeComponent();
            foreach (var t in Enum.GetValues(typeof(DbType)))
                cbxDbType.Items.Add(t.ToString());
        }

        public void SetMember(Member m)
        {
            if (m == null || !(m is Field))
                field = null;
            else
                field = (Field)m;
            if (m != null)
            {
                memberName = m.Name;
                memberType = m.Type;
            }
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

        private void OnValueChanged(Control control)
        {
            if (ValueChanged != null)
                ValueChanged.Invoke(this, new EditValueChangedEventArgs(new EditorInfo(control)));
        }

        void ClearFields()
        {
            locked = true;
            ckbGenColumn.Checked = false;
            txtName.Clear();
            cbxDbType.Text = null;
            txtDefaultValue.Clear();
            txtLength.Clear();
            ckbNotNull.Checked = false;
            ckbPrimaryKey.Checked = false;
            ckbAutoIncrement.Checked = false;
            cbxIndex.Checked = false;
            locked = false;
        }

        void UpdateValues()
        {
            errorProvider.Clear();
            Error = false;
            if (field == null)
            {
                ClearFields();
                Enabled = false;
                return;
            }
            Enabled = true;
            if (!field.GenerateDbColumn)
            {
                ClearFields();
            }
            else
            {
                locked = true;
                ckbGenColumn.Checked = field.GenerateDbColumn;
                var info = field.DbSchema;
                if (info.Name.IsNullOrWhiteSpace())
                    info.Name = memberName;
                txtName.Text = info.Name;
                if (!info.DbType.HasValue)
                    info.DbType = Util.GetDbType(memberType);
                cbxDbType.SelectedItem = info.DbType.Value.ToString();
                txtDefaultValue.Text = info.DefaultValue;
                txtLength.Text = info.Length;
                ckbNotNull.Checked = info.NotNull;
                ckbPrimaryKey.Checked = info.IsPrimaryKey;
                ckbAutoIncrement.Checked = info.AutoIncrement;
                cbxIndex.Checked = info.Index;
                locked = false;
            }
            UpdateEditors();
        }

        void UpdateEditors()
        {
            txtName.Enabled = ckbGenColumn.Checked;
            cbxDbType.Enabled = ckbGenColumn.Checked;
            txtDefaultValue.Enabled = ckbGenColumn.Checked;
            txtLength.Enabled = ckbGenColumn.Checked;
            ckbNotNull.Enabled = ckbGenColumn.Checked;
            ckbPrimaryKey.Enabled = ckbGenColumn.Checked;
            ckbAutoIncrement.Enabled = ckbGenColumn.Checked;
            cbxIndex.Enabled = ckbGenColumn.Checked;
        }

        private void ckbGenColumn_CheckedChanged(object sender, EventArgs e)
        {
            if (locked || field == null) return;
            bool changed = field.GenerateDbColumn != ckbGenColumn.Checked;
            field.GenerateDbColumn = ckbGenColumn.Checked;
            UpdateValues();
            if (changed)
                OnValueChanged(ckbGenColumn);
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (locked || field == null) return;
            try
            {
                errorProvider.SetError(txtName, null);
                Error = false;
                bool changed = field.DbSchema.Name != txtName.Text;
                field.DbSchema.Name = txtName.Text;
                if (changed)
                    OnValueChanged(txtName);
            }
            catch (SyntaxErrorException ex)
            {
                e.Cancel = true;
                errorProvider.SetError(txtName, ex.Message);
                Error = true;
            }
        }

        private void cbxDbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locked || field == null) return;
            bool changed = field.DbSchema.DbType != cbxDbType.SelectedItem.ConvertTo<DbType>();
            field.DbSchema.DbType = cbxDbType.SelectedItem.ConvertTo<DbType>();
            if (changed)
                OnValueChanged(cbxDbType);
        }

        private void txtDefaultValue_Validating(object sender, CancelEventArgs e)
        {
            if (locked || field == null) return;
            bool changed = field.DbSchema.DefaultValue != txtDefaultValue.Text;
            field.DbSchema.DefaultValue = txtDefaultValue.Text;
            if (changed)
                OnValueChanged(txtDefaultValue);
        }

        private void txtLength_Validating(object sender, CancelEventArgs e)
        {
            if (locked || field == null) return;
            bool changed = field.DbSchema.Length != txtLength.Text;
            field.DbSchema.Length = txtLength.Text;
            if (changed)
                OnValueChanged(txtLength);
        }

        private void ckbNotNull_CheckedChanged(object sender, EventArgs e)
        {
            if (locked || field == null) return;
            bool changed = field.DbSchema.NotNull != ckbNotNull.Checked;
            field.DbSchema.NotNull = ckbNotNull.Checked;
            if (changed)
                OnValueChanged(ckbNotNull);
        }

        private void ckbPrimaryKey_CheckedChanged(object sender, EventArgs e)
        {
            if (locked || field == null) return;
            bool changed = field.DbSchema.IsPrimaryKey != ckbPrimaryKey.Checked;
            field.DbSchema.IsPrimaryKey = ckbPrimaryKey.Checked;
            if (changed)
                OnValueChanged(ckbPrimaryKey);
        }

        private void ckbAutoIncrement_CheckedChanged(object sender, EventArgs e)
        {
            if (locked || field == null) return;
            bool changed = field.DbSchema.AutoIncrement != ckbAutoIncrement.Checked;
            field.DbSchema.AutoIncrement = ckbAutoIncrement.Checked;
            if (changed)
                OnValueChanged(ckbAutoIncrement);
        }

        private void cbxIndex_CheckedChanged(object sender, EventArgs e)
        {
            if (locked || field == null) return;
            bool changed = field.DbSchema.Index != cbxIndex.Checked;
            field.DbSchema.Index = cbxIndex.Checked;
            if (changed)
                OnValueChanged(cbxIndex);
        }
    }
}
