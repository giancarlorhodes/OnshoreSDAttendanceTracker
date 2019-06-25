using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnshoreSDAttendanceTrackerNetDAL.Models;

namespace OnshoreSDAttendanceTrackerNet.Models
{
    public class UserCredentialsViewModel
    {
        // TODO: Implement User Credentials View Model
        public UserCredentialsViewModel()
        {
            UserCredential = new UserCredentialPO();
            ListOfUserPO = new List<UserCredentialPO>();
            ListOfUserDO = new List<UserCredentialsDO>();
        }

        public UserCredentialPO UserCredential { get; set; }
        public List<UserCredentialPO> ListOfUserPO { get; set; }
        public List<UserCredentialsDO> ListOfUserDO { get; set; }
        public string ErrorMessage { get; set; }
    }
}