using System;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Forms;
using EAP.ModelFirst.Controls.Dialogs;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;

namespace EAP.ModelFirst
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            CrashHandler.CreateGlobalErrorHandler();
            Settings.UpdateSettings();

            // Set the user interface language
            UILanguage language = UILanguage.CreateUILanguage(Settings.Default.UILanguage);
            if (language != null)
                Strings.Culture = language.Culture;

            // Some GUI settings
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ToolStripManager.VisualStylesEnabled = false;

            // Launch the application
            Application.Run(new MainForm());

            // Save application settings
            Settings.Default.Save();
        }
    }
}