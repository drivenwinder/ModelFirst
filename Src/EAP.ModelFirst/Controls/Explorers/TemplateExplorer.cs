using System.Collections.Generic;
using System.Linq;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Template;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Controls.Documents;
using EAP.ModelFirst.Controls.TemplateView;

namespace EAP.ModelFirst.Controls.Explorers
{
    public partial class TemplateExplorer : ExplorerBase, IPropertyConfigurable
    {
        static TemplateExplorer instance;

        static TemplateExplorer Instance
        {
            get
            {
                if (instance == null)
                    instance = new TemplateExplorer();
                return instance;
            }
        }

        public static void Show(IDockForm dockForm)
        {
            Instance.DockForm = dockForm;
            Instance.Show(dockForm.DockPanel);
        }

        public override IDockForm DockForm
        {
            get { return base.DockForm; }
            set
            {
                view.DockForm = value;
                base.DockForm = value;
            }
        }

        protected TemplateExplorer()
        {
            InitializeComponent();
            UpdateTexts();
        }

        void Default_TemplateFolderChanged(object sender, System.EventArgs e)
        {
            DoLoadTemplates();
        }

        public override void UpdateTexts()
        {
            Text = Strings.TemplateExplorer;
            TabText = Strings.TemplateExplorer;
            base.UpdateTexts();
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            DoLoadTemplates();
            Settings.Default.TemplateFolderChanged += new System.EventHandler(Default_TemplateFolderChanged);
        }

        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            DoLoadTemplates();
        }

        void DoLoadTemplates()
        {
            if (DockForm != null)
            {
                using (StatusBusy s = new StatusBusy(Strings.LoadingTemplate, DockForm))
                {
                    var files = DockForm.DockPanel.Documents
                        .OfType<IDocument>()
                        .Select(p => p.DocumentItem)
                        .OfType<TemplateFile>();
                    view.Load(files);
                }
                DockForm.StatusText = Strings.TemplateLoaded;
            }
        }

        private void btnView_Click(object sender, System.EventArgs e)
        {
            if (view.SelectedNode is FileNode)
                DockForm.OpenDocument(((FileNode)view.SelectedNode).TemplateFile);
            
        }

        private void view_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            btnView.Visible = e.Node is FileNode;
            PropertyWindow.SetPropertyObject(PropertyObject);
        }

        public object PropertyObject
        {
            get
            {
                if (view.SelectedNode is TemplateNode)
                {
                    var node = (TemplateNode)view.SelectedNode;
                    return node.Template;
                }
                if (view.SelectedNode is FolderNode)
                {
                    var node = (FolderNode)view.SelectedNode;
                    return node.TemplateFolder;
                }
                else if (view.SelectedNode is FileNode)
                {
                    var node = (FileNode)view.SelectedNode;
                    return node.TemplateFile;
                }
                return null;
            }
        }
    }
}
