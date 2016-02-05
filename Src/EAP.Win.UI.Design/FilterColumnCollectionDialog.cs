using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Runtime;
using System.Threading;
using System.Drawing.Design;

namespace EAP.Win.UI.Design
{
    public partial class FilterColumnCollectionDialog : Form
    {
        private Button addButton;
        private FilterAddColumnDialog addColumnDialog;
        private TableLayoutPanel addRemoveTableLayoutPanel;
        private Button cancelButton;
        private static ColorMap[] colorMap = new ColorMap[] { new ColorMap() };
        private bool columnCollectionChanging;
        private Hashtable columnsNames;
        private FilterColumnCollection columnsPrivateCopy;
        private IComponentChangeService compChangeService;
        private FilterControl filterPrivateCopy;
        private Button deleteButton;
        private bool formIsDirty;
        private static System.Type iComponentChangeServiceType = typeof(IComponentChangeService);
        private static System.Type iHelpServiceType = typeof(IHelpService);
        private static System.Type iTypeDiscoveryServiceType = typeof(ITypeDiscoveryService);
        private static System.Type iTypeResolutionServiceType = typeof(ITypeResolutionService);
        private static System.Type iUIServiceType = typeof(IUIService);
        private const int LISTBOXITEMHEIGHT = 0x11;
        private FilterControl liveFilterControl;
        private Button moveDown;
        private Button moveUp;
        private Button okButton;
        private TableLayoutPanel okCancelTableLayoutPanel;
        private TableLayoutPanel overarchingTableLayoutPanel;
        private const int OWNERDRAWHORIZONTALBUFFER = 3;
        private const int OWNERDRAWITEMIMAGEBUFFER = 2;
        private const int OWNERDRAWVERTICALBUFFER = 4;
        private PropertyGrid propertyGrid1;
        private Label propertyGridLabel;
        private ListBox selectedColumns;
        private static Bitmap selectedColumnsItemBitmap;
        private Label selectedColumnsLabel;
        private IServiceProvider serviceProvider;
        private static System.Type toolboxBitmapAttributeType = typeof(ToolboxBitmapAttribute);
        private Hashtable userAddedColumns;

        internal FilterColumnCollectionDialog(IServiceProvider provider)
        {
            this.serviceProvider = provider;
            this.InitializeComponent();
            this.filterPrivateCopy = new FilterControl();
            this.columnsPrivateCopy = this.filterPrivateCopy.Columns;
            this.columnsPrivateCopy.CollectionChanged += new CollectionChangeEventHandler(this.columnsPrivateCopy_CollectionChanged);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            int count;
            if (this.selectedColumns.SelectedIndex == -1)
            {
                count = this.selectedColumns.Items.Count;
            }
            else
            {
                count = this.selectedColumns.SelectedIndex + 1;
            }
            if (this.addColumnDialog == null)
            {
                this.addColumnDialog = new FilterAddColumnDialog(this.columnsPrivateCopy, this.liveFilterControl);
                this.addColumnDialog.StartPosition = FormStartPosition.CenterParent;
            }
            this.addColumnDialog.Start(count, false);
            this.addColumnDialog.ShowDialog(this);
        }

        private void columnsPrivateCopy_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            if (!this.columnCollectionChanging)
            {
                this.PopulateSelectedColumns();
                if (e.Action == CollectionChangeAction.Add)
                {
                    this.selectedColumns.SelectedIndex = this.columnsPrivateCopy.IndexOf((FilterColumn)e.Element);
                    ListBoxItem selectedItem = this.selectedColumns.SelectedItem as ListBoxItem;
                    this.userAddedColumns[selectedItem.FilterColumn] = true;
                    this.columnsNames[selectedItem.FilterColumn] = selectedItem.FilterColumn.Name;
                }
                this.formIsDirty = true;
            }
        }

