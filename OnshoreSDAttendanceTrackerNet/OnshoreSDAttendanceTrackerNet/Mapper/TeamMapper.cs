using OnshoreSDAttendanceTrackerNet.Interfaces;
using OnshoreSDAttendanceTrackerNet.Models;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using OnshoreSDAttendanceTrackerNetBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Mapper
{
    public class TeamMapper : ITeamMapper
    {
        public List<ITeamBO> MapListOfDOsToListOfBOs(List<ITeamDO> teamDOs)
        {
            var listOfTeamBOs = new List<ITeamBO>();

            // Iterate through DOs
            foreach(ITeamDO team in teamDOs)
            {
                var teamPO = MapTeamDOtoBO(team);
                listOfTeamBOs.Add(teamPO);
            }

            return listOfTeamBOs;
        }

        public List<TeamPO> MapListOfDOsToListOfPOs(List<ITeamDO> teamDOs)
        {
            var listOfTeamPOs = new List<TeamPO>();

            // Iterate through DOs
            foreach(ITeamDO team in teamDOs)
            {
                var teamPO = MapTeamDOtoPO(team);
                listOfTeamPOs.Add(teamPO);
            }

            return listOfTeamPOs;
        }

        public TeamPO MapTeamBOtoPO(TeamBO teamBO)
        {
            var oTeam = new TeamPO();
            oTeam.TeamID = teamBO.TeamID;
            oTeam.Name = teamBO.Name;
            oTeam.Comment = teamBO.Comment;
            oTeam.Active = teamBO.Active;

            return oTeam;
        }

        public ITeamBO MapTeamDOtoBO(ITeamDO teamDO)
        {
            ITeamBO oTeam = new TeamBO();
            oTeam.TeamID = teamDO.TeamID;
            oTeam.Name = teamDO.Name;
            oTeam.Comment = teamDO.Comment;
            oTeam.Active = teamDO.Active;

            return oTeam;
        }

        public TeamPO MapTeamDOtoPO(ITeamDO teamDO)
        {
            var oTeam = new TeamPO();
            oTeam.TeamID = teamDO.TeamID;
            oTeam.Name = teamDO.Name;
            oTeam.Comment = teamDO.Comment;
            oTeam.Active = teamDO.Active;

            return oTeam;
        }

        public ITeamDO MapTeamPOtoDO(TeamPO teamPO)
        {
            ITeamDO oTeam = new TeamDO();
            oTeam.TeamID = teamPO.TeamID;
            oTeam.Name = teamPO.Name;
            oTeam.Comment = teamPO.Comment;
            oTeam.Active = teamPO.Active;

            return oTeam;
        }
    }
}