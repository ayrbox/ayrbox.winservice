using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using ayrbox.winservice.Logging;
using System.ServiceProcess;
using System.Timers;
using System.ComponentModel;
using System.Reflection;

namespace ayrbox.winservice.Core {
    public abstract class BaseService : ServiceBase, IComparable<BaseService> {

        private IContainer components = null;

        protected ILogger _logger;
        protected Timer _timer;



        protected abstract double Interval { get; }
        protected abstract int Order { get; }
        protected abstract void Process();


        protected void Process(object source, ElapsedEventArgs e) {
            Process();
        }

        public virtual void Start() {
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


        protected BaseService(string serviceName, ILogger logger) {
            components = new System.ComponentModel.Container();
            this.ServiceName = serviceName;
            _logger = logger;
        }



        protected override void OnStart(string[] args) {
            Start();
        }


        protected override void OnStop() {            
            _logger.Info(ServiceName, "Stopping...");

            _timer.Enabled = false;
            _timer.Stop();
            _timer = null;


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

        public int CompareTo(BaseService other) {
            // If other is not a valid object reference, this instance is greater.
            if (other == null) return 1;
            return Order.CompareTo(other.Order);
        }




        public static IEnumerable<BaseService> GetAllServices(params object[] args) {

            List<BaseService> objects = new List<BaseService>();
            foreach (Type type in
                Assembly.GetAssembly(typeof(BaseService)).GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseService)))) {
                        objects.Add((BaseService)Activator.CreateInstance(type, args));
            }
            objects.Sort();
            return objects;
        }
    }
}
