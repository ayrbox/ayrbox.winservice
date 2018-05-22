using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ayrbox.winservice.Logging
{
    public class Log
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string FullMessage { get; set; }
        public string Reference { get; set; }
        public DateTime CreatedOnUtd { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}