using System.Text.RegularExpressions;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Members;

namespace EAP.ModelFirst.Uml
{
    internal sealed class UmlEnumValue : EnumValue
    {
        // <name> [= value]
        const string EnumNamePattern = "(?<name>" + UnifiedModelingLanguage.NamePattern + ")";
        const string EnumItemPattern = @"^\s*" + EnumNamePattern +
            @"(\s*=\s*(?<value>\d+))?\s*$";

        static Regex enumItemRegex = new Regex(EnumItemPattern, RegexOptions.ExplicitCapture);

        /// <exception cref="BadSyntaxException">
        /// The <paramref name="declaration"/> does not fit to the syntax.
        /// </exception>
        internal UmlEnumValue(string declaration)
            : base(declaration)
        {
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
            return Name + " = " + InitValue;
        }

        protected internal override EnumValue Clone()
        {
            return new UmlEnumValue(GetDeclaration());
        }
    }
}
