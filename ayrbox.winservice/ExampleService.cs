using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using ayrbox.winservice.Logging;
using System.Timers;
using ayrbox.winservice.Core;

namespace ayrbox.winservice {
    public class ExampleService : BaseService {
        public ExampleService(ILogger logger)
            : base("ExampleService", logger) {
        }

        protected override double Interval {
            get {
                return int.Parse(ConfigurationManager.AppSettings["Interval.ExampleService"]);
            }
        }

        protected override int Order {
            get { return 0; }
        }

        protected override void Process() {
            _logger.Info(ServiceName, "Running Example Service.......");
        }
    }
}
