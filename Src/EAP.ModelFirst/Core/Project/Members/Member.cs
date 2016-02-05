using System;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Core.Project.Members
{
	public abstract class Member : LanguageElement
	{
		string name;
		string type;
		AccessModifier access = AccessModifier.Default;
        CompositeType parent;
        string comments;

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// The language of <paramref name="parent"/> does not equal.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="parent"/> is null.
		/// </exception>
		protected Member(string name, CompositeType parent)
		{
			if (parent == null)
				throw new ArgumentNullException("parent");
            if (parent.Language != this.Language)
				throw new ArgumentException(Strings.ErrorLanguagesDoNotEqual);

			Initializing = true;
			Parent = parent;
			Name = name;
			ValidType = DefaultType;
			Initializing = false;
		}

		public abstract MemberType MemberType
		{
			get;
		}

		/// <exception cref="ArgumentNullException">
		/// <paramref name="value"/> is null.
		/// </exception>
		public CompositeType Parent
		{
			get
			{
				return parent;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				if (parent != value) {
					parent = value;
					Changed();
				}
			}
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="value"/> does not fit to the syntax.
		/// </exception>
		public virtual string Name
		{
			get
			{
				return name;
			}
			set
			{
				
				string newName = Language.GetValidName(value, true);

				if (newName != name) {
					name = newName;
					Changed();
				}
			}
		}

        public virtual string Comments
        {
            get { return comments; }
            set
            {
                if (comments != value)
                {
                    comments = value;
                    Changed();
                }
            }
        }

		protected string ValidName
		{
			set
			{
				if (name != value) {
					name = value;
					Changed();
				}
			}
		}

		public virtual bool IsNameReadonly
		{
			get { return false; }
		}

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="value"/> does not fit to the syntax.
		/// </exception>
		public virtual string Type
		{
			get
			{
				return type;
			}
			set
			{
				string newType = Language.GetValidTypeName(value);

				if (newType != type) {
					type = newType;
					Changed();
				}
			}
		}

		protected string ValidType
		{
			set {
				if (type != value) {
					type = value;
					Changed();
				}
			}
		}

		public virtual bool IsTypeReadonly
		{
			get { return false; }
		}

		protected abstract string DefaultType
		{
			get;
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set access visibility.
		/// </exception>
		public virtual AccessModifier AccessModifier
		{
			get
			{
				return access;
			}
			set
			{
				if (!Language.IsValidModifier(value))
					throw new BadSyntaxException(Strings.ErrorInvalidModifier);

				if (access != value) {
					access = value;
					Changed();
				}
			}
		}

		public virtual AccessModifier DefaultAccess
		{
			get { return Parent.DefaultMemberAccess; }
		}

		public AccessModifier Access
		{
			get
			{
				if (AccessModifier == AccessModifier.Default)
					return DefaultAccess;
				else
					return AccessModifier;
			}
		}

		public virtual bool IsAccessModifiable
		{
			get { return true; }
		}

		public abstract bool IsModifierless
		{
			get;
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set static modifier.
		/// </exception>
		public abstract bool IsStatic
		{
			get;
			set;
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set hider modifier.
		/// </exception>
		public abstract bool IsHider
		{
			get;
			set;
		}

		public string GetUmlDescription()
		{
			return GetUmlDescription(true, true, true, true);
		}

		public string GetUmlDescription(bool getType, bool getParameters, bool getParameterNames)
		{
			return GetUmlDescription(getType, getParameters, getParameterNames, getType);
		}

		public abstract string GetUmlDescription(bool getType, bool getParameters,
			bool getParameterNames, bool getInitValue);

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="declaration"/> does not fit to the syntax.
		/// </exception>
		public abstract void InitFromString(string declaration);

		protected virtual void CopyFrom(Member member)
		{
			name = member.name;
			type = member.type;
			access = member.access;
            comments = member.comments;
		}
	}
}
