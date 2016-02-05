namespace EAP.ModelFirst.Controls.Dialogs
{
    public partial class OptionsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tabOptions = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.grpTemplate = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelTemplate = new System.Windows.Forms.TableLayoutPanel();
            this.btnBrowser = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lblSavePath = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtSavePath = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.grpProject = new System.Windows.Forms.GroupBox();
            this.btnClearRecents = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.chkRememberOpenProjects = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.grpGeneral = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelGeneral = new System.Windows.Forms.TableLayoutPanel();
            this.cboLanguage = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.lblLanguage = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.tabStyle = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelStyle = new System.Windows.Forms.TableLayoutPanel();
            this.btnClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.stylePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.cboStyles = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.btnLoad = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tabDiagram = new System.Windows.Forms.TabPage();
            this.grpShowChevron = new System.Windows.Forms.GroupBox();
            this.radChevronAlways = new System.Windows.Forms.RadioButton();
            this.radChevronAsNeeded = new System.Windows.Forms.RadioButton();
            this.radChevronNever = new System.Windows.Forms.RadioButton();
            this.grpUseClearType = new System.Windows.Forms.GroupBox();
            this.chkClearTypeForImages = new System.Windows.Forms.CheckBox();
            this.radClearTypeAlways = new System.Windows.Forms.RadioButton();
            this.radClearTypeWhenZoomed = new System.Windows.Forms.RadioButton();
            this.radClearTypeNever = new System.Windows.Forms.RadioButton();
            this.chkUsePrecisionSnapping = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.tabOptions.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.grpTemplate.SuspendLayout();
            this.tableLayoutPanelTemplate.SuspendLayout();
            this.grpProject.SuspendLayout();
            this.grpGeneral.SuspendLayout();
            this.tableLayoutPanelGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboLanguage)).BeginInit();
            this.tabStyle.SuspendLayout();
            this.tableLayoutPanelStyle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboStyles)).BeginInit();
            this.tabDiagram.SuspendLayout();
            this.grpShowChevron.SuspendLayout();
            this.grpUseClearType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(254, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Values.Text = "Cancel";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.AutoSize = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(173, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 24);
            this.btnOK.TabIndex = 1;
            this.btnOK.Values.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.tabGeneral);
            this.tabOptions.Controls.Add(this.tabStyle);
            this.tabOptions.Controls.Add(this.tabDiagram);
            this.tabOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOptions.Location = new System.Drawing.Point(5, 5);
            this.tabOptions.Multiline = true;
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.SelectedIndex = 0;
            this.tabOptions.Size = new System.Drawing.Size(332, 338);
            this.tabOptions.TabIndex = 4;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.grpTemplate);
            this.tabGeneral.Controls.Add(this.grpProject);
            this.tabGeneral.Controls.Add(this.grpGeneral);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(324, 312);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // grpTemplate
            // 
            this.grpTemplate.AutoSize = true;
            this.grpTemplate.Controls.Add(this.tableLayoutPanelTemplate);
            this.grpTemplate.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpTemplate.Location = new System.Drawing.Point(3, 132);
            this.grpTemplate.Name = "grpTemplate";
            this.grpTemplate.Size = new System.Drawing.Size(318, 50);
            this.grpTemplate.TabIndex = 9;
            this.grpTemplate.TabStop = false;
            this.grpTemplate.Text = "Template";
            // 
            // tableLayoutPanelTemplate
            // 
            this.tableLayoutPanelTemplate.AutoSize = true;
            this.tableLayoutPanelTemplate.ColumnCount = 3;
            this.tableLayoutPanelTemplate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelTemplate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTemplate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelTemplate.Controls.Add(this.btnBrowser, 2, 0);
            this.tableLayoutPanelTemplate.Controls.Add(this.lblSavePath, 0, 0);
            this.tableLayoutPanelTemplate.Controls.Add(this.txtSavePath, 1, 0);
            this.tableLayoutPanelTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelTemplate.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanelTemplate.Name = "tableLayoutPanelTemplate";
            this.tableLayoutPanelTemplate.RowCount = 1;
            this.tableLayoutPanelTemplate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTemplate.Size = new System.Drawing.Size(312, 30);
            this.tableLayoutPanelTemplate.TabIndex = 10;
            // 
            // btnBrowser
            // 
            this.btnBrowser.AutoSize = true;
            this.btnBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBrowser.Location = new System.Drawing.Point(275, 3);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(34, 24);
            this.btnBrowser.TabIndex = 2;
            this.btnBrowser.Values.Text = "...";
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // lblSavePath
            // 
            this.lblSavePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSavePath.Location = new System.Drawing.Point(3, 3);
            this.lblSavePath.Name = "lblSavePath";
            this.lblSavePath.Size = new System.Drawing.Size(64, 24);
            this.lblSavePath.TabIndex = 0;
            this.lblSavePath.Values.Text = "Save Path";
            // 
            // txtSavePath
            // 
            this.txtSavePath.Location = new System.Drawing.Point(73, 3);
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.Size = new System.Drawing.Size(196, 20);
            this.txtSavePath.TabIndex = 1;
            // 
            // grpProject
            // 
            this.grpProject.Controls.Add(this.btnClearRecents);
            this.grpProject.Controls.Add(this.chkRememberOpenProjects);
            this.grpProject.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpProject.Location = new System.Drawing.Point(3, 50);
            this.grpProject.Name = "grpProject";
            this.grpProject.Size = new System.Drawing.Size(318, 82);
            this.grpProject.TabIndex = 8;
            this.grpProject.TabStop = false;
            this.grpProject.Text = "Project";
            // 
            // btnClearRecents
            // 
            this.btnClearRecents.Location = new System.Drawing.Point(11, 44);
            this.btnClearRecents.Name = "btnClearRecents";
            this.btnClearRecents.Size = new System.Drawing.Size(171, 24);
            this.btnClearRecents.TabIndex = 6;
            this.btnClearRecents.Values.Text = "Clear recent files list";
            this.btnClearRecents.Click += new System.EventHandler(this.btnClearRecents_Click);
            // 
            // chkRememberOpenProjects
            // 
            this.chkRememberOpenProjects.Location = new System.Drawing.Point(11, 18);
            this.chkRememberOpenProjects.Name = "chkRememberOpenProjects";
            this.chkRememberOpenProjects.Size = new System.Drawing.Size(197, 20);
            this.chkRememberOpenProjects.TabIndex = 5;
            this.chkRememberOpenProjects.Values.Text = "Remember last opened projects";
            // 
            // grpGeneral
            // 
            this.grpGeneral.AutoSize = true;
            this.grpGeneral.Controls.Add(this.tableLayoutPanelGeneral);
            this.grpGeneral.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpGeneral.Location = new System.Drawing.Point(3, 3);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(318, 47);
            this.grpGeneral.TabIndex = 0;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // tableLayoutPanelGeneral
            // 
            this.tableLayoutPanelGeneral.AutoSize = true;
            this.tableLayoutPanelGeneral.ColumnCount = 2;
            this.tableLayoutPanelGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelGeneral.Controls.Add(this.cboLanguage, 1, 0);
            this.tableLayoutPanelGeneral.Controls.Add(this.lblLanguage, 0, 0);
            this.tableLayoutPanelGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelGeneral.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanelGeneral.Name = "tableLayoutPanelGeneral";
            this.tableLayoutPanelGeneral.RowCount = 1;
            this.tableLayoutPanelGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelGeneral.Size = new System.Drawing.Size(312, 27);
            this.tableLayoutPanelGeneral.TabIndex = 10;
            // 
            // cboLanguage
            // 
            this.cboLanguage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLanguage.DropDownWidth = 285;
            this.cboLanguage.Location = new System.Drawing.Point(73, 3);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size(239, 21);
            this.cboLanguage.TabIndex = 10;
            // 
            // lblLanguage
            // 
            this.lblLanguage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLanguage.Location = new System.Drawing.Point(3, 3);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(64, 21);
            this.lblLanguage.TabIndex = 11;
            this.lblLanguage.Values.Text = "Language";
            // 
            // tabStyle
            // 
            this.tabStyle.Controls.Add(this.tableLayoutPanelStyle);
            this.tabStyle.Location = new System.Drawing.Point(4, 22);
            this.tabStyle.Name = "tabStyle";
            this.tabStyle.Padding = new System.Windows.Forms.Padding(3);
            this.tabStyle.Size = new System.Drawing.Size(324, 312);
            this.tabStyle.TabIndex = 1;
            this.tabStyle.Text = "Style";
            this.tabStyle.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelStyle
            // 
            this.tableLayoutPanelStyle.ColumnCount = 3;
            this.tableLayoutPanelStyle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelStyle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelStyle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelStyle.Controls.Add(this.btnClear, 0, 2);
            this.tableLayoutPanelStyle.Controls.Add(this.stylePropertyGrid, 0, 0);
            this.tableLayoutPanelStyle.Controls.Add(this.cboStyles, 0, 1);
            this.tableLayoutPanelStyle.Controls.Add(this.btnLoad, 1, 2);
            this.tableLayoutPanelStyle.Controls.Add(this.btnSave, 2, 2);
            this.tableLayoutPanelStyle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelStyle.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelStyle.Name = "tableLayoutPanelStyle";
            this.tableLayoutPanelStyle.RowCount = 3;
            this.tableLayoutPanelStyle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelStyle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelStyle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelStyle.Size = new System.Drawing.Size(318, 306);
            this.tableLayoutPanelStyle.TabIndex = 6;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(78, 279);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 24);
            this.btnClear.TabIndex = 4;
            this.btnClear.Values.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // stylePropertyGrid
            // 
            this.stylePropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelStyle.SetColumnSpan(this.stylePropertyGrid, 3);
            this.stylePropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.stylePropertyGrid.Name = "stylePropertyGrid";
            this.stylePropertyGrid.Size = new System.Drawing.Size(312, 243);
            this.stylePropertyGrid.TabIndex = 0;
            this.stylePropertyGrid.ToolbarVisible = false;
            // 
            // cboStyles
            // 
            this.cboStyles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelStyle.SetColumnSpan(this.cboStyles, 3);
            this.cboStyles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStyles.DropDownWidth = 312;
            this.cboStyles.FormattingEnabled = true;
            this.cboStyles.Location = new System.Drawing.Point(3, 252);
            this.cboStyles.Name = "cboStyles";
            this.cboStyles.Size = new System.Drawing.Size(312, 21);
            this.cboStyles.TabIndex = 1;
            this.cboStyles.SelectedIndexChanged += new System.EventHandler(this.cboStyles_SelectedIndexChanged);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(159, 279);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 24);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Values.Text = "Load";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(240, 279);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 24);
            this.btnSave.TabIndex = 3;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabDiagram
            // 
            this.tabDiagram.Controls.Add(this.grpShowChevron);
            this.tabDiagram.Controls.Add(this.grpUseClearType);
            this.tabDiagram.Controls.Add(this.chkUsePrecisionSnapping);
            this.tabDiagram.Location = new System.Drawing.Point(4, 22);
            this.tabDiagram.Name = "tabDiagram";
            this.tabDiagram.Padding = new System.Windows.Forms.Padding(3);
            this.tabDiagram.Size = new System.Drawing.Size(324, 312);
            this.tabDiagram.TabIndex = 3;
            this.tabDiagram.Text = "Diagram";
            this.tabDiagram.UseVisualStyleBackColor = true;
            // 
            // grpShowChevron
            // 
            this.grpShowChevron.Controls.Add(this.radChevronAlways);
            this.grpShowChevron.Controls.Add(this.radChevronAsNeeded);
            this.grpShowChevron.Controls.Add(this.radChevronNever);
            this.grpShowChevron.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpShowChevron.Location = new System.Drawing.Point(3, 127);
            this.grpShowChevron.Name = "grpShowChevron";
            this.grpShowChevron.Size = new System.Drawing.Size(318, 83);
            this.grpShowChevron.TabIndex = 5;
            this.grpShowChevron.TabStop = false;
            this.grpShowChevron.Text = "groupBox1";
            // 
            // radChevronAlways
            // 
            this.radChevronAlways.AutoSize = true;
            this.radChevronAlways.Location = new System.Drawing.Point(15, 18);
            this.radChevronAlways.Name = "radChevronAlways";
            this.radChevronAlways.Size = new System.Drawing.Size(59, 16);
            this.radChevronAlways.TabIndex = 3;
            this.radChevronAlways.TabStop = true;
            this.radChevronAlways.Text = "Always";
            this.radChevronAlways.UseVisualStyleBackColor = true;
            // 
            // radChevronAsNeeded
            // 
            this.radChevronAsNeeded.AutoSize = true;
            this.radChevronAsNeeded.Location = new System.Drawing.Point(15, 39);
            this.radChevronAsNeeded.Name = "radChevronAsNeeded";
            this.radChevronAsNeeded.Size = new System.Drawing.Size(125, 16);
            this.radChevronAsNeeded.TabIndex = 2;
            this.radChevronAsNeeded.TabStop = true;
            this.radChevronAsNeeded.Text = "When mouse hovers";
            this.radChevronAsNeeded.UseVisualStyleBackColor = true;
            // 
            // radChevronNever
            // 
            this.radChevronNever.AutoSize = true;
            this.radChevronNever.Location = new System.Drawing.Point(15, 60);
            this.radChevronNever.Name = "radChevronNever";
            this.radChevronNever.Size = new System.Drawing.Size(53, 16);
            this.radChevronNever.TabIndex = 4;
            this.radChevronNever.TabStop = true;
            this.radChevronNever.Text = "Never";
            this.radChevronNever.UseVisualStyleBackColor = true;
            // 
            // grpUseClearType
            // 
            this.grpUseClearType.Controls.Add(this.chkClearTypeForImages);
            this.grpUseClearType.Controls.Add(this.radClearTypeAlways);
            this.grpUseClearType.Controls.Add(this.radClearTypeWhenZoomed);
            this.grpUseClearType.Controls.Add(this.radClearTypeNever);
            this.grpUseClearType.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpUseClearType.Location = new System.Drawing.Point(3, 23);
            this.grpUseClearType.Name = "grpUseClearType";
            this.grpUseClearType.Size = new System.Drawing.Size(318, 104);
            this.grpUseClearType.TabIndex = 4;
            this.grpUseClearType.TabStop = false;
            this.grpUseClearType.Text = "groupBox1";
            // 
            // chkClearTypeForImages
            // 
            this.chkClearTypeForImages.AutoSize = true;
            this.chkClearTypeForImages.Location = new System.Drawing.Point(15, 81);
            this.chkClearTypeForImages.Name = "chkClearTypeForImages";
            this.chkClearTypeForImages.Size = new System.Drawing.Size(276, 16);
            this.chkClearTypeForImages.TabIndex = 13;
            this.chkClearTypeForImages.Text = "In exported images (disables transparency)";
            this.chkClearTypeForImages.UseVisualStyleBackColor = true;
            // 
            // radClearTypeAlways
            // 
            this.radClearTypeAlways.AutoSize = true;
            this.radClearTypeAlways.Location = new System.Drawing.Point(15, 18);
            this.radClearTypeAlways.Name = "radClearTypeAlways";
            this.radClearTypeAlways.Size = new System.Drawing.Size(59, 16);
            this.radClearTypeAlways.TabIndex = 11;
            this.radClearTypeAlways.TabStop = true;
            this.radClearTypeAlways.Text = "Always";
            this.radClearTypeAlways.UseVisualStyleBackColor = true;
            // 
            // radClearTypeWhenZoomed
            // 
            this.radClearTypeWhenZoomed.AutoSize = true;
            this.radClearTypeWhenZoomed.Location = new System.Drawing.Point(15, 39);
            this.radClearTypeWhenZoomed.Name = "radClearTypeWhenZoomed";
            this.radClearTypeWhenZoomed.Size = new System.Drawing.Size(131, 16);
            this.radClearTypeWhenZoomed.TabIndex = 10;
            this.radClearTypeWhenZoomed.TabStop = true;
            this.radClearTypeWhenZoomed.Text = "When zoomed in/out";
            this.radClearTypeWhenZoomed.UseVisualStyleBackColor = true;
            // 
            // radClearTypeNever
            // 
            this.radClearTypeNever.AutoSize = true;
            this.radClearTypeNever.Location = new System.Drawing.Point(15, 60);
            this.radClearTypeNever.Name = "radClearTypeNever";
            this.radClearTypeNever.Size = new System.Drawing.Size(53, 16);
            this.radClearTypeNever.TabIndex = 12;
            this.radClearTypeNever.TabStop = true;
            this.radClearTypeNever.Text = "Never";
            this.radClearTypeNever.UseVisualStyleBackColor = true;
            // 
            // chkUsePrecisionSnapping
            // 
            this.chkUsePrecisionSnapping.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkUsePrecisionSnapping.Location = new System.Drawing.Point(3, 3);
            this.chkUsePrecisionSnapping.Name = "chkUsePrecisionSnapping";
            this.chkUsePrecisionSnapping.Size = new System.Drawing.Size(318, 20);
            this.chkUsePrecisionSnapping.TabIndex = 0;
            this.chkUsePrecisionSnapping.Values.Text = "Use precision snapping";
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.btnCancel);
            this.kryptonPanel.Controls.Add(this.btnOK);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel.Location = new System.Drawing.Point(5, 343);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(332, 30);
            this.kryptonPanel.TabIndex = 6;
            // 
            // OptionsDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(342, 378);
            this.Controls.Add(this.tabOptions);
            this.Controls.Add(this.kryptonPanel);
            this.MinimumSize = new System.Drawing.Size(350, 372);
            this.Name = "OptionsDialog";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Options";
            this.tabOptions.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.grpTemplate.ResumeLayout(false);
            this.grpTemplate.PerformLayout();
            this.tableLayoutPanelTemplate.ResumeLayout(false);
            this.tableLayoutPanelTemplate.PerformLayout();
            this.grpProject.ResumeLayout(false);
            this.grpProject.PerformLayout();
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            this.tableLayoutPanelGeneral.ResumeLayout(false);
            this.tableLayoutPanelGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboLanguage)).EndInit();
            this.tabStyle.ResumeLayout(false);
            this.tableLayoutPanelStyle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboStyles)).EndInit();
            this.tabDiagram.ResumeLayout(false);
            this.tabDiagram.PerformLayout();
            this.grpShowChevron.ResumeLayout(false);
            this.grpShowChevron.PerformLayout();
            this.grpUseClearType.ResumeLayout(false);
            this.grpUseClearType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOK;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private System.Windows.Forms.TabControl tabOptions;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.GroupBox grpGeneral;
        private System.Windows.Forms.TabPage tabStyle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelStyle;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private System.Windows.Forms.PropertyGrid stylePropertyGrid;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cboStyles;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnLoad;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClear;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkRememberOpenProjects;
        private System.Windows.Forms.TabPage tabDiagram;
        private System.Windows.Forms.CheckBox chkClearTypeForImages;
        private System.Windows.Forms.RadioButton radClearTypeNever;
        private System.Windows.Forms.RadioButton radClearTypeAlways;
        private System.Windows.Forms.RadioButton radClearTypeWhenZoomed;
        private System.Windows.Forms.RadioButton radChevronAsNeeded;
        private System.Windows.Forms.RadioButton radChevronNever;
        private System.Windows.Forms.RadioButton radChevronAlways;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkUsePrecisionSnapping;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnBrowser;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtSavePath;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblSavePath;
        private System.Windows.Forms.GroupBox grpTemplate;
        private System.Windows.Forms.GroupBox grpProject;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cboLanguage;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClearRecents;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblLanguage;
        private System.Windows.Forms.GroupBox grpShowChevron;
        private System.Windows.Forms.GroupBox grpUseClearType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGeneral;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTemplate;
    }
}