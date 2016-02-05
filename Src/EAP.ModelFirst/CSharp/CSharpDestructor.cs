using System;
using EAP.ModelFirst.Core.Project;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Project.Members;

namespace EAP.ModelFirst.CSharp
{
	internal sealed class CSharpDestructor : Destructor
	{
		/// <exception cref="ArgumentException">
		/// The language of <paramref name="parent"/> does not equal.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="parent"/> is null.
		/// </exception>
		internal CSharpDestructor(CompositeType parent) : base(parent)
		{
			AccessModifier = AccessModifier.Default;
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="value"/> does not fit to the syntax.
		/// </exception>
		public override string Name
		{
			get
			{
				return "~" + GetNameWithoutGeneric(Parent.Name);
			}
			set
			{
				if (value != null && value != "~" + GetNameWithoutGeneric(Parent.Name))
					throw new BadSyntaxException(Strings.ErrorDestructorName);
			}
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set access visibility.
		/// </exception>
		public override AccessModifier AccessModifier
		{
			get
			{
				return base.AccessModifier;
			}
			set
			{
				if (value != AccessModifier.Default)
					throw new BadSyntaxException(Strings.ErrorCannotSetAccess);
			}
		}

		public override AccessModifier DefaultAccess
		{
			get
			{
				return AccessModifier.Private;
			}
		}

		public override bool IsAccessModifiable
		{
			get { return false; }
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set virtual modifier.
		/// </exception>
		public override bool IsVirtual
		{
			get
			{
				return base.IsVirtual;
			}
			set
			{
				if (value)
					throw new BadSyntaxException(Strings.ErrorCannotSetModifier);
			}
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set override modifier.
		/// </exception>
		public override bool IsOverride
		{
			get
			{
				return base.IsOverride;
			}
			set
			{
				if (value)
					throw new BadSyntaxException(Strings.ErrorCannotSetModifier);
			}
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set sealed modifier.
		/// </exception>
		public override bool IsSealed
		{
			get
			{
				return base.IsSealed;
			}
			set
			{
				if (value)
					throw new BadSyntaxException(Strings.ErrorCannotSetModifier);
			}
		}

		public override Language Language
		{
			get { return CSharpLanguage.Instance; }
		}

		public override void InitFromString(string declaration)
		{
			ValidName = "~" + Parent.Name;
		}

		public override string GetDeclaration()
		{
			return Name + "()";
		}

		public override Operation Clone(CompositeType newParent)
		{
			return new CSharpDestructor(newParent);
		}
	}
}
