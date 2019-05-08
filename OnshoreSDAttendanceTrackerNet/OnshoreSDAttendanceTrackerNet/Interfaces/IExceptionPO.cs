using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnshoreSDAttendanceTrackerNet.Interfaces
{
    public interface IExceptionPO
    {
        int LogID { get; set; }
        string ExceptionMessage { get; set; }
        string ExceptionType { get; set; }
        string ExceptionSource { get; set; }
        string ExceptionURL { get; set; }
        DateTime LogDate { get; set; }
    }
}
