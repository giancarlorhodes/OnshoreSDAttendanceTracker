using OnshoreSDAttendanceTracketNetBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnshoreSDAttendanceTracketNetBLL.Models
{
    public class UserCredentialsBO : IUserCredentialsBO
    {
        public int UserCredentailsID { get; set; }
        public string UserPassword { get; set; }
        public int UserID_FK { get; set; }
        public string Salt { get; set; }
    }
}
