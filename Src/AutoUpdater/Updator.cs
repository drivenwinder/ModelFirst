using ICCEmbedded.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Xml.Linq;

namespace AutoUpdate
{
    /// <summary>
    /// 更新檢測與下載的工具。
    /// <para>在服務器和本地都有一份模塊信息的列表Manifest.xml，
    /// 列表中定義了主模塊（Main）、模塊版本、模塊依賴等信息。</para>
    /// <para>本地不一定安裝了全部模塊，用本地模塊與服務器模塊對比，
    /// 如果有更新，或者依賴項有更新則下載更新</para>
    /// <para>程序動態安裝模塊后，需要更新本地Manifest.xml</para>
    /// </summary>
    public class Updator
    {
        string m_UpdateAddress = ConfigurationManager.AppSettings["updateAddress"];
        string m_ProcessName = ConfigurationManager.AppSettings["processName"];
        string m_UpdateFileFolder = "TempUpdateFile";
        List<string> m_Downloaded = new List<string>();
        List<Manifest.ModuleInfo> m_Newer = new List<Manifest.ModuleInfo>();

        public MainWindow MainWindow { get; set; }

        public void Update()
        {
            MainWindow.AppendInfo(Resources.CheckingUpdate);
            DownloadFile(m_UpdateAddress, Manifest.FileName, (x, y) =>
            {
                using (var stream = new MemoryStream(y.Result))
                {
                    var server = Manifest.Load(stream);//服務器上定義的模塊信息
                    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Manifest.FileName);
                    Manifest.EnsureManifestFile(path);
                    var local = Manifest.Load(File.OpenRead(path));//本地的模塊信息，本地模塊是服務器模塊的子集，本地不一定安裝有全部模塊
                    var newer = Compare(local, server);
                    MainWindow.UpdateProgress(5);
                    if (newer.Count > 0)
                    {
                        DownloadAndExtractFile(newer, (paths) =>
                        {
                            if (KillProcess())
                            {
                                MainWindow.AppendInfo(Resources.CopyingFiles);
                                foreach (var m in newer)
                                {
                                    m.Files.Clear();
                                    m.Files.AddRange(CopyDirectory(paths.Where(p => p.Item2.CIEquals(m.Name)).Select(p => p.Item1)));
                                }
                                MainWindow.UpdateProgress(10);
                                UpdateManifest(local, newer);
                                MainWindow.UpdateProgress(10);
                                MainWindow.AppendInfo(Resources.DeletingTempFiles);
                                DeleteDirectory(m_UpdateFileFolder);
                                RunStartup();
                            }
                            MainWindow.Close();
                        });
                    }
                    else
                    {
                        MainWindow.UpdateProgress(50);
                        DeleteDirectory(m_UpdateFileFolder);
                        MainWindow.UpdateProgress(40);
                        RunStartup();
                        MainWindow.Close();
                    }
                }
            }, 5);
        }

        /// <summary>
        /// 删除进程
        /// </summary>
        bool KillProcess()
        {
            Process[] processes = Process.GetProcessesByName(m_ProcessName);
            if (processes.Length > 0)
            {
                var result = MessageBox.Show(MainWindow, Resources.KillProcessConfirmation, MainWindow.Title, MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                    return KillProcess();
                return false;
            }
            return true;
            //if (processes.Length > 0)
            //foreach (var p in processes)
            //    p.Kill();
        }

        /// <summary>
        /// 复制目录中的所以文件和子目录
        /// </summary>
        IList<string> CopyDirectory(string sourceDirName, string destDirName)
        {
            List<string> result = new List<string>();
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
                File.SetAttributes(destDirName, File.GetAttributes(sourceDirName));
            }
            if (destDirName[destDirName.Length - 1] != Path.DirectorySeparatorChar)
                destDirName = destDirName + Path.DirectorySeparatorChar;
            string[] files = Directory.GetFiles(sourceDirName);
            foreach (string file in files)
            {
                var dest = destDirName + Path.GetFileName(file);
                File.Copy(file, dest, true);
                File.SetAttributes(dest, FileAttributes.Normal);
                result.Add(dest);
            }
            string[] dirs = Directory.GetDirectories(sourceDirName);
            foreach (string dir in dirs)
            {
                result.AddRange(CopyDirectory(dir, destDirName + Path.GetFileName(dir)));
            }
            return result;
        }

        IList<string> CopyDirectory(IEnumerable<string> paths)
        {
            List<string> files = new List<string>();
            foreach (var path in paths)
                files.AddRange(CopyDirectory(path, AppDomain.CurrentDomain.BaseDirectory));
            return files;
        }

        /// <summary>
        /// 下载更新文件
        /// </summary>
        void DownloadFile(string baseAddress, string file, DownloadDataCompletedEventHandler downloadCompleted, double progress)
        {
            var client = new WebClient();
            client.BaseAddress = baseAddress;
            double value = 0;
            client.DownloadProgressChanged += (sender, e) =>
            {
                var i = e.ProgressPercentage * progress / 100 - value;
                value = e.ProgressPercentage * progress / 100;
                MainWindow.UpdateProgress(i);
            };
            client.DownloadDataCompleted += (x, y) =>
            {
                if (y.Error != null)
                    MainWindow.ShowError(y.Error);
                else
                {
                    try
                    {
                        downloadCompleted(x, y);
                    }
                    catch (Exception exc) { MainWindow.ShowError(exc); }
                }
            };
            client.DownloadDataAsync(new Uri(file, UriKind.Relative));
        }

