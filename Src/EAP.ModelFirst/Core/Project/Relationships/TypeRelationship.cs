using System;
using System.Xml;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Core.Project.Relationships
{
	public abstract class TypeRelationship : Relationship
	{
		TypeBase first;
		TypeBase second;

		/// <exception cref="ArgumentNullException">
		/// <paramref name="first"/> is null.-or-
		/// <paramref name="second"/> is null.
		/// </exception>
		protected TypeRelationship(TypeBase first, TypeBase second)
		{
			if (first == null)
				throw new ArgumentNullException("first");
			if (second == null)
				throw new ArgumentNullException("second");

			this.first = first;
			this.second = second;
		}

		public sealed override IEntity First
		{
			get { return first; }
			protected set { first = (TypeBase) value; }
		}

		public sealed override IEntity Second
		{
			get { return second; }
			protected set { second = (TypeBase) value; }
		}
	}
}
