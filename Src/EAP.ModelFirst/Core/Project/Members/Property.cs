using System;
using System.Text;
using System.Text.RegularExpressions;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Core.Project.Members
{
	public abstract class Property : Operation
	{
		bool isReadonly = false;
        bool isWriteonly = false;
		AccessModifier readAccess = AccessModifier.Default;
		AccessModifier writeAccess = AccessModifier.Default;

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// The language of <paramref name="parent"/> does not equal.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="parent"/> is null.
		/// </exception>
		protected Property(string name, CompositeType parent) : base(name, parent)
        {
		}

		public sealed override MemberType MemberType
		{
			get { return MemberType.Property; }
		}

		public override bool HasBody
		{
			get { return true; }
		}

		public bool HasImplementation
		{
			get
			{
				return (!IsAbstract && !(Parent is InterfaceType));
			}
		}

		public bool IsReadonly
		{
			get
			{
				return isReadonly;
			}
			set
			{
				if (isReadonly != value) {
					if (value)
						isWriteonly = false;
					isReadonly = value;
					Changed();
				}
			}
		}

		public bool IsWriteonly
		{
			get
			{
				return isWriteonly;
			}
			set
			{
				if (isWriteonly != value) {
					if (value)
						isReadonly = false;
					isWriteonly = value;
					Changed();
				}
			}
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set accessor modifier.
		/// </exception>
		public AccessModifier ReadAccess
		{
			get
			{
				return readAccess;
			}
			protected set
			{
				if (value == readAccess)
					return;

				if (value == AccessModifier.Default || (value != Access &&
					WriteAccess == AccessModifier.Default && !IsReadonly && !IsWriteonly))
				{
					readAccess = value;
					Changed();
				}
				else {
					throw new BadSyntaxException(Strings.ErrorAccessorModifier);
				}
			}
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set accessor modifier.
		/// </exception>
		public AccessModifier WriteAccess
		{
			get
			{
				return writeAccess;
			}
			protected set
			{
				if (value == writeAccess)
					return;

				if (value == AccessModifier.Default || (value != Access &&
					ReadAccess == AccessModifier.Default && !IsReadonly && !IsWriteonly))
				{
					writeAccess = value;
					Changed();
				}
				else {
					throw new BadSyntaxException(Strings.ErrorAccessorModifier);
				}
			}
		}

		protected override void CopyFrom(Member member)
		{
			base.CopyFrom(member);

			Property property = (Property) member;
			isReadonly = property.isReadonly;
			isWriteonly = property.isWriteonly;
			readAccess = property.readAccess;
			writeAccess = property.writeAccess;
		}
	}
}
