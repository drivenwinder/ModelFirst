namespace EAP.ModelFirst.Controls.Dialogs
{
	partial class AssociationDialog
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
            this.btnOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.cbxEndRole = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.cbxStartRole = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.txtName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.cbxStartMultiplicity = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.picArrow = new System.Windows.Forms.PictureBox();
            this.cbxEndMultiplicity = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.cbxEndRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxStartRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxStartMultiplicity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picArrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxEndMultiplicity)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.AutoSize = true;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(191, 93);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 24);
            this.btnOK.TabIndex = 4;
            this.btnOK.Values.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(272, 93);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Values.Text = "Cancel";
            // 
            // cbxEndRole
            // 
            this.cbxEndRole.DropDownWidth = 125;
            this.cbxEndRole.Location = new System.Drawing.Point(222, 55);
            this.cbxEndRole.Name = "cbxEndRole";
            this.cbxEndRole.Size = new System.Drawing.Size(125, 21);
            this.cbxEndRole.TabIndex = 8;
            // 
            // cbxStartRole
            // 
            this.cbxStartRole.DropDownWidth = 125;
            this.cbxStartRole.Location = new System.Drawing.Point(12, 55);
            this.cbxStartRole.Name = "cbxStartRole";
            this.cbxStartRole.Size = new System.Drawing.Size(125, 21);
            this.cbxStartRole.TabIndex = 7;
            // 
            // txtName
            // 
            this.txtName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtName.Location = new System.Drawing.Point(99, 11);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(160, 20);
            this.txtName.TabIndex = 0;
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cbxStartMultiplicity
            // 
            this.cbxStartMultiplicity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStartMultiplicity.DropDownWidth = 50;
            this.cbxStartMultiplicity.FormattingEnabled = true;
            this.cbxStartMultiplicity.Items.AddRange(new object[] {
            "1",
            "0..1",
            "0..*",
            "1..*",
            "*"});
            this.cbxStartMultiplicity.Location = new System.Drawing.Point(12, 11);
            this.cbxStartMultiplicity.Name = "cbxStartMultiplicity";
            this.cbxStartMultiplicity.Size = new System.Drawing.Size(50, 21);
            this.cbxStartMultiplicity.TabIndex = 6;
            // 
            // picArrow
            // 
            this.picArrow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picArrow.Location = new System.Drawing.Point(12, 36);
            this.picArrow.Name = "picArrow";
            this.picArrow.Size = new System.Drawing.Size(335, 14);
            this.picArrow.TabIndex = 4;
            this.picArrow.TabStop = false;
            this.picArrow.Paint += new System.Windows.Forms.PaintEventHandler(this.picArrow_Paint);
            this.picArrow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picArrow_MouseDown);
            // 
            // cbxEndMultiplicity
            // 
            this.cbxEndMultiplicity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxEndMultiplicity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEndMultiplicity.DropDownWidth = 50;
            this.cbxEndMultiplicity.FormattingEnabled = true;
            this.cbxEndMultiplicity.Items.AddRange(new object[] {
            "1",
            "0..1",
            "0..*",
            "1..*",
            "*"});
            this.cbxEndMultiplicity.Location = new System.Drawing.Point(297, 11);
            this.cbxEndMultiplicity.Name = "cbxEndMultiplicity";
            this.cbxEndMultiplicity.Size = new System.Drawing.Size(50, 21);
            this.cbxEndMultiplicity.TabIndex = 1;
            // 
            // AssociationDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(359, 135);
            this.Controls.Add(this.cbxEndRole);
            this.Controls.Add(this.cbxStartRole);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.cbxStartMultiplicity);
            this.Controls.Add(this.picArrow);
            this.Controls.Add(this.cbxEndMultiplicity);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AssociationDialog";
            this.Text = "Edit Association";
            ((System.ComponentModel.ISupportInitialize)(this.cbxEndRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxStartRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxStartMultiplicity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picArrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxEndMultiplicity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOK;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbxEndMultiplicity;
        private System.Windows.Forms.PictureBox picArrow;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbxStartMultiplicity;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtName;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbxStartRole;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbxEndRole;
	}
}