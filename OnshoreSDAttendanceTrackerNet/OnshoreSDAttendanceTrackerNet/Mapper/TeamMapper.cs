using OnshoreSDAttendanceTrackerNet.Interfaces;
using OnshoreSDAttendanceTrackerNet.Models;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTracketNetBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Mapper
{
    public class TeamMapper : ITeamMapper
    {
        public List<ITeamBO> MapListOfDOsToListOfBOs(List<ITeamDO> userDOs)
        {
            throw new NotImplementedException();
        }

        public List<TeamPO> MapListOfDOsToListOfPOs(List<ITeamDO> userDOs)
        {
            throw new NotImplementedException();
        }

        public ITeamPO MapUserBOtoPO(ITeamPO userBO)
        {
            throw new NotImplementedException();
        }

        public ITeamDO MapUserDOtoBO(ITeamDO userBO)
        {
            throw new NotImplementedException();
        }

        public ITeamPO MapUserDOtoPO(ITeamDO userDO)
        {
            throw new NotImplementedException();
        }

        public ITeamDO MapUserPOtoDO(ITeamPO userPO)
        {
            throw new NotImplementedException();
        }
    }
}