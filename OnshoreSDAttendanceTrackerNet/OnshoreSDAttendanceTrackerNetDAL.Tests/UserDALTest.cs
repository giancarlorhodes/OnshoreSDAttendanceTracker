using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnshoreSDAttendanceTrackerNetDAL;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using System.Collections.Generic;

namespace OnshoreSDAttendanceTrackerNet.Tests
{
    [TestClass]
    public class UserDALTest
    {
        private static UserDataAccess _UserDataAccess;

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            _UserDataAccess = new UserDataAccess();
        }

        [TestMethod]
        public void testGetAllUsersEmpty()
        {
            List<IUserDO> users = _UserDataAccess.GetAllUsers();
            Assert.AreEqual(users.Count, 0);
        }
    }
}