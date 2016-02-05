using System;
using System.Xml;
using EAP.ModelFirst.Core.Project.Entities;

namespace EAP.ModelFirst.Core.Project
{
    public interface IEntity : ISerializableElement, IModifiable
    {
        [System.ComponentModel.Browsable(false)]
        Guid Id { get; }

        string Name { get; }

        EntityType EntityType { get; }
    }
}
