using System;
using System.Windows.Forms;

namespace EAP.ModelFirst.Controls
{
	public class ToolStripSimplifiedRenderer : ToolStripProfessionalRenderer
	{
		static ToolStripSimplifiedRenderer renderer = new ToolStripSimplifiedRenderer();

		private ToolStripSimplifiedRenderer()
		{
		}

		public static ToolStripSimplifiedRenderer Default
		{
			get { return renderer; }
		}

		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			if (e.ToolStrip is ToolStripDropDown)
				base.OnRenderToolStripBackground(e);
		}

		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			if (e.ToolStrip is ToolStripDropDown)
				base.OnRenderToolStripBorder(e);
		}
	}
}
