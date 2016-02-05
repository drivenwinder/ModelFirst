namespace EAP.ModelFirst.Controls.Explorers
{
    partial class Navigator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Navigator));
            this.diagramNavigator = new EAP.ModelFirst.Controls.DiagramNavigator();
            this.SuspendLayout();
            // 
            // diagramNavigator
            // 
            this.diagramNavigator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diagramNavigator.DocumentVisualizer = null;
            this.diagramNavigator.Location = new System.Drawing.Point(0, 0);
            this.diagramNavigator.Name = "diagramNavigator";
            this.diagramNavigator.Size = new System.Drawing.Size(292, 246);
            this.diagramNavigator.TabIndex = 0;
            this.diagramNavigator.Text = "diagramNavigator";
            // 
            // Navigator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 246);
            this.Controls.Add(this.diagramNavigator);
            this.DockAreas = ((EAP.Win.UI.DockAreas)(((((EAP.Win.UI.DockAreas.Float | EAP.Win.UI.DockAreas.DockLeft) 
            | EAP.Win.UI.DockAreas.DockRight) 
            | EAP.Win.UI.DockAreas.DockTop) 
            | EAP.Win.UI.DockAreas.DockBottom)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Navigator";
            this.ShowHint = EAP.Win.UI.DockState.DockRight;
            this.TabText = "Naviagtor";
            this.Text = "Naviagtor";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.DiagramNavigator diagramNavigator;

    }
}