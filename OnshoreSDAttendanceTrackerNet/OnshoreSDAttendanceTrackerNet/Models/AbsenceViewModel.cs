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
    public class AbsenceViewModel : IListOfPOs<AbsencePO>
    {
        public AbsenceViewModel()
        {
            Absence = new AbsencePO();
            Team = new TeamPO();
            User = new UserPO();
            ListOfPos = new List<AbsencePO>();
            SMTeams = new List<TeamPO>();
            Absences = new List<SelectListItem>();
            TopTeam = new TeamAttendanceHelper();
            BottomTeam = new TeamAttendanceHelper();
            SMRanker = new TeamAttendanceHelper();
            TeamRanker = new TeamAttendanceHelper();
            TopEmployee = new TeamAttendanceHelper();
        }

        // annotations use the display for formatting labels for properties
        public AbsencePO Absence { get; set; }
        public TeamPO Team { get; set; }
        public UserPO User { get; set; }

        public List<AbsencePO> ListOfPos { get; set; }
        public List<TeamPO> SMTeams { get; set; }
        public TeamAttendanceHelper TopTeam { get; set; }
        public TeamAttendanceHelper BottomTeam { get; set; }
        public TeamAttendanceHelper SMRanker { get; set; }
        public TeamAttendanceHelper TeamRanker { get; set; }
        public TeamAttendanceHelper TopEmployee { get; set; }
        public List<SelectListItem> Absences { get; set; }
        public List<SelectListItem> AbsenceTypes { get; set; }
        public string ErrorMessage { get; set; }
    }
}