using System;
using System.Text.RegularExpressions;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Parameters;

namespace EAP.ModelFirst.Uml
{
	internal class UmlArgumentList : ArgumentList
	{
		// [<modifiers>] <type> <name> [,]
		const string ParameterPattern =
			@"(?<modifier>out|ref|params)?(?(modifier)\s+|)" +
			@"(?<type>" + UnifiedModelingLanguage.GenericTypePattern2 + @")\s+" +
			@"(?<name>" + UnifiedModelingLanguage.NamePattern + @")" +
			@"(\s*=\s*(?<defval>([^,""]+|""(\\""|[^""])*"")))?";
		const string ParameterStringPattern = @"^\s*(" + ParameterPattern + @"\s*(,\s*|$))*$";

		static Regex parameterRegex =
			new Regex(ParameterPattern, RegexOptions.ExplicitCapture);
		static Regex singleParamterRegex =
			new Regex("^" + ParameterPattern + "$", RegexOptions.ExplicitCapture);
		static Regex parameterStringRegex =
			new Regex(ParameterStringPattern, RegexOptions.ExplicitCapture);

		internal UmlArgumentList()
		{
		}

		private UmlArgumentList(int capacity) : base(capacity)
		{
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="declaration"/> does not fit to the syntax.
		/// </exception>
		/// <exception cref="ReservedNameException">
		/// The parameter name is already exists.
		/// </exception>
		public override Parameter Add(string declaration)
		{
			Match match = singleParamterRegex.Match(declaration);

			if (match.Success)
			{
				Group nameGroup = match.Groups["name"];
				Group typeGroup = match.Groups["type"];
				Group modifierGroup = match.Groups["modifier"];
				Group defvalGroup = match.Groups["defval"];

				if (IsReservedName(nameGroup.Value))
					throw new ReservedNameException(nameGroup.Value);

				Parameter parameter = new UmlParameter(nameGroup.Value, typeGroup.Value,
					ParseParameterModifier(modifierGroup.Value), defvalGroup.Value);
				InnerList.Add(parameter);

				return parameter;
			}
			else
			{
				throw new BadSyntaxException(
					Strings.ErrorInvalidParameterDeclaration);
			}
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="declaration"/> does not fit to the syntax.
		/// </exception>
		/// <exception cref="ReservedNameException">
		/// The parameter name is already exists.
		/// </exception>
		public override Parameter ModifyParameter(Parameter parameter, string declaration)
		{
			Match match = singleParamterRegex.Match(declaration);
			int index = InnerList.IndexOf(parameter);

			if (index < 0)
				return parameter;

			if (match.Success)
			{
				Group nameGroup = match.Groups["name"];
				Group typeGroup = match.Groups["type"];
				Group modifierGroup = match.Groups["modifier"];
				Group defvalGroup = match.Groups["defval"];

				if (IsReservedName(nameGroup.Value, index))
					throw new ReservedNameException(nameGroup.Value);

				Parameter newParameter = new UmlParameter(nameGroup.Value, typeGroup.Value,
					ParseParameterModifier(modifierGroup.Value), defvalGroup.Value);
				InnerList[index] = newParameter;
				return newParameter;
			}
			else
			{
				throw new BadSyntaxException(
					Strings.ErrorInvalidParameterDeclaration);
			}
		}

		private ParameterModifier ParseParameterModifier(string modifierString)
		{
			switch (modifierString)
			{
				case "ref":
					return ParameterModifier.Inout;

				case "out":
					return ParameterModifier.Out;

				case "params":
					return ParameterModifier.Params;

				case "in":
				default:
					return ParameterModifier.In;
			}
		}

		public override ArgumentList Clone()
		{
			UmlArgumentList argumentList = new UmlArgumentList(Capacity);
			foreach (Parameter parameter in InnerList)
			{
				 argumentList.InnerList.Add(parameter.Clone());
			}
			return argumentList;
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="declaration"/> does not fit to the syntax.
		/// </exception>
		public override void InitFromString(string declaration)
		{
			if (parameterStringRegex.IsMatch(declaration))
			{
				Clear();

				bool optionalPart = false;
				foreach (Match match in parameterRegex.Matches(declaration))
				{
					Group nameGroup = match.Groups["name"];
					Group typeGroup = match.Groups["type"];
					Group modifierGroup = match.Groups["modifier"];
					Group defvalGroup = match.Groups["defval"];

					if (defvalGroup.Success)
						optionalPart = true;
					else if (optionalPart)
					{
//						throw new BadSyntaxException(Strings.ErrorInvalidParameterDeclaration);
					}
					InnerList.Add(new UmlParameter(nameGroup.Value, typeGroup.Value,
						ParseParameterModifier(modifierGroup.Value), defvalGroup.Value));
				}
			}
			else
			{
				throw new BadSyntaxException(Strings.ErrorInvalidParameterDeclaration);
			}
		}
	}
}
