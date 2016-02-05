namespace EAP.ModelFirst.Controls.Documents
{
    partial class TemplateDocument
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
            this.txtTemplate = new EAP.ModelFirst.Controls.Editors.TemplateEditor();
            this.kryptonContextMenu = new ComponentFactory.Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuItems1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.kcmCopy = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kcmCut = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kcmPaste = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuSeparator1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuSeparator();
            this.kcmComplie = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.kryptonContextMenuSeparator2 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuSeparator();
            this.kcmSelectAll = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.SuspendLayout();
            // 
            // txtTemplate
            // 
            this.txtTemplate.AcceptsTab = true;
            this.txtTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTemplate.Font = new System.Drawing.Font("新宋体", 12F);
            this.txtTemplate.KryptonContextMenu = this.kryptonContextMenu;
            this.txtTemplate.Location = new System.Drawing.Point(0, 0);
            this.txtTemplate.Name = "txtTemplate";
            this.txtTemplate.Size = new System.Drawing.Size(509, 309);
            this.txtTemplate.StateActive.Content.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTemplate.TabIndex = 0;
            this.txtTemplate.Text = global::EAP.ModelFirst.Properties.Strings.Translator;
            this.txtTemplate.WordWrap = false;
            this.txtTemplate.ZoomChanged += new System.EventHandler(this.txtTemplate_MouseHWheel);
            this.txtTemplate.SelectionChanged += new System.EventHandler(this.txtTemplate_SelectionChanged);
            this.txtTemplate.TextChanged += new System.EventHandler(this.txtTemplate_TextChanged);
            // 
            // kryptonContextMenu
            // 
            this.kryptonContextMenu.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItems1});
            this.kryptonContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.kryptonContextMenu_Opening);
            // 
            // kryptonContextMenuItems1
            // 
            this.kryptonContextMenuItems1.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kcmCopy,
            this.kcmCut,
            this.kcmPaste,
            this.kryptonContextMenuSeparator1,
            this.kcmComplie,
            this.kryptonContextMenuSeparator2,
            this.kcmSelectAll});
            // 
            // kcmCopy
            // 
            this.kcmCopy.Image = global::EAP.ModelFirst.Properties.Resources.Copy;
            this.kcmCopy.Text = "&Copy";
            this.kcmCopy.Click += new System.EventHandler(this.kcmCopy_Click);
            // 
            // kcmCut
            // 
            this.kcmCut.Image = global::EAP.ModelFirst.Properties.Resources.Cut;
            this.kcmCut.Text = "Cu&t";
            this.kcmCut.Click += new System.EventHandler(this.kcmCut_Click);
            // 
            // kcmPaste
            // 
            this.kcmPaste.Image = global::EAP.ModelFirst.Properties.Resources.Paste;
            this.kcmPaste.Text = "&Paste";
            this.kcmPaste.Click += new System.EventHandler(this.kcmPaste_Click);
            // 
            // kcmComplie
            // 
            this.kcmComplie.Image = global::EAP.ModelFirst.Properties.Resources.Compile;
            this.kcmComplie.Text = "Comp&lie";
            this.kcmComplie.Click += new System.EventHandler(this.kcmComplie_Click);
            // 
            // kcmSelectAll
            // 
            this.kcmSelectAll.Text = "&Select All";
            this.kcmSelectAll.Click += new System.EventHandler(this.kcmSelectAll_Click);
            // 
            // TemplateDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 309);
            this.Controls.Add(this.txtTemplate);
            this.Name = "TemplateDocument";
            this.TabText = "Model";
            this.Text = "Model";
            this.ResumeLayout(false);

        }

        #endregion

        private EAP.ModelFirst.Controls.Editors.TemplateEditor txtTemplate;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenu kryptonContextMenu;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kcmCopy;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kcmCut;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kcmPaste;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuSeparator kryptonContextMenuSeparator1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kcmComplie;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kcmSelectAll;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuSeparator kryptonContextMenuSeparator2;
    }
}