        /// <summary>
        /// 运行起始文件
        /// </summary>
        public void RunStartup()
        {
            var info = new System.Diagnostics.ProcessStartInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, m_ProcessName + ".exe"));
            info.UseShellExecute = true;
            info.Arguments = System.Environment.CommandLine;
            System.Diagnostics.Process.Start(info);
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        void DeleteDirectory(string path)
        {
            try
            {
                System.IO.Directory.Delete(path, true);
            }
            catch { MainWindow.AppendInfo(Resources.DeleteFileFailure); }
        }

        /// <summary>
        /// 保存并解压更新文件
        /// </summary>
        string SaveAndExtractZip(byte[] bytes, string zipFilePath, string updateFile, double progress)
        {
            if (!Directory.Exists(zipFilePath))
                Directory.CreateDirectory(zipFilePath);
            var zipFile = Path.Combine(zipFilePath, updateFile);
            using (BinaryWriter writer = new BinaryWriter(new FileStream(zipFile, FileMode.OpenOrCreate)))
            {
                writer.Write(bytes);
                writer.Flush();
                writer.Close();
            }
            var extractPath = Path.Combine(zipFilePath, "extract");
            FastZipEvents events = new FastZipEvents();
            //events.CompletedFile = (sender, e) =>
            //{
            //    MainWindow.AppendInfo(Properties.Resources.ExtractedFile + ":" + e.Name);
            //};
            double value = 0;
            events.TotalProgress = (sender, e) =>
            {
                var i = e.PercentComplete * progress / 100 - value;
                value = e.PercentComplete * progress / 100;
                MainWindow.UpdateProgress(i);
            };
            FastZip zip = new FastZip(events);
            zip.ExtractZip(zipFile, extractPath, "");
            return extractPath;
        }

        void DownloadAndExtractFile(IList<Manifest.ModuleInfo> newer, Action<IList<Tuple<string, string>>> complete)
        {
            List<Tuple<string, string>> extractPaths = new List<Tuple<string, string>>();
            var count = newer.Sum(p => p.Files.Count);
            double progress = 70 / count;
            foreach (var module in newer)
            {
                foreach (var file in module.Files)
                {
                    MainWindow.AppendInfo(Resources.Downloading + ":" + file);
                    DownloadFile(m_UpdateAddress, file, (m, n) =>
                    {
                        var zipFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, m_UpdateFileFolder, Guid.NewGuid().ToString("N"));
                        var extractPath = SaveAndExtractZip(n.Result, zipFilePath, file, progress * .5);
                        extractPaths.Add(new Tuple<string, string>(extractPath, module.Name));
                        if (extractPaths.Count == count)
                            complete.Invoke(extractPaths);
                    }, progress * .5);
                }
            }
        }

        bool CheckVersion(Manifest.ModuleInfo local, Manifest.ModuleInfo server)
        {
            Version serverVersion;
            Version localVersion;
            if (Version.TryParse(server.Version, out serverVersion) &&
                Version.TryParse(local.Version, out localVersion))
            {
                if (localVersion.CompareTo(serverVersion) >= 0)
                    return true;
            }
            return false;
        }

        IList<Manifest.ModuleInfo> Compare(IList<Manifest.ModuleInfo> local, IList<Manifest.ModuleInfo> server)
        {
            List<Manifest.ModuleInfo> result = new List<Manifest.ModuleInfo>();
            foreach (var localModule in local)
            {
                var serverModule = server.Where(p => p.Name.CIEquals(localModule.Name)).FirstOrDefault();
                if (serverModule != null)
                {
                    result.AddRange(CompareReference(local, serverModule.Reference));
                    if (CheckVersion(localModule, serverModule))
                        continue;
                    result.Add(serverModule);
                }
            }
            return result.Distinct(Manifest.ModuleInfo.Comparer).ToList();
        }

        IList<Manifest.ModuleInfo> CompareReference(IList<Manifest.ModuleInfo> local, IList<Manifest.ModuleInfo> references)
        {
            List<Manifest.ModuleInfo> result = new List<Manifest.ModuleInfo>();
            foreach (var serverModule in references)
            {
                result.AddRange(CompareReference(local, serverModule.Reference));
                var localModule = local.Where(p => p.Name.CIEquals(serverModule.Name)).FirstOrDefault();
                if (localModule != null)
                {
                    if (CheckVersion(localModule, serverModule))
                        continue;
                }
                result.Add(serverModule);
            }
            return result;
        }

        List<Manifest.ModuleInfo> UpdateManifest(IList<Manifest.ModuleInfo> local, IList<Manifest.ModuleInfo> newer)
        {
            var modules = new List<Manifest.ModuleInfo>();
            foreach (var n in newer)
            {
                n.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                modules.Add(n);
            }
            foreach (var n in local.Where(p => !modules.Any(q => p.Name.CIEquals(q.Name))).ToList())
            {
                modules.Add(n);
            }
            Manifest.Save(modules, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Manifest.FileName));
            return modules;
        }
    }
}
