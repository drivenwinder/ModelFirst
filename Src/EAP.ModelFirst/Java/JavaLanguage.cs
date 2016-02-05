using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Xml.Serialization;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Members;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Parameters;

namespace EAP.ModelFirst.Java
{
	public sealed class JavaLanguage : Language
	{
		static JavaLanguage instance = new JavaLanguage();

		#region Regex patterns

		internal const string InitialChar = @"[\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Pc}\p{Lm}]";
		internal const string DeclarationEnding = @"\s*(;\s*)?$";

		// System.String
		private const string TypeNamePattern = InitialChar + @"(\w|\." + InitialChar + @")*";

		// [,][][,,]
		private const string ArrayPattern = @"(\s*\[\s*\])*";

		// System.String[]
		internal const string BaseTypePattern = TypeNamePattern + ArrayPattern;

		// <System.String[], System.String[]>
		private const string GenericPattern =
			@"<\s*" + BaseTypePattern + @"(\s*,\s*" + BaseTypePattern + @")*\s*>";
		
		// System.Collections.Generic.List<System.String[], System.String[]>[]
		internal const string GenericTypePattern =
			TypeNamePattern + @"(\s*" + GenericPattern + @")?(\s*\[\s*\])?";
		
		// <List<int>[], List<string>>
		private const string GenericPattern2 =
			@"<\s*" + GenericTypePattern + @"(\s*,\s*" + GenericTypePattern + @")*\s*>";
		
		// System.Collections.Generic.List<List<int>[]>[]
		internal const string GenericTypePattern2 =
			TypeNamePattern + @"(\s*" + GenericPattern2 + @")?(\s*\[\s*\])?";


		// Name
		internal const string NamePattern = InitialChar + @"\w*";
		
		// <T, K>
		private const string BaseGenericPattern =
			@"<\s*" + NamePattern + @"(\s*,\s*" + NamePattern + @")*\s*>";
		
		// Name<T>
		internal const string GenericNamePattern =
			NamePattern + @"(\s*" + BaseGenericPattern + ")?";

		// [abstract | final]
		internal const string OperationModifiersPattern =
			@"((?<modifier>static|final|abstract)\s+)*";

		// [public | protected | private]
		internal const string AccessPattern = @"((?<access>public|protected|private)\s+)*";


		// For validating identifier names.
		private const string ClosedNamePattern = @"^\s*(?<name>" + NamePattern + @")\s*$";

		// For validating generic identifier names.
		private const string ClosedGenericNamePattern = @"^\s*(?<name>" + GenericNamePattern + @")\s*$";

		// For validating type names.
		private const string ClosedTypePattern = @"^\s*(?<type>" + GenericTypePattern2 + @")\s*$";

		#endregion

		static Regex nameRegex = new Regex(ClosedNamePattern, RegexOptions.ExplicitCapture);
		static Regex genericNameRegex = new Regex(ClosedGenericNamePattern, RegexOptions.ExplicitCapture);
		static Regex typeRegex = new Regex(ClosedTypePattern, RegexOptions.ExplicitCapture);

		static readonly string[] reservedNames = {
			"abstract", "assert", "break","case", "catch", "class", "const", "continue", 
			"default", "do", "else", "enum", "extends", "false", "final", "finally", 
			"for", "goto", "if", "implements", "import", "instanceof", "interface", 
			"native", "new", "null", "package", "private", "protected", "public", 
			"return", "static", "strictfp", "super", "switch", "synchronized", "this", 
			"throw", "throws", "transient", "true", "try", "volatile", "while"
		};
		static readonly string[] typeKeywords = {
			"boolean", "byte", "char", "double", "float", "int", "long", "short", "void"
		};
		static readonly Dictionary<AccessModifier, string> validAccessModifiers;
		static readonly Dictionary<ClassModifier, string> validClassModifiers;
		static readonly Dictionary<FieldModifier, string> validFieldModifiers;
		static readonly Dictionary<OperationModifier, string> validOperationModifiers;

		static JavaClass objectClass;

		static JavaLanguage()
		{

            objectClass = JavaClass.CreateObjectClass();

			// validAccessModifiers initialization
			validAccessModifiers = new Dictionary<AccessModifier, string>(4);
			validAccessModifiers.Add(AccessModifier.Default, "Default");
			validAccessModifiers.Add(AccessModifier.Public, "Public");
			validAccessModifiers.Add(AccessModifier.Protected, "Protected");
			validAccessModifiers.Add(AccessModifier.Private, "Private");

			// validClassModifiers initialization
            validClassModifiers = new Dictionary<ClassModifier, string>(4);
            validClassModifiers.Add(ClassModifier.None, "None");
			validClassModifiers.Add(ClassModifier.Abstract, "Abstract");
			validClassModifiers.Add(ClassModifier.Sealed, "Final");
			validClassModifiers.Add(ClassModifier.Static, "Static");

			// validFieldModifiers initialization
			validFieldModifiers = new Dictionary<FieldModifier, string>(3);
			validFieldModifiers.Add(FieldModifier.Static, "Static");
			validFieldModifiers.Add(FieldModifier.Readonly, "Final");
			validFieldModifiers.Add(FieldModifier.Volatile, "Volatile");

			// validOperationModifiers initialization
			validOperationModifiers = new Dictionary<OperationModifier, string>(3);
			validOperationModifiers.Add(OperationModifier.Static, "Static");
			validOperationModifiers.Add(OperationModifier.Abstract, "Abstract");
			validOperationModifiers.Add(OperationModifier.Sealed, "Final");
		}

