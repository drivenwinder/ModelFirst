using System;
using System.Xml;

namespace EAP.ModelFirst.Core
{
	public interface ISerializableElement
	{
		event SerializeEventHandler Serializing;
		event SerializeEventHandler Deserializing;

		/// <exception cref="ArgumentNullException">
		/// <paramref name="node"/> is null.
		/// </exception>
		void Serialize(XmlElement node);

		/// <exception cref="BadSyntaxException">
		/// An error occured while deserializing.
        /// �ϧǦC�Ʈɵo�Ϳ��~
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// The XML document is corrupt.
        /// XML���ɤ����T
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="node"/> is null.
		/// </exception>
		void Deserialize(XmlElement node);
	}
}
