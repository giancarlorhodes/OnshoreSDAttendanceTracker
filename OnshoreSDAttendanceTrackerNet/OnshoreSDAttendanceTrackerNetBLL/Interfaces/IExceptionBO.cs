using System;
using System.Collections.Generic;
using System.Text;

namespace OnshoreSDAttendanceTracketNetBLL.Interfaces
{
    public interface IExceptionBO
    {
        int LogID { get; set; }
        string ExceptionMessage { get; set; }
        string ExceptionType { get; set; }
        string ExceptionSource { get; set; }
        string ExceptionURL { get; set; }
        DateTime LogDate { get; set; }
    }
}
