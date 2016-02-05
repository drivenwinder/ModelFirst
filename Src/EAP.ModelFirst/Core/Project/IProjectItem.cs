using System;
using System.Xml;

namespace EAP.ModelFirst.Core.Project
{
	public interface IProjectItem : IModifiable, INamedObject
	{
		Project ProjectInfo { get; set; }

        PackageBase Package { get; set; }

        ProjectItemType ItemType { get; }

		bool IsUntitled { get; }

		/// <exception cref="ArgumentNullException">
		/// <paramref name="node"/> is null.
		/// </exception>+
		void Serialize(XmlElement node);

		/// <exception cref="InvalidDataException">
		/// The serialized format is corrupt and could not be loaded.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="node"/> is null.
		/// </exception>
        void Deserialize(DeserializeContext context, XmlElement node);
	}
}
