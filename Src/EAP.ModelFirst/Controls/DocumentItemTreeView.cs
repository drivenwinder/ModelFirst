using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAP.ModelFirst.Core;
using ComponentFactory.Krypton.Toolkit;
using System.ComponentModel;
using EAP.ModelFirst.Utils;
using System.Windows.Forms;
using System.Drawing;

namespace EAP.ModelFirst.Controls
{
    [DesignerCategory("Component")]
    public class DocumentItemTreeView : KryptonTreeView
    {
        public DocumentItemTreeView()
        {
            TreeView.Tag = this;
            LabelEdit = true;
            var font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            StateNormal.Node.Content.ShortText.Font = font;
            StateCommon.Node.Content.ShortText.Font = font;
            StateDisabled.Node.Content.ShortText.Font = font;
            StatePressed.Node.Content.ShortText.Font = font;
            StatePressed.Node.Content.ShortText.Font = font;
        }

        public IDockForm DockForm
        {
            get;
            set;
        }

        #region OnChanged

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);
            ItemNode node = (ItemNode)e.Node;
            node.Click();
            if (!node.IsSelected)
                SelectedNode = node;
        }

        protected override void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseDoubleClick(e);
            ItemNode node = (ItemNode)e.Node;
            node.DoubleClick();
        }

        protected override void OnAfterCollapse(TreeViewEventArgs e)
        {
            base.OnAfterCollapse(e);
            ItemNode node = (ItemNode)e.Node;
            node.StateChanged();
        }

        protected override void OnAfterExpand(TreeViewEventArgs e)
        {
            base.OnAfterExpand(e);
            ItemNode node = (ItemNode)e.Node;
            node.StateChanged();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Enter)
            {
                ItemNode selectedNode = SelectedNode as ItemNode;
                if (selectedNode != null)
                    selectedNode.EnterPressed();
            }
            else if (e.KeyCode == Keys.F2)
            {
                ItemNode selectedNode = SelectedNode as ItemNode;
                if (selectedNode != null)
                    selectedNode.EditLabel();
            }
        }

        protected override void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
        {
            base.OnBeforeLabelEdit(e);

            ItemNode node = (ItemNode)e.Node;
            if (!node.EditingLabel)
                e.CancelEdit = true;
        }

        protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
        {
            base.OnAfterLabelEdit(e);

            ItemNode node = (ItemNode)e.Node;
            try
            {
                if (!e.CancelEdit && e.Label != null)
                {
                    node.LabelModified(e);
                    if (!e.CancelEdit)
                    {
                        node.Text = e.Label;
                        Sort();
                        SelectedNode = node;
                    }
                }
                node.LabelEdited();
            }
            catch (Exception exc)
            {
                Client.ShowInfo(exc.Message);
                e.CancelEdit = true;//因为已排序,所以要取消,否则其它Node的Lable会被修改.
                node.LabelEdited();
                node.EditLabel();
            }
        }

        #endregion
        
        protected virtual Color GetTrackingBackColor()
        {
            return StateTracking.Node.PaletteBack.GetBackColor1(PaletteState.Tracking);
        }

        protected virtual Color GetNormalBackColor()
        {
            return SystemColors.Window;
            //return StateNormal.Node.PaletteBack.GetBackColor1(PaletteState.Normal);
        }

        protected virtual Color GetTrackingForeColor()
        {
            return StateTracking.Node.PaletteContent.GetContentShortTextColor1(PaletteState.Tracking);
        }

        protected virtual Color GetNormalForeColor()
        {
            return StateNormal.Node.PaletteContent.GetContentShortTextColor1(PaletteState.Normal);
        }
    }
}
