using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ayrbox.winservice.Logging
{    
    public static class LoggerExtensions
    {

        public static void Debug(this ILogger logger, string message, Exception exception = null, string reference = null)
        {
            WriteLog(logger, LogLevel.Debug, message, exception, reference);
        }

        public static void Info(this ILogger logger, string message, Exception exception = null, string reference = null)
        {
            WriteLog(logger, LogLevel.Information, message, exception, reference);
        }

        public static void Warn(this ILogger logger, string message, Exception exception = null, string reference = null)
        {
            WriteLog(logger, LogLevel.Warning, message, exception, reference);
        }

        public static void Error(this ILogger logger, string message, Exception exception = null, string reference = null)
        {
            WriteLog(logger, LogLevel.Error, message, exception, reference);
        }

        public static void Fatal(this ILogger logger, string message, Exception exception = null, string reference = null)
        {
            WriteLog(logger, LogLevel.Fatal, message, exception, reference);
        }

        private static void WriteLog(ILogger logger, LogLevel level,
            string message,
            Exception ex = null,
            string reference = null) {

            if(ex is ThreadAbortException) return;

            var fullMessage = string.Empty;
            if(ex != null) {
                fullMessage = ex.ToString();
            }

            logger.InsertLog(level, message, fullMessage, reference);
        }
    }
}
