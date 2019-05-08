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
    public interface IAttendanceMapper
    {
        IAttendanceDO MapAttendancePOtoDO(AttendancePO attendancePO);
        AttendancePO MapAttendanceDOtoPO(IAttendanceDO attendanceDO);
        IAttendanceBO MapAttendanceDOtoBO(IAttendanceDO attendanceBO);
        AttendancePO MapAttendanceBOtoPO(AttendanceBO attendanceBO);
        List<AttendancePO> MapListOfDOsToListOfPOs(List<IAttendanceDO> attendanceDOs);
        List<IAttendanceBO> MapListOfDOsToListOfBOs(List<IAttendanceDO> attendanceDOs);
    }
}
