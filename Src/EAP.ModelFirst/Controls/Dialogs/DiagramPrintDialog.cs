﻿using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using EAP.ModelFirst.Controls.Editors.DiagramEditor;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;

namespace EAP.ModelFirst.Controls.Dialogs
{
    public partial class DiagramPrintDialog : DialogForm
	{
		IPrintable document = null;
		int pageIndex = 0;
		int rows = 1;
		int columns = 1;
		bool selectedOnly = false;
		Style selectedStyle = Style.CurrentStyle;
		Style printingStyle = null;

		public DiagramPrintDialog()
		{
			InitializeComponent();
			printPreview.AutoZoom = true;
			printDocument.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);
		}

        public IPrintable Document
		{
			get { return document; }
			set { document = value; }
		}

		private int PageCount
		{
			get { return rows * columns; }
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			UpdateTexts();
			LoadSettings();
			LoadStyles();
		}

		private void UpdateTexts()
		{
			this.Text = Strings.Print;
			btnPrinter.Text = Strings.ButtonSelectPrinter;
			btnPageSetup.Text = Strings.ButtonPageSetup;
			lblStyle.Text = Strings.Style;
			lblPages.Text = Strings.Pages;
			chkSelectedOnly.Text = Strings.PrintOnlySelectedElements;
			btnPrint.Text = Strings.ButtonPrint;
			btnCancel.Text = Strings.ButtonCancel;

			int buttonWidth = Math.Max(btnPrinter.Width, btnPageSetup.Width);
			btnPrinter.Width = buttonWidth;
			btnPageSetup.Width = buttonWidth;

			int minLeft = btnPrinter.Left + buttonWidth + 6;
			lblStyle.Left = minLeft;
			lblPages.Left = minLeft;

			minLeft = Math.Max(lblStyle.Right, lblPages.Right);
			cboStyle.Left = minLeft + 6;
			numColumns.Left = minLeft + 6;
			lblX.Left = numColumns.Right + 1;
			numRows.Left = lblX.Right + 1;
			chkSelectedOnly.Left = numRows.Right + 14;
		}

		private void LoadSettings()
		{
            PrintingSettings settings = Properties.Settings.Default.PrintingSettings;
			if (settings != null)
			{
				printDocument.DefaultPageSettings.Landscape = settings.Landscape;
				printDocument.DefaultPageSettings.Margins = settings.Margins;
				printDocument.DefaultPageSettings.PaperSize = settings.PaperSize;
				printDocument.DefaultPageSettings.PaperSource = settings.PaperSource;
				printDocument.DefaultPageSettings.PrinterSettings.PrinterName = settings.PrinterName;
			}

			if (!pageSetupDialog.PrinterSettings.IsValid)
			{
				printDocument.PrinterSettings = new PrinterSettings();
				pageSetupDialog.Document = printDocument;
			}
		}

		private void LoadStyles()
		{
			cboStyle.Items.Clear();
			foreach (Style style in Style.AvaiableStyles)
			{
				cboStyle.Items.Add(style);
				if (style == Style.CurrentStyle)
					cboStyle.SelectedItem = style;
			}
		}

		private void SaveSettings()
		{
			Settings.Default.PrintingSettings = new PrintingSettings()
			{
				Landscape = printDocument.DefaultPageSettings.Landscape,
				Margins = printDocument.DefaultPageSettings.Margins,
				PaperSize = printDocument.DefaultPageSettings.PaperSize,
				PaperSource = printDocument.DefaultPageSettings.PaperSource,
				PrinterName = printDocument.PrinterSettings.PrinterName
			};
			Settings.Default.Save();
		}

		public new DialogResult ShowDialog()
		{
			return ShowDialog(null);
		}

		public new DialogResult ShowDialog(IWin32Window owner)
		{
			if (printDocument.PrinterSettings.IsValid)
			{
				printPreview.InvalidatePreview();
				return base.ShowDialog(owner);
			}
			else
			{
                Client.ShowInfo(Strings.ErrorNoPrinters);
				return DialogResult.Cancel;
			}
		}

		private void Print()
		{
			try
			{
				printDocument.Print();
				SaveSettings();
			}
			catch (InvalidPrinterException ex)
            {
                Client.ShowInfo(string.Format(Strings.ErrorPrinting, ex.Message));
			}
		}

		private static Style MakeShadowsOpaque(Style selectedStyle)
		{
			Style converted = selectedStyle.Clone();
			converted.ShadowColor = DisableTransparency(converted.ShadowColor);
			return converted;
		}

		private static Color DisableTransparency(Color color)
		{
			int red = color.R * color.A / 255 + (255 - color.A);
			int green = color.G * color.A / 255 + (255 - color.A);
			int blue = color.B * color.A / 255 + (255 - color.A);

			return Color.FromArgb(red, green, blue);
		}

		private void printDocument_BeginPrint(object sender, PrintEventArgs e)
		{
			if (document != null && printDocument.PrinterSettings.IsValid)
			{
				pageIndex = 0;
				printingStyle = MakeShadowsOpaque(selectedStyle);
                printDocument.DocumentName = document.DocumentName;
			}
			else
			{
				e.Cancel = true;
			}
		}

