using OnshoreSDAttendanceTrackerNet.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
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
            User = new UserPO();
            ListOfAbsencePO = new List<AbsencePO>();
            ListOfAbsenceDO = new List<AbsenceDO>();
            AbsenceTypes = new List<SelectListItem>();
            TeamRunningTotals = new List<TeamPO>();
            TopTeam = new TeamAttendanceHelper();
            BottomTeam = new TeamAttendanceHelper();
            SMRanker = new TeamAttendanceHelper();
            TeamRanker = new TeamAttendanceHelper();
            TopEmployee = new TeamAttendanceHelper();

        }

        public AbsencePO Absence { get; set; }
        public TeamPO Team { get; set; }
        public UserPO User { get; set; }
        public TeamAttendanceHelper TopTeam { get; set; }
        public TeamAttendanceHelper BottomTeam { get; set; }
        public TeamAttendanceHelper SMRanker { get; set; }
        public TeamAttendanceHelper TeamRanker { get; set; }
        public TeamAttendanceHelper TopEmployee { get; set; }
        public List<AbsencePO> ListOfAbsencePO { get; set; }
        public List<AbsenceDO> ListOfAbsenceDO { get; set; }       
        public List<SelectListItem> AbsenceTypes { get; set; }
        public List<TeamPO> TeamRunningTotals { get; set; }
        public string ErrorMessage { get; set; }
    }
}