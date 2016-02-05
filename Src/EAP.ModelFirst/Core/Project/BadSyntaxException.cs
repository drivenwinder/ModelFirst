using System;
using System.Runtime.Serialization;

namespace EAP.ModelFirst.Core.Project
{
	public class BadSyntaxException : InvalidException
	{
		public BadSyntaxException()
		{
		}

		public BadSyntaxException(string message) : base(message)
		{
		}

		public BadSyntaxException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected BadSyntaxException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
