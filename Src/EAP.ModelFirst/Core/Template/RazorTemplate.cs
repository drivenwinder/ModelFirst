using System;
using System.IO;
using System.Text.RegularExpressions;

namespace EAP.ModelFirst.Core.Template
{
    public class RazorTemplate : IRazorTemplate
    {
        const string outputPattern = @"var\s+output\s*=\s*(?<output>[^\s;](.*[^\s;])?)";
        const string generateModeFilePattern = @"var\s+generateMode\s*=\s*(?<generateMode>[^\s;](.*[^\s;])?)";
        const string includePattern = @"#Include\(.*?\;";
        const string includeValuePattern = @"#Include\(\s*""<generateMode>[^\s""\);](.*[^\s;])?)";

        public RazorTemplate(TemplateFile file)
        {
            var text = file.OpenText();
            Output = GetOutput(text);
            Mode = GetGenerateMode(text);
            Template = GetTemplate(text, file.DirectoryName);
        }

        public string Output { get; private set; }

        public string Template { get; private set; }

        public GenerateMode Mode { get; private set; }

        string GetOutput(string template)
        {
            var regex = new Regex(outputPattern, RegexOptions.ExplicitCapture);
            Match match = regex.Match(template);
            try
            {
                if (match.Success)
                {
                    Group group = match.Groups["output"];
                    return group.Value;
                }
            }
            catch (Exception)
            {
            }
            return "";
        }

        GenerateMode GetGenerateMode(string template)
        {
            var regex = new Regex(generateModeFilePattern, RegexOptions.ExplicitCapture);
            Match match = regex.Match(template);
            try
            {
                if (match.Success)
                {
                    Group group = match.Groups["generateMode"];
                    return group.Value.Trim('"').ConvertTo(GenerateMode.Single);
                }
            }
            catch (Exception)
            {
            }
            return GenerateMode.Single;
        }

        string GetTemplate(string text, string dir)
        {
            var regex = new Regex(includePattern, RegexOptions.ExplicitCapture);
            foreach (Match m in regex.Matches(text))
            {
                var start = m.Value.IndexOf('"') + 1;
                var end = m.Value.LastIndexOf('"');
                var value = m.Value.Substring(start, end - start);
                var file = GetTemplateFile(value, dir);
                var template = file.GetTemplate();
                text = text.Replace(m.Value, template.Template);
            }
            return text;
        }

        TemplateFile GetTemplateFile(string include, string dir)
        {
            var file = include;
            if (!Path.IsPathRooted(file))
            {
                Directory.SetCurrentDirectory(dir);
                file = Path.GetFullPath(include);
            }
            return new TemplateFile(file);
        }
    }
}
