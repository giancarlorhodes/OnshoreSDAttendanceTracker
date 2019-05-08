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
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUser_FK { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedUser_FK { get; set; }
    }
}