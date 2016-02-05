using System;
using System.Drawing;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes
{
	public sealed class ClassShape : CompositeTypeShape
	{
		ClassType _class;

		/// <exception cref="ArgumentNullException">
		/// <paramref name="classType"/> is null.
		/// </exception>
		internal ClassShape(ClassType classType)
			: base(classType)
		{
			_class = classType;
			UpdateMinSize();
		}

		public override CompositeType CompositeType
		{
			get { return _class; }
		}

		public ClassType ClassType
		{
			get { return _class; }
		}

		protected override bool CloneEntity(Diagram diagram)
		{
			return diagram.InsertClass(ClassType.Clone());
		}

		protected override Color GetBackgroundColor(Style style)
		{
            return style.ClassBackgroundColor;
		}

		protected override Color GetBorderColor(Style style)
		{
			return style.ClassBorderColor;
		}

		protected override int GetBorderWidth(Style style)
		{
			switch (_class.Modifier)
			{
				case ClassModifier.Abstract:
					return style.AbstractClassBorderWidth;

				case ClassModifier.Sealed:
					return style.SealedClassBorderWidth;

				case ClassModifier.Static:
					return style.StaticClassBorderWidth;

				case ClassModifier.None:
				default:
					return style.ClassBorderWidth;
			}
		}

		protected override bool IsBorderDashed(Style style)
		{
			switch (_class.Modifier)
			{
				case ClassModifier.Abstract:
					return style.IsAbstractClassBorderDashed;

				case ClassModifier.Sealed:
					return style.IsSealedClassBorderDashed;

				case ClassModifier.Static:
					return style.IsStaticClassBorderDashed;

				case ClassModifier.None:
				default:
					return style.IsClassBorderDashed;
			}
		}

		protected override Color GetHeaderColor(Style style)
		{
            return style.ClassHeaderColor;
		}

		protected override Font GetNameFont(Style style)
		{
			if (_class.Modifier == ClassModifier.Abstract)
				return style.AbstractNameFont;
			else
				return base.GetNameFont(style);
		}

		protected override int GetRoundingSize(Style style)
		{
			return style.ClassRoundingSize;
		}

		protected override GradientStyle GetGradientHeaderStyle(Style style)
		{
			return style.ClassGradientHeaderStyle;
		}
	}
}
