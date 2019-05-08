using System;
using System.Collections.Generic;
using System.Text;

namespace OnshoreSDAttendanceTracketNetBLL.Interfaces
{
    public class IUserBO
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
