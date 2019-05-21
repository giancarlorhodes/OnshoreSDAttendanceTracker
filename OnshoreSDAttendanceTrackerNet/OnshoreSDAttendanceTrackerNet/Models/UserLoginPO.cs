namespace OnshoreSDAttendanceTrackerNet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web;
    using OnshoreSDAttendanceTrackerNet.Common;

    public class UserLoginPO
    {
        public RoleEnum UserRole;

        [DisplayName("User")]
        public string UserEmail { get; set; }

        [DisplayName("Password")]
        public string UserPassword { get; set; }

        public string FirstName { get; set;}

        public string LastName { get; set; }
    }
}