using System.Collections.Generic;
using System.Linq;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Template;

namespace EAP.ModelFirst.CodeGenerator
{
    public class TypeCollectionModel : RazorModel
    {
        public TypeCollectionModel(IEnumerable<TypeBase> types, IRazorTemplate template)
            : base(types.First().ProjectInfo.Name, template)
        {
            Types = types;
        }

        public IEnumerable<TypeBase> Types { get; private set; }
    }
}
