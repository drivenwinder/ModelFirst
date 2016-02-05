using System;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Members;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Java
{
	internal sealed class JavaInterface : InterfaceType
	{
        internal JavaInterface()
            : this(Strings.Untitled)
		{
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		internal JavaInterface(string name) : base(name)
		{
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

		public override bool SupportsFields
		{
			get { return true; }
		}

		public override bool SupportsProperties
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
						AccessModifier = AccessModifier.Default;
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

		public override Field AddField()
		{
			JavaField field = new JavaField(this);

			field.IsStatic = true;
			field.IsReadonly = true;
			AddField(field);

			return field;
		}

		public override Method AddMethod()
		{
			Method method = new JavaMethod(this);

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
			StringBuilder builder = new StringBuilder(30);

			if (AccessModifier != AccessModifier.Default) {
				builder.Append(Language.GetAccessString(AccessModifier, true));
				builder.Append(" ");
			}
			if (IsNested)
				builder.Append("static ");

			builder.AppendFormat("interface {0}", Name);

			if (HasExplicitBase) {
				builder.Append(" extends ");
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
			JavaInterface newInterface = new JavaInterface();
            newInterface.CopyFrom(this);
            newInterface.Id = Guid.NewGuid();
			return newInterface;
		}
	}
}
