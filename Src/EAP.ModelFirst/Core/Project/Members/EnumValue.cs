using System;
using System.Text.RegularExpressions;

namespace EAP.ModelFirst.Core.Project.Members
{
	public abstract class EnumValue : LanguageElement
	{
        string name = null;
        int? initValue;

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="declaration"/> does not fit to the syntax.
		/// </exception>
		protected EnumValue(string declaration)
		{
			Initializing = true;
			InitFromString(declaration);
			Initializing = false;
		}

		public string Name
		{
			get
			{
				return name;
			}
			protected set
			{
				if (name != value)
				{
					name = value;
					Changed();
				}
			}
		}

        public int? InitValue
        {
            get { return initValue; }
            set { initValue = value; }
        }

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="declaration"/> does not fit to the syntax.
		/// </exception>
		public abstract void InitFromString(string declaration);

		protected internal abstract EnumValue Clone();

		public override string ToString()
		{
			return GetDeclaration();
		}
	}
}