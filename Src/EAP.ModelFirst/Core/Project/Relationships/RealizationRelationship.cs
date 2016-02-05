using System;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Core.Project.Relationships
{
	public sealed class RealizationRelationship : TypeRelationship
	{
		/// <exception cref="RelationshipException">
		/// Cannot create realization.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="implementer"/> is null.-or-
		/// <paramref name="baseType"/> is null.
		/// </exception>
		internal RealizationRelationship(TypeBase implementer, InterfaceType baseType)
			: base(implementer, baseType)
		{
			if (!(implementer is IInterfaceImplementer))
				throw new RelationshipException(Strings.ErrorNotInterfaceImplementer);
			Attach();
		}

		public override RelationshipType RelationshipType
		{
			get { return RelationshipType.Realization; }
		}

		private IInterfaceImplementer Implementer
		{
			get { return (IInterfaceImplementer) First; }
		}

        [System.ComponentModel.Browsable(false)]
		public InterfaceType BaseType
		{
			get { return (InterfaceType) Second; }
		}

		public RealizationRelationship Clone(TypeBase implementer, InterfaceType baseType)
		{
			RealizationRelationship realization = new RealizationRelationship(implementer, baseType);
			realization.CopyFrom(this);
			return realization;
		}

		/// <exception cref="RelationshipException">
		/// Cannot finalize relationship.
		/// </exception>
		protected override void OnAttaching(EventArgs e)
		{
			Implementer.AddRealization(this);
		}

		protected override void OnDetaching(EventArgs e)
		{
			Implementer.RemoveRealization(this);
		}

		public override string ToString()
		{
			return string.Format("{0}: {1} --> {2}",
				Strings.Realization, First.Name, Second.Name);
		}
	}
}
