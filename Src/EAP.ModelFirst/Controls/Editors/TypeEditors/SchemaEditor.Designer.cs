namespace EAP.ModelFirst.Controls.Editors.TypeEditors
{
    partial class SchemaEditor
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
            this.ckbGenColumn = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.tableLayoutPanelCkb = new System.Windows.Forms.TableLayoutPanel();
            this.cbxIndex = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ckbAutoIncrement = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ckbPrimaryKey = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.ckbNotNull = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.txtLength = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtDefaultValue = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblDefaultValue = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbxDbType = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.lblLength = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblDbType = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblName = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel.SuspendLayout();
            this.tableLayoutPanelCkb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDbType)).BeginInit();
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
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel.Controls.Add(this.ckbGenColumn, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.tableLayoutPanelCkb, 4, 0);
            this.tableLayoutPanel.Controls.Add(this.txtLength, 3, 1);
            this.tableLayoutPanel.Controls.Add(this.txtDefaultValue, 5, 1);
            this.tableLayoutPanel.Controls.Add(this.lblDefaultValue, 4, 1);
            this.tableLayoutPanel.Controls.Add(this.cbxDbType, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.lblLength, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.lblDbType, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.txtName, 3, 0);
            this.tableLayoutPanel.Controls.Add(this.lblName, 2, 0);
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(753, 57);
            this.tableLayoutPanel.TabIndex = 2;
            // 
            // ckbGenColumn
            // 
            this.ckbGenColumn.Dock = System.Windows.Forms.DockStyle.Left;
            this.ckbGenColumn.Location = new System.Drawing.Point(46, 3);
            this.ckbGenColumn.Name = "ckbGenColumn";
            this.ckbGenColumn.Size = new System.Drawing.Size(135, 24);
            this.ckbGenColumn.TabIndex = 0;
            this.ckbGenColumn.Values.Text = "Generate DbColumn";
            this.ckbGenColumn.CheckedChanged += new System.EventHandler(this.ckbGenColumn_CheckedChanged);
            // 
            // tableLayoutPanelCkb
            // 
            this.tableLayoutPanelCkb.AutoSize = true;
            this.tableLayoutPanelCkb.ColumnCount = 4;
            this.tableLayoutPanel.SetColumnSpan(this.tableLayoutPanelCkb, 2);
            this.tableLayoutPanelCkb.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelCkb.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelCkb.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelCkb.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelCkb.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelCkb.Controls.Add(this.cbxIndex, 3, 0);
            this.tableLayoutPanelCkb.Controls.Add(this.ckbAutoIncrement, 2, 0);
            this.tableLayoutPanelCkb.Controls.Add(this.ckbPrimaryKey, 0, 0);
            this.tableLayoutPanelCkb.Controls.Add(this.ckbNotNull, 1, 0);
            this.tableLayoutPanelCkb.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanelCkb.Location = new System.Drawing.Point(557, 0);
            this.tableLayoutPanelCkb.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelCkb.Name = "tableLayoutPanelCkb";
            this.tableLayoutPanelCkb.RowCount = 1;
            this.tableLayoutPanelCkb.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelCkb.Size = new System.Drawing.Size(196, 30);
            this.tableLayoutPanelCkb.TabIndex = 11;
            // 
            // cbxIndex
            // 
            this.cbxIndex.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbxIndex.Location = new System.Drawing.Point(150, 3);
            this.cbxIndex.Name = "cbxIndex";
            this.cbxIndex.Size = new System.Drawing.Size(43, 24);
            this.cbxIndex.TabIndex = 10;
            this.toolTip.SetToolTip(this.cbxIndex, "Index");
            this.cbxIndex.Values.Text = "IDX";
            this.cbxIndex.CheckedChanged += new System.EventHandler(this.cbxIndex_CheckedChanged);
            // 
            // ckbAutoIncrement
            // 
            this.ckbAutoIncrement.Dock = System.Windows.Forms.DockStyle.Left;
            this.ckbAutoIncrement.Location = new System.Drawing.Point(101, 3);
            this.ckbAutoIncrement.Name = "ckbAutoIncrement";
            this.ckbAutoIncrement.Size = new System.Drawing.Size(35, 24);
            this.ckbAutoIncrement.TabIndex = 9;
            this.toolTip.SetToolTip(this.ckbAutoIncrement, "Auto Increment");
            this.ckbAutoIncrement.Values.Text = "AI";
            this.ckbAutoIncrement.CheckedChanged += new System.EventHandler(this.ckbAutoIncrement_CheckedChanged);
            // 
            // ckbPrimaryKey
            // 
            this.ckbPrimaryKey.Dock = System.Windows.Forms.DockStyle.Left;
            this.ckbPrimaryKey.Location = new System.Drawing.Point(3, 3);
            this.ckbPrimaryKey.Name = "ckbPrimaryKey";
            this.ckbPrimaryKey.Size = new System.Drawing.Size(38, 24);
            this.ckbPrimaryKey.TabIndex = 6;
            this.toolTip.SetToolTip(this.ckbPrimaryKey, "Primary Key");
            this.ckbPrimaryKey.Values.Text = "PK";
            this.ckbPrimaryKey.CheckedChanged += new System.EventHandler(this.ckbPrimaryKey_CheckedChanged);
            // 
            // ckbNotNull
            // 
            this.ckbNotNull.Dock = System.Windows.Forms.DockStyle.Left;
            this.ckbNotNull.Location = new System.Drawing.Point(52, 3);
            this.ckbNotNull.Name = "ckbNotNull";
            this.ckbNotNull.Size = new System.Drawing.Size(42, 24);
            this.ckbNotNull.TabIndex = 7;
            this.toolTip.SetToolTip(this.ckbNotNull, "Not Null");
            this.ckbNotNull.Values.Text = "NN";
            this.ckbNotNull.CheckedChanged += new System.EventHandler(this.ckbNotNull_CheckedChanged);
            // 
            // txtLength
            // 
            this.txtLength.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLength.Location = new System.Drawing.Point(300, 33);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(194, 20);
            this.txtLength.TabIndex = 4;
            this.txtLength.Validating += new System.ComponentModel.CancelEventHandler(this.txtLength_Validating);
            // 
            // txtDefaultValue
            // 
            this.txtDefaultValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDefaultValue.Location = new System.Drawing.Point(556, 33);
            this.txtDefaultValue.Name = "txtDefaultValue";
            this.txtDefaultValue.Size = new System.Drawing.Size(194, 20);
            this.txtDefaultValue.TabIndex = 5;
            this.txtDefaultValue.Validating += new System.ComponentModel.CancelEventHandler(this.txtDefaultValue_Validating);
            // 
            // lblDefaultValue
            // 
            this.lblDefaultValue.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDefaultValue.Location = new System.Drawing.Point(500, 33);
            this.lblDefaultValue.Name = "lblDefaultValue";
            this.lblDefaultValue.Size = new System.Drawing.Size(50, 21);
            this.lblDefaultValue.TabIndex = 14;
            this.toolTip.SetToolTip(this.lblDefaultValue, "Default Value");
            this.lblDefaultValue.Values.Text = "Default";
            // 
            // cbxDbType
            // 
            this.cbxDbType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxDbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDbType.DropDownWidth = 244;
            this.cbxDbType.Location = new System.Drawing.Point(46, 33);
            this.cbxDbType.Name = "cbxDbType";
            this.cbxDbType.Size = new System.Drawing.Size(194, 21);
            this.cbxDbType.TabIndex = 3;
            this.cbxDbType.SelectedIndexChanged += new System.EventHandler(this.cbxDbType_SelectedIndexChanged);
            // 
            // lblLength
            // 
            this.lblLength.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblLength.Location = new System.Drawing.Point(246, 33);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(48, 21);
            this.lblLength.TabIndex = 11;
            this.lblLength.Values.Text = "Length";
            // 
            // lblDbType
            // 
            this.lblDbType.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDbType.Location = new System.Drawing.Point(3, 33);
            this.lblDbType.Name = "lblDbType";
            this.lblDbType.Size = new System.Drawing.Size(37, 21);
            this.lblDbType.TabIndex = 10;
            this.lblDbType.Values.Text = "Type";
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Location = new System.Drawing.Point(300, 3);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(194, 20);
            this.txtName.TabIndex = 2;
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
            // 
            // lblName
            // 
            this.lblName.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblName.Location = new System.Drawing.Point(251, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(43, 24);
            this.lblName.TabIndex = 0;
            this.lblName.Values.Text = "Name";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // SchemaEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "SchemaEditor";
            this.Size = new System.Drawing.Size(756, 60);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.tableLayoutPanelCkb.ResumeLayout(false);
            this.tableLayoutPanelCkb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDbType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtLength;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDefaultValue;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblDefaultValue;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbxDbType;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblLength;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblDbType;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblName;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckbNotNull;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckbPrimaryKey;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckbAutoIncrement;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox cbxIndex;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox ckbGenColumn;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelCkb;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
