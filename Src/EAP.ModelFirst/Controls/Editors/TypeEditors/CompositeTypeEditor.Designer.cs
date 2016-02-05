namespace EAP.ModelFirst.Controls.Editors.TypeEditors
{
    partial class CompositeTypeEditor
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
            this.tableLayoutPanelTypeInfo = new System.Windows.Forms.TableLayoutPanel();
            this.lblComments = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtComments = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtTableName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.cbxModifier = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.lblAccess = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbxAccess = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.lblModifier = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblName = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblTableName = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.kryptonHeaderGroupTypeInfo = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.kryptonHeaderGroupMembers = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.memberListEditor = new EAP.ModelFirst.Controls.Editors.TypeEditors.MemberListEditor();
            this.tableLayoutPanelTypeInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxModifier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxAccess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupTypeInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupTypeInfo.Panel)).BeginInit();
            this.kryptonHeaderGroupTypeInfo.Panel.SuspendLayout();
            this.kryptonHeaderGroupTypeInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupMembers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupMembers.Panel)).BeginInit();
            this.kryptonHeaderGroupMembers.Panel.SuspendLayout();
            this.kryptonHeaderGroupMembers.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelTypeInfo
            // 
            this.tableLayoutPanelTypeInfo.AutoSize = true;
            this.tableLayoutPanelTypeInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelTypeInfo.ColumnCount = 6;
            this.tableLayoutPanelTypeInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelTypeInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanelTypeInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelTypeInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanelTypeInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelTypeInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanelTypeInfo.Controls.Add(this.lblComments, 2, 1);
            this.tableLayoutPanelTypeInfo.Controls.Add(this.txtComments, 3, 1);
            this.tableLayoutPanelTypeInfo.Controls.Add(this.txtTableName, 5, 0);
            this.tableLayoutPanelTypeInfo.Controls.Add(this.cbxModifier, 3, 0);
            this.tableLayoutPanelTypeInfo.Controls.Add(this.lblAccess, 0, 0);
            this.tableLayoutPanelTypeInfo.Controls.Add(this.cbxAccess, 1, 0);
            this.tableLayoutPanelTypeInfo.Controls.Add(this.lblModifier, 2, 0);
            this.tableLayoutPanelTypeInfo.Controls.Add(this.lblName, 0, 1);
            this.tableLayoutPanelTypeInfo.Controls.Add(this.txtName, 1, 1);
            this.tableLayoutPanelTypeInfo.Controls.Add(this.lblTableName, 4, 0);
            this.tableLayoutPanelTypeInfo.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelTypeInfo.Name = "tableLayoutPanelTypeInfo";
            this.tableLayoutPanelTypeInfo.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.tableLayoutPanelTypeInfo.RowCount = 3;
            this.tableLayoutPanelTypeInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelTypeInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelTypeInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelTypeInfo.Size = new System.Drawing.Size(826, 53);
            this.tableLayoutPanelTypeInfo.TabIndex = 0;
            // 
            // lblComments
            // 
            this.lblComments.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblComments.Location = new System.Drawing.Point(256, 30);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(70, 20);
            this.lblComments.TabIndex = 20;
            this.lblComments.Values.Text = "Comments";
            // 
            // txtComments
            // 
            this.tableLayoutPanelTypeInfo.SetColumnSpan(this.txtComments, 3);
            this.txtComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComments.Location = new System.Drawing.Point(332, 30);
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(476, 20);
            this.txtComments.TabIndex = 19;
            this.txtComments.Validating += new System.ComponentModel.CancelEventHandler(this.txtComments_Validating);
            // 
            // txtTableName
            // 
            this.txtTableName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTableName.Location = new System.Drawing.Point(614, 3);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(194, 20);
            this.txtTableName.TabIndex = 7;
            this.txtTableName.Validating += new System.ComponentModel.CancelEventHandler(this.TxtTableNameValidating);
            // 
            // cbxModifier
            // 
            this.cbxModifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxModifier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxModifier.DropDownWidth = 244;
            this.cbxModifier.Location = new System.Drawing.Point(332, 3);
            this.cbxModifier.Name = "cbxModifier";
            this.cbxModifier.Size = new System.Drawing.Size(194, 21);
            this.cbxModifier.TabIndex = 1;
            this.cbxModifier.SelectedIndexChanged += new System.EventHandler(this.cbxModifier_SelectedIndexChanged);
            this.cbxModifier.Validated += new System.EventHandler(this.cbxModifier_Validated);
            // 
            // lblAccess
            // 
            this.lblAccess.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblAccess.Location = new System.Drawing.Point(3, 3);
            this.lblAccess.Name = "lblAccess";
            this.lblAccess.Size = new System.Drawing.Size(47, 21);
            this.lblAccess.TabIndex = 1;
            this.lblAccess.Values.Text = "Access";
            // 
            // cbxAccess
            // 
            this.cbxAccess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxAccess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAccess.DropDownWidth = 214;
            this.cbxAccess.Location = new System.Drawing.Point(56, 3);
            this.cbxAccess.Name = "cbxAccess";
            this.cbxAccess.Size = new System.Drawing.Size(194, 21);
            this.cbxAccess.TabIndex = 0;
            this.cbxAccess.SelectedIndexChanged += new System.EventHandler(this.cbxAccess_SelectedIndexChanged);
            this.cbxAccess.Validated += new System.EventHandler(this.cbxModifier_Validated);
            // 
            // lblModifier
            // 
            this.lblModifier.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblModifier.Location = new System.Drawing.Point(269, 3);
            this.lblModifier.Name = "lblModifier";
            this.lblModifier.Size = new System.Drawing.Size(57, 21);
            this.lblModifier.TabIndex = 3;
            this.lblModifier.Values.Text = "Modifier";
            // 
            // lblName
            // 
            this.lblName.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblName.Location = new System.Drawing.Point(7, 30);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(43, 20);
            this.lblName.TabIndex = 5;
            this.lblName.Values.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Location = new System.Drawing.Point(56, 30);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(194, 20);
            this.txtName.TabIndex = 2;
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
            // 
            // lblTableName
            // 
            this.lblTableName.Location = new System.Drawing.Point(532, 3);
            this.lblTableName.Name = "lblTableName";
            this.lblTableName.Size = new System.Drawing.Size(76, 20);
            this.lblTableName.TabIndex = 6;
            this.lblTableName.Values.Text = "Table Name";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // kryptonHeaderGroupTypeInfo
            // 
            this.kryptonHeaderGroupTypeInfo.AutoSize = true;
            this.kryptonHeaderGroupTypeInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonHeaderGroupTypeInfo.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kryptonHeaderGroupTypeInfo.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroupTypeInfo.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroupTypeInfo.Location = new System.Drawing.Point(0, 0);
            this.kryptonHeaderGroupTypeInfo.Name = "kryptonHeaderGroupTypeInfo";
            // 
            // kryptonHeaderGroupTypeInfo.Panel
            // 
            this.kryptonHeaderGroupTypeInfo.Panel.Controls.Add(this.tableLayoutPanelTypeInfo);
            this.kryptonHeaderGroupTypeInfo.Size = new System.Drawing.Size(820, 78);
            this.kryptonHeaderGroupTypeInfo.TabIndex = 0;
            this.kryptonHeaderGroupTypeInfo.ValuesPrimary.Heading = "Type Info.";
            // 
            // kryptonHeaderGroupMembers
            // 
            this.kryptonHeaderGroupMembers.AutoSize = true;
            this.kryptonHeaderGroupMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroupMembers.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.kryptonHeaderGroupMembers.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.kryptonHeaderGroupMembers.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroupMembers.Location = new System.Drawing.Point(0, 78);
            this.kryptonHeaderGroupMembers.Name = "kryptonHeaderGroupMembers";
            // 
            // kryptonHeaderGroupMembers.Panel
            // 
            this.kryptonHeaderGroupMembers.Panel.Controls.Add(this.memberListEditor);
            this.kryptonHeaderGroupMembers.Panel.Padding = new System.Windows.Forms.Padding(5);
            this.kryptonHeaderGroupMembers.Size = new System.Drawing.Size(820, 422);
            this.kryptonHeaderGroupMembers.TabIndex = 1;
            this.kryptonHeaderGroupMembers.ValuesPrimary.Heading = "Members";
            // 
            // memberListEditor
            // 
            this.memberListEditor.AutoSize = true;
            this.memberListEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memberListEditor.Location = new System.Drawing.Point(5, 5);
            this.memberListEditor.Name = "memberListEditor";
            this.memberListEditor.Size = new System.Drawing.Size(808, 389);
            this.memberListEditor.TabIndex = 3;
            // 
            // CompositeTypeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(820, 500);
            this.AutoSize = true;
            this.Controls.Add(this.kryptonHeaderGroupMembers);
            this.Controls.Add(this.kryptonHeaderGroupTypeInfo);
            this.Name = "CompositeTypeEditor";
            this.Size = new System.Drawing.Size(820, 500);
            this.tableLayoutPanelTypeInfo.ResumeLayout(false);
            this.tableLayoutPanelTypeInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxModifier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxAccess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupTypeInfo.Panel)).EndInit();
            this.kryptonHeaderGroupTypeInfo.Panel.ResumeLayout(false);
            this.kryptonHeaderGroupTypeInfo.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupTypeInfo)).EndInit();
            this.kryptonHeaderGroupTypeInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupMembers.Panel)).EndInit();
            this.kryptonHeaderGroupMembers.Panel.ResumeLayout(false);
            this.kryptonHeaderGroupMembers.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroupMembers)).EndInit();
            this.kryptonHeaderGroupMembers.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblTableName;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtTableName;

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTypeInfo;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAccess;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbxAccess;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblModifier;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbxModifier;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblName;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtName;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private MemberListEditor memberListEditor;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroupTypeInfo;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroupMembers;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblComments;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtComments;
    }
}
