using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Core.Project.Members
{
    public class DbSchema : IModifiable
    {
        private string name;
        private System.Data.DbType? dbType;
        private string defaultValue;
        private bool index;
        private bool autoIncrement;
        private bool notNull;
        private string length;
        private bool isPrimaryKey;

        #region IModifiable

        public event EventHandler Modified;

        public bool IsDirty { get; private set; }

        public virtual void Clean()
        {
            IsDirty = false;
        }

        #endregion

        protected internal bool Initialing { get; set; }

        protected virtual void OnModified(EventArgs e)
        {
            if (Initialing) return;
            IsDirty = true;
            if (Modified != null)
                Modified(this, e);
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (!Initialing && value.IsNullOrEmpty())
                    throw new SyntaxErrorException(Strings.ErrorNameIsRequired);
                if (name != value)
                {
                    name = value;
                    OnModified(EventArgs.Empty);
                }
            }
        }

        public DbType? DbType
        {
            get { return dbType; }
            set
            {
                if (dbType != value)
                {
                    dbType = value;
                    OnModified(EventArgs.Empty);
                }
            }
        }

        public bool IsPrimaryKey
        {
            get { return isPrimaryKey; }
            set
            {
                if (isPrimaryKey != value)
                {
                    isPrimaryKey = value;
                    OnModified(EventArgs.Empty);
                }
            }
        }

        public string Length
        {
            get { return length; }
            set
            {
                if (length != value)
                {
                    length = value;
                    OnModified(EventArgs.Empty);
                }
            }
        }

        public bool NotNull
        {
            get { return notNull; }
            set
            {
                if (notNull != value)
                {
                    notNull = value;
                    OnModified(EventArgs.Empty);
                }
            }
        }

        public bool AutoIncrement
        {
            get { return autoIncrement; }
            set
            {
                if (autoIncrement != value)
                {
                    autoIncrement = value;
                    OnModified(EventArgs.Empty);
                }
            }
        }

        public bool Index
        {
            get { return index; }
            set
            {
                if (index != value)
                {
                    index = value;
                    OnModified(EventArgs.Empty);
                }
            }
        }

        public string DefaultValue
        {
            get { return defaultValue; }
            set
            {
                if (defaultValue != value)
                {
                    defaultValue = value;
                    OnModified(EventArgs.Empty);
                }
            }
        }
    }
}
