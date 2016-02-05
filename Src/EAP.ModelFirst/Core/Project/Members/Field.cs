using System;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Core.Project.Members
{
	public abstract class Field : Member, IDbSchema
	{
		FieldModifier modifier = FieldModifier.None;
		string initialValue = "";
        bool getter = true;
        bool setter = true;
        bool generateDbColumn;
        DbSchema schemaInfo;

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// The language of <paramref name="parent"/> does not equal.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="parent"/> is null.
		/// </exception>
		protected Field(string name, CompositeType parent) : base(name, parent)
        {
		}

		public sealed override MemberType MemberType
		{
			get { return MemberType.Field; }
		}

        public bool GenerateDbColumn
        {
            get { return generateDbColumn; }
            set
            {
                if (generateDbColumn != value)
                {
                    generateDbColumn = value;
                    Changed();
                }
            }
        }

        public DbSchema DbSchema
        {
            get
            {
                if (schemaInfo == null)
                {
                    schemaInfo = new DbSchema();
                    schemaInfo.Modified += delegate(object sender, EventArgs e) { Changed(); };
                }
                return schemaInfo;
            }
        }

		public override string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
                var related = GenerateDbColumn && DbSchema.Name == base.Name;
				ValidName = Language.GetValidName(value, false);
				if(related)
					DbSchema.Name = base.Name;
			}
		}

        public bool Getter
        {
            get
            {
                return getter;
            }
            set
            {
                if (getter != value)
                {
                    getter = value;
                    Changed();
                }
            }
        }

        public bool Setter
        {
            get
            {
                return setter;
            }
            set
            {
                if (setter != value)
                {
                    if (value && IsReadonly)
                        throw new BadSyntaxException(Strings.ErrorConnotSetSetterForReadOnlyField);
                    setter = value;
                    Changed();
                }
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
				if (value == AccessModifier)
					return;

				AccessModifier previousAccess = base.AccessModifier;

				try {
					RaiseChangedEvent = false;

					base.AccessModifier = value;
					Language.ValidateField(this);
				}
				catch {
					base.AccessModifier = previousAccess;
					throw;
				}
				finally {
					RaiseChangedEvent = true;
				}
			}
		}

		public FieldModifier Modifier
		{
			get { return modifier; }
		}

		public sealed override bool IsModifierless
		{
			get
			{
				return (modifier == FieldModifier.None);
			}
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set static modifier.
		/// </exception>
		public override bool IsStatic
		{
			get
			{
				return ((modifier & FieldModifier.Static) != 0);
			}
			set
			{
				if (value == IsStatic)
					return;

				FieldModifier previousModifier = modifier;

				try {
					if (value)
						modifier |= FieldModifier.Static;
					else
						modifier &= ~FieldModifier.Static;
					Language.ValidateField(this);
					Changed();
				}
				catch {
					modifier = previousModifier;
					throw;
				}
			}
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set hider modifier.
		/// </exception>
		public override bool IsHider
		{
			get
			{
				return ((modifier & FieldModifier.Hider) != 0);
			}
			set
			{
				if (value == IsHider)
					return;

				FieldModifier previousModifier = modifier;

				try {
					if (value)
						modifier |= FieldModifier.Hider;
					else
						modifier &= ~FieldModifier.Hider;
					Language.ValidateField(this);
					Changed();
				}
				catch {
					modifier = previousModifier;
					throw;
				}
			}
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set readonly modifier.
		/// </exception>
		public virtual bool IsReadonly
		{
			get
			{
				return ((modifier & FieldModifier.Readonly) != 0);
			}
			set
			{
				if (value == IsReadonly)
					return;

				FieldModifier previousModifier = modifier;

				try {
					if (value)
						modifier |= FieldModifier.Readonly;
					else
						modifier &= ~FieldModifier.Readonly;
					Language.ValidateField(this);
                    if (value)
                        setter = false;
					Changed();
				}
				catch {
					modifier = previousModifier;
					throw;
				}
			}
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set constant modifier.
		/// </exception>
		public virtual bool IsConstant
		{
			get
			{
				return ((modifier & FieldModifier.Constant) != 0);
			}
			set
			{
				if (value == IsConstant)
					return;

				FieldModifier previousModifier = modifier;

				try {
					if (value)
						modifier |= FieldModifier.Constant;
					else
						modifier &= ~FieldModifier.Constant;
					Language.ValidateField(this);
					Changed();
				}
				catch {
					modifier = previousModifier;
					throw;
				}
			}
		}

		/// <exception cref="BadSyntaxException">
		/// Cannot set volatile modifier.
		/// </exception>
		public virtual bool IsVolatile
		{
			get
			{
				return ((modifier & FieldModifier.Volatile) != 0);
			}
			set
			{
				if (value == IsVolatile)
					return;

				FieldModifier previousModifier = modifier;

				try {
					if (value)
						modifier |= FieldModifier.Volatile;
					else
						modifier &= ~FieldModifier.Volatile;
					Language.ValidateField(this);
					Changed();
				}
				catch {
					modifier = previousModifier;
					throw;
				}
			}
		}

		public virtual string InitialValue
		{
			get
			{
				return initialValue;
			}
			set
			{
				if (initialValue != value)
				{
					initialValue = value;
					Changed();
				}
			}
		}

		public bool HasInitialValue
		{
			get
			{
				return !string.IsNullOrEmpty(InitialValue);
			}
		}

		public virtual void ClearModifiers()
		{
			if (modifier != FieldModifier.None) {
				modifier = FieldModifier.None;
				Changed();
			}
		}

		public sealed override string GetUmlDescription(bool getType, bool getParameters,
			bool getParameterNames, bool getInitValue)
		{
			StringBuilder builder = new StringBuilder(50);

			builder.Append(Name);
			if (getType)
				builder.AppendFormat(": {0}", Type);
			if (getInitValue && HasInitialValue)
				builder.AppendFormat(" = {0}", InitialValue);

			return builder.ToString();
		}

		protected override void CopyFrom(Member member)
		{
			base.CopyFrom(member);

			Field field = (Field) member;
			modifier = field.modifier;
			initialValue = field.initialValue;
            setter = field.setter;
            getter = field.getter;
            generateDbColumn = field.generateDbColumn;
            DbSchema.Initialing = true;
            DbSchema.NotNull = field.DbSchema.NotNull;
            DbSchema.AutoIncrement = field.DbSchema.AutoIncrement;
            DbSchema.DbType = field.DbSchema.DbType;
            DbSchema.DefaultValue = field.DbSchema.DefaultValue;
            DbSchema.Index = field.DbSchema.Index;
            DbSchema.IsPrimaryKey = field.DbSchema.IsPrimaryKey;
            DbSchema.Length = field.DbSchema.Length;
            DbSchema.Name = field.DbSchema.Name;
            DbSchema.Initialing = false;
		}

		protected internal abstract Field Clone(CompositeType newParent);
	}
}
