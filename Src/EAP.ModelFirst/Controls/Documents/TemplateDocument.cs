using System;
using System.Windows.Forms;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Template;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;
using System.ComponentModel;
using EAP.ModelFirst.CodeGenerator;
using EAP.ModelFirst.Controls.Editors;
using EAP.ModelFirst.Controls.Explorers;

namespace EAP.ModelFirst.Controls.Documents
{
    public partial class TemplateDocument : DocumentBase, IDocument, IEditable, IZoomable, ITextFormatable, IPropertyConfigurable
    {
        bool locked = false;
        TemplateFile template;

        public event EventHandler EditStateChanged;

        public event EventHandler ZoomChanged;

        public TemplateDocument(TemplateFile file, IDockForm dockForm)
        {
            InitializeComponent();
            DockForm = dockForm;
            Handler.EditHandler = this;
            Handler.ZoomHandler = this;
            Handler.TextFormatHandler = this;
            Handler.Compile += new EventHandler(Handler_Compile);
            template = file;
            template.Renamed += new EventHandler(template_StateChanged);
            template.StateChanged += new EventHandler(template_StateChanged);
            UpdateName();
            LoadFile();
        }

        void Handler_Compile(object sender, EventArgs e)
        {
            Complie();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var f = DockPanel.FindForm();
            if (f != null)
                f.Activated += new EventHandler(TemplateDocument_Activated);
        }

        protected override void OnClosed(EventArgs e)
        {
            var f = DockPanel.FindForm();
            if (f != null)
                f.Activated -= new EventHandler(TemplateDocument_Activated);
            template.Renamed -= new EventHandler(template_StateChanged);
            template.StateChanged -= new EventHandler(template_StateChanged);
            base.OnClosed(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!e.Cancel && IsDirty)
            {
                DialogResult result = Client.Show(Strings.SaveChangesConfirmation, Strings.Confirmation,
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    Save();
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        void TemplateDocument_Activated(object sender, EventArgs e)
        {
            if (!locked && template.IsFileChanged())
            {
                locked = true;
                DialogResult result = Client.ShowConfirm(Strings.ReloadModifiedFileConfirmation.FormatArgs(template.Name));

                if (result == DialogResult.OK)
                    LoadFile();
                else
                    template.Refresh();
                locked = false;
            }
        }

        void template_StateChanged(object sender, EventArgs e)
        {
            UpdateName();
            OnEditStateChanged(EventArgs.Empty);
        }

        void UpdateName()
        {
            Name = template.Name;
            Text = IsDirty ? template.Name + "*" : template.Name;
            TabText = Text;
        }

        public void LoadFile()
        {
            try
            {
                txtTemplate.Text = template.Open();
                template.Clean();
            }
            catch (Exception exc)
            {
                Client.ShowError(exc.ToString());
            }
        }

        public void Save()
        {
            try
            {
                template.Save(txtTemplate.Text);
            }
            catch (Exception exc)
            {
                Client.ShowError(exc.ToString());
            }
        }

        public bool IsDirty { get { return template.IsDirty; } }

        public IDocumentItem DocumentItem
        {
            get { return template; }
        }

        public string GetStatus()
        {
            return Strings.Ready;
        }

        protected override string GetPersistString()
        {
            return base.GetPersistString() + "|" + template.FullName;
        }

        public static IDocument LoadForm(string id, DocumentManager manager)
        {
            return new TemplateDocument(new TemplateFile(id), manager.DockForm);
        }

        public void SaveAs()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.FileName = template.Name;
                dialog.InitialDirectory = template.DirectoryName;
                dialog.Filter = "Model First Template (*{0})|*{0}".FormatArgs(TemplateFile.Extension);

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        template.SaveAs(txtTemplate.Text, dialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        Client.ShowError(Strings.Error + ": " + ex);
                    }
                }
            }
        }

        protected virtual void OnEditStateChanged(EventArgs e)
        {
            if (EditStateChanged != null)
                EditStateChanged(this, e);
        }

