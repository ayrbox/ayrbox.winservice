using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using ayrbox.winservice.Logging;
using System.ServiceProcess;
using System.Timers;
using System.ComponentModel;

namespace ayrbox.winservice {
    public abstract class BaseService : ServiceBase {

        private IContainer components = null;

        protected ILogger _logger;
        protected Timer _timer;



        protected abstract double Interval { get; }
        public abstract void Process();


        public void Process(object source, ElapsedEventArgs e) {
            Process();
        }


        protected BaseService(string serviceName, ILogger logger) {
            components = new System.ComponentModel.Container();
            this.ServiceName = serviceName;
            _logger = logger;
        }



        protected override void OnStart(string[] args) {
            try {
                _logger.Info(ServiceName, "Starting...");
                _timer = new Timer(this.Interval);
                _timer.Elapsed += new ElapsedEventHandler(this.Process);
                _timer.Enabled = true;

                _logger.Info(this.ServiceName, "Started");
            } catch (Exception ex) {
                _logger.Error(ServiceName, "Unable to start service", ex);
            }
        }


        protected override void OnStop() {
            _logger.Info(ServiceName, "Stopping...");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            _logger.Info(this.ServiceName, "Stopped");
        }


        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}