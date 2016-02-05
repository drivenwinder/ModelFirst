using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using EAP.Win.UI.Utils;

namespace EAP.Win.UI
{
    [ToolboxItem(true)]
    [Designer("EAP.Win.UI.Design.FilterControlDesigner, EAP.Win.UI.Design")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [Docking(DockingBehavior.Ask)]
    [DefaultEvent("CellContentClick")]
    [ComplexBindingProperties("DataSource", "DataMember")]
    [Editor("EAP.Win.UI.Design.FilterControlComponentEditor, EAP.Win.UI", typeof(ComponentEditor))]
    [ToolboxBitmap(typeof(resfinder), "EAP.Win.UI.Filtering.FilterControl.bmp")]
    public class FilterControl : KryptonPanel, ISupportInitialize
    {
        FilterColumnCollection columns;
        int columnCount = 3;
        bool autoSize = true;
        bool initialized = false;
        Dictionary<FilterColumn, Control> controls = new Dictionary<FilterColumn, Control>();
        Padding ControlPadding = new Padding(3, 0, 10, 3);
        private static readonly object event_ColumnRemoved = new object();
        private static readonly object event_DataSourceChanged = new object();
        private static readonly object event_DataMemberChanged = new object();

        public FilterControl()
        {
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Width = 400;
            Height = 100;
            base.PerformLayout();
            base.Invalidate();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("EAP.Win.UI.Design.FilterColumnCollectionEditor, EAP.Win.UI.Design", typeof(UITypeEditor))]
        [MergableProperty(false)]
        public FilterColumnCollection Columns
        {
            get
            {
                if (this.columns == null)
                {
                    this.columns = this.CreateColumnsInstance();
                    columns.CollectionChanged += new CollectionChangeEventHandler(columns_CollectionChanged);
                }
                return this.columns;
            }
        }

        [DefaultValue(3)]
        public int ColumnCount
        {
            get { return columnCount; }
            set
            {
                if (value != columnCount)
                {
                    columnCount = value;
                    OnPropertyChanged();
                }
            }
        }

        [DefaultValue(true), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public override bool AutoSize
        {
            get { return autoSize; }
            set
            {
                if (value != autoSize)
                {
                    autoSize = value;
                    OnPropertyChanged();
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public QueryCriteria Value
        {
            get
            {
                GroupOperator result = new GroupOperator(QueryGroup.And);
                foreach (FilterColumn col in Columns)
                {
                    object value = col.ExtractValues(controls[col]);
                    if (col is IScopable && ((IScopable)col).IsScoped)
                    {
                        object[] objs = (object[])value;
                        if (!IsNull(objs[0]))
                        {
                            BinaryOperator op = new BinaryOperator(col.DataPropertyName, objs[0],
                                QueryType.GreaterOrEqual);
                            result.Operands.Add(op);
                        }
                        if (!IsNull(objs[1]))
                        {
                            BinaryOperator op = new BinaryOperator(col.DataPropertyName, objs[1],
                                QueryType.LessOrEqual);
                            result.Operands.Add(op);
                        }
                    }
                    else
                    {
                        if (col is FilterComboBoxColumn)
                        {
                            if (!IsNull(value))
                            {
                                BinaryOperator op = new BinaryOperator(col.DataPropertyName, value,
                                    QueryType.Equal);
                                result.Operands.Add(op);
                            }
                        }
                        else if (col is FilterCheckListBoxColumn || col is FilterListBoxColumn)
                        {
                            object[] arr = value as object[];
                            if (arr != null && arr.Length > 0)
                            {
                                InOperator op = new InOperator(col.DataPropertyName, arr);
                                result.Operands.Add(op);
                            }
                        }
                        else
                        {
                            if (!IsNull(value))
                            {
                                QueryType type = (col is FilterTextBoxColumn || col is FilterRichTextBoxColumn) ?
                                    QueryType.Like : QueryType.Equal;
                                BinaryOperator op = new BinaryOperator(col.DataPropertyName, value, type);
                                result.Operands.Add(op);
                            }
                        }
                    }
                }
                return result;
            }
        }

        bool IsNull(object o)
        {
            if (o == null || o == DBNull.Value)
                return true;
            if (o is string && ((string)o).IsNullOrEmpty())
                return true;
            if (o is DateTime && (((DateTime)o) == DateTime.MaxValue || ((DateTime)o) == DateTime.MinValue))
                return true;
            return false;
        }

        private FilterColumnCollection CreateColumnsInstance()
        {
            return new FilterColumnCollection(this);
        }

        int[,] CaculateWidth()
        {
            int[,] result = new int[2, ColumnCount];
            int index = 0;
            foreach (FilterColumn c in Columns)
            {
                if (index >= columnCount)
                {
                    index = 0;
                }
                SizeF size = DrawHelper.GetFontSize(c.HeaderText, Font);
                result[0, index] = Math.Max(result[0, index], (int)size.Width);
                result[1, index] = Math.Max(result[1, index], c.Width);
                index++;
            }
            return result;
        }

        void CreateControls()
        {
            Controls.Clear();
            controls.Clear();
            int[,] widths = CaculateWidth();
            int height = 0;
            int width = 0;
            int x = Padding.Left;
            int y = 5 + Padding.Top;
            int index = 0;
            int headerHeight = (int)DrawHelper.GetFontSize("A", Font).Height;
            foreach (FilterColumn c in Columns)
            {
                if (index >= ColumnCount)
                {
                    y += ControlPadding.Bottom + height;
                    x = Padding.Left;
                    index = 0;
                    height = 0;
                }
                Control ctl = c.CreateControl();
                controls.Add(c, ctl);
                if (c.HeaderText.IsNotEmpty())
                {
                    KryptonLabel header = new KryptonLabel();
                    header.LabelStyle = LabelStyle.NormalPanel;
                    header.Text = c.HeaderText;
                    header.AutoSize = true;
                    header.Location = new Point(x, y);
                    Controls.Add(header);
                    x += (int)widths[0, index] + ControlPadding.Left;
                    ctl.Location = new Point(x + 5, Math.Max(y + (headerHeight - ctl.Height) / 2, y - ControlPadding.Bottom));
                }
                else
                {
                    ctl.Location = new Point(x + 5, y);
                }

                Controls.Add(ctl);
                height = Math.Max(height, ctl.Height);
                x += c.Width + ControlPadding.Right;
                width = Math.Max(x, width);
                index++;
            }
            if (AutoSize)
            {
                Height = height + y + Padding.Bottom - ControlPadding.Bottom + 2;
                AutoScrollMinSize = new Size(width, Height);
            }
            else
                AutoScrollMinSize = Size.Empty;
        }

        protected override void OnCreateControl()
        {
            CreateControls();
            base.OnCreateControl();
        }

        [RefreshProperties(RefreshProperties.Repaint), DefaultValue((string)null), AttributeProvider(typeof(IListSource))]
        public object DataSource { get; set; }

        public string DataMember { get; set; }

        public event EventHandler DataSourceChanged
        {
            add
            {
                base.Events.AddHandler(event_DataSourceChanged, value);
            }
            remove
            {
                base.Events.RemoveHandler(event_DataSourceChanged, value);
            }
        }

        public event EventHandler DataMemberChanged
        {
            add
            {
                base.Events.AddHandler(event_DataMemberChanged, value);
            }
            remove
            {
                base.Events.RemoveHandler(event_DataMemberChanged, value);
            }
        }

        public event FilterControlColumnEventHandler ColumnRemoved
        {
            add
            {
                base.Events.AddHandler(event_ColumnRemoved, value);
            }
            remove
            {
                base.Events.RemoveHandler(event_ColumnRemoved, value);
            }
        }

        public override void BeginInit()
        {
            initialized = false;
            base.BeginInit();
        }

        public override void EndInit()
        {
            initialized = true;
            base.EndInit();
        }

        protected override void OnAutoSizeChanged(EventArgs e)
        {
            base.OnAutoSizeChanged(e);
            OnPropertyChanged();
        }

        void columns_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            OnPropertyChanged();
        }

        protected virtual void OnPropertyChanged()
        {
            if (initialized)
                CreateControls();
        }
    }
}
