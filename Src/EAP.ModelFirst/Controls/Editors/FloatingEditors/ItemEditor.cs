using System;
using System.ComponentModel;
using System.Windows.Forms;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;

namespace EAP.ModelFirst.Controls.Editors.FloatingEditors
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ItemEditor : EditorWindow
    {
        bool needValidation = false;

        public abstract class ItemEditorHandler : FloatingEditorHandler
        {
            ItemEditor editor { get { return Window as ItemEditor; } }

            internal abstract override void Relocate(DiagramElement element);

            protected internal abstract void HideEditor();

            protected internal abstract void RefreshValues();

            protected internal abstract bool ValidateDeclarationLine();

            protected internal abstract void SelectPrevious();

            protected internal abstract void SelectNext();

            protected internal abstract void MoveUp();

            protected internal abstract void MoveDown();

            protected internal abstract void Delete();

            internal override void Init(DiagramElement element)
            {
                RefreshValues();
            }

            public override void ValidateData()
            {
                ValidateDeclarationLine();
                editor.SetError(null);
            }
        }

        ItemEditorHandler editor { get { return EditorHandler as ItemEditorHandler; } }

        private ItemEditor()
            :this(null)
        {
        }

        public ItemEditor(ItemEditorHandler handler)
            :base(handler)
        {
            InitializeComponent();
            UpdateTexts();
            toolStrip.Renderer = ToolStripSimplifiedRenderer.Default;
        }

        protected string DeclarationText
        {
            get { return txtDeclaration.Text; }
            set { txtDeclaration.Text = value; }
        }

        protected int SelectionStart
        {
            get { return txtDeclaration.SelectionStart; }
            set { txtDeclaration.SelectionStart = value; }
        }

        protected bool NeedValidation
        {
            get { return needValidation; }
            set { needValidation = value; }
        }

        protected virtual void UpdateTexts()
        {
            toolMoveUp.ToolTipText = Strings.MoveUp + " (Ctrl+Up)";
            toolMoveDown.ToolTipText = Strings.MoveDown + " (Ctrl+Down)";
            toolDelete.ToolTipText = Strings.Delete;
        }

        protected void SetError(string message)
        {
            if (MonoHelper.IsRunningOnMono && MonoHelper.IsOlderVersionThan("2.4"))
                return;

            errorProvider.SetError(this, message);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            txtDeclaration.SelectionStart = 0;
        }

        private void txtDeclaration_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    editor.ValidateDeclarationLine();
                    e.Handled = true;
                    break;

                case Keys.Escape:
                    needValidation = false;
                    editor.HideEditor();
                    e.Handled = true;
                    break;

                case Keys.Up:
                    if (e.Shift || e.Control)
                        editor.MoveUp();
                    else
                        editor.SelectPrevious();
                    e.Handled = true;
                    break;

                case Keys.Down:
                    if (e.Shift || e.Control)
                        editor.MoveDown();
                    else
                        editor.SelectNext();
                    e.Handled = true;
                    break;
            }
        }

        private void txtDeclaration_TextChanged(object sender, EventArgs e)
        {
            needValidation = true;
        }

        private void txtDeclaration_Validating(object sender, CancelEventArgs e)
        {
            editor.ValidateDeclarationLine();
        }

        private void toolMoveUp_Click(object sender, EventArgs e)
        {
            editor.MoveUp();
        }

        private void toolMoveDown_Click(object sender, EventArgs e)
        {
            editor.MoveDown();
        }

        private void toolDelete_Click(object sender, EventArgs e)
        {
            editor.Delete();
        }
    }
}
