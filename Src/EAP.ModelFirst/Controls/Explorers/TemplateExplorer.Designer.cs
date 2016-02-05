namespace EAP.ModelFirst.Controls.Explorers
{
    partial class TemplateExplorer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateExplorer));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.view = new EAP.ModelFirst.Controls.TemplateView.TemplateTreeView();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnView = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "folder");
            this.imageList.Images.SetKeyName(1, "file");
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
            this.view.ShowNodeToolTips = true;
            this.view.Size = new System.Drawing.Size(292, 242);
            this.view.Sorted = true;
            this.view.TabIndex = 1;
            this.view.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.view_AfterSelect);
            // 
            // toolStrip
            // 
            this.toolStrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.btnView});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.toolStrip.Size = new System.Drawing.Size(292, 25);
            this.toolStrip.TabIndex = 0;
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
            // btnView
            // 
            this.btnView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnView.Image = global::EAP.ModelFirst.Properties.Resources.CodeWindow;
            this.btnView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(23, 22);
            this.btnView.Text = "View";
            this.btnView.Visible = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // TemplateExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 267);
            this.Controls.Add(this.view);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TemplateExplorer";
            this.ShowHint = EAP.Win.UI.DockState.DockRight;
            this.TabText = "Template";
            this.Text = "Template Explorer";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private TemplateView.TemplateTreeView view;
        private System.Windows.Forms.ToolStripButton btnView;
    }
}