using OnshoreSDAttendanceTrackerNet.Models;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnshoreSDAttendanceTrackerNet.Interfaces
{
    public interface ITeamMapper
    {
        ITeamDO MapTeamPOtoDO(TeamPO TeamPO);
        TeamPO MapTeamDOtoPO(ITeamDO TeamDO);
        ITeamBO MapTeamDOtoBO(ITeamDO TeamBO);
        TeamPO MapTeamBOtoPO(TeamBO TeamBO);
        List<TeamPO> MapListOfDOsToListOfPOs(List<ITeamDO> TeamDOs);
        List<ITeamBO> MapListOfDOsToListOfBOs(List<ITeamDO> TeamDOs);
    }
}
