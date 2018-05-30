using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ServiceProcess;
using Ayrbox.Windows.Services.Logging;
using System.Timers;
using Ayrbox.Windows.Services.Data;
using System.Reflection;

namespace Ayrbox.Windows.Services {
    public abstract class BaseService : ServiceBase, IComparable<BaseService> {

        private IContainer components = null;

        protected ILogger _logger;
        protected IDataContext _dataContext;
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


        protected BaseService(string serviceName,
            ILogger logger,
            IDataContext dataContext) {
            components = new System.ComponentModel.Container();
            this.ServiceName = serviceName;
            _logger = logger;
            _dataContext = dataContext;
        }


        protected override void OnStart(string[] args) {
            Start();
        }


        protected override void OnStop() {
            _logger.Info(ServiceName, "Stopping...");

            if (_timer != null) {
                _timer.Enabled = false;
                _timer.Stop();
                _timer = null;
            }

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
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                foreach(Type baseServiceType in assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseService)))) {
                    objects.Add((BaseService)Activator.CreateInstance(baseServiceType, args));
                }
            }

            objects.Sort();
            return objects;
        }

        public static bool IsDebug() {
            var args = Environment.GetCommandLineArgs();
            return args.Select(s => s.ToLower()).Contains("debug");
        }


        public static void Run(ILogger logger, IDataContext dataContext) {
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

        public static void Run(IDataContext dataContext) {
            var logger = CreateLogger();
            Run(logger, dataContext);
        }


        //Factory method for creating logger
        public static ILogger CreateLogger() {
            if (BaseService.IsDebug()) {
                return new ConsoleLogger();
            } else {
                return new WindowsEventLogger("ayrbox.winservice");
            }
        }
    }
}
