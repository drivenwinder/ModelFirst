using System;
using System.Text;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Core.Project.Members
{
	public abstract class Method : Operation
	{
		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// The language of <paramref name="parent"/> does not equal.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="parent"/> is null.
		/// </exception>
		protected Method(string name, CompositeType parent) : base(name, parent)
		{
		}

		public override MemberType MemberType
		{
			get { return MemberType.Method; }
		}

		public abstract bool IsOperator
		{
			get;
		}

		public sealed override string GetUmlDescription(bool getType, bool getParameters,
			bool getParameterNames, bool getInitValue)
		{
			StringBuilder builder = new StringBuilder(100);

			builder.AppendFormat("{0}(", Name);

			if (getParameters) {
				for (int i = 0; i < ArgumentList.Count; i++) {
					builder.Append(ArgumentList[i].GetUmlDescription(getParameterNames));
					if (i < ArgumentList.Count - 1)
						builder.Append(", ");
				}
			}

			if (getType && !string.IsNullOrEmpty(Type))
				builder.AppendFormat(") : {0}", Type);
			else
				builder.Append(")");

			return builder.ToString();
		}
	}
}
