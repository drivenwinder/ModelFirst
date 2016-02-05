using System;
using System.Collections.Generic;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Members;
using EAP.ModelFirst.Core.Project.Relationships;
using System.Xml;

namespace EAP.ModelFirst.Core.Project.Entities
{
	public abstract class SingleInharitanceType : CompositeType, IInterfaceImplementer
	{
		string tableName;
		List<InterfaceType> interfaceList = new List<InterfaceType>();
		GeneralizationRelationship generalization;
		List<RealizationRelationship> realizations = new List<RealizationRelationship>();

		/// <exception cref="BadSyntaxException">
		/// The <paramref name="name"/> does not fit to the syntax.
		/// </exception>
		protected SingleInharitanceType(string name) : base(name)
		{
		}

		/// <exception cref="RelationshipException">
		/// The base and derived types do not equal.-or-
		/// The <paramref name="value"/> is descendant of the type.
		/// </exception>
		public abstract SingleInharitanceType Base
		{
			get;
			set;
		}

        [System.ComponentModel.Browsable(false)]
		public virtual GeneralizationRelationship GeneralizationRelationship
		{
			get { return generalization; }
			set
			{
				if (value == null)
					Base = null;
				else
					Base = (SingleInharitanceType)value.Second;
				generalization = value;
			}
		}

        [System.ComponentModel.Browsable(false)]
		public virtual List<RealizationRelationship> RealizationRelationships
		{
			get { return realizations; }
		}

        [System.ComponentModel.Browsable(false)]
		public abstract IEnumerable<Operation> OverridableOperations
		{
			get;
		}

        [System.ComponentModel.Browsable(false)]
		protected List<InterfaceType> InterfaceList
		{
			get { return interfaceList; }
		}

        [System.ComponentModel.Browsable(false)]
		public IEnumerable<InterfaceType> Interfaces
		{
			get { return interfaceList; }
		}

        [System.ComponentModel.Browsable(false)]
		public bool ImplementsInterface
		{
			get
			{
				return (interfaceList.Count > 0);
			}
		}

		/// <exception cref="RelationshipException">
		/// The language of <paramref name="interfaceType"/> does not equal.-or-
		/// <paramref name="interfaceType"/> is earlier implemented interface.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="interfaceType"/> is null.
		/// </exception>
		public virtual void AddRealization(RealizationRelationship real)
		{
			var interfaceType = real.BaseType;

			if (interfaceType == null)
				throw new ArgumentNullException("interfaceType");

			if (InterfaceList.Contains(interfaceType))
				throw new RelationshipException(Strings.ErrorCannotAddSameInterface);

			//foreach (InterfaceType implementedInterface in InterfaceList)
			//{
			//    if (interfaceType == implementedInterface)
			//        throw new RelationshipException(Strings.ErrorCannotAddSameInterface);
			//}
			
			InterfaceList.Add(interfaceType);
			interfaceType.Realisztions.Add(this);
			realizations.Add(real);
			Changed();
		}

		public void RemoveRealization(RealizationRelationship real)
		{
			var interfaceType = real.BaseType;

			interfaceType.Realisztions.Remove(this);
			realizations.Remove(real);
			if (InterfaceList.Remove(interfaceType))
				Changed();
		}

		/// <exception cref="ArgumentException">
		/// The language of <paramref name="operation"/> does not equal.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="operation"/> is null.
		/// </exception>
		public Operation Implement(Operation operation, bool explicitly)
		{
			if (operation == null)
				throw new ArgumentNullException("operation");

			if (operation.Language != Language)
				throw new ArgumentException(Strings.ErrorLanguagesDoNotEqual);

			if (!(operation.Parent is InterfaceType)) {
				throw new ArgumentException("The operation is not a member of an interface.");
			}

			if (explicitly && !operation.Language.SupportsExplicitImplementation) {
				throw new ArgumentException(
					Strings.ErrorExplicitImplementation, "explicitly");
			}

			Operation newOperation = Language.Implement(operation, this, explicitly);
			newOperation.Parent = this;

			AddOperation(newOperation);
			return newOperation;
		}

		/// <exception cref="ArgumentException">
		/// <paramref name="operation"/> cannot be overridden.-or-
		/// The language of <paramref name="operation"/> does not equal.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="operation"/> is null.
		/// </exception>
		public Operation Override(Operation operation)
		{
			if (operation == null)
				throw new ArgumentNullException("operation");

			if (operation.Language != Language)
				throw new ArgumentException(Strings.ErrorLanguagesDoNotEqual);

			Operation newOperation = Language.Override(operation, this);

			AddOperation(newOperation);
			return newOperation;
		}
		
		public string TableName
		{			
			get { return tableName; }
			set
			{
                if (value.IsNullOrEmpty())
                    throw new BadSyntaxException(Strings.ErrorNameIsRequired);

				if (tableName != value)
				{
					tableName = value;
					Changed();
				}
			}
		}

		protected override void CopyFrom(TypeBase type)
		{
			base.CopyFrom(type);

			tableName = ((SingleInharitanceType)type).TableName;
		}
		
        protected internal override void Serialize(XmlElement node)
        {
            base.Serialize(node);
            
            node.CreateElement("tableName", tableName);
        }
        
		protected internal override void Deserialize(XmlElement node)
		{
			RaiseChangedEvent = false;
			base.Deserialize(node);
			tableName = node["tableName"].GetValue(Name);
			RaiseChangedEvent = true;
		}
		
		public override string Name {
			get { return base.Name; }
			set { 
				var related = tableName == base.Name;
				base.Name = value;
				if(related)
					tableName = base.Name;
			}
		}
	}
}
