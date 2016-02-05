using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Xml;
using EAP.ModelFirst.Controls.Editors;
using EAP.ModelFirst.Core.Project.Members;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Core.Project.Entities
{
	public abstract class CompositeType : TypeBase
	{
		List<Field> fields = new List<Field>();
		List<Operation> operations = new List<Operation>();
		List<TypeBase> nestedChildren = new List<TypeBase>();
        MemberInfo memberInfo;

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		protected CompositeType(string name)
			: base(name)
		{
            memberInfo = new MemberInfo(this);
		}

        [System.ComponentModel.Browsable(false)]
		public abstract bool SupportsFields { get; }

        [System.ComponentModel.Browsable(false)]
		public bool SupportsOperations
		{
			get
			{
				return (
					SupportsMethods || SupportsConstuctors || SupportsDestructors ||
					SupportsProperties || SupportsEvents
				);
			}
		}

        [System.ComponentModel.Browsable(false)]
		public abstract bool SupportsMethods { get; }

        [System.ComponentModel.Browsable(false)]
		public abstract bool SupportsConstuctors { get; }

        [System.ComponentModel.Browsable(false)]
		public abstract bool SupportsDestructors { get; }

        [System.ComponentModel.Browsable(false)]
		public abstract bool SupportsProperties { get; }

        [System.ComponentModel.Browsable(false)]
		public abstract bool SupportsEvents { get; }

        [System.ComponentModel.Browsable(false)]
		public abstract bool SupportsNesting { get; }

        [System.ComponentModel.Browsable(false)]
		public abstract bool IsAllowedParent { get; }

        [System.ComponentModel.Browsable(false)]
		public abstract bool IsAllowedChild { get; }

        [System.ComponentModel.Browsable(false)]
		public abstract bool HasExplicitBase { get; }

        [System.ComponentModel.Browsable(false)]
		public abstract AccessModifier DefaultMemberAccess { get; }

		protected List<Field> FieldList
		{
			get { return fields; }
		}

        [System.ComponentModel.Browsable(false)]
		public IEnumerable<Field> Fields
		{
			get { return fields; }
		}

        [Editor(typeof(TypeMemberEditor), typeof(UITypeEditor))]
        public MemberInfo Members
        {
            get { return memberInfo; }
        }

        [System.ComponentModel.Browsable(false)]
		public int FieldCount
		{
			get { return fields.Count; }
		}

		protected List<Operation> OperationList
		{
			get { return operations; }
		}

        [System.ComponentModel.Browsable(false)]
		public IEnumerable<Operation> Operations
		{
			get { return operations; }
		}

        [System.ComponentModel.Browsable(false)]
		public int OperationCount
		{
			get { return operations.Count; }
		}

        [System.ComponentModel.Browsable(false)]
		public int MemberCount
		{
			get
			{
				return FieldCount + OperationCount;
			}
		}

        [System.ComponentModel.Browsable(false)]
		public IEnumerable<TypeBase> NestedChildren
		{
			get { return nestedChildren; }
		}

		/// <exception cref="InvalidOperationException">
		/// The type does not support fields.
		/// </exception>
		public abstract Field AddField();

		/// <exception cref="InvalidOperationException">
		/// The type does not support methods.
		/// </exception>
		public abstract Method AddMethod();

		/// <exception cref="InvalidOperationException">
		/// The type does not support constructors.
		/// </exception>
		public abstract Constructor AddConstructor();

		/// <exception cref="InvalidOperationException">
		/// The type does not support destructors.
		/// </exception>
		public abstract Destructor AddDestructor();

		/// <exception cref="InvalidOperationException">
		/// The type does not support properties.
		/// </exception>
		public abstract Property AddProperty();

		/// <exception cref="InvalidOperationException">
		/// The type does not support events.
		/// </exception>
		public abstract Event AddEvent();

		/// <exception cref="InvalidOperationException">
		/// The type does not support the given kind of member.
		/// </exception>
		public void InsertMember(MemberType type, int index)
		{
			if (type == MemberType.Field)
			{
				if (index > FieldCount)
					index = FieldCount;
			}
			else
			{
				index -= FieldCount;
				if (index > OperationCount)
					index = OperationCount;
			}

			if (index < 0)
				index = 0;

			switch (type)
			{
				case MemberType.Field:
					Field field = AddField();
					fields.RemoveAt(FieldCount - 1);
					fields.Insert(index, field);
					break;

				case MemberType.Method:
					Method method = AddMethod();
					operations.RemoveAt(OperationCount - 1);
					operations.Insert(index, method);
					break;

				case MemberType.Constructor:
					Constructor constructor = AddConstructor();
					operations.RemoveAt(OperationCount - 1);
					operations.Insert(index, constructor);
					break;

				case MemberType.Destructor:
					Destructor destructor = AddDestructor();
					operations.RemoveAt(OperationCount - 1);
					operations.Insert(index, destructor);
					break;

				case MemberType.Property:
					Property property = AddProperty();
					operations.RemoveAt(OperationCount - 1);
					operations.Insert(index, property);
					break;

				case MemberType.Event:
					Event _event = AddEvent();
					operations.RemoveAt(OperationCount - 1);
					operations.Insert(index, _event);
					break;
			}
		}

		protected void AddField(Field field)
		{
			if (field != null && !FieldList.Contains(field))
			{
				fields.Add(field);
				field.Modified += delegate { Changed(); };
				Changed();
			}
		}

		protected void AddOperation(Operation operation)
		{
			if (operation != null && !OperationList.Contains(operation))
			{
				operations.Add(operation);
				operation.Modified += delegate { Changed(); };
				Changed();
			}
		}

		public Field GetField(int index)
		{
			if (index >= 0 && index < fields.Count)
				return fields[index];
			else
				return null;
		}

		public Operation GetOperation(int index)
		{
			if (index >= 0 && index < operations.Count)
				return operations[index];
			else
				return null;
		}

		public void RemoveMember(Member member)
		{
			if (member is Field)
			{
				if (FieldList.Remove((Field) member))
					Changed();
			}
			else if (member is Operation)
			{
				if (OperationList.Remove((Operation) member))
					Changed();
			}
		}

		internal void AddNestedChild(TypeBase type)
		{
			if (type != null && !nestedChildren.Contains(type))
			{
				nestedChildren.Add(type);
				Changed();
			}
		}

		internal void RemoveNestedChild(TypeBase type)
		{
			if (type != null && nestedChildren.Remove(type))
				Changed();
		}

		public Operation GetDefinedOperation(Operation operation)
		{
			if (operation == null)
				return null;

			for (int i = 0; i < OperationList.Count; i++)
			{
				if (OperationList[i].HasSameSignatureAs(operation))
					return OperationList[i];
			}

			return null;
		}

		public sealed override bool MoveUpItem(object item)
		{
			if (item is Field)
			{
				if (MoveUp(FieldList, item))
				{
					Changed();
					return true;
				}
			}
			else if (item is Operation)
			{
				if (MoveUp(OperationList, item))
				{
					Changed();
					return true;
				}
			}
			return false;
		}

		public sealed override bool MoveDownItem(object item)
		{
			if (item is Field)
			{
				if (MoveDown(FieldList, item))
				{
					Changed();
					return true;
				}
			}
			else if (item is Operation)
			{
				if (MoveDown(OperationList, item))
				{
					Changed();
					return true;
				}
			}
			return false;
		}

		public void SortMembers(SortingMode sortingMode)
		{
			switch (sortingMode)
			{
				case SortingMode.ByName:
					FieldList.Sort(MemberComparisonByName);
					OperationList.Sort(MemberComparisonByName);
					Changed();
					break;

				case SortingMode.ByAccess:
					FieldList.Sort(MemberComparisonByAccess);
					OperationList.Sort(MemberComparisonByAccess);
					Changed();
					break;

				case SortingMode.ByKind:
					FieldList.Sort(MemberComparisonByKind);
					OperationList.Sort(MemberComparisonByKind);
					Changed();
					break;
			}
		}

		private static int MemberComparisonByName(Member member1, Member member2)
		{
			return member1.Name.CompareTo(member2.Name);
		}

		private static int MemberComparisonByAccess(Member member1, Member member2)
		{
			int access1 = (int) member1.Access;
			int access2 = (int) member2.Access;

			if (access1 == access2)
				return MemberComparisonByKind(member1, member2);
			else
				return access1 - access2;
		}

		private static int MemberComparisonByKind(Member member1, Member member2)
		{
			int ret = GetMemberOrdinal(member1) - GetMemberOrdinal(member2);

			if (ret == 0)
				return MemberComparisonByName(member1, member2);

			return ret;
		}

		private static int GetMemberOrdinal(Member member)
		{
			if (member is Field)
			{
				if (((Field) member).IsConstant)
					return 0;
				else
					return 1;
			}
			if (member is Property)
			{
				Property property = (Property) member;

				if (property.IsReadonly)
					return 2;
				else if (property.IsWriteonly)
					return 3;
				else
					return 4;
			}
			if (member is Constructor)
				return 5;
			if (member is Method && ((Method) member).IsOperator)
				return 6;
			if (member is Destructor)
				return 8; // (!)
			if (member is Method)
				return 7;
			if (member is Event)
				return 9;

			// Unreachable case
			return 10;
		}

		protected override void CopyFrom(TypeBase type)
		{
			base.CopyFrom(type);

			CompositeType compositeType = (CompositeType) type;
			fields.Clear();
			fields.Capacity = compositeType.fields.Capacity;
			operations.Clear();
			operations.Capacity = compositeType.operations.Capacity;

			foreach (Field field in compositeType.fields)
			{
				AddField(field.Clone(this));
			}
			foreach (Operation operation in compositeType.operations)
			{
				AddOperation(operation.Clone(this));
			}
		}

		/// <exception cref="ArgumentNullException">
		/// <paramref name="node"/> is null.
		/// </exception>
        protected internal override void Serialize(XmlElement node)
        {
            base.Serialize(node);

            foreach (Field field in FieldList)
            {
                XmlElement child = node.OwnerDocument.CreateElement("Member");
                child.SetAttribute("type", field.MemberType.ToString());
                if(field.Getter)
                    child.SetAttribute("getter", "true");
                if (field.Setter)
                    child.SetAttribute("setter", "true");
                if (field.Comments.IsNotEmpty())
                    child.SetAttribute("Comments", field.Comments);
                SaveSchemaInfo(child, field);
                child.InnerText = field.ToString();
                node.AppendChild(child);
            }
            foreach (Operation operation in OperationList)
            {
                XmlElement child = node.OwnerDocument.CreateElement("Member");
                child.SetAttribute("type", operation.MemberType.ToString());
                if (operation.Comments.IsNotEmpty())
                    child.SetAttribute("Comments", operation.Comments);
                child.InnerText = operation.ToString();
                node.AppendChild(child);
            }
        }

		/// <exception cref="BadSyntaxException">
		/// An error occured while deserializing.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// The XML document is corrupt.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="node"/> is null.
		/// </exception>
		protected internal override void Deserialize(XmlElement node)
		{
			RaiseChangedEvent = false;
            fields.Clear();
            operations.Clear();
			foreach (XmlElement childNode in node.SelectNodes("Member"))
			{
                string type = childNode.GetAttribute("type");

				if (type == "Field")
				{
					Field field = AddField();
                    field.InitFromString(childNode.InnerText);
                    field.Getter = childNode.GetAttributeValue("getter", false);
                    field.Setter = childNode.GetAttributeValue("setter", false);
                    field.Comments = childNode.GetAttributeValue("Comments", "");
                    LoadSchemaInfo(childNode, field);
				}
				else
				{
					Operation operation = GetOperation(type);
                    operation.InitFromString(childNode.InnerText);
                    operation.Comments = childNode.GetAttributeValue("Comments", "");
				}
			}

			base.Deserialize(node);
			RaiseChangedEvent = true;
		}

        void SaveSchemaInfo(XmlElement child, Field field)
        {
            if (field.GenerateDbColumn)
            {
                child.SetAttribute("GenColumn", field.GenerateDbColumn.ToString());
                child.SetAttribute("Name", field.DbSchema.Name);
                child.SetAttribute("DbType", field.DbSchema.DbType.ToString());
                if (field.DbSchema.NotNull)
                    child.SetAttribute("NotNull", "true");
                if (field.DbSchema.AutoIncrement)
                    child.SetAttribute("AutoIncrement", "true");
                if (field.DbSchema.DefaultValue.IsNotEmpty())
                    child.SetAttribute("DefaultValue", field.DbSchema.DefaultValue);
                if (field.DbSchema.Index)
                    child.SetAttribute("Index", "true");
                if (field.DbSchema.IsPrimaryKey)
                    child.SetAttribute("IsPrimaryKey", "true");
                if (field.DbSchema.Length.IsNotEmpty())
                    child.SetAttribute("Length", field.DbSchema.Length);
            }
        }

        void LoadSchemaInfo(XmlElement child, Field field)
        {
            field.GenerateDbColumn = child.GetAttributeValue("GenColumn", false);
            if (field.GenerateDbColumn)
            {
                field.DbSchema.Initialing = true;
                field.DbSchema.Name = child.GetAttributeValue("Name", "");
                field.DbSchema.NotNull = child.GetAttributeValue("NotNull", false);
                field.DbSchema.AutoIncrement = child.GetAttributeValue("AutoIncrement", false);
                field.DbSchema.DbType = child.GetAttributeValue<System.Data.DbType?>("DbType", null);
                field.DbSchema.DefaultValue = child.GetAttributeValue("DefaultValue", "");
                field.DbSchema.Index = child.GetAttributeValue("Index", false);
                field.DbSchema.IsPrimaryKey = child.GetAttributeValue("IsPrimaryKey", false);
                field.DbSchema.Length = child.GetAttributeValue("Length", "");
                field.DbSchema.Initialing = false;
            }
        }

		private Operation GetOperation(string type)
		{
			switch (type)
			{
				case "Constructor":
					return AddConstructor();

				case "Destructor":
					return AddDestructor();

				case "Method":
					return AddMethod();

				case "Property":
					return AddProperty();

				case "Event":
					return AddEvent();

                default:
                    throw new InvalidDataException(Strings.ErrorCorruptSaveFormat);
			}
		}
	}
}