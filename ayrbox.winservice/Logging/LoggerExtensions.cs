using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ayrbox.winservice.Logging
{    
    public static class LoggerExtensions
    {

        public static void Debug(this ILogger logger, string source, string message, Exception exception = null, string reference = null)
        {
            WriteLog(logger, LogLevel.Debug, source, message, exception, reference);
        }

        public static void Info(this ILogger logger, string source, string message, Exception exception = null, string reference = null)
        {
            WriteLog(logger, LogLevel.Information, source, message, exception, reference);
        }

        public static void Warn(this ILogger logger, string source, string message, Exception exception = null, string reference = null)
        {
            WriteLog(logger, LogLevel.Warning, source, message, exception, reference);
        }

        public static void Error(this ILogger logger, string source, string message, Exception exception = null, string reference = null)
        {
            WriteLog(logger, LogLevel.Error, source, message, exception, reference);
        }

        public static void Fatal(this ILogger logger, string source, string message, Exception exception = null, string reference = null)
        {
            WriteLog(logger, LogLevel.Fatal, source, message, exception, reference);
        }

        private static void WriteLog(ILogger logger, LogLevel level,
            string source,
            string message,
            Exception ex = null,
            string reference = null) {

            if(ex is ThreadAbortException) return;

            var fullMessage = string.Empty;
            if(ex != null) {
                fullMessage = ex.ToString();
            }

            logger.InsertLog(new Log {
                Message = message,
                FullMessage = fullMessage,
                Reference = reference,
                Source = source,
                LogLevel = level,
                CreatedOnUtd = DateTime.UtcNow
            });
        }
    }
}
