using EAP.ModelFirst.Core.Template;

namespace EAP.ModelFirst.CodeGenerator
{
    public class RazorModel
    {
        public RazorModel(string project, IRazorTemplate template)
        {
            ProjectName = project;
            Template = template;
        }

        public string ProjectName { get; protected set; }

        public IRazorTemplate Template { get; protected set; }
    }
}
