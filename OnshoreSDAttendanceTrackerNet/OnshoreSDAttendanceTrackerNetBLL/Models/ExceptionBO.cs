using OnshoreSDAttendanceTrackerNetBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnshoreSDAttendanceTrackerNetBLL.Models
{
    public class ExceptionBO : IExceptionBO
    {
        public int LogID { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionSource { get; set; }
        public string ExceptionURL { get; set; }
        public DateTime LogDate { get; set; }
    }
}
