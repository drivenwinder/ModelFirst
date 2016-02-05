using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;

namespace EAP.Win.UI.Design
{
    internal class FilterColumnTypeEditor : UITypeEditor
    {
        private FilterColumnTypePicker columnTypePicker;

        private FilterColumnTypeEditor()
        {
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));
                if ((edSvc == null) || (context.Instance == null))
                {
                    return value;
                }
                if (this.columnTypePicker == null)
                {
                    this.columnTypePicker = new FilterColumnTypePicker();
                }
                FilterColumnCollectionDialog.ListBoxItem instance = (FilterColumnCollectionDialog.ListBoxItem)context.Instance;
                IDesignerHost service = (IDesignerHost) provider.GetService(typeof(IDesignerHost));
                ITypeDiscoveryService discoveryService = null;
                if (service != null)
                {
                    discoveryService = (ITypeDiscoveryService) service.GetService(typeof(ITypeDiscoveryService));
                }
                this.columnTypePicker.Start(edSvc, discoveryService, instance.FilterColumn.GetType());
                edSvc.DropDownControl(this.columnTypePicker);
                if (this.columnTypePicker.SelectedType != null)
                {
                    value = this.columnTypePicker.SelectedType;
                }
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override bool IsDropDownResizable
        {
            get
            {
                return true;
            }
        }
    }
}
