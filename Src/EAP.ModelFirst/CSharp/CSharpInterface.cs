using System;
using System.Text;
using System.Collections.Generic;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Members;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.CSharp
{
	internal sealed class CSharpInterface : InterfaceType
	{
        internal CSharpInterface()
            : base(Strings.Untitled)
        {
        }

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		internal CSharpInterface(string name) : base(name)
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
			get { return AccessModifier.Public; }
		}

		public override bool SupportsProperties
		{
			get { return true; }
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

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		public override Method AddMethod()
		{
			Method method = new CSharpMethod(this);

			AddOperation(method);
			return method;
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		public override Property AddProperty()
		{
			Property property = new CSharpProperty(this);

			AddOperation(property);
			return property;
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		public override Event AddEvent()
		{
			Event newEvent = new CSharpEvent(this);

			AddOperation(newEvent);
			return newEvent;
		}

		public override string GetDeclaration()
		{
			StringBuilder builder = new StringBuilder(30);

			if (AccessModifier != AccessModifier.Default) {
				builder.Append(Language.GetAccessString(AccessModifier, true));
				builder.Append(" ");
			}
			builder.AppendFormat("interface {0}", Name);

			if (HasExplicitBase) {
				builder.Append(" : ");
				for (int i = 0; i < BaseList.Count; i++) {
					builder.Append(BaseList[i].Name);
					if (i < BaseList.Count - 1)
						builder.Append(", ");
				}
			}

			return builder.ToString();
		}

		public override InterfaceType Clone()
		{
            CSharpInterface newInterface = new CSharpInterface();
            newInterface.CopyFrom(this);
            newInterface.Id = Guid.NewGuid();
			return newInterface;
		}
	}
}
