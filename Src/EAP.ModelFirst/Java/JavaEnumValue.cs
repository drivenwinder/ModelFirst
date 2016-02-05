using System;
using System.Text;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Core.Project.Members;
using System.Text.RegularExpressions;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Java
{
	internal sealed class JavaEnumValue : EnumValue
    {
        // <name> (value)
        const string EnumNamePattern = "(?<name>" + JavaLanguage.NamePattern + ")";
        const string EnumItemPattern = @"^\s*" + EnumNamePattern +
            @"(\s*=\s*(?<value>\d+))?\s*$";

        static Regex enumItemRegex = new Regex(EnumItemPattern, RegexOptions.ExplicitCapture);

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="declaration"/> does not fit to the syntax.
		/// </exception>
		internal JavaEnumValue(string declaration) : base(declaration)
		{
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="declaration"/> does not fit to the syntax.
		/// </exception>
		public override void InitFromString(string declaration)
        {
            Match match = enumItemRegex.Match(declaration);

            try
            {
                RaiseChangedEvent = false;

                if (match.Success)
                {
                    Group nameGroup = match.Groups["name"];
                    Group valueGroup = match.Groups["value"];

                    Name = nameGroup.Value;
                    if (valueGroup.Success)
                    {
                        int intValue;
                        if (int.TryParse(valueGroup.Value, out intValue))
                            InitValue = intValue;
                        else
                            InitValue = null;
                    }
                    else
                    {
                        InitValue = null;
                    }
                }
                else
                {
                    throw new BadSyntaxException(Strings.ErrorInvalidDeclaration);
                }
            }
            finally
            {
                RaiseChangedEvent = true;
            }
		}

		public override string GetDeclaration()
        {
            if (InitValue == null)
                return Name;
            return Name + "(" + InitValue + ")";
		}

		protected internal override EnumValue Clone()
		{
			return new JavaEnumValue(Name);
		}

        public override Language Language
        {
            get { return JavaLanguage.Instance; }
        }
	}
}
