using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using EAP.ModelFirst.Controls.Dialogs;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes;
using EAP.ModelFirst.Controls.KryptonContextMenus;
using EAP.ModelFirst.Core;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Relationships;

namespace EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Connections
{
	internal sealed class Generalization : Connection
	{
        static Pen linePen = new Pen(Color.Black);
        static SolidBrush textBrush = new SolidBrush(Color.Black);
        static StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic);

		GeneralizationRelationship generalization;

		static Generalization()
		{
			linePen.MiterLimit = 2.0F;
			linePen.LineJoin = LineJoin.MiterClipped;
		}

		/// <exception cref="ArgumentNullException">
		/// <paramref name="generalization"/> is null.-or-
		/// <paramref name="startShape"/> is null.-or-
		/// <paramref name="endShape"/> is null.
		/// </exception>
		public Generalization(GeneralizationRelationship generalization, Shape startShape, Shape endShape)
			: base(generalization, startShape, endShape)
		{
			this.generalization = generalization;
		}

		internal GeneralizationRelationship GeneralizationRelationship
		{
			get { return generalization; }
		}

		protected internal override Relationship Relationship
		{
			get { return generalization; }
		}

		protected override Size EndCapSize
		{
			get { return Arrowhead.ClosedArrowSize; }
		}

		protected override int EndSelectionOffset
		{
			get { return Arrowhead.ClosedArrowHeight; }
        }

        public override void Draw(IGraphics g, bool onScreen, Style style)
        {
            base.Draw(g, onScreen, style);

            if(generalization.Second is ClassType)
                DrawRole(g, style, ((ClassType)generalization.Second).InheritanceStrategy.ToString(), 
                    RouteCache[0], RouteCache[1], StartCapSize);
        }

        private void DrawRole(IGraphics g, Style style, string text, Point firstPoint,
            Point secondPoint, Size capSize)
        {
            float angle = GetAngle(firstPoint, secondPoint);
            Point point = firstPoint;

            if (angle == 0) // Down
            {
                point.X -= capSize.Width / 2 + TextMargin.Width;
                point.Y += style.ShadowOffset.Height + TextMargin.Height;
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Near;
            }
            else if (angle == 90) // Left
            {
                point.X -= TextMargin.Width;
                point.Y += capSize.Width / 2 + TextMargin.Height;
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Near;
            }
            else if (angle == 180) // Up
            {
                point.X -= capSize.Width / 2 + TextMargin.Width;
                point.Y -= TextMargin.Height;
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;
            }
            else // Right
            {
                point.X += style.ShadowOffset.Width + TextMargin.Width;
                point.Y += capSize.Width / 2 + TextMargin.Height;
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Near;
            }

            textBrush.Color = style.RelationshipTextColor;
            g.DrawString(text, style.RelationshipTextFont, textBrush, point, stringFormat);
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
			CompositeType firstType = first.Entity as CompositeType;
			CompositeType secondType = second.Entity as CompositeType;

			if (firstType != null && secondType != null)
			{
				GeneralizationRelationship clone = generalization.Clone(firstType, secondType);
				return diagram.InsertGeneralization(clone);
			}
			else
			{
				return false;
			}
        }

        protected override void OnDoubleClick(AbsoluteMouseEventArgs e)
        {
            base.OnDoubleClick(e);

            if (!e.Handled && generalization.Second is ClassType)
            {
                ShowEditDialog();
            }
        }

        protected internal override void ShowEditor()
        {
            ShowEditDialog();
        }

        internal void ShowEditDialog()
        {
            using (GeneralizationDialog dialog = new GeneralizationDialog())
            {
                dialog.Generalization = GeneralizationRelationship;
                dialog.ShowDialog();
            }
        }

        protected internal override ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems GetKryptonContextMenuItems(Diagram diagram)
        {
            return GeneralizationKryptonContextMenu.Default.GetMenuItems(diagram);
        }
    }
}
