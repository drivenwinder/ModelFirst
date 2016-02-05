using System;
using System.IO;

namespace EAP.Utils
{
    [Serializable]
    public class FileStreamInfo : MemoryStream
    {
        public string Name { get; set; }

        public FileStreamInfo() { }

        public FileStreamInfo(string name)
        {
            Name = name;
        }
    }
}
