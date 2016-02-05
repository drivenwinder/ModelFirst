namespace EAP.ModelFirst.Controls.Explorers
{
    partial class PropertyWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertyWindow));
            this.cbxObjectList = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.propertyObjectBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.grid = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.cbxObjectList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyObjectBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxObjectList
            // 
            this.cbxObjectList.DataSource = this.propertyObjectBindingSource;
            this.cbxObjectList.DisplayMember = "DisplayName";
            this.cbxObjectList.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbxObjectList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxObjectList.DropDownWidth = 292;
            this.cbxObjectList.Location = new System.Drawing.Point(0, 0);
            this.cbxObjectList.Name = "cbxObjectList";
            this.cbxObjectList.Size = new System.Drawing.Size(292, 20);
            this.cbxObjectList.TabIndex = 0;
            this.cbxObjectList.ValueMember = "Object";
            this.cbxObjectList.SelectedIndexChanged += new System.EventHandler(this.cbxObjectList_SelectedIndexChanged);
            // 
            // propertyObjectBindingSource
            // 
            this.propertyObjectBindingSource.DataSource = typeof(EAP.ModelFirst.Controls.Explorers.PropertyObject);
            this.propertyObjectBindingSource.Sort = "DisplayName";
            // 
            // grid
            // 
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 20);
            this.grid.Name = "grid";
            this.grid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.grid.Size = new System.Drawing.Size(292, 247);
            this.grid.TabIndex = 2;
            this.grid.SelectedObjectsChanged += new System.EventHandler(this.grid_SelectedObjectsChanged);
            // 
            // PropertyWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 267);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.cbxObjectList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PropertyWindow";
            this.ShowHint = EAP.Win.UI.DockState.DockRight;
            this.TabText = "Properties";
            this.Text = "Properties";
            ((System.ComponentModel.ISupportInitialize)(this.cbxObjectList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyObjectBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbxObjectList;
        private System.Windows.Forms.PropertyGrid grid;
        private System.Windows.Forms.BindingSource propertyObjectBindingSource;
    }
}