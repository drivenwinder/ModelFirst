using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EAP.ModelFirst.Properties;
using System.Text.RegularExpressions;

namespace EAP.ModelFirst.Core.Template
{
    public class TemplateFile : IDocumentItem
    {
        public event EventHandler Closing;
        public event EventHandler StateChanged;
        public event EventHandler Renamed;

        FileInfo fileInfo;

        DateTime lastWriteTime;

        public TemplateFile(FileInfo file)
        {
            fileInfo = file;
            lastWriteTime = fileInfo.LastWriteTimeUtc;
        }

        public TemplateFile(string fileName)
        {
            fileInfo = new FileInfo(fileName);
        }

        public string Name
        {
            get { return fileInfo.Name; }
            set
            {
                if (!value.EndsWith(Extension))
                    throw new InvalidException(Strings.ErrorTemplateFileExtension);
                if (Name != value)
                {
                    var newFile = Path.Combine(fileInfo.DirectoryName, value);
                    if( File.Exists(newFile))
                        throw new InvalidException(Strings.ErrorNameExists.FormatArgs(value));
                    fileInfo.MoveTo(newFile);
                    OnRename(EventArgs.Empty);
                }
            }
        }

        public void MoveTo(TemplateFolder folder)
        {
            fileInfo.MoveTo(Path.Combine(folder.FullName, Name));
        }

        public static string Extension
        {
            get { return ".mft"; }
        }

        [System.ComponentModel.Browsable(false)]
        public bool IsDirty
        {
            get;
            private set;
        }

        public void Clean()
        {
            IsDirty = false;
            OnStateChanged(EventArgs.Empty);
        }

        public void Close()
        {
            OnClosing(EventArgs.Empty);
        }

        protected virtual void OnClosing(EventArgs e)
        {
            if (Closing != null)
                Closing(this, e);
        }
        
        protected virtual void OnStateChanged(EventArgs e)
        {
            if (StateChanged != null)
                StateChanged(this, e);
        }

        protected virtual void OnRename(EventArgs e)
        {
            if (Renamed != null)
                Renamed(this, e);
        }

        public void ContentChanged()
        {
            if (!IsDirty)
            {
                IsDirty = true;
                OnStateChanged(EventArgs.Empty);
            }
        }

        public void Save(string content)
        {
            using (StreamWriter sw = new StreamWriter(fileInfo.FullName, false, Encoding.UTF8))
            {
                sw.Write(content);
                sw.Close();
                Refresh();
                Clean();
            }
        }

        public void SaveAs(string content, string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                sw.Write(content);
                sw.Close();
                Refresh();
                Clean();
            }
        }

        /// <summary>
        /// Read text and refresh status
        /// </summary>
        /// <returns></returns>
        public string Open()
        {
            string content = OpenText();
            Refresh();
            Clean();
            return content;
        }

        /// <summary>
        /// Read text
        /// </summary>
        /// <returns></returns>
        internal string OpenText()
        {
            string content;
            using (var stream = fileInfo.OpenText())
            {
                content = stream.ReadToEnd();
                stream.Close();
            }
            return content;
        }

        public IRazorTemplate GetTemplate()
        {
            return new RazorTemplate(this);
        }

        /// <summary>
        /// 取得目錄或檔案的完整路徑
        /// </summary>
        public string FullName
        {
            get { return fileInfo.FullName; }
        }

        /// <summary>
        /// 取得表示目錄完整路徑的字串
        /// </summary>
        public string DirectoryName
        {
            get { return fileInfo.DirectoryName; }
        }

        public void Delete()
        {
            fileInfo.Delete();
        }

        /// <summary>
        /// Location the file in Windows Explorer
        /// </summary>
        public void LookupFile()
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            psi.Arguments = " /select," + fileInfo.FullName;
            System.Diagnostics.Process.Start(psi);
        }

        /// <summary>
        /// 目前檔案或目錄上次被寫入的時間(UTC)是否已改变
        /// </summary>
        /// <returns></returns>
        public bool IsFileChanged()
        {
            fileInfo.Refresh();
            return lastWriteTime != fileInfo.LastWriteTimeUtc;
        }

        /// <summary>
        /// Refresh LastWriteTimeUtc
        /// </summary>
        public void Refresh()
        {
            fileInfo.Refresh();
            lastWriteTime = fileInfo.LastWriteTimeUtc;
        }
    }
}
