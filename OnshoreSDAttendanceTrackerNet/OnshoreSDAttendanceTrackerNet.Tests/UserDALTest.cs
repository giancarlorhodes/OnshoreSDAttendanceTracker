using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnshoreSDAttendanceTrackerNetDAL;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using System.Collections.Generic;
using System.Configuration;

namespace OnshoreSDAttendanceTrackerNet.Tests
{ 
    [TestClass]
    public class UserDALTest
    {
        private static UserDataAccess _UserDataAccess;

        public UserDALTest()
        {
            string connection = ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString;
            _UserDataAccess = new UserDataAccess(connection);
        }

        [TestMethod]
        public void testGetAllUsersEmpty()
        { 
            List<IUserDO> users=_UserDataAccess.GetAllUsers();
            Assert.AreEqual(users.Count, 0);
        }
    }
}
