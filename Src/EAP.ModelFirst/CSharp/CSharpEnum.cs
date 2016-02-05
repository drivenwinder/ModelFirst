using System;
using System.Text;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Members;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.CSharp
{
	internal sealed class CSharpEnum : EnumType
	{
        internal CSharpEnum()
            : base(Strings.Untitled)
        {
        }

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		internal CSharpEnum(string name) : base(name)
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
		/// The name does not fit to the syntax.
		/// </exception>
		/// <exception cref="ReservedNameException">
		/// The name is a reserved name.
		/// </exception>
		public override EnumValue AddValue(string declaration)
		{
			EnumValue newValue = new CSharpEnumValue(declaration);

			AddValue(newValue);
			return newValue;
		}

		/// <exception cref="BadSyntaxException">
		/// The name does not fit to the syntax.
		/// </exception>
		/// <exception cref="ReservedNameException">
		/// The name is a reserved name.
		/// </exception>
		public override EnumValue ModifyValue(EnumValue value, string declaration)
		{
			EnumValue newValue = new CSharpEnumValue(declaration);

			if (ChangeValue(value, newValue))
				return newValue;
			else
				return value;
		}
		
		public override string GetDeclaration()
		{
			StringBuilder builder = new StringBuilder();

			if (AccessModifier != AccessModifier.Default) {
                builder.Append(Language.GetAccessString(AccessModifier, true));
				builder.Append(" ");
			}
			builder.AppendFormat("enum {0}", Name);

			return builder.ToString();
		}

		public override EnumType Clone()
		{
            CSharpEnum newEnum = new CSharpEnum();
            newEnum.CopyFrom(this);
            newEnum.Id = Guid.NewGuid();
			return newEnum;
		}
	}
}
