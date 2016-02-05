using System;
using System.Text.RegularExpressions;

namespace EAP.ModelFirst.Core.Project.Parameters
{
	public abstract class Parameter : LanguageElement
	{
		string type;
		string name;
		ParameterModifier modifier;
		string defaultValue;

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> or <paramref name="type"/> 
		/// does not fit to the syntax.
		/// </exception>
		protected Parameter(string name, string type, ParameterModifier modifier, string defaultValue)
		{
			Initializing = true;
			Name = name;
			Type = type;
			Modifier = modifier;
			DefaultValue = defaultValue;
			Initializing = false;
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="value"/> does not fit to the syntax.
		/// </exception>
		public virtual string Name
		{
			get
			{
				return name;
			}
			protected set
			{
				string newName = Language.GetValidName(value, false);

				if (newName != name) {
					name = newName;
					Changed();
				}
			}
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="value"/> does not fit to the syntax.
		/// </exception>
		public virtual string Type
		{
			get
			{
				return type;
			}
			protected set
			{
				string newType = Language.GetValidTypeName(value);

				if (newType != type) {
					type = newType;
					Changed();
				}
			}
		}

		public virtual ParameterModifier Modifier
		{
			get
			{
				return modifier;
			}
			protected set
			{
				if (modifier != value) {
					modifier = value;
					Changed();
				}
			}
		}

		public virtual string DefaultValue
		{
			get
			{
				return defaultValue;
			}
			protected set
			{
				if (string.IsNullOrWhiteSpace(value))
					value = null;

				if (defaultValue != value) {
					defaultValue = value;
					Changed();
				}
			}
		}

		public bool IsOptional
		{
			get { return (DefaultValue != null); }
		}

		private static string GetModifierString(ParameterModifier modifier)
		{
			switch (modifier) {
				case ParameterModifier.Inout:
					return "inout";

				case ParameterModifier.Out:
					return "out";

				case ParameterModifier.Params:
					return "params";

				default:
					return "in";
			}
		}

		public string GetUmlDescription(bool getName)
		{
			if (getName) {
				if (Modifier == ParameterModifier.In)
					return Name + ": " + Type;
				else
					return string.Format("{0} {1}: {2}", GetModifierString(Modifier), Name, Type);
			}
			else {
				return Type;
			}
		}

		public abstract Parameter Clone();

		public override string ToString()
		{
			return GetDeclaration();
		}
	}
}