		private JavaLanguage()
		{
		}

		public static JavaLanguage Instance
		{
			get { return instance; }
		}

		internal static JavaClass ObjectClass
		{
			get { return objectClass; }
		}

		public override string Name
		{
			get { return "Java"; }
		}

		public override string AssemblyName
		{
            get { return GetType().Assembly.FullName; }
		}

		public override bool SupportsInterfaces
		{
			get { return true; }
		}

		public override bool SupportsStructures
		{
			get { return false; }
		}

		public override bool SupportsEnums
		{
			get { return true; }
		}

		public override bool SupportsDelegates
		{
			get { return false; }
		}

		public override bool SupportsExplicitImplementation
		{
			get { return false; }
		}

		public override bool SupportsAssemblyImport
		{
			get { return false; }
		}

		public override bool ExplicitVirtualMethods
		{
			get { return false; }
		}

		[XmlIgnore]
		public override Dictionary<AccessModifier, string> ValidAccessModifiers
		{
			get { return validAccessModifiers; }
		}

		[XmlIgnore]
		public override Dictionary<ClassModifier, string> ValidClassModifiers
		{
			get { return validClassModifiers; }
		}

		[XmlIgnore]
		public override Dictionary<FieldModifier, string> ValidFieldModifiers
		{
			get { return validFieldModifiers; }
		}

		[XmlIgnore]
		public override Dictionary<OperationModifier, string> ValidOperationModifiers
		{
			get { return validOperationModifiers; }
		}

		protected override string[] ReservedNames
		{
			get { return reservedNames; }
		}

		protected override string[] TypeKeywords
		{
			get { return typeKeywords; }
		}

		public override string DefaultFileExtension
		{
			get { return ".jd"; }
		}

		public override bool IsValidModifier(AccessModifier modifier)
		{
			return (
				modifier == AccessModifier.Public ||
				modifier == AccessModifier.Protected ||
				modifier == AccessModifier.Default ||
				modifier == AccessModifier.Private
			);
		}

		public override bool IsValidModifier(FieldModifier modifier)
		{
			return (
				modifier == FieldModifier.Static ||
				modifier == FieldModifier.Readonly ||
				modifier == FieldModifier.Volatile
			);
		}

