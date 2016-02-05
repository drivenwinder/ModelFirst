using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAP.Entity;
using EAP.ModelFirst.Core.Project.Members;
using EAP.ModelFirst.Core.Project.Entities;
using System.Data;
using System.ComponentModel;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Relationships;
using EAP.ModelFirst.CodeGenerator;

namespace EAP.ModelFirst.Controls.Dialogs
{
    public class DbSchemaModels : DataObjectList<DbSchemaModel>
    {
        public void Load(IEnumerable<SingleInharitanceType> types)
        {
            BeginInit();
            Clear();
            LoadSchemaModels(types);
            EndInit();
        }

        private void LoadSchemaModels(IEnumerable<SingleInharitanceType> types)
        {
            List<DbSchemaModel> lst = new List<DbSchemaModel>();

            List<SingleInharitanceType> typeList = new List<SingleInharitanceType>();
            foreach (var t in types)
            {
                DbTable table = new DbTable(t);
                foreach (var f in t.Fields)
                {
                    DbColumn column = new DbColumn(f);
                    Add(new DbSchemaModel(table, column));
                }
            }
        }
    }

    public class DbSchemaModel : DataObject<DbSchemaModel>
    {
        DbTable table;
        DbColumn column;

        public DbSchemaModel()
        {
            table = new DbTable();
            column = new DbColumn();
        }

        public DbSchemaModel(DbTable t, DbColumn c)
        {
            table = t;
            column = c;
        }

        [Browsable(false)]
        public SingleInharitanceType Type
        {
            get { return table.Type; }
        }

        [Browsable(false)]
        public Field Field
        {
            get { return column.Field; }
        }

        public string TableName
        {
            get { return table.TableName; }
            set
            {
                if (table.TableName != value)
                {
                    var old = table.TableName;
                    table.TableName = value;
                    if (DataState != Entity.DataState.Initializing)
                        OnValueChanged("TableName", value, old);
                    OnPropertyChanged("TableName");
                }
            }
        }

        public string TypeName
        {
            get { return column.TypeName; }
            set
            {
                if (column.TypeName != value)
                {
                    var old = column.TypeName;
                    column.TypeName = value;
                    if (DataState != Entity.DataState.Initializing)
                        OnValueChanged("TypeName", value, old);
                    OnPropertyChanged("TypeName");
                }
            }
        }

        public string MemberName
        {
            get { return column.MemberName; }
            set
            {
                if (column.MemberName != value)
                {
                    var old = column.MemberName;
                    column.MemberName = value;
                    if (DataState != Entity.DataState.Initializing)
                        OnValueChanged("MemberName", value, old);
                    OnPropertyChanged("MemberName");
                }
            }
        }

        public string MemberType
        {
            get { return column.MemberType; }
            set
            {
                if (column.MemberType != value)
                {
                    var old = column.MemberType;
                    column.MemberType = value;
                    if (DataState != Entity.DataState.Initializing)
                        OnValueChanged("MemberType", value, old);
                    OnPropertyChanged("MemberType");
                }
            }
        }

        public bool GenerateDbColumn
        {
            get { return column.GenerateDbColumn; }
            set
            {
                if (column.GenerateDbColumn != value)
                {
                    var old = column.GenerateDbColumn;
                    column.GenerateDbColumn = value;
                    if (DataState != Entity.DataState.Initializing)
                        OnValueChanged("GenerateDbColumn", value, old);
                    OnPropertyChanged("GenerateDbColumn");
                }
            }
        }

        public string ColumnName
        {
            get { return column.ColumnName; }
            set
            {
                if (column.ColumnName != value)
                {
                    var old = column.ColumnName;
                    column.ColumnName = value;
                    if (DataState != Entity.DataState.Initializing)
                        OnValueChanged("ColumnName", value, old);
                    OnPropertyChanged("ColumnName");
                }
            }
        }

        public DbType DbType
        {
            get { return column.DbType; }
            set
            {
                if (column.DbType != value)
                {
                    var old = column.DbType;
                    column.DbType = value;
                    if (DataState != Entity.DataState.Initializing)
                        OnValueChanged("DbType", value, old);
                    OnPropertyChanged("DbType");
                }
            }
        }

        public string Length
        {
            get { return column.Length; }
            set
            {
                if (column.Length != value)
                {
                    var old = column.Length;
                    column.Length = value;
                    if (DataState != Entity.DataState.Initializing)
                        OnValueChanged("Length", value, old);
                    OnPropertyChanged("Length");
                }
            }
        }

