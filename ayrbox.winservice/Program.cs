using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using ayrbox.winservice.Logging;

namespace ayrbox.winservice {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args) {
            ILogger logger;
            if (args.Length > 0 && args[0].ToLower() == "debug") {
                Console.WriteLine("Starting debug");


                logger = new ConsoleLogger();
                (new ExampleService(logger)).Process();
                (new ReadEmailService(logger)).Process();
                Console.WriteLine("Press any key to continue...");                

            } else {
                logger = new WindowsEventLogger("ayrbox.winservice");
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]  { 
                    new ExampleService(logger),
                    new ReadEmailService(logger)
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}