using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using ayrbox.winservice.Logging;
using System.Timers;

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

        public override void Process() {
            _logger.Info(ServiceName, "Processing the services.");
        }
    }
}
