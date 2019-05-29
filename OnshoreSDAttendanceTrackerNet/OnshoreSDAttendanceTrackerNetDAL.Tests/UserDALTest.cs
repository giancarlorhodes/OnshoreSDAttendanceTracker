using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnshoreSDAttendanceTrackerNetDAL;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;

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
        public void testGetAllUsers()
        {
                List<IUserDO> users = _UserDataAccess.GetAllUsers();
                Assert.AreEqual(users.Count, 0);   
        }

        
        [ClassCleanup]
        public static void TestFixtureTearDown()
        {
        }
    }
}