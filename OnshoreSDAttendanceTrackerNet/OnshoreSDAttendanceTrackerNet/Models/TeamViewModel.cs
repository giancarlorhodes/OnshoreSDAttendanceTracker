using OnshoreSDAttendanceTrackerNetDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Models
{
    public class TeamViewModel
    {
        public TeamViewModel()
        {
            Team = new TeamPO();
            ListOfTeamPO = new List<TeamPO>();
            ListOfTeamDO = new List<TeamDO>();
        }
        
        public TeamPO Team { get; set; }
        public List<TeamPO> ListOfTeamPO { get; set; }
        public List<TeamDO> ListOfTeamDO { get; set; }
        public string ErrorMessage { get; set; }
    }
}