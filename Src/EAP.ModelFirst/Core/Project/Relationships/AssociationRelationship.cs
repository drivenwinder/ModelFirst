using System;
using System.Text;
using System.Xml;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Core.Project.Relationships
{
	public sealed class AssociationRelationship : TypeRelationship
	{
		AssociationType associationType = AssociationType.Association;
		Direction direction = Direction.Unidirectional;
		string startRole, endRole;
		string startMultiplicity = "1";
        string endMultiplicity = "1";

		public event EventHandler Reversed;

		/// <exception cref="ArgumentNullException">
		/// <paramref name="first"/> or <paramref name="second"/> is null.
		/// </exception>
		internal AssociationRelationship(TypeBase first, TypeBase second)
			: base(first, second)
        {
			Attach();
		}

		/// <exception cref="ArgumentNullException">
		/// <paramref name="first"/> is null.-or-
		/// <paramref name="second"/> is null.
		/// </exception>
		internal AssociationRelationship(TypeBase first, TypeBase second, AssociationType type)
			: base(first, second)
		{
			this.associationType = type;
			Attach();
		}

        protected override void OnAttaching(EventArgs e)
        {
            base.OnAttaching(e);

            ((TypeBase)First).AddAssociationRelationship(this);
        }

        protected override void OnDetaching(EventArgs e)
        {
            base.OnDetaching(e);

            ((TypeBase)First).RemoveAssociationRelationship(this);
        }

		public override RelationshipType RelationshipType
		{
			get { return RelationshipType.Association; }
		}

		public override bool SupportsLabel
		{
			get { return true; }
		}

		public Direction Direction
		{
			get
			{
				return direction;
			}
			set
			{
				if (direction != value)
				{
					direction = value;
					Changed();
				}
			}
		}

		public AssociationType AssociationType
		{
			get
			{
				return associationType;
			}
			set
			{
				if (associationType != value)
				{
					associationType = value;
					Changed();
				}
			}
		}

        [System.ComponentModel.Browsable(false)]
		public bool IsAggregation
		{
			get
			{
				return (associationType == AssociationType.Aggregation);
			}
		}

        [System.ComponentModel.Browsable(false)]
		public bool IsComposition
		{
			get
			{
				return (associationType == AssociationType.Composition);
			}
		}

		public string StartRole
		{
			get
			{
				return startRole;
			}
			set
			{
				if (value == "")
					value = null;

				if (startRole != value)
				{
					startRole = value;
					Changed();
				}
			}
		}

		public string EndRole
		{
			get
			{
				return endRole;
			}
			set
			{
				if (value == "")
					value = null;

				if (endRole != value)
				{
					endRole = value;
					Changed();
				}
			}
		}

		public string StartMultiplicity
		{
			get
			{
				return startMultiplicity;
			}
			set
			{
				if (startMultiplicity != value)
				{
					startMultiplicity = value;
					Changed();
				}
			}
		}

		public string EndMultiplicity
		{
			get
			{
				return endMultiplicity;
			}
			set
			{
				if (endMultiplicity != value)
				{
					endMultiplicity = value;
					Changed();
				}
			}
		}

		public void Reverse()
		{
            ((TypeBase)First).RemoveAssociationRelationship(this);

            IEntity first = First;
			First = Second;
            Second = first;

            string temp = StartRole;
            StartRole = EndRole;
            EndRole = temp;

            temp = StartMultiplicity;
            StartMultiplicity = EndMultiplicity;
            EndMultiplicity = temp;

            ((TypeBase)First).AddAssociationRelationship(this);            

			OnReversed(EventArgs.Empty);
			Changed();
		}

		protected override void CopyFrom(Relationship relationship)
		{
			base.CopyFrom(relationship);

			AssociationRelationship association = (AssociationRelationship) relationship;
			associationType = association.associationType;
			direction = association.direction;
			startRole = association.startRole;
			endRole = association.endRole;
			startMultiplicity = association.startMultiplicity;
			endMultiplicity = association.endMultiplicity;
		}

		public AssociationRelationship Clone(TypeBase first, TypeBase second)
		{
			AssociationRelationship association = new AssociationRelationship(first, second);
			association.CopyFrom(this);
			return association;
		}

		/// <exception cref="ArgumentNullException">
		/// <paramref name="node"/> is null.
		/// </exception>
		protected internal override void Serialize(XmlElement node)
		{
			base.Serialize(node);

            node.CreateElement("Direction", Direction.ToString());
            node.CreateElement("AssociationType", AssociationType.ToString());

			if (StartRole.IsNotEmpty())
                node.CreateElement("StartRole", StartRole);

            if (EndRole.IsNotEmpty())
                node.CreateElement("EndRole", EndRole);

            if (StartMultiplicity.IsNotEmpty())
                node.CreateElement("StartMultiplicity", StartMultiplicity);

            if (EndMultiplicity.IsNotEmpty())
                node.CreateElement("EndMultiplicity", EndMultiplicity);
		}

		/// <exception cref="ArgumentNullException">
		/// <paramref name="node"/> is null.
		/// </exception>
		protected internal override void Deserialize(XmlElement node)
		{
			base.Deserialize(node);

			RaiseChangedEvent = false;

            direction = node["Direction"].GetValue(Direction.Bidirectional);
            associationType = node["AssociationType"].GetValue(AssociationType.Association);
            startRole = node["StartRole"].GetValue("");
            endRole = node["EndRole"].GetValue("");
            startMultiplicity = node["StartMultiplicity"].GetValue("1");
            endMultiplicity = node["EndMultiplicity"].GetValue("1");

			RaiseChangedEvent = true;
		}

		private void OnReversed(EventArgs e)
		{
			if (Reversed != null)
				Reversed(this, e);
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder(50);

			if (IsAggregation)
				builder.Append(Strings.Aggregation);
			else if (IsComposition)
				builder.Append(Strings.Composition);
			else
				builder.Append(Strings.Association);
			builder.Append(": ");
			builder.Append(First.Name);

			switch (Direction)
			{
				case Direction.Bidirectional:
					if (AssociationType == AssociationType.Association)
						builder.Append(" --- ");
					else
						builder.Append(" <>-- ");
					break;
				case Direction.Unidirectional:
					if (AssociationType == AssociationType.Association)
						builder.Append(" --> ");
					else
						builder.Append(" <>-> ");
					break;
				default:
					builder.Append(", ");
					break;
			}
			builder.Append(Second.Name);

			return builder.ToString();
		}
	}
}