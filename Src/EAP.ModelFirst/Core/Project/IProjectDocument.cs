using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAP.ModelFirst.Core.Project
{
    public interface IProjectDocument : IProjectItem, IDocumentItem
    {
        [System.ComponentModel.Browsable(false)]
        Guid Id { get; }
    }
}
