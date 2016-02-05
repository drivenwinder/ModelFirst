using System;
using System.Text;
using System.Text.RegularExpressions;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Members;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Java
{
	internal sealed class JavaMethod : Method
	{
		// [<access>] [<modifiers>] <type> <name>(<args>)
		const string MethodPattern =
			@"^\s*" + JavaLanguage.AccessPattern + JavaLanguage.OperationModifiersPattern +
			@"(?<type>" + JavaLanguage.GenericTypePattern2 + @")\s+" +
			@"(?<name>" + JavaLanguage.GenericNamePattern + ")" +
			@"\((?<args>.*)\)" + JavaLanguage.DeclarationEnding;

		static Regex methodRegex = new Regex(MethodPattern, RegexOptions.ExplicitCapture);

		/// <exception cref="ArgumentNullException">
		/// <paramref name="parent"/> is null.
		/// </exception>
		internal JavaMethod(CompositeType parent) : this("newMethod", parent)
		{
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// The language of <paramref name="parent"/> does not equal.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="parent"/> is null.
		/// </exception>
		internal JavaMethod(string name, CompositeType parent) : base(name, parent)
		{
		}

		protected override string DefaultType
		{
			get { return "void"; }
		}

		public override AccessModifier AccessModifier
		{
			get
			{
				return base.AccessModifier;
			}
			set
			{
				if (value != AccessModifier.Default && value != AccessModifier.Public &&
					Parent is InterfaceType)
				{
					throw new BadSyntaxException(Strings.ErrorInterfaceMemberAccess);
				}

				base.AccessModifier = value;
			}
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set virtual modifier.
		/// </exception>
		public override bool IsVirtual
		{
			get
			{
				return false;
			}
			set
			{
				if (value)
					throw new BadSyntaxException(Strings.ErrorInvalidModifier);
			}
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set override modifier.
		/// </exception>
		public override bool IsOverride
		{
			get
			{
				return false;
			}
			set
			{
				if (value)
					throw new BadSyntaxException(Strings.ErrorInvalidModifier);
			}
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set hider modifier.
		/// </exception>
		public override bool IsHider
		{
			get
			{
				return false;
			}
			set
			{
				if (value)
					throw new BadSyntaxException(Strings.ErrorInvalidModifier);
			}
		}

		public override bool IsOperator
		{
			get { return false; }
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
			Match match = methodRegex.Match(declaration);
			RaiseChangedEvent = false;

			try {
				if (match.Success) {
					ClearModifiers();
					Group nameGroup = match.Groups["name"];
					Group typeGroup = match.Groups["type"];
					Group accessGroup = match.Groups["access"];
					Group modifierGroup = match.Groups["modifier"];
					Group argsGroup = match.Groups["args"];

					if (JavaLanguage.Instance.IsForbiddenName(nameGroup.Value))
						throw new BadSyntaxException(Strings.ErrorInvalidName);
					if (JavaLanguage.Instance.IsForbiddenTypeName(typeGroup.Value))
						throw new BadSyntaxException(Strings.ErrorInvalidTypeName);
					ValidName = nameGroup.Value;
					ValidType = typeGroup.Value;

					ArgumentList.InitFromString(argsGroup.Value);
					AccessModifier = Language.TryParseAccessModifier(accessGroup.Value);
					foreach (Capture modifierCapture in modifierGroup.Captures) {
						if (modifierCapture.Value == "static")
							IsStatic = true;
						if (modifierCapture.Value == "abstract")
							IsAbstract = true;
						if (modifierCapture.Value == "final")
							IsSealed = true;
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
			return GetDeclarationLine(true);
		}

		public string GetDeclarationLine(bool withSemicolon)
		{
			StringBuilder builder = new StringBuilder(100);

			if (AccessModifier != AccessModifier.Default) {
				builder.Append(Language.GetAccessString(AccessModifier, true));
				builder.Append(" ");
			}
			if (IsStatic)
				builder.Append("static ");
			if (IsSealed)
				builder.Append("final ");
			if (IsAbstract)
				builder.Append("abstract ");

			builder.AppendFormat("{0} {1}(", Type, Name);

			for (int i = 0; i < ArgumentList.Count; i++) {
				builder.Append(ArgumentList[i]);
				if (i < ArgumentList.Count - 1)
					builder.Append(", ");
			}
			builder.Append(")");

			if (withSemicolon && !HasBody)
				builder.Append(";");

			return builder.ToString();
		}

		public override Operation Clone(CompositeType newParent)
		{
			JavaMethod method = new JavaMethod(newParent);
			method.CopyFrom(this);
			return method;
		}

		public override string ToString()
		{
			return GetDeclarationLine(false);
		}
	}
}
