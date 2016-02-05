using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using EAP.Collections;
using EAP.ModelFirst.CodeGenerator;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Template;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;
using EAP.ModelFirst.Core.Project;

namespace EAP.ModelFirst.Controls.Explorers
{
    public partial class Generator : ExplorerBase
    {
        static Generator instance;

        static Generator Instance
        {
            get
            {
                if (instance == null)
                    instance = new Generator();
                return instance;
            }
        }

        protected override string GetPersistString()
        {
            var persist = typeof(Generator) + "|";
            if (types == null || !types.Any())
                return persist;
            foreach (var t in types)
                persist += t.Id.ToString() + ",";
            return persist.TrimEnd(',');
        }

        public new static Generator LoadForm(string ids, DocumentManager manager)
        {
            Instance.DockForm = manager.DockForm;
            var types = new List<TypeBase>();
            foreach (var id in ids.Split(','))
            {
                foreach (var p in manager.DockForm.Workspace.Projects)
                {
                    IProjectDocument item = p.FindItem(id.ConvertTo<Guid>());
                    if (item != null && item is TypeBase)
                        types.Add((TypeBase)item);
                }
            }
            Instance.types = types;
            Instance.UpdateTypes();
            Instance.UpdateButton();
            return Instance;
        }

        public static void SetDockForm(IDockForm dockForm)
        {
            Instance.DockForm = dockForm;
        }

        public static void Show(IEnumerable<TypeBase> t)
        {
            if (t == null)
                Instance.types = new TypeBase[0];
            else
                Instance.types = t.ToArray();
            Instance.UpdateTypes();
            Instance.UpdateButton();
            Instance.Show(Instance.DockForm.DockPanel);
        }

        protected Generator()
        {
            InitializeComponent();
            var font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            tvTemplate.StateNormal.Node.Content.ShortText.Font = font;
            tvTemplate.StateCommon.Node.Content.ShortText.Font = font;
            tvTemplate.StateDisabled.Node.Content.ShortText.Font = font;
            tvTemplate.StatePressed.Node.Content.ShortText.Font = font;
            tvTemplate.StatePressed.Node.Content.ShortText.Font = font;
            lsbTemplate.ListBox.DoubleClick += new EventHandler(ListBox_DoubleClick);
            tvTemplate.Nodes.Add(templateNode);
            UpdateTexts();
        }

        public override void UpdateTexts()
        {
            Text = Strings.CodeGenerator;
            TabText = Strings.CodeGenerator;
            base.UpdateTexts();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtOutput.Text = Settings.Default.OutputPath;
            LoadTemplates();
            LoadSelectedTemplates();
            UpdateButton();
            DockForm.FormClosing += new FormClosingEventHandler(DockForm_FormClosing);
        }

        void DockForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSetings();
        }

        void SaveSetings()
        {
            if (Settings.Default.OutputPath != txtOutput.Text)
                Settings.Default.OutputPath = txtOutput.Text;

            Settings.Default.SelectedTemplates.Clear();

            foreach (var item in lsbTemplate.Items.Cast<ValueTextPair>())
            {
                Settings.Default.SelectedTemplates.Add(((TemplateFile)item.Value).FullName);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            SaveSetings();
        }

        IEnumerable<TypeBase> types;
        TreeNode templateNode = new TreeNode(Strings.Templates);

        void UpdateButton()
        {
            btnGenerateCode.Enabled = types != null && types.Any() && lsbTemplate.Items.Count > 0;
        }

        void UpdateTypes()
        {
            lsbType.Items.Clear();
            foreach (var t in types)
            {
                lsbType.Items.Add(t.GetPackageName(trimEnd: false) + t.Name);
            }
        }

        void LoadSelectedTemplates()
        {
            var dir = Settings.Default.GetTemplateFolder();
            foreach (var item in Settings.Default.SelectedTemplates)
            {
                if (item.StartsWith(dir.FullName) && File.Exists(item))
                {
                    lsbTemplate.Items.Add(new ValueTextPair()
                    {
                        Value = new TemplateFile(item),
                        Text = item.Substring(dir.FullName.Length + 1)
                    });
                }
            }
        }

        void LoadTemplates()
        {
            var dir = Settings.Default.GetTemplateFolder();
            templateNode.Nodes.Clear();
            LoadTemplates(templateNode, dir);
            tvTemplate.ExpandAll();
        }

        void LoadTemplates(TreeNode node, DirectoryInfo dir)
        {
            foreach (var d in dir.GetDirectories())
            {
                if ((d.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    continue;
                TreeNode folder = new TreeNode(d.Name);
                node.Nodes.Add(folder);
                LoadTemplates(folder, d);
            }
            foreach (var f in dir.GetFiles("*" + TemplateFile.Extension))
            {
                TreeNode fileNode = new TreeNode(f.Name);
                fileNode.ImageKey = "template";
                fileNode.SelectedImageKey = "template";
                fileNode.Tag = new TemplateFile(f);
                node.Nodes.Add(fileNode);
            }
        }

        void AddTemplate(TreeNode node)
        {
            if (node == null) return;
            var f = node.Tag as TemplateFile;
            if (f != null && !IsTemplateSelected(f))
                lsbTemplate.Items.Add(new ValueTextPair(f, GetPath(node)));
            foreach (TreeNode n in node.Nodes)
                AddTemplate(n);
            UpdateButton();
        }

        string GetPath(TreeNode node)
        {
            return node.FullPath.Substring(templateNode.Text.Length + 1);
        }

        bool IsTemplateSelected(TemplateFile f)
        {
            return lsbTemplate.Items.Cast<ValueTextPair>()
                .Any(p => ((TemplateFile)p.Value).FullName == f.FullName);
        }

        private void btnGenerateCode_Click(object sender, EventArgs e)
        {
            try
            {
                var templates = lsbTemplate.Items.Cast<ValueTextPair>()
                    .Select(p => (TemplateFile)p.Value);
                foreach (var t in types)
                {
                    if (t.ProjectInfo != null && t.ProjectInfo.IsDirty)
                        DockForm.Workspace.SaveProject(t.ProjectInfo);
                }
                RazorHelper.GenerateCode(types, templates, txtOutput.Text, DockForm);
            }
            catch (Exception exc)
            {
                Client.ShowInfo(exc.ToString());
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTemplates();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            AddTemplate(templateNode);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            AddTemplate(tvTemplate.SelectedNode);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            lsbTemplate.Items.Remove(lsbTemplate.SelectedItem);
            UpdateButton();
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            lsbTemplate.Items.Clear();
            UpdateButton();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var r = folderBrowserDialog.ShowDialog();
            if (r == System.Windows.Forms.DialogResult.OK)
            {
                txtOutput.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void tvTemplate_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            AddTemplate(e.Node);
        }

        void ListBox_DoubleClick(object sender, EventArgs e)
        {
            lsbTemplate.Items.Remove(lsbTemplate.SelectedItem);
            UpdateButton();
        }
    }
}
