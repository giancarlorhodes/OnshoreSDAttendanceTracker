using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnshoreSDAttendanceTrackerNetDAL;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace OnshoreSDAttendanceTrackerNet.Tests
{
    [TestClass]
    public class UserDALTest
    {
        private static UserDataAccess _UserDataAccess;
        private static TeamDataAccess _TeamDataAccess;
        private readonly static ITeamDO TESTCREATETEAMDATA = new TeamDO
        {
            TeamID = 1,
            RunningTotal = 0,
            Active = true,
            Name = "test",
            Comment="test"
        };
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
            _TeamDataAccess = new TeamDataAccess();
        }       

        [TestMethod]
        public void testGetAllUsers()
        {
                List<IUserDO> users = _UserDataAccess.GetAllUsers();
                Assert.AreEqual(users.Count, 0);   
        }

        [TestMethod]
        public void testCreateUser()
        {
           Assert.IsTrue(_TeamDataAccess.CreateNewTeam(TESTCREATETEAMDATA, TESTCREATEUSERDATA1.UserID));
           Assert.IsTrue(_UserDataAccess.CreateUser(TESTCREATEUSERDATA1,TESTCREATETEAMDATA.TeamID));
           Assert.AreEqual(TESTCREATEUSERDATA1.LastName,_UserDataAccess.GetAllUsers().Where(
               u => u.UserID == 1 && u.FirstName == "TestUser" && u.LastName == "Test").First().LastName);    
        }
        [TestMethod]
        public void testGetUserById()
        {
            Assert.AreEqual(_UserDataAccess.GetUserByID(TESTCREATEUSERDATA1.UserID).UserID, TESTCREATEUSERDATA1.UserID);
        }

        [TestMethod]
        public void testUpdateUser()
        {
            Assert.IsTrue(_UserDataAccess.UpdateUser(TESTUPDATEUSERDATA,TESTCREATEUSERDATA2.UserID));
            Assert.AreEqual(TESTCREATEUSERDATA1.UserID, TESTUPDATEUSERDATA.UserID);
        }

      

        [TestMethod]
        public void testRemoveUser()
        {
            Assert.IsTrue(_UserDataAccess.RemoveUser(TESTCREATEUSERDATA1.UserID, TESTCREATEUSERDATA2.UserID));
           
            Assert.AreEqual(null, _UserDataAccess.GetUserByID(TESTCREATEUSERDATA1.UserID));
        }

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
            _TeamDataAccess.DeactivateTeam(TESTCREATETEAMDATA.TeamID);
        }
    }
}