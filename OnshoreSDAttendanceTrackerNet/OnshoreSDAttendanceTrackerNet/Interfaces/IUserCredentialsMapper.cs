using OnshoreSDAttendanceTrackerNet.Models;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTracketNetBLL.Interfaces;
using OnshoreSDAttendanceTracketNetBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnshoreSDAttendanceTrackerNet.Interfaces
{
    public interface IUserCredentialsMapper
    {
        IUserCredentialsDO MapUserCredentialsPOtoDO(UserCredentialPO userCredentialsPO);
        UserCredentialPO MapUserCredentialsDOtoPO(IUserCredentialsDO userCredentialsDO);
        IUserCredentialsBO MapUserCredentialsDOtoBO(IUserCredentialsDO userCredentialsDO);
        UserCredentialPO MapUserCredentialsBOtoPO(UserCredentialsBO userCredentialsBO);
        List<UserCredentialPO> MapListOfDOsToListOfPOs(List<IUserCredentialsDO> userCredentialsDOs);
        List<IUserCredentialsBO> MapListOfDOsToListOfBOs(List<IUserCredentialsBO> userCredentialsDOs);
    }
}
