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
                {"Detail", string.Format("{0,-100}", log.FullMessage)}
            };

            Console.WriteLine(
                string.Join("\t", logData
                    .Where(d => !string.IsNullOrWhiteSpace(d.Value))
                    .Select(d => string.Format("{0} : {1}", d.Key, d.Value))
                    .ToArray())
                );

            //foreach (var l in logData.Where(d => !string.IsNullOrWhiteSpace(d.Value))) {
            //    Console.WriteLine("{0}\t:\t{1}", l.Key, l.Value);
            //}

            //Console.WriteLine(string.Format("Source: {0}\t LogLevel: {1}\t Message: {2}\t FullMessage: {3}\t Reference: {4}",
            //    log.Source, log.LogLevel.ToString(), log.Message, log.FullMessage, log.Reference));
        }

    }
}
