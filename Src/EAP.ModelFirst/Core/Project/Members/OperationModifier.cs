using System;

namespace EAP.ModelFirst.Core.Project.Members
{
	[Flags]
	public enum OperationModifier
	{
		/// <summary>
		/// The operation has no modifiers.
		/// </summary>
		None = 0,

		/// <summary>
		/// A static operation does not operate on a specific instance,
		/// it belongs to the type.
		/// </summary>
		Static = 1,

		/// <summary>
		/// Declares a method whose implementation can be changed by an 
		/// overriding member in a derived class.
		/// </summary>
		Virtual = 2,

		/// <summary>
		/// Abstract members must be implemented by classes
		/// that derive from the abstract class.
		/// </summary>
		Abstract = 4,

		/// <summary>
		/// Provides a new implementation of a virtual member
		/// inherited from a base class.
		/// </summary>
		Override = 8,

		/// <summary>
		/// A sealed method overrides a method in a base class,
		/// but itself cannot be overridden further in any derived class.
		/// </summary>
		Sealed = 16,

		/// <summary>
		/// The derived class member hides the base class member.
		/// </summary>
		Hider = 32
	}
}
