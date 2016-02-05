using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAP.ModelFirst.Core.Project.Members;

namespace EAP.ModelFirst.Core.Project.Members
{
    public interface IDbSchema
    {
        DbSchema DbSchema { get; }
    }
}
