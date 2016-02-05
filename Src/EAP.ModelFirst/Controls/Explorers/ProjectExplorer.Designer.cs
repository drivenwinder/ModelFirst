namespace EAP.ModelFirst.Controls.Explorers
{
    partial class ProjectExplorer
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectExplorer));
            this.view = new EAP.ModelFirst.Controls.ProjectView.ProjectTreeView();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnGenerate = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnView = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // view
            // 
            this.view.AllowDrop = true;
            this.view.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.InputControlStandalone;
            this.view.BorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.InputControlStandalone;
            this.view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.view.DockForm = null;
            this.view.ImageIndex = 0;
            this.view.ItemHeight = 17;
            this.view.ItemStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.ListItem;
            this.view.LabelEdit = true;
            this.view.Location = new System.Drawing.Point(0, 25);
            this.view.Name = "view";
            this.view.SelectedImageIndex = 0;
            this.view.Size = new System.Drawing.Size(292, 242);
            this.view.Sorted = true;
            this.view.TabIndex = 0;
            this.view.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.view_AfterSelect);
            // 
            // toolStrip
            // 
            this.toolStrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.btnGenerate,
            this.btnEdit,
            this.btnView});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.toolStrip.Size = new System.Drawing.Size(292, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = global::EAP.ModelFirst.Properties.Resources.Refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23, 22);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerate.Image = global::EAP.ModelFirst.Properties.Resources.CodeGenerator;
            this.btnGenerate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(23, 22);
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEdit.Image = global::EAP.ModelFirst.Properties.Resources.FormEdit;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(23, 22);
            this.btnEdit.Text = "Edit";
            this.btnEdit.Visible = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnView
            // 
            this.btnView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnView.Image = global::EAP.ModelFirst.Properties.Resources.ViewDiagram;
            this.btnView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(23, 22);
            this.btnView.Text = "View";
            this.btnView.Visible = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // ProjectExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 267);
            this.Controls.Add(this.view);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProjectExplorer";
            this.ShowHint = EAP.Win.UI.DockState.DockRight;
            this.TabText = "Project";
            this.Text = "Project Explorer";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EAP.ModelFirst.Controls.ProjectView.ProjectTreeView view;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripButton btnGenerate;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnView;
    }
}