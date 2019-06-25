using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Models
{
    public class UserViewModel
    {
        // TODO: Implement the User View Model
        public UserViewModel()
        {
            User = new UserPO();
            ListOfUserPO = new List<UserPO>();
            ListOfUserDO = new List<UserPO>();
        }

        public UserPO User { get; set; }
        public List<UserPO> ListOfUserPO { get; set; }
        public List<UserPO> ListOfUserDO { get; set; }
        public string ErrorMessage { get; set; }
    }
}