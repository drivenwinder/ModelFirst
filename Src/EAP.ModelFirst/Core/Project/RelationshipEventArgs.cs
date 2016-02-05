using System;
using EAP.ModelFirst.Core.Project.Relationships;

namespace EAP.ModelFirst.Core.Project
{
	public delegate void RelationshipEventHandler(object sender, RelationshipEventArgs e);

	public class RelationshipEventArgs : EventArgs
	{
		Relationship relationship;

		public RelationshipEventArgs(Relationship relationship)
		{
			this.relationship = relationship;
		}

		public Relationship Relationship
		{
			get { return relationship; }
		}
	}
}
