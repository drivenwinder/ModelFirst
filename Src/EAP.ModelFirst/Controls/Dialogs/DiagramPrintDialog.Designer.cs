namespace EAP.ModelFirst.Controls.Dialogs
{
	partial class DiagramPrintDialog
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
            this.pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.chkSelectedOnly = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.lblPages = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblStyle = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.selectPrinterDialog = new System.Windows.Forms.PrintDialog();
            this.cboStyle = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnPrint = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnPrinter = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lblX = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.numColumns = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.numRows = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.btnPageSetup = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.printPreview = new System.Windows.Forms.PrintPreviewControl();
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.cboStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pageSetupDialog
            // 
            this.pageSetupDialog.Document = this.printDocument;
            // 
            // printDocument
            // 
            this.printDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument_BeginPrint);
            this.printDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument_EndPrint);
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
            // 
            // chkSelectedOnly
            // 
            this.chkSelectedOnly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkSelectedOnly.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalControl;
            this.chkSelectedOnly.Location = new System.Drawing.Point(156, 29);
            this.chkSelectedOnly.Name = "chkSelectedOnly";
            this.chkSelectedOnly.Size = new System.Drawing.Size(183, 19);
            this.chkSelectedOnly.TabIndex = 9;
            this.chkSelectedOnly.Text = "Print only the selected elements";
            this.chkSelectedOnly.Values.Text = "Print only the selected elements";
            this.chkSelectedOnly.CheckedChanged += new System.EventHandler(this.chkSelectedOnly_CheckedChanged);
            // 
            // lblPages
            // 
            this.lblPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPages.Location = new System.Drawing.Point(3, 29);
            this.lblPages.Name = "lblPages";
            this.lblPages.Size = new System.Drawing.Size(45, 19);
            this.lblPages.TabIndex = 5;
            this.lblPages.Values.Text = "Pages:";
            // 
            // lblStyle
            // 
            this.lblStyle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStyle.Location = new System.Drawing.Point(3, 3);
            this.lblStyle.Name = "lblStyle";
            this.lblStyle.Size = new System.Drawing.Size(45, 20);
            this.lblStyle.TabIndex = 3;
            this.lblStyle.Values.Text = "Style:";
            // 
            // selectPrinterDialog
            // 
            this.selectPrinterDialog.AllowPrintToFile = false;
            this.selectPrinterDialog.Document = this.printDocument;
            this.selectPrinterDialog.UseEXDialog = true;
            // 
            // cboStyle
            // 
            this.cboStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel1.SetColumnSpan(this.cboStyle, 4);
            this.cboStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStyle.DropDownWidth = 217;
            this.cboStyle.FormattingEnabled = true;
            this.cboStyle.Location = new System.Drawing.Point(54, 3);
            this.cboStyle.Name = "cboStyle";
            this.cboStyle.Size = new System.Drawing.Size(217, 20);
            this.cboStyle.TabIndex = 4;
            this.cboStyle.SelectedIndexChanged += new System.EventHandler(this.cboStyle_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(596, 37);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 26);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Values.Text = "Cancel";
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.AutoSize = true;
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnPrint.Location = new System.Drawing.Point(515, 37);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 26);
            this.btnPrint.TabIndex = 10;
            this.btnPrint.Values.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPrinter
            // 
            this.btnPrinter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrinter.AutoSize = true;
            this.btnPrinter.Location = new System.Drawing.Point(10, 7);
            this.btnPrinter.Name = "btnPrinter";
            this.btnPrinter.Size = new System.Drawing.Size(128, 26);
            this.btnPrinter.TabIndex = 1;
            this.btnPrinter.Values.Text = "Select printer...";
            this.btnPrinter.Click += new System.EventHandler(this.btnPrinter_Click);
            // 
            // lblX
            // 
            this.lblX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblX.Location = new System.Drawing.Point(94, 29);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(16, 19);
            this.lblX.TabIndex = 7;
            this.lblX.Values.Text = "x";
            // 
            // numColumns
            // 
            this.numColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numColumns.Location = new System.Drawing.Point(54, 29);
            this.numColumns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numColumns.Name = "numColumns";
            this.numColumns.Size = new System.Drawing.Size(34, 19);
            this.numColumns.TabIndex = 8;
            this.numColumns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numColumns.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numColumns.ValueChanged += new System.EventHandler(this.numColumns_ValueChanged);
            // 
            // numRows
            // 
            this.numRows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numRows.Location = new System.Drawing.Point(116, 29);
            this.numRows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRows.Name = "numRows";
            this.numRows.Size = new System.Drawing.Size(34, 19);
            this.numRows.TabIndex = 6;
            this.numRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numRows.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRows.ValueChanged += new System.EventHandler(this.numRows_ValueChanged);
            // 
            // btnPageSetup
            // 
            this.btnPageSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPageSetup.AutoSize = true;
            this.btnPageSetup.Location = new System.Drawing.Point(10, 37);
            this.btnPageSetup.Name = "btnPageSetup";
            this.btnPageSetup.Size = new System.Drawing.Size(128, 26);
            this.btnPageSetup.TabIndex = 2;
            this.btnPageSetup.Values.Text = "Page setup...";
            this.btnPageSetup.Click += new System.EventHandler(this.btnPageSetup_Click);
            // 
            // printPreview
            // 
            this.printPreview.AutoZoom = false;
            this.printPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printPreview.Document = this.printDocument;
            this.printPreview.Location = new System.Drawing.Point(5, 5);
            this.printPreview.Name = "printPreview";
            this.printPreview.Size = new System.Drawing.Size(682, 480);
            this.printPreview.TabIndex = 0;
            this.printPreview.UseAntiAlias = true;
            this.printPreview.Zoom = 0.40718562874251496D;
            this.printPreview.Click += new System.EventHandler(this.printPreview_Click);
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel.Controls.Add(this.btnPrint);
            this.kryptonPanel.Controls.Add(this.btnCancel);
            this.kryptonPanel.Controls.Add(this.btnPageSetup);
            this.kryptonPanel.Controls.Add(this.btnPrinter);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel.Location = new System.Drawing.Point(5, 485);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(682, 76);
            this.kryptonPanel.TabIndex = 12;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblPages, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cboStyle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.numColumns, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.chkSelectedOnly, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblX, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.numRows, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblStyle, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(150, 11);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(342, 51);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // DiagramPrintDialog
            // 
            this.AcceptButton = this.btnPrint;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(692, 566);
            this.Controls.Add(this.printPreview);
            this.Controls.Add(this.kryptonPanel);
            this.Name = "DiagramPrintDialog";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Print";
            ((System.ComponentModel.ISupportInitialize)(this.cboStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PageSetupDialog pageSetupDialog;
		private System.Drawing.Printing.PrintDocument printDocument;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkSelectedOnly;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblPages;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblStyle;
		private System.Windows.Forms.PrintDialog selectPrinterDialog;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cboStyle;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnPrint;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnPrinter;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblX;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown numColumns;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown numRows;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnPageSetup;
		private System.Windows.Forms.PrintPreviewControl printPreview;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}