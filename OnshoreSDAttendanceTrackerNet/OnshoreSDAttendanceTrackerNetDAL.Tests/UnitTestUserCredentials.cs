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
        private static UserCredentialsDataAccess _UserCredentialsDataAccess;

        public UnitTestUserCredential()
        {
            string connection = ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString;
            _UserCredentialsDataAccess = new UserCredentialsDataAccess(connection);
        }

        //[TestMethod]
        //public void GetAllUserCredentials_()
        //{
        //    // arrange
        //    List<IUserCredentialsDO> _list = new List<IUserCredentialsDO>();
        //    _list = _UserCredentialsDataAccess.GetAllUserCredentials();


        //    // act



        //    // assert

      
        //    //Assert.AreEqual(users.Count, 0);



        //}
    }

}
