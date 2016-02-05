using System;
using System.ComponentModel;
using EAP.ModelFirst.Controls.Editors.TypeEditors;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Controls.Documents
{
    public partial class TypeDocument : DocumentBase, IDocument
    {
        TypeEditor editor = null;

        public TypeDocument(TypeBase document, IDockForm dockForm)
        {
            InitializeComponent();
            DockForm = dockForm;
            InitEditor(document);
        }

        void typeBase_StateChanged(object sender, EventArgs e)
        {
            UpdateName();
        }

        void UpdateName()
        {
            Name = editor.TypeBase.Name;
            Text = editor.TypeBase.IsDirty ? editor.TypeBase.Name + "*" : editor.TypeBase.Name;
            TabText = Text;
        }

        void InitEditor(TypeBase typeBase)
        {
            if (typeBase is CompositeType)
            {
                editor = new CompositeTypeEditor().SetCompositeType((CompositeType)typeBase);
                
            }
            else if (typeBase is EnumType)
            {
                editor = new EnumTypeEditor().SetEnumType((EnumType)typeBase);
            }
            else if (typeBase is DelegateType)
            {
                editor = new DelegateTypeEditor().SetDelegateType((DelegateType)typeBase);
            }
            else
                throw new NotSupportedException(typeBase.GetType().FullName);

            Handler.EditHandler = editor;
            editor.Dock = System.Windows.Forms.DockStyle.Fill;
            Controls.Add(editor);
            UpdateName();
            typeBase.Renamed += new EventHandler(typeBase_StateChanged);
            typeBase.StateChanged += new EventHandler(typeBase_StateChanged);
        }

        public IDocumentItem DocumentItem
        {
            get { return editor.TypeBase; }
        }

        public string GetStatus()
        {
            return "";
        }

        protected override void OnClosed(EventArgs e)
        {
            editor.OnClosed(e);
            base.OnClosed(e);
            GC.Collect();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            editor.TypeBase.Renamed -= new EventHandler(typeBase_StateChanged);
            editor.TypeBase.StateChanged -= new EventHandler(typeBase_StateChanged);
            editor.OnClosing(e);
            base.OnClosing(e);
        }

        protected override string GetPersistString()
        {
            return base.GetPersistString() + "|" + editor.TypeBase.Id;
        }

        public static IDocument LoadForm(string id, DocumentManager manager)
        {
            foreach (var p in manager.DockForm.Workspace.Projects)
            {
                IProjectDocument item = p.FindItem(id.ConvertTo<Guid>());
                if (item != null && item is TypeBase)
                {
                    return new TypeDocument((TypeBase)item, manager.DockForm);
                }
            }
            return (IDocument)null;
        }

        public void Save()
        {
            DockForm.Workspace.SaveProject(editor.TypeBase.ProjectInfo);
        }

        public bool IsDirty
        {
            get { return editor.TypeBase.ProjectInfo.IsDirty; }
        }

        public void SaveAs()
        {
            DockForm.Workspace.SaveProjectAs(editor.TypeBase.ProjectInfo);
        }
    }
}
