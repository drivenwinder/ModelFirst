﻿using System;

namespace EAP.ModelFirst.Controls.Editors.DiagramEditor
{
	partial class Canvas
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // Canvas
            // 
            this.AllowDrop = true;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(15, 15);
            this.Name = "Canvas";
            this.ResumeLayout(false);

		}

		#endregion

	}
}
