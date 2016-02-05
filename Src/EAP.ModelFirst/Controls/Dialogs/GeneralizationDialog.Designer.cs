namespace EAP.ModelFirst.Controls.Dialogs
{
    partial class GeneralizationDialog
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
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lblStrategy = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbxStrategy = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.cbxStrategy)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Location = new System.Drawing.Point(151, 34);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 26);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Values.Text = "Cancel";
            // 
            // btnOK
            // 
            this.btnOK.AutoSize = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(61, 34);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(84, 26);
            this.btnOK.TabIndex = 3;
            this.btnOK.Values.Text = "Ok";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblStrategy
            // 
            this.lblStrategy.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStrategy.Location = new System.Drawing.Point(3, 3);
            this.lblStrategy.Name = "lblStrategy";
            this.lblStrategy.Size = new System.Drawing.Size(52, 20);
            this.lblStrategy.TabIndex = 4;
            this.lblStrategy.Values.Text = "Strategy";
            // 
            // cbxStrategy
            // 
            this.tableLayoutPanel.SetColumnSpan(this.cbxStrategy, 2);
            this.cbxStrategy.DisplayMember = "Text";
            this.cbxStrategy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxStrategy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStrategy.DropDownWidth = 174;
            this.cbxStrategy.Location = new System.Drawing.Point(61, 3);
            this.cbxStrategy.Name = "cbxStrategy";
            this.cbxStrategy.Size = new System.Drawing.Size(174, 20);
            this.cbxStrategy.TabIndex = 5;
            this.cbxStrategy.ValueMember = "Value";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel.Controls.Add(this.lblStrategy, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.btnCancel, 2, 2);
            this.tableLayoutPanel.Controls.Add(this.cbxStrategy, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.btnOK, 1, 2);
            this.tableLayoutPanel.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(238, 63);
            this.tableLayoutPanel.TabIndex = 6;
            // 
            // GeneralizationDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(257, 81);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GeneralizationDialog";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Edit Generalization";
            ((System.ComponentModel.ISupportInitialize)(this.cbxStrategy)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOK;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblStrategy;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbxStrategy;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    }
}