using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnshoreSDAttendanceTrackerNetDAL;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace OnshoreSDAttendanceTrackerNet.Tests
{
    [TestClass]
    public class UserDALTest
    {
        private static UserDataAccess _UserDataAccess;
        private readonly IUserDO TESTCREATEUSERDATA1 = new UserDO
        {
            UserID = 1,
            FirstName = "TestUser",
            LastName = "Test",
            RoleID_FK = 1,
            Email = "Test@outsourcing.com"
        };
        private readonly IUserDO TESTCREATEUSERDATA2 = new UserDO
        {
            UserID = 2,
            FirstName = "TestUser2",
            LastName = "Test2",
            RoleID_FK = 2,
            Email = "Test@outsourcing.com"
        };
        private readonly IUserDO TESTUPDATEUSERDATA = new UserDO
        {
            UserID = 1,
            FirstName = "UpdateUser",
            LastName = "Test2",
            RoleID_FK = 1,
            Email = "Test@outsourcing.com"
        };
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

        //[TestMethod]
        //public void testCreateUser()
        //{
        //    Assert.IsTrue(_UserDataAccess.CreateUser(TESTCREATEUSERDATA1));
        //    Assert.IsTrue(_UserDataAccess.CreateUser(TESTCREATEUSERDATA2));         
        //}

        //[TestMethod]
        //public void testUpdateUser()
        //{
        //    Assert.IsTrue(_UserDataAccess.UpdateUser(TESTUPDATEUSERDATA, TESTCREATEUSERDATA1.TeamID));
        //    Assert.AreEqual(TESTCREATEUSERDATA1.TeamID, TESTUPDATEUSERDATA.TeamID);
        //}

        [TestMethod]
        public void testGetUserById()
        {
            Assert.AreSame(_UserDataAccess.GetUserByID(TESTCREATEUSERDATA1.UserID), TESTCREATEUSERDATA1);
        }

        //[TestMethod]
        //public void testRemoveUser()
        //{
        //    foreach (IUserDO user in _UserDataAccess.GetAllUsers())
        //    {
        //        Assert.IsTrue(_UserDataAccess.RemoveUser(user.UserID, user.UserID));
        //    }
        //    testGetAllUsers();
        //}

        [ClassCleanup]
        public static void TestFixtureTearDown()
        {
            //cleanup
            string clearSql = "truncate table [User]";

            using (SqlConnection connection = new SqlConnection(_UserDataAccess.ConnectionParms))
            {
                using (SqlCommand command = new SqlCommand(clearSql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}