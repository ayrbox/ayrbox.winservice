using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using ayrbox.winservice.Data;
using Ayrbox.Windows.Services;

namespace ayrbox.winservice {
    static class Program {

        static void Main(string[] args) {

            var _serviceDataContext = new ServiceData();
            BaseService.Run(_serviceDataContext);

        }
    }
}