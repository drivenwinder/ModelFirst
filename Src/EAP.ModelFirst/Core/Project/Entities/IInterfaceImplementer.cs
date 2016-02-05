using System;
using System.Collections.Generic;
using EAP.ModelFirst.Core.Project.Members;
using EAP.ModelFirst.Core.Project.Relationships;

namespace EAP.ModelFirst.Core.Project.Entities
{
    public interface IInterfaceImplementer
    {
        IEnumerable<InterfaceType> Interfaces { get; }

        string Name { get; }

        Language Language { get; }

        bool ImplementsInterface { get; }

        Operation GetDefinedOperation(Operation operation);

        /// <exception cref="ArgumentException">
        /// The language of <paramref name="operation"/> does not equal.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="operation"/> is null.
        /// </exception>
        Operation Implement(Operation operation, bool isExplicit);

        /// <exception cref="RelationshipException">
        /// The language of <paramref name="interfaceType"/> does not equal.-or-
        /// <paramref name="interfaceType"/> is earlier implemented interface.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="interfaceType"/> is null.
        /// </exception>
        void AddRealization(RealizationRelationship realization);

        void RemoveRealization(RealizationRelationship realization);

        List<RealizationRelationship> RealizationRelationships { get; }
    }
}
