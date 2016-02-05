using System;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;
using System.Xml;

namespace EAP.ModelFirst.Core.Project.Relationships
{
	public sealed class GeneralizationRelationship : TypeRelationship
	{
		/// <exception cref="RelationshipException">
		/// Cannot create generalization.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="derivedType"/> is null.-or-
		/// <paramref name="baseType"/> is null.
		/// </exception>
		internal GeneralizationRelationship(CompositeType derivedType, CompositeType baseType)
			: base(derivedType, baseType)
		{
			Attach();
		}

		public override RelationshipType RelationshipType
		{
			get { return RelationshipType.Generalization; }
		}

		private CompositeType DerivedType
		{
			get { return (CompositeType) First; }
		}

		private CompositeType BaseType
		{
			get { return (CompositeType) Second; }
		}

		public GeneralizationRelationship Clone(CompositeType derivedType, CompositeType baseType)
		{
			GeneralizationRelationship generalization = 
				new GeneralizationRelationship(derivedType, baseType);
			generalization.CopyFrom(this);
			return generalization;
		}

		/// <exception cref="RelationshipException">
		/// Cannot finalize relationship.
		/// </exception>
		protected override void OnAttaching(EventArgs e)
		{
			base.OnAttaching(e);

			if (!DerivedType.IsAllowedChild)
				throw new RelationshipException(Strings.ErrorNotAllowedChild);
			if (!BaseType.IsAllowedParent)
				throw new RelationshipException(Strings.ErrorNotAllowedParent);
            if (First is SingleInharitanceType && ((SingleInharitanceType)First).HasExplicitBase)
				throw new RelationshipException(Strings.ErrorMultipleBases);
			if (First is SingleInharitanceType ^ Second is SingleInharitanceType ||
				First is InterfaceType ^ Second is InterfaceType)
				throw new RelationshipException(Strings.ErrorInvalidBaseType);

			if (First is SingleInharitanceType && Second is SingleInharitanceType) {
                ((SingleInharitanceType)First).GeneralizationRelationship = this;
			}
			else if (First is InterfaceType && Second is InterfaceType) {
                ((InterfaceType)First).AddGeneralization(this);
			}
		}

		protected override void OnDetaching(EventArgs e)
		{
			base.OnDetaching(e);

            if (First is SingleInharitanceType)
                ((SingleInharitanceType)First).GeneralizationRelationship = null;
			else if (First is InterfaceType)
                ((InterfaceType)First).RemoveGeneralization(this);
		}

		public override string ToString()
		{
			return string.Format("{0}: {1} --> {2}",
				Strings.Generalization, First.Name, Second.Name);
		}
	}
}
