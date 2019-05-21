namespace OnshoreSDAttendanceTrackerNetDAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Company: Onshore Outsourcing
    /// Author: Giancarlo Rhodes
    /// Description: Used for login checks and need fields from both tables User and UserCredentials
    /// </summary>
    public interface IUserLoginDO
    {

        int UserID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }       
        string Email { get; set; }
        string Salt { get; set; }
        string Password { get; set; }
        int RoleID_FK { get; set; }
        string RoleNameShort { get; set; }
        string RoleNameLong { get; set; }

    }
}
