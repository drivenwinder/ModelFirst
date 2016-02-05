using System;

namespace EAP.ModelFirst.Core.Project
{
	public abstract class LanguageElement : Element
    {
        [System.ComponentModel.Browsable(false)]
        public abstract Language Language { get; }

		public abstract string GetDeclaration();

		public override string ToString()
		{
			return GetDeclaration();
		}

		//[Obsolete]
		protected static string GetNameWithoutGeneric(string name)
		{
			int index = name.IndexOf('<');
			if (index > 0)
				return name.Substring(0, index);
			else
				return name;
		}
	}
}
