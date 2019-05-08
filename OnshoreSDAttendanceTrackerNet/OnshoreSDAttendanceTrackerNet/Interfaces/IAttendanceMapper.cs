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
    public interface IAttendanceMapper
    {
        IAttendanceDO MapUserPOtoDO(IAttendancePO userPO);
        IAttendancePO MapUserDOtoPO(IAttendanceDO userDO);
        IAttendanceDO MapUserDOtoBO(IAttendanceBO userBO);
        IAttendancePO MapUserBOtoPO(IAttendanceBO userBO);
        List<AttendancePO> MapListOfDOsToListOfPOs(List<IAttendanceDO> userDOs);
        List<IAttendanceBO> MapListOfDOsToListOfBOs(List<IAttendanceDO> userDOs);
    }
}
