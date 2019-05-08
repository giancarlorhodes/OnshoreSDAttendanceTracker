using System;
using System.Collections.Generic;
using System.Text;

namespace OnshoreSDAttendanceTracketNetBLL.Interfaces
{
    public interface IUserCredentialsBO
    {
        int UserCredentailsID { get; set; }
        string UserPassword { get; set; }
        int UserID_FK { get; set; }
        string Salt { get; set; }
        DateTime CreateDate { get; set; }
        int CreateUser { get; set; }
        DateTime ModifiedDate { get; set; }
        int ModifiedUser { get; set; }
    }
}
