// NClass - Free class diagram editor
// Copyright (C) 2006-2009 Balazs Tihanyi
// 
using System;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Parameters;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Java
{
	internal sealed class JavaParameter : Parameter
	{
		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> or <paramref name="type"/> 
		/// does not fit to the syntax.
		/// </exception>
		internal JavaParameter(string name, string type) : base(name, type, ParameterModifier.In, null)
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
				if (value == "void") {
					throw new BadSyntaxException(
						Strings.ErrorInvalidParameterDeclaration);
				}
				base.Type = value;
			}
		}

		public override string DefaultValue
		{
			get
			{
				return null;
			}
			protected set
			{
				if (value != null)
					throw new BadSyntaxException(Strings.ErrorInvalidParameterDeclaration);
			}
		}

		public override Language Language
		{
			get { return JavaLanguage.Instance; }
		}

		public override string GetDeclaration()
		{
			return Type + " " + Name;
		}

		public override Parameter Clone()
		{
			return new JavaParameter(Name, Type);
		}
	}
}
