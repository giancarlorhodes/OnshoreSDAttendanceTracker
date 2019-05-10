using OnshoreSDAttendanceTrackerNetBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnshoreSDAttendanceTrackerNetBLL.Models
{
    public class UserBO : IUserBO
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleID_FK { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}
