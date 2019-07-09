using OnshoreSDAttendanceTrackerNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Models
{
    public class AbsencePO : IAbsencePO
    {
        public int AbsenceTypeID { get; set; }
        public string Name { get; set; }
        public string TeamName { get; set; }
        public decimal Point { get; set; }
        public bool Active { get; set; }
        public int TeamID_FK { get; set; }
        public decimal RunningTotal { get; set; }
        public string Comments { get; set; }
        public int AbsentUserID { get; set; }
        public DateTime AbsenceDate { get; set; }
        public int TeamMgtID { get; set; }
        public string Status { get; set; }
        public string EmployeeName { get; set; }
    }
}