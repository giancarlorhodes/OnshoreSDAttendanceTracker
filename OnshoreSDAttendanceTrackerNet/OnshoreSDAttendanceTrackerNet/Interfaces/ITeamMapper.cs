using OnshoreSDAttendanceTrackerNet.Models;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTracketNetBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnshoreSDAttendanceTrackerNet.Interfaces
{
    public interface ITeamMapper
    {
        ITeamDO MapUserPOtoDO(ITeamPO userPO);
        ITeamPO MapUserDOtoPO(ITeamDO userDO);
        ITeamDO MapUserDOtoBO(ITeamDO userBO);
        ITeamPO MapUserBOtoPO(ITeamPO userBO);
        List<TeamPO> MapListOfDOsToListOfPOs(List<ITeamDO> userDOs);
        List<ITeamBO> MapListOfDOsToListOfBOs(List<ITeamDO> userDOs);
    }
}
