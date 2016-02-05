using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace EAP.ModelFirst.Controls.Editors
{
    public class TemplateEditor : KryptonRichTextBox
    {
        public event EventHandler ZoomChanged;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 256) // 
            {
                OnZoomChanged(EventArgs.Empty);
            }
            if (!base.IsDisposed)
            {
                base.WndProc(ref m);
            }
        }

        protected virtual void OnZoomChanged(EventArgs e)
        {
            if (ZoomChanged != null)
                ZoomChanged(this, e);
        }

        public void Indent()
        {
            int start = SelectionStart;
            int index = Math.Max(Text.LastIndexOf('\n', Math.Min(start, TextLength - 1)), 0);
            Select(index, start - index + SelectionLength);
            var selectedText = SelectedText.Replace("\n", "\n\t");
            if(index == 0)
                selectedText = "\t" + selectedText;
            if (selectedText.EndsWith("\n\t"))
                selectedText = selectedText.TrimEnd('\t');
            SelectedText = selectedText;
            Select(start + 1, selectedText.Length - start + index - 1);
        }

        public void Outdent()
        {
            int start = SelectionStart;
            int index = Text.LastIndexOf('\n', Math.Min(start, TextLength - 1));
            var decreased = TextLength > index + 1 && (Text[index + 1] == ' ' || Text[index + 1] == '\t');
            var begin = Math.Max(index, 0);
            Select(begin, start - begin + SelectionLength);
            var selectedText = SelectedText.Replace("\n ", "\n").Replace("\n\t", "\n");
            if (index == -1 && (selectedText.StartsWith(" ") || selectedText.StartsWith("\t")))
                selectedText = selectedText.Remove(0, 1);
            SelectedText = selectedText;
            if (decreased && start > 0)
                start--;
            Select(start, selectedText.Length - start + begin);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                if (SelectionLength > 0)
                {
                    Indent();
                    return true;
                }
            }
            else if (keyData == (Keys.Shift | Keys.Tab))
            {
                if (SelectionLength > 0)
                {
                    Outdent();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.KeyData == Keys.Enter)
            {
                Select(SelectionStart, 0);//Call Select to create the restore point for undo
            }
        }

        public void Delete()
        {
            int selectionStart = SelectionStart;
            if (SelectionLength == 0)
                Select(selectionStart, 1);
            SelectedText = "";
            SelectionStart = selectionStart;
        }
    }
}
