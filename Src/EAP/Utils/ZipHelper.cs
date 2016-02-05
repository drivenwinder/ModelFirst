using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace EAP.Utils
{
    public class ZipHelper
    {
        public static void Compress(Stream stream, FileStreamInfo file, params FileStreamInfo[] files)
        {
            List<FileStreamInfo> lst = new List<FileStreamInfo>();
            lst.Add(file);
            lst.AddRange(files);
            Compress(stream, lst.ToArray());
        }

        public static void Compress(Stream stream, FileStreamInfo[] files)
        {
            ZipOutputStream zipStream = new ZipOutputStream(stream);
            zipStream.SetLevel(6);

            for (int i = 0; i < files.Length; i++)
            {
                FileStreamInfo file = files[i];
                byte[] buffer = file.ToArray();

                ZipEntry entryPart = new ZipEntry(file.Name);
                entryPart.DateTime = DateTime.Now;
                entryPart.Size = buffer.Length;

                zipStream.PutNextEntry(entryPart);
                zipStream.Write(buffer, 0, buffer.Length);
            }
            zipStream.Finish();
        }
    }
}
