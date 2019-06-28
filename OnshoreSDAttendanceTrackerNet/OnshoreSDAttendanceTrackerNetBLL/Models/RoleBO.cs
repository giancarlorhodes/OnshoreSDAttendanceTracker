using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnshoreSDAttendanceTrackerNetBLL.Models
{
    public class RoleBO
    {
        public int RoleID { get; set; }
        public string RoleNameShort { get; set; }
        public string RoleNameLong { get; set; }
        public string Comment { get; set; }
    }
}
