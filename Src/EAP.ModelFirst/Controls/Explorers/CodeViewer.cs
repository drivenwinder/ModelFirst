using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using EAP.ModelFirst.Core;
using System.IO;

namespace EAP.ModelFirst.Controls.Explorers
{
    public partial class CodeViewer : EAP.ModelFirst.Controls.Documents.DocumentBase
    {
        static List<CodeViewer> viewers = new List<CodeViewer>();

        public static void Show(IDockForm dockFrom, string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            var viewer = viewers.FirstOrDefault(p => p.File.FullName.Equals(fileInfo.FullName, StringComparison.OrdinalIgnoreCase));
            if (viewer == null)
            {
                viewer = new CodeViewer(fileInfo);
                viewer.FormClosed += (s, e) => { viewers.Remove(viewer); };
                viewers.Add(viewer);
            }
            dockFrom.DockPanel.ActiveDocumentChanged += (s, e) =>
            {
                if (dockFrom.DockPanel.ActiveDocument == viewer)
                {
                    if (viewer.File.LastWriteTime != System.IO.File.GetLastWriteTime(viewer.File.FullName))
                        viewer.LoadContent();
                }
            };
            viewer.Show(dockFrom.DockPanel);
        }

        FileInfo File { get; set; }

        protected CodeViewer(FileInfo file)
        {
            InitializeComponent();
            File = file;
            TabText = File.Name;
            UpdateTexts();
            LoadContent();
        }

        void LoadContent()
        {
            using (var sr = new StreamReader(File.FullName, Encoding.UTF8))
            {
                var content = sr.ReadToEnd();
                txtContent.Text = content;
                sr.Close();
            }
        }
    }
}
