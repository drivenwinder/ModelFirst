using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Design;

namespace EAP.Win.UI
{
    [ListBindable(false)]
    public class FilterColumnCollection : BaseCollection, IList
    {
        private ArrayList items = new ArrayList();
        private FilterControl filterControl;

        public FilterColumnCollection(FilterControl filterControl)
        {
            this.filterControl = filterControl;
        }
        CollectionChangeEventHandler onCollectionChanged { get; set; }
        public event CollectionChangeEventHandler CollectionChanged
        {
            add { onCollectionChanged += value; }
            remove { onCollectionChanged -= value; }
        }

        protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
        {
            if (onCollectionChanged != null)
            {
                onCollectionChanged(this, e);
            }
        }

        public int Add(FilterColumn column)
        {
            int result = items.Add(column);
            OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
            return result;
        }

        public FilterColumn this[int index]
        {
            get { return items[index] as FilterColumn; }
        }

        public void Insert(int index, FilterColumn column)
        {
            items.Insert(index, column);
            OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
        }

        public void RemoveAt(int index)
        {
            FilterColumn column = (FilterColumn)this.items[index];
            items.RemoveAt(index);
            OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, column));
        }

        public void AddRange(FilterColumn[] columns)
        {
            items.AddRange(columns);
            OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
        }

        public bool Contains(string name)
        {
            foreach (FilterColumn item in items)
                if (item.Name == name)
                    return true;
            return false;
        }

        public void Remove(FilterColumn column)
        {
            items.Remove(column);
            OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, column));
        }

        public void Clear()
        {
            items.Clear();
            OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
        }

        public int IndexOf(FilterColumn filterColumn)
        {
            return items.IndexOf(filterColumn);
        }

        protected override ArrayList List
        {
            get
            {
                return this.items;
            }
        }

        int ICollection.Count
        {
            get
            {
                return this.items.Count;
            }
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                return false;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                return this;
            }
        }

        bool IList.IsFixedSize
        {
            get
            {
                return false;
            }
        }

        bool IList.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this.items.CopyTo(array, index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        int IList.Add(object value)
        {
            return this.Add((FilterColumn)value);
        }

        void IList.Clear()
        {
            this.Clear();
        }

        bool IList.Contains(object value)
        {
            return this.items.Contains(value);
        }

        int IList.IndexOf(object value)
        {
            return this.items.IndexOf(value);
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (FilterColumn)value);
        }

        void IList.Remove(object value)
        {
            this.Remove((FilterColumn)value);
        }

        void IList.RemoveAt(int index)
        {
            this.RemoveAt(index);
        }
    }

    [DesignTimeVisible(false), Designer("EAP.Win.UI.Design.FilterColumnDesigner, EAP.Win.UI.Design"), ToolboxItem(false)]
    //[TypeConverter(typeof(FilterColumnConverter))]
    public abstract class FilterColumn : Component
    {
        string name;
        int width = 180;

        [Localizable(true), DefaultValue("")]
        public string ToolTipText { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public virtual object Tag { get; set; }

        [Browsable(false)]
        public string Name
        {
            get
            {
                if ((this.Site != null) && this.Site.Name.IsNotEmpty())
                {
                    this.name = this.Site.Name;
                }
                return this.name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.name = string.Empty;
                }
                else
                {
                    this.name = value;
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public virtual FilterControl FilterControl { get; set; }

        [RefreshProperties(RefreshProperties.Repaint), Localizable(true), DefaultValue(180)]
        public virtual int Width
        {
            get { return width; }
            set { width = value; }
        }

        [Localizable(true)]
        public virtual string HeaderText { get; set; }

        [DefaultValue(true), Localizable(true)]
        public virtual bool Visible { get; set; }

        [Browsable(true), DefaultValue(""), TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), Editor("EAP.Win.UI.Design.FilterColumnDataPropertyNameEditor, EAP.Win.UI.Design", typeof(UITypeEditor))]
        public virtual string DataPropertyName { get; set; }

        public FilterColumn Clone()
        {
            FilterColumn column = (FilterColumn)Activator.CreateInstance(base.GetType());
            if (column != null)
            {
                this.CloneInternal(column);
            }
            return column;
        }

        protected internal virtual void CloneInternal(FilterColumn column)
        {
            column.Name = Name;
            column.HeaderText = HeaderText;
            column.DataPropertyName = DataPropertyName;
            column.Tag = Tag;
        }

        public Control CreateControl()
        {
            Control ctl = InitialControl();
            ctl.Width = Width;
            if (ToolTipText.IsNotEmpty())
            {
                ToolTip tip = new ToolTip();
                tip.SetToolTip(ctl, ToolTipText);
            }
            return ctl;
        }

        protected abstract Control InitialControl();

        public abstract object ExtractValues(Control control);
    }
}