        private void ColumnTypeChanged(ListBoxItem item, System.Type newType)
        {
            FilterColumn filterColumn = item.FilterColumn;
            FilterColumn destColumn = Activator.CreateInstance(newType) as FilterColumn;
            ITypeResolutionService tr = this.liveFilterControl.Site.GetService(iTypeResolutionServiceType) as ITypeResolutionService;
            ComponentDesigner componentDesignerForType = FilterAddColumnDialog.GetComponentDesignerForType(tr, newType);
            CopyFilterColumnProperties(filterColumn, destColumn);
            this.columnCollectionChanging = true;
            int selectedIndex = this.selectedColumns.SelectedIndex;
            this.selectedColumns.Focus();
            base.ActiveControl = this.selectedColumns;
            try
            {
                ListBoxItem selectedItem = (ListBoxItem)this.selectedColumns.SelectedItem;
                bool flag = (bool)this.userAddedColumns[selectedItem.FilterColumn];
                string str = string.Empty;
                if (this.columnsNames.Contains(selectedItem.FilterColumn))
                {
                    str = (string)this.columnsNames[selectedItem.FilterColumn];
                    this.columnsNames.Remove(selectedItem.FilterColumn);
                }
                if (this.userAddedColumns.Contains(selectedItem.FilterColumn))
                {
                    this.userAddedColumns.Remove(selectedItem.FilterColumn);
                }
                if (selectedItem.FilterColumnDesigner != null)
                {
                    TypeDescriptor.RemoveAssociation(selectedItem.FilterColumn, selectedItem.FilterColumnDesigner);
                }
                this.selectedColumns.Items.RemoveAt(selectedIndex);
                this.selectedColumns.Items.Insert(selectedIndex, new ListBoxItem(destColumn, this, componentDesignerForType));
                this.columnsPrivateCopy.RemoveAt(selectedIndex);
                this.columnsPrivateCopy.Insert(selectedIndex, destColumn);
                if (str.IsNotEmpty())
                {
                    this.columnsNames[destColumn] = str;
                }
                this.userAddedColumns[destColumn] = flag;
                this.selectedColumns.SelectedIndex = selectedIndex;
                this.propertyGrid1.SelectedObject = this.selectedColumns.SelectedItem;
            }
            finally
            {
                this.columnCollectionChanging = false;
            }
        }

        private void CommitChanges()
        {
            if (this.formIsDirty)
            {
                try
                {
                    IComponentChangeService service = (IComponentChangeService)this.liveFilterControl.Site.GetService(iComponentChangeServiceType);
                    PropertyDescriptor member = TypeDescriptor.GetProperties(this.liveFilterControl)["Columns"];
                    IContainer container = (this.liveFilterControl.Site != null) ? this.liveFilterControl.Site.Container : null;
                    FilterColumn[] array = new FilterColumn[this.liveFilterControl.Columns.Count];
                    this.liveFilterControl.Columns.CopyTo(array, 0);
                    service.OnComponentChanging(this.liveFilterControl, member);
                    this.liveFilterControl.Columns.Clear();
                    service.OnComponentChanged(this.liveFilterControl, member, null, null);
                    if (container != null)
                    {
                        for (int m = 0; m < array.Length; m++)
                        {
                            container.Remove(array[m]);
                        }
                    }
                    FilterColumn[] columnArray2 = new FilterColumn[this.columnsPrivateCopy.Count];
                    bool[] flagArray = new bool[this.columnsPrivateCopy.Count];
                    string[] strArray = new string[this.columnsPrivateCopy.Count];
                    for (int i = 0; i < this.columnsPrivateCopy.Count; i++)
                    {
                        FilterColumn column = (FilterColumn)this.columnsPrivateCopy[i].Clone();
                        //column.ContextMenuStrip = this.columnsPrivateCopy[i].ContextMenuStrip;
                        columnArray2[i] = column;
                        flagArray[i] = (bool)this.userAddedColumns[this.columnsPrivateCopy[i]];
                        strArray[i] = (string)this.columnsNames[this.columnsPrivateCopy[i]];
                    }
                    if (container != null)
                    {
                        for (int n = 0; n < columnArray2.Length; n++)
                        {
                            if (strArray[n].IsNotEmpty() && ValidateName(container, strArray[n], columnArray2[n]))
                            {
                                container.Add(columnArray2[n], strArray[n]);
                            }
                            else
                            {
                                container.Add(columnArray2[n]);
                            }
                        }
                    }
                    service.OnComponentChanging(this.liveFilterControl, member);
                    for (int j = 0; j < columnArray2.Length; j++)
                    {
                        PropertyDescriptor descriptor2 = TypeDescriptor.GetProperties(columnArray2[j])["DisplayIndex"];
                        if (descriptor2 != null)
                        {
                            descriptor2.SetValue(columnArray2[j], -1);
                        }
                        this.liveFilterControl.Columns.Add(columnArray2[j]);
                    }
                    service.OnComponentChanged(this.liveFilterControl, member, null, null);
                    for (int k = 0; k < flagArray.Length; k++)
                    {
                        PropertyDescriptor descriptor3 = TypeDescriptor.GetProperties(columnArray2[k])["UserAddedColumn"];
                        if (descriptor3 != null)
                        {
                            descriptor3.SetValue(columnArray2[k], flagArray[k]);
                        }
                    }
                }
                catch (InvalidOperationException exception)
                {
                    IUIService uiService = (IUIService)this.liveFilterControl.Site.GetService(typeof(IUIService));
                    FilterControlDesigner.ShowErrorDialog(uiService, exception, this.liveFilterControl);
                    base.DialogResult = DialogResult.Cancel;
                }
            }
        }

