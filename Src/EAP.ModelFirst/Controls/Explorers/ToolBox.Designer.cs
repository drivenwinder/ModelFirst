namespace EAP.ModelFirst.Controls.Explorers
{
    partial class ToolBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolBox));
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView.LabelWrap = false;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.ShowItemToolTips = true;
            this.listView.Size = new System.Drawing.Size(406, 346);
            this.listView.SmallImageList = this.imageList;
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.VirtualListSize = 1;
            this.listView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listView_ItemDrag);
            // 
            // columnHeader
            // 
            this.columnHeader.Width = 100;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "folder");
            this.imageList.Images.SetKeyName(1, "aggregation");
            this.imageList.Images.SetKeyName(2, "association");
            this.imageList.Images.SetKeyName(3, "class");
            this.imageList.Images.SetKeyName(4, "commentRel");
            this.imageList.Images.SetKeyName(5, "composition");
            this.imageList.Images.SetKeyName(6, "dependency");
            this.imageList.Images.SetKeyName(7, "interface");
            this.imageList.Images.SetKeyName(8, "nesting");
            this.imageList.Images.SetKeyName(9, "comment");
            this.imageList.Images.SetKeyName(10, "delegate");
            this.imageList.Images.SetKeyName(11, "enum");
            this.imageList.Images.SetKeyName(12, "structure");
            this.imageList.Images.SetKeyName(13, "generalization");
            this.imageList.Images.SetKeyName(14, "realization");
            // 
            // ToolBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 346);
            this.Controls.Add(this.listView);
            this.DockAreas = ((EAP.Win.UI.DockAreas)(((((EAP.Win.UI.DockAreas.Float | EAP.Win.UI.DockAreas.DockLeft) 
            | EAP.Win.UI.DockAreas.DockRight) 
            | EAP.Win.UI.DockAreas.DockTop) 
            | EAP.Win.UI.DockAreas.DockBottom)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ToolBox";
            this.ShowHint = EAP.Win.UI.DockState.DockLeft;
            this.TabText = "Tool Box";
            this.Text = "Tool Box";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeader;
        private System.Windows.Forms.ImageList imageList;

    }
}