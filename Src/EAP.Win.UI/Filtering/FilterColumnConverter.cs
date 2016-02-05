using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design.Serialization;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;

namespace EAP.Win.UI
{
    internal class FilterColumnConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
        {
            try
            {
                return ((destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType));
            }
            catch (Exception exc)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(@"D:\a.txt", true);
                sw.Write(exc.ToString());
                sw.Close();
                return false;
            }
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
        {
            try
            {
                if (destinationType == null)
                {
                    throw new ArgumentNullException("destinationType");
                }
                FilterColumn column = value as FilterColumn;
                if ((destinationType == typeof(InstanceDescriptor)) && (column != null))
                {
                    ConstructorInfo constructor;
                    constructor = column.GetType().GetConstructor(new System.Type[0]);
                    if (constructor != null)
                    {
                        return new InstanceDescriptor(constructor, new object[0], false);
                    }
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }
            catch (Exception exc)
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(@"D:\a.txt", true);
                sw.Write(exc.ToString());
                sw.Close();
                return null;
            }
        }
    }
}
