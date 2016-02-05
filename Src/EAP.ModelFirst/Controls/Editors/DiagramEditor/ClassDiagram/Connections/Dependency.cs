using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Relationships;

namespace EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Connections
{
	internal sealed class Dependency : Connection
	{
		static Pen linePen = new Pen(Color.Black);

		DependencyRelationship dependency;

		static Dependency()
		{
			linePen.MiterLimit = 2.0F;
			linePen.LineJoin = LineJoin.MiterClipped;
		}

		/// <exception cref="ArgumentNullException">
		/// <paramref name="dependency"/> is null.-or-
		/// <paramref name="startShape"/> is null.-or-
		/// <paramref name="endShape"/> is null.
		/// </exception>
		public Dependency(DependencyRelationship dependency, Shape startShape, Shape endShape)
			: base(dependency, startShape, endShape)
		{
			this.dependency = dependency;
		}

		internal DependencyRelationship DependencyRelationship
		{
			get { return dependency; }
		}

		protected internal override Relationship Relationship
		{
			get { return dependency; }
		}

		protected override bool IsDashed
		{
			get { return true; }
		}

		protected override Size EndCapSize
		{
			get { return Arrowhead.OpenArrowSize; }
		}

		protected override void DrawEndCap(IGraphics g, bool onScreen, Style style)
		{
			linePen.Color = style.RelationshipColor;
			linePen.Width = style.RelationshipWidth;
			g.DrawLines(linePen, Arrowhead.OpenArrowPoints);
		}

		protected override bool CloneRelationship(Diagram diagram, Shape first, Shape second)
		{
			TypeBase firstType = first.Entity as TypeBase;
			TypeBase secondType = second.Entity as TypeBase;

			if (firstType != null && secondType != null)
			{
				DependencyRelationship clone = dependency.Clone(firstType, secondType);
				return diagram.InsertDependency(clone);
			}
			else
			{
				return false;
			}
		}
	}
}
