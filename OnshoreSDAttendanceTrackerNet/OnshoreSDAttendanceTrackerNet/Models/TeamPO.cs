using OnshoreSDAttendanceTrackerNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Models
{
    public class TeamPO : ITeamPO
    {
        public int TeamID { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int Active { get; set; }
    }
}