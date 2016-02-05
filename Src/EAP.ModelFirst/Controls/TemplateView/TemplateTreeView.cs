using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComponentFactory.Krypton.Toolkit;
using System.Windows.Forms;
using System.ComponentModel;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Properties;
using System.IO;
using EAP.ModelFirst.Core.Template;
using System.Collections;
using System.Drawing;
using EAP.ModelFirst.Utils;

namespace EAP.ModelFirst.Controls.TemplateView
{
    public class TemplateTreeView : DocumentItemTreeView
    {
        private IContainer components;

        public TemplateTreeView()
        {
            InitializeComponent();
            AllowDrop = true;
            TreeViewNodeSorter = new ProjectItemSortor();
        }

        public void Load(IEnumerable<TemplateFile> openedFiles)
        {
            var dir = Settings.Default.GetTemplateFolder();
            TemplateNode root = new TemplateNode(new TemplateFolder(dir));
            root.ToolTipText = dir.FullName;
            Nodes.Clear();
            Nodes.Add(root);
            LoadFiles(root, dir, openedFiles);
            ExpandAll();
        }

        void LoadFiles(TreeNode node, DirectoryInfo dir, IEnumerable<TemplateFile> openedFiles)
        {
            foreach (var d in dir.GetDirectories())
            {
                if ((d.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    continue;
                FolderNode folder = new FolderNode(new TemplateFolder(d));
                node.Nodes.Add(folder);
                LoadFiles(folder, d, openedFiles);
            }
            foreach (var f in dir.GetFiles("*" + TemplateFile.Extension))
            {
                var file = openedFiles.FirstOrDefault(p => p.FullName == f.FullName);
                if(file == null)
                    file = new TemplateFile(f);
                FileNode fileNode = new FileNode(file);
                node.Nodes.Add(fileNode);
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateTreeView));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "templateGear");
            this.imageList.Images.SetKeyName(1, "folder");
            this.imageList.Images.SetKeyName(2, "template");
            // 
            // TemplateTreeView
            // 
            this.ImageIndex = 0;
            this.ImageList = this.imageList;
            this.SelectedImageIndex = 0;
            this.ShowNodeToolTips = true;
            this.ResumeLayout(false);

        }


        #region Node Drag & Drop

        DateTime startTime;
        private ImageList imageList;
        ItemNode dragDropTreeNode;

        void ExpandNode(TreeNode tn)
        {
            if (tn != null)
            {
                if (dragDropTreeNode != tn) //移动到新的节点  
                {
                    if (tn.Nodes.Count > 0 && tn.IsExpanded == false)
                        startTime = DateTime.Now;//设置新的起始时间  
                }
                else if (tn.Nodes.Count > 0 && tn.IsExpanded == false && startTime != DateTime.MinValue)
                {
                    TimeSpan ts = DateTime.Now - this.startTime;
                    if (ts.TotalMilliseconds >= 1000) //一秒  
                    {
                        tn.Expand();
                        startTime = DateTime.MinValue;
                    }
                }
            }
        }

