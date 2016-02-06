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
using ICSharpCode.TextEditor.Document;

namespace EAP.ModelFirst.Controls.Documents
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
            viewer.Show(dockFrom.DockPanel);
        }

        FileInfo File { get; set; }

        protected CodeViewer(FileInfo file)
        {
            InitializeComponent();
            txtContent.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("C#");
            File = file;
            TabText = File.Name;
            UpdateTexts();
            LoadContent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var f = DockPanel.FindForm();
            if (f != null)
                f.Activated += f_Activated;
        }
        protected override void OnClosed(EventArgs e)
        {
            var f = DockPanel.FindForm();
            if (f != null)
                f.Activated -= f_Activated;
            base.OnClosed(e);
        }

        void f_Activated(object sender, EventArgs e)
        {
            if (File.LastWriteTime < System.IO.File.GetLastWriteTime(File.FullName))
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
