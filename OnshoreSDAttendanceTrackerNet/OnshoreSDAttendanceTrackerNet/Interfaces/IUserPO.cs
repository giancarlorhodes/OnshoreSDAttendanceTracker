using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Interfaces
{
    public interface IUserPO
    {
        int UserID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        int RoleID_FK { get; set; }
        string Email { get; set; }
        bool Active { get; set; }
        DateTime CreateDate { get; set; }
        int CreateUser_FK { get; set; }
        DateTime ModifiedDate { get; set; }
        int ModifiedUser_FK { get; set; }
    }
}