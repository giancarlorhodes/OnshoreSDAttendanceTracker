using OnshoreSDAttendanceTrackerNet.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnshoreSDAttendanceTrackerNet.Models
{
    public class TeamViewModel : IListOfPOs<TeamPO>
    {
        public TeamViewModel()
        {
            Absence = new AbsencePO();
            Team = new TeamPO();
            User = new UserPO();
            ListOfPos = new List<TeamPO>();
            SMTeams = new List<TeamPO>();
            ListOfTeamAbsences = new List<AbsencePO>();
            TopTeam = new TeamAttendanceHelper();
            BottomTeam = new TeamAttendanceHelper();
            Absences = new List<SelectListItem>();
            TeamRanker = new TeamAttendanceHelper();
        }
        
        public AbsencePO Absence { get; set; }
        public TeamPO Team { get; set; }
        public UserPO User { get; set; }
        public string ErrorMessage { get; set; }
        public List<TeamPO> ListOfPos { get; set; }
        public List<TeamPO> SMTeams { get; set; }
        public List<AbsencePO> ListOfTeamAbsences { get; set; }
        public List<SelectListItem> Absences { get; set; }
        public TeamAttendanceHelper TopTeam { get; set; }
        public TeamAttendanceHelper BottomTeam { get; set; }
        public TeamAttendanceHelper TeamRanker { get; set; }

    }
}