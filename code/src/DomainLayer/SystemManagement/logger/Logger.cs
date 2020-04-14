using NLog;
using System.Diagnostics;
using System.Text;

namespace ECommerceSystem.DomainLayer.SystemManagement
{
    public static class SystemLogger
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();

        public static void LogMethodInfo(string paramInfo)
        {
            var stackFrame = new StackFrame(2);
            var methodName = stackFrame.GetMethod().Name;
            logger.Info("Method call [Name: {0}], [Parameters: {1}]", methodName, paramInfo);
        }
    }
}
