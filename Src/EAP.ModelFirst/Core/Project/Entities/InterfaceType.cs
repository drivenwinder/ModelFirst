using System;
using System.Collections.Generic;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Members;
using EAP.ModelFirst.Core.Project.Relationships;

namespace EAP.ModelFirst.Core.Project.Entities
{
	public abstract class InterfaceType : CompositeType
	{
		List<InterfaceType> baseList;
        List<GeneralizationRelationship> generalizationRelationshipList;
        List<InterfaceType> children;
        List<SingleInharitanceType> realisztions;

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		protected InterfaceType(string name) : base(name)
		{
			baseList = new List<InterfaceType>();
            generalizationRelationshipList = new List<GeneralizationRelationship>();
            children = new List<InterfaceType>();
            realisztions = new List<SingleInharitanceType>();
		}

		public sealed override EntityType EntityType
		{
			get { return EntityType.Interface; }
		}

        protected List<GeneralizationRelationship> GeneralizationRelationshipList
        {
            get { return generalizationRelationshipList; }
        }

        [System.ComponentModel.Browsable(false)]
        public IEnumerable<GeneralizationRelationship> GeneralizationRelationships
        {
            get { return generalizationRelationshipList; }
        }

		protected List<InterfaceType> BaseList
		{
			get { return baseList; }
		}

        [System.ComponentModel.Browsable(false)]
		public IEnumerable<InterfaceType> Bases
		{
			get { return baseList; }
		}

        [System.ComponentModel.Browsable(false)]
        public List<SingleInharitanceType> Realisztions
        {
            get { return realisztions; }
        }

        [System.ComponentModel.Browsable(false)]
        public IEnumerable<InterfaceType> Children
        {
            get { return children; }
        }

		public override bool SupportsFields
		{
			get { return false; }
		}

		public override bool SupportsMethods
		{
			get { return true; }
		}

		public override bool SupportsConstuctors
		{
			get { return false; }
		}

		public override bool SupportsDestructors
		{
			get { return false; }
		}

		public override bool SupportsNesting
		{
			get { return false; }
		}

		public override bool HasExplicitBase
		{
			get { return (baseList.Count > 0); }
		}

		public override bool IsAllowedParent
		{
			get { return true; }
		}

		public override bool IsAllowedChild
		{
			get { return true; }
		}

		public sealed override string Signature
		{
			get
			{
                return (Language.GetAccessString(Access, false) + " Interface");
			}
		}

		public override string Stereotype
		{
			get { return "«interface»"; }
		}

		private bool IsAncestor(InterfaceType _interface)
		{
			foreach (InterfaceType baseInterface in baseList) {
				if (baseInterface.IsAncestor(_interface))
					return true;
			}
			return (_interface == this);
		}

        internal void RemoveGeneralization(GeneralizationRelationship g)
        {
            RemoveBase(g.Second as InterfaceType);
            generalizationRelationshipList.Remove(g);
        }

        internal void AddGeneralization(GeneralizationRelationship g)
        {
            AddBase((InterfaceType)g.Second);
            generalizationRelationshipList.Add(g);
        }

		void AddBase(InterfaceType _base)
		{
			if (_base == null)
				throw new ArgumentNullException("_base");

			if (BaseList.Contains(_base)) {
				throw new RelationshipException(
					Strings.ErrorCannotAddSameBaseInterface);
			}
			if (_base.IsAncestor(this)) {
					throw new RelationshipException(string.Format(Strings.ErrorCyclicBase, Name, _base.Name));
			}

            if (_base.Language != Language)
				throw new RelationshipException(Strings.ErrorLanguagesDoNotEqual);

			BaseList.Add(_base);
            _base.children.Add(this);
			Changed();
		}

		bool RemoveBase(InterfaceType _base)
		{
            _base.children.Remove(this);
			if (BaseList.Remove(_base)) {
				Changed();
				return true;
			}
			else {
				return false;
			}
		}

		/// <exception cref="InvalidOperationException">
		/// The type does not support fields.
		/// </exception>
		public override Field AddField()
		{
			throw new InvalidOperationException("Interfaces do not support fields.");
		}

		/// <exception cref="InvalidOperationException">
		/// The type does not support constructors.
		/// </exception>
		public sealed override Constructor AddConstructor()
		{
			throw new InvalidOperationException("Interfaces do not support constructors.");
		}

		/// <exception cref="InvalidOperationException">
		/// The type does not support destructors.
		/// </exception>
		public sealed override Destructor AddDestructor()
		{
			throw new InvalidOperationException("Interfaces do not support destructors.");
		}

		public abstract InterfaceType Clone();
	}
}
