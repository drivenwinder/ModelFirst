using System;
using System.Text;
using System.Text.RegularExpressions;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Members;

namespace EAP.ModelFirst.Uml
{
	internal sealed class UmlEvent : Event
	{
		// [<access>] [<modifiers>] event <type> <name>
		const string EventPattern =
			@"^\s*" + UnifiedModelingLanguage.AccessPattern + UnifiedModelingLanguage.OperationModifiersPattern +
			@"event\s+" +
			@"(?<type>" + UnifiedModelingLanguage.GenericTypePattern2 + @")\s+" +
			@"(?<name>" + UnifiedModelingLanguage.GenericOperationNamePattern + ")" +
			UnifiedModelingLanguage.DeclarationEnding;

		const string EventNamePattern =
			@"^\s*(?<name>" + UnifiedModelingLanguage.GenericOperationNamePattern + @")\s*$";

		static Regex eventRegex = new Regex(EventPattern, RegexOptions.ExplicitCapture);
		static Regex nameRegex = new Regex(EventNamePattern, RegexOptions.ExplicitCapture);

		bool isExplicitImplementation = false;
		
		/// <exception cref="ArgumentNullException">
		/// <paramref name="parent"/> is null.
		/// </exception>
		internal UmlEvent(CompositeType parent) : this("NewEvent", parent)
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
        internal UmlEvent(string name, CompositeType parent)
            : base(name, parent)
		{
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="value"/> does not fit to the syntax.
		/// </exception>
		public override string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				Match match = nameRegex.Match(value);

				if (match.Success) {
					try {
						RaiseChangedEvent = false;
						ValidName = match.Groups["name"].Value;
						IsExplicitImplementation = match.Groups["namedot"].Success;
					}
					finally {
						RaiseChangedEvent = true;
					}
				}
				else {
					throw new BadSyntaxException(Strings.ErrorInvalidName);
				}
			}
		}

		protected override string DefaultType
		{
			get { return "EventHandler"; }
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set access visibility.
		/// </exception>
		public override AccessModifier AccessModifier
		{
			get
			{
				return base.AccessModifier;
			}
			set
			{
				if (value != AccessModifier.Default && IsExplicitImplementation) {
					throw new BadSyntaxException(
						Strings.ErrorExplicitImplementationAccess);
				}
				if (value != AccessModifier.Default && Parent is InterfaceType) {
					throw new BadSyntaxException(
						Strings.ErrorInterfaceMemberAccess);
				}

				base.AccessModifier = value;
			}
		}

		public override bool IsAccessModifiable
		{
			get
			{
				return (base.IsAccessModifiable && !IsExplicitImplementation);
			}
		}

		public bool IsExplicitImplementation
		{
			get
			{
				return isExplicitImplementation;
			}
			private set
			{
				if (isExplicitImplementation != value) {
					try {
						RaiseChangedEvent = false;

						if (value)
							AccessModifier = AccessModifier.Default;
						isExplicitImplementation = value;
						Changed();
					}
					finally {
						RaiseChangedEvent = true;
					}
				}
			}
		}

		public override bool HasBody
		{
			get
			{
				return IsExplicitImplementation;
			}
		}

		public override Language Language
		{
            get { return UnifiedModelingLanguage.Instance; }
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="declaration"/> does not fit to the syntax.
		/// </exception>
		public override void InitFromString(string declaration)
		{
			Match match = eventRegex.Match(declaration);
			RaiseChangedEvent = false;

			try {
				if (match.Success) {
					ClearModifiers();
					Group nameGroup = match.Groups["name"];
					Group typeGroup = match.Groups["type"];
					Group accessGroup = match.Groups["access"];
					Group modifierGroup = match.Groups["modifier"];
					Group nameDotGroup = match.Groups["namedot"];

                    if (UnifiedModelingLanguage.Instance.IsForbiddenName(nameGroup.Value))
						throw new BadSyntaxException(Strings.ErrorInvalidName);
                    if (UnifiedModelingLanguage.Instance.IsForbiddenTypeName(typeGroup.Value))
						throw new BadSyntaxException(Strings.ErrorInvalidTypeName);

					ValidName = nameGroup.Value;
					ValidType = typeGroup.Value;
					IsExplicitImplementation = nameDotGroup.Success;
					AccessModifier = Language.TryParseAccessModifier(accessGroup.Value);

					foreach (Capture modifierCapture in modifierGroup.Captures) {
						if (modifierCapture.Value == "static")
							IsStatic = true;
						if (modifierCapture.Value == "virtual")
							IsVirtual = true;
						if (modifierCapture.Value == "abstract")
							IsAbstract = true;
						if (modifierCapture.Value == "override")
							IsOverride = true;
						if (modifierCapture.Value == "sealed")
							IsSealed = true;
						if (modifierCapture.Value == "new")
							IsHider = true;
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
			bool needsSemicolon = !IsExplicitImplementation;
			return GetDeclarationLine(needsSemicolon);
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
			if (IsStatic)
				builder.Append("static ");
			if (IsVirtual)
				builder.Append("virtual ");
			if (IsAbstract)
				builder.Append("abstract ");
			if (IsSealed)
				builder.Append("sealed ");
			if (IsOverride)
				builder.Append("override ");

			builder.AppendFormat("event {0} {1}", Type, Name);

			if (withSemicolon && !HasBody)
				builder.Append(";");

			return builder.ToString();
		}

		public override Operation Clone(CompositeType newParent)
		{
            UmlEvent newEvent = new UmlEvent(newParent);
			newEvent.CopyFrom(this);
			return newEvent;
		}

		public override string ToString()
		{
			return GetDeclarationLine(false);
		}
	}
}
