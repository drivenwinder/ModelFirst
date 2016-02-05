namespace EAP.ModelFirst.Controls
{
    partial class DockContent
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
            this.tabPageContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeOtherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPageContextMenuStrip
            // 
            this.tabPageContextMenuStrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tabPageContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.closeOtherToolStripMenuItem});
            this.tabPageContextMenuStrip.Name = "tabContextMenuStrip";
            this.tabPageContextMenuStrip.Size = new System.Drawing.Size(153, 70);
            this.tabPageContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.tabPageContextMenuStrip_Opening);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // closeOtherToolStripMenuItem
            // 
            this.closeOtherToolStripMenuItem.Name = "closeOtherToolStripMenuItem";
            this.closeOtherToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeOtherToolStripMenuItem.Text = "Close &Other";
            this.closeOtherToolStripMenuItem.Click += new System.EventHandler(this.closeOtherToolStripMenuItem_Click);
            // 
            // DockContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Name = "DockContent";
            this.TabText = "DockContent";
            this.Text = "DockContent";
            this.tabPageContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip tabPageContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeOtherToolStripMenuItem;
    }
}