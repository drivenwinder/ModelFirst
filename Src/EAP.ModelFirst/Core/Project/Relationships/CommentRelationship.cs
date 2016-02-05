using System;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Core.Project.Relationships
{
	public sealed class CommentRelationship : Relationship
	{
		Comment comment;
		IEntity entity;

		/// <exception cref="ArgumentNullException">
		/// <paramref name="comment"/> is null.-or-
		/// <paramref name="entity"/> is null.
		/// </exception>
		internal CommentRelationship(Comment comment, IEntity entity)
		{
			if (comment == null)
				throw new ArgumentNullException("comment");
			if (entity == null)
				throw new ArgumentNullException("entity");

			this.comment = comment;
			this.entity = entity;
			Attach();
		}

		public override RelationshipType RelationshipType
		{
			get { return RelationshipType.Comment; }
		}

		public override IEntity First
		{
			get { return comment; }
			protected set { comment = (Comment) value; }
		}

		public override IEntity Second
		{
			get { return entity; }
			protected set { entity = value; }
		}

		public CommentRelationship Clone(Comment comment, IEntity entity)
		{
			CommentRelationship relationship = new CommentRelationship(comment, entity);
			relationship.CopyFrom(this);
			return relationship;
		}

		public override string ToString()
		{
			return string.Format("{0}: {1} --- {2}",
				Strings.Comment, comment.ToString(), entity.Name);
		}
	}
}
