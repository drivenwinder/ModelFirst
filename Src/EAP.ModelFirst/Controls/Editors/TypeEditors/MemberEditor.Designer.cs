namespace EAP.ModelFirst.Controls.Editors.TypeEditors
{
    partial class MemberEditor
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblComments = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtComments = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtInitValue = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblInitValue = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbxAccess = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.cbxType = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.lblAccess = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblName = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtSyntax = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblSyntax = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonPanelModifiers = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.gbOperationModifiers = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.tableLayoutPanelOperationModifiers = new System.Windows.Forms.TableLayoutPanel();
            this.ckbOperationSealed = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ckbOperationOverride = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ckbOperationNew = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ckbOperationAbstract = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ckbOperationStatic = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ckbOperationVirtual = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.gbFieldModifiers = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.tableLayoutPanelFiledModifiers = new System.Windows.Forms.TableLayoutPanel();
            this.ckbFieldVolatile = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ckbFieldNew = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ckbFieldConst = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ckbFieldStatic = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ckbFieldReadonly = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.lblType = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.tableLayoutPanelGetterSetter = new System.Windows.Forms.TableLayoutPanel();
            this.ckbGetter = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ckbSetter = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxAccess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelModifiers)).BeginInit();
            this.kryptonPanelModifiers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbOperationModifiers)).BeginInit();
            this.gbOperationModifiers.Panel.SuspendLayout();
            this.gbOperationModifiers.SuspendLayout();
            this.tableLayoutPanelOperationModifiers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbFieldModifiers)).BeginInit();
            this.gbFieldModifiers.Panel.SuspendLayout();
            this.gbFieldModifiers.SuspendLayout();
            this.tableLayoutPanelFiledModifiers.SuspendLayout();
            this.tableLayoutPanelGetterSetter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel.ColumnCount = 6;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.lblComments, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.txtComments, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.txtInitValue, 3, 3);
            this.tableLayoutPanel.Controls.Add(this.lblInitValue, 2, 3);
            this.tableLayoutPanel.Controls.Add(this.cbxAccess, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.cbxType, 3, 2);
            this.tableLayoutPanel.Controls.Add(this.lblAccess, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.lblName, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.txtName, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.txtSyntax, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.lblSyntax, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.kryptonPanelModifiers, 5, 0);
            this.tableLayoutPanel.Controls.Add(this.lblType, 2, 2);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanelGetterSetter, 3, 4);
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 5;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(749, 115);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // lblComments
            // 
            this.lblComments.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblComments.Location = new System.Drawing.Point(3, 92);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(70, 20);
            this.lblComments.TabIndex = 20;
            this.lblComments.Values.Text = "Comments";
            // 
            // txtComments
            // 
            this.txtComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComments.Location = new System.Drawing.Point(79, 92);
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(194, 20);
            this.txtComments.TabIndex = 19;
            this.txtComments.Validating += new System.ComponentModel.CancelEventHandler(this.txtComments_Validating);
            // 
            // txtInitValue
            // 
            this.txtInitValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInitValue.Location = new System.Drawing.Point(359, 65);
            this.txtInitValue.Name = "txtInitValue";
            this.txtInitValue.Size = new System.Drawing.Size(194, 20);
            this.txtInitValue.TabIndex = 4;
            this.txtInitValue.Validating += new System.ComponentModel.CancelEventHandler(this.txtInitValue_Validating);
            // 
            // lblInitValue
            // 
            this.lblInitValue.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblInitValue.Location = new System.Drawing.Point(279, 65);
            this.lblInitValue.Name = "lblInitValue";
            this.lblInitValue.Size = new System.Drawing.Size(74, 21);
            this.lblInitValue.TabIndex = 14;
            this.lblInitValue.Values.Text = "Initial Value";
            // 
            // cbxAccess
            // 
            this.cbxAccess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxAccess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAccess.DropDownWidth = 244;
            this.cbxAccess.Location = new System.Drawing.Point(79, 65);
            this.cbxAccess.Name = "cbxAccess";
            this.cbxAccess.Size = new System.Drawing.Size(194, 21);
            this.cbxAccess.TabIndex = 3;
            this.cbxAccess.SelectedIndexChanged += new System.EventHandler(this.cbxAccess_SelectedIndexChanged);
            this.cbxAccess.Validated += new System.EventHandler(this.cbxAccess_Validated);
            // 
            // cbxType
            // 
            this.cbxType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxType.DropDownWidth = 244;
            this.cbxType.Items.AddRange(new object[] {
            "string",
            "int",
            "bool",
            "DateTime"});
            this.cbxType.Location = new System.Drawing.Point(359, 38);
            this.cbxType.Name = "cbxType";
            this.cbxType.Size = new System.Drawing.Size(194, 21);
            this.cbxType.TabIndex = 2;
            this.cbxType.Validating += new System.ComponentModel.CancelEventHandler(this.cbxType_Validating);
            // 
            // lblAccess
            // 
            this.lblAccess.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblAccess.Location = new System.Drawing.Point(26, 65);
            this.lblAccess.Name = "lblAccess";
            this.lblAccess.Size = new System.Drawing.Size(47, 21);
            this.lblAccess.TabIndex = 11;
            this.lblAccess.Values.Text = "Access";
            // 
            // lblName
            // 
            this.lblName.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblName.Location = new System.Drawing.Point(30, 38);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(43, 21);
            this.lblName.TabIndex = 9;
            this.lblName.Values.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Location = new System.Drawing.Point(79, 38);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(194, 20);
            this.txtName.TabIndex = 1;
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
            // 
            // txtSyntax
            // 
            this.tableLayoutPanel.SetColumnSpan(this.txtSyntax, 3);
            this.txtSyntax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSyntax.Location = new System.Drawing.Point(79, 12);
            this.txtSyntax.Name = "txtSyntax";
            this.txtSyntax.Size = new System.Drawing.Size(474, 20);
            this.txtSyntax.TabIndex = 0;
            this.txtSyntax.Validating += new System.ComponentModel.CancelEventHandler(this.txtSyntax_Validating);
            // 
            // lblSyntax
            // 
            this.lblSyntax.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblSyntax.Location = new System.Drawing.Point(27, 12);
            this.lblSyntax.Name = "lblSyntax";
            this.lblSyntax.Size = new System.Drawing.Size(46, 20);
            this.lblSyntax.TabIndex = 0;
            this.lblSyntax.Values.Text = "Syntax";
            // 
            // kryptonPanelModifiers
            // 
            this.kryptonPanelModifiers.AutoSize = true;
            this.kryptonPanelModifiers.Controls.Add(this.gbOperationModifiers);
            this.kryptonPanelModifiers.Controls.Add(this.gbFieldModifiers);
            this.kryptonPanelModifiers.Location = new System.Drawing.Point(569, 3);
            this.kryptonPanelModifiers.Name = "kryptonPanelModifiers";
            this.kryptonPanelModifiers.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.tableLayoutPanel.SetRowSpan(this.kryptonPanelModifiers, 5);
            this.kryptonPanelModifiers.Size = new System.Drawing.Size(177, 109);
            this.kryptonPanelModifiers.TabIndex = 16;
            // 
            // gbOperationModifiers
            // 
            this.gbOperationModifiers.AutoSize = true;
            this.gbOperationModifiers.Location = new System.Drawing.Point(0, -1);
            this.gbOperationModifiers.Name = "gbOperationModifiers";
            // 
            // gbOperationModifiers.Panel
            // 
            this.gbOperationModifiers.Panel.Controls.Add(this.tableLayoutPanelOperationModifiers);
            this.gbOperationModifiers.Size = new System.Drawing.Size(159, 107);
            this.gbOperationModifiers.TabIndex = 5;
            this.gbOperationModifiers.Values.Heading = "Modifiers";
            // 
            // tableLayoutPanelOperationModifiers
            // 
            this.tableLayoutPanelOperationModifiers.AutoSize = true;
            this.tableLayoutPanelOperationModifiers.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelOperationModifiers.ColumnCount = 2;
            this.tableLayoutPanelOperationModifiers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOperationModifiers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelOperationModifiers.Controls.Add(this.ckbOperationSealed, 1, 2);
            this.tableLayoutPanelOperationModifiers.Controls.Add(this.ckbOperationOverride, 1, 1);
            this.tableLayoutPanelOperationModifiers.Controls.Add(this.ckbOperationNew, 1, 0);
            this.tableLayoutPanelOperationModifiers.Controls.Add(this.ckbOperationAbstract, 0, 2);
            this.tableLayoutPanelOperationModifiers.Controls.Add(this.ckbOperationStatic, 0, 0);
            this.tableLayoutPanelOperationModifiers.Controls.Add(this.ckbOperationVirtual, 0, 1);
            this.tableLayoutPanelOperationModifiers.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelOperationModifiers.Name = "tableLayoutPanelOperationModifiers";
            this.tableLayoutPanelOperationModifiers.RowCount = 3;
            this.tableLayoutPanelOperationModifiers.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelOperationModifiers.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelOperationModifiers.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelOperationModifiers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanelOperationModifiers.Size = new System.Drawing.Size(152, 78);
            this.tableLayoutPanelOperationModifiers.TabIndex = 0;
            // 
            // ckbOperationSealed
            // 
            this.ckbOperationSealed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbOperationSealed.Location = new System.Drawing.Point(78, 55);
            this.ckbOperationSealed.Name = "ckbOperationSealed";
            this.ckbOperationSealed.Size = new System.Drawing.Size(71, 20);
            this.ckbOperationSealed.TabIndex = 5;
            this.ckbOperationSealed.Values.Text = "Sealed";
            this.ckbOperationSealed.CheckedChanged += new System.EventHandler(this.ckbOperationSealed_CheckedChanged);
            // 
            // ckbOperationOverride
            // 
            this.ckbOperationOverride.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbOperationOverride.Location = new System.Drawing.Point(78, 29);
            this.ckbOperationOverride.Name = "ckbOperationOverride";
            this.ckbOperationOverride.Size = new System.Drawing.Size(71, 20);
            this.ckbOperationOverride.TabIndex = 4;
            this.ckbOperationOverride.Values.Text = "Override";
            this.ckbOperationOverride.CheckedChanged += new System.EventHandler(this.ckbOperationOverride_CheckedChanged);
            // 
            // ckbOperationNew
            // 
            this.ckbOperationNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbOperationNew.Location = new System.Drawing.Point(78, 3);
            this.ckbOperationNew.Name = "ckbOperationNew";
            this.ckbOperationNew.Size = new System.Drawing.Size(71, 20);
            this.ckbOperationNew.TabIndex = 3;
            this.ckbOperationNew.Values.Text = "New";
            this.ckbOperationNew.CheckedChanged += new System.EventHandler(this.ckbOperationNew_CheckedChanged);
            // 
            // ckbOperationAbstract
            // 
            this.ckbOperationAbstract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbOperationAbstract.Location = new System.Drawing.Point(3, 55);
            this.ckbOperationAbstract.Name = "ckbOperationAbstract";
            this.ckbOperationAbstract.Size = new System.Drawing.Size(69, 20);
            this.ckbOperationAbstract.TabIndex = 2;
            this.ckbOperationAbstract.Values.Text = "Abstract";
            this.ckbOperationAbstract.CheckedChanged += new System.EventHandler(this.ckbOperationAbstract_CheckedChanged);
            // 
            // ckbOperationStatic
            // 
            this.ckbOperationStatic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbOperationStatic.Location = new System.Drawing.Point(3, 3);
            this.ckbOperationStatic.Name = "ckbOperationStatic";
            this.ckbOperationStatic.Size = new System.Drawing.Size(69, 20);
            this.ckbOperationStatic.TabIndex = 0;
            this.ckbOperationStatic.Values.Text = "Static";
            this.ckbOperationStatic.CheckedChanged += new System.EventHandler(this.ckbOperationStatic_CheckedChanged);
            // 
            // ckbOperationVirtual
            // 
            this.ckbOperationVirtual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbOperationVirtual.Location = new System.Drawing.Point(3, 29);
            this.ckbOperationVirtual.Name = "ckbOperationVirtual";
            this.ckbOperationVirtual.Size = new System.Drawing.Size(69, 20);
            this.ckbOperationVirtual.TabIndex = 1;
            this.ckbOperationVirtual.Values.Text = "Virtual";
            this.ckbOperationVirtual.CheckedChanged += new System.EventHandler(this.ckbOperationVirtual_CheckedChanged);
            // 
            // gbFieldModifiers
            // 
            this.gbFieldModifiers.AutoSize = true;
            this.gbFieldModifiers.Location = new System.Drawing.Point(0, -1);
            this.gbFieldModifiers.Name = "gbFieldModifiers";
            // 
            // gbFieldModifiers.Panel
            // 
            this.gbFieldModifiers.Panel.Controls.Add(this.tableLayoutPanelFiledModifiers);
            this.gbFieldModifiers.Size = new System.Drawing.Size(157, 107);
            this.gbFieldModifiers.TabIndex = 6;
            this.gbFieldModifiers.Values.Heading = "Modifiers";
            // 
            // tableLayoutPanelFiledModifiers
            // 
            this.tableLayoutPanelFiledModifiers.AutoSize = true;
            this.tableLayoutPanelFiledModifiers.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelFiledModifiers.ColumnCount = 2;
            this.tableLayoutPanelFiledModifiers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelFiledModifiers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelFiledModifiers.Controls.Add(this.ckbFieldVolatile, 1, 1);
            this.tableLayoutPanelFiledModifiers.Controls.Add(this.ckbFieldNew, 1, 0);
            this.tableLayoutPanelFiledModifiers.Controls.Add(this.ckbFieldConst, 0, 2);
            this.tableLayoutPanelFiledModifiers.Controls.Add(this.ckbFieldStatic, 0, 0);
            this.tableLayoutPanelFiledModifiers.Controls.Add(this.ckbFieldReadonly, 0, 1);
            this.tableLayoutPanelFiledModifiers.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelFiledModifiers.Name = "tableLayoutPanelFiledModifiers";
            this.tableLayoutPanelFiledModifiers.RowCount = 3;
            this.tableLayoutPanelFiledModifiers.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFiledModifiers.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFiledModifiers.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFiledModifiers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanelFiledModifiers.Size = new System.Drawing.Size(150, 78);
            this.tableLayoutPanelFiledModifiers.TabIndex = 0;
            // 
            // ckbFieldVolatile
            // 
            this.ckbFieldVolatile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbFieldVolatile.Location = new System.Drawing.Point(83, 29);
            this.ckbFieldVolatile.Name = "ckbFieldVolatile";
            this.ckbFieldVolatile.Size = new System.Drawing.Size(64, 20);
            this.ckbFieldVolatile.TabIndex = 4;
            this.ckbFieldVolatile.Values.Text = "Volatile";
            this.ckbFieldVolatile.CheckedChanged += new System.EventHandler(this.ckbFieldVolatile_CheckedChanged);
            // 
            // ckbFieldNew
            // 
            this.ckbFieldNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbFieldNew.Location = new System.Drawing.Point(83, 3);
            this.ckbFieldNew.Name = "ckbFieldNew";
            this.ckbFieldNew.Size = new System.Drawing.Size(64, 20);
            this.ckbFieldNew.TabIndex = 3;
            this.ckbFieldNew.Values.Text = "New";
            this.ckbFieldNew.CheckedChanged += new System.EventHandler(this.ckbFieldNew_CheckedChanged);
            // 
            // ckbFieldConst
            // 
            this.ckbFieldConst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbFieldConst.Location = new System.Drawing.Point(3, 55);
            this.ckbFieldConst.Name = "ckbFieldConst";
            this.ckbFieldConst.Size = new System.Drawing.Size(74, 20);
            this.ckbFieldConst.TabIndex = 2;
            this.ckbFieldConst.Values.Text = "Const";
            this.ckbFieldConst.CheckedChanged += new System.EventHandler(this.ckbFieldConst_CheckedChanged);
            // 
            // ckbFieldStatic
            // 
            this.ckbFieldStatic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbFieldStatic.Location = new System.Drawing.Point(3, 3);
            this.ckbFieldStatic.Name = "ckbFieldStatic";
            this.ckbFieldStatic.Size = new System.Drawing.Size(74, 20);
            this.ckbFieldStatic.TabIndex = 0;
            this.ckbFieldStatic.Values.Text = "Static";
            this.ckbFieldStatic.CheckedChanged += new System.EventHandler(this.ckbFieldStatic_CheckedChanged);
            // 
            // ckbFieldReadonly
            // 
            this.ckbFieldReadonly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckbFieldReadonly.Location = new System.Drawing.Point(3, 29);
            this.ckbFieldReadonly.Name = "ckbFieldReadonly";
            this.ckbFieldReadonly.Size = new System.Drawing.Size(74, 20);
            this.ckbFieldReadonly.TabIndex = 1;
            this.ckbFieldReadonly.Values.Text = "Readonly";
            this.ckbFieldReadonly.CheckedChanged += new System.EventHandler(this.ckbFieldReadonly_CheckedChanged);
            // 
            // lblType
            // 
            this.lblType.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblType.Location = new System.Drawing.Point(316, 38);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(37, 21);
            this.lblType.TabIndex = 10;
            this.lblType.Values.Text = "Type";
            // 
            // tableLayoutPanelGetterSetter
            // 
            this.tableLayoutPanelGetterSetter.AutoSize = true;
            this.tableLayoutPanelGetterSetter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelGetterSetter.ColumnCount = 2;
            this.tableLayoutPanelGetterSetter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelGetterSetter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelGetterSetter.Controls.Add(this.ckbGetter, 0, 0);
            this.tableLayoutPanelGetterSetter.Controls.Add(this.ckbSetter, 1, 0);
            this.tableLayoutPanelGetterSetter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelGetterSetter.Location = new System.Drawing.Point(356, 89);
            this.tableLayoutPanelGetterSetter.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelGetterSetter.Name = "tableLayoutPanelGetterSetter";
            this.tableLayoutPanelGetterSetter.RowCount = 1;
            this.tableLayoutPanelGetterSetter.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelGetterSetter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelGetterSetter.Size = new System.Drawing.Size(200, 26);
            this.tableLayoutPanelGetterSetter.TabIndex = 17;
            // 
            // ckbGetter
            // 
            this.ckbGetter.Dock = System.Windows.Forms.DockStyle.Left;
            this.ckbGetter.Location = new System.Drawing.Point(3, 3);
            this.ckbGetter.Name = "ckbGetter";
            this.ckbGetter.Size = new System.Drawing.Size(58, 20);
            this.ckbGetter.TabIndex = 0;
            this.ckbGetter.Values.Text = "Getter";
            this.ckbGetter.CheckedChanged += new System.EventHandler(this.ckbGetter_CheckedChanged);
            // 
            // ckbSetter
            // 
            this.ckbSetter.Dock = System.Windows.Forms.DockStyle.Left;
            this.ckbSetter.Location = new System.Drawing.Point(103, 3);
            this.ckbSetter.Name = "ckbSetter";
            this.ckbSetter.Size = new System.Drawing.Size(56, 20);
            this.ckbSetter.TabIndex = 1;
            this.ckbSetter.Values.Text = "Setter";
            this.ckbSetter.CheckedChanged += new System.EventHandler(this.ckbSetter_CheckedChanged);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // MemberEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "MemberEditor";
            this.Size = new System.Drawing.Size(752, 118);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxAccess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanelModifiers)).EndInit();
            this.kryptonPanelModifiers.ResumeLayout(false);
            this.kryptonPanelModifiers.PerformLayout();
            this.gbOperationModifiers.Panel.ResumeLayout(false);
            this.gbOperationModifiers.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbOperationModifiers)).EndInit();
            this.gbOperationModifiers.ResumeLayout(false);
            this.tableLayoutPanelOperationModifiers.ResumeLayout(false);
            this.tableLayoutPanelOperationModifiers.PerformLayout();
            this.gbFieldModifiers.Panel.ResumeLayout(false);
            this.gbFieldModifiers.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbFieldModifiers)).EndInit();
            this.gbFieldModifiers.ResumeLayout(false);
            this.tableLayoutPanelFiledModifiers.ResumeLayout(false);
            this.tableLayoutPanelFiledModifiers.PerformLayout();
            this.tableLayoutPanelGetterSetter.ResumeLayout(false);
            this.tableLayoutPanelGetterSetter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtInitValue;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblInitValue;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbxAccess;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAccess;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblType;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblName;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtName;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtSyntax;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblSyntax;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanelModifiers;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox gbOperationModifiers;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelOperationModifiers;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckbOperationSealed;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckbOperationOverride;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckbOperationNew;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckbOperationAbstract;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckbOperationStatic;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckbOperationVirtual;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox gbFieldModifiers;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFiledModifiers;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckbFieldVolatile;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckbFieldNew;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckbFieldConst;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckbFieldStatic;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckbFieldReadonly;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbxType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGetterSetter;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckbGetter;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckbSetter;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblComments;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtComments;
    }
}
