using OnshoreSDAttendanceTrackerNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace OnshoreSDAttendanceTrackerNet.Models
{
    public class UserPO : IUserPO
    {
        public int UserID { get; set ; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleID_FK { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string Employee { get; set; }
    }
}