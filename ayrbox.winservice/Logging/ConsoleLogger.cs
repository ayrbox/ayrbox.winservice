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
            Console.WriteLine(string.Format("Source: {0}\t LogLevel: {1}\t Message: {2}\t FullMessage: {3}\t Reference: {4}",
                log.Source, log.LogLevel.ToString(), log.Message, log.FullMessage, log.Reference));
        }

    }
}
