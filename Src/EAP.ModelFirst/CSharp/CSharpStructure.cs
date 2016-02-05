using System;
using System.Text;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Members;
using EAP.ModelFirst.Core.Project.Relationships;

namespace EAP.ModelFirst.CSharp
{
	internal sealed class CSharpStructure : StructureType
	{
        internal CSharpStructure()
            : base(Strings.Untitled)
        {
        }

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		internal CSharpStructure(string name) : base(name)
		{
		}

        public override Language Language
        {
            get { return CSharpLanguage.Instance; }
        }

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

		public override bool SupportsEvents
		{
			get { return true; }
		}

		public override SingleInharitanceType Base
		{
			get
			{
				return CSharpLanguage.ObjectClass;
			}
			set
			{
				throw new InvalidOperationException("Cannot set the base class of structures.");
			}
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

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		public override Field AddField()
		{
			Field field = new CSharpField(this);

			AddField(field);
			return field;
		}

		public override Constructor AddConstructor()
		{
			Constructor constructor = new CSharpConstructor(this);
			
			constructor.AccessModifier = AccessModifier.Public;
			AddOperation(constructor);

			return constructor;
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		public override Method AddMethod()
		{
			Method method = new CSharpMethod(this);

			method.AccessModifier = AccessModifier.Public;
			AddOperation(method);

			return method;
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		public override Property AddProperty()
		{
			Property property = new CSharpProperty(this);

			property.AccessModifier = AccessModifier.Public;
			AddOperation(property);

			return property;
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		public override Event AddEvent()
		{
			Event newEvent = new CSharpEvent(this);

			newEvent.AccessModifier = AccessModifier.Public;
			AddOperation(newEvent);

			return newEvent;
		}

        public override void AddRealization(RealizationRelationship real)
        {
            if (!(real.BaseType is CSharpInterface))
                throw new RelationshipException(string.Format(Strings.ErrorInterfaceLanguage, "C#"));

            base.AddRealization(real);
        }

		public override string GetDeclaration()
		{
			StringBuilder builder = new StringBuilder();

			if (AccessModifier != AccessModifier.Default) {
                builder.Append(Language.GetAccessString(AccessModifier, true));
				builder.Append(" ");
			}
			builder.AppendFormat("struct {0}", Name);

			if (InterfaceList.Count > 0) {
				builder.Append(" : ");
				for (int i = 0; i < InterfaceList.Count; i++) {
					builder.Append(InterfaceList[i].Name);
					if (i < InterfaceList.Count - 1)
						builder.Append(", ");
				}
			}

			return builder.ToString();
		}

		public override StructureType Clone()
		{
            CSharpStructure structure = new CSharpStructure();
            structure.CopyFrom(this);
            structure.Id = Guid.NewGuid();
			return structure;
		}
	}
}
