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

namespace OnshoreSDAttendanceTrackerNet.Mapper
{
    public class UserCredentialsMapper
    {
        public static List<IUserCredentialsBO> MapListOfDOsToListOfBOs(List<IUserCredentialsBO> userCredentialsDOs)
        {
            var listOfUserCredentialsBO = new List<IUserCredentialsBO>();

            // Iterate through DOs
            foreach(IUserCredentialsDO user in userCredentialsDOs)
            {
                var userCredentials = MapUserCredentialsDOtoBO(user);
                listOfUserCredentialsBO.Add(userCredentials);
            }

            return listOfUserCredentialsBO;
        }

        public static List<UserCredentialPO> MapListOfDOsToListOfPOs(List<IUserCredentialsDO> userCredentialsDOs)
        {
            var listOfUserCredentialsPOs = new List<UserCredentialPO>();

            // Iterate through DOs
            foreach(IUserCredentialsDO user in userCredentialsDOs)
            {
                var userCredentials = MapUserCredentialsDOtoPO(user);
                listOfUserCredentialsPOs.Add(userCredentials);
            }
            return listOfUserCredentialsPOs;
        }

        public static UserCredentialPO MapUserCredentialsBOtoPO(UserCredentialsBO userCredentialsBO)
        {
            var oUserCredentials = new UserCredentialPO();
            oUserCredentials.UserCredentailsID = userCredentialsBO.UserCredentailsID;
            oUserCredentials.UserPassword = userCredentialsBO.UserPassword;
            oUserCredentials.UserID_FK = userCredentialsBO.UserID_FK;
            oUserCredentials.Salt = userCredentialsBO.Salt;

            return oUserCredentials;
        }

        public static IUserCredentialsBO MapUserCredentialsDOtoBO(IUserCredentialsDO userCredentialsDO)
        {
            IUserCredentialsBO oUserCredentials = new UserCredentialsBO();
            oUserCredentials.UserCredentailsID = userCredentialsDO.UserCredentailsID;
            oUserCredentials.UserPassword = userCredentialsDO.UserPassword;
            oUserCredentials.UserID_FK = userCredentialsDO.UserID_FK;
            oUserCredentials.Salt = userCredentialsDO.Salt;

            return oUserCredentials;
        }

        public static UserCredentialPO MapUserCredentialsDOtoPO(IUserCredentialsDO userCredentialsDO)
        {
            var oUserCredentials = new UserCredentialPO();
            oUserCredentials.UserCredentailsID = userCredentialsDO.UserCredentailsID;
            oUserCredentials.UserPassword = userCredentialsDO.UserPassword;
            oUserCredentials.UserID_FK = userCredentialsDO.UserID_FK;
            oUserCredentials.Salt = userCredentialsDO.Salt;

            return oUserCredentials;
        }

        public static IUserCredentialsDO MapUserCredentialsPOtoDO(UserCredentialPO userCredentialsPO)
        {
            IUserCredentialsDO oUserCredentials = new UserCredentialsDO();
            oUserCredentials.UserCredentailsID = userCredentialsPO.UserCredentailsID;
            oUserCredentials.UserPassword = userCredentialsPO.UserPassword;
            oUserCredentials.UserID_FK = userCredentialsPO.UserID_FK;
            oUserCredentials.Salt = userCredentialsPO.Salt;

            return oUserCredentials;
        }
    }
}