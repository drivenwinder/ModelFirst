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
        /// 反序列化時發生錯誤
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// The XML document is corrupt.
        /// XML文檔不正確
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="node"/> is null.
		/// </exception>
		void Deserialize(XmlElement node);
	}
}
