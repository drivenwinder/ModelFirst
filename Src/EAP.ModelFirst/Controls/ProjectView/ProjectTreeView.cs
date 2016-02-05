using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using EAP.ModelFirst.Controls.Editors.DiagramEditor;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.CSharp;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;
using System.Collections.Generic;

namespace EAP.ModelFirst.Controls.ProjectView
{
    public partial class ProjectTreeView : DocumentItemTreeView, ILocalizable
	{
		Font boldFont, normalFont;

		public ProjectTreeView()
		{
            InitializeComponent();
            TreeView.Controls.Add(lblAddProject); 
            UpdateTexts();
			normalFont = new Font(this.Font, FontStyle.Regular);
            boldFont = new Font(this.Font, FontStyle.Bold);
            AllowDrop = true;
            TreeViewNodeSorter = new ProjectItemSortor();
		}

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);
            foreach (ProjectNode node in Nodes)
            {
                if (e.Node == node || e.Node is IProjectItemNode && (e.Node as IProjectItemNode).ProjectItem.ProjectInfo == node.Project)
                    node.NodeFont = boldFont;
                else
                    node.NodeFont = normalFont;
                node.Text = node.Text; // Little hack to update the text's clipping size
            }
            if (MonoHelper.IsRunningOnMono)
                this.Refresh();
        }

        public void UpdateTexts()
        {
            mnuNewProject.Text = Strings.MenuNewProject;
            mnuOpen.Text = Strings.MenuOpen;
            mnuOpenFile.Text = Strings.MenuOpenFile;
            mnuSaveAll.Text = Strings.MenuSaveAllProjects;
            mnuCloseAll.Text = Strings.MenuCloseAllProjects;

            lblAddProject.Text = Strings.DoubleClickToAddProject;
        }

		public void AddProject(Project project)
		{
			ItemNode projectNode = new ProjectNode(project);
			Nodes.Add(projectNode);
			projectNode.AfterInitialized();

			SelectedNode = projectNode;
            if(!project.Collapsed)
                projectNode.Expand();
			lblAddProject.Visible = false;

			if (project.ItemCount == 1)
			{
				foreach (IProjectItem item in project.Items)
				{
					IDiagram document = item as IDiagram;
                    if (document != null)
                        DockForm.OpenDocument(document);
				}
			}
			if (project.IsUntitled)
			{
				projectNode.EditLabel();
			}
		}

		public void RemoveProject(Project project)
		{
			foreach (ProjectNode projectNode in Nodes)
			{
				if (projectNode.Project == project)
				{
					projectNode.Delete();
					break;
				}
			}
            if (Nodes.Count == 0)
                lblAddProject.Visible = true;
		}

		public void RemoveProjects()
		{
			foreach (ItemNode node in Nodes)
			{
				node.BeforeDelete();
			}
			Nodes.Clear();
			lblAddProject.Visible = true;
		}

        public void LoadProjects(IEnumerable<Project> projects)
		{
            Nodes.Clear();
            foreach (Project project in projects.OrderBy(p=>p.Name))
                AddProject(project);
            lblAddProject.Visible = Nodes.Count == 0;
		}

		private void lblAddProject_DoubleClick(object sender, EventArgs e)
		{
            if (!DockForm.Workspace.HasProject)
            {
                DockForm.Workspace.AddEmptyProject();
            }
		}
        
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);

			normalFont.Dispose();
			boldFont.Dispose();
			normalFont = new Font(this.Font, FontStyle.Regular);
			boldFont = new Font(this.Font, FontStyle.Bold);
		}

        #region Context menu event handlers

        private void contextMenu_Opening(object sender, CancelEventArgs e)
		{
            if (DockForm.Workspace.HasProject)
            {
                mnuSaveAll.Enabled = true;
                mnuCloseAll.Enabled = true;
            }
            else
            {
                mnuSaveAll.Enabled = false;
                mnuCloseAll.Enabled = false;
            }
		}

		private void mnuNewProject_Click(object sender, EventArgs e)
		{
            Project project = DockForm.Workspace.AddEmptyProject(CSharpLanguage.Instance);
            DockForm.Workspace.ActiveProject = project;
		}

		private void mnuOpen_DropDownOpening(object sender, EventArgs e)
		{
			foreach (ToolStripItem item in mnuOpen.DropDownItems)
			{
				if (item.Tag is int)
				{
					int index = (int) item.Tag;

                    if (index < Settings.Default.RecentFiles.Count)
                    {
                        item.Text = Settings.Default.RecentFiles[index];
                        item.Visible = true;
                    }
                    else
                    {
                        item.Visible = false;
                    }
				}
			}

			mnuSepOpenFile.Visible = (Settings.Default.RecentFiles.Count > 0);
		}

		private void mnuOpenFile_Click(object sender, EventArgs e)
		{
            DockForm.Workspace.OpenProject();
		}

		private void OpenRecentFile_Click(object sender, EventArgs e)
		{
			int index = (int) ((ToolStripItem) sender).Tag;
            if (index >= 0 && index < Settings.Default.RecentFiles.Count)
            {
                string fileName = Settings.Default.RecentFiles[index];
                DockForm.Workspace.OpenProject(fileName);
            }
		}

		private void mnuSaveAll_Click(object sender, EventArgs e)
		{
            DockForm.Workspace.SaveAllProjects();
		}

		private void mnuCloseAll_Click(object sender, EventArgs e)
		{
            DockForm.Workspace.RemoveAll();
		}

		#endregion

        #region Node Drag & Drop

        DateTime startTime;
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
                if (e.Button == MouseButtons.Left && node is IProjectItemNode) //只允许拖放IProjectItemNode,ProjectNode不能拖。  
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

                    Project project = null;
                    if (node is ContainerNode)
                        project = (node as ContainerNode).PackageBase.ProjectInfo;
                    else if (node is IProjectItemNode)
                        project = (node as IProjectItemNode).ProjectItem.ProjectInfo;

                    //设置拖放标签Effect状态  
                    if (project != null && node != SelectedNode) //当控件移动到空白处时，设置不可用。  
                    {
                        if ((e.AllowedEffect & DragDropEffects.Move) != 0)
                            e.Effect = DragDropEffects.Move;
                        if(IsChildNode(node))
                            e.Effect = DragDropEffects.None;
                        else if (e.Data.GetDataPresent(typeof(PackageNode)))
                        {
                            var n = (PackageNode)e.Data.GetData(typeof(PackageNode));
                            if (project != n.ProjectItem.ProjectInfo)//判断是否相同Project  
                                e.Effect = DragDropEffects.None;
                        }
                        else if (e.Data.GetDataPresent(typeof(TypeNode)))
                        {
                            var n = (TypeNode)e.Data.GetData(typeof(TypeNode));
                            if (project != n.ProjectItem.ProjectInfo)//判断是否相同Project
                                e.Effect = DragDropEffects.None;
                        }
                        else if (e.Data.GetDataPresent(typeof(DiagramNode)))
                        {
                            var n = (DiagramNode)e.Data.GetData(typeof(DiagramNode));
                            if (project != n.ProjectItem.ProjectInfo)//判断是否相同Project
                                e.Effect = DragDropEffects.None;
                        }
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
                if (e.Data.GetDataPresent(typeof(TypeNode)))
                    node = (TypeNode)e.Data.GetData(typeof(TypeNode));
                else if(e.Data.GetDataPresent(typeof(PackageNode)))
                    node = (PackageNode)e.Data.GetData(typeof(PackageNode));
                else if (e.Data.GetDataPresent(typeof(DiagramNode)))
                    node = (DiagramNode)e.Data.GetData(typeof(DiagramNode));

                if (node != null && node != dragDropTreeNode)
                {
                    IProjectItemNode item = node as IProjectItemNode;
                    try
                    {
                        ContainerNode container = null;

                        if (dragDropTreeNode is ContainerNode)
                            container = (ContainerNode)dragDropTreeNode;
                        else if (dragDropTreeNode.Parent is ContainerNode)
                            container = (ContainerNode)dragDropTreeNode.Parent;

                        if (container != null && container != node.Parent && container.PackageBase.ProjectInfo == item.ProjectItem.ProjectInfo)
                        {
                            item.ProjectItem.Package.Move(item.ProjectItem, container.PackageBase);
                            node.Remove();//从原父节点移除被拖得节点  
                            container.Nodes.Add(node);//添加被拖得节点到新节点下面  
                            if (!container.IsExpanded)
                                container.Expand();//展开节点
                            SelectedNode = node;
                        }
                        else
                        {
                            //TODO may be need to Copy the node to another project.
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
                if (x is ProjectNode)
                    a = "A" + ((ItemNode)x).Text;
                else if(x is DiagramNode)
                    a = "B" + ((ItemNode)x).Text;
                else if (x is PackageNode)
                    a = "C" + ((ItemNode)x).Text;
                else if (x is TypeNode)
                    a = "D" + ((ItemNode)x).Text;

                if (y is ProjectNode)
                    b = "A" + ((ItemNode)y).Text;
                else if (y is DiagramNode)
                    b = "B" + ((ItemNode)y).Text;
                else if (y is PackageNode)
                    b = "C" + ((ItemNode)y).Text;
                else if (y is TypeNode)
                    b = "D" + ((ItemNode)y).Text;
                return a.CompareTo(b);
            }
        }
    }
}
