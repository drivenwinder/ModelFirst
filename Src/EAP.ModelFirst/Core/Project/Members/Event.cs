using System;
using System.Text;
using System.Text.RegularExpressions;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Core.Project.Members
{
	public abstract class Event : Operation
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
		protected Event(string name, CompositeType parent) : base(name, parent)
		{
		}

		public sealed override MemberType MemberType
		{
			get { return MemberType.Event; }
		}

		public sealed override string GetUmlDescription(bool getType, bool getParameters,
			bool getParameterNames, bool getInitValue)
		{
			if (getType)
				return Name + " : " + Type;
			else
				return Name;
		}
	}
}
