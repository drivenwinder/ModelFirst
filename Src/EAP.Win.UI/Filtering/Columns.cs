using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using EAP.Win.UI.Utils;
using System.ComponentModel;
using System.Drawing;

namespace EAP.Win.UI
{
    [ToolboxBitmap(typeof(resfinder), "EAP.Win.UI.Filtering.Resources.FilterCheckBoxColumn.bmp")]
    public class FilterCheckBoxColumn : FilterColumn
    {
        protected override Control InitialControl()
        {
            KryptonCheckBox ctl = new KryptonCheckBox();
            ctl.Text = "";
            ctl.Height = 15;
            return ctl;
        }

        public override object ExtractValues(Control control)
        {
            KryptonCheckBox ctl = control as KryptonCheckBox;
            return ctl.Checked;
        }
    }

    [ToolboxBitmap(typeof(resfinder), "EAP.Win.UI.Filtering.Resources.FilterListBoxColumn.bmp")]
    public class FilterListBoxColumn : FilterColumn
    {
        [AttributeProvider(typeof(IListSource)), DefaultValue((string)null), RefreshProperties(RefreshProperties.Repaint)]
        public object DataSource { get; set; }
        public string DisplayMember { get; set; }
        public string ValueMember { get; set; }

        protected internal override void CloneInternal(FilterColumn column)
        {
            base.CloneInternal(column);
            FilterListBoxColumn c = (FilterListBoxColumn)column;
            c.DataSource = DataSource;
            c.DisplayMember = DisplayMember;
            c.ValueMember = ValueMember;
        }

        protected override Control InitialControl()
        {
            KryptonListBox ctl = new KryptonListBox();
            ctl.DataSource = DataSource;
            ctl.DisplayMember = DisplayMember;
            ctl.ValueMember = ValueMember;
            return ctl;
        }

        public override object ExtractValues(Control control)
        {
            KryptonListBox ctl = control as KryptonListBox;
            object[] value = new object[ctl.SelectedItems.Count];
            for (int i = 0; i < ctl.SelectedItems.Count; i++)
            {
                if (ctl.ValueMember.IsNotEmpty())
                {
                    object o = ctl.SelectedItems[i];
                    value[i] = o.GetType().GetProperty(ctl.ValueMember).GetValue(o, null);
                }
                else
                    value[i] = ctl.SelectedItems[i];
            }
            return value;
        }
    }

    [ToolboxBitmap(typeof(resfinder), "EAP.Win.UI.Filtering.Resources.FilterRichTextBoxColumn.bmp")]
    public class FilterRichTextBoxColumn : FilterColumn
    {
        public FilterRichTextBoxColumn()
        {
            Height = 50;
        }

        public int Height { get; set; }

        protected override Control InitialControl()
        {
            KryptonRichTextBox ctl = new KryptonRichTextBox();
            ctl.Height = Height;
            return ctl;
        }

        protected internal override void CloneInternal(FilterColumn column)
        {
            FilterRichTextBoxColumn c = column as FilterRichTextBoxColumn;
            c.Height = Height;
            base.CloneInternal(column);
        }

        public override object ExtractValues(Control control)
        {
            KryptonRichTextBox ctl = control as KryptonRichTextBox;
            return ctl.Text;
        }
    }

    [ToolboxBitmap(typeof(resfinder), "EAP.Win.UI.Filtering.Resources.FilterComboBoxColumn.bmp")]
    public class FilterComboBoxColumn : FilterColumn
    {
        [AttributeProvider(typeof(IListSource)), DefaultValue((string)null), RefreshProperties(RefreshProperties.Repaint)]
        public object DataSource { get; set; }
        public string DisplayMember { get; set; }
        public string ValueMember { get; set; }

        protected internal override void CloneInternal(FilterColumn column)
        {
            base.CloneInternal(column);
            FilterComboBoxColumn c = (FilterComboBoxColumn)column;
            c.DataSource = DataSource;
            c.DisplayMember = DisplayMember;
            c.ValueMember = ValueMember;
        }

