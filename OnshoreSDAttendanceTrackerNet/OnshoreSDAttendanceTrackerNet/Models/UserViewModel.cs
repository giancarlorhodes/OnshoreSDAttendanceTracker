using OnshoreSDAttendanceTrackerNet.Interfaces;
using OnshoreSDAttendanceTrackerNet.Models;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Models
{
    public class UserViewModel : IUserPO,IUserCredentialsPO, ITeamPO, IRolePO
    {
        public UserViewModel()
        {
            User = new UserPO();
            UserCred = new UserCredentialPO();
            TeamPO = new TeamPO();
            RolePO = new RolePO();
            ListOfUserPO = new List<UserPO>();
            ListOfUserDO = new List<UserDO>();
            ListOfTeamPO = new List<ITeamPO>();
            ListOfTeamDO = new List<TeamDO>();
            ListOfRolePO = new List<RolePO>();
        }

        public int userTeam { get; set; }
        public UserPO User { get; set; }
        public UserCredentialPO UserCred { get; set; }
        public TeamPO TeamPO { get; set; }
        public RolePO RolePO { get; set; }
        public List<UserPO> ListOfUserPO { get; set; }
        public List<UserDO> ListOfUserDO { get; set; }
        public List<ITeamPO> ListOfTeamPO { get; set; }
        public List<TeamDO> ListOfTeamDO { get; set; }
        public List<RolePO> ListOfRolePO { get; set; }
        public string ErrorMessage { get; set; }

        //implementing User,UserCred, Team Interfaces
        //User
        public int UserID { get => ((IUserPO)User).UserID; set => ((IUserPO)User).UserID = value; }
        public string FirstName { get => ((IUserPO)User).FirstName; set => ((IUserPO)User).FirstName = value; }
        public string LastName { get => ((IUserPO)User).LastName; set => ((IUserPO)User).LastName = value; }
        public int RoleID_FK { get => ((IUserPO)User).RoleID_FK; set => ((IUserPO)User).RoleID_FK = value; }
        public string RoleName { get => ((IUserPO)User).RoleName; set => ((IUserPO)User).RoleName = value; }
        public string Email { get => ((IUserPO)User).Email; set => ((IUserPO)User).Email = value; }
        public bool Active { get => ((IUserPO)User).Active; set => ((IUserPO)User).Active = value; }

        //UserCred
        public int UserCredentailsID { get => ((IUserCredentialsPO)UserCred).UserCredentailsID; set => ((IUserCredentialsPO)UserCred).UserCredentailsID = value; }
        public string UserPassword { get => ((IUserCredentialsPO)UserCred).UserPassword; set => ((IUserCredentialsPO)UserCred).UserPassword = value; }
        public int UserID_FK { get => ((IUserCredentialsPO)UserCred).UserID_FK; set => ((IUserCredentialsPO)UserCred).UserID_FK = value; }
        public string Salt { get => ((IUserCredentialsPO)UserCred).Salt; set => ((IUserCredentialsPO)UserCred).Salt = value; }

        //Team
        public int TeamID { get => ((ITeamPO)TeamPO).TeamID; set => ((ITeamPO)TeamPO).TeamID = value; }
        public string Name { get => ((ITeamPO)TeamPO).Name; set => ((ITeamPO)TeamPO).Name = value; }
        public string Comment { get => ((ITeamPO)TeamPO).Comment; set => ((ITeamPO)TeamPO).Comment = value; }

        public int RoleID { get; set; }
        public string RoleNameShort { get; set; }
        public string RoleNameLong { get ; set; }
        public decimal RunningTotal { get; set; }
    }
}