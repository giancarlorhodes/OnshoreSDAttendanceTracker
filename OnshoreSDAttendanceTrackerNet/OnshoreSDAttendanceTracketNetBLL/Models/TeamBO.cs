using System;
using System.Collections.Generic;
using System.Text;

namespace OnshoreSDAttendanceTracketNetBLL.Models
{
    public class TeamBO : ITeamBO
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