        public bool NotNull
        {
            get { return column.NotNull; }
            set
            {
                if (column.NotNull != value)
                {
                    var old = column.NotNull;
                    column.NotNull = value;
                    if (DataState != Entity.DataState.Initializing)
                        OnValueChanged("NotNull", value, old);
                    OnPropertyChanged("NotNull");
                }
            }
        }

        public bool PrimaryKey
        {
            get { return column.PrimaryKey; }
            set
            {
                if (column.PrimaryKey != value)
                {
                    var old = column.PrimaryKey;
                    column.PrimaryKey = value;
                    if (DataState != Entity.DataState.Initializing)
                        OnValueChanged("PrimaryKey", value, old);
                    OnPropertyChanged("PrimaryKey");
                }
            }
        }

        public bool AutoIncrement
        {
            get { return column.AutoIncrement; }
            set
            {
                if (column.AutoIncrement != value)
                {
                    var old = column.AutoIncrement;
                    column.AutoIncrement = value;
                    if (DataState != Entity.DataState.Initializing)
                        OnValueChanged("AutoIncrement", value, old);
                    OnPropertyChanged("AutoIncrement");
                }
            }
        }

        public bool Index
        {
            get { return column.Index; }
            set
            {
                if (column.Index != value)
                {
                    var old = column.Index;
                    column.Index = value;
                    if (DataState != Entity.DataState.Initializing)
                        OnValueChanged("Index", value, old);
                    OnPropertyChanged("Index");
                }
            }
        }

        public string DefaultValue
        {
            get { return column.DefaultValue; }
            set
            {
                if (column.DefaultValue != value)
                {
                    var old = column.DefaultValue;
                    column.DefaultValue = value;
                    if (DataState != Entity.DataState.Initializing)
                        OnValueChanged("DefaultValue", value, old);
                    OnPropertyChanged("DefaultValue");
                }
            }
        }

        public string Comments
        {
            get { return column.Comments; }
            set
            {
                if (column.Comments != value)
                {
                    var old = column.Comments;
                    column.Comments = value;
                    if (DataState != Entity.DataState.Initializing)
                        OnValueChanged("Comments", value, old);
                    OnPropertyChanged("Comments");
                }
            }
        }
    }

    public class DbTable
    {
        SingleInharitanceType type;

        [Browsable(false)]
        public SingleInharitanceType Type
        {
            get { return type; }
            set
            {
                type = value;
                TableName = type.TableName;
            }
        }

        public string TableName { get; set; }

        public DbTable()
        {
        }

        public DbTable(SingleInharitanceType t)
        {
            Type = t;
        }
    }

    public class DbColumn
    {
        Field field;

        [Browsable(false)]
        public Field Field
        {
            get { return field; }
            set
            {
                field = value;
                GenerateDbColumn = field.GenerateDbColumn;
                TypeName = field.Parent.Name;
                MemberName = field.Name;
                MemberType = field.Type;
                ColumnName = field.DbSchema.Name;
                if (field.DbSchema.DbType.HasValue)
                    DbType = field.DbSchema.DbType.Value;
                else
                    DbType = Util.GetDbType(field.Type);
                Length = field.DbSchema.Length;
                NotNull = field.DbSchema.NotNull;
                PrimaryKey = field.DbSchema.IsPrimaryKey;
                AutoIncrement = field.DbSchema.AutoIncrement;
                Index = field.DbSchema.Index;
                DefaultValue = field.DbSchema.DefaultValue;
                Comments = field.Comments;
            }
        }

        public DbColumn()
        {
        }

        public DbColumn(Field f)
        {
            Field = f;
        }
        bool generateDbColumn;
        public bool GenerateDbColumn
        {
            get { return generateDbColumn; }
            set
            {
                if (value)
                {
                    if (ColumnName.IsNullOrWhiteSpace())
                        ColumnName = field.Name;
                    if (field.DbSchema.DbType.HasValue)
                        DbType = field.DbSchema.DbType.Value;
                    else
                        DbType = Util.GetDbType(field.Type);
                }
                generateDbColumn = value;
            }
        }

        public string TypeName { get; set; }

        public string MemberName { get; set; }

        public string MemberType { get; set; }

        public string ColumnName { get; set; }

        public DbType DbType { get; set; }

        public string Length { get; set; }

        public bool NotNull { get; set; }

        public bool PrimaryKey { get; set; }

        public bool AutoIncrement { get; set; }

        public bool Index { get; set; }

        public string DefaultValue { get; set; }

        public string Comments { get; set; }
    }
}
