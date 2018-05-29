using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using ayrbox.winservice.Logging;
using ayrbox.winservice.Utils;
using ayrbox.winservice.Core;
using ayrbox.winservice.Data;

namespace ayrbox.winservice {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args) {

            ILogger logger;
            logger = CreateLogger();

            IDataContext dataContext = new ServiceData();

            var services = BaseService.GetAllServices(logger, dataContext);
            if (BaseService.IsDebug()) {

                logger.Debug("Main", "Running services instances.......");

                foreach (var s in services) {
                    s.Start();
                }
                
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();

                foreach (var s in services) {
                    s.Stop();
                }

            } else {
                ServiceBase.Run(services.ToArray());
            }
        }

        
        //Factory method for creating logger
        private static ILogger CreateLogger() {
            if (BaseService.IsDebug()) {
                return new ConsoleLogger();
            } else {
                return new WindowsEventLogger("ayrbox.winservice");
            }
        }
    }
}