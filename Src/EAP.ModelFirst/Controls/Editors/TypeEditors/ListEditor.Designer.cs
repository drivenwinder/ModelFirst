namespace EAP.ModelFirst.Controls.Editors.TypeEditors
{
    partial class ListEditor
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolMoveUp = new System.Windows.Forms.ToolStripButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.lblItemCaption = new System.Windows.Forms.ToolStripLabel();
            this.txtItem = new System.Windows.Forms.ToolStripTextBox();
            this.btnAccept = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolMoveDown = new System.Windows.Forms.ToolStripButton();
            this.toolDelete = new System.Windows.Forms.ToolStripButton();
            this.lstItems = new System.Windows.Forms.ListView();
            this.value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolMoveUp
            // 
            this.toolMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolMoveUp.Enabled = false;
            this.toolMoveUp.Image = global::EAP.ModelFirst.Properties.Resources.MoveUp;
            this.toolMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolMoveUp.Name = "toolMoveUp";
            this.toolMoveUp.Size = new System.Drawing.Size(23, 22);
            this.toolMoveUp.Text = "Move Up";
            this.toolMoveUp.Click += new System.EventHandler(this.toolMoveUp_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.errorProvider.SetError(this.toolStrip, "Error");
            this.toolStrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.errorProvider.SetIconAlignment(this.toolStrip, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblItemCaption,
            this.txtItem,
            this.btnAccept,
            this.toolStripSeparator,
            this.toolMoveUp,
            this.toolMoveDown,
            this.toolDelete});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(418, 25);
            this.toolStrip.TabIndex = 9;
            this.toolStrip.Text = "toolStrip";
            // 
            // lblItemCaption
            // 
            this.lblItemCaption.Name = "lblItemCaption";
            this.lblItemCaption.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblItemCaption.Size = new System.Drawing.Size(93, 22);
            this.lblItemCaption.Text = "Add Item to List:";
            // 
            // txtItem
            // 
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(200, 25);
            this.txtItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItem_KeyDown);
            // 
            // btnAccept
            // 
            this.btnAccept.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAccept.Image = global::EAP.ModelFirst.Properties.Resources.Accept;
            this.btnAccept.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(23, 22);
            this.btnAccept.Text = "Accept";
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // toolMoveDown
            // 
            this.toolMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolMoveDown.Enabled = false;
            this.toolMoveDown.Image = global::EAP.ModelFirst.Properties.Resources.MoveDown;
            this.toolMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolMoveDown.Name = "toolMoveDown";
            this.toolMoveDown.Size = new System.Drawing.Size(23, 22);
            this.toolMoveDown.Text = "Move Down";
            this.toolMoveDown.Click += new System.EventHandler(this.toolMoveDown_Click);
            // 
            // toolDelete
            // 
            this.toolDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDelete.Enabled = false;
            this.toolDelete.Image = global::EAP.ModelFirst.Properties.Resources.Delete;
            this.toolDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDelete.Name = "toolDelete";
            this.toolDelete.Size = new System.Drawing.Size(23, 22);
            this.toolDelete.Text = "Delete";
            this.toolDelete.Click += new System.EventHandler(this.toolDelete_Click);
            // 
            // lstItems
            // 
            this.lstItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.value});
            this.lstItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstItems.HideSelection = false;
            this.lstItems.Location = new System.Drawing.Point(0, 25);
            this.lstItems.MultiSelect = false;
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(418, 349);
            this.lstItems.TabIndex = 10;
            this.lstItems.UseCompatibleStateImageBehavior = false;
            this.lstItems.View = System.Windows.Forms.View.Details;
            this.lstItems.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstItems_ItemSelectionChanged);
            this.lstItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItem_KeyDown);
            // 
            // value
            // 
            this.value.Text = "Item";
            this.value.Width = 248;
            // 
            // ListEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstItems);
            this.Controls.Add(this.toolStrip);
            this.Name = "ListEditor";
            this.Size = new System.Drawing.Size(418, 374);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripButton toolMoveUp;
        private System.Windows.Forms.ErrorProvider errorProvider;
        protected System.Windows.Forms.ListView lstItems;
        private System.Windows.Forms.ColumnHeader value;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolDelete;
        private System.Windows.Forms.ToolStripButton toolMoveDown;
        private System.Windows.Forms.ToolStripLabel lblItemCaption;
        private System.Windows.Forms.ToolStripTextBox txtItem;
        private System.Windows.Forms.ToolStripButton btnAccept;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
    }
}
