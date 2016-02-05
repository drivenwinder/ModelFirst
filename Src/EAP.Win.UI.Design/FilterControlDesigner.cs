using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace EAP.Win.UI.Design
{
    public class FilterControlDesigner : ControlDesigner
    {
        private DesignerActionListCollection actionLists;
        private CurrencyManager cm;
        protected DesignerVerbCollection designerVerbs;
        private static System.Type typeofFilterCheckBoxColumn = typeof(FilterCheckBoxColumn);
        private static System.Type typeofFilterTextBoxColumn = typeof(FilterTextBoxColumn);
        private static System.Type typeofFilterDatePickerColumn = typeof(FilterDateTimePickerColumn);
        private static System.Type typeofIList = typeof(IList);

        public FilterControlDesigner()
        {
            base.AutoResizeHandles = true;
        }

        private void BuildActionLists()
        {
            this.actionLists = new DesignerActionListCollection();
            this.actionLists.Add(new FilterControlChooseDataSourceActionList(this));
            this.actionLists.Add(new FilterColumnEditingActionList(this));
            this.actionLists[0].AutoShow = true;
        }

        private void filter_ColumnRemoved(object sender, FilterColumnEventArgs e)
        {
            //if (e.Column != null)
            //{
            //    e.Column.DisplayIndex = -1;
            //}
        }

        private void filterChanged(object sender, EventArgs e)
        {
            FilterControl component = (FilterControl)base.Component;
            CurrencyManager manager = null;
            if ((component.DataSource != null) && (component.BindingContext != null))
            {
                manager = (CurrencyManager)component.BindingContext[component.DataSource, component.DataMember];
            }
            if (manager != this.cm)
            {
                if (this.cm != null)
                {
                    this.cm.MetaDataChanged -= new EventHandler(this.dataGridViewMetaDataChanged);
                }
                this.cm = manager;
                if (this.cm != null)
                {
                    this.cm.MetaDataChanged += new EventHandler(this.dataGridViewMetaDataChanged);
                }
            }
            if (component.BindingContext == null || component.DataSource != null)
            {
                MakeSureColumnsAreSited(component);
            }
            else
            {
                this.RefreshColumnCollection();
            }
        }

        private void FilterControlDesigner_ComponentRemoving(object sender, ComponentEventArgs e)
        {
            FilterControl component = base.Component as FilterControl;
            if ((e.Component != null) && (e.Component == component.DataSource))
            {
                IComponentChangeService service = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
                string dataMember = component.DataMember;
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
                PropertyDescriptor member = (properties != null) ? properties["DataMember"] : null;
                if ((service != null) && (member != null))
                {
                    service.OnComponentChanging(component, member);
                }
                component.DataSource = null;
                if ((service != null) && (member != null))
                {
                    service.OnComponentChanged(component, member, dataMember, "");
                }
            }
        }

        private void dataGridViewMetaDataChanged(object sender, EventArgs e)
        {
            this.RefreshColumnCollection();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                FilterControl component = base.Component as FilterControl;
                component.DataSourceChanged -= new EventHandler(this.filterChanged);
                component.DataMemberChanged -= new EventHandler(this.filterChanged);
                component.BindingContextChanged -= new EventHandler(this.filterChanged);
                component.ColumnRemoved -= new FilterControlColumnEventHandler(this.filter_ColumnRemoved);
                if (this.cm != null)
                {
                    this.cm.MetaDataChanged -= new EventHandler(this.dataGridViewMetaDataChanged);
                }
                this.cm = null;
                if (base.Component.Site != null)
                {
                    IComponentChangeService service = base.Component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                    if (service != null)
                    {
                        service.ComponentRemoving -= new ComponentEventHandler(this.FilterControlDesigner_ComponentRemoving);
                    }
                }
            }
            base.Dispose(disposing);
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            if (component.Site != null)
            {
                IComponentChangeService service = component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                if (service != null)
                {
                    service.ComponentRemoving += new ComponentEventHandler(this.FilterControlDesigner_ComponentRemoving);
                }
            }
            FilterControl view = (FilterControl)component;
            view.DataSourceChanged += new EventHandler(this.filterChanged);
            view.DataMemberChanged += new EventHandler(this.filterChanged);
            view.BindingContextChanged += new EventHandler(this.filterChanged);
            this.filterChanged(base.Component, EventArgs.Empty);
            view.ColumnRemoved += new FilterControlColumnEventHandler(this.filter_ColumnRemoved);
        }

        public override void InitializeNewComponent(IDictionary defaultValues)
        {
            base.InitializeNewComponent(defaultValues);
        }

        private static void MakeSureColumnsAreSited(FilterControl filter)
        {
            IContainer container = (filter.Site != null) ? filter.Site.Container : null;
            for (int i = 0; i < filter.Columns.Count; i++)
            {
                FilterColumn component = filter.Columns[i];
                IContainer container2 = (component.Site != null) ? component.Site.Container : null;
                if (container != container2)
                {
                    if (container2 != null)
                    {
                        container2.Remove(component);
                    }
                    if (container != null)
                    {
                        container.Add(component);
                    }
                }
            }
        }

        public void OnAddColumn(object sender, EventArgs e)
        {
            try
            {
                DesignerTransaction transaction = (base.Component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost).CreateTransaction("AddColumnTransactionString");
                DialogResult cancel = DialogResult.Cancel;
                FilterAddColumnDialog dialog = new FilterAddColumnDialog(((FilterControl)base.Component).Columns, (FilterControl)base.Component);
                dialog.Start(((FilterControl)base.Component).Columns.Count, true);
                try
                {
                    cancel = this.ShowDialog(dialog);
                }
                finally
                {
                    if (cancel == DialogResult.OK)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Cancel();
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        public void OnEditColumns(object sender, EventArgs e)
        {
            IDesignerHost service = base.Component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
            FilterColumnCollectionDialog dialog = new FilterColumnCollectionDialog(((FilterControl)base.Component).Site);
            dialog.SetLiveFilterControl((FilterControl)base.Component);
            DesignerTransaction transaction = service.CreateTransaction("EditColumnsTransactionString");
            DialogResult cancel = DialogResult.Cancel;
            try
            {
                cancel = this.ShowDialog(dialog);
            }
            finally
            {
                if (cancel == DialogResult.OK)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Cancel();
                }
            }
        }

        protected override void PreFilterProperties(IDictionary properties)
        {
            base.PreFilterProperties(properties);
            string[] strArray = new string[] { "AutoSizeColumnsMode", "DataSource" };
            Attribute[] attributes = new Attribute[0];
            for (int i = 0; i < strArray.Length; i++)
            {
                PropertyDescriptor oldPropertyDescriptor = (PropertyDescriptor)properties[strArray[i]];
                if (oldPropertyDescriptor != null)
                {
                    properties[strArray[i]] = TypeDescriptor.CreateProperty(typeof(FilterControlDesigner), oldPropertyDescriptor, attributes);
                }
            }
        }

        private bool ProcessSimilarSchema(FilterControl dataGridView)
        {
            PropertyDescriptorCollection itemProperties = null;
            if (this.cm != null)
            {
                try
                {
                    itemProperties = this.cm.GetItemProperties();
                }
                catch (ArgumentException exception)
                {
                    throw new InvalidOperationException("DataSourceNoLongerValid", exception);
                }
            }
            IContainer container = (dataGridView.Site != null) ? dataGridView.Site.Container : null;
            bool flag = false;
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                FilterColumn component = dataGridView.Columns[i];
                if (component.DataPropertyName.IsNotEmpty())
                {
                    PropertyDescriptor descriptor = TypeDescriptor.GetProperties(component)["UserAddedColumn"];
                    if ((descriptor == null) || !((bool)descriptor.GetValue(component)))
                    {
                        PropertyDescriptor descriptor2 = (itemProperties != null) ? itemProperties[component.DataPropertyName] : null;
                        bool flag2 = false;
                        if (descriptor2 == null)
                        {
                            flag2 = true;
                        }
                        else if (typeofIList.IsAssignableFrom(descriptor2.PropertyType) && !TypeDescriptor.GetConverter(typeof(Image)).CanConvertFrom(descriptor2.PropertyType))
                        {
                            flag2 = true;
                        }
                        flag = !flag2;
                        if (flag)
                        {
                            break;
                        }
                    }
                }
            }
            if (flag)
            {
                IComponentChangeService service = base.Component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                PropertyDescriptor member = TypeDescriptor.GetProperties(base.Component)["Columns"];
                try
                {
                    service.OnComponentChanging(base.Component, member);
                }
                catch (InvalidOperationException)
                {
                    return flag;
                }
                int num2 = 0;
                while (num2 < dataGridView.Columns.Count)
                {
                    FilterColumn column2 = dataGridView.Columns[num2];
                    if (string.IsNullOrEmpty(column2.DataPropertyName))
                    {
                        num2++;
                    }
                    else
                    {
                        PropertyDescriptor descriptor4 = TypeDescriptor.GetProperties(column2)["UserAddedColumn"];
                        if ((descriptor4 != null) && ((bool)descriptor4.GetValue(column2)))
                        {
                            num2++;
                            continue;
                        }
                        PropertyDescriptor descriptor5 = (itemProperties != null) ? itemProperties[column2.DataPropertyName] : null;
                        bool flag3 = false;
                        if (descriptor5 == null)
                        {
                            flag3 = true;
                        }
                        else if (typeofIList.IsAssignableFrom(descriptor5.PropertyType) && !TypeDescriptor.GetConverter(typeof(Image)).CanConvertFrom(descriptor5.PropertyType))
                        {
                            flag3 = true;
                        }
                        if (flag3)
                        {
                            dataGridView.Columns.Remove(column2);
                            if (container != null)
                            {
                                container.Remove(column2);
                            }
                        }
                        else
                        {
                            num2++;
                        }
                    }
                }
                service.OnComponentChanged(base.Component, member, null, null);
            }
            return flag;
        }

        private void RefreshColumnCollection()
        {
            FilterControl component = (FilterControl)base.Component;
            ISupportInitializeNotification dataSource = component.DataSource as ISupportInitializeNotification;
            if ((dataSource == null) || dataSource.IsInitialized)
            {
                IComponentChangeService service = null;
                PropertyDescriptor member = null;
                IDesignerHost provider = base.Component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (!this.ProcessSimilarSchema(component))
                {
                    PropertyDescriptorCollection itemProperties = null;
                    if (this.cm != null)
                    {
                        try
                        {
                            itemProperties = this.cm.GetItemProperties();
                        }
                        catch (ArgumentException exception)
                        {
                            throw new InvalidOperationException("DataSourceNoLongerValid", exception);
                        }
                    }
                    IContainer container = (component.Site != null) ? component.Site.Container : null;
                    service = base.Component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                    member = TypeDescriptor.GetProperties(base.Component)["Columns"];
                    service.OnComponentChanging(base.Component, member);
                    FilterColumn[] columnArray = new FilterColumn[component.Columns.Count];
                    int index = 0;
                    for (int i = 0; i < component.Columns.Count; i++)
                    {
                        FilterColumn column = component.Columns[i];
                        if (column.DataPropertyName.IsNotEmpty())
                        {
                            PropertyDescriptor descriptor2 = TypeDescriptor.GetProperties(column)["UserAddedColumn"];
                            if ((descriptor2 == null) || !((bool)descriptor2.GetValue(column)))
                            {
                                columnArray[index] = column;
                                index++;
                            }
                        }
                    }
                    for (int j = 0; j < index; j++)
                    {
                        component.Columns.Remove(columnArray[j]);
                    }
                    service.OnComponentChanged(base.Component, member, null, null);
                    if (container != null)
                    {
                        for (int m = 0; m < index; m++)
                        {
                            container.Remove(columnArray[m]);
                        }
                    }
                    FilterColumn[] columnArray2 = null;
                    int num5 = 0;
                    if (component.DataSource != null)
                    {
                        columnArray2 = new FilterColumn[itemProperties.Count];
                        num5 = 0;
                        for (int n = 0; n < itemProperties.Count; n++)
                        {
                            System.Type typeofDataGridViewImageColumn;
                            FilterColumn column2 = null;
                            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Image));
                            System.Type propertyType = itemProperties[n].PropertyType;
                            if ((propertyType == typeof(bool)) || (propertyType == typeof(CheckState)))
                            {
                                typeofDataGridViewImageColumn = typeofFilterCheckBoxColumn;
                            }
                            else
                            {
                                typeofDataGridViewImageColumn = typeofFilterTextBoxColumn;
                            }
                            string name = DesignerUtils.NameFromText(itemProperties[n].Name, typeofDataGridViewImageColumn, base.Component.Site);
                            column2 = TypeDescriptor.CreateInstance(provider, typeofDataGridViewImageColumn, null, null) as FilterColumn;
                            column2.DataPropertyName = itemProperties[n].Name;
                            column2.HeaderText = itemProperties[n].DisplayName.IsNotEmpty() ? itemProperties[n].DisplayName : itemProperties[n].Name;
                            column2.Name = itemProperties[n].Name;
                            provider.Container.Add(column2, name);
                            columnArray2[num5] = column2;
                            num5++;
                        }
                    }
                    service.OnComponentChanging(base.Component, member);
                    for (int k = 0; k < num5; k++)
                    {
                        component.Columns.Add(columnArray2[k]);
                    }
                    service.OnComponentChanged(base.Component, member, null, null);
                }
            }
        }

        private bool ShouldSerializeDataSource()
        {
            return (((FilterControl)base.Component).DataSource != null);
        }

        private DialogResult ShowDialog(Form dialog)
        {
            IUIService service = base.Component.Site.GetService(typeof(IUIService)) as IUIService;
            if (service != null)
            {
                return service.ShowDialog(dialog);
            }
            return dialog.ShowDialog(base.Component as IWin32Window);
        }

        internal static void ShowErrorDialog(IUIService uiService, Exception ex, Control filter)
        {
            if (uiService != null)
            {
                uiService.ShowError(ex);
            }
            else
            {
                MessageBox.Show(filter, ex.ToString(), null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, 0);
            }
        }

        internal static void ShowErrorDialog(IUIService uiService, string errorString, Control filter)
        {
            if (uiService != null)
            {
                uiService.ShowError(errorString);
            }
            else
            {
                MessageBox.Show(filter, errorString, null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, 0);
            }
        }

        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (this.actionLists == null)
                {
                    this.BuildActionLists();
                }
                return this.actionLists;
            }
        }

        public override ICollection AssociatedComponents
        {
            get
            {
                FilterControl component = base.Component as FilterControl;
                if (component != null)
                {
                    return component.Columns;
                }
                return base.AssociatedComponents;
            }
        }

        public object DataSource
        {
            get
            {
                return ((FilterControl)base.Component).DataSource;
            }
            set
            {
                FilterControl component = base.Component as FilterControl;
                ((FilterControl)base.Component).DataSource = value;
            }
        }

        protected override System.ComponentModel.InheritanceAttribute InheritanceAttribute
        {
            get
            {
                if ((base.InheritanceAttribute != System.ComponentModel.InheritanceAttribute.Inherited) && (base.InheritanceAttribute != System.ComponentModel.InheritanceAttribute.InheritedReadOnly))
                {
                    return base.InheritanceAttribute;
                }
                return System.ComponentModel.InheritanceAttribute.InheritedReadOnly;
            }
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (this.designerVerbs == null)
                {
                    this.designerVerbs = new DesignerVerbCollection();
                    this.designerVerbs.Add(new DesignerVerb("Edit Columns", new EventHandler(this.OnEditColumns)));
                    this.designerVerbs.Add(new DesignerVerb("Add Column", new EventHandler(this.OnAddColumn)));
                }
                return this.designerVerbs;
            }
        }

        [ComplexBindingProperties("DataSource", "DataMember")]
        private class FilterControlChooseDataSourceActionList : DesignerActionList
        {
            private FilterControlDesigner owner;

            public FilterControlChooseDataSourceActionList(FilterControlDesigner owner)
                : base(owner.Component)
            {
                this.owner = owner;
            }

            public override DesignerActionItemCollection GetSortedActionItems()
            {
                DesignerActionItemCollection items = new DesignerActionItemCollection();
                DesignerActionPropertyItem item = new DesignerActionPropertyItem("DataSource", "Choose DataSource")
                {
                    RelatedComponent = this.owner.Component
                };
                items.Add(item);
                return items;
            }

            [AttributeProvider(typeof(IListSource))]
            public object DataSource
            {
                get
                {
                    return this.owner.DataSource;
                }
                set
                {
                    FilterControl component = (FilterControl)this.owner.Component;
                    IDesignerHost host = this.owner.Component.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
                    PropertyDescriptor member = TypeDescriptor.GetProperties(component)["DataSource"];
                    IComponentChangeService service = this.owner.Component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                    DesignerTransaction transaction = host.CreateTransaction("ChooseDataSourceTransactionString[{0}]".FormatArgs(new object[] { component.Name }));
                    try
                    {
                        service.OnComponentChanging(this.owner.Component, member);
                        this.owner.DataSource = value;
                        service.OnComponentChanged(this.owner.Component, member, null, null);
                        transaction.Commit();
                        transaction = null;
                    }
                    finally
                    {
                        if (transaction != null)
                        {
                            transaction.Cancel();
                        }
                    }
                }
            }
        }

        private class FilterColumnEditingActionList : DesignerActionList
        {
            private FilterControlDesigner owner;

            public FilterColumnEditingActionList(FilterControlDesigner owner)
                : base(owner.Component)
            {
                this.owner = owner;
            }

            public void AddColumn()
            {
                this.owner.OnAddColumn(this, EventArgs.Empty);
            }

            public void EditColumns()
            {
                this.owner.OnEditColumns(this, EventArgs.Empty);
            }

            public override DesignerActionItemCollection GetSortedActionItems()
            {
                DesignerActionItemCollection items = new DesignerActionItemCollection();
                items.Add(new DesignerActionMethodItem(this, "EditColumns", "Edit Columns", true));
                items.Add(new DesignerActionMethodItem(this, "AddColumn", "Add Column", true));
                return items;
            }
        }
    }
}
