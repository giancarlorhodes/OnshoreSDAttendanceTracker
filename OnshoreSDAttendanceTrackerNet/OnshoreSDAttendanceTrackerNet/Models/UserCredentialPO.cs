using OnshoreSDAttendanceTrackerNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Models
{
    public class UserCredentialPO : IUserCredentialsPO
    {
        public int UserCredentailsID { get; set; }
        public string UserPassword { get; set; }
        public int UserID_FK { get; set; }
        public string Salt { get; set; } 
    }
}