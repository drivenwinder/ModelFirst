using System;
using System.Collections;
using System.Collections.Generic;

namespace EAP.ModelFirst.Core.Project.Parameters
{
	public abstract class ArgumentList : CollectionBase, IEnumerable<Parameter>
	{
		protected ArgumentList()
		{
		}

		protected ArgumentList(int capacity) : base(capacity)
		{
		}

		public Parameter this[int index]
		{
			get { return (InnerList[index] as Parameter); }
		}

		IEnumerator<Parameter> IEnumerable<Parameter>.GetEnumerator()
		{
			for (int i = 0; i < InnerList.Count; i++)
				yield return (Parameter) InnerList[i];
		}

		public void Remove(Parameter parameter)
		{
			InnerList.Remove(parameter);
		}

		protected bool IsReservedName(string name)
		{
			return IsReservedName(name, -1);
		}

		protected bool IsReservedName(string name, int index)
		{
			for (int i = 0; i < Count; i++) {
				if (((Parameter) InnerList[i]).Name == name && i != index)
					return true;
			}
			return false;
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="declaration"/> does not fit to the syntax.
		/// </exception>
		/// <exception cref="ReservedNameException">
		/// The parameter name is already exists.
		/// </exception>
		public abstract Parameter Add(string declaration);

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="declaration"/> does not fit to the syntax.
		/// </exception>
		/// <exception cref="ReservedNameException">
		/// The parameter name is already exists.
		/// </exception>
		public abstract Parameter ModifyParameter(Parameter parmeter, string declaration);

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="declaration"/> does not fit to the syntax.
		/// </exception>
		public abstract void InitFromString(string declaration);

		public abstract ArgumentList Clone();
	}
}
