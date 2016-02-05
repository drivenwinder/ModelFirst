using System;

namespace EAP.Logging
{
    [Flags]
    public enum LogLevel
    {
        Trace = 1,
        Info = 2,
        Debug = 4,
        Warn = 8,
        Error = 16,
        Fatel = 32,
    }
}
