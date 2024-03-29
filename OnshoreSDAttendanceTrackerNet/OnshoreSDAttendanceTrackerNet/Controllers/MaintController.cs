﻿using OnshoreSDAttendanceTrackerErrorLogger;
using OnshoreSDAttendanceTrackerNet.AutoMapper;
using OnshoreSDAttendanceTrackerNet.Models;
using OnshoreSDAttendanceTrackerNet.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web.Mvc;
using System.Linq;
using OnshoreSDAttendanceTrackerNetBLL;
using OnshoreSDAttendanceTrackerNetBLL.Models;
using OnshoreSDAttendanceTrackerNetBLL.Interfaces;
using OnshoreSDAttendanceTrackerNet.Common;

namespace OnshoreSDAttendanceTrackerNet.Controllers
{
    [Authorize]
    public class MaintController : Controller
    {
        private TeamDataAccess _TeamDataAccess;
        private AbsenceTypeDataAccess _AbsenceDataAccess;
        private UserDataAccess _UserDataAccess;
        private TeamBusinessLogic _TeamBusinessLogic;

        public MaintController()
        {
            string teamConn = ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString;
            string absenceConn = ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString;
            _TeamDataAccess = new TeamDataAccess();
            _AbsenceDataAccess = new AbsenceTypeDataAccess();
            _UserDataAccess = new UserDataAccess();
            _TeamBusinessLogic = new TeamBusinessLogic();
        }

        #region Team

        [HttpGet]
        //[ValidateAntiForgeryToken]
        ///<summary>
        /// Gets form for creating a new team
        /// </summary>
        /// TODO: Style more on the view
        public ActionResult AddTeam()
        {
            ActionResult oResponse = null;
            var userPO = (IUserPO)Session["UserModel"];

            // Ensure user is authenticated
            if (userPO != null && userPO.RoleID_FK == (int)RoleEnum.Administrator)
            {
                var teamVM = new TeamViewModel();

                oResponse = View(teamVM);
            }
            else
            {
                // User doesn't have access, redirect
                oResponse = RedirectToAction("Index", "Home");
            }

            return oResponse;
        }

        [HttpPost]
        ///<summary>
        /// Sends request to database for creating a new team
        /// </summary>
        public ActionResult AddTeam(TeamViewModel iViewModel)
        {
            ActionResult oResponse = null;
            var userPO = (IUserPO)Session["UserModel"];

            // Ensure user is authenticated
            if (userPO.Email != null && userPO.RoleID_FK == (int)RoleEnum.Administrator)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Map Team properties from presentation to data objects
                        ITeamDO lTeamForm = TeamMapper.MapTeamPOtoDO(iViewModel.Team);

                        // Passes form to AddTeam method
                        _TeamDataAccess.CreateNewTeam(lTeamForm, userPO.UserID);
                        oResponse = RedirectToAction("ViewAllTeams", "Maint");
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex, "AddTeam", "Maint");
                        iViewModel.ErrorMessage = "There was an issue adding a new team. Please try again. If the problem persists contact your IT department.";

