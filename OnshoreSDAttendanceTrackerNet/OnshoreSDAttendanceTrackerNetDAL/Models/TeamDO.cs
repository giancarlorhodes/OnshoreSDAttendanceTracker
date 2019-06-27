using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnshoreSDAttendanceTrackerNetDAL.Models
{
    public class TeamDO : ITeamDO
    {
        public int TeamID { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int Active { get; set; }
    }
}
