using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using ayrbox.winservice.Logging;
using ayrbox.winservice.Utils;

namespace ayrbox.winservice {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args) {

            ILogger logger;
            logger = CreateLogger();

            var services = TypeFinder.FindObjectOfType<BaseService>(logger);
            if (IsDebug()) {

                logger.Debug("Main", "Running services instances.......");

                foreach (var s in services) {
                    s.Process();
                }
                
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

            } else {
                ServiceBase.Run(services.ToArray());
            }
        }

        private static bool IsDebug() {
            var args = Environment.GetCommandLineArgs();
            return args.Select(s => s.ToLower()).Contains("debug");
        }


        //Factory method for creating logger
        private static ILogger CreateLogger() {
            if (IsDebug()) {
                return new ConsoleLogger();
            } else {
                return new WindowsEventLogger("ayrbox.winservice");
            }
        }
    }
}