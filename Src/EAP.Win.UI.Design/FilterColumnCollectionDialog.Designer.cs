using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace EAP.Win.UI.Design
{
    public partial class FilterColumnCollectionDialog
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FilterColumnCollectionDialog));
            this.addButton = new Button();
            this.deleteButton = new Button();
            this.moveDown = new Button();
            this.moveUp = new Button();
            this.selectedColumns = new ListBox();
            this.overarchingTableLayoutPanel = new TableLayoutPanel();
            this.addRemoveTableLayoutPanel = new TableLayoutPanel();
            this.selectedColumnsLabel = new Label();
            this.propertyGridLabel = new Label();
            this.propertyGrid1 = new VsPropertyGrid(this.serviceProvider);
            this.okCancelTableLayoutPanel = new TableLayoutPanel();
            this.cancelButton = new Button();
            this.okButton = new Button();
            this.overarchingTableLayoutPanel.SuspendLayout();
            this.addRemoveTableLayoutPanel.SuspendLayout();
            this.okCancelTableLayoutPanel.SuspendLayout();
            base.SuspendLayout();
            manager.ApplyResources(this.addButton, "addButton");
            this.addButton.Margin = new Padding(0, 0, 3, 0);
            this.addButton.Name = "addButton";
            this.addButton.Padding = new Padding(10, 0, 10, 0);
            this.addButton.Click += new EventHandler(this.addButton_Click);
            manager.ApplyResources(this.deleteButton, "deleteButton");
            this.deleteButton.Margin = new Padding(3, 0, 0, 0);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Padding = new Padding(10, 0, 10, 0);
            this.deleteButton.Click += new EventHandler(this.deleteButton_Click);
            manager.ApplyResources(this.moveDown, "moveDown");
            this.moveDown.Margin = new Padding(0, 1, 0x12, 0);
            this.moveDown.Name = "moveDown";
            this.moveDown.Click += new EventHandler(this.moveDown_Click);
            manager.ApplyResources(this.moveUp, "moveUp");
            this.moveUp.Margin = new Padding(0, 0, 0x12, 1);
            this.moveUp.Name = "moveUp";
            this.moveUp.Click += new EventHandler(this.moveUp_Click);
            manager.ApplyResources(this.selectedColumns, "selectedColumns");
            this.selectedColumns.DrawMode = DrawMode.OwnerDrawFixed;
            this.selectedColumns.Margin = new Padding(0, 2, 3, 3);
            this.selectedColumns.Name = "selectedColumns";
            this.overarchingTableLayoutPanel.SetRowSpan(this.selectedColumns, 2);
            this.selectedColumns.SelectedIndexChanged += new EventHandler(this.selectedColumns_SelectedIndexChanged);
            this.selectedColumns.KeyPress += new KeyPressEventHandler(this.selectedColumns_KeyPress);
            this.selectedColumns.DrawItem += new DrawItemEventHandler(this.selectedColumns_DrawItem);
            this.selectedColumns.KeyUp += new KeyEventHandler(this.selectedColumns_KeyUp);
            manager.ApplyResources(this.overarchingTableLayoutPanel, "overarchingTableLayoutPanel");
            this.overarchingTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            this.overarchingTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            this.overarchingTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent));
            this.overarchingTableLayoutPanel.Controls.Add(this.addRemoveTableLayoutPanel, 0, 3);
            this.overarchingTableLayoutPanel.Controls.Add(this.moveUp, 1, 1);
            this.overarchingTableLayoutPanel.Controls.Add(this.selectedColumnsLabel, 0, 0);
            this.overarchingTableLayoutPanel.Controls.Add(this.moveDown, 1, 2);
            this.overarchingTableLayoutPanel.Controls.Add(this.propertyGridLabel, 2, 0);
            this.overarchingTableLayoutPanel.Controls.Add(this.selectedColumns, 0, 1);
            this.overarchingTableLayoutPanel.Controls.Add(this.propertyGrid1, 2, 1);
            this.overarchingTableLayoutPanel.Controls.Add(this.okCancelTableLayoutPanel, 0, 4);
            this.overarchingTableLayoutPanel.Name = "overarchingTableLayoutPanel";
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            this.overarchingTableLayoutPanel.RowStyles.Add(new RowStyle());
            manager.ApplyResources(this.addRemoveTableLayoutPanel, "addRemoveTableLayoutPanel");
            this.addRemoveTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.addRemoveTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.addRemoveTableLayoutPanel.Controls.Add(this.addButton, 0, 0);
            this.addRemoveTableLayoutPanel.Controls.Add(this.deleteButton, 1, 0);
            this.addRemoveTableLayoutPanel.Margin = new Padding(0, 3, 3, 3);
            this.addRemoveTableLayoutPanel.Name = "addRemoveTableLayoutPanel";
            this.addRemoveTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            manager.ApplyResources(this.selectedColumnsLabel, "selectedColumnsLabel");
            this.selectedColumnsLabel.Margin = new Padding(0);
            this.selectedColumnsLabel.Name = "selectedColumnsLabel";
            manager.ApplyResources(this.propertyGridLabel, "propertyGridLabel");
            this.propertyGridLabel.Margin = new Padding(3, 0, 0, 0);
            this.propertyGridLabel.Name = "propertyGridLabel";
            manager.ApplyResources(this.propertyGrid1, "propertyGrid1");
            this.propertyGrid1.LineColor = SystemColors.ScrollBar;
            this.propertyGrid1.Margin = new Padding(3, 2, 0, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.overarchingTableLayoutPanel.SetRowSpan(this.propertyGrid1, 3);
            this.propertyGrid1.PropertyValueChanged += new PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            manager.ApplyResources(this.okCancelTableLayoutPanel, "okCancelTableLayoutPanel");
            this.okCancelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.okCancelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.okCancelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
            this.okCancelTableLayoutPanel.Controls.Add(this.cancelButton, 1, 0);
            this.okCancelTableLayoutPanel.Controls.Add(this.okButton, 0, 0);
            this.okCancelTableLayoutPanel.Name = "okCancelTableLayoutPanel";
            this.overarchingTableLayoutPanel.SetColumnSpan(this.okCancelTableLayoutPanel, 3);
            this.okCancelTableLayoutPanel.RowStyles.Add(new RowStyle());
            manager.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.DialogResult = DialogResult.Cancel;
            this.cancelButton.Margin = new Padding(3, 0, 0, 0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Padding = new Padding(10, 0, 10, 0);
            manager.ApplyResources(this.okButton, "okButton");
            this.okButton.DialogResult = DialogResult.OK;
            this.okButton.Margin = new Padding(0, 0, 3, 0);
            this.okButton.Name = "okButton";
            this.okButton.Padding = new Padding(10, 0, 10, 0);
            this.okButton.Click += new EventHandler(this.okButton_Click);
            base.AcceptButton = this.okButton;
            manager.ApplyResources(this, "$this");
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.cancelButton;
            base.Controls.Add(this.overarchingTableLayoutPanel);
            base.HelpButton = true;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FilterColumnCollectionDialog";
            base.Padding = new Padding(12);
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.HelpButtonClicked += new CancelEventHandler(this.FilterColumnCollectionDialog_HelpButtonClicked);
            base.Closed += new EventHandler(this.FilterColumnCollectionDialog_Closed);
            base.Load += new EventHandler(this.FilterColumnCollectionDialog_Load);
            base.HelpRequested += new HelpEventHandler(this.FilterColumnCollectionDialog_HelpRequested);
            this.overarchingTableLayoutPanel.ResumeLayout(false);
            this.overarchingTableLayoutPanel.PerformLayout();
            this.addRemoveTableLayoutPanel.ResumeLayout(false);
            this.addRemoveTableLayoutPanel.PerformLayout();
            this.okCancelTableLayoutPanel.ResumeLayout(false);
            this.okCancelTableLayoutPanel.PerformLayout();
            base.ResumeLayout(false);
        }
    }
}
