using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Linq;

namespace AutoUpdate
{
    public static class Expand
    {
        /// <summary>
        /// 异步刷新UI
        /// </summary>
        /// <param name="app"></param>
        public static void DoEvent(this Application app)
        {
            try
            {
                DispatcherFrame frame = new DispatcherFrame();
                Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrame), frame);
                Dispatcher.PushFrame(frame);
            }
            catch { }
        }

        static object ExitFrame(object f)
        {
            try
            {
                ((DispatcherFrame)f).Continue = false;
                return null;
            }
            catch { return null; }
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNotEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string FormatArgs(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        public static string GetElementValue(this XElement element, string name)
        {
            if (element == null)
                return null;
            var node = element.Element(name);
            if (node == null)
                return null;
            return node.Value;
        }

        public static bool CIEquals(this string x, string y)
        {
            if(x != null)
                return x.Equals(y, StringComparison.OrdinalIgnoreCase);
            if (y != null)
                return y.Equals(x, StringComparison.OrdinalIgnoreCase);
            return true;
        }
    }
}
