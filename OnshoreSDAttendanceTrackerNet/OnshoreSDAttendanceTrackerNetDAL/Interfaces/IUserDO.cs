using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
namespace OnshoreSDAttendanceTrackerNetDAL.Interfaces
{
    public interface IUserDO
    {
        int UserID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        int RoleID_FK { get; set; }
        string RoleName { get; set; }
        string Email { get; set; }
        bool Active { get; set; }
        int TeamID { get; set; }
    }
}
