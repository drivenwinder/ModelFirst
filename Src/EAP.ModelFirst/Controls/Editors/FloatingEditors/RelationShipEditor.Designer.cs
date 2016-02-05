namespace EAP.ModelFirst.Controls.Editors.FloatingEditors
{
    partial class RelationShipEditor
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnAssociation = new System.Windows.Forms.ToolStripButton();
            this.btnComposition = new System.Windows.Forms.ToolStripButton();
            this.btnAggregation = new System.Windows.Forms.ToolStripButton();
            this.btnGeneralization = new System.Windows.Forms.ToolStripButton();
            this.btnRealization = new System.Windows.Forms.ToolStripButton();
            this.btnDependency = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAssociation,
            this.btnComposition,
            this.btnAggregation,
            this.btnGeneralization,
            this.btnRealization,
            this.btnDependency});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.toolStrip.Size = new System.Drawing.Size(177, 25);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "toolStrip1";
            // 
            // btnAssociation
            // 
            this.btnAssociation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAssociation.Image = global::EAP.ModelFirst.Properties.Resources.Association;
            this.btnAssociation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAssociation.Name = "btnAssociation";
            this.btnAssociation.Size = new System.Drawing.Size(23, 22);
            this.btnAssociation.Text = "toolStripButton1";
            this.btnAssociation.Click += new System.EventHandler(this.btnAssociation_Click);
            // 
            // btnComposition
            // 
            this.btnComposition.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnComposition.Image = global::EAP.ModelFirst.Properties.Resources.Composition;
            this.btnComposition.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnComposition.Name = "btnComposition";
            this.btnComposition.Size = new System.Drawing.Size(23, 22);
            this.btnComposition.Text = "toolStripButton2";
            this.btnComposition.Click += new System.EventHandler(this.btnComposition_Click);
            // 
            // btnAggregation
            // 
            this.btnAggregation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAggregation.Image = global::EAP.ModelFirst.Properties.Resources.Aggregation;
            this.btnAggregation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAggregation.Name = "btnAggregation";
            this.btnAggregation.Size = new System.Drawing.Size(23, 22);
            this.btnAggregation.Text = "toolStripButton3";
            this.btnAggregation.Click += new System.EventHandler(this.btnAggregation_Click);
            // 
            // btnGeneralization
            // 
            this.btnGeneralization.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGeneralization.Image = global::EAP.ModelFirst.Properties.Resources.Generalization;
            this.btnGeneralization.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGeneralization.Name = "btnGeneralization";
            this.btnGeneralization.Size = new System.Drawing.Size(23, 22);
            this.btnGeneralization.Text = "toolStripButton4";
            this.btnGeneralization.Click += new System.EventHandler(this.btnGeneralization_Click);
            // 
            // btnRealization
            // 
            this.btnRealization.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRealization.Image = global::EAP.ModelFirst.Properties.Resources.Realization;
            this.btnRealization.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRealization.Name = "btnRealization";
            this.btnRealization.Size = new System.Drawing.Size(23, 22);
            this.btnRealization.Text = "toolStripButton5";
            this.btnRealization.Click += new System.EventHandler(this.btnRealization_Click);
            // 
            // btnDependency
            // 
            this.btnDependency.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDependency.Image = global::EAP.ModelFirst.Properties.Resources.Dependency;
            this.btnDependency.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDependency.Name = "btnDependency";
            this.btnDependency.Size = new System.Drawing.Size(23, 22);
            this.btnDependency.Text = "toolStripButton6";
            this.btnDependency.Click += new System.EventHandler(this.btnDependency_Click);
            // 
            // RelationShipEditor
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.toolStrip);
            this.Name = "RelationShipEditor";
            this.Size = new System.Drawing.Size(177, 25);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnAssociation;
        private System.Windows.Forms.ToolStripButton btnComposition;
        private System.Windows.Forms.ToolStripButton btnAggregation;
        private System.Windows.Forms.ToolStripButton btnGeneralization;
        private System.Windows.Forms.ToolStripButton btnRealization;
        private System.Windows.Forms.ToolStripButton btnDependency;
	}
}
