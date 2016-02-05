namespace EAP.ModelFirst.Controls.Explorers
{
    partial class Generator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Generator));
            this.gbTemplate = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.tableLayoutPanelTemplate = new System.Windows.Forms.TableLayoutPanel();
            this.tvTemplate = new ComponentFactory.Krypton.Toolkit.KryptonTreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.lsbTemplate = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnSelectAll = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnRemoveAll = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSelect = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnRemove = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnGenerateCode = new System.Windows.Forms.ToolStripButton();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.lsbType = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.gbType = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.gbOutput = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtOutput = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.btnBrowse = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.gbTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbTemplate.Panel)).BeginInit();
            this.gbTemplate.Panel.SuspendLayout();
            this.gbTemplate.SuspendLayout();
            this.tableLayoutPanelTemplate.SuspendLayout();
            this.tableLayoutPanelButtons.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbType.Panel)).BeginInit();
            this.gbType.Panel.SuspendLayout();
            this.gbType.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbOutput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbOutput.Panel)).BeginInit();
            this.gbOutput.Panel.SuspendLayout();
            this.gbOutput.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbTemplate
            // 
            this.gbTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTemplate.Location = new System.Drawing.Point(3, 188);
            this.gbTemplate.Name = "gbTemplate";
            // 
            // gbTemplate.Panel
            // 
            this.gbTemplate.Panel.Controls.Add(this.tableLayoutPanelTemplate);
            this.gbTemplate.Panel.Padding = new System.Windows.Forms.Padding(5);
            this.gbTemplate.Size = new System.Drawing.Size(694, 271);
            this.gbTemplate.TabIndex = 9;
            this.gbTemplate.Values.Heading = "Template";
            // 
            // tableLayoutPanelTemplate
            // 
            this.tableLayoutPanelTemplate.ColumnCount = 3;
            this.tableLayoutPanelTemplate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelTemplate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanelTemplate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelTemplate.Controls.Add(this.tvTemplate, 0, 0);
            this.tableLayoutPanelTemplate.Controls.Add(this.lsbTemplate, 2, 0);
            this.tableLayoutPanelTemplate.Controls.Add(this.tableLayoutPanelButtons, 1, 0);
            this.tableLayoutPanelTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelTemplate.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanelTemplate.Name = "tableLayoutPanelTemplate";
            this.tableLayoutPanelTemplate.RowCount = 1;
            this.tableLayoutPanelTemplate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTemplate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 237F));
            this.tableLayoutPanelTemplate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 237F));
            this.tableLayoutPanelTemplate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 237F));
            this.tableLayoutPanelTemplate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 237F));
            this.tableLayoutPanelTemplate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 237F));
            this.tableLayoutPanelTemplate.Size = new System.Drawing.Size(680, 237);
            this.tableLayoutPanelTemplate.TabIndex = 10;
            // 
            // tvTemplate
            // 
            this.tvTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTemplate.ImageIndex = 0;
            this.tvTemplate.ImageList = this.imageList;
            this.tvTemplate.Location = new System.Drawing.Point(3, 3);
            this.tvTemplate.Name = "tvTemplate";
            this.tvTemplate.SelectedImageIndex = 0;
            this.tvTemplate.Size = new System.Drawing.Size(294, 231);
            this.tvTemplate.TabIndex = 1;
            this.tvTemplate.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvTemplate_NodeMouseDoubleClick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "folder");
            this.imageList.Images.SetKeyName(1, "template");
            // 
            // lsbTemplate
            // 
            this.lsbTemplate.DisplayMember = "Text";
            this.lsbTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbTemplate.Location = new System.Drawing.Point(383, 3);
            this.lsbTemplate.Name = "lsbTemplate";
            this.lsbTemplate.Size = new System.Drawing.Size(294, 231);
            this.lsbTemplate.TabIndex = 5;
            this.lsbTemplate.ValueMember = "Value";
            // 
            // tableLayoutPanelButtons
            // 
            this.tableLayoutPanelButtons.ColumnCount = 1;
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelButtons.Controls.Add(this.btnSelectAll, 0, 1);
            this.tableLayoutPanelButtons.Controls.Add(this.btnRemoveAll, 0, 4);
            this.tableLayoutPanelButtons.Controls.Add(this.btnSelect, 0, 2);
            this.tableLayoutPanelButtons.Controls.Add(this.btnRemove, 0, 3);
            this.tableLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelButtons.Location = new System.Drawing.Point(303, 3);
            this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            this.tableLayoutPanelButtons.Padding = new System.Windows.Forms.Padding(10, 18, 10, 9);
            this.tableLayoutPanelButtons.RowCount = 6;
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelButtons.Size = new System.Drawing.Size(74, 231);
            this.tableLayoutPanelButtons.TabIndex = 6;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(13, 31);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(48, 31);
            this.btnSelectAll.TabIndex = 6;
            this.btnSelectAll.Values.Text = ">>";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Location = new System.Drawing.Point(13, 169);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(48, 31);
            this.btnRemoveAll.TabIndex = 9;
            this.btnRemoveAll.Values.Text = "<<";
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(13, 77);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(48, 31);
            this.btnSelect.TabIndex = 7;
            this.btnSelect.Values.Text = ">";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(13, 123);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(48, 31);
            this.btnRemove.TabIndex = 8;
            this.btnRemove.Values.Text = "<";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGenerateCode,
            this.btnRefresh});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStrip.Size = new System.Drawing.Size(700, 25);
            this.toolStrip.TabIndex = 13;
            this.toolStrip.Text = "ToolStrip";
            // 
            // btnGenerateCode
            // 
            this.btnGenerateCode.Enabled = false;
            this.btnGenerateCode.Image = global::EAP.ModelFirst.Properties.Resources.CodeGenerator;
            this.btnGenerateCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerateCode.Name = "btnGenerateCode";
            this.btnGenerateCode.Size = new System.Drawing.Size(74, 22);
            this.btnGenerateCode.Text = "Generate";
            this.btnGenerateCode.Click += new System.EventHandler(this.btnGenerateCode_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::EAP.ModelFirst.Properties.Resources.Refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(66, 22);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lsbType
            // 
            this.lsbType.DisplayMember = "Text";
            this.lsbType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbType.Location = new System.Drawing.Point(5, 5);
            this.lsbType.Name = "lsbType";
            this.lsbType.Size = new System.Drawing.Size(680, 145);
            this.lsbType.TabIndex = 14;
            this.lsbType.ValueMember = "Value";
            // 
            // gbType
            // 
            this.gbType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbType.Location = new System.Drawing.Point(3, 3);
            this.gbType.Name = "gbType";
            // 
            // gbType.Panel
            // 
            this.gbType.Panel.Controls.Add(this.lsbType);
            this.gbType.Panel.Padding = new System.Windows.Forms.Padding(5);
            this.gbType.Size = new System.Drawing.Size(694, 179);
            this.gbType.TabIndex = 15;
            this.gbType.Values.Heading = "Type";
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 700F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.Controls.Add(this.gbTemplate, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.gbType, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.gbOutput, 0, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 4;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 277F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(700, 575);
            this.tableLayoutPanelMain.TabIndex = 16;
            // 
            // gbOutput
            // 
            this.gbOutput.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbOutput.Location = new System.Drawing.Point(3, 465);
            this.gbOutput.Name = "gbOutput";
            // 
            // gbOutput.Panel
            // 
            this.gbOutput.Panel.Controls.Add(this.tableLayoutPanel1);
            this.gbOutput.Panel.Padding = new System.Windows.Forms.Padding(5);
            this.gbOutput.Size = new System.Drawing.Size(694, 65);
            this.gbOutput.TabIndex = 16;
            this.gbOutput.Values.Heading = "Output";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.Controls.Add(this.txtOutput, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnBrowse, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(680, 31);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // txtOutput
            // 
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Location = new System.Drawing.Point(3, 5);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(584, 20);
            this.txtOutput.TabIndex = 6;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowse.Location = new System.Drawing.Point(593, 3);
            this.btnBrowse.Name = "btnBrowse";
            this.tableLayoutPanel1.SetRowSpan(this.btnBrowse, 3);
            this.btnBrowse.Size = new System.Drawing.Size(84, 25);
            this.btnBrowse.TabIndex = 7;
            this.btnBrowse.Values.Text = "Browse...";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // Generator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(700, 600);
            this.ClientSize = new System.Drawing.Size(704, 594);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Generator";
            this.ShowHint = EAP.Win.UI.DockState.Document;
            this.TabText = "Code Generator";
            this.Text = "Code Generator";
            ((System.ComponentModel.ISupportInitialize)(this.gbTemplate.Panel)).EndInit();
            this.gbTemplate.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbTemplate)).EndInit();
            this.gbTemplate.ResumeLayout(false);
            this.tableLayoutPanelTemplate.ResumeLayout(false);
            this.tableLayoutPanelButtons.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbType.Panel)).EndInit();
            this.gbType.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbType)).EndInit();
            this.gbType.ResumeLayout(false);
            this.tableLayoutPanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbOutput.Panel)).EndInit();
            this.gbOutput.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gbOutput)).EndInit();
            this.gbOutput.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox gbTemplate;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnRemoveAll;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnRemove;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSelect;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSelectAll;
        private ComponentFactory.Krypton.Toolkit.KryptonTreeView tvTemplate;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox lsbTemplate;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnGenerateCode;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox lsbType;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox gbType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox gbOutput;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnBrowse;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtOutput;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTemplate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}