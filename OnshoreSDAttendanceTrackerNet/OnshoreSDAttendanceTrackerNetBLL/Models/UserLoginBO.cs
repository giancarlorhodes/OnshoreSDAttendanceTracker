using OnshoreSDAttendanceTrackerNetBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnshoreSDAttendanceTrackerNetBLL.Models
{
   public class UserLoginBO : IUserLoginBO
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public int RoleID_FK { get; set; }
        public string RoleNameShort { get; set; }
        public string RoleNameLong { get; set; }
    }
}
