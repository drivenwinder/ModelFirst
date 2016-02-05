using System;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Parameters;

namespace EAP.ModelFirst.CSharp
{
	internal sealed class CSharpParameter : Parameter
	{
		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> or <paramref name="type"/> 
		/// does not fit to the syntax.
		/// </exception>
		internal CSharpParameter(string name, string type, ParameterModifier modifier, string defaultValue)
			: base(name, type, modifier, defaultValue)
		{
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
			protected set
			{
				if (value == "void")
				{
					throw new BadSyntaxException(Strings.ErrorInvalidParameterDeclaration);
				}
				base.Type = value;
			}
		}

		public override ParameterModifier Modifier
		{
			get
			{
				return base.Modifier;
			}
			protected set
			{
				if (value != ParameterModifier.In && DefaultValue != null)
				{
					throw new BadSyntaxException(Strings.ErrorInvalidParameterDeclaration);
				}
				base.Modifier = value;
			}
		}

		public override string DefaultValue
		{
			get
			{
				return base.DefaultValue;
			}
			protected set
			{
				if (!string.IsNullOrWhiteSpace(value) && Modifier != ParameterModifier.In)
				{
					throw new BadSyntaxException(Strings.ErrorInvalidParameterDeclaration);
				}
				base.DefaultValue = value;
			}
		}

		public override Language Language
		{
			get { return CSharpLanguage.Instance; }
		}

		public override string GetDeclaration()
		{
			if (DefaultValue != null)
			{
				return Type + " " + Name + " = " + DefaultValue;
			}
			else if (Modifier == ParameterModifier.In)
			{
				return Type + " " + Name;
			}
			else if (Modifier == ParameterModifier.Inout)
			{
				return string.Format("ref {0} {1}", Type, Name);
			}
			else
			{
				return string.Format("{0} {1} {2}",
					Modifier.ToString().ToLower(), Type, Name);
			}
		}

		public override Parameter Clone()
		{
			return new CSharpParameter(Name, Type, Modifier, DefaultValue);
		}
	}
}
