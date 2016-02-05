using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Parameters;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.CSharp
{
	internal sealed class CSharpDelegate : DelegateType
	{
        internal CSharpDelegate()
            : base(Strings.Untitled)
        {
        }

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		internal CSharpDelegate(string name) : base(name)
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

		protected override string DefaultReturnType
		{
			get { return "void"; }
		}

		public override string GetDeclaration()
		{
			StringBuilder builder = new StringBuilder();

			if (AccessModifier != AccessModifier.Default) {
                builder.Append(Language.GetAccessString(AccessModifier, true));
				builder.Append(" ");
			}
			builder.AppendFormat("delegate {0} {1}(", ReturnType, Name);

			int parameterIndex = 0;
			foreach (Parameter parameter in Arguments) {
				builder.Append(parameter.ToString());
				if (parameterIndex++ < ArgumentCount - 1)
					builder.Append(", ");
			}
			builder.Append(");");

			return builder.ToString();
		}

		public override DelegateType Clone()
		{
			CSharpDelegate newDelegate = new CSharpDelegate();
            newDelegate.CopyFrom(this);
            newDelegate.Id = Guid.NewGuid();
			return newDelegate;
		}
	}
}
