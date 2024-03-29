﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Globalization;
using System.Collections;

namespace EAP.Win.UI.Design
{
    public class FilterAddColumnDialog : Form
    {
        private Button addButton;
        private Button cancelButton;
        private TableLayoutPanel checkBoxesTableLayoutPanel;
        private Label columnInDataSourceLabel;
        private static System.Type[] columnTypes = new System.Type[] { typeof(FilterCheckBoxColumn), typeof(FilterComboBoxColumn), typeof(FilterTextBoxColumn), typeof(FilterListBoxColumn), typeof(FilterRichTextBoxColumn), typeof(FilterCheckListBoxColumn) };
        private ComboBox columnTypesCombo;
        private IContainer components = null;
        private RadioButton dataBoundColumnRadioButton;
        private ListBox dataColumns;
        private static System.Type filterColumnDesignTimeVisibleAttributeType = typeof(FilterColumnDesignTimeVisibleAttribute);
        private FilterColumnCollection filterColumns;
        private static System.Type filterColumnType = typeof(FilterColumn);
        private TextBox headerTextBox;
        private Label headerTextLabel;
        private static System.Type iComponentChangeServiceType = typeof(IComponentChangeService);
        private static System.Type iDesignerHostType = typeof(IDesignerHost);
        private static System.Type iDesignerType = typeof(IDesigner);
        private static System.Type iHelpServiceType = typeof(IHelpService);
        private static System.Type iNameCreationServiceType = typeof(INameCreationService);
        private int initialFilterColumnsCount = -1;
        private int insertAtPosition = -1;
        private static System.Type iTypeDiscoveryServiceType = typeof(ITypeDiscoveryService);
        private static System.Type iTypeResolutionServiceType = typeof(ITypeResolutionService);
        private static System.Type iUIServiceType = typeof(IUIService);
        private FilterControl liveFilterControl;
        private Label nameLabel;
        private TextBox nameTextBox;
        private TableLayoutPanel okCancelTableLayoutPanel;
        private TableLayoutPanel overarchingTableLayoutPanel;
        private bool persistChangesToDesigner;
        private Label typeLabel;
        private RadioButton unboundColumnRadioButton;
        private CheckBox visibleCheckBox;

        public FilterAddColumnDialog(FilterColumnCollection columns, FilterControl liveFilterControl)
        {
            this.filterColumns = columns;
            this.liveFilterControl = liveFilterControl;
            Font defaultFont = Control.DefaultFont;
            IUIService service = (IUIService)this.liveFilterControl.Site.GetService(iUIServiceType);
            if (service != null)
            {
                defaultFont = (Font)service.Styles["DialogFont"];
            }
            this.Font = defaultFont;
            this.InitializeComponent();
            this.EnableDataBoundSection();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.cancelButton.Text = "Close";
                this.AddColumn();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void AddColumn()
        {
            FilterColumn column = Activator.CreateInstance(((ComboBoxItem)this.columnTypesCombo.SelectedItem).ColumnType) as FilterColumn;
            if (!this.persistChangesToDesigner)
            {
                column.HeaderText = this.headerTextBox.Text;
                column.Name = this.nameTextBox.Text;
                this.filterColumns.Insert(this.insertAtPosition, column);
                this.insertAtPosition++;
            }
            column.HeaderText = this.headerTextBox.Text;
            column.Name = this.nameTextBox.Text;
            column.Visible = this.visibleCheckBox.Checked;
            if (this.dataBoundColumnRadioButton.Checked && (this.dataColumns.SelectedIndex > -1))
            {
                column.DataPropertyName = ((ListBoxItem)this.dataColumns.SelectedItem).PropertyName;
            }
            if (this.persistChangesToDesigner)
            {
                try
                {
                    this.filterColumns.Insert(this.insertAtPosition, column);
                    this.insertAtPosition++;
                    this.liveFilterControl.Site.Container.Add(column, column.Name);
                }
                catch (InvalidOperationException exception)
                {
                    IUIService uiService = (IUIService)this.liveFilterControl.Site.GetService(typeof(IUIService));
                    FilterControlDesigner.ShowErrorDialog(uiService, exception, this.liveFilterControl);
                    return;
                }
            }
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(column)["UserAddedColumn"];
            if (descriptor != null)
            {
                descriptor.SetValue(column, true);
            } 
            this.headerTextBox.Text = this.AssignName();
            this.nameTextBox.Text = "col" + this.headerTextBox.Text;
            this.nameTextBox.Focus();
        }

        private string AssignName()
        {
            int num = 1;
            string name = "Column" + num.ToString(CultureInfo.InvariantCulture);
            IDesignerHost service = null;
            IContainer container = null;
            service = this.liveFilterControl.Site.GetService(iDesignerHostType) as IDesignerHost;
            if (service != null)
            {
                container = service.Container;
            }
            while (!ValidName(name, this.filterColumns, container, null, this.liveFilterControl.Columns, !this.persistChangesToDesigner))
            {
                num++;
                name = "Column" + num.ToString(CultureInfo.InvariantCulture);
            }
            return name;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void dataBoundColumnRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.columnInDataSourceLabel.Enabled = this.dataBoundColumnRadioButton.Checked;
            this.dataColumns.Enabled = this.dataBoundColumnRadioButton.Checked;
            this.dataColumns_SelectedIndexChanged(null, EventArgs.Empty);
        }

        private void dataColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.dataColumns.SelectedItem != null)
            {
                this.headerTextBox.Text = ((ListBoxItem)this.dataColumns.SelectedItem).PropertyName;
                this.nameTextBox.Text = "col" + this.headerTextBox.Text;
                this.SetDefaultFilterColumnType(((ListBoxItem)this.dataColumns.SelectedItem).PropertyType);
            }
        }

