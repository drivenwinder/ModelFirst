using System;
using System.Drawing;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes
{
	public sealed class InterfaceShape : CompositeTypeShape
	{
		InterfaceType _interface;

		/// <exception cref="ArgumentNullException">
		/// <paramref name="interfaceType"/> is null.
		/// </exception>
		internal InterfaceShape(InterfaceType interfaceType)
			: base(interfaceType)
		{
			_interface = interfaceType;
			UpdateMinSize();
		}

		public override CompositeType CompositeType
		{
			get { return _interface; }
		}

		public InterfaceType InterfaceType
		{
			get { return _interface; }
		}

		protected override bool CloneEntity(Diagram diagram)
		{
			return diagram.InsertInterface(InterfaceType.Clone());
		}

		protected override Color GetBackgroundColor(Style style)
		{
            return style.InterfaceBackgroundColor;
		}

		protected override Color GetBorderColor(Style style)
		{
			return style.InterfaceBorderColor;
		}

		protected override int GetBorderWidth(Style style)
		{
			return style.InterfaceBorderWidth;
		}

		protected override bool IsBorderDashed(Style style)
		{
			return style.IsInterfaceBorderDashed;
		}

		protected override Color GetHeaderColor(Style style)
		{
            return style.InterfaceHeaderColor;
		}

		protected override int GetRoundingSize(Style style)
		{
			return style.InterfaceRoundingSize;
		}

		protected override GradientStyle GetGradientHeaderStyle(Style style)
		{
			return style.InterfaceGradientHeaderStyle;
		}
	}
}
