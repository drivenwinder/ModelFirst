using System;
using EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Shapes;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Relationships;

namespace EAP.ModelFirst.Controls.Editors.DiagramEditor.ClassDiagram.Connections
{
	internal sealed class CommentConnection : Connection
	{
		CommentRelationship relationship;

		/// <exception cref="ArgumentNullException">
		/// <paramref name="relationship"/> is null.-or-
		/// <paramref name="startShape"/> is null.-or-
		/// <paramref name="endShape"/> is null.
		/// </exception>
		public CommentConnection(CommentRelationship relationship, Shape startShape, Shape endShape)
			: base(relationship, startShape, endShape)
		{
			this.relationship = relationship;
		}

		internal CommentRelationship CommentRelationship
		{
			get { return relationship; }
		}

		protected internal override Relationship Relationship
		{
			get { return relationship; }
		}

		protected override bool IsDashed
		{
			get { return true; }
		}

		protected override bool CloneRelationship(Diagram diagram, Shape first, Shape second)
		{
			Comment comment = first.Entity as Comment;
			if (comment != null)
			{
				CommentRelationship clone = relationship.Clone(comment, second.Entity);
				return diagram.InsertCommentRelationship(clone);
			}
			else
			{
				return false;
			}
		}
	}
}
