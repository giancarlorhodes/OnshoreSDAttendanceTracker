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
    public interface IExceptionMapper
    {
        IExceptionDO MapExceptionPOtoDO(ExceptionPO exceptionPO);
        ExceptionPO MapExceptionDOtoPO(IExceptionDO exceptionDO);
        IExceptionBO MapExceptionDOtoBO(IExceptionDO exceptionBO);
        ExceptionPO MapExceptionBOtoPO(ExceptionBO exceptionBO);
        List<ExceptionPO> MapListOfDOsToListOfPOs(List<IExceptionDO> exceptionDOs);
        List<IExceptionBO> MapListOfDOsToListOfBOs(List<IExceptionDO> exceptionDOs);
    }
}
