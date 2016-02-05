using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Template;
using System.Collections.Generic;

namespace EAP.ModelFirst.CodeGenerator
{
    public class TypeModel : RazorModel
    {
        public TypeModel(TypeBase type, IEnumerable<TypeBase> types, IRazorTemplate template)
            : base(type.ProjectInfo.Name, template)
        {
            Type = type;
            Types = types;
        }

        public TypeBase Type { get; set; }

        public IEnumerable<TypeBase> Types { get; private set; }

        public string GetNested(TypeBase type)
        {
            var model = new TypeModel(type, Types, Template);
            return RazorHelper.Parse(Template.Template, model);
        }
    }
}
