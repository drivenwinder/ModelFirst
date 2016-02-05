using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Controls
{
    public partial class DockContent : EAP.Win.UI.DockContent, ILocalizable
    {
        public DockContent()
        {
            InitializeComponent();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (HideOnClose)
                Hide();
            else
                Close();
        }

        private void closeOtherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var d in DockHandler.DockPanel.Documents.ToList())
                if (d != this)
                {
                    if (d.DockHandler.HideOnClose)
                        d.DockHandler.Hide();
                    else
                        d.DockHandler.Close();
                }
        }

        protected override void OnDockStateChanged(EventArgs e)
        {
            base.OnDockStateChanged(e);
            if (DockState == Win.UI.DockState.Document)
                TabPageContextMenuStrip = tabPageContextMenuStrip;
            else
                TabPageContextMenuStrip = null;
        }

        public virtual void UpdateTexts()
        {
            closeToolStripMenuItem.Text = Strings.MenuCloseTab;
            closeOtherToolStripMenuItem.Text = Strings.MenuCloseAllTabsButThis;
        }

        private void tabPageContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            closeOtherToolStripMenuItem.Enabled = DockHandler.DockPanel.Documents.Any(p => p != this);
        }
    }
}
