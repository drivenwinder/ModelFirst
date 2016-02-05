using System;
using System.Xml;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Core.Project.Relationships
{
	public sealed class DependencyRelationship : TypeRelationship
	{
		/// <exception cref="ArgumentNullException">
		/// <paramref name="first"/> is null.-or-
		/// <paramref name="second"/> is null.
		/// </exception>
		internal DependencyRelationship(TypeBase first, TypeBase second) : base(first, second)
		{
			Attach();
		}

		public override RelationshipType RelationshipType
		{
			get { return RelationshipType.Dependency; }
		}

		public override bool SupportsLabel
		{
			get { return true; }
		}

		public DependencyRelationship Clone(TypeBase first, TypeBase second)
		{
			DependencyRelationship dependency = new DependencyRelationship(first, second);
			dependency.CopyFrom(this);
			return dependency;
		}

		public override string ToString()
		{
			return string.Format("{0}: {1} --> {2}",
				Strings.Dependency, First.Name, Second.Name);
		}
	}
}
