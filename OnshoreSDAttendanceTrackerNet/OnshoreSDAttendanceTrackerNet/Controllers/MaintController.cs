using OnshoreSDAttendanceTrackerErrorLogger;
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
        private AbsenceDataAccess _AbsenceDataAccess;
        private UserDataAccess _UserDataAccess;
        private TeamBusinessLogic _TeamBusinessLogic;

        public MaintController()
        {
            string teamConn = ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString;
            string absenceConn = ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString;
            _TeamDataAccess = new TeamDataAccess();
            _AbsenceDataAccess = new AbsenceDataAccess();
            _UserDataAccess = new UserDataAccess();
            _TeamBusinessLogic = new TeamBusinessLogic();
        }

        #region Team

        [HttpGet]
        ///<summary>
        /// Gets form for creating a new team
        /// </summary>
        public ActionResult AddTeam()
        {
            ActionResult oResponse = null;
            var userPO = (IUserPO)Session["UserModel"];

            // Ensure user is authenticated
            // TODO: Implement session checks after session has been handled
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
        // TODO: Uncomment when ready to touch OWASP
        // [Authorize][ValidateAntiForgeryToken]
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
        ///<summary>
        /// Views all teams(admin)
        /// </summary>
        public ActionResult ViewAllTeams()
        {
            ActionResult oResponse = null;
            var ViewAllTeamsVM = new TeamViewModel();
            var userPO = (IUserPO)Session["UserModel"];

            // Ensures authenticated
            if (userPO.Email != null && userPO.RoleID_FK == (int)RoleEnum.Administrator)
            {
                try
                {
                    // Calls GetAllTeams from DAL and stores in allTeams
                    List<ITeamDO> allTeams = _TeamDataAccess.GetAllTeams();

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

        [HttpGet]
        ///<summary>
        /// Retrieves all Teams by a given user(i.e. Service Manager with multiple teams) 
        /// </summary>
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
                        var allTeams = _TeamDataAccess.GetAllSMTeamAbsencesByUserID(userID);

                        // Retrieve lists of absences and users for LINQ
                        var allAbsences = PointsDataAccess.ViewAllAbsences();
                        var allUsers = _UserDataAccess.GetAllUsers();

                        // TODO: Fix LINQ query or create SQL Join
                        var topEmployee = _TeamBusinessLogic.QueryBestStandingTeamMember(allTeams, allAbsences, allUsers, userPO.RoleID_FK);

                        // Maps Team from data objects to presentation objects
                        selectedUserTeams.ListOfPos = TeamMapper.MapListOfDOsToListOfPOs(allTeams);

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
        ///<summary>
        /// Retrieves all users for a given team
        /// </summary>
        public ActionResult ViewUsersByTeamID(int teamID)
        {
            ActionResult oResponse = null;
            var selectedUsers = new UserViewModel();
            var userPO = (IUserPO)Session["UserModel"];

            if (ModelState.IsValid)
            {
                if (userPO.Email != null && userPO.RoleID_FK >= (int)RoleEnum.Administrator && userPO.RoleID_FK <= (int)RoleEnum.Service_Manager)
                {
                    try
                    {
                        // Stores employees for a team
                        List<IUserDO> iUsers = _TeamDataAccess.ViewUsersByTeamID(teamID);

                        // Map iUsers from data objects to presentation objects
                        selectedUsers.ListOfUserPO = UserMapper.MapListOfDOsToListOfPOs(iUsers);

                        oResponse = View(selectedUsers);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex, "ViewUserByTeamID", "Maint");
                        selectedUsers.ErrorMessage = "Failed to load users. Please try again. If the issue persists reach out to IT.";

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
                        iTeam.ErrorMessage = "There was an issue with updating the selected team. Please try again. If the problem persists contact your IT team."; // TODO Add meaningful message for user

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
                    error.ErrorMessage = "There was an issue with archiving the selected team. Please try again. If the problem persists contact your IT team."; // TODO: Add meaningful message to user
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
                        // Maps absence PO to DO during creation
                        IAbsenceDO lAbsenceForm = AbsenceMapper.MapAbsencePOtoDO(iViewModel.Absence);

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

                    AssociateAdminValues(viewAllAbsenceEntries, bestStandingTeam, bottomStandingTeam, allAbsences);

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

            foreach(var absence in allAbsences)
            {
                viewAllAbsenceEntries.Absences.Add(new SelectListItem() { Text = absence.Name, Value = absence.Name});
            }
        }

        [HttpGet]
        ///<summary>
        /// Views all absences by for a given team(TL, SM, Admin)
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewAbsencesByTeamID(int teamID)
        {
            ActionResult oResponse = null;
            var selectedTeamAbsences = new AbsenceViewModel();
            var userPO = (IUserPO)Session["UserModel"];
            // TODO: Query name of team based off teamID parameter.

            if (userPO.Email != null && userPO.RoleID_FK <= (int)RoleEnum.Team_Lead && userPO.RoleID_FK >= (int)RoleEnum.Administrator)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Stores list of absences by TeamID
                        var absences = _AbsenceDataAccess.GetAbsenceTypesByTeamID(teamID);
                        var teamName = _TeamDataAccess.GetTeamNameByID(teamID);

                        // Retrieve lists for LINQ queries
                        var allAbsences = PointsDataAccess.ViewAllAbsences();
                        var allTeams = _TeamDataAccess.GetAllTeams();
                        var allUsers = _UserDataAccess.GetAllUsers();
                        var topMemeberBOs = UserMapper.MapListOfDOsToListOfBOs(allUsers);

                        switch (userPO.RoleID_FK)
                        {
                            // Admin
                            case 1:
                                var bestStandingTeam = _TeamBusinessLogic.QueryBestStandingTeam(TeamMapper.MapListOfDOsToListOfBOs(allTeams), allAbsences);
                                var bottomStandingTeam = _TeamBusinessLogic.QueryWorstStandingTeam(TeamMapper.MapListOfDOsToListOfBOs(allTeams), allAbsences);
                                var topEmployee = _TeamBusinessLogic.QueryBestStandingEmployee(allTeams, allAbsences, allUsers);
                                var teamRanker = _TeamBusinessLogic.QueryTeamRanker(TeamMapper.MapListOfDOsToListOfBOs(allTeams), allAbsences);

                                // TODO: Switch Case to determine how to associate the values using session role id
                                MapAdminObjects(selectedTeamAbsences, allTeams, absences);
                                AssociateAdminValues(selectedTeamAbsences, teamRanker, teamName, bestStandingTeam, bottomStandingTeam, topEmployee);
                                break;
                            // Service Manager
                            case 2:
                                //MapServiceManagerObjects(selectedTeamAbsences, allTeams, absences);
                                //AssociateServiceManagerObjects(selectedTeamAbsences, topEmployee);
                                break;
                            // Team Lead
                            case 3:
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
            ViewBag.Employee = topEmployee.Item1;
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
        ///<summary>
        /// Views all absences for all teams under a Service Manager -- Check to see if this already exists
        /// </summary>
        public ActionResult ViewAllAbsencesForSMTeams(int userID)
        {
            ActionResult oResponse = null;
            var selectedTeamAbsences = new AbsenceViewModel();
            var userPO = (IUserPO)Session["UserModel"];

            if (userPO.Email != null && userPO.RoleID_FK <= (int)RoleEnum.Service_Manager && userPO.RoleID_FK >=(int)RoleEnum.Administrator)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var absences = new List<IAbsenceDO>();
                        // Stores list of absences by UserID
                        absences = _AbsenceDataAccess.GetAbsencesAssociatedWithUserID(userID);
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
                var absenceDO = _AbsenceDataAccess.GetAbsenceByID(pointBankID);
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
    }
}