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
    public class ExceptionMapper : IExceptionMapper
    {
        public List<IExceptionBO> MapListOfDOsToListOfBOs(List<IExceptionDO> userDOs)
        {
            throw new NotImplementedException();
        }

        public List<ExceptionPO> MapListOfDOsToListOfPOs(List<IExceptionDO> userDOs)
        {
            throw new NotImplementedException();
        }

        public IExceptionPO MapUserBOtoPO(IExceptionBO userBO)
        {
            throw new NotImplementedException();
        }

        public IExceptionDO MapUserDOtoBO(IExceptionBO userBO)
        {
            throw new NotImplementedException();
        }

        public IExceptionPO MapUserDOtoPO(IExceptionDO userDO)
        {
            throw new NotImplementedException();
        }

        public IExceptionDO MapUserPOtoDO(IExceptionPO userPO)
        {
            throw new NotImplementedException();
        }
    }
}