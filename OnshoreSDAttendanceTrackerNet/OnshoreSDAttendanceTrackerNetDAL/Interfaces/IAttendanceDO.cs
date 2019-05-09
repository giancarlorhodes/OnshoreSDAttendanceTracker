using System;
using System.Collections.Generic;
using System.Text;

namespace OnshoreSDAttendanceTrackerNetDAL.Interfaces
{
    public interface IAttendanceDO
    {
        int AbsenceTypeID { get; set; }
        string Name { get; set; }
        decimal Point { get; set; }
        bool Active { get; set; }
        int TeamID_FK { get; set; }
        decimal RunningTotal { get; set; }
    }
}
