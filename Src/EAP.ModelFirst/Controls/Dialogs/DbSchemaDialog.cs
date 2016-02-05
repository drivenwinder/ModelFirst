using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EAP.ModelFirst.Core.Project;
using EAP.Entity;
using EAP.Collections;
using EAP.ModelFirst.Utils;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Controls.Dialogs
{
    public partial class DbSchemaDialog : DialogForm
    {
        IEnumerable<SingleInharitanceType> types;

        DbSchemaModels models = new DbSchemaModels();

        public DbSchemaDialog()
        {
            InitializeComponent();
            InitDbType();
            UpdateTexts();
        }

        void InitDbType()
        {
            ValueTextList lst = new ValueTextList();
            foreach (var t in Enum.GetValues(typeof(DbType)))
                lst.Add(t, t.ToString());
            dbTypeBindingSource.DataSource = lst;
        }

        public void UpdateTexts()
        {
            btnClose.Text = Strings.ButtonCancel;
            btnOk.Text = Strings.ButtonOK;
            btnUndo.Text = Strings.Undo;
            btnRedo.Text = Strings.Redo;
        }

        public void ShowDialog(IEnumerable<SingleInharitanceType> types)
        {
            this.types = types;
            models.Load(types);
            dbSchemaModelBindingSource.DataSource = models;
            models.ListChanged += new EventHandler<ListChangedEventArgs<DbSchemaModel>>(models_ListChanged);
            models.BeginEdit();
            RefreshButtonStatus();
            ShowDialog();
        }

        void models_ListChanged(object sender, ListChangedEventArgs<DbSchemaModel> e)
        {
            RefreshButtonStatus();
        }

        void RefreshButtonStatus()
        {
            btnUndo.Enabled = models.CanUndo;
            btnRedo.Enabled = models.CanRedo;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            foreach (var m in models)
            {
                if (m.DataState == DataState.Modified)
                {
                    m.Type.TableName = m.TableName;
                    m.Field.GenerateDbColumn = m.GenerateDbColumn;
                    m.Field.Comments = m.Comments;
                    m.Field.DbSchema.Name = m.ColumnName;
                    m.Field.DbSchema.Length = m.Length;
                    m.Field.DbSchema.IsPrimaryKey = m.PrimaryKey;
                    m.Field.DbSchema.Index = m.Index;
                    m.Field.DbSchema.DefaultValue = m.DefaultValue;
                    m.Field.DbSchema.DbType = m.DbType;
                    m.Field.DbSchema.AutoIncrement = m.AutoIncrement;
                    m.Field.DbSchema.NotNull = m.NotNull;
                }
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            gridView.EndEdit();
            if (models.CanUndo)
            {
                EditedObject<DbSchemaModel> edited = models.Undo();
                SetFocused(edited);
            }
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            gridView.EndEdit();
            if (models.CanRedo)
            {
                EditedObject<DbSchemaModel> edited = models.Redo();
                SetFocused(edited);
            }
        }

        void SetFocused(EditedObject<DbSchemaModel> edited)
        {
            if (edited.NewState == DataState.Modified)
            {
                int index = models.IndexOf(edited.DataObject);
                gridView.ClearSelection();
                gridView.Rows[index].Cells[gridView.Columns["col"
                    + edited.PropertyName].DisplayIndex].Selected = true;
            }
        }

        private void gridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < gridView.Rows.Count; i++)
                gridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
        }

        private void gridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
            e.ThrowException = false;
            Client.ShowError("Error in row[" + (e.RowIndex + 1) + "] column["
                + (e.ColumnIndex + 1) + "]:" + e.Exception.ToString());
        }

        private void gridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            gridView.Refresh();
        }
    }
}
