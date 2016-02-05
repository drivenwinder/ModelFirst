using System;
using System.Configuration;
using System.Net;

namespace EAP.Logging
{
    public delegate string LogInfoHandler();

    public class Log
    {
        public static LogInfoHandler IPAddressHandler;

        public static LogInfoHandler ReporterHandler;

        public static LogInfoHandler ClientInfoHandler;

        public static void Trace(string message)
        {
            Write("", message, LogLevel.Trace);
        }

        public static void Trace(string caption, string message)
        {
            Write(caption, message, LogLevel.Trace);
        }

        public static void Info(string message)
        {
            Write("", message, LogLevel.Info);
        }

        public static void Info(string caption, string message)
        {
            Write(caption, message, LogLevel.Info);
        }

        public static void Warn(string message)
        {
            Write("", message, LogLevel.Warn);
        }

        public static void Warn(string caption, string message)
        {
            Write(caption, message, LogLevel.Warn);
        }

        public static void Debug(string message)
        {
            Write("", message, LogLevel.Debug);
        }

        public static void Debug(string caption, string message)
        {
            Write(caption, message, LogLevel.Debug);
        }

        public static void Error(string message)
        {
            Write("", message, LogLevel.Error);
        }

        public static void Error(string caption, string message)
        {
            Write(caption, message, LogLevel.Error);
        }

        public static void Fatal(string message)
        {
            Write("", message, LogLevel.Fatel);
        }

        public static void Fatal(string caption, string message)
        {
            Write(caption, message, LogLevel.Fatel);
        }

        public static void Write(string caption, string message, LogLevel level)
        {
            Write(caption, message, level, GetIP(), GetClientInfo(), GetReporter());
        }

        static string GetReporter()
        {
            if (ReporterHandler != null)
                return ReporterHandler.Invoke();
            return Environment.UserName;
        }

        static string GetClientInfo()
        {
            if (ClientInfoHandler != null)
                return ClientInfoHandler.Invoke();
            return "OSVersion:{0}\r\nCLR Version:{1}"
                .FormatArgs(Environment.OSVersion, Environment.Version);
        }

        static string GetIP()
        {
            if (IPAddressHandler != null)
                return IPAddressHandler.Invoke();
            string hostName = Dns.GetHostName();
            IPHostEntry host = Dns.GetHostEntry(hostName);
            return host.AddressList[0].ToString();
        }

        public static void Write(string caption, string message, LogLevel level,
            string ipAddress, string clientInfo, string reporter)
        {
            LogEvent log = new LogEvent();
            log.AppId = ConfigurationManager.AppSettings["AppLogId"];
            log.IPAddress = ipAddress;
            log.Level = level;
            log.Caption = caption;
            log.Message = message;
            log.Reporter = reporter;
            log.SystemInfo = clientInfo;
            Write(log);
        }

        public static void Write(LogEvent log)
        {
            try
            {
                AsyncDelegate ad = new AsyncDelegate(write);
                ad.BeginInvoke(log, new AsyncCallback(CallbackMethod), ad);
            }
            catch { }
        }

        static void CallbackMethod(IAsyncResult ar) { }
        delegate void AsyncDelegate(LogEvent log);

        static void write(LogEvent log)
        {
            try
            {
                ILogProvider logger = new RefObjectCreator().Create<ILogProvider>();
                if (logger == null)
                    logger = new LogServiceProvider();
                logger.Write(log);
            }
            catch (Exception exc)
            {
                try
                {
                    string message = "ServiceError:{0}\n\nClientIP:{1}\nAppID:{2}\nMessage:{3}".FormatArgs(
                        exc,
                        log.IPAddress,
                        log.AppId,
                        log.Message);
                    System.Diagnostics.EventLog eventLog = new System.Diagnostics.EventLog();
                    eventLog.Log = "Application";
                    eventLog.Source = "LogService";
                    if (message.Length > 5000)
                        message = message.Substring(0, 5000);
                    eventLog.WriteEntry(message, System.Diagnostics.EventLogEntryType.Error);
                }
                catch { }
            }
        }
    }
}
