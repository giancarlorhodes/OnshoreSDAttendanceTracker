﻿using OnshoreSDAttendanceTrackerNet.Interfaces;
using OnshoreSDAttendanceTrackerNet.Models;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using OnshoreSDAttendanceTrackerNetBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.AutoMapper
{
    public class TeamMapper
    {
        public static List<ITeamBO> MapListOfDOsToListOfBOs(List<ITeamDO> teamDOs)
        {
            var listOfTeamBOs = new List<ITeamBO>();

            // Iterate through DOs
            foreach (ITeamDO team in teamDOs)
            {
                var teamPO = MapTeamDOtoBO(team);
                listOfTeamBOs.Add(teamPO);
            }

            return listOfTeamBOs;
        }

        public static List<TeamPO> MapListOfDOsToListOfPOs(List<ITeamDO> teamDOs)
        {
            var listOfTeamPOs = new List<TeamPO>();

            // Iterate through DOs
            foreach (ITeamDO team in teamDOs)
            {
                var teamPO = MapTeamDOtoPO(team);
                listOfTeamPOs.Add(teamPO);
            }

            return listOfTeamPOs;
        }

        public static TeamPO MapTeamBOtoPO(TeamBO teamBO)
        {
            var oTeam = new TeamPO();
            oTeam.TeamID = teamBO.TeamID;
            oTeam.Name = teamBO.Name;
            oTeam.Comment = teamBO.Comment;
            oTeam.Active = teamBO.Active;
            oTeam.RunningTotal = teamBO.RunningTotal;

            return oTeam;
        }

        public static ITeamBO MapTeamDOtoBO(ITeamDO teamDO)
        {
            ITeamBO oTeam = new TeamBO();
            oTeam.TeamID = teamDO.TeamID;
            oTeam.Name = teamDO.Name;
            oTeam.Comment = teamDO.Comment;
            oTeam.Active = teamDO.Active;
            oTeam.RunningTotal = teamDO.RunningTotal;

            return oTeam;
        }

        public static TeamPO MapTeamDOtoPO(ITeamDO teamDO)
        {
            var oTeam = new TeamPO();
            oTeam.TeamID = teamDO.TeamID;
            oTeam.Name = teamDO.Name;
            oTeam.Comment = teamDO.Comment;
            oTeam.Active = teamDO.Active;
            oTeam.RunningTotal = teamDO.RunningTotal;

            return oTeam;
        }

        public static ITeamDO MapTeamPOtoDO(TeamPO teamPO)
        {
            ITeamDO oTeam = new TeamDO();
            oTeam.TeamID = teamPO.TeamID;
            oTeam.Name = teamPO.Name;
            oTeam.Comment = teamPO.Comment;
            oTeam.Active = teamPO.Active;
            oTeam.RunningTotal = teamPO.RunningTotal;

            return oTeam;
        }
    }
}