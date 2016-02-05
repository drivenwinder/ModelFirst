
namespace EAP.ModelFirst.Core.Template
{
    public interface IRazorTemplate
    {
        string Output { get; }

        string Template { get; }

        GenerateMode Mode { get; }
    }
}
