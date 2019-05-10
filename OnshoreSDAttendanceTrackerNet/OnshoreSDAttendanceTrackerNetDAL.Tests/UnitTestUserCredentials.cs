namespace OnshoreSDAttendanceTrackerNetDAL.Tests
{

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OnshoreSDAttendanceTrackerNetDAL.Interfaces;

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class UnitTestUserCredential
    {
        private static UserDataAccess _UserDataAccess;

        public UnitTestUserCredential()
        {
            string connection = ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString;
            _UserDataAccess = new UserDataAccess(connection);
        }

        [TestMethod]
        public void testGetAllUsersEmpty()
        {
            // arrange


            // act


            // assert



            List<IUserDO> users = _UserDataAccess.GetAllUsers();
            //Assert.AreEqual(users.Count, 0);



        }
    }

}
