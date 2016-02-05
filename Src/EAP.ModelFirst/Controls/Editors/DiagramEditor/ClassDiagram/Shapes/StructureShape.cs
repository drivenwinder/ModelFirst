using System;
using System.Drawing;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes
{
	internal sealed class StructureShape : CompositeTypeShape
	{
		StructureType structure;

		/// <exception cref="ArgumentNullException">
		/// <paramref name="structType"/> is null.
		/// </exception>
		internal StructureShape(StructureType structure) : base(structure)
		{
			this.structure = structure;
			UpdateMinSize();
		}

		public override CompositeType CompositeType
		{
			get { return structure; }
		}

		public StructureType StructureType
		{
			get { return structure; }
		}

		protected override bool CloneEntity(Diagram diagram)
		{
			return diagram.InsertStructure(StructureType.Clone());
		}

		protected override Color GetBackgroundColor(Style style)
		{
            return style.StructureBackgroundColor;
		}

		protected override Color GetBorderColor(Style style)
		{
			return style.StructureBorderColor;
		}

		protected override int GetBorderWidth(Style style)
		{
			return style.StructureBorderWidth;
		}

		protected override bool IsBorderDashed(Style style)
		{
			return style.IsStructureBorderDashed;
		}

		protected override Color GetHeaderColor(Style style)
		{
            return style.StructureHeaderColor;
		}

		protected override int GetRoundingSize(Style style)
		{
			return style.StructureRoundingSize;
		}

		protected override GradientStyle GetGradientHeaderStyle(Style style)
		{
			return style.StructureGradientHeaderStyle;
		}
	}
}
