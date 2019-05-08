using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnshoreSDAttendanceTrackerNet.Interfaces
{
    public interface ITeamPO
    {
        int TeamID { get; set; }
        string Name { get; set; }
        string Comment { get; set; }
        bool Active { get; set; }
        DateTime CreateDate { get; set; }
        int CreateUser_FK { get; set; }
        DateTime ModifiedDate { get; set; }
        int ModifiedUser_FK { get; set; }
    }
}
