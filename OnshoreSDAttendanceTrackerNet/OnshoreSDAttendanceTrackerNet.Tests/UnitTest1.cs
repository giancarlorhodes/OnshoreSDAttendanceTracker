using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnshoreSDAttendanceTrackerNetDAL;

namespace OnshoreSDAttendanceTrackerNet.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            UserDataAccess uda = new UserDataAccess("Data Source=LAPTOP-262;Initial Catalog=OnshoreSDAttendanceTracker;Integrated Security=True");
        }
    }
}
