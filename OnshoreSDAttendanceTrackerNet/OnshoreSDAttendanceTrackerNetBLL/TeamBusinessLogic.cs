﻿using OnshoreSDAttendanceTrackerNetBLL.Interfaces;
using OnshoreSDAttendanceTrackerNetBLL.Models;
using OnshoreSDAttendanceTrackerNetDAL;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnshoreSDAttendanceTrackerNetBLL
{
    public class TeamBusinessLogic
    {
        private TeamDataAccess _TeamDataAccess;
        private AbsenceTypeDataAccess _AbsenceDataAccess;
        private UserDataAccess _UserDataAccess;

        public TeamBusinessLogic()
        {
            _TeamDataAccess = new TeamDataAccess();
            _AbsenceDataAccess = new AbsenceTypeDataAccess();
            _UserDataAccess = new UserDataAccess();
        }

        public Tuple<string, decimal> QueryBestStandingTeam(List<ITeamBO> allTeams, List<IAbsenceDO> allAbsences)
        {
            var bestTeam = (from team in allTeams
                            join absence in allAbsences
                              on team.TeamID equals absence.TeamID_FK into selectedAbsences
                            select new { team.Name, Points = selectedAbsences.Sum(x => x.Point) })
                   .Distinct().OrderByDescending(t => t.Points).LastOrDefault();

            Tuple<string, decimal> bestStandingTeam = new Tuple<string, decimal>(bestTeam.Name, bestTeam.Points);

            return bestStandingTeam;
        }

        public Tuple<string, decimal> QueryWorstStandingTeam(List<ITeamBO> allTeams, List<IAbsenceDO> allAbsences)
        {
            var worstTeam = (from team in allTeams
                             join absence in allAbsences
                             on team.TeamID equals absence.TeamID_FK into selectedAbsences
                             select new { team.Name, Points = selectedAbsences.Sum(x => x.Point) })
                                           .Distinct().OrderByDescending(t => t.Points).FirstOrDefault();

            Tuple<string, decimal> worstStandingTeam = new Tuple<string, decimal>(worstTeam.Name, worstTeam.Points);

            return worstStandingTeam;
        }

        public Tuple<string, decimal> QueryBestStandingEmployee(List<ITeamDO> allTeams, List<IAbsenceDO> allAbsences, List<IUserDO> allUsers)
        {
            var topEmployee = (from team in allTeams
                               join absence in allAbsences
                               on team.TeamID equals absence.TeamID_FK into AllTeamAbsences
                               from entry in AllTeamAbsences
                               join employee in allUsers
                               on entry.AbsentUserID equals employee.UserID
                               select new
                               {
                                   Employee = employee.FirstName + " " + employee.LastName,
                                   Points = AllTeamAbsences.Sum(x => x.Point)
                               }).Distinct().OrderByDescending(t => t.Points).LastOrDefault();

            Tuple<string, decimal> bestStandingEmployee = new Tuple<string, decimal>(topEmployee.Employee, topEmployee.Points);

            return bestStandingEmployee;
        }

        // TODO: Correct issues with Top Employee query/Maybe write a SP instead?
        public Tuple<string,decimal> QueryBestStandingTeamMember(List<ITeamDO> allTeams, List<IAbsenceDO> allAbsences, List<IUserDO> allUsers, int roleID)
        {
            var topEmployee = (from team in allTeams
                               join absence in allAbsences
                               on team.TeamID equals absence.TeamID_FK into AllTeamAbsences
                               from entry in AllTeamAbsences
                               join employee in allUsers
                               on entry.AbsentUserID equals employee.UserID
                               where employee.RoleID_FK == roleID
                               select new
                               {
                                   Employee = employee.FirstName + " " + employee.LastName,
                                   Points = AllTeamAbsences.Sum(x => x.Point)
                               }).Distinct().OrderByDescending(t => t.Points).LastOrDefault();

            Tuple<string, decimal> bestStandingEmployee = new Tuple<string, decimal>(topEmployee.Employee, topEmployee.Points);

            return bestStandingEmployee;
        }

        public List<Tuple<string, decimal>> QueryTeamRanker(List<ITeamBO> allTeams, List<IAbsenceDO> allAbsences)
        {
            List<Tuple<string, decimal>> teamRankings = new List<Tuple<string, decimal>>();

            var teamRanks = (from team in allTeams
                             join absence in allAbsences
                             on team.TeamID equals absence.TeamID_FK into selectedAbsences
                             select new { Team = team.Name, Points = selectedAbsences.Sum(x => x.Point) })
                                           .Distinct().OrderByDescending(t => t.Points);

            foreach (var item in teamRanks)
            {
                Tuple<string, decimal> teamEntry = new Tuple<string, decimal>(item.Team, item.Points);
                teamRankings.Add(teamEntry);
            }

            return teamRankings;
        }

        public List<Tuple<string, string, decimal, DateTime>> QueryTeamAbsences(List<ITeamDO> allTeams, List<IAbsenceDO> allAbsences, List<IUserDO> allUsers)
        {
            var teamAbsences = new List<Tuple<string, string, decimal, DateTime>>();

            var listOfTeamAbsences = (from absence in allAbsences
                                      join team in allTeams on absence.TeamID_FK equals team.TeamID
                                      join user in allUsers on absence.AbsentUserID equals user.UserID
                                      select new
                                      {
                                          Employee = user.FirstName + ' ' + user.LastName,
                                          TeamName = team.Name,
                                          Points = absence.Point,
                                          absence.AbsenceDate
                                      }).Distinct().OrderBy(d => d.AbsenceDate);

            foreach(var item in listOfTeamAbsences)
            {
                Tuple<string, string, decimal, DateTime> absence = new Tuple<string, string, decimal, DateTime>(item.Employee, item.TeamName, item.Points, item.AbsenceDate);
                teamAbsences.Add(absence);
            }

            return teamAbsences;
        }
    }
}