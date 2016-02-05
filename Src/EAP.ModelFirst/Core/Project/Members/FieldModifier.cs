using System;

namespace EAP.ModelFirst.Core.Project.Members
{
	[Flags]
	public enum FieldModifier
	{
		/// <summary>
		/// The field has no modifiers.
		/// </summary>
		None = 0,

		/// <summary>
		/// A static field does not operate on a specific instance,
		/// it belongs to the type.
		/// </summary>
		Static = 1,

		/// <summary>
		/// A readonly field cannot be modified after the object initialization.
		/// </summary>
		Readonly = 2,

		/// <summary>
		/// A constant field cannot be changed runtime.
		/// </summary>
		Constant = 4,

		/// <summary>
		/// The derived class member hides the base class member.
		/// </summary>
		Hider = 8,

		/// <summary>
		/// The field is volatile.
		/// </summary>
		Volatile = 16,
	}
}
