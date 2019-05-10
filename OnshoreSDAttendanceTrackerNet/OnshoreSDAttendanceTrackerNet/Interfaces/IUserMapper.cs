using OnshoreSDAttendanceTrackerNet.Models;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetBLL.Interfaces;
using OnshoreSDAttendanceTrackerNetBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnshoreSDAttendanceTrackerNet.Interfaces
{
    public interface IUserMapper
    {
        IUserDO MapUserPOtoDO(UserPO userPO);
        UserPO MapUserDOtoPO(IUserDO userDO);
        IUserBO MapUserDOtoBO(IUserDO userDO);
        UserPO MapUserBOtoPO(UserBO userBO);
        List<UserPO> MapListOfDOsToListOfPOs(List<IUserDO> userDOs);
        List<IUserBO> MapListOfDOsToListOfBOs(List<IUserBO> userDOs);
    }
}
