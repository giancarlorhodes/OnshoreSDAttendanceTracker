using OnshoreSDAttendanceTrackerNetBLL.Interfaces;
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
        private AbsenceDataAccess _AbsenceDataAccess;
        private UserDataAccess _UserDataAccess;

        public TeamBusinessLogic()
        {
            _TeamDataAccess = new TeamDataAccess();
            _AbsenceDataAccess = new AbsenceDataAccess();
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

        public Tuple<string, decimal> QueryBestStandingEmployee(List<ITeamBO> allTeams, List<IAbsenceDO> allAbsences, List<IUserBO> allUsers)
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
    }
}