using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using EAP.ModelFirst.Controls.Dialogs;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;
using EAP.ModelFirst.Controls.Documents;

namespace EAP.ModelFirst.Controls.Explorers
{
    public partial class GeneratedFileList : ExplorerBase
    {
        static GeneratedFileList instance;

        static GeneratedFileList Instance
        {
            get
            {
                if (instance == null)
                    instance = new GeneratedFileList();
                return instance;
            }
        }

        public static void Show(IDockForm dockForm)
        {
            Instance.DockForm = dockForm;
            Instance.Show(dockForm.DockPanel);
        }

        protected GeneratedFileList()
        {
            InitializeComponent();
            lstFile.ListBox.DoubleClick += new EventHandler(ListBox_DoubleClick);
            UpdateTexts();
        }

        void ListBox_DoubleClick(object sender, EventArgs e)
        {
            if (lstFile.SelectedItems.Count > 0)
            {
                try
                {
                    var item = lstFile.SelectedItems[0];
                    ViewFile(item.ToString());
                }
                catch (Exception exc)
                {
                    Client.ShowError(exc.ToString());
                }
            }
        }

        public override void UpdateTexts()
        {
            Text = Strings.GeneratedFileList;
            TabText = Strings.GeneratedFileList;
            viewToolStripMenuItem.Text = Strings.MenuOpen;
            openToolStripMenuItem.Text = Strings.MenuOpenFileFolder;
            base.UpdateTexts();
        }

        public static void Clear()
        {
            if (instance == null) return;
            Instance.lstFile.Items.Clear();
        }

        public static void AddFile(string fileName)
        {
            Instance.lstFile.Items.Add(fileName);
        }

        void ViewFile(string fileName)
        {
            CodeViewer.Show(Instance.DockForm, fileName);
        }

        void LookupFile(string fileName)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            psi.Arguments = " /select," + fileName;
            System.Diagnostics.Process.Start(psi);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstFile.SelectedItems.Count > 0)
            {
                try
                {
                    var item = lstFile.SelectedItems[0];
                    LookupFile(item.ToString());
                }
                catch (Exception exc)
                {
                    Client.ShowError(exc.ToString());
                }
            }
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstFile.SelectedItems.Count > 0)
            {
                try
                {
                    var item = lstFile.SelectedItems[0];
                    ViewFile(item.ToString());
                }
                catch (Exception exc)
                {
                    Client.ShowError(exc.ToString());
                }
            }
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            var enabled = lstFile.SelectedItems.Count > 0;
            viewToolStripMenuItem.Enabled = enabled;
            openToolStripMenuItem.Enabled = enabled;
        }
    }
}
