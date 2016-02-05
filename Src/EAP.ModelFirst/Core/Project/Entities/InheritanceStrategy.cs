using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAP.ModelFirst.Core.Project.Entities
{
    public enum InheritanceStrategy
    {
        /// <summary>
        /// 单表
        /// </summary>
        Subclass,
        /// <summary>
        /// 类表
        /// </summary>
        Joined,
        /// <summary>
        /// 具体表
        /// </summary>
        Union
    }
}
