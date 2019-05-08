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
    public interface IUserMapper
    {
        IUserDO MapUserPOtoDO(IUserPO userPO);
        IUserPO MapUserDOtoPO(IUserDO userDO);
        IUserDO MapUserDOtoBO(IUserBO userBO);
        UserPO MapUserBOtoPO(UserBO userBO);
        List<UserPO> MapListOfDOsToListOfPOs(List<IUserDO> userDOs);
        List<IUserBO> MapListOfDOsToListOfBOs(List<IUserDO> userDOs);

    }
}