        private void FilterAddColumnDialog_Closed(object sender, EventArgs e)
        {
            if (this.persistChangesToDesigner)
            {
                try
                {
                    IComponentChangeService service = (IComponentChangeService)this.liveFilterControl.Site.GetService(iComponentChangeServiceType);
                    if (service == null)
                    {
                        return;
                    }
                    FilterColumn[] columns = new FilterColumn[this.liveFilterControl.Columns.Count - this.initialFilterColumnsCount];
                    for (int i = this.initialFilterColumnsCount; i < this.liveFilterControl.Columns.Count; i++)
                    {
                        columns[i - this.initialFilterColumnsCount] = this.liveFilterControl.Columns[i];
                    }
                    int initialFilterColumnsCount = this.initialFilterColumnsCount;
                    while (initialFilterColumnsCount < this.liveFilterControl.Columns.Count)
                    {
                        this.liveFilterControl.Columns.RemoveAt(this.initialFilterColumnsCount);
                    }
                    PropertyDescriptor member = TypeDescriptor.GetProperties(this.liveFilterControl)["Columns"];
                    service.OnComponentChanging(this.liveFilterControl, member);
                    this.liveFilterControl.Columns.AddRange(columns);
                    service.OnComponentChanged(this.liveFilterControl, member, null, null);
                }
                catch (InvalidOperationException exc)
                {
                    MessageBox.Show(exc.ToString());
                }
            }
            base.DialogResult = DialogResult.OK;
        }

