using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ayrbox.winservice.Logging;

namespace ayrbox.winservice {
    public class ReadEmailService : BaseService {
        public ReadEmailService(ILogger logger)
            : base("ReadEmailService", logger) {
        }

        protected override double Interval {
            get {
                return 360000;
            }
        }


        public override void Process() {
            _logger.Info(ServiceName, "Processing email...");



            _logger.Info(ServiceName, "Getting list of mailboxes...");

            string[] mailboxes = {"mailbox1", "mailbox2", "mailbox3"};
            foreach(var mailbox in mailboxes) {
                _logger.Info(ServiceName, "checking mailbox : " + mailbox);
                try {
                    //actual mailbox reading
                } catch (Exception ex) {
                    _logger.Error(ServiceName, "Error reading mailbox",
                        ex, mailbox);
                }
            }

            _logger.Info(ServiceName, "Reading email completed");
        }
    }
}