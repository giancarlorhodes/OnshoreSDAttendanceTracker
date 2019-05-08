using OnshoreSDAttendanceTrackerNet.Interfaces;
using OnshoreSDAttendanceTrackerNet.Models;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTracketNetBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Mapper
{
    public class UserCredentialsMapper : IUserCredentialsMapper
    {
        public List<IUserCredentialsBO> MapListOfDOsToListOfBOs(List<IUserCredentialsDO> userDOs)
        {
            throw new NotImplementedException();
        }

        public List<UserCredentialPO> MapListOfDOsToListOfPOs(List<IUserCredentialsDO> userDOs)
        {
            throw new NotImplementedException();
        }

        public IUserCredentialsPO MapUserBOtoPO(IUserCredentialsBO userBO)
        {
            throw new NotImplementedException();
        }

        public IUserCredentialsDO MapUserDOtoBO(IUserCredentialsBO userBO)
        {
            throw new NotImplementedException();
        }

        public IUserCredentialsPO MapUserDOtoPO(IUserCredentialsDO userDO)
        {
            throw new NotImplementedException();
        }

        public IUserCredentialsDO MapUserPOtoDO(IUserCredentialsPO userPO)
        {
            throw new NotImplementedException();
        }
    }
}