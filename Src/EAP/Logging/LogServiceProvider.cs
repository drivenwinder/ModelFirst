using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAP.LogService;

namespace EAP.Logging
{
    public class LogServiceProvider : ILogProvider
    {
        public void Write(LogEvent log)
        {
            using (LogServiceClient client = new LogServiceClient())
            {
                LogService.LogEvent l = new LogService.LogEvent();
                l.AppId = log.AppId;
                l.Caption = log.Caption;
                l.IPAddress = log.IPAddress;
                l.Level = (LogService.LogLevel)log.Level;
                l.LogTime = log.LogTime;
                l.Message = log.Message;
                l.Reporter = log.Reporter;
                l.SystemInfo = log.SystemInfo;
                client.Write(l);
            }
        }
    }
}
