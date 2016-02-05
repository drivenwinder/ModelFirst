using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EAP.ModelFirst.Controls;
using EAP.ModelFirst.Controls.Explorers;
using EAP.ModelFirst.Core.Project.Entities;
using EAP.ModelFirst.Core.Template;
using EAP.ModelFirst.Properties;
using EAP.ModelFirst.Utils;

namespace EAP.ModelFirst.CodeGenerator
{
    public static class RazorHelper
    {
        static bool cancel = false;

        static RazorEngine.Templating.TemplateService TemplateService;
        static RazorEngine.Compilation.ICompilerService CompilerService;

        static RazorHelper()
        {
            CompilerService = new RazorEngine.Compilation.DefaultCompilerServiceFactory().CreateCompilerService();
        }

        static void ResetTemplateService()
        {
            TemplateService = new RazorEngine.Templating.TemplateService(CompilerService);
        }

        public static void Cancel()
        {
            cancel = true;
        }

        public static void Compile(string path, string template, IDockForm form)
        {
            Cursor _oldCursor = form.Cursor;
            form.Cursor = Cursors.WaitCursor;
            form.StatusText = Strings.Compiling;
            Application.DoEvents();

            ErrorList.Clear();
            try
            {
                ResetTemplateService();
                TemplateService.CompileWithAnonymous(template, Guid.NewGuid().ToString());
            }
            catch (Exception exc)
            {
                ErrorList.SetError(exc);
            }

            form.Cursor = _oldCursor;
            if (ErrorList.HasError)
            {
                form.StatusText = Strings.ErrorTemplateCompile;
                Client.ShowInfo(Strings.ErrorTemplateCompile);
                ErrorList.Show(form);
            }
            else
            {
                form.StatusText = Strings.Compiled;
                Client.ShowInfo(Strings.Compiled);
            }
        }

        public static void GenerateCode(IEnumerable<TypeBase> types, IEnumerable<TemplateFile> templates, string output, IDockForm form)
        {
            form.StatusText = Strings.Generating;
            form.UpdateProgress(0);
            ErrorList.Clear();
            GeneratedFileList.Clear();
            Application.DoEvents();

            try
            {
                ResetTemplateService();

                if (output.IsNullOrEmpty())
                    output = Application.StartupPath;
                else if (output.StartsWith("\\") || output.StartsWith("/"))
                    output = Application.ExecutablePath + output;

                DoGenerateCode(types, templates, output, form);
                if (cancel)
                {
                    cancel = false;
                    form.StatusText = Strings.Canceled;
                    if (ErrorList.HasError)
                        ErrorList.Show(form);
                }
                else
                {
                    if (ErrorList.HasError)
                    {
                        form.StatusText = Strings.ErrorGenerateCode;
                        Client.ShowInfo(Strings.ErrorGenerateCode);
                        ErrorList.Show(form);
                    }
                    else
                    {
                        form.StatusText = Strings.CodeGenerated;
                        Client.ShowInfo(Strings.CodeGenerated);
                        GeneratedFileList.Show(form);
                    }
                }
            }
            finally
            {
                form.UpdateProgress(100);
            }
        }

        static void DoGenerateCode(IEnumerable<TypeBase> types, IRazorTemplate template, string output, ProcessInfo info, IDockForm form)
        {
            if (template.Mode == GenerateMode.Batch)
            {
                TypeCollectionModel model = new TypeCollectionModel(types, template);
                var content = Parse(template.Template, model);
                if (content.IsNotEmpty())
                {
                    var fileName = output + Parse("@{var output=" + template.Output + ";@output}", model);
                    fileName = fileName.Replace('<', '_').Replace(">", "");
                    SaveFile(content, fileName);
                    form.StatusText = Strings.Saved + ":" + fileName;
                    GeneratedFileList.AddFile(fileName);
                }
                info.Process += info.TypeCount;
                form.UpdateProgress(100 * info.Process / info.Total);
                Application.DoEvents();
            }
            else if (template.Mode == GenerateMode.Nested)
            {
                List<TypeBase> root = new List<TypeBase>();
                foreach (var t in types)
                {
                    if (cancel)
                        return;
                    var type = t;
                    if (type.IsNested)
                    {
                        type = type.GetNestingRoot();
                        if (root.Contains(type))
                        {
                            info.Process++;
                            continue;
                        }
                    }
                    root.Add(type);

                    TypeModel model = new TypeModel(type, types, template);
                    var content = Parse(template.Template, model);
                    if (content.IsNotEmpty())
                    {
                        var fileName = output + Parse("@{var output=" + template.Output + ";@output}", model);
                        fileName = fileName.Replace('<', '_').Replace(">", "");
                        SaveFile(content, fileName);
                        form.StatusText = Strings.Saved + ":" + fileName;
                        GeneratedFileList.AddFile(fileName);
                    }
                    info.Process++;
                    form.UpdateProgress(100 * info.Process / info.Total);
                    Application.DoEvents();
                }
            }
            else
            {
                foreach (var type in types)
                {
                    if (cancel)
                        return;
                    TypeModel model = new TypeModel(type, types, template);
                    var content = Parse(template.Template, model);
                    if (content.IsNotEmpty())
                    {
                        var fileName = output + Parse("@{var output=" + template.Output + ";@output}", model);
                        fileName = fileName.Replace('<', '_').Replace(">", "");
                        SaveFile(content, fileName);
                        form.StatusText = Strings.Saved + ":" + fileName;
                        GeneratedFileList.AddFile(fileName);
                    }
                    info.Process++;
                    form.UpdateProgress(100 * info.Process / info.Total);
                    Application.DoEvents();
                }
            }
        }

        static void DoGenerateCode(IEnumerable<TypeBase> types, IEnumerable<TemplateFile> templates, string output, IDockForm form)
        {
            var process = new ProcessInfo();
            process.TypeCount = types.Count();
            process.Total = process.TypeCount * templates.Count();

            foreach (var t in templates)
            {
                if (cancel)
                    return;
                DoGenerateCode(types, t.GetTemplate(), output, process, form);
            }
        }

        class ProcessInfo
        {
            public int Process { get; set; }
            public int TypeCount { get; set; }
            public int Total { get; set; }
        }

        static void SaveFile(string content, string fileName)
        {
            try
            {
                FileInfo file = new FileInfo(fileName);
                if (!file.Directory.Exists)
                    file.Directory.Create();
                using (StreamWriter sw = new StreamWriter(file.FullName, false, Encoding.UTF8))
                {
                    sw.Write(content);
                    sw.Close();
                }
            }
            catch (Exception exc)
            {
                Client.ShowError(exc.ToString());
            }
        }

        public static string Parse<T>(string template, T model)
        {
            try
            {
                return TemplateService.Parse(template, model);
            }
            catch (Exception exc)
            {
                ErrorList.SetError(exc);
            }
            return "";
        }
    }
}