        protected override Control InitialControl()
        {
            KryptonComboBox ctl = new KryptonComboBox();
            ctl.DataSource = DataSource;
            ctl.DisplayMember = DisplayMember;
            ctl.ValueMember = ValueMember;
            return ctl;
        }

        public override object ExtractValues(Control control)
        {
            KryptonComboBox ctl = control as KryptonComboBox;
            return ctl.SelectedValue;
        }
    }

    [ToolboxBitmap(typeof(resfinder), "EAP.Win.UI.Filtering.Resources.FilterCheckListBoxColumn.bmp")]
    public class FilterCheckListBoxColumn : FilterColumn
    {

        protected override Control InitialControl()
        {
            KryptonCheckedListBox ctl = new KryptonCheckedListBox();
            return ctl;
        }

        public override object ExtractValues(Control control)
        {
            KryptonCheckedListBox ctl = control as KryptonCheckedListBox;
            object[] value = new object[ctl.SelectedItems.Count];
            ctl.SelectedItems.CopyTo(value, 0);
            return value;
        }
    }

    [ToolboxBitmap(typeof(resfinder), "EAP.Win.UI.Filtering.Resources.FilterNumericUpDownColumn.bmp")]
    public class FilterNumericBoxColumn : FilterColumn, IScopable
    {
        public FilterNumericBoxColumn()
        {

        }

        public bool IsScoped { get; set; }

        public int DecimalPlaces { get; set; }

        protected internal override void CloneInternal(FilterColumn column)
        {
            base.CloneInternal(column);
            FilterNumericBoxColumn c = (FilterNumericBoxColumn)column;
            c.IsScoped = IsScoped;
            c.DecimalPlaces = DecimalPlaces;
        }

        protected override Control InitialControl()
        {
            if (IsScoped)
            {
                Panel ctl = new Panel();
                ctl.BackColor = Color.Transparent;
                KryptonNumericUpDown begin = new KryptonNumericUpDown();
                KryptonNumericUpDown end = new KryptonNumericUpDown();
                KryptonLabel lbl = new KryptonLabel();
                begin.Width = Width / 2 - 7;
                begin.Dock = DockStyle.Left;
                begin.DecimalPlaces = DecimalPlaces;
                end.Width = Width / 2 - 7;
                end.Dock = DockStyle.Right;
                end.DecimalPlaces = DecimalPlaces;
                lbl.Text = "-";
                lbl.Dock = DockStyle.Fill;
                lbl.LabelStyle = LabelStyle.NormalPanel;
                ctl.Controls.Add(lbl);
                ctl.Controls.Add(begin);
                ctl.Controls.Add(end);
                ctl.Height = begin.Height;
                return ctl;
            }
            else
            {
                KryptonNumericUpDown ctl = new KryptonNumericUpDown();
                ctl.DecimalPlaces = DecimalPlaces;
                return ctl;
            }
        }

        public override object ExtractValues(Control control)
        {
            if (IsScoped)
            {
                Panel ctl = control as Panel;
                KryptonNumericUpDown begin = ctl.Controls[1] as KryptonNumericUpDown;
                KryptonNumericUpDown end = ctl.Controls[2] as KryptonNumericUpDown;
                object[] value = new object[2];
                value[0] = begin.Value;
                value[1] = end.Value;
                return value;
            }
            else
            {
                KryptonNumericUpDown ctl = control as KryptonNumericUpDown;
                return ctl.Value;
            }
        }
    }

    [ToolboxBitmap(typeof(resfinder), "EAP.Win.UI.Filtering.Resources.FilterTextBoxColumn.bmp")]
    public class FilterTextBoxColumn : FilterColumn, IScopable
    {
        public bool IsScoped { get; set; }

        protected internal override void CloneInternal(FilterColumn column)
        {
            base.CloneInternal(column);
            FilterTextBoxColumn c = (FilterTextBoxColumn)column;
            c.IsScoped = IsScoped;
        }

