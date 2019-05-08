using System;
using System.Collections.Generic;
using System.Text;

namespace OnshoreSDAttendanceTrackerNetDAL.Interfaces
{
    public interface IExceptionDO
    {
        int LogID { get; set; }
        string ExceptionMessage { get; set; }
        string ExceptionType { get; set; }
        string ExceptionSource { get; set; }
        string ExceptionURL { get; set; }
        DateTime LogDate { get; set; }
    }
}
