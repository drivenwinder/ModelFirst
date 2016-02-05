using System;
using System.ComponentModel;
using System.Windows.Forms;
using EAP.ModelFirst.Controls.Dialogs;
using EAP.ModelFirst.Properties;
using RazorEngine.Templating;

namespace EAP.ModelFirst.Controls.Explorers
{
    public partial class ErrorList : ExplorerBase
    {
        static ErrorList instance;

        static ErrorList Instance
        {
            get
            {
                if (instance == null)
                    instance = new ErrorList();
                return instance;
            }
        }

        public static void Show(IDockForm dockForm)
        {
            Instance.DockForm = dockForm;
            Instance.Show(dockForm.DockPanel);
        }

        protected ErrorList()
        {
            InitializeComponent();
            UpdateTexts();
        }

        public override void UpdateTexts()
        {
            Text = Strings.ErrorList;
            TabText = Strings.ErrorList;
            viewToolStripMenuItem.Text = Strings.MenuOpen;
            copyToolStripMenuItem.Text = Strings.MenuCopy;
            base.UpdateTexts();
        }

        public static void Clear()
        {
            if (instance == null) return;
            Instance.lstError.Items.Clear();
        }

        public static bool HasError
        {
            get { return Instance.lstError.Items.Count > 0; }
        }

        public static void SetError(Exception exc)
        {
            Instance.Set(exc);
        }

        void Set(Exception exc)
        {
            if (exc.InnerException != null)
                Set(exc.InnerException);
            if (exc is TemplateCompilationException)
            {
                var tce = (TemplateCompilationException)exc;
                foreach (var e in tce.Errors)
                {
                    ListViewItem item = new ListViewItem("");
                    item.ImageKey = e.IsWarning ? "Warning" : "Critical";
                    item.SubItems.Add((lstError.Items.Count + 1).ToString());
                    item.SubItems.Add(exc.Message);
                    item.SubItems.Add(e.ErrorText);
                    item.SubItems.Add(e.ErrorNumber);
                    item.SubItems.Add("{0}. at Line {1} Column {2}".FormatArgs(e.FileName, e.Line, e.Column));
                    var message = "Message:{0}{7}{7}Type:{1}{7}{7}Details:{2}{7}{7}ErrorNumber:{3}{7}{7}FileName:{4} as Line {5} Column {6}"
                        .FormatArgs(exc.Message, e.IsWarning ? "Warning" : "Critical", e.ErrorText,
                        e.ErrorNumber, e.FileName, e.Line, e.Column, Environment.NewLine);
                    item.Tag = message;
                    lstError.Items.Add(item); 
                }
            }
            else
            {
                ListViewItem item = new ListViewItem("");
                item.ImageKey = "Critical";
                item.SubItems.Add((lstError.Items.Count + 1).ToString());
                item.SubItems.Add(exc.Message);
                item.SubItems.Add(exc.StackTrace);
                item.SubItems.Add("");
                item.SubItems.Add(exc.Source);
                var message = "Message:{0}{4}{4}Type:{1}{4}{4}Details:{2}{4}{4}Source:{3}"
                    .FormatArgs(exc.Message, "Critical", exc.StackTrace, exc.Source, Environment.NewLine);
                item.Tag = message;
                lstError.Items.Add(item);
            }
            lstError.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void lstError_DoubleClick(object sender, EventArgs e)
        {
            if (lstError.SelectedItems.Count > 0)
            {
                var item = lstError.SelectedItems[0];
                ViewError(item);
            }
        }

        void ViewError(ListViewItem item)
        {
            TextViewer viewer = new TextViewer(item.Tag.ToSafeString());
            viewer.Text = "Error";
            viewer.Show();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstError.SelectedItems.Count > 0)
            {
                var item = lstError.SelectedItems[0];
                ViewError(item);
            }
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            var enabled = lstError.SelectedItems.Count > 0;
            viewToolStripMenuItem.Enabled = enabled;
            copyToolStripMenuItem.Enabled = enabled;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstError.SelectedItems.Count > 0)
            {
                var item = lstError.SelectedItems[0];
                System.Windows.Forms.Clipboard.SetText(item.Tag.ToSafeString());
            }
        }
    }
}