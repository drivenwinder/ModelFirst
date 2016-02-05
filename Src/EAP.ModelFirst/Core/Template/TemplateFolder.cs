using System.IO;
using System.Text;
using System;

namespace EAP.ModelFirst.Core.Template
{
    public class TemplateFolder : INamedObject
    {
        public event System.EventHandler Renamed;

        DirectoryInfo directoryInfo;

        public TemplateFolder(DirectoryInfo dir)
        {
            directoryInfo = dir;
        }

        public TemplateFolder(string path)
        {
            directoryInfo = new DirectoryInfo(path);
        }

        public string Name
        {
            get { return directoryInfo.Name; }
            set
            {
                if (directoryInfo.Name != value)
                {
                    directoryInfo.MoveTo(Path.Combine(directoryInfo.Parent.FullName, value));
                    OnRename(EventArgs.Empty);
                }
            }
        }

        protected virtual void OnRename(EventArgs e)
        {
            if (Renamed != null)
                Renamed(this, e);
        }

        public string FullName
        {
            get { return directoryInfo.FullName; }
        }

        public TemplateFolder AddNewFolder()
        {
            var newName = GetNewFolderName("NewFolder");
            var dir = Directory.CreateDirectory(Path.Combine(directoryInfo.FullName, newName));
            return new TemplateFolder(dir);
        }

        public TemplateFile AddNewTemplate()
        {
            var newName = GetNewTemplateName("NewTemplate");
            var file = new FileInfo(Path.Combine(directoryInfo.FullName, newName));
            using (StreamWriter sw = new StreamWriter(file.Create(), Encoding.UTF8))
            {
                sw.Write("@{ var output=\"\\\\\"+Model.ProjectName+\"\\\\template.txt\"; }\r\n");
                sw.Close();
            }
            return new TemplateFile(file);
        }

        public string GetNewFolderName(string initName)
        {
            string n = initName;
            int i = 1;
            do
            {
                n = initName + i++;
                if (!ExistsFolder(n))
                    return n;
            }
            while (true);
        }

        public string GetNewTemplateName(string initName)
        {
            string n = initName;
            int i = 1;
            do
            {
                n = initName + i++;
                if (!ExistsFile(n + TemplateFile.Extension))
                    return n + TemplateFile.Extension;
            }
            while (true);
        }

        public bool ExistsFile(string name)
        {
            return File.Exists(Path.Combine(directoryInfo.FullName, name));
        }

        public bool ExistsFolder(string name)
        {
            return Directory.Exists(Path.Combine(directoryInfo.FullName, name));
        }

        public void MoveTo(TemplateFolder folder)
        {
            directoryInfo.MoveTo(Path.Combine(folder.directoryInfo.FullName, Name));
        }

        public void Delete()
        {
            directoryInfo.Delete(true);
        }

        public void LookupFolder()
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            psi.Arguments = " /e," + directoryInfo.FullName;
            System.Diagnostics.Process.Start(psi);
        }
    }
}
