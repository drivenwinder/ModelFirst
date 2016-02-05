using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Relationships;

namespace EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Connections
{
	internal sealed class Realization : Connection
	{
		static Pen linePen = new Pen(Color.Black);

		RealizationRelationship realization;

		static Realization()
		{
			linePen.MiterLimit = 2.0F;
			linePen.LineJoin = LineJoin.MiterClipped;
		}

		/// <exception cref="ArgumentNullException">
		/// <paramref name="realization"/> is null.-or-
		/// <paramref name="startShape"/> is null.-or-
		/// <paramref name="endShape"/> is null.
		/// </exception>
		public Realization(RealizationRelationship realization, Shape startShape, Shape endShape)
			: base(realization, startShape, endShape)
		{
			this.realization = realization;
		}

		internal RealizationRelationship RealizationRelationship
		{
			get { return realization; }
		}

		protected internal override Relationship Relationship
		{
			get { return realization; }
		}

		protected override bool IsDashed
		{
			get { return true; }
		}

		protected override Size EndCapSize
		{
			get { return Arrowhead.ClosedArrowSize; }
		}

		protected override int EndSelectionOffset
		{
			get { return Arrowhead.ClosedArrowHeight; }
		}

		protected override void DrawEndCap(IGraphics g, bool onScreen, Style style)
		{
			linePen.Color = style.RelationshipColor;
			linePen.Width = style.RelationshipWidth;

			g.FillPath(Brushes.White, Arrowhead.ClosedArrowPath);
			g.DrawPath(linePen, Arrowhead.ClosedArrowPath);
		}

		protected override bool CloneRelationship(Diagram diagram, Shape first, Shape second)
		{
			TypeBase firstType = first.Entity as TypeBase;
			InterfaceType secondType = second.Entity as InterfaceType;

			if (firstType != null && secondType != null)
			{
				RealizationRelationship clone = realization.Clone(firstType, secondType);
				return diagram.InsertRealization(clone);
			}
			else
			{
				return false;
			}
		}
	}
}
