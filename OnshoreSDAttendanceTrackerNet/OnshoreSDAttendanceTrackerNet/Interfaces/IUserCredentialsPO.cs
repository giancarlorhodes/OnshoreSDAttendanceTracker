using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Interfaces
{
    public interface IUserCredentialsPO
    {
        int UserCredentailsID { get; set; }
        string UserPassword { get; set; }
        int UserID_FK { get; set; }
        string Salt { get; set; } 
    }
}