using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel.Design.Serialization;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Runtime.InteropServices;

namespace EAP.Win.UI.Design
{
    public class DesignerUtils
    {

        public static IContainer CheckForNestedContainer(IContainer container)
        {
            NestedContainer container2 = container as NestedContainer;
            if (container2 != null)
            {
                return container2.Owner.Site.Container;
            }
            return container;
        }

        public static string GetUniqueSiteName(IDesignerHost host, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            INameCreationService service = (INameCreationService)host.GetService(typeof(INameCreationService));
            if (service == null)
            {
                return null;
            }
            if (host.Container.Components[name] == null)
            {
                if (!service.IsValidName(name))
                {
                    return null;
                }
                return name;
            }
            string str = name;
            for (int i = 1; !service.IsValidName(str); i++)
            {
                str = name + i.ToString(CultureInfo.InvariantCulture);
            }
            return str;
        }

        internal static string NameFromText(string text, System.Type componentType, IServiceProvider serviceProvider)
        {
            if (serviceProvider != null)
            {
                string str = null;
                INameCreationService service = serviceProvider.GetService(typeof(INameCreationService)) as INameCreationService;
                IContainer container = (IContainer)serviceProvider.GetService(typeof(IContainer));
                if ((service != null) && (container != null))
                {
                    str = service.CreateName(container, componentType);
                    if (((text == null) || (text.Length == 0)) || (text == "-"))
                    {
                        return str;
                    }
                    string name = componentType.Name;
                    StringBuilder builder = new StringBuilder(text.Length + name.Length);
                    bool flag = false;
                    for (int i = 0; i < text.Length; i++)
                    {
                        char c = text[i];
                        if (flag)
                        {
                            if (char.IsLower(c))
                            {
                                c = char.ToUpper(c, CultureInfo.CurrentCulture);
                            }
                            flag = false;
                        }
                        if (char.IsLetterOrDigit(c))
                        {
                            if (builder.Length == 0)
                            {
                                if (char.IsDigit(c))
                                {
                                    goto Label_011D;
                                }
                                if (char.IsLower(c) != char.IsLower(str[0]))
                                {
                                    if (char.IsLower(c))
                                    {
                                        c = char.ToUpper(c, CultureInfo.CurrentCulture);
                                    }
                                    else
                                    {
                                        c = char.ToLower(c, CultureInfo.CurrentCulture);
                                    }
                                }
                            }
                            builder.Append(c);
                        Label_011D: ;
                        }
                        else if (char.IsWhiteSpace(c))
                        {
                            flag = true;
                        }
                    }
                    if (builder.Length == 0)
                    {
                        return str;
                    }
                    builder.Append(name);
                    string str3 = builder.ToString();
                    if (container.Components[str3] == null)
                    {
                        if (!service.IsValidName(str3))
                        {
                            return str;
                        }
                        return str3;
                    }
                    string str4 = str3;
                    for (int j = 1; !service.IsValidName(str4) || (container.Components[str4] != null); j++)
                    {
                        str4 = str3 + j.ToString(CultureInfo.InvariantCulture);
                    }
                    return str4;
                }
            }
            return null;
        }
        internal static bool IsCriticalException(Exception ex)
        {
#pragma warning disable 0618
            return (((((ex is NullReferenceException) || (ex is StackOverflowException)) || ((ex is OutOfMemoryException) || (ex is ThreadAbortException))) || ((ex is ExecutionEngineException) || (ex is IndexOutOfRangeException))) || (ex is AccessViolationException));
        }

        internal static ICollection FilterGenericTypes(System.Collections.ICollection types)
        {
            if ((types == null) || (types.Count == 0))
            {
                return types;
            }
            ArrayList list = new ArrayList(types.Count);
            foreach (System.Type type in types)
            {
                if (!type.ContainsGenericParameters)
                {
                    list.Add(type);
                }
            }
            return list;
        }

        public static void ApplyTreeViewThemeStyles(TreeView treeView)
        {
            if (treeView == null)
            {
                throw new ArgumentNullException("treeView");
            }
            treeView.HotTracking = true;
            treeView.ShowLines = false;
            IntPtr handle = treeView.Handle;
            SetWindowTheme(handle, "Explorer", null);
            int extendedStyle = TreeView_GetExtendedStyle(handle) | 0x44;
            TreeView_SetExtendedStyle(handle, extendedStyle, 0);
        }

        private static int TreeView_GetExtendedStyle(IntPtr handle)
        {
            return SendMessage(handle, 0x112d, IntPtr.Zero, IntPtr.Zero).ToInt32();
        }

        private static void TreeView_SetExtendedStyle(IntPtr handle, int extendedStyle, int mask)
        {
            SendMessage(handle, 0x112c, new IntPtr(mask), new IntPtr(extendedStyle));
        }

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowTheme(IntPtr hWnd, string subAppName, string subIdList);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [In, Out] TV_HITTESTINFO lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, [In, Out] HDHITTESTINFO lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        public class TV_HITTESTINFO
        {
            public int pt_x;
            public int pt_y;
            public int flags;
            public int hItem;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class HDHITTESTINFO
        {
            public int pt_x;
            public int pt_y;
            public int flags;
            public int iItem;
        }
    }
}
