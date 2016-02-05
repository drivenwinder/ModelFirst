using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel.Design;
using System.Runtime;

namespace EAP.Win.UI.Design
{
    [ToolboxItem(false), DesignTimeVisible(false)]
    internal class FilterColumnTypePicker : ContainerControl
    {
        private static System.Type dataGridViewColumnType = typeof(FilterColumn);
        private IWindowsFormsEditorService edSvc;
        private const int MinimumHeight = 90;
        private const int MinimumWidth = 100;
        private System.Type selectedType;
        private ListBox typesListBox = new ListBox();

        public FilterColumnTypePicker()
        {
            base.Size = this.typesListBox.Size;
            this.typesListBox.Dock = DockStyle.Fill;
            this.typesListBox.Sorted = true;
            this.typesListBox.HorizontalScrollbar = true;
            this.typesListBox.SelectedIndexChanged += new EventHandler(this.typesListBox_SelectedIndexChanged);
            base.Controls.Add(this.typesListBox);
            this.BackColor = SystemColors.Control;
            base.ActiveControl = this.typesListBox;
        }

        private void CloseDropDown()
        {
            if (this.edSvc != null)
            {
                this.edSvc.CloseDropDown();
            }
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if ((BoundsSpecified.Width & specified) == BoundsSpecified.Width)
            {
                width = Math.Max(width, 100);
            }
            if ((BoundsSpecified.Height & specified) == BoundsSpecified.Height)
            {
                height = Math.Max(height, 90);
            }
            base.SetBoundsCore(x, y, width, height, specified);
        }

        public void Start(IWindowsFormsEditorService edSvc, ITypeDiscoveryService discoveryService, System.Type defaultType)
        {
            this.edSvc = edSvc;
            this.typesListBox.Items.Clear();
            foreach (System.Type type in DesignerUtils.FilterGenericTypes(discoveryService.GetTypes(dataGridViewColumnType, false)))
            {
                if (((type != dataGridViewColumnType) && !type.IsAbstract) && (type.IsPublic || type.IsNestedPublic))
                {
                    FilterColumnDesignTimeVisibleAttribute attribute = TypeDescriptor.GetAttributes(type)[typeof(FilterColumnDesignTimeVisibleAttribute)] as FilterColumnDesignTimeVisibleAttribute;
                    if ((attribute == null) || attribute.Visible)
                    {
                        this.typesListBox.Items.Add(new ListBoxItem(type));
                    }
                }
            }
            this.typesListBox.SelectedIndex = this.TypeToSelectedIndex(defaultType);
            this.selectedType = null;
            base.Width = Math.Max(base.Width, this.PreferredWidth + (SystemInformation.VerticalScrollBarWidth * 2));
        }

        private void typesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedType = ((ListBoxItem)this.typesListBox.SelectedItem).ColumnType;
            this.edSvc.CloseDropDown();
        }

        private int TypeToSelectedIndex(System.Type type)
        {
            for (int i = 0; i < this.typesListBox.Items.Count; i++)
            {
                if (type == ((ListBoxItem)this.typesListBox.Items[i]).ColumnType)
                {
                    return i;
                }
            }
            return -1;
        }

        private int PreferredWidth
        {
            get
            {
                int num = 0;
                using (Graphics graphics = this.typesListBox.CreateGraphics())
                {
                    for (int i = 0; i < this.typesListBox.Items.Count; i++)
                    {
                        ListBoxItem item = (ListBoxItem)this.typesListBox.Items[i];
                        num = Math.Max(num, Size.Ceiling(graphics.MeasureString(item.ToString(), this.typesListBox.Font)).Width);
                    }
                }
                return num;
            }
        }

        public System.Type SelectedType
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.selectedType;
            }
        }

        private class ListBoxItem
        {
            private System.Type columnType;

            public ListBoxItem(System.Type columnType)
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
    }
}