                        oResponse = View(iViewModel);
                    }
                }
                else
                {
                    oResponse = View(iViewModel);
                }
            }
            else
            {
                // User doesn't have access
                oResponse = RedirectToAction("Index", "Home");
            }

            return oResponse;
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        ///<summary>
        /// Views all teams(admin, service manager and team leads)
        /// </summary>
        public ActionResult ViewAllTeams()
        {
            ActionResult oResponse = null;
            var viewAllTeamsVM = new TeamViewModel();
            var userPO = (IUserPO)Session["UserModel"];

            // Ensures authenticated
            if (userPO.Email != null && userPO.RoleID_FK >= (int)RoleEnum.Administrator && (int)RoleEnum.Team_Lead <= 3)
            {
                try
                {
                    var allTeams = _TeamDataAccess.GetAllTeams();
                    var smAllTeams = _TeamDataAccess.GetAllSMTeamsByUserID(userPO.UserID);
                    var smTeams = _TeamDataAccess.GetAllSMTeams();
                    var allUsers = _UserDataAccess.GetAllUsers();
                    var allAbsences = PointsDataAccess.ViewAllAbsences();

                    switch (userPO.RoleID_FK)
                    {
                        case 1:
                            // TODO: Add widget data to view model/view                            
                            // Maps from data objects to presentation objects.
                            viewAllTeamsVM.ListOfPos = TeamMapper.MapListOfDOsToListOfPOs(allTeams);
                            var bestStandingTeam = _TeamBusinessLogic.QueryBestStandingTeam(TeamMapper.MapListOfDOsToListOfBOs(allTeams), allAbsences);
                            var bottomStandingTeam = _TeamBusinessLogic.QueryWorstStandingTeam(TeamMapper.MapListOfDOsToListOfBOs(allTeams), allAbsences);
                            var teamRanker = _TeamBusinessLogic.QueryTeamRanker(TeamMapper.MapListOfDOsToListOfBOs(allTeams), allAbsences);
                            AssociateAdminValues(viewAllTeamsVM, bestStandingTeam, bottomStandingTeam, allAbsences, teamRanker, userPO);

                            oResponse = View(viewAllTeamsVM);
                            break;
                        case 2:
                            // TODO: Add Widget data to view model/view

                            // Maps from data objects to presentation objects.
                            viewAllTeamsVM.ListOfPos = TeamMapper.MapListOfDOsToListOfPOs(smAllTeams);
                            //var teamAbsences = allAbsences.Where(a => a.); Need to retrieve absences by Team
                            // TODO: list of service manager team absences
                            viewAllTeamsVM.ListOfTeamAbsences = AbsenceMapper.MapListOfDOsToListOfPOs(allAbsences);
                            var topEmployee = _TeamBusinessLogic.QueryBestStandingTeamMember(smTeams, allAbsences, allUsers, userPO.RoleID_FK);

                            oResponse = View(viewAllTeamsVM);
                            break;
                        case 3:
                            // TODO: Finish DA call for Team Lead lolololol
                            var getAllTeams = _TeamDataAccess.GetAllTeamsByUserID(userPO.UserID);
                            viewAllTeamsVM.ListOfPos = TeamMapper.MapListOfDOsToListOfPOs(getAllTeams);
                            var topTeamMember = _TeamBusinessLogic.QueryBestStandingTeamMember(smTeams, allAbsences, allUsers, userPO.RoleID_FK);

                            oResponse = View(viewAllTeamsVM);
                            break;
                        default:
                            oResponse = View("Index", "Home");
                            break;
                    }

                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex, "ViewAllTeams", "Maint");
                    viewAllTeamsVM.ErrorMessage = "There was an issure retrieving the view all teams. Please try again. If the problem persists contact your IT department.";
                }
            }
            else
            {
                oResponse = RedirectToAction("Index", "Home");
            }

            return oResponse;
        }

        private void AssociateAdminValues(TeamViewModel viewAllTeamsVM, Tuple<string, decimal> bestStandingTeam, Tuple<string, decimal> bottomStandingTeam, List<IAbsenceDO> absenceDOs, List<Tuple<string, decimal>> teamRanker, IUserPO userPO)
        {
            // Assign values to model for widgets
            viewAllTeamsVM.TopTeam.Team.Name = bestStandingTeam.Item1;
            viewAllTeamsVM.TopTeam.Absence.RunningTotal = bestStandingTeam.Item2;
            viewAllTeamsVM.BottomTeam.Team.Name = bottomStandingTeam.Item1;
            viewAllTeamsVM.BottomTeam.Absence.RunningTotal = bottomStandingTeam.Item2;

            // Map absences from DO to PO for displaying to the user
            viewAllTeamsVM.ListOfTeamAbsences = AbsenceMapper.MapListOfDOsToListOfPOs(absenceDOs);

            foreach (var absence in absenceDOs)
            {
                viewAllTeamsVM.Absences.Add(new SelectListItem() { Text = absence.Name, Value = absence.Name });
            }
            foreach (var item in teamRanker)
            {
                viewAllTeamsVM.TeamRanker.Team.Name = item.Item1;
                viewAllTeamsVM.TeamRanker.Absence.Point = item.Item2;
            }
            viewAllTeamsVM.User.RoleID_FK = userPO.RoleID_FK;
            viewAllTeamsVM.User.Email = userPO.Email;
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        ///<summary>
        /// Retrieves all Teams by a given user(i.e. Service Manager with multiple teams) 
        /// </summary>
        /// TODO: Remove the reference to this as ViewAllTeams has all three roles captured!!!!
        public ActionResult ViewTeamsByUserID(int userID)
        {
            ActionResult oResponse = null;
            var selectedUserTeams = new TeamViewModel();
            var userPO = (IUserPO)Session["UserModel"];

            if (ModelState.IsValid)
            {
                if (userPO.Email != null && userPO.RoleID_FK >= (int)RoleEnum.Administrator && userPO.RoleID_FK <= (int)RoleEnum.Service_Manager)
                {
                    try
                    {
                        // Stores teams of user using their id
                        var allTeams = _TeamDataAccess.GetAllSMTeamsByUserID(userID);
                        // Retrieve lists of absences and users for LINQ
                        var allAbsences = PointsDataAccess.ViewAllAbsences();
                        var allUsers = _UserDataAccess.GetAllUsers();

                        // TODO: Fix LINQ query or create SQL Join
                        //var topEmployee = _TeamBusinessLogic.QueryBestStandingTeamMember(allTeams, allAbsences, allUsers, userPO.RoleID_FK);

                        // Maps Team from data objects to presentation objects
                        selectedUserTeams.ListOfPos = TeamMapper.MapListOfDOsToListOfPOs(allTeams);
                        selectedUserTeams.ListOfPos.FirstOrDefault(team => team.Name != null);

                        oResponse = View(selectedUserTeams);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex, "ViewTeamsByUserID", "Maint");
                        selectedUserTeams.ErrorMessage = "There was an issue retrieving the user's team! Please try again.";

                        oResponse = View(selectedUserTeams);
                    }
                }
                else
                {
                    oResponse = View(selectedUserTeams);
                }
            }
            else
            {
                oResponse = View(selectedUserTeams);
            }

            return oResponse;
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        ///<summary>
        /// Retrieves form for updating team information
        /// </summary>
        public ActionResult UpdateTeamInformation(int teamID)
        {
            ActionResult oResponse = null;
            var userPO = (IUserPO)Session["UserModel"];

            // User is authenticated
            if (userPO.Email != null && userPO.RoleID_FK == (int)RoleEnum.Administrator)
            {
                var teamVM = new TeamViewModel();

                // Retrieve team information with the correct DA call
                //ITeamDO teamDO = _TeamDataAccess.GetTeamNameByID(teamID);

                // Map teamDO from data objects to presentation objects
                //teamVM.Team = TeamMapper.MapTeamDOtoPO(teamDO);   // Uncomment after DA fix

                oResponse = View(teamVM);
            }
            else
            {
                oResponse = RedirectToAction("Index", "Home");
            }

            return oResponse;
        }

        [HttpPost]
        ///<summary>
        /// Updates information for a team
        /// </summary>
        public ActionResult UpdateTeamInformation(TeamViewModel iTeam)
        {
            ActionResult oResponse = null;
            var userPO = (IUserPO)Session["UserModel"];
            iTeam.User.Email = userPO.Email;
            iTeam.User.RoleID_FK = userPO.RoleID_FK;

            // Ensure user is authenticated
            if (userPO.Email != null && userPO.RoleID_FK == (int)RoleEnum.Administrator)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Map team from presentation to data objects
                        ITeamDO lTeamForm = TeamMapper.MapTeamPOtoDO(iTeam.Team);

                        // Passes form to be updated
                        _TeamDataAccess.UpdateTeam(lTeamForm, userPO.UserID);

                        oResponse = RedirectToAction("ViewAllTeams", "Maint");
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex, "UpdateTeamInformation", "Maint");
                        iTeam.ErrorMessage = "There was an issue with updating the selected team. Please try again. If the problem persists contact your IT team.";

                        oResponse = View(iTeam);
                    }
                }
                else
                {
                    oResponse = View(iTeam);
                }
            }

            return oResponse;
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        ///<summary>
        /// Attempts to deactivate team
        /// </summary>
        public ActionResult DeactivateTeam(int teamID)
        {
            ActionResult oResponse = null;
            var userPO = (IUserPO)Session["UserModel"];

            if (userPO.Email != null && userPO.RoleID_FK == (int)RoleEnum.Administrator)
            {
                try
                {
                    _TeamDataAccess.DeactivateTeam(teamID);
                }
                catch (Exception ex)
                {
                    var error = new TeamViewModel();
                    ErrorLogger.LogError(ex, "DeactivateTeam", "Maint");
                    error.ErrorMessage = "There was an issue with archiving the selected team. Please try again. If the problem persists contact your IT team.";
                }
                finally
                {
                    // TODO: Always return to view all - not set in stone can be discussed
                    oResponse = RedirectToAction("ViewAllTeams", "Maint");
                }
            }
            else
            {
                oResponse = RedirectToAction("Index", "Home");
            }

            return oResponse;
        }

        #endregion

        #region AttendanceType

        [HttpGet]
        //[ValidateAntiForgeryToken]
        ///<summary>
        /// Retrieves form for creating a new absence entry
        /// </summary>
        public ActionResult AddAbsenceEntry()
        {
            ActionResult oResponse = null;
            var userPO = (IUserPO)Session["UserModel"];

            if (userPO.Email != null && userPO.RoleID_FK == (int)RoleEnum.Administrator)
            {
                var absenceVM = new AbsenceViewModel();
                var absenceTypes = _AbsenceDataAccess.GetAllAbsenceTypes();
                var absencePOs = AbsenceMapper.MapListOfDOsToListOfPOs(absenceTypes);
                var users = _UserDataAccess.GetAllUsers();
                var userPOs = UserMapper.MapListOfDOsToListOfPOs(users);
                absenceVM.Users = userPOs.ConvertAll(a =>
                {
                    return new SelectListItem()
                    {
                        Text = a.UserID.ToString(),
                        Value = a.FirstName + " " + a.LastName,
                        Selected = false
                    };
                });
                absenceVM.AbsenceTypes = absencePOs.ConvertAll(a =>
                {
                    return new SelectListItem()
                    {
                        Text = a.AbsenceTypeID.ToString(),
                        Value = a.Name,
                        Selected = false
                    };
                });

                oResponse = View(absenceVM);
            }
            else
            {
                // User doesn't have access to create, redirect home
                oResponse = RedirectToAction("Index", "Home");

            }

            return oResponse;
        }

        [HttpPost]
        ///<summary>
        /// Sends the absence form to the database to be added
        /// </summary>
        /// <returns></returns>
        public ActionResult AddAbsenceEntry(AbsenceViewModel iViewModel)
        {
            ActionResult oResponse = null;
            var userPO = (IUserPO)Session["UserModel"];

            // User is authenticated(Admin, Service Manager or Team Lead)
            if (userPO.Email != null && userPO.RoleID_FK < (int)RoleEnum.Service_Desk_Employee && userPO.RoleID_FK >= (int)RoleEnum.Administrator)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var absenceTypes = _AbsenceDataAccess.GetAllAbsenceTypes();
                        var absenceTypePOs = AbsenceMapper.MapListOfDOsToListOfPOs(absenceTypes);

                        // Maps absence PO to DO during creation
                        IAbsenceDO lAbsenceForm = AbsenceMapper.MapAbsencePOtoDO(iViewModel.Absence);
                        var absenceType = absenceTypePOs.Where(at => at.AbsenceTypeID == iViewModel.Absence.AbsenceTypeID);
                        // Passes form to data access to add event to db
                        PointsDataAccess.AddAbsence(lAbsenceForm, userPO.UserID);
                        oResponse = RedirectToAction("ViewAllAbsence", "Maint");
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex, "AddAbsenceEntry", "Maint");
                        iViewModel.ErrorMessage = "Something went wrong when creating the absence to the system. Please try again.";
                    }
                }
                else
                {
                    oResponse = View(iViewModel);
                }
            }
            else
            {
                // User doesn't have privileges to create an absense entry, redirect to home.
                oResponse = RedirectToAction("Index", "Home");
            }

            return oResponse;
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        ///<summary>
        /// Admin view all absences by all employees
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewAllAbsenceEntries()
        {
            ActionResult oResponse = null;
            var userPO = (IUserPO)Session["UserModel"];
            var viewAllAbsenceEntries = new AbsenceViewModel();

            // User can view all absences if Admin
            if (userPO.Email != null && userPO.RoleID_FK == (int)RoleEnum.Administrator)
            {
                try
                {
                    // Calls to retrieve all absences from data access
                    var allAbsences = PointsDataAccess.ViewAllAbsences();
                    var allTeams = _TeamDataAccess.GetAllTeams();

                    // Retrieve widget values
                    var bestStandingTeam = _TeamBusinessLogic.QueryBestStandingTeam(TeamMapper.MapListOfDOsToListOfBOs(allTeams), allAbsences);
                    var bottomStandingTeam = _TeamBusinessLogic.QueryWorstStandingTeam(TeamMapper.MapListOfDOsToListOfBOs(allTeams), allAbsences);
                    var teamRanker = _TeamBusinessLogic.QueryTeamRanker(TeamMapper.MapListOfDOsToListOfBOs(allTeams), allAbsences);
                    foreach (var item in teamRanker)
                    {
                        viewAllAbsenceEntries.TeamRanker.Team.Name = item.Item1;
                        viewAllAbsenceEntries.TeamRanker.Absence.RunningTotal = item.Item2;
                    }

                    AssociateAdminValues(viewAllAbsenceEntries, bestStandingTeam, bottomStandingTeam, allAbsences);

                    viewAllAbsenceEntries.User.RoleID_FK = userPO.RoleID_FK;
                    viewAllAbsenceEntries.User.Email = userPO.Email;

                    oResponse = View(viewAllAbsenceEntries);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex, "ViewAllAbsenceEntries", "Maint");
                    viewAllAbsenceEntries.ErrorMessage = "Something went wrong retrieving the list of absences. Please try again.";

                    oResponse = View(viewAllAbsenceEntries);
                }
            }
            else
            {
                // State was invalid redirect home
                oResponse = RedirectToAction("Index", "Home");
            }

            return oResponse;
        }

        // TODO: Rename and refactor to handle all role types
        private void AssociateAdminValues(AbsenceViewModel viewAllAbsenceEntries, Tuple<string, decimal> bestStandingTeam, Tuple<string, decimal> bottomStandingTeam, List<IAbsenceDO> allAbsences)
        {
            // Assign values to model for widgets
            viewAllAbsenceEntries.TopTeam.Team.Name = bestStandingTeam.Item1;
            viewAllAbsenceEntries.TopTeam.Absence.RunningTotal = bestStandingTeam.Item2;
            viewAllAbsenceEntries.BottomTeam.Team.Name = bottomStandingTeam.Item1;
            viewAllAbsenceEntries.BottomTeam.Absence.RunningTotal = bottomStandingTeam.Item2;

            // Map absences from DO to PO for displaying to the user
            viewAllAbsenceEntries.ListOfPos = AbsenceMapper.MapListOfDOsToListOfPOs(allAbsences);

            //foreach (var absence in allAbsences)
            //{
            //    viewAllAbsenceEntries.Absences.Add(new SelectListItem() { Text = absence.Name, Value = absence.Name });
            //}
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        ///<summary>
        /// Views all absences by for a given team(TL, SM, Admin)
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewAbsencesByTeamID(int teamID)
        {
            ActionResult oResponse = null;
            var selectedTeamAbsences = new AbsenceViewModel();
            var userPO = (IUserPO)Session["UserModel"];

            if (userPO.Email != null && userPO.RoleID_FK <= (int)RoleEnum.Team_Lead && userPO.RoleID_FK >= (int)RoleEnum.Administrator)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Stores list of absences by TeamID
                        var absences = PointsDataAccess.GetAbsencesByTeamID(teamID);
                        var teamName = _TeamDataAccess.GetTeamNameByID(teamID);

                        // Retrieve lists for LINQ queries
                        var allAbsences = PointsDataAccess.ViewAllAbsences();
                        var allTeams = _TeamDataAccess.GetAllTeams();
                        var allUsers = _UserDataAccess.GetAllUsers();
                        var topMemeberBOs = UserMapper.MapListOfDOsToListOfBOs(allUsers);

                        // LINQ Queries
                        var bestStandingTeam = _TeamBusinessLogic.QueryBestStandingTeam(TeamMapper.MapListOfDOsToListOfBOs(allTeams), allAbsences);
                        var bottomStandingTeam = _TeamBusinessLogic.QueryWorstStandingTeam(TeamMapper.MapListOfDOsToListOfBOs(allTeams), allAbsences);
                        var topEmployee = _TeamBusinessLogic.QueryBestStandingEmployee(allTeams, allAbsences, allUsers);
                        var teamRanker = _TeamBusinessLogic.QueryTeamRanker(TeamMapper.MapListOfDOsToListOfBOs(allTeams), allAbsences);
                        MapAdminObjects(selectedTeamAbsences, allTeams, absences);

                        switch (userPO.RoleID_FK)
                        {
                            // Admin
                            case 1:
                                selectedTeamAbsences.ListOfPos = AbsenceMapper.MapListOfDOsToListOfPOs(absences);
                                AssociateAdminValues(selectedTeamAbsences, teamRanker, teamName, bestStandingTeam, bottomStandingTeam, topEmployee);
                                break;
                            // Service Manager
                            case 2:
                                var teamAbsences = AbsenceMapper.MapListOfDOsToListOfPOs(absences);
                                var smTeams = _TeamDataAccess.GetAllSMTeamsByUserID(userPO.UserID);
                                selectedTeamAbsences.SMTeams = TeamMapper.MapListOfDOsToListOfPOs(smTeams);
                                AssociateAdminValues(selectedTeamAbsences, teamRanker, teamName, bestStandingTeam, bottomStandingTeam, topEmployee);
                                //selectedTeamAbsences.ListOfPos = teamAbsences;
                                //selectedTeamAbsences.TopTeam.Team.Name = bestStandingTeam.Item1;
                                //selectedTeamAbsences.TopTeam.Absence.Point = bestStandingTeam.Item2;
                                //selectedTeamAbsences.BottomTeam.Team.Name = bottomStandingTeam.Item1;
                                //selectedTeamAbsences.BottomTeam.Absence.Point = bottomStandingTeam.Item2;
                                //selectedTeamAbsences.TopEmployee.Name = topEmployee.Item1;
                                //selectedTeamAbsences.TopEmployee.Absence.Point = topEmployee.Item2;
                                //MapServiceManagerObjects(selectedTeamAbsences, allTeams, absences);
                                //AssociateServiceManagerObjects(selectedTeamAbsences, topEmployee);

                                oResponse = View(selectedTeamAbsences);
                                break;
                            // Team Lead
                            case 3:
                                var tlAbsences = AbsenceMapper.MapListOfDOsToListOfPOs(absences);
                                selectedTeamAbsences.ListOfPos = tlAbsences;
                                AssociateAdminValues(selectedTeamAbsences, teamRanker, teamName, bestStandingTeam, bottomStandingTeam, topEmployee);
                                //selectedTeamAbsences.TopTeam.Team.Name = bestStandingTeam.Item1;
                                //selectedTeamAbsences.TopTeam.Absence.Point = bestStandingTeam.Item2;
                                //selectedTeamAbsences.BottomTeam.Team.Name = bottomStandingTeam.Item1;
                                //selectedTeamAbsences.BottomTeam.Absence.Point = bottomStandingTeam.Item2;
                                //selectedTeamAbsences.TopEmployee.Name = topEmployee.Item1;
                                //selectedTeamAbsences.TopEmployee.Absence.Point = topEmployee.Item2;

                                oResponse = View(selectedTeamAbsences);
                                break;
                            default:
                                break;
                        }

                        oResponse = View(selectedTeamAbsences);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex, "ViewAbsencesByTeamID", "Maint");
                        selectedTeamAbsences.ErrorMessage = "Something went wrong retrieving the list of absences. Please try again.";
                        oResponse = View(selectedTeamAbsences);
                    }
                }
                else
                {
                    oResponse = View(selectedTeamAbsences);
                }
            }

            return oResponse;
        }

        // TODO: Rename and add switch/case for role decisions
        private void AssociateAdminValues(AbsenceViewModel selectedTeamAbsences, List<Tuple<string, decimal>> teamRanker, ITeamDO teamName,
            Tuple<string, decimal> bestStandingTeam, Tuple<string, decimal> bottomStandingTeam, Tuple<string, decimal> topEmployee)
        {
            foreach (var item in teamRanker)
            {
                selectedTeamAbsences.TeamRanker.Team.Name = item.Item1;
                selectedTeamAbsences.TeamRanker.Absence.Point = item.Item2;
            }
            selectedTeamAbsences.Team.Name = teamName.Name;
            selectedTeamAbsences.TopTeam.Team.Name = bestStandingTeam.Item1;
            selectedTeamAbsences.TopTeam.Absence.RunningTotal = bestStandingTeam.Item2;
            selectedTeamAbsences.BottomTeam.Team.Name = bottomStandingTeam.Item1;
            selectedTeamAbsences.BottomTeam.Absence.RunningTotal = bottomStandingTeam.Item2;
            selectedTeamAbsences.TopEmployee.User.Employee = topEmployee.Item1;
            selectedTeamAbsences.TopEmployee.Absence.Point = topEmployee.Item2;
        }

        private void MapAdminObjects(AbsenceViewModel selectedTeamAbsences, List<ITeamDO> allTeams, List<IAbsenceDO> absences)
        {
            // Map values to correct properties for view
            foreach (IAbsenceDO absence in absences)
            {
                selectedTeamAbsences.Absence = AbsenceMapper.MapAbsenceDOtoPO(absence);
                selectedTeamAbsences.ListOfPos.Add(selectedTeamAbsences.Absence);
            }
            foreach (ITeamDO team in allTeams)
            {
                selectedTeamAbsences.Team = TeamMapper.MapTeamDOtoPO(team);

            }
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        ///<summary>
        /// Views all absences for all teams under a Service Manager -- Check to see if this already exists
        /// </summary>
        public ActionResult ViewAllAbsencesForSMTeams(int userID)
        {
            ActionResult oResponse = null;
            var selectedTeamAbsences = new AbsenceViewModel();
            var userPO = (IUserPO)Session["UserModel"];

            if (userPO.Email != null && userPO.RoleID_FK <= (int)RoleEnum.Service_Manager && userPO.RoleID_FK >= (int)RoleEnum.Administrator)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var absences = new List<IAbsenceDO>();
                        // Stores list of absences by UserID
                        absences = PointsDataAccess.ViewAbsencesByUserID(userID);
                        InitializeViewData(selectedTeamAbsences, (UserPO)userPO, absences);

                        oResponse = View(selectedTeamAbsences);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex, "ViewAbsenceByTeamID", "Maint");
                        selectedTeamAbsences.ErrorMessage = "Something went wrong retrieving the list of absences. Please try again.";
                        oResponse = View(selectedTeamAbsences);
                    }
                }
                else
                {
                    oResponse = View(selectedTeamAbsences);
                }
            }

            return oResponse;
        }

        private void InitializeViewData(AbsenceViewModel selectedTeamAbsences, UserPO userPO, List<IAbsenceDO> absences)
        {
            var smName = new StringBuilder(selectedTeamAbsences.User.FirstName, 25);
            ViewBag.Name = smName.Append(" " + selectedTeamAbsences.User.LastName);

            // Maps list of absences from DO to PO
            foreach (IAbsenceDO absence in absences)
            {
                selectedTeamAbsences.Absence = AbsenceMapper.MapAbsenceDOtoPO(absence);
            }
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        ///<summary>
        /// Retrieves form for the given absence selected
        /// </summary>
        public ActionResult UpdateAbsenceEntry(IAbsenceDO iAbsence, int pointBankID)
        {
            ActionResult oResponse = null;
            var userPO = (IUserPO)Session["UserModel"];

            if (userPO.Email != null && userPO.RoleID_FK >= (int)RoleEnum.Administrator && userPO.RoleID_FK <= (int)RoleEnum.Team_Lead)
            {
                var absenceVM = new AbsenceViewModel();

                // Retrieve selected absence
                var absenceDO = PointsDataAccess.GetAbsenceByID(pointBankID);
                ViewBag.Name = "Modify Employee Absence";

                // Maps absence DO to PO
                absenceVM.Absence = AbsenceMapper.MapAbsenceDOtoPO(absenceDO);

                oResponse = View(absenceVM);
            }
            else
            {
                // User doesn't have priveleges redirect home
                oResponse = RedirectToAction("Index", "Home");
            }

            return oResponse;
        }

        [HttpPost]
        ///<summary>
        /// Sends the form to the database to be modified for an absence
        /// </summary>
        public ActionResult UpdateAbsenceEntry(AbsenceViewModel iViewModel, int userID)
        {
            ActionResult oResponse = null;
            var userPO = (IUserPO)Session["UserModel"];

            // Ensure user has priveleges
            if (userPO.Email != null && userPO.RoleID_FK >= (int)RoleEnum.Administrator && userPO.RoleID_FK <= (int)RoleEnum.Team_Lead)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Map absence from PO to DO
                        IAbsenceDO lAbsenceForm = AbsenceMapper.MapAbsencePOtoDO(iViewModel.Absence);

                        // Passes form to data access to update the database
                        PointsDataAccess.UpdateAbsenceInformation(lAbsenceForm, userPO.UserID);

                        // Determine redirect based off role
                        switch (userPO.RoleID_FK)
                        {
                            case 1:
                                oResponse = RedirectToAction("ViewAllAbsenceEntries", "Maint");
                                break;
                            case 2:
                                oResponse = RedirectToAction("ViewAllAbsencesForSMTeam", "Maint");
                                break;
                            case 3:
                                oResponse = RedirectToAction("ViewAbsencesByTeamID", "Maint");
                                break;
                            default:
                                oResponse = RedirectToAction("Index", "Home");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex, "UpdateAbsenceEntry", "Maint");
                        iViewModel.ErrorMessage = "Something went wrong updating the absence entry. Please try again.";
                        oResponse = View(iViewModel);
                    }
                }
                else
                {
                    oResponse = View(iViewModel);
                }
            }
            else
            {
                oResponse = RedirectToAction("Index", "Home");
            }

            return oResponse;
        }

        // TODO: Create method for reusability for navigation based of Role Enum

        #endregion

        #region TestDACalls

        [HttpPost]
        [ValidateAntiForgeryToken]
        ///<summary>
        /// Views all teams(admin)
        /// </summary>
        public ActionResult TestViews()
        {
            ActionResult oResponse = null;
            var ViewAllTeamsVM = new TeamViewModel();
            var userPO = (IUserPO)Session["UserModel"];

            // Ensures authenticated
            if (userPO.Email != null && userPO.RoleID_FK == (int)RoleEnum.Administrator)
            {
                try
                {
                    // Test for retrieves
                    var allTeams = _TeamDataAccess.GetAllTeams();
                    var allSMTeams = _TeamDataAccess.GetAllSMTeams();
                    var team = _TeamDataAccess.GetTeamNameByID(5);
                    var allSMTeamAbsences = _TeamDataAccess.GetAllSMTeamsByUserID(8);
                    var viewUserAbsence = PointsDataAccess.GetAbsenceByID(4);
                    var viewAllAbsences = PointsDataAccess.ViewAllAbsences();
                    var teamAbsences = PointsDataAccess.GetAbsencesByTeamID(5);
                    var viewUserAbsences = PointsDataAccess.ViewAbsencesByUserID(8);

                    // Maps from data objects to presentation objects.
                    ViewAllTeamsVM.ListOfPos = TeamMapper.MapListOfDOsToListOfPOs(allTeams);

                    oResponse = View(ViewAllTeamsVM);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex, "ViewAllTeams", "Maint");
                    ViewAllTeamsVM.ErrorMessage = ""; // TODO: Add meaningful front end message
                }
            }
            else
            {
                oResponse = RedirectToAction("Index", "Home");
            }

            return oResponse;
        }
        #endregion
    }
}