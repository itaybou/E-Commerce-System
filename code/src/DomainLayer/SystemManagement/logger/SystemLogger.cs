using NLog;
using System;
using System.Diagnostics;
using System.IO;

namespace ECommerceSystem.DomainLayer.SystemManagement
{
    public static class SystemLogger
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly string EVENT_LOG_FILE = "eventlog.txt";
        private static readonly string ERROR_LOG_FILE = "errorlog.txt";

        public static void initLogger()
        {
            var logPath = Directory.GetParent(Environment.CurrentDirectory).ToString() + "\\Log\\";
            var eventLogPath = logPath + EVENT_LOG_FILE;
            var errorLogPath = logPath + ERROR_LOG_FILE;
            if (!File.Exists(eventLogPath))
            {
                File.WriteAllLines(eventLogPath, new string[] {
                     "###################################################################\n" +
                    "# EVENT LOG\n" +
                    "#  format: <time-stamp> | <method name>, (<param info>)\\n\n" +
                    "#  param info format: <index>, <type>, <param name>, <param value>\n" +
                    "###################################################################"
                    });
            }

            if (!File.Exists(errorLogPath))
            {
                File.WriteAllLines(errorLogPath, new string[] {
                     "###################################################################\n" +
                    "# ERROR LOG\n" +
                    "#  format: <time-stamp> | ERROR: <error details>\\n<stack-trace>\n" +
                    "#  stack-trace format: (at <stack-trace-method>\\n)*\n" +
                    "###################################################################"
                    });
            }
        }

        public static void LogError(string errorDesc)
        {
            var stackTrace = new StackTrace(1).ToString();
            logger.Error(errorDesc + "\n" + stackTrace);
        }

        public static void LogMethodInfo(string paramInfo)
        {
            var stackFrame = new StackFrame(2);
            var methodName = stackFrame.GetMethod().Name;
            logger.Info("{0}, ({1})", methodName, paramInfo);
        }
    }
}