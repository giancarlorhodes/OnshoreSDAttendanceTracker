using OnshoreSDAttendanceTrackerNet.Interfaces;
using OnshoreSDAttendanceTrackerNet.Models;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using OnshoreSDAttendanceTrackerNetBLL.Interfaces;
using OnshoreSDAttendanceTrackerNetBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace OnshoreSDAttendanceTrackerNet.AutoMapper
{
    public class UserMapper
    {
        public static List<IUserBO> MapListOfDOsToListOfBOs(List<IUserBO> userDOs)
        {
            var listOfUsers = new List<IUserBO>();

            // Iterate through DOs
            foreach (IUserDO user in userDOs)
            {
                var userBO = MapUserDOtoBO(user);
                listOfUsers.Add(userBO);
            }

            return listOfUsers;
        }

        public static List<UserPO> MapListOfDOsToListOfPOs(List<IUserDO> userDOs)
        {
            var listOfUsers = new List<UserPO>();

            // Iterate through DOs
            foreach(IUserDO user in userDOs)
            {
                var userPO = MapUserDOtoPO(user);
                listOfUsers.Add(userPO);
            }

            return listOfUsers;
        }

        public static UserPO MapUserBOtoPO(UserBO userBO)
        {
            var oUser = new UserPO();
            oUser.UserID = userBO.UserID;
            oUser.FirstName = userBO.FirstName;
            oUser.LastName = userBO.LastName;
            oUser.RoleID_FK = userBO.RoleID_FK;
            oUser.Email = userBO.Email;
            oUser.Active = userBO.Active;

            return oUser;
        }

        public static IUserBO MapUserDOtoBO(IUserDO userDO)
        {
            IUserBO oUser = new UserBO();
            oUser.UserID = userDO.UserID;
            oUser.FirstName = userDO.FirstName;
            oUser.LastName = userDO.LastName;
            oUser.RoleID_FK = userDO.RoleID_FK;
            oUser.Email = userDO.Email;
            oUser.Active = userDO.Active;
           

            return oUser;
        }

        public static UserPO MapUserDOtoPO(IUserDO userDO)
        {
            var oUser = new UserPO();
            oUser.UserID = userDO.UserID;
            oUser.FirstName = userDO.FirstName;
            oUser.LastName = userDO.LastName;
            oUser.RoleID_FK = userDO.RoleID_FK;
            oUser.Email = userDO.Email;
            oUser.Active = userDO.Active;

            return oUser;
        }

        public static IUserDO MapUserPOtoDO(UserPO userPO)
        {
            IUserDO oUser = new UserDO();
            oUser.UserID = userPO.UserID;
            oUser.FirstName = userPO.FirstName;
            oUser.LastName = userPO.LastName;
            oUser.RoleID_FK = userPO.RoleID_FK;
            oUser.Email = userPO.Email;
            oUser.Active = userPO.Active;
            //oUser.TeamID = userPO.TeamID;
            //oUser.TeamManagementID = userPO.TeamManagementID;

            return oUser;
        }
    }
}