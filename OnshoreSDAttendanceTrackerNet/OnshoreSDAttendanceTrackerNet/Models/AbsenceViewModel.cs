using OnshoreSDAttendanceTrackerNetDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnshoreSDAttendanceTrackerNet.Models
{
    public class AbsenceViewModel
    {
        public AbsenceViewModel()
        {
            Absence = new AbsencePO();
            Team = new TeamPO();
            ListOfAbsencePO = new List<AbsencePO>();
            ListOfAbsenceDO = new List<AbsenceDO>();
            AbsenceTypes = new List<SelectListItem>();
        }

        public AbsencePO Absence { get; set; }
        public List<AbsencePO> ListOfAbsencePO { get; set; }
        public List<AbsenceDO> ListOfAbsenceDO { get; set; }
        public TeamPO Team { get; set; }
        public List<SelectListItem> AbsenceTypes { get; set; }
        public string ErrorMessage { get; set; }
    }
}