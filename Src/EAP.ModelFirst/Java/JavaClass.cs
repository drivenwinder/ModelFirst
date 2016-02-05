using System;
using System.Text;
using System.Collections.Generic;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Relationships;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Members;

namespace EAP.ModelFirst.Java
{
	internal sealed class JavaClass : ClassType
	{
        internal JavaClass()
            : this(Strings.Untitled)
		{
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		internal JavaClass(string name) : base(name)
		{
		}

        internal static JavaClass CreateObjectClass()
        {
            string[] objectMethods = {
				"protected Object clone()",
				"public boolean equals(Object obj)",
				"protected void finalize()",
				"public final Class getClass()",
				"public int hashCode()",
				"public final void notify()",
				"public final void notifyAll()",
				"public String toString()",
				"public final void wait()",
				"public final void wait(long timeout)",
				"public final void wait(long timeout, int nanos)"
			};
            var objectClass = new JavaClass("Object") { Id = Guid.Empty };
            objectClass.AddConstructor();
            foreach (string methodDeclaration in objectMethods)
                objectClass.AddMethod().InitFromString(methodDeclaration);
            return objectClass;
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
					value == AccessModifier.Public)
				{
					base.AccessModifier = value;
				}
			}
		}

		public override ClassType BaseClass
		{
			get
			{
				if (base.BaseClass == null && this != JavaLanguage.ObjectClass)
					return JavaLanguage.ObjectClass;
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
			get { return AccessModifier.Internal; }
		}

		public override bool SupportsProperties
		{
			get { return false; }
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
			get { return false; }
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

		public override Language Language
		{
			get { return JavaLanguage.Instance; }
		}



        public override void AddRealization(RealizationRelationship real)
        {
            if (!(real.BaseType is JavaInterface))
                throw new RelationshipException(string.Format(Strings.ErrorInterfaceLanguage, "Java"));
            base.AddRealization(real);
        }

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		public override Field AddField()
		{
			Field field = new JavaField(this);

			field.AccessModifier = AccessModifier.Private;
			AddField(field);

			return field;
		}

		public override Constructor AddConstructor()
		{
			Constructor constructor = new JavaConstructor(this);

			if (Modifier == ClassModifier.Abstract)
				constructor.AccessModifier = AccessModifier.Protected;
			else
				constructor.AccessModifier = AccessModifier.Public;
			AddOperation(constructor);

			return constructor;
		}

		/// <exception cref="InvalidOperationException">
		/// The type does not support destructors.
		/// </exception>
		public override Destructor AddDestructor()
		{
			throw new InvalidOperationException("Java class does not support destructors.");
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		public override Method AddMethod()
		{
			Method method = new JavaMethod(this);

			method.AccessModifier = AccessModifier.Public;
			AddOperation(method);

			return method;
		}

		/// <exception cref="InvalidOperationException">
		/// The type does not support properties.
		/// </exception>
		public override Property AddProperty()
		{
			throw new InvalidOperationException("Java language does not support properties.");
		}

		/// <exception cref="InvalidOperationException">
		/// The type does not support events.
		/// </exception>
		public override Event AddEvent()
		{
			throw new InvalidOperationException("Java language does not support events.");
		}

		public override string GetDeclaration()
		{
			StringBuilder builder = new StringBuilder();

			if (AccessModifier != AccessModifier.Default) {
				builder.Append(Language.GetAccessString(AccessModifier, true));
				builder.Append(" ");
			}
			if (IsNested || Modifier == ClassModifier.Static) {
				builder.Append("static ");
			}
			if (Modifier != ClassModifier.None && Modifier != ClassModifier.Static) {
				builder.Append(Language.GetClassModifierString(Modifier, true));
				builder.Append(" ");
			}

			builder.AppendFormat("class {0}", Name);

			if (HasExplicitBase) {
				builder.Append(" extends ");
				builder.Append(BaseClass.Name);
			}
			if (InterfaceList.Count > 0) {
				builder.Append(" implements ");
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
			JavaClass newClass = new JavaClass();
            newClass.CopyFrom(this);
            newClass.Id = Guid.NewGuid();
			return newClass;
		}
	}
}
