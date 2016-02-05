namespace EAP.ModelFirst.Controls.Explorers
{
    partial class ErrorList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorList));
            this.lstError = new System.Windows.Forms.ListView();
            this.icon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rownum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.errorNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.source = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstError
            // 
            this.lstError.AutoArrange = false;
            this.lstError.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.icon,
            this.rownum,
            this.message,
            this.description,
            this.errorNumber,
            this.source});
            this.lstError.ContextMenuStrip = this.contextMenuStrip;
            this.lstError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstError.FullRowSelect = true;
            this.lstError.GridLines = true;
            this.lstError.HideSelection = false;
            this.lstError.Location = new System.Drawing.Point(0, 0);
            this.lstError.MultiSelect = false;
            this.lstError.Name = "lstError";
            this.lstError.ShowGroups = false;
            this.lstError.Size = new System.Drawing.Size(992, 306);
            this.lstError.SmallImageList = this.imageList;
            this.lstError.TabIndex = 1;
            this.lstError.UseCompatibleStateImageBehavior = false;
            this.lstError.View = System.Windows.Forms.View.Details;
            this.lstError.DoubleClick += new System.EventHandler(this.lstError_DoubleClick);
            // 
            // icon
            // 
            this.icon.Text = global::EAP.ModelFirst.Properties.Strings.Translator;
            this.icon.Width = 25;
            // 
            // rownum
            // 
            this.rownum.Text = global::EAP.ModelFirst.Properties.Strings.Translator;
            this.rownum.Width = 25;
            // 
            // message
            // 
            this.message.Text = "Message";
            this.message.Width = 200;
            // 
            // description
            // 
            this.description.Text = "Description";
            this.description.Width = 400;
            // 
            // errorNumber
            // 
            this.errorNumber.Text = "Error Number";
            this.errorNumber.Width = 86;
            // 
            // source
            // 
            this.source.Text = "Source";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList.Images.SetKeyName(0, "Warning");
            this.imageList.Images.SetKeyName(1, "Critical");
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem,
            this.copyToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(99, 48);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.viewToolStripMenuItem.Text = "&View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // ErrorList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 306);
            this.Controls.Add(this.lstError);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ErrorList";
            this.ShowHint = EAP.Win.UI.DockState.DockBottomAutoHide;
            this.TabText = "Errors";
            this.Text = "Errors";
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstError;
        private System.Windows.Forms.ColumnHeader message;
        private System.Windows.Forms.ColumnHeader description;
        private System.Windows.Forms.ColumnHeader rownum;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ColumnHeader errorNumber;
        private System.Windows.Forms.ColumnHeader source;
        private System.Windows.Forms.ColumnHeader icon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    }
}