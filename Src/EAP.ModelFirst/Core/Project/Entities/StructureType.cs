using System;
using System.Text;
using System.Collections.Generic;
using EAP.ModelFirst.Core.Project.Members;

namespace EAP.ModelFirst.Core.Project.Entities
{
	public abstract class StructureType : SingleInharitanceType
	{
		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		protected StructureType(string name) : base(name)
		{
		}

		public sealed override EntityType EntityType
		{
			get { return EntityType.Structure; }
		}

		public override bool SupportsFields
		{
			get { return true; }
		}

		public override bool SupportsMethods
		{
			get { return true; }
		}

		public override bool SupportsConstuctors
		{
			get { return true; }
		}

		public override bool SupportsDestructors
		{
			get { return false; }
		}

		public override bool SupportsNesting
		{
			get { return true; }
		}

		public override bool HasExplicitBase
		{
			get { return false; }
		}

		public override bool IsAllowedParent
		{
			get { return false; }
		}

		public override bool IsAllowedChild
		{
			get { return false; }
		}

		public override IEnumerable<Operation> OverridableOperations
		{
			get { return new Operation[] { } ; }
		}

		public sealed override string Signature
		{
			get
			{
                return (Language.GetAccessString(Access, false) + " Structure");
			}
		}

		public override string Stereotype
		{
			get { return "Structure"; }
		}

		/// <exception cref="InvalidOperationException">
		/// The type does not support destructors.
		/// </exception>
		public override Destructor AddDestructor()
		{
			throw new InvalidOperationException("Structures do not support destructors.");
		}

		public abstract StructureType Clone();
	}
}
