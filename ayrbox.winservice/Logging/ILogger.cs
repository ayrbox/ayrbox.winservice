using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ayrbox.winservice.Logging
{
    public interface ILogger
    {
        void Delete(Log log);
        void ClearLog();
        void InsertLog(LogLevel loglevel, string message, string fullMessage = "", string reference = null);
    }
}
