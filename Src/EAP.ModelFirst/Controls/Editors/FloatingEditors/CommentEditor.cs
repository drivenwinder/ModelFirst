using System;
using System.Drawing;
using System.Windows.Forms;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Utils;

namespace EAP.ModelFirst.Controls.Editors.FloatingEditors
{
    [System.ComponentModel.ToolboxItem(false)]
	public sealed partial class CommentEditor : EditorWindow
	{
		CommentShape shape = null;

        class CommentEditorHandler : EditorHandler
        {
            CommentEditor editor { get { return Window as CommentEditor; } }

            internal override void Init(DiagramElement element)
            {
                editor.Init((CommentShape)element);
            }

            internal override void Relocate(DiagramElement element)
            {
                editor.Relocate((CommentShape)element);
            }

            public override void ValidateData()
            {
                editor.shape.Comment.Text = editor.txtComment.Text;
            }
        }

        public CommentEditor()
            : base(new CommentEditorHandler())
		{
			InitializeComponent();
		}

		protected override void OnLocationChanged(EventArgs e)
		{
			base.OnLocationChanged(e);
		}

        internal void Init(CommentShape element)
        {
            shape = element;
            txtComment.BackColor = shape.GetBackColor(Style.CurrentStyle);
            txtComment.ForeColor = Style.CurrentStyle.CommentTextColor;
            txtComment.Text = shape.Comment.Text;

            Font font = Style.CurrentStyle.CommentFont;
            txtComment.Font = new Font(font.FontFamily,
                font.SizeInPoints * shape.Diagram.Zoom, font.Style);
        }

		internal void Relocate(CommentShape shape)
		{
			Diagram diagram = shape.Diagram;
			if (diagram != null)
			{
				Rectangle absolute = shape.GetTextRectangle();
				// The following lines are required because of a .NET bug:
				// http://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=380085
				if (!MonoHelper.IsRunningOnMono)
				{
					absolute.X -= 3;
					absolute.Width += 3;
				}
				
				this.SetBounds(
					(int) (absolute.X * diagram.Zoom) - diagram.Offset.X + ParentLocation.X,
					(int) (absolute.Y * diagram.Zoom) - diagram.Offset.Y + ParentLocation.Y,
					(int) (absolute.Width * diagram.Zoom),
					(int) (absolute.Height * diagram.Zoom));
			}
		}

		private void txtComment_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter && e.Modifiers != Keys.None ||
				e.KeyCode == Keys.Escape)
			{
				shape.HideEditor();
			}
		}

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.A))
            {
                txtComment.SelectAll();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.C))
            {
                txtComment.Copy();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.X))
            {
                txtComment.Cut();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.V))
            {
                txtComment.Paste();
                return true;
            }
            else if (keyData == Keys.Delete)
            {
                //应该删除文字,而不是删除图形!!
                //txtComment
                return true;
            }
            else if (keyData == Keys.Enter && txtComment.AcceptsReturn)
            {
                OnKeyDown(new KeyEventArgs(keyData));
                return true;
            }
            else if (keyData == Keys.Tab && txtComment.AcceptsTab)
            {
                OnKeyDown(new KeyEventArgs(keyData));
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
	}
}
