using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using ayrbox.winservice.Logging;

namespace ayrbox.winservice
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

            var logger = new WindowsEventLogger("ayrboxwinserviceTemplate",
                "ExampleService");




            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[] 
            //{ 
            //    new ExampleService() 
            //};
            //ServiceBase.Run(ServicesToRun);


        }
    }
}