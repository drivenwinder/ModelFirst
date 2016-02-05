namespace EAP.ModelFirst.Controls.Editors.TypeEditors
{
    partial class EnumTypeEditor
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
            this.kryptonHeaderGroupEnumInfo = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblComments = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtComments = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblName = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbxAccess = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.lblAccess = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonHeaderGroupEnumValues = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.enumValueEditor = new EAP.ModelFirst.Controls.Editors.TypeEditors.EnumValueEditor();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupEnumInfo)).BeginInit();
            this.kryptonHeaderGroupEnumInfo.Panel.SuspendLayout();
            this.kryptonHeaderGroupEnumInfo.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxAccess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupEnumValues)).BeginInit();
            this.kryptonHeaderGroupEnumValues.Panel.SuspendLayout();
            this.kryptonHeaderGroupEnumValues.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonHeaderGroupEnumInfo
            // 
            this.kryptonHeaderGroupEnumInfo.AutoSize = true;
            this.kryptonHeaderGroupEnumInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderGroupEnumInfo.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kryptonHeaderGroupEnumInfo.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroupEnumInfo.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroupEnumInfo.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroupEnumInfo.Name = "kryptonHeaderGroupEnumInfo";
            // 
            // kryptonHeaderGroupEnumInfo.Panel
            // 
            this.kryptonHeaderGroupEnumInfo.Panel.Controls.Add(this.tableLayoutPanel);
            this.kryptonHeaderGroupEnumInfo.Size = new System.Drawing.Size(400, 101);
            this.kryptonHeaderGroupEnumInfo.TabIndex = 0;
            this.kryptonHeaderGroupEnumInfo.ValuesPrimary.Heading = "Enum";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel.Controls.Add(this.lblComments, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.txtComments, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.txtName, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.lblName, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.cbxAccess, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.lblAccess, 0, 0);
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(286, 78);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // lblComments
            // 
            this.lblComments.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblComments.Location = new System.Drawing.Point(3, 55);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(65, 20);
            this.lblComments.TabIndex = 22;
            this.lblComments.Values.Text = "Comments";
            // 
            // txtComments
            // 
            this.txtComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComments.Location = new System.Drawing.Point(74, 55);
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(194, 20);
            this.txtComments.TabIndex = 21;
            this.txtComments.Validating += new System.ComponentModel.CancelEventHandler(this.txtComments_Validating);
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Location = new System.Drawing.Point(74, 29);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(194, 20);
            this.txtName.TabIndex = 7;
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
            // 
            // lblName
            // 
            this.lblName.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblName.Location = new System.Drawing.Point(28, 29);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(40, 20);
            this.lblName.TabIndex = 6;
            this.lblName.Values.Text = "Name";
            // 
            // cbxAccess
            // 
            this.cbxAccess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxAccess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAccess.DropDownWidth = 214;
            this.cbxAccess.Location = new System.Drawing.Point(74, 3);
            this.cbxAccess.Name = "cbxAccess";
            this.cbxAccess.Size = new System.Drawing.Size(194, 20);
            this.cbxAccess.TabIndex = 3;
            this.cbxAccess.SelectedIndexChanged += new System.EventHandler(this.cbxAccess_SelectedIndexChanged);
            this.cbxAccess.Validated += new System.EventHandler(this.cbxAccess_Validated);
            // 
            // lblAccess
            // 
            this.lblAccess.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblAccess.Location = new System.Drawing.Point(21, 3);
            this.lblAccess.Name = "lblAccess";
            this.lblAccess.Size = new System.Drawing.Size(47, 20);
            this.lblAccess.TabIndex = 2;
            this.lblAccess.Values.Text = "Access";
            // 
            // kryptonHeaderGroupEnumValues
            // 
            this.kryptonHeaderGroupEnumValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroupEnumValues.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kryptonHeaderGroupEnumValues.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroupEnumValues.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroupEnumValues.Location = new System.Drawing.Point(0, 101);
            this.kryptonHeaderGroupEnumValues.Name = "kryptonHeaderGroupEnumValues";
            // 
            // kryptonHeaderGroupEnumValues.Panel
            // 
            this.kryptonHeaderGroupEnumValues.Panel.Controls.Add(this.enumValueEditor);
            this.kryptonHeaderGroupEnumValues.Panel.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonHeaderGroupEnumValues.Size = new System.Drawing.Size(400, 199);
            this.kryptonHeaderGroupEnumValues.TabIndex = 1;
            this.kryptonHeaderGroupEnumValues.ValuesPrimary.Heading = "Enum Values";
            // 
            // enumValueEditor
            // 
            this.enumValueEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.enumValueEditor.Location = new System.Drawing.Point(5, 5);
            this.enumValueEditor.Name = "enumValueEditor";
            this.enumValueEditor.Size = new System.Drawing.Size(388, 168);
            this.enumValueEditor.TabIndex = 0;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // EnumTypeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.kryptonHeaderGroupEnumValues);
            this.Controls.Add(this.kryptonHeaderGroupEnumInfo);
            this.Name = "EnumTypeEditor";
            this.Size = new System.Drawing.Size(400, 300);
            this.kryptonHeaderGroupEnumInfo.Panel.ResumeLayout(false);
            this.kryptonHeaderGroupEnumInfo.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupEnumInfo)).EndInit();
            this.kryptonHeaderGroupEnumInfo.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxAccess)).EndInit();
            this.kryptonHeaderGroupEnumValues.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupEnumValues)).EndInit();
            this.kryptonHeaderGroupEnumValues.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroupEnumInfo;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroupEnumValues;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAccess;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbxAccess;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblName;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtName;
        private EnumValueEditor enumValueEditor;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblComments;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtComments;
    }
}
