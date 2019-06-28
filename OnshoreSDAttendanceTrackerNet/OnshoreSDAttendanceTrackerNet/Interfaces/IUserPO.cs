using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace OnshoreSDAttendanceTrackerNet.Interfaces
{
    public interface IUserPO
    {
        int UserID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        int RoleID_FK { get; set; }
        string RoleName { get; set; }
        string Email { get; set; }
        bool Active { get; set; }
     
    }
}