using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ayrbox.winservice.Logging {
    public class WindowsEventLogger : ILogger {
        private readonly string _logName;
        private readonly string _machineName;


        public WindowsEventLogger(string applicationName) {
            _machineName = Environment.MachineName;
            _logName = applicationName;
        }

        public void Delete(Log log) {
            throw new NotImplementedException();
        }

        public void ClearLog() {
            EventLog.Delete(_logName);
        }

        public void InsertLog(Log log) {

            if (log.LogLevel == LogLevel.Debug) return;     //Skip logging debug in windows event



            CreateLogIfNotExists(log.Source);

            EventInstance eventInstance = new EventInstance(0, 0, ParseLogLevel(log.LogLevel));

            var eventData = new Dictionary<string, string>() {
                {"Message", log.Message},
                {"Detail", log.FullMessage},
                {"Reference", log.Reference}
            };

            EventLog.WriteEvent(log.Source,
                eventInstance,
                eventData
                    .Where(d => !string.IsNullOrWhiteSpace(d.Value))
                    .Select(d => string.Format("{0}: {1}", d.Key, d.Value))
                    .ToArray());
        }

        private EventLogEntryType ParseLogLevel(LogLevel logLevel) {
            switch (logLevel) {
                case LogLevel.Error:
                case LogLevel.Fatal:
                    return EventLogEntryType.Error;

                case LogLevel.Warning:
                    return EventLogEntryType.Warning;

                case LogLevel.Information:
                case LogLevel.Debug:
                default:
                    return EventLogEntryType.Information;
            }
        }

        private void CreateLogIfNotExists(string sourceName) {
            if (!EventLog.SourceExists(sourceName, _machineName)) {
                EventLog.CreateEventSource(sourceName, _logName, _machineName);
            }
        }
    }
}