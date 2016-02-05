using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAP.Logging
{
    public interface ILogProvider
    {
        void Write(LogEvent log);
    }
}