		public override bool IsValidModifier(OperationModifier modifier)
		{
			return (
				modifier == OperationModifier.Static ||
				modifier == OperationModifier.Abstract ||
				modifier == OperationModifier.Sealed
			);
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="operation"/> contains invalid modifier combinations.
		/// </exception>
        protected internal override void ValidateOperation(Operation operation)
		{
			if (operation.IsAbstract) {
				if (operation.IsStatic) {
					throw new BadSyntaxException(string.Format(
						Strings.ErrorInvalidModifierCombination, "abstract", "static"));
				}
				if (operation.IsSealed) {
					throw new BadSyntaxException(string.Format(
					Strings.ErrorInvalidModifierCombination, "sealed", "abstract"));
				}
			}

			if (operation.Access == AccessModifier.Private && operation.IsAbstract) {
				throw new BadSyntaxException(string.Format(
					Strings.ErrorInvalidModifierCombination, "private", "abstract"));
			}

			if (operation.IsAbstract) {
				ClassType parent = operation.Parent as ClassType;
				if (parent == null)
					throw new BadSyntaxException(Strings.ErrorInvalidModifier);
				else
					parent.Modifier = ClassModifier.Abstract;
			}
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="field"/> contains invalid modifier combinations.
		/// </exception>
        protected internal override void ValidateField(Field field)
		{
			if (field.IsReadonly && field.IsVolatile) {
				throw new BadSyntaxException(string.Format(
					Strings.ErrorInvalidModifierCombination, "volatile", "readonly"));
			}
		}

		/// <exception cref="ArgumentException">
		/// The language does not support explicit interface implementation.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="operation"/> is null.-or-
		/// <paramref name="newParent"/> is null.
		/// </exception>
        protected internal override Operation Implement(Operation operation,
			CompositeType newParent, bool explicitly)
		{
			if (operation == null)
				throw new ArgumentNullException("operation");
			if (newParent == null)
				throw new ArgumentNullException("newParent");

			if (explicitly) {
				throw new ArgumentException("Java does not support explicit" +
					"interface implementation.", "explicitly");
			}

			Operation newOperation = operation.Clone(newParent);

			newOperation.AccessModifier = AccessModifier.Default;
			newOperation.ClearModifiers();
			newOperation.IsStatic = false;

			return newOperation;
		}

		/// <exception cref="ArgumentException">
		/// <paramref name="operation"/> cannot be overridden.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="operation"/> is null.
		/// </exception>
        protected internal override Operation Override(Operation operation, CompositeType newParent)
		{
			if (operation == null)
				throw new ArgumentNullException("operation");

			if (operation.Modifier == OperationModifier.Sealed) {
				throw new ArgumentException(
					Strings.ErrorCannotOverride, "operation");
			}

			Operation newOperation = operation.Clone(newParent);
			newOperation.ClearModifiers();

			return newOperation;
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		public override string GetValidName(string name, bool isGenericName)
		{
			Match match = (isGenericName ? nameRegex.Match(name) : genericNameRegex.Match(name));

			if (match.Success) {
				string validName = match.Groups["name"].Value;
				return base.GetValidName(validName, isGenericName);
			}
			else {
				throw new BadSyntaxException(Strings.ErrorInvalidName);
			}			
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		public override string GetValidTypeName(string name)
		{
			Match match = typeRegex.Match(name);

			if (match.Success) {
				string validName = match.Groups["type"].Value;
				return base.GetValidTypeName(validName);
			}
			else {
				throw new BadSyntaxException(Strings.ErrorInvalidTypeName);
			}			
		}

		public override OperationModifier TryParseOperationModifier(string value)
		{
			if (value == "final")
				return OperationModifier.Sealed;
			else
				return base.TryParseOperationModifier(value);
		}

		public override string GetAccessString(AccessModifier access, bool forCode)
		{
			switch (access) {
				case AccessModifier.Default:
					if (forCode)
						return "";
					else
						return "Default";

				case AccessModifier.Internal:
					if (forCode)
						return "";
					else
						return "Package";

				default:
					if (forCode)
						return access.ToString().ToLower();
					else
						return access.ToString();
			}
		}

		public override string GetFieldModifierString(FieldModifier modifier, bool forCode)
		{
			if (modifier == FieldModifier.None) {
				if (forCode)
					return "";
				else
					return "None";
			}

			StringBuilder builder = new StringBuilder(20);
			if ((modifier & FieldModifier.Static) != 0)
				builder.Append(forCode ? "static " : "Static, ");
			if ((modifier & FieldModifier.Readonly) != 0)
				builder.Append(forCode ? "readonly " : "Readonly, ");
			if ((modifier & FieldModifier.Volatile) != 0)
				builder.Append(forCode ? "volatile " : "Volatile, ");

			if (forCode)
				builder.Remove(builder.Length - 1, 1);
			else
				builder.Remove(builder.Length - 2, 2);

			return builder.ToString();
		}

		public override string GetOperationModifierString(OperationModifier modifier, bool forCode)
		{
			if (modifier == OperationModifier.None) {
				if (forCode)
					return "";
				else
					return "None";
			}

			StringBuilder builder = new StringBuilder(20);
			if ((modifier & OperationModifier.Static) != 0)
				builder.Append(forCode ? "static " : "Static, ");
			if ((modifier & OperationModifier.Sealed) != 0)
				builder.Append(forCode ? "final " : "Final, ");
			if ((modifier & OperationModifier.Abstract) != 0)
				builder.Append(forCode ? "abstract " : "Abstract, ");

			if (forCode)
				builder.Remove(builder.Length - 1, 1);
			else
				builder.Remove(builder.Length - 2, 2);

			return builder.ToString();
		}

		public override string GetClassModifierString(ClassModifier modifier, bool forCode)
		{
			if (validClassModifiers.ContainsKey(modifier))
			{
				if (forCode)
					return validClassModifiers[modifier].ToLower();
				else
					return validClassModifiers[modifier];
			}
			else
			{
				if (modifier == ClassModifier.None)
					return Strings.None;
				else
					return string.Empty;
			}
		}

        protected internal override ClassType CreateClass(string name)
		{
			return new JavaClass(name);
		}

		/// <exception cref="InvalidOperationException">
		/// The language does not support structures.
		/// </exception>
        protected internal override StructureType CreateStructure(string name)
		{
			throw new InvalidOperationException(
				string.Format(Strings.ErrorCannotCreateStructure, Name));
		}

        protected internal override InterfaceType CreateInterface(string name)
		{
			return new JavaInterface(name);
		}

        protected internal override EnumType CreateEnum(string name)
		{
			return new JavaEnum(name);
		}

		/// <exception cref="InvalidOperationException">
		/// The language does not support delegates.
		/// </exception>
        protected internal override DelegateType CreateDelegate(string name)
		{
			throw new InvalidOperationException(
				string.Format(Strings.ErrorCannotCrateDelegate, Name));
		}

        protected internal override ArgumentList CreateParameterCollection()
		{
			return new JavaArgumentList();
		}
	}
}