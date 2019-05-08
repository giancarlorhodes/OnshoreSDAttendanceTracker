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
    public interface IExceptionMapper
    {
        IExceptionDO MapUserPOtoDO(IExceptionPO userPO);
        IExceptionPO MapUserDOtoPO(IExceptionDO userDO);
        IExceptionDO MapUserDOtoBO(IExceptionBO userBO);
        IExceptionPO MapUserBOtoPO(IExceptionBO userBO);
        List<ExceptionPO> MapListOfDOsToListOfPOs(List<IExceptionDO> userDOs);
        List<IExceptionBO> MapListOfDOsToListOfBOs(List<IExceptionDO> userDOs);
    }
}
