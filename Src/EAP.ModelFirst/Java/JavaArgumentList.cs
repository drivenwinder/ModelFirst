using System;
using System.Text.RegularExpressions;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Parameters;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Java
{
	internal class JavaArgumentList : ArgumentList
	{
		// <type> <name> [,]
		const string JavaParameterPattern =
			@"\s*(?<type>" + JavaLanguage.GenericTypePattern2 + @")\s+" +
			@"(?<name>" + JavaLanguage.NamePattern + @")\s*(,|$)";
		const string ParameterStringPattern = @"^\s*(" + JavaParameterPattern + ")*$";

		static Regex parameterRegex =
			new Regex(JavaParameterPattern, RegexOptions.ExplicitCapture);
		static Regex singleParamterRegex =
			new Regex("^" + JavaParameterPattern + "$", RegexOptions.ExplicitCapture);
		static Regex parameterStringRegex =
			new Regex(ParameterStringPattern, RegexOptions.ExplicitCapture);

		internal JavaArgumentList()
		{
		}

		internal JavaArgumentList(int capacity)
			: base(capacity)
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

				if (IsReservedName(nameGroup.Value))
					throw new ReservedNameException(nameGroup.Value);

				Parameter parameter = new JavaParameter(nameGroup.Value, typeGroup.Value);
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

				if (IsReservedName(nameGroup.Value, index))
					throw new ReservedNameException(nameGroup.Value);

				Parameter newParameter = new JavaParameter(nameGroup.Value, typeGroup.Value);
				InnerList[index] = newParameter;
				return newParameter;
			}
			else
			{
				throw new BadSyntaxException(
					Strings.ErrorInvalidParameterDeclaration);
			}
		}

		public override ArgumentList Clone()
		{
			JavaArgumentList argumentList = new JavaArgumentList(Capacity);
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
				foreach (Match match in parameterRegex.Matches(declaration))
				{
					Group nameGroup = match.Groups["name"];
					Group typeGroup = match.Groups["type"];

					InnerList.Add(new JavaParameter(nameGroup.Value, typeGroup.Value));
				}
			}
			else
			{
				throw new BadSyntaxException(
					Strings.ErrorInvalidParameterDeclaration);
			}
		}
	}
}