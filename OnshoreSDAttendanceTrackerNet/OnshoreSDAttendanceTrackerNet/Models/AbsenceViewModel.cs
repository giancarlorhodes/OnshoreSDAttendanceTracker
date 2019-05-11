using OnshoreSDAttendanceTrackerNetDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Models
{
    public class AbsenceViewModel
    {
        public AbsenceViewModel()
        {
            Absence = new AbsencePO();
            ListOfAbsencePO = new List<AbsencePO>();
            ListOfAbsenceDO = new List<AbsenceDO>();
        }

        public AbsencePO Absence { get; set; }
        public List<AbsencePO> ListOfAbsencePO { get; set; }
        public List<AbsenceDO> ListOfAbsenceDO { get; set; }
        public string ErrorMessage { get; set; }
    }
}