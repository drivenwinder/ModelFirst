using System.Drawing;
using System.Windows.Forms;
using EAP.ModelFirst.Core.Project.Members;

namespace EAP.ModelFirst.Controls.Editors.FloatingEditors
{
	public abstract class FloatingEditorHandler : EditorHandler
	{
        EditorWindow window;
		protected const int MarginSize = 20;
		static readonly Color beginColor = SystemColors.ControlLight;
		static readonly Color endColor = SystemColors.Control;

		static MemberType newMemberType = MemberType.Method;

        public override EditorWindow Window
        {
            get { return window; }
            set
            {
                window = value;
                window.BackColor = System.Drawing.SystemColors.Control;
                window.Padding = new Padding(1);
            }
        }

		protected internal static MemberType NewMemberType
		{
			get { return newMemberType; }
			set { newMemberType = value; }
		}

        protected internal override void OnWindowPaintBackground(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(SystemPens.ControlDark, 0, 0, Window.Width - 1, Window.Height - 1);
        }
    }
}
