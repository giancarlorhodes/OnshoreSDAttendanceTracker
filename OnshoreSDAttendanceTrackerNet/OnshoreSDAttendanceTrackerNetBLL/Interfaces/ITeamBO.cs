using System;
using System.Collections.Generic;
using System.Text;

namespace OnshoreSDAttendanceTracketNetBLL.Models
{
    public interface ITeamBO
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
