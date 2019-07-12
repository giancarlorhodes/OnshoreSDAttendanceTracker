using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Models
{
    public class TeamAttendanceHelper
    {
        public TeamAttendanceHelper()
        {
            Team = new TeamPO();
            Absence = new AbsencePO();
            User = new UserPO();
        }

        public TeamPO Team { get; set; }
        public AbsencePO Absence { get; set; }
        public UserPO User { get; set; }
        public string Name { get; set; }
    }
}