using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EAP.ModelFirst.Controls.Dialogs;
using EAP.ModelFirst.Controls.Documents;
using EAP.ModelFirst.Controls.Editors.DiagramEditor;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Template;
using EAP.Win.UI;

namespace EAP.ModelFirst.Controls
{
    public class DocumentManager
    {
        public event DocumentEventHandler ActiveDocumentChanged;
        public event DocumentEventHandler DocumentAdded;
        public event DocumentEventHandler DocumentRemoved;

        #region Property

        IDockForm dockForm;

        public IDockForm DockForm { get { return dockForm; } }

        List<IDocument> documents;
        public IEnumerable<IDocument> Documents
        {
            get { return documents; }
        }

        public IDocument ActiveDocument
        {
            get { return dockForm.DockPanel.ActiveDocument as IDocument; }
        }

        public bool HasActiveDocument
        {
            get { return ActiveDocument != null; }
        }

        #endregion

        public DocumentManager(IDockForm form)
        {
            dockForm = form;
            documents = new List<IDocument>();
        }

        public void OpenDocument(IDocumentItem item)
        {
            if (item is TypeBase)
            {
                using (TypeBaseDialog dialog = new TypeBaseDialog())
                {
                    dialog.ShowDialog((TypeBase)item);
                }
            }
            else
            {
                IDocument doc = Documents.FirstOrDefault(p => p.DocumentItem == item);
                if (doc != null)
                    Show(doc);
                else
                    Open(CreateDocument(item));
            }
        }

        public IDocument CreateDocument(IDocumentItem item)
        {
            if (item is IDiagram)
                return new DiagramDocument((IDiagram)item, DockForm);            
            else if (item is TemplateFile)
                return new TemplateDocument((TemplateFile)item, DockForm);
            throw new NotSupportedException(item.GetType().Name);
        }

        public void Open(IDocument doc)
        {
            Add(doc);
            Show(doc);
        }

        void Add(IDocument doc)
        {
            if (!Documents.Contains(doc))
            {
                doc.DocumentItem.Closing += new EventHandler(DocumentItem_Closing);
                doc.DockHandler.Form.FormClosed += new System.Windows.Forms.FormClosedEventHandler(Form_FormClosed);
                doc.DockHandler.Form.Activated += new EventHandler(Form_Activated);
                documents.Add(doc);
                OnDocumentAdded(new DocumentEventArgs(doc));
            }
        }

        void DocumentItem_Closing(object sender, EventArgs e)
        {
            var item = sender as IDocumentItem;
            IDocument doc = Documents.FirstOrDefault(p => p.DocumentItem == item);
            if (doc != null)
                doc.DockHandler.Close();
        }

        void Show(IDocument doc)
        {
            doc.DockHandler.Show(dockForm.DockPanel, DockState.Document);
        }

        void Form_Activated(object sender, EventArgs e)
        {
            OnActiveDocumentChanged(new DocumentEventArgs((IDocument)sender));
        }

        void Form_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            IDocument doc = sender as IDocument;
            doc.DockHandler.Form.FormClosed -= new System.Windows.Forms.FormClosedEventHandler(Form_FormClosed);
            doc.DockHandler.Form.Activated -= new EventHandler(Form_Activated);
            doc.DocumentItem.Closing -= new EventHandler(DocumentItem_Closing);
            documents.Remove(doc);
            OnDocumentRemoved(new DocumentEventArgs(doc));
        }

        public IDockContent LoadForm(string persistString)
        {
            try
            {
                if (persistString.IsNotEmpty())
                {
                    string[] persist = persistString.Split('|');
                    var doc = GetType().Assembly.GetType(persist[0])
                        .GetMethod("LoadForm", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)
                        .Invoke(null, new object[] { persist[1], this });
                    if (doc is IDocument)
                        Add((IDocument)doc);
                    return (IDockContent)doc;
                }
            }
            catch
            {
                //eat the error
            }
            return (IDockContent)null;
        }

        protected virtual void OnActiveDocumentChanged(DocumentEventArgs e)
        {
            if (ActiveDocumentChanged != null)
                ActiveDocumentChanged(this, e);
        }

        protected virtual void OnDocumentAdded(DocumentEventArgs e)
        {
            if (DocumentAdded != null)
                DocumentAdded(this, e);
        }

        protected virtual void OnDocumentRemoved(DocumentEventArgs e)
        {
            if (DocumentRemoved != null)
                DocumentRemoved(this, e);
        }
    }
}
