using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Controls.Editors.DiagramEditor;
using EAP.ModelFirst.Controls;
using System.Reflection;

namespace EAP.ModelFirst.Utils
{
    internal static class CrashHandler
    {
        public static void CreateGlobalErrorHandler()
        {
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(AppDomain_UnhandledException);
        }


        public static string GetVersionString()
        {
            Version CurrentVersion = Assembly.GetExecutingAssembly().GetName().Version;
            if (CurrentVersion.Minor == 0)
            {
                return string.Format("Model First {0}.0", CurrentVersion.Major);
            }
            else
            {
                return string.Format("Model First {0}.{1:00}",
                    CurrentVersion.Major, CurrentVersion.Minor);
            }
        }

        private static void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var appDataDirectory =
                Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "Model First");
            if (e.IsTerminating)
            {
                string crashDir = Path.Combine(appDataDirectory, "crash");
                Directory.CreateDirectory(crashDir);

                CreateBackups(crashDir);
                Exception ex = (Exception)e.ExceptionObject;
                CreateCrashLog(crashDir, ex);


                Client.ShowError(Strings.ProgramTerminates + "\r\n" + ex);

                System.Diagnostics.Process.Start(crashDir);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                // Goodbye!
            }
        }

        private static void CreateBackups(string directory)
        {
            int untitledCount = 0;
            foreach (Project project in MainForm.Workspace.Projects)
            {
                if (project.IsDirty)
                {
                    try
                    {
                        string fileName = project.FileName;
                        if (project.IsUntitled)
                        {
                            untitledCount++;
                            fileName = project.Name + untitledCount + Project.FileExtension;
                        }
                        string filePath = Path.Combine(directory, fileName);

                        project.Save(filePath);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private static void CreateCrashLog(string directory, Exception exception)
        {
            StreamWriter writer = null;

            try
            {
                string filePath = Path.Combine(directory, "crash.log");
                writer = new StreamWriter(filePath);

                writer.WriteLine(string.Format(
                    Strings.SendLogFile, Properties.Resources.MailAddress));
                writer.WriteLine();
                writer.WriteLine("Version: {0}", GetVersionString());
                writer.WriteLine("Mono: {0}", MonoHelper.IsRunningOnMono ? "yes" : "no");
                if (MonoHelper.IsRunningOnMono)
                    writer.WriteLine("Mono version: {0}", MonoHelper.Version);
                writer.WriteLine("OS: {0}", Environment.OSVersion.VersionString);

                writer.WriteLine();
                writer.WriteLine(exception.Message);
                Exception innerException = exception.InnerException;
                while (innerException != null)
                {
                    writer.WriteLine(innerException.Message);
                    innerException = innerException.InnerException;
                }

                writer.WriteLine();
                writer.WriteLine(exception.StackTrace);
            }
            catch
            {
                // Do nothing
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
