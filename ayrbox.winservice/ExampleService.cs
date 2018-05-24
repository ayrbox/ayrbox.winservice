using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using ayrbox.winservice.Logging;
using System.Timers;
using ayrbox.winservice.Core;
using ayrbox.winservice.Data;
using System.Data;
using Dapper;
using ayrbox.winservice.Models;

namespace ayrbox.winservice {
    public class ExampleService : BaseService {
        public ExampleService(ILogger logger, IDataContext dataContext)
            : base("ExampleService", logger, dataContext) {
        }


        private bool _running = false;

        protected override double Interval {
            get {
                return int.Parse(ConfigurationManager.AppSettings["Interval.ExampleService"]);
            }
        }

        protected override int Order {
            get { return 0; }
        }

        protected override void Process() {

            if (!_running) {                
                _logger.Info(ServiceName, "Running Example Service.......");
                _running = true;


                var people = _dataContext.Get<IEnumerable<Person>>(c => c.Query<Person>(@"SpGetPeople",
                        commandType: CommandType.StoredProcedure));

                foreach (var p in people) {
                    _logger.Debug(ServiceName, p.Name);
                }

                _running = false;
                _logger.Debug(ServiceName, "Example service ran successfully...");                
            }
        }
    }
}
