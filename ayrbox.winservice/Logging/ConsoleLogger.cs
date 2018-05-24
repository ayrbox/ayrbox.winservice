using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ayrbox.winservice.Logging {
    public class ConsoleLogger : ILogger {

        public void Delete(Log log) {
            throw new NotImplementedException();
        }

        public void ClearLog() {
            Console.Clear();
        }

        public void InsertLog(Log log) {
            
            var logData = new Dictionary<string, string>() {
                {"Source", log.Source},
                {"Message", string.Format("{0,-50}", log.Message)},
                {"LogLevel", log.LogLevel.ToString()},
                {"Reference", log.Reference},
                {"Detail", string.Format("{0,-100}", log.FullMessage)},
                {"Time", log.CreatedOnUtd.ToString() }
            };

            Console.WriteLine(
                string.Join("\t", logData
                    .Where(d => !string.IsNullOrWhiteSpace(d.Value))
                    .Select(d => string.Format("{0} : {1}", d.Key, d.Value))
                    .ToArray())
                );
        }

    }
}
