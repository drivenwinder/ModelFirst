using System;
using System.Text;
using System.Collections.Generic;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Members;
using EAP.ModelFirst.Core.Project.Relationships;

namespace EAP.ModelFirst.Uml
{
	internal sealed class UmlClass : ClassType
	{
        internal UmlClass()
            : base(Strings.Untitled)
        {
        }

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		internal UmlClass(string name) : base(name)
		{
		}

        internal static UmlClass CreateObjectClass()
        {
            // objectClass initialization
            string[] objectMethods = {
				"public static bool Equals(object objA, object objB)",
				"public virtual bool Equals(object obj)",
				"public virtual int GetHashCode()",
				"public System.Type GetType()",
				"protected object MemberwiseClone()",
				"public static bool ReferenceEquals(object objA, object objB)",
				"public virtual string ToString()"
			};
            var objectClass = new UmlClass("Object") { Id = Guid.Empty };
            objectClass.AddConstructor();
            foreach (string methodDeclaration in objectMethods)
                objectClass.AddMethod().InitFromString(methodDeclaration);
            return objectClass;
        }

        public override Language Language
        {
            get { return UnifiedModelingLanguage.Instance; }
        }

		/// <exception cref="BadSyntaxException">
		/// The type visibility is not valid in the current context.
		/// </exception>
		public override AccessModifier AccessModifier
		{
			get
			{
				return base.AccessModifier;
			}
			set
			{
				if (IsNested ||
					value == AccessModifier.Default ||
					value == AccessModifier.Internal ||
					value == AccessModifier.Public)
				{
					base.AccessModifier = value;
				}
			}
		}

		/// <exception cref="RelationshipException">
		/// The language of <paramref name="value"/> does not equal.-or-
		/// <paramref name="value"/> is static or sealed class.-or-
		/// The <paramref name="value"/> is descendant of the class.
		/// </exception>
		public override ClassType BaseClass
		{
			get
			{
				if (base.BaseClass == null && this != UnifiedModelingLanguage.ObjectClass)
					return UnifiedModelingLanguage.ObjectClass;
				else
					return base.BaseClass;
			}
			set
			{
				base.BaseClass = value;
			}
		}

		public override AccessModifier DefaultAccess
		{
			get { return AccessModifier.Internal; }
		}

		public override AccessModifier DefaultMemberAccess
		{
			get { return AccessModifier.Private; }
        }

        public override bool SupportsProperties
        {
            get { return true; }
        }

        public override bool SupportsConstuctors
        {
            get
            {
                return (base.SupportsConstuctors && Modifier != ClassModifier.Static);
            }
        }

        public override bool SupportsDestructors
        {
            get { return false; }
        }

        public override bool SupportsEvents
        {
            get { return true; }
        }

		/// <exception cref="ArgumentException">
		/// The <paramref name="value"/> is already a child member of the type.
		/// </exception>
		public override CompositeType NestingParent
		{
			get
			{
				return base.NestingParent;
			}
			protected internal set
			{
				try {
					RaiseChangedEvent = false;

					base.NestingParent = value;
					if (NestingParent == null && Access != AccessModifier.Public)
						AccessModifier = AccessModifier.Internal;
				}
				finally {
					RaiseChangedEvent = true;
				}
			}
		}

        public override void AddRealization(RealizationRelationship real)
        {
            if (!(real.BaseType is UmlInterface))
                throw new RelationshipException(string.Format(Strings.ErrorInterfaceLanguage, "uml"));
            base.AddRealization(real);
        }

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		public override Field AddField()
		{
            Field field = new UmlField(this);
            field.AccessModifier = AccessModifier.Public;

			AddField(field);
			return field;
		}

		public override Constructor AddConstructor()
		{
			Constructor constructor = new UmlConstructor(this);

			if (Modifier == ClassModifier.Abstract)
				constructor.AccessModifier = AccessModifier.Protected;
			else if (Modifier != ClassModifier.Static)
				constructor.AccessModifier = AccessModifier.Public;
			
			AddOperation(constructor);
			return constructor;
		}

		public override Destructor AddDestructor()
        {
            throw new InvalidOperationException("uml class does not support destructors.");
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		public override Method AddMethod()
		{
			Method method = new UmlMethod(this);

			method.AccessModifier = AccessModifier.Public;
			method.IsStatic = (Modifier == ClassModifier.Static);
			
			AddOperation(method);
			return method;
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		public override Property AddProperty()
        {
            Property property = new UmlProperty(this);
            property.AccessModifier = AccessModifier.Public;

            AddOperation(property);
            return property;
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		public override Event AddEvent()
        {
            Event newEvent = new UmlEvent(this);

            newEvent.AccessModifier = AccessModifier.Public;
            newEvent.IsStatic = (Modifier == ClassModifier.Static);

            AddOperation(newEvent);
            return newEvent;
		}

		public override string GetDeclaration()
		{
			StringBuilder builder = new StringBuilder();

			if (AccessModifier != AccessModifier.Default) {
                builder.Append(Language.GetAccessString(AccessModifier, true));
				builder.Append(" ");
			}
			if (Modifier != ClassModifier.None) {
                builder.Append(Language.GetClassModifierString(Modifier, true));
				builder.Append(" ");
			}
			builder.AppendFormat("class {0}", Name);
			
			if (HasExplicitBase || InterfaceList.Count > 0) {
				builder.Append(" : ");
				if (HasExplicitBase) {
					builder.Append(BaseClass.Name);
					if (InterfaceList.Count > 0)
						builder.Append(", ");
				}
				for (int i = 0; i < InterfaceList.Count; i++) {
					builder.Append(InterfaceList[i].Name);
					if (i < InterfaceList.Count - 1)
						builder.Append(", ");
				}
			}

			return builder.ToString();
		}

		public override ClassType Clone()
		{
            UmlClass newClass = new UmlClass();
            newClass.CopyFrom(this);
            newClass.Id = Guid.NewGuid();
			return newClass;
		}
	}
}
