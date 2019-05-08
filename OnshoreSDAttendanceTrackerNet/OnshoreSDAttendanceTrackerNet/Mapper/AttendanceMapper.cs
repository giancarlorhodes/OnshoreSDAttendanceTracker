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
    public class AttendanceMapper : IAttendanceMapper
    {
        public List<IAttendanceBO> MapListOfDOsToListOfBOs(List<IAttendanceDO> userDOs)
        {
            throw new NotImplementedException();
        }

        public List<AttendancePO> MapListOfDOsToListOfPOs(List<IAttendanceDO> userDOs)
        {
            throw new NotImplementedException();
        }

        public IAttendancePO MapUserBOtoPO(IAttendanceBO userBO)
        {
            throw new NotImplementedException();
        }

        public IAttendanceDO MapUserDOtoBO(IAttendanceBO userBO)
        {
            throw new NotImplementedException();
        }

        public IAttendancePO MapUserDOtoPO(IAttendanceDO userDO)
        {
            throw new NotImplementedException();
        }

        public IAttendanceDO MapUserPOtoDO(IAttendancePO userPO)
        {
            throw new NotImplementedException();
        }
    }
}