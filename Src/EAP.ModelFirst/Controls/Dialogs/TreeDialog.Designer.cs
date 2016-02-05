namespace EAP.ModelFirst.Controls.Dialogs
{
	partial class TreeDialog
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
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.treOperations = new ComponentFactory.Krypton.Toolkit.KryptonTreeView();
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(225, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 26);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Values.Text = "Cancel";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.AutoSize = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(144, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 26);
            this.btnOK.TabIndex = 1;
            this.btnOK.Values.Text = "OK";
            // 
            // treOperations
            // 
            this.treOperations.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.InputControlStandalone;
            this.treOperations.BorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.InputControlStandalone;
            this.treOperations.CheckBoxes = true;
            this.treOperations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treOperations.ItemHeight = 17;
            this.treOperations.ItemStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.ListItem;
            this.treOperations.Location = new System.Drawing.Point(5, 5);
            this.treOperations.Name = "treOperations";
            this.treOperations.ShowLines = false;
            this.treOperations.ShowNodeToolTips = true;
            this.treOperations.Size = new System.Drawing.Size(302, 280);
            this.treOperations.TabIndex = 0;
            this.treOperations.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treMembers_AfterCheck);
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.btnCancel);
            this.kryptonPanel.Controls.Add(this.btnOK);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel.Location = new System.Drawing.Point(5, 285);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(302, 33);
            this.kryptonPanel.TabIndex = 3;
            // 
            // TreeDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(312, 323);
            this.Controls.Add(this.treOperations);
            this.Controls.Add(this.kryptonPanel);
            this.MinimumSize = new System.Drawing.Size(300, 350);
            this.Name = "TreeDialog";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Implement Interfaces";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOK;
        private ComponentFactory.Krypton.Toolkit.KryptonTreeView treOperations;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
	}
}