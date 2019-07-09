using OnshoreSDAttendanceTrackerNet.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Models
{
    public class TeamViewModel : IListOfPOs<TeamPO>
    {
        public TeamViewModel()
        {
            Team = new TeamPO();
            User = new UserPO();
            ListOfPos = new List<TeamPO>();
        }
        
        public TeamPO Team { get; set; }
        public UserPO User { get; set; }
        public string ErrorMessage { get; set; }
        public List<TeamPO> ListOfPos { get; set; }
    }
}