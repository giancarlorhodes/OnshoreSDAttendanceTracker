using OnshoreSDAttendanceTrackerNetDAL.Models;
using OnshoreSDAttendanceTracketNetBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Mapper
{
    public class AutoMapper
    {
        public static LogicUser Map(UserDO daUser)
        {
            UserBO boUser = new UserBO();

            var type_daUser = daUser.GetType();
            var type_boUser = boUser.GetType();
        }
    }
}