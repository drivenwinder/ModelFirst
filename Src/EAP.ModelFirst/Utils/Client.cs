using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComponentFactory.Krypton.Toolkit;
using System.Windows.Forms;
using EAP.ModelFirst.Properties;
using System.Runtime.InteropServices;

namespace EAP.ModelFirst.Utils
{
    public static class Client
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]

        internal static extern IntPtr GetFocus();

        public static Control GetFocusedControl()
        {
            Control focusedControl = null;
            IntPtr focusedHandle = GetFocus();
            if (focusedHandle != IntPtr.Zero)
                focusedControl = Control.FromHandle(focusedHandle);
            return focusedControl;
        }

        public static void ShowError(string message)
        {
            KryptonMessageBox.Show(message, Strings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowInfo(string message)
        {
            KryptonMessageBox.Show(message, Strings.Prompt, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// DialogResult : OK/Cancel
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static DialogResult ShowConfirm(string message,
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            return KryptonMessageBox.Show(message, Strings.Confirmation, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, defaultButton);
        }

        public static DialogResult Show(string message, string caption,
            MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            return KryptonMessageBox.Show(message, caption, buttons, icon, defaultButton);
        }
    }
}
