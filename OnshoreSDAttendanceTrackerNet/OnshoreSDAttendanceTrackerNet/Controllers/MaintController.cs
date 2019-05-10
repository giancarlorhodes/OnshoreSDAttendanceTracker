using OnshoreSDAttendanceTrackerErrorLogger;
using OnshoreSDAttendanceTrackerNet.Mapper;
using OnshoreSDAttendanceTrackerNet.Models;
using OnshoreSDAttendanceTrackerNetDAL;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace OnshoreSDAttendanceTrackerNet.Controllers
{
    public class MaintController : Controller
    {
        private TeamDataAccess _TeamDataAccess;
        private AbsenceDataAccess _AbsenceDataAccess;

        public MaintController()
        {
            string teamConn = ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString;
            string absenceConn = ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString;
            _TeamDataAccess = new TeamDataAccess();
            _AbsenceDataAccess = new AbsenceDataAccess();
        }

        #region Team

        [HttpGet]
        public ActionResult AddTeam()
        {
            ActionResult oResponse = null;
            var userPO = (UserPO)Session["UserModel"];

            // Ensure user is authenticated
            // TODO: Implement session checks after session has been handled
            if (userPO != null && userPO.RoleID_FK == 1)
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

        // [Authorize]
        // [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddTeam(TeamViewModel iViewModel)
        {
            ActionResult oResponse = null;
            var userPO = (UserPO)Session["UserModel"];

            // Ensure user is authenticated
            if (userPO.Email != null && userPO.RoleID_FK == 1)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Map Team properties from presentation to data objects
                        ITeamDO lTeamForm = TeamMapper.MapTeamPOtoDO(iViewModel.Team);

                        // Passes form to AddTeam method
                        _TeamDataAccess.CreateNewTeam(lTeamForm, lTeamForm.TeamID);
                        oResponse = RedirectToAction("ViewAllTeams", "Maint");
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex, "AddTeam", "Maint");
                        iViewModel.ErrorMessage = ""; // TODO: Add meaningful message

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
        // Retrieves all teams
        public ActionResult ViewAllTeams()
        {
            ActionResult oResponse = null;
            var ViewAllTeamsVM = new TeamViewModel();
            var userPO = (UserPO)Session["UserModel"];

            // Ensures authenticated
            if (userPO.Email != null && userPO.RoleID_FK == 1)
            {
                try
                {
                    // Calls GetAllTeams from DAL and stores in allTeams
                    List<ITeamDO> allTeams = _TeamDataAccess.GetAllTeams();

                    // Maps from data objects to presentation objects.
                    ViewAllTeamsVM.ListOfTeamPO = TeamMapper.MapListOfDOsToListOfPOs(allTeams);

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

        [HttpGet]
        // Retrieves all Teams by a given user(i.e. Service Manager with multiple teams)
        public ActionResult ViewTeamsByUserID(int userID)
        {
            ActionResult oResponse = null;
            var selectedUserTeams = new TeamViewModel();
            var userPO = (UserPO)Session["UserModel"];

            if (ModelState.IsValid)
            {
                if (userPO.Email != null && userPO.RoleID_FK > 0 && userPO.RoleID_FK <= 2)
                {
                    try
                    {
                        // Stores teams of user using their id
                        ITeamDO iTeam = _TeamDataAccess.GetAllTeamsByID(userID);

                        // Maps Team from data objects to presentation objects
                        selectedUserTeams.Team = TeamMapper.MapTeamDOtoPO(iTeam);

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
        // Retrieves all users for a given team
        public ActionResult ViewUserByTeamID(int teamID)
        {
            ActionResult oResponse = null;
            var selectedUsers = new TeamViewModel();
            var userPO = (UserPO)Session["UserModel"];

            if (ModelState.IsValid)
            {
                if (userPO.Email != null && userPO.RoleID_FK < 0 && userPO.RoleID_FK <= 2)
                {


                    try
                    {
                        // Stores users for a team
                        ITeamDO iTeam = _TeamDataAccess.ViewUsersByTeamID(teamID);

                        // Map iTeam from data objects to presentation objects
                        selectedUsers.Team = TeamMapper.MapTeamDOtoPO(iTeam);

                        oResponse = View(selectedUsers);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex, "ViewUserByTeamID", "Maint");
                        selectedUsers.ErrorMessage = ""; // TODO: Add meaningful message to the user

                        oResponse = View(selectedUsers);
                    }
                }
                else
                {
                    oResponse = View(selectedUsers);
                }
            }
            else
            {
                oResponse = View(selectedUsers);
            }

            return oResponse;
        }

        [HttpGet]
        // Retrieves form for updating team information
        public ActionResult UpdateTeamInformation(int teamID)
        {
            ActionResult oResponse = null;
            var userPO = (UserPO)Session["UserModel"];

            // User is authenticated
            if (userPO.Email != null && userPO.RoleID_FK == 1)
            {
                var teamVM = new TeamViewModel();

                // Retrieve team information
                ITeamDO teamDO = _TeamDataAccess.GetTeamByID(teamID);

                // Map teamDO from data objects to presentation objects
                teamVM.Team = TeamMapper.MapTeamDOtoPO(teamDO);

                oResponse = View(teamVM);
            }
            else
            {
                oResponse = RedirectToAction("Index", "Home");
            }

            return oResponse;
        }

        [HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        // Updates information for a team
        public ActionResult UpdateTeamInformation(TeamViewModel iTeam)
        {
            ActionResult oResponse = null;
            var userPO = (UserPO)Session["UserModel"];

            // Ensure user is authenticated
            if (userPO.Email != null && userPO.RoleID_FK == 1)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Map team from presentation to data objects
                        ITeamDO lTeamForm = TeamMapper.MapTeamPOtoDO(iTeam.Team);

                        // Passes form to be updated
                        _TeamDataAccess.UpdateTeam(lTeamForm, lTeamForm.TeamID);

                        oResponse = RedirectToAction("ViewAllTeams", "Maint");
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex, "UpdateTeamInformation", "Maint");
                        iTeam.ErrorMessage = ""; // TODO Add meaningful message for user

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
        // Attempts to deactivate team
        public ActionResult DeactivateTeam(int teamID)
        {
            ActionResult oResponse = null;
            var userPO = (UserPO)Session["UserModel"];

            if (userPO.Email != null && userPO.RoleID_FK == 1)
            {
                try
                {
                    _TeamDataAccess.DeactivateTeam(teamID);
                }
                catch (Exception ex)
                {
                    var error = new TeamViewModel();
                    ErrorLogger.LogError(ex, "DeactivateTeam", "Maint");
                    error.ErrorMessage = ""; // TODO: Add meaningful message to user
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
    }
}