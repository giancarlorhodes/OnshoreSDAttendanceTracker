using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnshoreSDAttendanceTrackerNetDAL;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace OnshoreSDAttendanceTrackerNet.Tests
{
    [TestClass]
    public class UserDALTest
    {
        private static UserDataAccess _UserDataAccess;
        private static SqlConnection sqlConn;
        private static IDbTransaction sqlTran;

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            _UserDataAccess = new UserDataAccess();
            sqlConn=new SqlConnection(_UserDataAccess.ConnectionParms);
            sqlTran = sqlConn.BeginTransaction();
        }       

        [TestMethod]
        public void testGetAllUsers()
        {
            List<IUserDO> users = _UserDataAccess.GetAllUsers();
            Assert.AreEqual(users.Count, 0);
        }

        
        [ClassCleanup]
        public static void TestFixtureTearDown(TestContext context)
        {
            sqlTran.Rollback();
            sqlConn.Close();
        }
    }
}