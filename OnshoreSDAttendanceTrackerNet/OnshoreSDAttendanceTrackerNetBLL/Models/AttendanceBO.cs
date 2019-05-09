using OnshoreSDAttendanceTracketNetBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnshoreSDAttendanceTracketNetBLL.Models
{
    public class AttendanceBO : IAttendanceBO
    {
        public int AbsenceTypeID { get; set; }
        public string Name { get; set; }
        public decimal Point { get; set; }
        public bool Active { get; set; }
        public int TeamID_FK { get; set; }
        public decimal RunningTotal { get; set; }
    }
}