        private void FilterAddColumnDialog_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            this.FilterAddColumnDialog_HelpRequestHandled();
            e.Cancel = true;
        }

        private void FilterAddColumnDialog_HelpRequested(object sender, HelpEventArgs e)
        {
            this.FilterAddColumnDialog_HelpRequestHandled();
            e.Handled = true;
        }

        private void FilterAddColumnDialog_HelpRequestHandled()
        {
            IHelpService service = this.liveFilterControl.Site.GetService(iHelpServiceType) as IHelpService;
            if (service != null)
            {
                service.ShowHelpFromKeyword("vs.FilterAddColumnDialog");
            }
        }

        private void FilterAddColumnDialog_Load(object sender, EventArgs e)
        {
            if (this.dataBoundColumnRadioButton.Checked)
            {
                this.headerTextBox.Text = this.nameTextBox.Text = this.AssignName();
            }
            else
            {
                string str = this.AssignName();
                this.headerTextBox.Text = this.nameTextBox.Text = str;
            }
            this.PopulateColumnTypesCombo();
            this.PopulateDataColumns();
            this.EnableDataBoundSection();
            this.cancelButton.Text = "Cancel";
        }

        private void FilterAddColumnDialog_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible && base.IsHandleCreated)
            {
                if (this.dataBoundColumnRadioButton.Checked)
                {
                    this.dataColumns.Select();
                }
                else
                {
                    this.nameTextBox.Select();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void EnableDataBoundSection()
        {
            if (this.dataColumns.Items.Count > 0)
            {
                this.dataBoundColumnRadioButton.Enabled = true;
                this.dataBoundColumnRadioButton.Checked = true;
                this.dataBoundColumnRadioButton.Focus();
                this.headerTextBox.Text = this.nameTextBox.Text = this.AssignName();
            }
            else
            {
                this.dataBoundColumnRadioButton.Enabled = false;
                this.unboundColumnRadioButton.Checked = true;
                this.nameTextBox.Focus();
                this.headerTextBox.Text = this.nameTextBox.Text = this.AssignName();
            }
        }

        public static ComponentDesigner GetComponentDesignerForType(ITypeResolutionService tr, System.Type type)
        {
            ComponentDesigner designer = null;
            DesignerAttribute attribute = null;
            AttributeCollection attributes = TypeDescriptor.GetAttributes(type);
            for (int i = 0; i < attributes.Count; i++)
            {
                DesignerAttribute attribute2 = attributes[i] as DesignerAttribute;
                if (attribute2 != null)
                {
                    System.Type type2 = System.Type.GetType(attribute2.DesignerBaseTypeName);
                    if ((type2 != null) && (type2 == iDesignerType))
                    {
                        attribute = attribute2;
                        break;
                    }
                }
            }
            if (attribute != null)
            {
                System.Type c = null;
                if (tr != null)
                {
                    c = tr.GetType(attribute.DesignerTypeName);
                }
                else
                {
                    c = System.Type.GetType(attribute.DesignerTypeName);
                }
                if ((c != null) && typeof(ComponentDesigner).IsAssignableFrom(c))
                {
                    designer = (ComponentDesigner)Activator.CreateInstance(c);
                }
            }
            return designer;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FilterAddColumnDialog));
            this.dataBoundColumnRadioButton = new RadioButton();
            this.overarchingTableLayoutPanel = new TableLayoutPanel();
            this.checkBoxesTableLayoutPanel = new TableLayoutPanel();
            this.visibleCheckBox = new CheckBox();
            this.okCancelTableLayoutPanel = new TableLayoutPanel();
            this.addButton = new Button();
            this.cancelButton = new Button();
            this.columnInDataSourceLabel = new Label();
            this.dataColumns = new ListBox();
            this.unboundColumnRadioButton = new RadioButton();
            this.nameLabel = new Label();
            this.nameTextBox = new TextBox();
            this.typeLabel = new Label();
            this.columnTypesCombo = new ComboBox();
            this.headerTextLabel = new Label();
            this.headerTextBox = new TextBox();
            this.overarchingTableLayoutPanel.SuspendLayout();
            this.checkBoxesTableLayoutPanel.SuspendLayout();
            this.okCancelTableLayoutPanel.SuspendLayout();
            base.SuspendLayout();
            manager.ApplyResources(this.dataBoundColumnRadioButton, "dataBoundColumnRadioButton");
            this.dataBoundColumnRadioButton.Checked = true;
            this.overarchingTableLayoutPanel.SetColumnSpan(this.dataBoundColumnRadioButton, 2);
            this.dataBoundColumnRadioButton.Margin = new Padding(0, 0, 0, 3);
            this.dataBoundColumnRadioButton.Name = "dataBoundColumnRadioButton";
            this.dataBoundColumnRadioButton.CheckedChanged += new EventHandler(this.dataBoundColumnRadioButton_CheckedChanged);
            manager.ApplyResources(this.overarchingTableLayoutPanel, "overarchingTableLayoutPanel");
            this.overarchingTableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.overarchingTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            this.overarchingTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250f));
            this.overarchingTableLayoutPanel.Controls.Add(this.checkBoxesTableLayoutPanel, 0, 10);
            this.overarchingTableLayoutPanel.Controls.Add(this.okCancelTableLayoutPanel, 1, 11);
            this.overarchingTableLayoutPanel.Controls.Add(this.dataBoundColumnRadioButton, 0, 0);
            this.overarchingTableLayoutPanel.Controls.Add(this.columnInDataSourceLabel, 0, 1);
            this.overarchingTableLayoutPanel.Controls.Add(this.dataColumns, 0, 2);
            this.overarchingTableLayoutPanel.Controls.Add(this.unboundColumnRadioButton, 0, 3);
            this.overarchingTableLayoutPanel.Controls.Add(this.nameLabel, 0, 4);
            this.overarchingTableLayoutPanel.Controls.Add(this.nameTextBox, 1, 4);
            this.overarchingTableLayoutPanel.Controls.Add(this.typeLabel, 0, 6);
            this.overarchingTableLayoutPanel.Controls.Add(this.columnTypesCombo, 1, 6);
            this.overarchingTableLayoutPanel.Controls.Add(this.headerTextLabel, 0, 8);
            this.overarchingTableLayoutPanel.Controls.Add(this.headerTextBox, 1, 8);
            this.overarchingTableLayoutPanel.Margin = new Padding(12);
            this.overarchingTableLayoutPanel.Name = "overarchingTableLayoutPanel";
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            manager.ApplyResources(this.checkBoxesTableLayoutPanel, "checkBoxesTableLayoutPanel");
            this.checkBoxesTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            this.checkBoxesTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            this.checkBoxesTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            this.checkBoxesTableLayoutPanel.Controls.Add(this.visibleCheckBox, 0, 0);
            this.checkBoxesTableLayoutPanel.Margin = new Padding(0, 3, 0, 6);
            this.overarchingTableLayoutPanel.SetColumnSpan(this.checkBoxesTableLayoutPanel, 2);
            this.checkBoxesTableLayoutPanel.Name = "checkBoxesTableLayoutPanel";
            this.checkBoxesTableLayoutPanel.RowStyles.Add(new RowStyle());
            manager.ApplyResources(this.visibleCheckBox, "visibleCheckBox");
            this.visibleCheckBox.Checked = true;
            this.visibleCheckBox.CheckState = CheckState.Checked;
            this.visibleCheckBox.Margin = new Padding(0, 0, 3, 0);
            this.visibleCheckBox.Name = "visibleCheckBox";
            manager.ApplyResources(this.okCancelTableLayoutPanel, "okCancelTableLayoutPanel");
            this.okCancelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.okCancelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.okCancelTableLayoutPanel.Controls.Add(this.addButton, 0, 0);
            this.okCancelTableLayoutPanel.Controls.Add(this.cancelButton, 1, 0);
            this.okCancelTableLayoutPanel.Margin = new Padding(0, 6, 0, 0);
            this.okCancelTableLayoutPanel.Name = "okCancelTableLayoutPanel";
            this.okCancelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            manager.ApplyResources(this.addButton, "addButton");
            this.addButton.Margin = new Padding(0, 0, 3, 0);
            this.addButton.Name = "addButton";
            this.addButton.Click += new EventHandler(this.addButton_Click);
            manager.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.Margin = new Padding(3, 0, 0, 0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
            manager.ApplyResources(this.columnInDataSourceLabel, "columnInDataSourceLabel");
            this.overarchingTableLayoutPanel.SetColumnSpan(this.columnInDataSourceLabel, 2);
            this.columnInDataSourceLabel.Margin = new Padding(14, 3, 0, 0);
            this.columnInDataSourceLabel.Name = "columnInDataSourceLabel";
            manager.ApplyResources(this.dataColumns, "dataColumns");
            this.overarchingTableLayoutPanel.SetColumnSpan(this.dataColumns, 2);
            this.dataColumns.FormattingEnabled = true;
            this.dataColumns.Margin = new Padding(14, 2, 0, 3);
            this.dataColumns.Name = "dataColumns";
            this.dataColumns.SelectedIndexChanged += new EventHandler(this.dataColumns_SelectedIndexChanged);
            manager.ApplyResources(this.unboundColumnRadioButton, "unboundColumnRadioButton");
            this.overarchingTableLayoutPanel.SetColumnSpan(this.unboundColumnRadioButton, 2);
            this.unboundColumnRadioButton.Margin = new Padding(0, 6, 0, 3);
            this.unboundColumnRadioButton.Name = "unboundColumnRadioButton";
            this.unboundColumnRadioButton.CheckedChanged += new EventHandler(this.unboundColumnRadioButton_CheckedChanged);
            manager.ApplyResources(this.nameLabel, "nameLabel");
            this.nameLabel.Margin = new Padding(14, 3, 2, 3);
            this.nameLabel.Name = "nameLabel";
            manager.ApplyResources(this.nameTextBox, "nameTextBox");
            this.nameTextBox.Margin = new Padding(3, 3, 0, 3);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Validating += new CancelEventHandler(this.nameTextBox_Validating);
            manager.ApplyResources(this.typeLabel, "typeLabel");
            this.typeLabel.Margin = new Padding(14, 3, 2, 3);
            this.typeLabel.Name = "typeLabel";
            manager.ApplyResources(this.columnTypesCombo, "columnTypesCombo");
            this.columnTypesCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.columnTypesCombo.FormattingEnabled = true;
            this.columnTypesCombo.Margin = new Padding(3, 3, 0, 3);
            this.columnTypesCombo.Name = "columnTypesCombo";
            this.columnTypesCombo.Sorted = true;
            manager.ApplyResources(this.headerTextLabel, "headerTextLabel");
            this.headerTextLabel.Margin = new Padding(14, 3, 2, 3);
            this.headerTextLabel.Name = "headerTextLabel";
            manager.ApplyResources(this.headerTextBox, "headerTextBox");
            this.headerTextBox.Margin = new Padding(3, 3, 0, 3);
            this.headerTextBox.Name = "headerTextBox";
            manager.ApplyResources(this, "$this");
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.cancelButton;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.overarchingTableLayoutPanel);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.HelpButton = true;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FilterAddColumnDialog";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.HelpButtonClicked += new CancelEventHandler(this.FilterAddColumnDialog_HelpButtonClicked);
            base.Closed += new EventHandler(this.FilterAddColumnDialog_Closed);
            base.VisibleChanged += new EventHandler(this.FilterAddColumnDialog_VisibleChanged);
            base.Load += new EventHandler(this.FilterAddColumnDialog_Load);
            base.HelpRequested += new HelpEventHandler(this.FilterAddColumnDialog_HelpRequested);
            this.overarchingTableLayoutPanel.ResumeLayout(false);
            this.overarchingTableLayoutPanel.PerformLayout();
            this.checkBoxesTableLayoutPanel.ResumeLayout(false);
            this.checkBoxesTableLayoutPanel.PerformLayout();
            this.okCancelTableLayoutPanel.ResumeLayout(false);
            this.okCancelTableLayoutPanel.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void nameTextBox_Validating(object sender, CancelEventArgs e)
        {
            IDesignerHost service = null;
            INameCreationService nameCreationService = null;
            IContainer container = null;
            service = this.liveFilterControl.Site.GetService(iDesignerHostType) as IDesignerHost;
            if (service != null)
            {
                container = service.Container;
            }
            nameCreationService = this.liveFilterControl.Site.GetService(iNameCreationServiceType) as INameCreationService;
            string errorString = string.Empty;
            if (!ValidName(this.nameTextBox.Text, this.filterColumns, container, nameCreationService, this.liveFilterControl.Columns, !this.persistChangesToDesigner, out errorString))
            {
                IUIService uiService = (IUIService)this.liveFilterControl.Site.GetService(iUIServiceType);
                FilterControlDesigner.ShowErrorDialog(uiService, errorString, this.liveFilterControl);
                e.Cancel = true;
            }
        }

        private void PopulateColumnTypesCombo()
        {
            this.columnTypesCombo.Items.Clear();
            IDesignerHost host = (IDesignerHost)this.liveFilterControl.Site.GetService(iDesignerHostType);
            if (host != null)
            {
                ITypeDiscoveryService service = (ITypeDiscoveryService)host.GetService(iTypeDiscoveryServiceType);
                if (service != null)
                {
                    foreach (System.Type type in DesignerUtils.FilterGenericTypes(service.GetTypes(filterColumnType, false)))
                    {
                        if (((type != filterColumnType) && !type.IsAbstract) && (type.IsPublic || type.IsNestedPublic))
                        {
                            FilterColumnDesignTimeVisibleAttribute attribute = TypeDescriptor.GetAttributes(type)[filterColumnDesignTimeVisibleAttributeType] as FilterColumnDesignTimeVisibleAttribute;
                            if ((attribute == null) || attribute.Visible)
                            {
                                this.columnTypesCombo.Items.Add(new ComboBoxItem(type));
                            }
                        }
                    }
                    this.columnTypesCombo.SelectedIndex = this.TypeToSelectedIndex(typeof(FilterTextBoxColumn));
                }
            }
        }

        private void PopulateDataColumns()
        {
            int selectedIndex = this.dataColumns.SelectedIndex;
            this.dataColumns.SelectedIndex = -1;
            this.dataColumns.Items.Clear();
            if (this.liveFilterControl.DataSource != null)
            {
                CurrencyManager manager = null;
                try
                {
                    manager = this.BindingContext[this.liveFilterControl.DataSource, this.liveFilterControl.DataMember] as CurrencyManager;
                }
                catch (ArgumentException)
                {
                    manager = null;
                }
                PropertyDescriptorCollection descriptors = (manager != null) ? manager.GetItemProperties() : null;
                if (descriptors != null)
                {
                    for (int i = 0; i < descriptors.Count; i++)
                    {
                        if (!typeof(IList).IsAssignableFrom(descriptors[i].PropertyType) || TypeDescriptor.GetConverter(typeof(Image)).CanConvertFrom(descriptors[i].PropertyType))
                        {
                            this.dataColumns.Items.Add(new ListBoxItem(descriptors[i].PropertyType, descriptors[i].Name));
                        }
                    }
                }
            }
            if ((selectedIndex != -1) && (selectedIndex < this.dataColumns.Items.Count))
            {
                this.dataColumns.SelectedIndex = selectedIndex;
            }
            else
            {
                this.dataColumns.SelectedIndex = (this.dataColumns.Items.Count > 0) ? 0 : -1;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if ((keyData & ~Keys.KeyCode) == Keys.None)
            {
                Keys keys = keyData & Keys.KeyCode;
                if (keys == Keys.Return)
                {
                    IDesignerHost service = null;
                    INameCreationService nameCreationService = null;
                    IContainer container = null;
                    service = this.liveFilterControl.Site.GetService(iDesignerHostType) as IDesignerHost;
                    if (service != null)
                    {
                        container = service.Container;
                    }
                    nameCreationService = this.liveFilterControl.Site.GetService(iNameCreationServiceType) as INameCreationService;
                    string errorString = string.Empty;
                    if (ValidName(this.nameTextBox.Text, this.filterColumns, container, nameCreationService, this.liveFilterControl.Columns, !this.persistChangesToDesigner, out errorString))
                    {
                        this.AddColumn();
                        base.Close();
                    }
                    else
                    {
                        IUIService uiService = (IUIService)this.liveFilterControl.Site.GetService(iUIServiceType);
                        FilterControlDesigner.ShowErrorDialog(uiService, errorString, this.liveFilterControl);
                    }
                    return true;
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private void SetDefaultFilterColumnType(System.Type type)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Image));
            if ((type == typeof(bool)) || (type == typeof(CheckState)))
            {
                this.columnTypesCombo.SelectedIndex = this.TypeToSelectedIndex(typeof(FilterCheckBoxColumn));
            }
            else
            {
                this.columnTypesCombo.SelectedIndex = this.TypeToSelectedIndex(typeof(FilterTextBoxColumn));
            }
        }

        internal void Start(int insertAtPosition, bool persistChangesToDesigner)
        {
            this.insertAtPosition = insertAtPosition;
            this.persistChangesToDesigner = persistChangesToDesigner;
            if (this.persistChangesToDesigner)
            {
                this.initialFilterColumnsCount = this.liveFilterControl.Columns.Count;
            }
            else
            {
                this.initialFilterColumnsCount = -1;
            }
        }

        private int TypeToSelectedIndex(System.Type type)
        {
            for (int i = 0; i < this.columnTypesCombo.Items.Count; i++)
            {
                if (type == ((ComboBoxItem)this.columnTypesCombo.Items[i]).ColumnType)
                {
                    return i;
                }
            }
            return -1;
        }

        private void unboundColumnRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.unboundColumnRadioButton.Checked)
            {
                this.nameTextBox.Text = this.headerTextBox.Text = this.AssignName();
                this.nameTextBox.Focus();
            }
        }

        public static bool ValidName(string name, FilterColumnCollection columns, IContainer container, INameCreationService nameCreationService, FilterColumnCollection liveColumns, bool allowDuplicateNameInLiveColumnCollection)
        {
            if (columns.Contains(name))
            {
                return false;
            }
            if (((container != null) && (container.Components[name] != null)) && ((!allowDuplicateNameInLiveColumnCollection || (liveColumns == null)) || !liveColumns.Contains(name)))
            {
                return false;
            }
            return (((nameCreationService == null) || nameCreationService.IsValidName(name)) || ((allowDuplicateNameInLiveColumnCollection && (liveColumns != null)) && liveColumns.Contains(name)));
        }

        public static bool ValidName(string name, FilterColumnCollection columns, IContainer container, INameCreationService nameCreationService, FilterColumnCollection liveColumns, bool allowDuplicateNameInLiveColumnCollection, out string errorString)
        {
            if (columns.Contains(name))
            {
                errorString = "DuplicateColumnName[{0}]".FormatArgs(new object[] { name });
                return false;
            }
            if (((container != null) && (container.Components[name] != null)) && ((!allowDuplicateNameInLiveColumnCollection || (liveColumns == null)) || !liveColumns.Contains(name)))
            {
                errorString = "DesignerHostDuplicateName[{0}]".FormatArgs(new object[] { name });
                return false;
            }
            if (((nameCreationService != null) && !nameCreationService.IsValidName(name)) && ((!allowDuplicateNameInLiveColumnCollection || (liveColumns == null)) || !liveColumns.Contains(name)))
            {
                errorString = "CodeDomDesignerLoaderInvalidIdentifier[{0}]".FormatArgs(new object[] { name });
                return false;
            }
            errorString = string.Empty;
            return true;
        }

        private class ComboBoxItem
        {
            private System.Type columnType;

            public ComboBoxItem(System.Type columnType)
            {
                this.columnType = columnType;
            }

            public override string ToString()
            {
                return this.columnType.Name;
            }

            public System.Type ColumnType
            {
                get
                {
                    return this.columnType;
                }
            }
        }

        private class ListBoxItem
        {
            private string propertyName;
            private System.Type propertyType;

            public ListBoxItem(System.Type propertyType, string propertyName)
            {
                this.propertyType = propertyType;
                this.propertyName = propertyName;
            }

            public override string ToString()
            {
                return this.propertyName;
            }

            public string PropertyName
            {
                get
                {
                    return this.propertyName;
                }
            }

            public System.Type PropertyType
            {
                get
                {
                    return this.propertyType;
                }
            }
        }
    }
}