        private Graphics Graphics
        {
            get
            {
                return Graphics.FromHwnd(System.IntPtr.Zero);
            }
        }

		private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
		{
			// Scale the page to match sizes of the screen
			e.Graphics.PageUnit = GraphicsUnit.Inch;
			e.Graphics.PageScale = 1 / Graphics.DpiX;

			// Get the phisical page margins
			float marginScale = Graphics.DpiX / 100;
			RectangleF marginBounds = e.MarginBounds;
			if (!printDocument.PrintController.IsPreview)
				marginBounds.Offset(-e.PageSettings.HardMarginX, -e.PageSettings.HardMarginY);
			marginBounds = new RectangleF(
				marginBounds.X * marginScale, marginBounds.Y * marginScale,
				marginBounds.Width * marginScale, marginBounds.Height * marginScale);

			// Get logical area information
			RectangleF drawingArea = document.GetPrintingArea(selectedOnly);
			int column = pageIndex % columns;
			int row = pageIndex / columns;

			// Get zooming information if diagram is too big
			float scaleX = columns * marginBounds.Width / drawingArea.Width;
			float scaleY = rows * marginBounds.Height / drawingArea.Height;
			float scale = Math.Min(scaleX, scaleY);
			if (scale > 1) scale = 1; // No need for zooming in

			// Set the printing clip region
			RectangleF clipBounds = marginBounds;
			if (column == 0)
			{
				clipBounds.X = 0;
				clipBounds.Width += marginBounds.Left;
			}
			if (row == 0)
			{
				clipBounds.Y = 0;
				clipBounds.Height += marginBounds.Top;
			}
			if (column == columns - 1)
			{
				clipBounds.Width += marginBounds.Left;
			}
			if (row == rows - 1)
			{
				clipBounds.Height += marginBounds.Top;
			}
			e.Graphics.SetClip(clipBounds);

			// Moving the image to it's right position
			e.Graphics.TranslateTransform(-column * marginBounds.Width, -row * marginBounds.Height);
			e.Graphics.TranslateTransform(marginBounds.Left, marginBounds.Top);
			e.Graphics.ScaleTransform(scale, scale);
			e.Graphics.TranslateTransform(-drawingArea.Left, -drawingArea.Top);
			
			// Printing
			IGraphics graphics = new GdiGraphics(e.Graphics);
			document.Print(graphics, selectedOnly, printingStyle);
			e.HasMorePages = (++pageIndex < PageCount);
		}

		private void printDocument_EndPrint(object sender, PrintEventArgs e)
		{
			if (printingStyle != null)
			{
				printingStyle.Dispose();
				printingStyle = null;
			}
		}

		private void printPreview_Click(object sender, EventArgs e)
		{
			if (printPreview.AutoZoom)
			{
				printPreview.AutoZoom = false;
				printPreview.Zoom = 1.0;
			}
			else
			{
				printPreview.AutoZoom = true;
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			Print();
		}

		private void btnPrinter_Click(object sender, EventArgs e)
		{
			if (selectPrinterDialog.ShowDialog() == DialogResult.OK)
			{
				Print();
				this.Close();
			}
		}

		private void btnPageSetup_Click(object sender, EventArgs e)
		{
			Margins originalMargins = pageSetupDialog.PageSettings.Margins;

			if (System.Globalization.RegionInfo.CurrentRegion.IsMetric && !MonoHelper.IsRunningOnMono)
			{
				// This is necessary because of a bug in PageSetupDialog control.
				// More information: http://support.microsoft.com/?id=814355
				pageSetupDialog.PageSettings.Margins = PrinterUnitConvert.Convert(
					pageSetupDialog.PageSettings.Margins,
					PrinterUnit.Display, PrinterUnit.TenthsOfAMillimeter);
			}

			if (pageSetupDialog.ShowDialog() == DialogResult.OK)
				printPreview.InvalidatePreview();
			else
				pageSetupDialog.PageSettings.Margins = originalMargins;
		}

		private void cboStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			Style style = cboStyle.SelectedItem as Style;
			if (style != null)
			{
				selectedStyle = style;
				printPreview.InvalidatePreview();
			}
		}

		private void numColumns_ValueChanged(object sender, EventArgs e)
		{
			columns = (int) numColumns.Value;
			printPreview.Columns = columns;
			printPreview.AutoZoom = true;
			printPreview.InvalidatePreview();
		}

		private void numRows_ValueChanged(object sender, EventArgs e)
		{
			rows = (int) numRows.Value;
			printPreview.Rows = rows;
			printPreview.AutoZoom = true;
			printPreview.InvalidatePreview();
		}

		private void chkSelectedOnly_CheckedChanged(object sender, EventArgs e)
		{
			selectedOnly = (chkSelectedOnly.Checked);
			printPreview.InvalidatePreview();
		}
	}
}