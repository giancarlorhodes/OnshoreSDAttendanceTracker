using OnshoreSDAttendanceTrackerNet.Models;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTracketNetBLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnshoreSDAttendanceTrackerNet.Interfaces
{
    public interface IUserCredentialsMapper
    {
        IUserCredentialsDO MapUserPOtoDO(IUserCredentialsPO userPO);
        IUserCredentialsPO MapUserDOtoPO(IUserCredentialsDO userDO);
        IUserCredentialsDO MapUserDOtoBO(IUserCredentialsBO userBO);
        IUserCredentialsPO MapUserBOtoPO(IUserCredentialsBO userBO);
        List<UserCredentialPO> MapListOfDOsToListOfPOs(List<IUserCredentialsDO> userDOs);
        List<IUserCredentialsBO> MapListOfDOsToListOfBOs(List<IUserCredentialsDO> userDOs);
    }
}
