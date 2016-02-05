using System;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Core.Project.Relationships
{
	public sealed class NestingRelationship : TypeRelationship
	{
		/// <exception cref="RelationshipException">
		/// Cannot create nesting relationship.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="parentClass"/> is null.-or-
		/// <paramref name="innerClass"/> is null.
		/// </exception>
		internal NestingRelationship(CompositeType parentType, TypeBase innerType)
			: base(parentType, innerType)
		{
			Attach();
		}

		public override RelationshipType RelationshipType
		{
			get { return RelationshipType.Nesting; }
		}

		private CompositeType ParentType
		{
			get { return (CompositeType) First; }
		}

		private TypeBase InnerType
		{
			get { return (TypeBase) Second; }
		}

		public NestingRelationship Clone(CompositeType parentType, TypeBase innerType)
		{
			NestingRelationship nesting = new NestingRelationship(parentType, innerType);
			nesting.CopyFrom(this);
			return nesting;
		}

		/// <exception cref="RelationshipException">
		/// Cannot finalize relationship.
		/// </exception>
		protected override void OnAttaching(EventArgs e)
		{
			if (InnerType.IsNested)
				throw new RelationshipException(Strings.ErrorInnerTypeAlreadyNested);

			InnerType.NestingRelationship = this;
		}

		protected override void OnDetaching(EventArgs e)
		{
			InnerType.NestingParent = null;
		}

		public override string ToString()
		{
			return string.Format("{0}: {1} (+)--> {2}",
				Strings.Nesting, First.Name, Second.Name);
		}
	}
}
