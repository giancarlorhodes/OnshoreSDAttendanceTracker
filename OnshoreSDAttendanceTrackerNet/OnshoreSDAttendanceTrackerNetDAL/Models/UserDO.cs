using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnshoreSDAttendanceTrackerNetDAL.Models
{
    public class UserDO : IUserDO
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleID_FK { get; set; }
        public string Email { get; set; }
        public int TeamID { get; set; }
        public bool Active { get; set; }
    }
}
