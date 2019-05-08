using OnshoreSDAttendanceTrackerNet.Interfaces;
using OnshoreSDAttendanceTrackerNet.Models;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using OnshoreSDAttendanceTracketNetBLL.Interfaces;
using OnshoreSDAttendanceTracketNetBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Mapper
{
    public class UserMapper : IUserMapper
    {
        //public static UserBO Map(UserDO daUser)
        //{
        //    UserBO boUser = new UserBO();

        //    var type_daUser = daUser.GetType();
        //    var type_boUser = boUser.GetType();

        //    foreach(var field_UserDO in type_daUser.GetFields())
        //    {
        //        var field_UserBO = type_boUser.GetField(field_UserDO.Name);
        //        field_UserBO.SetValue(boUser, field_UserDO.GetValue(daUser));
        //    }

        //    foreach(var prop_UserDO in type_daUser.GetProperties())
        //    {
        //        var prop_UserBO = type_boUser.GetProperty(prop_UserDO.Name);
        //        prop_UserBO.SetValue(boUser, prop_UserDO.GetValue(daUser));
        //    }

        //    return boUser;
        //}

        public List<IUserBO> MapListOfDOsToListOfBOs(List<IUserDO> userDOs)
        {
            throw new NotImplementedException();
        }

        public List<UserPO> MapListOfDOsToListOfPOs(List<IUserDO> userDOs)
        {
            throw new NotImplementedException();
        }

        public UserPO MapUserBOtoPO(UserBO userBO)
        {
            throw new NotImplementedException();
        }

        public IUserDO MapUserDOtoBO(IUserBO userBO)
        {
            throw new NotImplementedException();
        }

        public IUserPO MapUserDOtoPO(IUserDO userDO)
        {
            throw new NotImplementedException();
        }

        public IUserDO MapUserPOtoDO(IUserPO userPO)
        {
            throw new NotImplementedException();
        }
    }
}