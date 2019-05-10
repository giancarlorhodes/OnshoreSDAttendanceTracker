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
    public interface IAbsenceMapper
    {
        IAbsenceDO MapAbsencePOtoDO(AbsencePO absencePO);
        AbsencePO MapAbsenceDOtoPO(IAbsenceDO absenceDO);
        IAbsenceBO MapAbsenceDOtoBO(IAbsenceDO absenceBO);
        AbsencePO MapAbsenceBOtoPO(AbsenceBO absenceBO);
        List<AbsencePO> MapListOfDOsToListOfPOs(List<IAbsenceDO> absenceDOs);
        List<IAbsenceBO> MapListOfDOsToListOfBOs(List<IAbsenceDO> absenceDOs);
    }
}
