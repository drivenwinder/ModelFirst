using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Utils;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Controls.Editors.TypeEditors;

namespace EAP.ModelFirst.Controls.Dialogs
{
    public partial class TypeBaseDialog : DialogForm
    {
        TypeBase type;
        TypeEditor editor;
        public TypeBaseDialog()
        {
            InitializeComponent();
        }

        public void ShowDialog(TypeBase typeBase, bool memberOnly = false)
        {
            type = typeBase;
            TypeBase copy = null;

            if (typeBase is CompositeType)
            {
                if (type is ClassType)
                    copy = ((ClassType)type).Clone();
                else if (type is StructureType)
                    copy = ((StructureType)type).Clone();
                else if (type is InterfaceType)
                    copy = ((InterfaceType)type).Clone();
                copy.ProjectInfo = type.ProjectInfo;
                editor = new CompositeTypeEditor().SetCompositeType((CompositeType)copy, memberOnly);
                MinimumSize = new Size(790, 610);
                Size = new Size(1000, 700);
            }
            else if (typeBase is EnumType)
            {
                copy = ((EnumType)type).Clone();
                copy.ProjectInfo = type.ProjectInfo;
                editor = new EnumTypeEditor().SetEnumType((EnumType)copy, memberOnly);
            }
            else if (typeBase is DelegateType)
            {
                copy = ((DelegateType)type).Clone();
                copy.ProjectInfo = type.ProjectInfo;
                editor = new DelegateTypeEditor().SetDelegateType((DelegateType)copy, memberOnly);
            }
            else
                throw new NotSupportedException(typeBase.GetType().FullName);

            UpdateButtons();
            editor.EditStateChanged += new EventHandler(editor_EditStateChanged);

            editor.Dock = System.Windows.Forms.DockStyle.Fill;
            kryptonPanelMain.Controls.Add(editor);
            ShowDialog();
        }

        void editor_EditStateChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        void UpdateButtons()
        {
            copyToolStripButton.Enabled = editor.CanCopyToClipboard;
            cutToolStripButton.Enabled = editor.CanCutToClipboard;
            pasteToolStripButton.Enabled = editor.CanPasteFromClipboard;
            undoToolStripButton.Enabled = editor.CanUndo;
            redoToolStripButton.Enabled = editor.CanRedo;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            type.Deserialize(Serializer.Serialize(editor.TypeBase));
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            if (editor.CanCopyToClipboard)
                editor.Copy();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            if (editor.CanCutToClipboard)
                editor.Cut();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            if (editor.CanPasteFromClipboard)
                editor.Paste();
        }

        private void undoToolStripButton_Click(object sender, EventArgs e)
        {
            if (editor.CanUndo)
                editor.Undo();
        }

        private void redoToolStripButton_Click(object sender, EventArgs e)
        {
            if (editor.CanRedo)
                editor.Redo();
        }
    }
}
