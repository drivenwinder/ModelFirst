namespace EAP.ModelFirst.Controls.Documents
{
    partial class DiagramDocument
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
            this.canvas = new EAP.ModelFirst.Controls.Editors.DiagramEditor.Canvas();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.AllowDrop = true;
            this.canvas.AutoScroll = true;
            this.canvas.AutoScrollMargin = new System.Drawing.Size(15, 15);
            this.canvas.Diagram = null;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.KryptonContextMenu = null;
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Name = "canvas";
            this.canvas.Offset = new System.Drawing.Point(0, 0);
            this.canvas.Size = new System.Drawing.Size(463, 350);
            this.canvas.TabIndex = 0;
            this.canvas.Zoom = 1F;
            // 
            // DiagramDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(463, 350);
            this.Controls.Add(this.canvas);
            this.DockAreas = ((EAP.Win.UI.DockAreas)((EAP.Win.UI.DockAreas.Float | EAP.Win.UI.DockAreas.Document)));
            this.Name = "DiagramDocument";
            this.TabText = "Diagram";
            this.Text = "Diagram";
            this.Activated += new System.EventHandler(this.DiagramDocument_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DiagramDocument_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private EAP.ModelFirst.Controls.Editors.DiagramEditor.Canvas canvas;

    }
}