        private void componentChanged(object sender, ComponentChangedEventArgs e)
        {
            if ((e.Component is ListBoxItem) && this.selectedColumns.Items.Contains(e.Component))
            {
                this.formIsDirty = true;
            }
        }

        private static void CopyFilterColumnProperties(FilterColumn srcColumn, FilterColumn destColumn)
        {
            destColumn.DataPropertyName = srcColumn.DataPropertyName;
            destColumn.HeaderText = srcColumn.HeaderText;
            destColumn.Name = srcColumn.Name;
            destColumn.Tag = srcColumn.Tag;
            destColumn.ToolTipText = srcColumn.ToolTipText;
            destColumn.Width = srcColumn.Width;
            destColumn.Visible = srcColumn.Visible;
        }

        private void FilterColumnCollectionDialog_Closed(object sender, EventArgs e)
        {
            for (int i = 0; i < this.selectedColumns.Items.Count; i++)
            {
                ListBoxItem item = this.selectedColumns.Items[i] as ListBoxItem;
                if (item.FilterColumnDesigner != null)
                {
                    TypeDescriptor.RemoveAssociation(item.FilterColumn, item.FilterColumnDesigner);
                }
            }
            this.columnsNames = null;
            this.userAddedColumns = null;
        }

        private void FilterColumnCollectionDialog_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.FilterColumnCollectionDialog_HelpRequestHandled();
        }

        private void FilterColumnCollectionDialog_HelpRequested(object sender, HelpEventArgs e)
        {
            this.FilterColumnCollectionDialog_HelpRequestHandled();
            e.Handled = true;
        }

        private void FilterColumnCollectionDialog_HelpRequestHandled()
        {
            IHelpService service = this.liveFilterControl.Site.GetService(iHelpServiceType) as IHelpService;
            if (service != null)
            {
                service.ShowHelpFromKeyword("vs.FilterColumnCollectionDialog");
            }
        }

        private void FilterColumnCollectionDialog_Load(object sender, EventArgs e)
        {
            Font defaultFont = Control.DefaultFont;
            IUIService service = (IUIService)this.liveFilterControl.Site.GetService(iUIServiceType);
            if (service != null)
            {
                defaultFont = (Font)service.Styles["DialogFont"];
            }
            this.Font = defaultFont;
            this.selectedColumns.SelectedIndex = Math.Min(0, this.selectedColumns.Items.Count - 1);
            this.moveUp.Enabled = this.selectedColumns.SelectedIndex > 0;
            this.moveDown.Enabled = this.selectedColumns.SelectedIndex < (this.selectedColumns.Items.Count - 1);
            this.deleteButton.Enabled = (this.selectedColumns.Items.Count > 0) && (this.selectedColumns.SelectedIndex != -1);
            this.propertyGrid1.SelectedObject = this.selectedColumns.SelectedItem;
            this.selectedColumns.ItemHeight = this.Font.Height + 4;
            base.ActiveControl = this.selectedColumns;
            this.SetSelectedColumnsHorizontalExtent();
            this.selectedColumns.Focus();
            this.formIsDirty = false;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.selectedColumns.SelectedIndex;
            this.columnsNames.Remove(this.columnsPrivateCopy[selectedIndex]);
            this.userAddedColumns.Remove(this.columnsPrivateCopy[selectedIndex]);
            this.columnsPrivateCopy.RemoveAt(selectedIndex);
            this.selectedColumns.SelectedIndex = Math.Min(this.selectedColumns.Items.Count - 1, selectedIndex);
            this.moveUp.Enabled = this.selectedColumns.SelectedIndex > 0;
            this.moveDown.Enabled = this.selectedColumns.SelectedIndex < (this.selectedColumns.Items.Count - 1);
            this.deleteButton.Enabled = (this.selectedColumns.Items.Count > 0) && (this.selectedColumns.SelectedIndex != -1);
            this.propertyGrid1.SelectedObject = this.selectedColumns.SelectedItem;
        }

        private void HookComponentChangedEventHandler(IComponentChangeService componentChangeService)
        {
            if (componentChangeService != null)
            {
                componentChangeService.ComponentChanged += new ComponentChangedEventHandler(this.componentChanged);
            }
        }

        internal class VsPropertyGrid : PropertyGrid
        {
            public VsPropertyGrid(IServiceProvider serviceProvider)
            {
                if (serviceProvider != null)
                {
                    IUIService service = serviceProvider.GetService(typeof(IUIService)) as IUIService;
                    if (service != null)
                    {
                        base.ToolStripRenderer = (ToolStripProfessionalRenderer)service.Styles["VsToolWindowRenderer"];
                    }
                }
            }
        }

        private static bool IsColumnAddedByUser(FilterColumn col)
        {
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(col)["UserAddedColumn"];
            return ((descriptor != null) && ((bool)descriptor.GetValue(col)));
        }

        private void moveDown_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.selectedColumns.SelectedIndex;
            this.columnCollectionChanging = true;
            try
            {
                ListBoxItem selectedItem = (ListBoxItem)this.selectedColumns.SelectedItem;
                this.selectedColumns.Items.RemoveAt(selectedIndex);
                this.selectedColumns.Items.Insert(selectedIndex + 1, selectedItem);
                this.columnsPrivateCopy.RemoveAt(selectedIndex);
                this.columnsPrivateCopy.Insert(selectedIndex + 1, selectedItem.FilterColumn);
            }
            finally
            {
                this.columnCollectionChanging = false;
            }
            this.formIsDirty = true;
            this.selectedColumns.SelectedIndex = selectedIndex + 1;
            this.moveUp.Enabled = this.selectedColumns.SelectedIndex > 0;
            this.moveDown.Enabled = this.selectedColumns.SelectedIndex < (this.selectedColumns.Items.Count - 1);
        }

        private void moveUp_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.selectedColumns.SelectedIndex;
            this.columnCollectionChanging = true;
            try
            {
                ListBoxItem item = (ListBoxItem)this.selectedColumns.Items[selectedIndex - 1];
                this.selectedColumns.Items.RemoveAt(selectedIndex - 1);
                this.selectedColumns.Items.Insert(selectedIndex, item);
                this.columnsPrivateCopy.RemoveAt(selectedIndex - 1);
                this.columnsPrivateCopy.Insert(selectedIndex, item.FilterColumn);
            }
            finally
            {
                this.columnCollectionChanging = false;
            }
            this.formIsDirty = true;
            this.selectedColumns.SelectedIndex = selectedIndex - 1;
            this.moveUp.Enabled = this.selectedColumns.SelectedIndex > 0;
            this.moveDown.Enabled = this.selectedColumns.SelectedIndex < (this.selectedColumns.Items.Count - 1);
            if ((this.selectedColumns.SelectedIndex != -1) && (this.selectedColumns.TopIndex > this.selectedColumns.SelectedIndex))
            {
                this.selectedColumns.TopIndex = this.selectedColumns.SelectedIndex;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.CommitChanges();
        }

        private void PopulateSelectedColumns()
        {
            int selectedIndex = this.selectedColumns.SelectedIndex;
            for (int i = 0; i < this.selectedColumns.Items.Count; i++)
            {
                ListBoxItem item = this.selectedColumns.Items[i] as ListBoxItem;
                if (item.FilterColumnDesigner != null)
                {
                    TypeDescriptor.RemoveAssociation(item.FilterColumn, item.FilterColumnDesigner);
                }
            }
            this.selectedColumns.Items.Clear();
            ITypeResolutionService tr = (ITypeResolutionService)this.liveFilterControl.Site.GetService(iTypeResolutionServiceType);
            for (int j = 0; j < this.columnsPrivateCopy.Count; j++)
            {
                ComponentDesigner componentDesignerForType = FilterAddColumnDialog.GetComponentDesignerForType(tr, this.columnsPrivateCopy[j].GetType());
                this.selectedColumns.Items.Add(new ListBoxItem(this.columnsPrivateCopy[j], this, componentDesignerForType));
            }
            this.selectedColumns.SelectedIndex = Math.Min(selectedIndex, this.selectedColumns.Items.Count - 1);
            this.SetSelectedColumnsHorizontalExtent();
            if (this.selectedColumns.Items.Count == 0)
            {
                this.propertyGridLabel.Text = "FilterControlProperties";
            }
        }

        private void propertyGrid1_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            if (!this.columnCollectionChanging)
            {
                this.formIsDirty = true;
                if (e.ChangedItem.PropertyDescriptor.Name.Equals("HeaderText"))
                {
                    int selectedIndex = this.selectedColumns.SelectedIndex;
                    Rectangle rc = new Rectangle(0, selectedIndex * this.selectedColumns.ItemHeight, this.selectedColumns.Width, this.selectedColumns.ItemHeight);
                    this.columnCollectionChanging = true;
                    try
                    {
                        this.selectedColumns.Items[selectedIndex] = this.selectedColumns.Items[selectedIndex];
                    }
                    finally
                    {
                        this.columnCollectionChanging = false;
                    }
                    this.selectedColumns.Invalidate(rc);
                    this.SetSelectedColumnsHorizontalExtent();
                }
                else if (e.ChangedItem.PropertyDescriptor.Name.Equals("DataPropertyName"))
                {
                    if (string.IsNullOrEmpty(((ListBoxItem)this.selectedColumns.SelectedItem).FilterColumn.DataPropertyName))
                    {
                        this.propertyGridLabel.Text = "UnboundColumnProperties";
                    }
                    else
                    {
                        this.propertyGridLabel.Text = "BoundColumnProperties";
                    }
                }
                else if (e.ChangedItem.PropertyDescriptor.Name.Equals("Name"))
                {
                    FilterColumn dataGridViewColumn = ((ListBoxItem)this.selectedColumns.SelectedItem).FilterColumn;
                    this.columnsNames[dataGridViewColumn] = dataGridViewColumn.Name;
                }
            }
        }

        private void selectedColumns_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                ListBoxItem item = this.selectedColumns.Items[e.Index] as ListBoxItem;
                e.Graphics.DrawImage(item.ToolboxBitmap, e.Bounds.X + 2, e.Bounds.Y + 2, item.ToolboxBitmap.Width, item.ToolboxBitmap.Height);
                Rectangle bounds = e.Bounds;
                bounds.Width -= item.ToolboxBitmap.Width + 4;
                bounds.X += item.ToolboxBitmap.Width + 4;
                bounds.Y += 2;
                bounds.Height -= 4;
                Brush brush = new SolidBrush(e.BackColor);
                Brush brush2 = new SolidBrush(e.ForeColor);
                Brush brush3 = new SolidBrush(this.selectedColumns.BackColor);
                string text = ((ListBoxItem)this.selectedColumns.Items[e.Index]).ToString();
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    int width = Size.Ceiling(e.Graphics.MeasureString(text, e.Font, new SizeF((float)bounds.Width, (float)bounds.Height))).Width;
                    Rectangle rect = new Rectangle(bounds.X, e.Bounds.Y + 1, width + 3, e.Bounds.Height - 2);
                    e.Graphics.FillRectangle(brush, rect);
                    rect.Inflate(-1, -1);
                    e.Graphics.DrawString(text, e.Font, brush2, rect);
                    rect.Inflate(1, 1);
                    if (this.selectedColumns.Focused)
                    {
                        ControlPaint.DrawFocusRectangle(e.Graphics, rect, e.ForeColor, e.BackColor);
                    }
                    e.Graphics.FillRectangle(brush3, new Rectangle(rect.Right + 1, e.Bounds.Y, (e.Bounds.Width - rect.Right) - 1, e.Bounds.Height));
                }
                else
                {
                    e.Graphics.FillRectangle(brush3, new Rectangle(bounds.X, e.Bounds.Y, e.Bounds.Width - bounds.X, e.Bounds.Height));
                    e.Graphics.DrawString(text, e.Font, brush2, bounds);
                }
                brush.Dispose();
                brush3.Dispose();
                brush2.Dispose();
            }
        }

        private void selectedColumns_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) != Keys.None)
            {
                e.Handled = true;
            }
        }

        private void selectedColumns_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Modifiers == Keys.None) && (e.KeyCode == Keys.F4))
            {
                this.propertyGrid1.Focus();
                e.Handled = true;
            }
        }

        private void selectedColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.columnCollectionChanging)
            {
                this.propertyGrid1.SelectedObject = this.selectedColumns.SelectedItem;
                this.moveDown.Enabled = (this.selectedColumns.Items.Count > 0) && (this.selectedColumns.SelectedIndex != (this.selectedColumns.Items.Count - 1));
                this.moveUp.Enabled = (this.selectedColumns.Items.Count > 0) && (this.selectedColumns.SelectedIndex > 0);
                this.deleteButton.Enabled = (this.selectedColumns.Items.Count > 0) && (this.selectedColumns.SelectedIndex != -1);
                if (this.selectedColumns.SelectedItem != null)
                {
                    if (string.IsNullOrEmpty(((ListBoxItem)this.selectedColumns.SelectedItem).FilterColumn.DataPropertyName))
                    {
                        this.propertyGridLabel.Text = "UnboundColumnProperties";
                    }
                    else
                    {
                        this.propertyGridLabel.Text = "BoundColumnProperties";
                    }
                }
                else
                {
                    this.propertyGridLabel.Text = "Properties";
                }
            }
        }

        internal void SetLiveFilterControl(FilterControl filter)
        {
            IComponentChangeService service = null;
            if (filter.Site != null)
            {
                service = (IComponentChangeService)filter.Site.GetService(iComponentChangeServiceType);
            }
            if (service != this.compChangeService)
            {
                this.UnhookComponentChangedEventHandler(this.compChangeService);
                this.compChangeService = service;
                this.HookComponentChangedEventHandler(this.compChangeService);
            }
            this.liveFilterControl = filter;
            this.filterPrivateCopy.Site = filter.Site;
            this.filterPrivateCopy.DataSource = filter.DataSource;
            this.filterPrivateCopy.DataMember = filter.DataMember;
            this.columnsNames = new Hashtable(this.columnsPrivateCopy.Count);
            this.columnsPrivateCopy.Clear();
            this.userAddedColumns = new Hashtable(this.liveFilterControl.Columns.Count);
            this.columnCollectionChanging = true;
            try
            {
                for (int i = 0; i < this.liveFilterControl.Columns.Count; i++)
                {
                    FilterColumn column = this.liveFilterControl.Columns[i];
                    FilterColumn dataGridViewColumn = (FilterColumn)column.Clone();
                    this.columnsPrivateCopy.Add(dataGridViewColumn);
                    if (column.Site != null)
                    {
                        this.columnsNames[dataGridViewColumn] = column.Site.Name;
                    }
                    this.userAddedColumns[dataGridViewColumn] = IsColumnAddedByUser(this.liveFilterControl.Columns[i]);
                }
            }
            finally
            {
                this.columnCollectionChanging = false;
            }
            this.PopulateSelectedColumns();
            this.propertyGrid1.Site = new FilterControlComponentPropertyGridSite(this.liveFilterControl.Site, this.liveFilterControl);
            this.propertyGrid1.SelectedObject = this.selectedColumns.SelectedItem;
        }

        internal class FilterControlComponentPropertyGridSite : ISite, IServiceProvider
        {
            private IComponent comp;
            private bool inGetService;
            private IServiceProvider sp;

            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            public FilterControlComponentPropertyGridSite(IServiceProvider sp, IComponent comp)
            {
                this.sp = sp;
                this.comp = comp;
            }

            public object GetService(Type t)
            {
                if (!this.inGetService && (this.sp != null))
                {
                    try
                    {
                        this.inGetService = true;
                        return this.sp.GetService(t);
                    }
                    finally
                    {
                        this.inGetService = false;
                    }
                }
                return null;
            }

            public IComponent Component
            {
                [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
                get
                {
                    return this.comp;
                }
            }

            public IContainer Container
            {
                get
                {
                    return null;
                }
            }

            public bool DesignMode
            {
                get
                {
                    return false;
                }
            }

            public string Name
            {
                get
                {
                    return null;
                }
                set
                {
                }
            }
        }

        private void SetSelectedColumnsHorizontalExtent()
        {
            int num = 0;
            for (int i = 0; i < this.selectedColumns.Items.Count; i++)
            {
                int width = TextRenderer.MeasureText(this.selectedColumns.Items[i].ToString(), this.selectedColumns.Font).Width;
                num = Math.Max(num, width);
            }
            this.selectedColumns.HorizontalExtent = ((this.SelectedColumnsItemBitmap.Width + 4) + num) + 3;
        }

        private void UnhookComponentChangedEventHandler(IComponentChangeService componentChangeService)
        {
            if (componentChangeService != null)
            {
                componentChangeService.ComponentChanged -= new ComponentChangedEventHandler(this.componentChanged);
            }
        }

        private static bool ValidateName(IContainer container, string siteName, IComponent component)
        {
            ComponentCollection components = container.Components;
            if (components != null)
            {
                for (int i = 0; i < components.Count; i++)
                {
                    IComponent component2 = components[i];
                    if ((component2 != null) && (component2.Site != null))
                    {
                        ISite site = component2.Site;
                        if (((site != null) && (site.Name != null)) && (string.Equals(site.Name, siteName, StringComparison.OrdinalIgnoreCase) && (site.Component != component)))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private Bitmap SelectedColumnsItemBitmap
        {
            get
            {
                if (selectedColumnsItemBitmap == null)
                {
                    selectedColumnsItemBitmap = new Bitmap(typeof(FilterColumnCollectionDialog), "Resources.FilterColumnsDialog.selectedColumns.bmp");
                    selectedColumnsItemBitmap.MakeTransparent(Color.Red);
                }
                return selectedColumnsItemBitmap;
            }
        }

        private class ColumnTypePropertyDescriptor : PropertyDescriptor
        {
            public ColumnTypePropertyDescriptor()
                : base("ColumnType", null)
            {
            }

            public override bool CanResetValue(object component)
            {
                return false;
            }

            public override object GetValue(object component)
            {
                if (component == null)
                {
                    return null;
                }
                FilterColumnCollectionDialog.ListBoxItem item = (FilterColumnCollectionDialog.ListBoxItem)component;
                return item.FilterColumn.GetType().Name;
            }

            public override void ResetValue(object component)
            {
            }

            public override void SetValue(object component, object value)
            {
                FilterColumnCollectionDialog.ListBoxItem item = (FilterColumnCollectionDialog.ListBoxItem)component;
                System.Type newType = value as System.Type;
                if (item.FilterColumn.GetType() != newType)
                {
                    item.Owner.ColumnTypeChanged(item, newType);
                    this.OnValueChanged(component, EventArgs.Empty);
                }
            }

            public override bool ShouldSerializeValue(object component)
            {
                return false;
            }

            public override AttributeCollection Attributes
            {
                get
                {
                    EditorAttribute attribute = new EditorAttribute(typeof(FilterColumnTypeEditor), typeof(UITypeEditor));
                    DescriptionAttribute attribute2 = new DescriptionAttribute("ColumnTypePropertyDescription");
                    CategoryAttribute design = CategoryAttribute.Design;
                    return new AttributeCollection(new Attribute[] { attribute, attribute2, design });
                }
            }

            public override System.Type ComponentType
            {
                get
                {
                    return typeof(FilterColumnCollectionDialog.ListBoxItem);
                }
            }

            public override bool IsReadOnly
            {
                get
                {
                    return false;
                }
            }

            public override System.Type PropertyType
            {
                get
                {
                    return typeof(System.Type);
                }
            }
        }

        internal class ListBoxItem : ICustomTypeDescriptor, IComponent, IDisposable
        {
            private FilterColumn column;
            private ComponentDesigner compDesigner;
            private FilterColumnCollectionDialog owner;
            private Image toolboxBitmap;

            event EventHandler IComponent.Disposed
            {
                add
                {
                }
                remove
                {
                }
            }

            public ListBoxItem(FilterColumn column, FilterColumnCollectionDialog owner, ComponentDesigner compDesigner)
            {
                this.column = column;
                this.owner = owner;
                this.compDesigner = compDesigner;
                if (this.compDesigner != null)
                {
                    this.compDesigner.Initialize(column);
                    TypeDescriptor.CreateAssociation(this.column, this.compDesigner);
                }
                ToolboxBitmapAttribute attribute = TypeDescriptor.GetAttributes(column)[FilterColumnCollectionDialog.toolboxBitmapAttributeType] as ToolboxBitmapAttribute;
                if (attribute != null)
                {
                    this.toolboxBitmap = attribute.GetImage(column, false);
                }
                else
                {
                    this.toolboxBitmap = this.owner.SelectedColumnsItemBitmap;
                }
                FilterColumnDesigner designer = compDesigner as FilterColumnDesigner;
                if (designer != null)
                {
                    designer.LiveFilterControl = this.owner.liveFilterControl;
                }
            }

            AttributeCollection ICustomTypeDescriptor.GetAttributes()
            {
                return TypeDescriptor.GetAttributes(this.column);
            }

            string ICustomTypeDescriptor.GetClassName()
            {
                return TypeDescriptor.GetClassName(this.column);
            }

            string ICustomTypeDescriptor.GetComponentName()
            {
                return TypeDescriptor.GetComponentName(this.column);
            }

            TypeConverter ICustomTypeDescriptor.GetConverter()
            {
                return TypeDescriptor.GetConverter(this.column);
            }

            EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
            {
                return TypeDescriptor.GetDefaultEvent(this.column);
            }

            PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
            {
                return TypeDescriptor.GetDefaultProperty(this.column);
            }

            object ICustomTypeDescriptor.GetEditor(System.Type type)
            {
                return TypeDescriptor.GetEditor(this.column, type);
            }

            EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
            {
                return TypeDescriptor.GetEvents(this.column);
            }

            EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attrs)
            {
                return TypeDescriptor.GetEvents(this.column, attrs);
            }

            PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
            {
                return ((ICustomTypeDescriptor)this).GetProperties(null);
            }

            PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attrs)
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.column);
                PropertyDescriptor[] array = null;
                if (this.compDesigner != null)
                {
                    Hashtable hashtable = new Hashtable();
                    for (int i = 0; i < properties.Count; i++)
                    {
                        hashtable.Add(properties[i].Name, properties[i]);
                    }
                    ((IDesignerFilter)this.compDesigner).PreFilterProperties(hashtable);
                    array = new PropertyDescriptor[hashtable.Count + 1];
                    hashtable.Values.CopyTo(array, 0);
                }
                else
                {
                    array = new PropertyDescriptor[properties.Count + 1];
                    properties.CopyTo(array, 0);
                }
                array[array.Length - 1] = new FilterColumnCollectionDialog.ColumnTypePropertyDescriptor();
                return new PropertyDescriptorCollection(array);
            }

            object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
            {
                if ((pd != null) && (pd is FilterColumnCollectionDialog.ColumnTypePropertyDescriptor))
                {
                    return this;
                }
                return this.column;
            }

            void IDisposable.Dispose()
            {
            }

            public override string ToString()
            {
                return this.column.HeaderText;
            }

            public FilterColumn FilterColumn
            {
                [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
                get
                {
                    return this.column;
                }
            }

            public ComponentDesigner FilterColumnDesigner
            {
                [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
                get
                {
                    return this.compDesigner;
                }
            }

            public FilterColumnCollectionDialog Owner
            {
                [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
                get
                {
                    return this.owner;
                }
            }

            ISite IComponent.Site
            {
                get
                {
                    return this.owner.liveFilterControl.Site;
                }
                set
                {
                }
            }

            public Image ToolboxBitmap
            {
                [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
                get
                {
                    return this.toolboxBitmap;
                }
            }
        }
    }
}
