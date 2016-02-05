using System;
using System.Runtime.Serialization;
using EAP.ModelFirst.Properties;

namespace EAP.ModelFirst.Core.Project.Relationships
{
	public class RelationshipException : Exception
	{
		public RelationshipException() : base(Strings.ErrorCannotCreateRelationship)
		{
		}

		public RelationshipException(string message) : base(message)
		{
		}

		public RelationshipException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected RelationshipException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
