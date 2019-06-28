using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Models
{
    public class RolePO
    {
        public int RoleID { get; set; }
        public string RoleNameShort { get; set; }
        public string RoleNameLong { get; set; }
        public string Comment { get; set; }
    }
}