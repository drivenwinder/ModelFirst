using System;
using System.Drawing;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Relationships;

namespace EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Connections
{
	internal sealed class Nesting : Connection
	{
		const int Radius = 9;
		const int Diameter = Radius * 2;
		const int CrossSize = 8;
		static Pen linePen = new Pen(Color.Black);

		NestingRelationship nesting;

		/// <exception cref="ArgumentNullException">
		/// <paramref name="nesting"/> is null.-or-
		/// <paramref name="startShape"/> is null.-or-
		/// <paramref name="endShape"/> is null.
		/// </exception>
		public Nesting(NestingRelationship nesting, Shape startShape, Shape endShape)
			: base(nesting, startShape, endShape)
		{
			this.nesting = nesting;
		}

		internal NestingRelationship NestingRelationship
		{
			get { return nesting; }
		}

		protected internal override Relationship Relationship
		{
			get { return nesting; }
		}

		protected override Size StartCapSize
		{
			get { return new Size(Diameter, Diameter); }
		}

		protected override int StartSelectionOffset
		{
			get { return Diameter; }
		}

		protected override void DrawStartCap(IGraphics g, bool onScreen, Style style)
		{
			linePen.Color = style.RelationshipColor;
			linePen.Width = style.RelationshipWidth;

			g.FillEllipse(Brushes.White, -Radius, 0, Diameter, Diameter);
			g.DrawEllipse(linePen, -Radius, 0, Diameter, Diameter);
			g.DrawLine(linePen, 0, Radius - CrossSize / 2, 0, Radius + CrossSize / 2);
			g.DrawLine(linePen, -CrossSize / 2, Radius, CrossSize / 2, Radius);
		}

		protected override bool CloneRelationship(Diagram diagram, Shape first, Shape second)
		{
			CompositeType firstType = first.Entity as CompositeType;
			TypeBase secondType = second.Entity as TypeBase;
			
			if (firstType != null && secondType != null)
			{
				NestingRelationship clone = nesting.Clone(firstType, secondType);
				return diagram.InsertNesting(clone);
			}
			else
			{
				return false;
			}
		}
	}
}
