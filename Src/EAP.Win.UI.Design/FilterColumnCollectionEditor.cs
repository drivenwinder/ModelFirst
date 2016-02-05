using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace EAP.Win.UI.Design
{
    public class FilterColumnCollectionEditor: UITypeEditor
    {
        private FilterColumnCollectionDialog dataGridViewColumnCollectionDialog;

        private FilterColumnCollectionEditor()
        {
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                IWindowsFormsEditorService service = (IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));
                if ((service == null) || (context.Instance == null))
                {
                    return value;
                }
                IDesignerHost host = (IDesignerHost) provider.GetService(typeof(IDesignerHost));
                if (host == null)
                {
                    return value;
                }
                if (this.dataGridViewColumnCollectionDialog == null)
                {
                    this.dataGridViewColumnCollectionDialog = new FilterColumnCollectionDialog(((FilterControl) context.Instance).Site);
                }
                this.dataGridViewColumnCollectionDialog.SetLiveFilterControl((FilterControl)context.Instance);
                using (DesignerTransaction transaction = host.CreateTransaction("FilterColumnCollectionTransaction"))
                {
                    if (service.ShowDialog(this.dataGridViewColumnCollectionDialog) == DialogResult.OK)
                    {
                        transaction.Commit();
                        return value;
                    }
                    transaction.Cancel();
                }
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
