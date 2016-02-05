using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using EAP.ModelFirst.Controls.Dialogs;
using System.ComponentModel;
using EAP.ModelFirst.Utils;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Controls.Editors
{
    public class TypeMemberEditor : UITypeEditor
    {
        private TypeBaseDialog dialog;

        private TypeMemberEditor()
        {
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            try
            {
                if (((provider != null) && (context != null)) && (context.Instance != null))
                {
                    if (dialog == null)
                        dialog = new TypeBaseDialog();
                    var m = value as MemberInfo;
                    if (m != null)
                        dialog.ShowDialog(m.Type, true);
                }
                return value;
            }
            catch (Exception exc)
            {
                Client.ShowError(exc.ToString());
                throw;
            }
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
