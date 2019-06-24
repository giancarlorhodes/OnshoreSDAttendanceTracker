namespace OnshoreSDAttendanceTrackerNet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web;
    using OnshoreSDAttendanceTrackerNet.Common;
    using OnshoreSDAttendanceTrackerNet.Interfaces;

    public class UserLoginPO : IUserLoginPO
    {
        public RoleEnum UserRole;

        
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DisplayName("User")]
        public string Email { get; set; }
        public string Salt { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }
        public int RoleID_FK { get; set; }
        public string RoleNameShort { get; set; }
        public string RoleNameLong { get; set; }
    }
}