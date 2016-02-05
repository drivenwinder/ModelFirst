using System;
using System.Text;
using System.Text.RegularExpressions;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Members;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Java
{
	internal sealed class JavaConstructor : Constructor
	{
		// [<access>] <name>([<args>])
		const string ConstructorPattern =
			@"^\s*" + JavaLanguage.AccessPattern +
			@"(?<name>" + JavaLanguage.NamePattern + ")" +
			@"\((?(static)|(?<args>.*))\)" + JavaLanguage.DeclarationEnding;

		static Regex constructorRegex =
			new Regex(ConstructorPattern, RegexOptions.ExplicitCapture);

		/// <exception cref="ArgumentException">
		/// The language of <paramref name="parent"/> does not equal.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="parent"/> is null.
		/// </exception>
		internal JavaConstructor(CompositeType parent) : base(parent)
		{
		}		

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="value"/> does not fit to the syntax.
		/// </exception>
		public sealed override string Name
		{
			get
			{
				return GetNameWithoutGeneric(Parent.Name);
			}
			set
			{
				if (value != null && value != GetNameWithoutGeneric(Parent.Name))
					throw new BadSyntaxException(Strings.ErrorConstructorName);
			}
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set static modifier.
		/// </exception>
		public override bool IsStatic
		{
			get
			{
				return base.IsStatic;
			}
			set
			{
				if (value)
					throw new BadSyntaxException(Strings.ErrorCannotSetStatic);
			}
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set sealed modifier.
		/// </exception>
		public override bool IsSealed
		{
			get
			{
				return false;
			}
			set
			{
				if (value)
					throw new BadSyntaxException(Strings.ErrorCannotSetModifier);
			}
		}

		public override Language Language
		{
			get { return JavaLanguage.Instance; }
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="declaration"/> does not fit to the syntax.
		/// </exception>
		public override void InitFromString(string declaration)
		{
			Match match = constructorRegex.Match(declaration);
			RaiseChangedEvent = false;

			try {
				if (match.Success) {
					try {
						Group nameGroup = match.Groups["name"];
						Group accessGroup = match.Groups["access"];
						Group argsGroup = match.Groups["args"];

						ValidName = nameGroup.Value;
						AccessModifier = Language.TryParseAccessModifier(accessGroup.Value);
						ArgumentList.InitFromString(argsGroup.Value);
					}
					catch (BadSyntaxException ex) {
						throw new BadSyntaxException(
							Strings.ErrorInvalidDeclaration, ex);
					}
				}
				else {
					throw new BadSyntaxException(Strings.ErrorInvalidDeclaration);
				}
			}
			finally {
				RaiseChangedEvent = true;
			}
		}

		public override string GetDeclaration()
		{
			StringBuilder builder = new StringBuilder(50);

			if (AccessModifier != AccessModifier.Default) {
				builder.Append(Language.GetAccessString(AccessModifier, true));
				builder.Append(" ");
			}

			builder.AppendFormat("{0}(", Name);

			for (int i = 0; i < ArgumentList.Count; i++) {
				builder.Append(ArgumentList[i]);
				if (i < ArgumentList.Count - 1)
					builder.Append(", ");
			}
			builder.Append(")");

			return builder.ToString();
		}

		public override Operation Clone(CompositeType newParent)
		{
			JavaConstructor constructor = new JavaConstructor(newParent);
			constructor.CopyFrom(this);
			return constructor;
		}
	}
}