        bool IsChildNode(TreeNode tn)
        {
            TreeNode n = tn;
            while (n.Parent != null)
            {
                n = n.Parent;
                if (n == SelectedNode)
                    return true;
            }
            return false;
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            base.OnItemDrag(e);
            //开始拖动操作事件  
            TreeNode node = e.Item as TreeNode;
            if (node != null)
            {
                SelectedNode = node;
                if (e.Button == MouseButtons.Left && node.Parent != null)
                    DoDragDrop(node, DragDropEffects.Move | DragDropEffects.Link);
            }
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
            e.Effect = DragDropEffects.Move;
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);
            try
            {
                //当光标悬停在 TreeView 控件上时，展开该控件中的 TreeNode  
                Point p = PointToClient(Control.MousePosition);
                TreeNode node = GetNodeAt(p);
                if (node != null)
                {
                    ExpandNode(node);

                    //设置拖放标签Effect状态  
                    if (node != SelectedNode) //当控件移动到空白处时，设置不可用。  
                    {
                        if ((e.AllowedEffect & DragDropEffects.Move) != 0)
                            e.Effect = DragDropEffects.Move;
                        if (IsChildNode(node))//判断是否拖到了子项  
                            e.Effect = DragDropEffects.None;
                    }
                    else
                        e.Effect = DragDropEffects.None;
                }
                else
                    e.Effect = DragDropEffects.None;

                //设置拖放目标TreeNode的背景色  
                if (e.Effect == DragDropEffects.None)
                {
                    if (dragDropTreeNode != null) //取消被放置的节点高亮显示  
                    {
                        dragDropTreeNode.BackColor = GetNormalBackColor();
                        dragDropTreeNode.ForeColor = GetNormalForeColor();
                        dragDropTreeNode = null;
                    }
                }
                else if (node != null)
                {
                    if (dragDropTreeNode == null)
                    {
                        dragDropTreeNode = node as ItemNode;//设置为新的节点  
                        dragDropTreeNode.BackColor = GetTrackingBackColor();
                        dragDropTreeNode.ForeColor = GetTrackingForeColor();
                    }
                    else if (dragDropTreeNode != node)
                    {
                        dragDropTreeNode.BackColor = GetNormalBackColor();//取消上一个被放置的节点高亮显示  
                        dragDropTreeNode.ForeColor = GetNormalForeColor();
                        dragDropTreeNode = node as ItemNode;//设置为新的节点  
                        dragDropTreeNode.BackColor = GetTrackingBackColor();
                        dragDropTreeNode.ForeColor = GetTrackingForeColor();
                    }
                }
            }
            catch (Exception exc)
            {
                Client.ShowInfo(exc.Message);
            }
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);
            if (dragDropTreeNode != null)
            {
                ItemNode node = null;
                if (e.Data.GetDataPresent(typeof(FolderNode)))
                    node = (FolderNode)e.Data.GetData(typeof(FolderNode));
                else if (e.Data.GetDataPresent(typeof(FileNode)))
                    node = (FileNode)e.Data.GetData(typeof(FileNode));

                if (node != null && node != dragDropTreeNode)
                {
                    try
                    {
                        FolderNode container = null;
                        if (dragDropTreeNode is FolderNode)
                            container = (FolderNode)dragDropTreeNode;
                        else if (dragDropTreeNode.Parent is FolderNode)
                            container = (FolderNode)dragDropTreeNode.Parent;

                        if (container != null && container != node.Parent)
                        {
                            if (node is FolderNode)
                                ((FolderNode)node).TemplateFolder.MoveTo(container.TemplateFolder);
                            else if (node is FileNode)
                                ((FileNode)node).TemplateFile.MoveTo(container.TemplateFolder);

                            node.Remove();//从原父节点移除被拖得节点  
                            container.Nodes.Add(node);//添加被拖得节点到新节点下面

                            if (!container.IsExpanded)
                                container.Expand();//展开节点
                            SelectedNode = node;
                        }
                    }
                    catch (Exception exc)
                    {
                        Client.ShowInfo(exc.Message);
                    }
                }
                //取消被放置的节点高亮显示  
                dragDropTreeNode.BackColor = GetNormalBackColor();
                dragDropTreeNode.ForeColor = GetNormalForeColor();
                dragDropTreeNode = null;
            }
        }

        protected override void OnDragLeave(EventArgs e)
        {
            base.OnDragLeave(e);
            if (dragDropTreeNode != null) //在按下{ESC}，取消被放置的节点高亮显示  
            {
                dragDropTreeNode.BackColor = GetNormalBackColor();
                dragDropTreeNode.ForeColor = GetNormalForeColor();
                dragDropTreeNode = null;
            }
        }

        #endregion

        class ProjectItemSortor : IComparer
        {
            public int Compare(object x, object y)
            {
                var a = string.Empty;
                var b = string.Empty;
                if (x is FolderNode)
                    a = "A" + ((ItemNode)x).Text;
                else if (x is FileNode)
                    a = "B" + ((ItemNode)x).Text;

                if (y is FolderNode)
                    b = "A" + ((ItemNode)y).Text;
                else if (y is FileNode)
                    b = "B" + ((ItemNode)y).Text;
                return a.CompareTo(b);
            }
        }
    }
}
