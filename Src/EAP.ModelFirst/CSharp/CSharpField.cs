using System;
using System.Text;
using System.Text.RegularExpressions;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Members;

namespace EAP.ModelFirst.CSharp
{
	internal sealed class CSharpField : Field
	{
		const string ModifiersPattern = @"((?<modifier>static|readonly|const|new|volatile)\s+)*";
		const string InitValuePattern = @"(?<initvalue>[^\s;](.*[^\s;])?)";

		// [<access>] [<modifiers>] <type> <name> [= <initvalue>]
		const string FieldPattern =
			@"^\s*" + CSharpLanguage.AccessPattern + ModifiersPattern +
			@"(?<type>" + CSharpLanguage.GenericTypePattern2 + @")\s+" +
			@"(?<name>" + CSharpLanguage.NamePattern + @")" +
			@"(\s*=\s*" + InitValuePattern + ")?" + CSharpLanguage.DeclarationEnding;

		static Regex fieldRegex = new Regex(FieldPattern, RegexOptions.ExplicitCapture);

		/// <exception cref="ArgumentNullException">
		/// <paramref name="parent"/> is null.
		/// </exception>
		internal CSharpField(CompositeType parent) : this("newField", parent)
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
		internal CSharpField(string name, CompositeType parent) : base(name, parent)
		{
			IsConstant = false;
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="value"/> does not fit to the syntax.
		/// </exception>
		public override string Type
		{
			get
			{
				return base.Type;
			}
			set
			{
				if (value == "void")
					throw new BadSyntaxException(string.Format(Strings.ErrorType, "void"));

				base.Type = value;
			}
		}

		protected override string DefaultType
		{
			get { return "int"; }
		}

		/// <exception cref="BadSyntaxException">
		/// <see cref="IsConst"/> is set to true while 
		/// <see cref="Field.InitialValue"/> is empty.
		/// </exception>
		public override bool IsConstant
		{
			get
			{
				return base.IsConstant;
			}
			set
			{
				if (value && !HasInitialValue) {
					throw new BadSyntaxException(
						Strings.ErrorLackOfInitialization);
				}
				base.IsConstant = value;
			}
		}

		public override Language Language
		{
			get { return CSharpLanguage.Instance; }
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="declaration"/> does not fit to the syntax.
		/// </exception>
		public override void InitFromString(string declaration)
		{
			Match match = fieldRegex.Match(declaration);
			RaiseChangedEvent = false;

			try {
				if (match.Success) {
					ClearModifiers();
					Group nameGroup = match.Groups["name"];
					Group typeGroup = match.Groups["type"];
					Group accessGroup = match.Groups["access"];
					Group modifierGroup = match.Groups["modifier"];
					Group initGroup = match.Groups["initvalue"];

					if (CSharpLanguage.Instance.IsForbiddenName(nameGroup.Value))
						throw new BadSyntaxException(Strings.ErrorInvalidName);
					if (CSharpLanguage.Instance.IsForbiddenTypeName(typeGroup.Value))
						throw new BadSyntaxException(Strings.ErrorInvalidTypeName);
					if (typeGroup.Value == "void")
						throw new BadSyntaxException(string.Format(Strings.ErrorType, "void"));

					ValidName = nameGroup.Value;
					ValidType = typeGroup.Value;
					AccessModifier = Language.TryParseAccessModifier(accessGroup.Value);
					InitialValue = (initGroup.Success) ? initGroup.Value : "";

					foreach (Capture modifierCapture in modifierGroup.Captures) {
						if (modifierCapture.Value == "static")
							IsStatic = true;
						if (modifierCapture.Value == "readonly")
							IsReadonly = true;
						if (modifierCapture.Value == "const")
							IsConstant = true;
						if (modifierCapture.Value == "new")
							IsHider = true;
						if (modifierCapture.Value == "volatile")
							IsVolatile = true;
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
			StringBuilder builder = new StringBuilder(50);

			if (AccessModifier != AccessModifier.Default) {
				builder.Append(Language.GetAccessString(AccessModifier, true));
				builder.Append(" ");
			}

			if (IsHider)
				builder.Append("new ");
			if (IsConstant)
				builder.Append("const ");
			if (IsStatic)
				builder.Append("static ");
			if (IsReadonly)
				builder.Append("readonly ");
			if (IsVolatile)
				builder.Append("volatile ");

			builder.AppendFormat("{0} {1}", Type, Name);
			if (HasInitialValue)
				builder.AppendFormat(" = {0}", InitialValue);

			if (withSemicolon)
				builder.Append(";");

			return builder.ToString();
		}

        protected internal override Field Clone(CompositeType newParent)
		{
			CSharpField field = new CSharpField(newParent);
			field.CopyFrom(this);
			return field;
		}

		public override string ToString()
		{
			return GetDeclarationLine(false);
		}
	}
}
