namespace EAP.ModelFirst.Controls.Editors.TypeEditors
{
    partial class DelegateTypeEditor
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
            this.kryptonHeaderGroupDelegateParameter = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.delegateParameterEditor = new EAP.ModelFirst.Controls.Editors.TypeEditors.DelegateParameterEditor();
            this.kryptonHeaderGroupDelegateInfo = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblComments = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtComments = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblName = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbxAccess = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.lblAccess = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblReturnType = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtReturnType = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupDelegateParameter)).BeginInit();
            this.kryptonHeaderGroupDelegateParameter.Panel.SuspendLayout();
            this.kryptonHeaderGroupDelegateParameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupDelegateInfo)).BeginInit();
            this.kryptonHeaderGroupDelegateInfo.Panel.SuspendLayout();
            this.kryptonHeaderGroupDelegateInfo.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxAccess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonHeaderGroupDelegateParameter
            // 
            this.kryptonHeaderGroupDelegateParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroupDelegateParameter.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kryptonHeaderGroupDelegateParameter.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroupDelegateParameter.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroupDelegateParameter.Location = new System.Drawing.Point(0, 127);
            this.kryptonHeaderGroupDelegateParameter.Name = "kryptonHeaderGroupDelegateParameter";
            // 
            // kryptonHeaderGroupDelegateParameter.Panel
            // 
            this.kryptonHeaderGroupDelegateParameter.Panel.Controls.Add(this.delegateParameterEditor);
            this.kryptonHeaderGroupDelegateParameter.Panel.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonHeaderGroupDelegateParameter.Size = new System.Drawing.Size(400, 173);
            this.kryptonHeaderGroupDelegateParameter.TabIndex = 0;
            this.kryptonHeaderGroupDelegateParameter.ValuesPrimary.Heading = "Delegate Parameter";
            // 
            // delegateParameterEditor
            // 
            this.delegateParameterEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.delegateParameterEditor.Location = new System.Drawing.Point(5, 5);
            this.delegateParameterEditor.Name = "delegateParameterEditor";
            this.delegateParameterEditor.Size = new System.Drawing.Size(388, 142);
            this.delegateParameterEditor.TabIndex = 3;
            // 
            // kryptonHeaderGroupDelegateInfo
            // 
            this.kryptonHeaderGroupDelegateInfo.AutoSize = true;
            this.kryptonHeaderGroupDelegateInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderGroupDelegateInfo.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kryptonHeaderGroupDelegateInfo.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroupDelegateInfo.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroupDelegateInfo.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroupDelegateInfo.Name = "kryptonHeaderGroupDelegateInfo";
            // 
            // kryptonHeaderGroupDelegateInfo.Panel
            // 
            this.kryptonHeaderGroupDelegateInfo.Panel.Controls.Add(this.tableLayoutPanel);
            this.kryptonHeaderGroupDelegateInfo.Size = new System.Drawing.Size(400, 127);
            this.kryptonHeaderGroupDelegateInfo.TabIndex = 1;
            this.kryptonHeaderGroupDelegateInfo.ValuesPrimary.Heading = "Delegate";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel.Controls.Add(this.lblComments, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.txtComments, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.txtName, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.lblName, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.cbxAccess, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.lblAccess, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.lblReturnType, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.txtReturnType, 1, 1);
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.tableLayoutPanel.RowCount = 4;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(293, 104);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // lblComments
            // 
            this.lblComments.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblComments.Location = new System.Drawing.Point(10, 81);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(65, 20);
            this.lblComments.TabIndex = 24;
            this.lblComments.Values.Text = "Comments";
            // 
            // txtComments
            // 
            this.txtComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComments.Location = new System.Drawing.Point(81, 81);
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(194, 20);
            this.txtComments.TabIndex = 23;
            this.txtComments.Validating += new System.ComponentModel.CancelEventHandler(this.txtComments_Validating);
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Location = new System.Drawing.Point(81, 55);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(194, 20);
            this.txtName.TabIndex = 2;
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
            // 
            // lblName
            // 
            this.lblName.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblName.Location = new System.Drawing.Point(35, 55);
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
            this.cbxAccess.Location = new System.Drawing.Point(81, 3);
            this.cbxAccess.Name = "cbxAccess";
            this.cbxAccess.Size = new System.Drawing.Size(194, 20);
            this.cbxAccess.TabIndex = 0;
            this.cbxAccess.SelectedIndexChanged += new System.EventHandler(this.cbxAccess_SelectedIndexChanged);
            // 
            // lblAccess
            // 
            this.lblAccess.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblAccess.Location = new System.Drawing.Point(28, 3);
            this.lblAccess.Name = "lblAccess";
            this.lblAccess.Size = new System.Drawing.Size(47, 20);
            this.lblAccess.TabIndex = 2;
            this.lblAccess.Values.Text = "Access";
            // 
            // lblReturnType
            // 
            this.lblReturnType.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblReturnType.Location = new System.Drawing.Point(3, 29);
            this.lblReturnType.Name = "lblReturnType";
            this.lblReturnType.Size = new System.Drawing.Size(72, 20);
            this.lblReturnType.TabIndex = 8;
            this.lblReturnType.Values.Text = "Return Type";
            // 
            // txtReturnType
            // 
            this.txtReturnType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReturnType.Location = new System.Drawing.Point(81, 29);
            this.txtReturnType.Name = "txtReturnType";
            this.txtReturnType.Size = new System.Drawing.Size(194, 20);
            this.txtReturnType.TabIndex = 1;
            this.txtReturnType.Validating += new System.ComponentModel.CancelEventHandler(this.txtReturnType_Validating);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // DelegateTypeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.kryptonHeaderGroupDelegateParameter);
            this.Controls.Add(this.kryptonHeaderGroupDelegateInfo);
            this.Name = "DelegateTypeEditor";
            this.Size = new System.Drawing.Size(400, 300);
            this.kryptonHeaderGroupDelegateParameter.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupDelegateParameter)).EndInit();
            this.kryptonHeaderGroupDelegateParameter.ResumeLayout(false);
            this.kryptonHeaderGroupDelegateInfo.Panel.ResumeLayout(false);
            this.kryptonHeaderGroupDelegateInfo.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupDelegateInfo)).EndInit();
            this.kryptonHeaderGroupDelegateInfo.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxAccess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroupDelegateParameter;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroupDelegateInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblName;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbxAccess;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAccess;
        private DelegateParameterEditor delegateParameterEditor;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblReturnType;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtReturnType;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblComments;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtComments;
    }
}
