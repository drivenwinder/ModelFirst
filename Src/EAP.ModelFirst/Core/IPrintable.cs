using System;
using System.Drawing;

namespace EAP.ModelFirst.Core
{
	public interface IPrintable
	{
        string DocumentName { get; }
		/// <exception cref="ArgumentNullException">
		/// <paramref name="g"/> is null.-or-
		/// <paramref name="style"/> is null.
		/// </exception>
		void Print(IGraphics g, bool selectedOnly, Style style);

		RectangleF GetPrintingArea(bool selectedOnly);
	}
}
