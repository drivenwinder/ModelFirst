using System.IO;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Uml;
using System.Collections.Specialized;
using System;

namespace EAP.ModelFirst.Properties
{
    public partial class Settings
    {
        const int MaxRecentFileCount = 5;

        public event EventHandler TemplateFolderChanged;
        public event EventHandler UILanguageChanged;

        public Language GetDefaultLanguage()
        {
            Language defaultLanguage = Language.GetLanguage(DefaultLanguageName);

            return defaultLanguage ?? UnifiedModelingLanguage.Instance;
        }

        public void AddRecentFile(string recentFile)
        {
            if (!File.Exists(recentFile))
                return;

            int index = RecentFiles.IndexOf(recentFile);

            if (index < 0)
            {
                if (RecentFiles.Count < MaxRecentFileCount)
                    RecentFiles.Add(string.Empty);

                for (int i = RecentFiles.Count - 2; i >= 0; i--)
                    RecentFiles[i + 1] = RecentFiles[i];
                RecentFiles[0] = recentFile;
            }
            else if (index > 0)
            {
                string temp = RecentFiles[index];
                for (int i = index; i > 0; i--)
                    RecentFiles[i] = RecentFiles[i - 1];
                RecentFiles[0] = temp;
            }
        }

        public DirectoryInfo GetTemplateFolder()
        {
            if (TemplateFolder.IsNullOrWhiteSpace())
                TemplateFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Model First", "Templates");
            if (!Directory.Exists(TemplateFolder))
                Directory.CreateDirectory(TemplateFolder);
            return new DirectoryInfo(TemplateFolder);
        }

        protected override void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);
            if (e.PropertyName == "TemplateFolder")
                OnTemplateFolderChanged(EventArgs.Empty);
            if (e.PropertyName == "UILanguage")
                OnTemplateFolderChanged(EventArgs.Empty);
        }

        void OnTemplateFolderChanged(EventArgs e)
        {
            if (TemplateFolderChanged != null)
                TemplateFolderChanged(this, e);
        }

        void OnUILanguageChanged(EventArgs e)
        {
            if (UILanguageChanged != null)
                UILanguageChanged(this, e);
        }

        public static void UpdateSettings()
        {
            if (Settings.Default.CallUpgrade)
            {
                Settings.Default.Upgrade();
                Settings.Default.CallUpgrade = false;
            }

            if (Settings.Default.OpenedProjects == null)
                Settings.Default.OpenedProjects = new StringCollection();
            if (Settings.Default.RecentFiles == null)
                Settings.Default.RecentFiles = new StringCollection();
            if (Settings.Default.SelectedTemplates == null)
                Settings.Default.SelectedTemplates = new StringCollection();
            if (Settings.Default.OutputPath.IsNullOrEmpty())
                Settings.Default.OutputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Model First", "Generated Code");
        }
    }
}
