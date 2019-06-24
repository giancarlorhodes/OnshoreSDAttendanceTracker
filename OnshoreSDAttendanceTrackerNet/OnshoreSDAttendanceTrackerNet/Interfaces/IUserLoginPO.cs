using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Interfaces
{
    public interface IUserLoginPO
    {
        // RoleEnum UserRole;

        int UserID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Salt { get; set; }
        string Password { get; set; }
        int RoleID_FK { get; set; }
        string RoleNameShort { get; set; }
        string RoleNameLong { get; set; }

    }
}