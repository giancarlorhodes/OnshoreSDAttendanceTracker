using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnshoreSDAttendanceTrackerNet.Interfaces
{
    public interface IRolePO
    {
       int RoleID { get; set; }
       string RoleNameShort { get; set; }
       string RoleNameLong { get; set; }
       string Comment { get; set; }
    }
}
