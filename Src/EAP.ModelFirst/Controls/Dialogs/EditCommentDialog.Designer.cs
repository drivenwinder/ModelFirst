namespace EAP.ModelFirst.Controls.Dialogs
{
	partial class EditCommentDialog
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
			if (disposing && (components != null)) {
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
            this.txtInput = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblEdit = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput.Location = new System.Drawing.Point(5, 21);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(332, 164);
            this.txtInput.TabIndex = 1;
            // 
            // lblEdit
            // 
            this.lblEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEdit.Location = new System.Drawing.Point(5, 5);
            this.lblEdit.Name = "lblEdit";
            this.lblEdit.Size = new System.Drawing.Size(332, 16);
            this.lblEdit.TabIndex = 0;
            this.lblEdit.Values.Text = "Type text:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(254, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 26);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Values.Text = "Cancel";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.AutoSize = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(173, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 26);
            this.btnOK.TabIndex = 2;
            this.btnOK.Values.Text = "OK";
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.AutoSize = true;
            this.kryptonPanel.Controls.Add(this.btnOK);
            this.kryptonPanel.Controls.Add(this.btnCancel);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel.Location = new System.Drawing.Point(5, 185);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(332, 33);
            this.kryptonPanel.TabIndex = 4;
            // 
            // EditCommentDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(342, 223);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.kryptonPanel);
            this.Controls.Add(this.lblEdit);
            this.MinimumSize = new System.Drawing.Size(200, 150);
            this.Name = "EditCommentDialog";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Edit Text";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtInput;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblEdit;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOK;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
	}
}