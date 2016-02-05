using System.Drawing;
using System.Windows.Forms;

namespace EAP.ModelFirst.Controls.Editors.FloatingEditors
{
    [System.ComponentModel.ToolboxItem(false)]
	public class PopupWindow : UserControl
	{
		const int ClientMargin = 20;
		
		Point parentLocation;

		public new Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				if (Parent != null)
				{
					Rectangle client = Parent.ClientRectangle;

					if (value.X < ClientMargin)
						value.X = ClientMargin;
					if (value.Y < ClientMargin)
						value.Y = ClientMargin;
					if (value.X + Width > client.Width - ClientMargin)
						value.X = client.Width - Width - ClientMargin;
					if (value.Y + Height > client.Height - ClientMargin)
						value.Y = client.Height - Height - ClientMargin;
				}
				base.Location = value;
			}
		}

		internal Point ParentLocation
		{
			get
			{
				return parentLocation;
			}
			set
			{
				Size offset = new Size(value.X - parentLocation.X, value.Y - parentLocation.Y);
				parentLocation = value;
				this.Location += offset;
			}
		}

		public virtual void Closing()
		{
		}
	}
}
