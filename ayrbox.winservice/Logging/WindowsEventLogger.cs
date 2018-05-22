using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ayrbox.winservice.Logging
{
    public class WindowsEventLogger: ILogger
    {
        private readonly string _sourceName;
        private readonly string _logName;
        private readonly string _machineName;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationName">Name of the Service Application (i.e. Main folder)</param>
        /// <param name="serviceName">Name of internal service (ie Log Name)</param>
        public WindowsEventLogger(string applicationName, string serviceName) {
            _machineName = Environment.MachineName;
            _logName = applicationName;         //Log area with multiple service source Main Folder
            _sourceName = serviceName;
            
            if (!EventLog.SourceExists(_sourceName, _machineName))
            {
                EventLog.CreateEventSource(_sourceName, _logName, _machineName);
            }            
        }

        public void Delete(Log log) {
            throw new NotImplementedException();
        }

        public void ClearLog() {
            EventLog.DeleteEventSource(_sourceName, _machineName);
            EventLog.Delete(_logName);
        }

        public void InsertLog(LogLevel logLevel, string message, string fullMessage = "", string reference = null) {

            EventInstance eventInstance = new EventInstance(0, 0, ParseLogLevel(logLevel));

            var eventData = new Dictionary<string, string>() {
                {"Message", message},
                {"Detail", fullMessage},
                {"Reference", reference}
            };

            EventLog.WriteEvent(_sourceName, 
                eventInstance, 
                eventData
                    .Where(d => !string.IsNullOrWhiteSpace(d.Value))
                    .Select(d => string.Format("{0}: {1}",d.Key, d.Value))
                    .ToArray());
        }

        private EventLogEntryType ParseLogLevel(LogLevel logLevel) {            
            switch (logLevel)
            {
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
    }
}