        private void txtTemplate_TextChanged(object sender, EventArgs e)
        {
            template.ContentChanged();
            OnEditStateChanged(EventArgs.Empty);
        }

        private void txtTemplate_SelectionChanged(object sender, EventArgs e)
        {
            OnEditStateChanged(EventArgs.Empty);
        }

        #region IEditable

        public bool IsEditing
        {
            get;
            private set;
        }

        public void BeginEdit()
        {
            IsEditing = true;
        }

        public void EndEdit()
        {
            IsEditing = false;
        }

        public void CancelEdit()
        {

        }

        public bool IsEmpty
        {
            get { return txtTemplate.TextLength == 0; }
        }

        public bool CanUndo
        {
            get { return txtTemplate.CanUndo; }
        }

        public bool CanRedo
        {
            get { return txtTemplate.CanRedo; }
        }

        public bool CanDelete
        {
            get { return txtTemplate.SelectionStart < txtTemplate.TextLength; }
        }

        public bool CanCutToClipboard
        {
            get { return txtTemplate.SelectionLength > 0; }
        }

        public bool CanCopyToClipboard
        {
            get { return txtTemplate.SelectionLength > 0; }
        }

        public bool CanPasteFromClipboard
        {
            get { return true; }
        }

        public void Undo()
        {
            txtTemplate.Undo();
        }

        public void Redo()
        {
            txtTemplate.Redo();
        }

        public void Cut()
        {
            txtTemplate.Cut();
        }

        public void Copy()
        {
            txtTemplate.Copy();
        }

        public void Paste()
        {
            txtTemplate.Paste();
        }

        public void SelectAll()
        {
            txtTemplate.SelectAll();
        }

        public void Delete()
        {
            txtTemplate.Delete();
        }

        #endregion

        protected virtual void OnZoomChanged(EventArgs e)
        {
            if (ZoomChanged != null)
                ZoomChanged(this, e);
        }

        public float Zoom
        {
            get { return txtTemplate.ZoomFactor; }
        }

        public void ChangeZoom(float zoom)
        {
            if (zoom < 0.1)
                zoom = 0.1f;
            else if (zoom > 4)
                zoom = 4;
            txtTemplate.ZoomFactor = zoom;
            OnZoomChanged(EventArgs.Empty);
        }

        public void ZoomIn()
        {
            ChangeZoom(txtTemplate.ZoomFactor + 0.1f);
        }

        public void ZoomOut()
        {
            ChangeZoom(txtTemplate.ZoomFactor - 0.1f);
        }

        public void AutoZoom()
        {
            ChangeZoom(1);
        }

        private void txtTemplate_MouseHWheel(object sender, EventArgs e)
        {
            OnZoomChanged(e);
        }

        private void kcmCopy_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void kcmCut_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void kcmPaste_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void kcmComplie_Click(object sender, EventArgs e)
        {
            Complie();
        }

        void Complie()
        {
            try
            {
                RazorHelper.Compile(template.DirectoryName, txtTemplate.Text, DockForm);
            }
            catch (Exception exc)
            {
                Client.ShowError(exc.ToString());
            }
        }

        public void Indent()
        {
            txtTemplate.Indent();
        }

        public void Outdent()
        {
            txtTemplate.Outdent();
        }

        private void kryptonContextMenu_Opening(object sender, CancelEventArgs e)
        {
            kcmCopy.Enabled = CanCopyToClipboard;
            kcmCut.Enabled = CanCutToClipboard;
            kcmPaste.Enabled = CanPasteFromClipboard;
            kcmSelectAll.Enabled = !IsEmpty;
        }

        private void kcmSelectAll_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

        public bool WordWrap
        {
            get { return txtTemplate.WordWrap; }
            set { txtTemplate.WordWrap = value; }
        }

        public object PropertyObject
        {
            get { return template; }
        }
    }
}
