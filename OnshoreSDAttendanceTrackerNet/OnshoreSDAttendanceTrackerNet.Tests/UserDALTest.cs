using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnshoreSDAttendanceTrackerNetDAL;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using System.Collections.Generic;


namespace OnshoreSDAttendanceTrackerNet.Tests
{
    [TestClass]
    public class UserDALTest
    {
        private static UserDataAccess uda;

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            uda = new UserDataAccess("Data Source=LAPTOP-262;Initial Catalog=OnshoreSDAttendanceTracker;Integrated Security=True");
        }

        [TestMethod]
        public void testGetAllUsersEmpty()
        {
            List<IUserDO> users=uda.GetAllUsers();
            Assert.AreEqual(users.Count, 0);
        }
    }
}
