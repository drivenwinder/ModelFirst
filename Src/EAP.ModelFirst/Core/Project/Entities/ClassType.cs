using System;
using System.Collections.Generic;
using System.Xml;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Members;
using EAP.ModelFirst.Core.Project.Relationships;

namespace EAP.ModelFirst.Core.Project.Entities
{
	public abstract class ClassType : SingleInharitanceType
	{
		ClassModifier modifier = ClassModifier.None;
		ClassType baseClass = null;
        //int derivedClassCount = 0;
        List<ClassType> children = new List<ClassType>();
        InheritanceStrategy strategy;

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		protected ClassType(string name) : base(name)
		{
		}

		public sealed override EntityType EntityType
		{
			get { return EntityType.Class; }
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="value"/> does not fit to the syntax.
		/// </exception>
		public virtual ClassModifier Modifier
		{
			get
			{
				return modifier;
			}
			set
			{
				if (modifier != value) {
					if (value == ClassModifier.Static && (IsSuperClass || HasExplicitBase))
						throw new BadSyntaxException(Strings.ErrorInvalidModifier);
					if (value == ClassModifier.Sealed && IsSuperClass)
						throw new BadSyntaxException(Strings.ErrorInvalidModifier);

					modifier = value;
					Changed();
				}
			}
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

		public override bool SupportsNesting
		{
			get { return true; }
		}

		public override bool IsAllowedParent
		{
			get
			{
				return (
					Modifier != ClassModifier.Sealed &&
					Modifier != ClassModifier.Static
				);
			}
		}

		public override bool IsAllowedChild
		{
			get
			{
				return (Modifier != ClassModifier.Static);
			}
		}

		public override bool HasExplicitBase
		{
			get
			{
				return (baseClass != null);
			}
		}

        [System.ComponentModel.Browsable(false)]
		public bool IsSuperClass
		{
			get { return (children.Count > 0); }
		}

		public sealed override string Signature
		{
			get
			{
                string accessString = Language.GetAccessString(Access, false);
                string modifierString = Language.GetClassModifierString(Modifier, false);

				if (Modifier == ClassModifier.None)
					return string.Format("{0} Class", accessString);
				else
					return string.Format("{0} {1} Class", accessString, modifierString);
			}
		}

		public override string Stereotype
		{
			get { return null; }
		}

		/// <exception cref="RelationshipException">
		/// The base and derived types do not equal.-or-
		/// The <paramref name="value"/> is descendant of the type.
		/// </exception>
		public override SingleInharitanceType Base
		{
			get
			{
				return BaseClass;
			}
			set
			{
				if (value != null && !(value is ClassType))
					throw new RelationshipException(Strings.ErrorInvalidBaseType);

				BaseClass = (ClassType) value;
			}
		}

        [System.ComponentModel.Browsable(false)]
        public IEnumerable<ClassType> Children
        {
            get { return children; }
        }

        public virtual InheritanceStrategy InheritanceStrategy
        {
            get
            {
                if (baseClass != null)
                    return baseClass.InheritanceStrategy;
                return strategy;
            }
            set
            {
                if (InheritanceStrategy != value)
                {
                    if (baseClass != null)
                        baseClass.InheritanceStrategy = value;
                    strategy = value;
                    Changed();
                }
            }
        }

        public ClassType GetBaseRoot()
        {
            if (baseClass != null)
                return baseClass.GetBaseRoot();
            return this;
        }

		/// <exception cref="RelationshipException">
		/// The language of <paramref name="value"/> does not equal.-or-
		/// <paramref name="value"/> is static or sealed class.-or-
		/// The <paramref name="value"/> is descendant of the class.
        /// </exception>
        [System.ComponentModel.Browsable(false)]
		public virtual ClassType BaseClass
		{
			get
			{
				return baseClass;
			}
			set
			{
				if (value == baseClass)
					return;

				if (value == null) {
                    if(baseClass != null)
                        baseClass.children.Remove(this);
					baseClass = null;
					Changed();
					return;
				}

				if (value == this)
					throw new RelationshipException(Strings.ErrorInvalidBaseType);

				if (value.Modifier == ClassModifier.Sealed ||
					value.Modifier == ClassModifier.Static)
				{
					throw new RelationshipException(Strings.ErrorCannotInherit);
				}
				if (value.IsAncestor(this)) {
					throw new RelationshipException(string.Format(Strings.ErrorCyclicBase, Name, value.Name));
				}
                if (value.Language != Language)
					throw new RelationshipException(Strings.ErrorLanguagesDoNotEqual);

                baseClass = value;
                baseClass.children.Add(this);
				Changed();
			}
		}

		public override IEnumerable<Operation> OverridableOperations
		{
			get
			{
				for (int i = 0; i < OperationList.Count; i++) {
					if (OperationList[i].Overridable)
						yield return OperationList[i];
				}
			}
		}

		private bool IsAncestor(ClassType classType)
		{
			if (BaseClass != null && BaseClass.IsAncestor(classType))
				return true;
			else
				return (classType == this);
		}

		protected override void CopyFrom(TypeBase type)
		{
			base.CopyFrom(type);
			ClassType classType = (ClassType) type;
			modifier = classType.modifier;
			strategy = classType.strategy;
		}

		public abstract ClassType Clone();

		/// <exception cref="ArgumentNullException">
		/// <paramref name="node"/> is null.
		/// </exception>
		protected internal override void Serialize(XmlElement node)
		{
			base.Serialize(node);

            node.CreateElement("Modifier", Modifier.ToString());
            if(IsSuperClass && baseClass == null)
                node.CreateElement("InheritanceStrategy", InheritanceStrategy.ToString());
		}

		/// <exception cref="BadSyntaxException">
		/// An error occured while deserializing.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// The XML document is corrupt.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="node"/> is null.
		/// </exception>
		protected internal override void Deserialize(XmlElement node)
		{
			RaiseChangedEvent = false;

			XmlElement child = node["Modifier"];
			if (child != null)
                Modifier = Language.TryParseClassModifier(child.InnerText);

            var s = node["InheritanceStrategy"];
            if (s != null)
                InheritanceStrategy = s.GetValue(InheritanceStrategy.Subclass);

			base.Deserialize(node);
			RaiseChangedEvent = true;
		}
	}
}
