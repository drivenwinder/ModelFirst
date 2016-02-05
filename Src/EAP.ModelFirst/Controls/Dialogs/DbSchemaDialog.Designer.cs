namespace EAP.ModelFirst.Controls.Dialogs
{
    partial class DbSchemaDialog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.btnOk = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnClose = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnUndo = new System.Windows.Forms.ToolStripButton();
            this.btnRedo = new System.Windows.Forms.ToolStripButton();
            this.gridView = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.dbTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dbSchemaModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemberName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemberType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGenerateDbColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDbType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNotNull = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colPrimaryKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colAutoIncrement = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colIndex = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDefaultValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colComments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
            this.panel.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbSchemaModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.btnOk);
            this.panel.Controls.Add(this.btnClose);
            this.panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel.Location = new System.Drawing.Point(5, 552);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1006, 36);
            this.panel.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.AutoSize = true;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(848, 10);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 11;
            this.btnOk.Values.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoSize = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(929, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Values.Text = "Cancel";
            // 
            // toolStrip
            // 
            this.toolStrip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnUndo,
            this.btnRedo});
            this.toolStrip.Location = new System.Drawing.Point(5, 5);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1006, 25);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip1";
            // 
            // btnUndo
            // 
            this.btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUndo.Image = global::EAP.ModelFirst.Properties.Resources.Undo;
            this.btnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(23, 22);
            this.btnUndo.Text = "Undo";
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRedo.Image = global::EAP.ModelFirst.Properties.Resources.Redo;
            this.btnRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(23, 22);
            this.btnRedo.Text = "Redo";
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // gridView
            // 
            this.gridView.AllowUserToAddRows = false;
            this.gridView.AllowUserToDeleteRows = false;
            this.gridView.AutoGenerateColumns = false;
            this.gridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTypeName,
            this.colTableName,
            this.colMemberName,
            this.colMemberType,
            this.colGenerateDbColumn,
            this.colColumnName,
            this.colDbType,
            this.colLength,
            this.colNotNull,
            this.colPrimaryKey,
            this.colAutoIncrement,
            this.colIndex,
            this.colDefaultValue,
            this.colComments});
            this.gridView.DataSource = this.dbSchemaModelBindingSource;
            this.gridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridView.Location = new System.Drawing.Point(5, 30);
            this.gridView.Name = "gridView";
            this.gridView.Size = new System.Drawing.Size(1006, 522);
            this.gridView.StateNormal.Background.Color1 = System.Drawing.SystemColors.AppWorkspace;
            this.gridView.TabIndex = 3;
            this.gridView.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView_CellValidated);
            this.gridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.gridView_DataBindingComplete);
            this.gridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gridView_DataError);
            // 
            // dbTypeBindingSource
            // 
            this.dbTypeBindingSource.DataSource = typeof(EAP.Collections.ValueTextPair);
            // 
            // dbSchemaModelBindingSource
            // 
            this.dbSchemaModelBindingSource.DataSource = typeof(EAP.ModelFirst.Controls.Dialogs.DbSchemaModel);
            // 
            // colTypeName
            // 
            this.colTypeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colTypeName.DataPropertyName = "TypeName";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.colTypeName.DefaultCellStyle = dataGridViewCellStyle1;
            this.colTypeName.HeaderText = "Type";
            this.colTypeName.Name = "colTypeName";
            this.colTypeName.ReadOnly = true;
            this.colTypeName.ToolTipText = "Type Name";
            this.colTypeName.Width = 60;
            // 
            // colTableName
            // 
            this.colTableName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colTableName.DataPropertyName = "TableName";
            this.colTableName.HeaderText = "Table";
            this.colTableName.Name = "colTableName";
            this.colTableName.ToolTipText = "Table Name";
            this.colTableName.Width = 63;
            // 
            // colMemberName
            // 
            this.colMemberName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colMemberName.DataPropertyName = "MemberName";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.colMemberName.DefaultCellStyle = dataGridViewCellStyle2;
            this.colMemberName.HeaderText = "Member";
            this.colMemberName.Name = "colMemberName";
            this.colMemberName.ReadOnly = true;
            this.colMemberName.ToolTipText = "Member Name";
            this.colMemberName.Width = 74;
            // 
            // colMemberType
            // 
            this.colMemberType.DataPropertyName = "MemberType";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.colMemberType.DefaultCellStyle = dataGridViewCellStyle3;
            this.colMemberType.HeaderText = "Member Type";
            this.colMemberType.Name = "colMemberType";
            this.colMemberType.ReadOnly = true;
            this.colMemberType.Width = 101;
            // 
            // colGenerateDbColumn
            // 
            this.colGenerateDbColumn.DataPropertyName = "GenerateDbColumn";
            this.colGenerateDbColumn.HeaderText = "DB";
            this.colGenerateDbColumn.Name = "colGenerateDbColumn";
            this.colGenerateDbColumn.ToolTipText = "Generate DbColumn";
            this.colGenerateDbColumn.Width = 32;
            // 
            // colColumnName
            // 
            this.colColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colColumnName.DataPropertyName = "ColumnName";
            this.colColumnName.HeaderText = "Column";
            this.colColumnName.Name = "colColumnName";
            this.colColumnName.ToolTipText = "Column Name";
            this.colColumnName.Width = 71;
            // 
            // colDbType
            // 
            this.colDbType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colDbType.DataPropertyName = "DbType";
            this.colDbType.DataSource = this.dbTypeBindingSource;
            this.colDbType.DisplayMember = "Text";
            this.colDbType.HeaderText = "DbType";
            this.colDbType.Name = "colDbType";
            this.colDbType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDbType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colDbType.ToolTipText = "DbType";
            this.colDbType.ValueMember = "Value";
            this.colDbType.Width = 74;
            // 
            // colLength
            // 
            this.colLength.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colLength.DataPropertyName = "Length";
            this.colLength.HeaderText = "Length";
            this.colLength.Name = "colLength";
            this.colLength.ToolTipText = "Length";
            this.colLength.Width = 69;
            // 
            // colNotNull
            // 
            this.colNotNull.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colNotNull.DataPropertyName = "NotNull";
            this.colNotNull.HeaderText = "NN";
            this.colNotNull.Name = "colNotNull";
            this.colNotNull.ToolTipText = "Not Null";
            this.colNotNull.Width = 33;
            // 
            // colPrimaryKey
            // 
            this.colPrimaryKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colPrimaryKey.DataPropertyName = "PrimaryKey";
            this.colPrimaryKey.HeaderText = "PK";
            this.colPrimaryKey.Name = "colPrimaryKey";
            this.colPrimaryKey.ToolTipText = "Primary Key";
            this.colPrimaryKey.Width = 31;
            // 
            // colAutoIncrement
            // 
            this.colAutoIncrement.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colAutoIncrement.DataPropertyName = "AutoIncrement";
            this.colAutoIncrement.HeaderText = "AI";
            this.colAutoIncrement.Name = "colAutoIncrement";
            this.colAutoIncrement.ToolTipText = "Auto Increment";
            this.colAutoIncrement.Width = 27;
            // 
            // colIndex
            // 
            this.colIndex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colIndex.DataPropertyName = "Index";
            this.colIndex.HeaderText = "IDX";
            this.colIndex.Name = "colIndex";
            this.colIndex.ToolTipText = "Index";
            this.colIndex.Width = 35;
            // 
            // colDefaultValue
            // 
            this.colDefaultValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colDefaultValue.DataPropertyName = "DefaultValue";
            this.colDefaultValue.HeaderText = "Value";
            this.colDefaultValue.Name = "colDefaultValue";
            this.colDefaultValue.ToolTipText = "Default Value";
            this.colDefaultValue.Width = 63;
            // 
            // colComments
            // 
            this.colComments.DataPropertyName = "Comments";
            this.colComments.HeaderText = "Comments";
            this.colComments.Name = "colComments";
            this.colComments.ToolTipText = "Comments";
            this.colComments.Width = 85;
            // 
            // DbSchemaDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 593);
            this.Controls.Add(this.gridView);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.panel);
            this.Name = "DbSchemaDialog";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DB Schema Info";
            ((System.ComponentModel.ISupportInitialize)(this.panel)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbSchemaModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel panel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClose;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnUndo;
        private System.Windows.Forms.ToolStripButton btnRedo;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView gridView;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOk;
        private System.Windows.Forms.BindingSource dbSchemaModelBindingSource;
        private System.Windows.Forms.BindingSource dbTypeBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemberName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemberType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colGenerateDbColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColumnName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colDbType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLength;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colNotNull;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colPrimaryKey;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colAutoIncrement;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDefaultValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComments;
    }
}