        protected override Control InitialControl()
        {
            if (IsScoped)
            {
                Panel ctl = new Panel();
                ctl.BackColor = Color.Transparent;
                KryptonTextBox begin = new KryptonTextBox();
                KryptonTextBox end = new KryptonTextBox();
                KryptonLabel lbl = new KryptonLabel();
                begin.Width = Width / 2 - 7;
                begin.Dock = DockStyle.Left;
                end.Width = Width / 2 - 7;
                end.Dock = DockStyle.Right;
                lbl.Text = "-";
                lbl.Dock = DockStyle.Fill;
                lbl.LabelStyle = LabelStyle.NormalPanel;
                ctl.Controls.Add(lbl);
                ctl.Controls.Add(begin);
                ctl.Controls.Add(end);
                ctl.Height = begin.Height + 2;
                return ctl;
            }
            else
            {
                KryptonTextBox ctl = new KryptonTextBox();
                return ctl;
            }
        }

        public override object ExtractValues(Control control)
        {
            if (IsScoped)
            {
                Panel ctl = control as Panel;
                KryptonTextBox begin = ctl.Controls[1] as KryptonTextBox;
                KryptonTextBox end = ctl.Controls[2] as KryptonTextBox;
                object[] value = new object[2];
                value[0] = begin.Text;
                value[1] = end.Text;
                return value;
            }
            else
            {
                KryptonTextBox ctl = control as KryptonTextBox;
                return ctl.Text;
            }
        }
    }

    [ToolboxBitmap(typeof(resfinder), "EAP.Win.UI.Filtering.Resources.FilterDateTimePickerColumn.bmp")]
    public class FilterDateTimePickerColumn : FilterColumn, IScopable
    {
        public FilterDateTimePickerColumn()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }

        public bool IsScoped { get; set; }

        public string DateTimeFormat { get; set; }

        protected internal override void CloneInternal(FilterColumn column)
        {
            base.CloneInternal(column);
            FilterDateTimePickerColumn c = (FilterDateTimePickerColumn)column;
            c.IsScoped = IsScoped;
            c.DateTimeFormat = DateTimeFormat;
        }

        protected override Control InitialControl()
        {
            if (IsScoped)
            {
                Panel ctl = new Panel();
                ctl.BackColor = Color.Transparent;
                KryptonDateTimePicker begin = new KryptonDateTimePicker();
                KryptonDateTimePicker end = new KryptonDateTimePicker();
                KryptonLabel lbl = new KryptonLabel();
                begin.Width = Width / 2 - 7;
                begin.Dock = DockStyle.Left;
                begin.Format = DateTimePickerFormat.Custom;
                begin.CustomFormat = DateTimeFormat;
                end.Width = Width / 2 - 7;
                end.Dock = DockStyle.Right;
                end.Format = DateTimePickerFormat.Custom;
                end.CustomFormat = DateTimeFormat;
                lbl.Text = "-";
                lbl.Dock = DockStyle.Fill;
                lbl.LabelStyle = LabelStyle.NormalPanel;
                ctl.Controls.Add(lbl);
                ctl.Controls.Add(begin);
                ctl.Controls.Add(end);
                ctl.Height = begin.Height;
                return ctl;
            }
            else
            {
                KryptonDateTimePicker ctl = new KryptonDateTimePicker();
                ctl.Format = DateTimePickerFormat.Custom;
                ctl.CustomFormat = DateTimeFormat;
                return ctl;
            }
        }

        public override object ExtractValues(Control control)
        {
            if (IsScoped)
            {
                Panel ctl = control as Panel;
                KryptonDateTimePicker begin = ctl.Controls[1] as KryptonDateTimePicker;
                KryptonDateTimePicker end = ctl.Controls[2] as KryptonDateTimePicker;
                object[] value = new object[2];
                value[0] = begin.Value == begin.MinDate ? DateTime.MinValue : begin.Value;
                value[1] = end.Value == end.MaxDate ? DateTime.MaxValue : end.Value;
                return value;
            }
            else
            {
                KryptonDateTimePicker ctl = control as KryptonDateTimePicker;
                if (ctl.Value == ctl.MinDate)
                    return DateTime.MinValue;
                return ctl.Value;
            }
        }
    }
}
