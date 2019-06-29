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
        ///<summary>
        /// Gets form for creating a new team
        /// </summary>
        public ActionResult AddTeam()
        {
            ActionResult oResponse = null;
            var userPO = (IUserPO)Session["UserModel"];

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
        ///<summary>
        /// Views all teams(admin)
        /// </summary>
        public ActionResult ViewAllTeams()
        {
            ActionResult oResponse = null;
            var ViewAllTeamsVM = new TeamViewModel();
            var userPO = (IUserPO)Session["UserModel"];

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
                if (userPO.Email != null && userPO.RoleID_FK > 0 && userPO.RoleID_FK <= 2)
                {
                    try
                    {
                        // Stores teams of user using their id
                        List<ITeamDO> iTeams = _TeamDataAccess.GetAllTeamsByID(userID);

                        // Maps Team from data objects to presentation objects
                        selectedUserTeams.ListOfTeamPO = TeamMapper.MapListOfDOsToListOfPOs(iTeams);

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
                if (userPO.Email != null && userPO.RoleID_FK > 0 && userPO.RoleID_FK <= 2)
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
        // TODO: Uncomment when ready to touch OWASP
        //[Authorize][ValidateAntiForgeryToken]
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
        ///<summary>
        /// Attempts to deactivate team
        /// </summary>
        public ActionResult DeactivateTeam(int teamID)
        {
            ActionResult oResponse = null;
            var userPO = (IUserPO)Session["UserModel"];

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

        #region AttendanceType

        [HttpGet]
        ///<summary>
        /// Retrieves form for creating a new absence entry
        /// </summary>
        public ActionResult AddAbsenceEntry()
        {
            ActionResult oResponse = null;
            var userPO = (IUserPO)Session["UserModel"];

            if (userPO.Email != null && userPO.RoleID_FK == 1)
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
        // TODO: Uncomment when ready to touch OWASP
        //[Authorize][ValidateAntiForgeryToken]
        ///<summary>
        /// Sends the absence form to the database to be added
        /// </summary>
        /// <returns></returns>
        public ActionResult AddAbsenceEntry(AbsenceViewModel iViewModel)
        {
            ActionResult oResponse = null;
            var userPO = (IUserPO)Session["UserModel"];

            // User is authenticated(Admin, Service Manager or Team Lead)
            if(userPO.Email != null && userPO.RoleID_FK < 4 && userPO.RoleID_FK > 0)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Maps absence PO to DO during creation
                        IAbsenceDO lAbsenceForm = AbsenceMapper.MapAbsencePOtoDO(iViewModel.Absence);

                        // Passes form to data access to add event to db
                        _AbsenceDataAccess.CreateAbsence(lAbsenceForm, userPO.UserID);
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
            var ViewAllAbsenceEntries = new AbsenceViewModel();

            // User can view all absences if Admin
            if(userPO.Email != null && userPO.RoleID_FK == 1)
            {
                try
                {
                    // Calls to retrieve all absences from data access
                    List<IAbsenceDO> allAbsences = _AbsenceDataAccess.GetAbsenceTypes();

                    // Map absences from DO to PO for displaying to the user
                    ViewAllAbsenceEntries.ListOfAbsencePO = AbsenceMapper.MapListOfDOsToListOfPOs(allAbsences);

                    oResponse = View(ViewAllAbsenceEntries);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex, "ViewAllAbsenceEntries", "Maint");
                    ViewAllAbsenceEntries.ErrorMessage = "Something went wrong retrieving the list of absences. Please try again.";

                    oResponse = View(ViewAllAbsenceEntries);
                }
            }
            else
            {
                // State was invalid redirect home
                oResponse = RedirectToAction("Index", "Home");
            }

            return oResponse;
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

            if (userPO.Email != null && userPO.RoleID_FK <= 3 && userPO.RoleID_FK > 0)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Stores list of absences by TeamID
                        var absences = new List<IAbsenceDO>();
                        absences = _AbsenceDataAccess.GetAbsenceTypesByTeamID(teamID);
                        var teamName = _TeamDataAccess.GetTeamByID(teamID);
                        selectedTeamAbsences.Team.Name = teamName.Name;

                        // Maps list of absences from DO to PO
                        foreach(IAbsenceDO absence in absences)
                        {
                            selectedTeamAbsences.Absence = AbsenceMapper.MapAbsenceDOtoPO(absence);
                            selectedTeamAbsences.ListOfAbsencePO.Add(selectedTeamAbsences.Absence);
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

        [HttpGet]
        ///<summary>
        /// Views all absences for all teams under a Service Manager
        /// </summary>
        public ActionResult ViewAllAbsencesForSMTeam(int teamID)
        {
            ActionResult oResponse = null;
            var selectedTeamAbsences = new AbsenceViewModel();
            var userPO = (IUserPO)Session["UserModel"];

            if (userPO.Email != null && userPO.RoleID_FK <= 3 && userPO.RoleID_FK > 0)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var absences = new List<IAbsenceDO>();
                        // Stores list of absences by TeamID
                        absences = _AbsenceDataAccess.GetAbsenceTypesForSMByTeamID(userPO.UserID, teamID);
                        InitializeViewData(selectedTeamAbsences, (UserPO)userPO);

                        // Maps list of absences from DO to PO
                        foreach (IAbsenceDO absence in absences)
                        {
                            selectedTeamAbsences.Absence = AbsenceMapper.MapAbsenceDOtoPO(absence);
                        }

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

        private void InitializeViewData(AbsenceViewModel selectedTeamAbsences, UserPO userPO)
        {               
            var smName = new StringBuilder(selectedTeamAbsences.User.FirstName, 25);
            ViewBag.Name = smName.Append(" " + selectedTeamAbsences.User.LastName);
        }

        [HttpGet]
        ///<summary>
        /// Retrieves form for the given absence selected
        /// </summary>
        public ActionResult UpdateAbsenceEntry(IAbsenceDO iAbsence,int absenceID)
        {
            ActionResult oResponse = null;
            var userPO = (IUserPO)Session["UserModel"];

            if(userPO.Email != null && userPO.RoleID_FK > 0 && userPO.RoleID_FK <= 3)
            {
                var absenceVM = new AbsenceViewModel();

                // Retrieve selected absence
                IAbsenceDO absenceDO = _AbsenceDataAccess.UpdateAbsenceType(iAbsence, absenceID);
                ViewBag.Name = "Modify Employee Absence";

                // Maps absence DO to PO
                absenceVM.Absence = AbsenceMapper.MapAbsenceDOtoPO(absenceDO);

                oResponse = View(absenceVM);
            }
            else
            {
                // User doesn't have priveleges redirect home
                oResponse = RedirectToAction("Index","Home");
            }

            return oResponse;
        }

        [HttpPost]
        // TODO: Uncomment when ready to touch OWASP
        //[Authorize][ValidateAntiForgeryToken]
        ///<summary>
        /// Sends the form to the database to be modified for an absence
        /// </summary>
        public ActionResult UpdateAbsenceEntry(AbsenceViewModel iViewModel, int userID)
        {
            ActionResult oResponse = null;
            var userPO = (IUserPO)Session["UserModel"];

            // Ensure user has priveleges
            if (userPO.Email != null && userPO.RoleID_FK > 0 && userPO.RoleID_FK <= 3)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Map absence from PO to DO
                        IAbsenceDO lAbsenceForm = AbsenceMapper.MapAbsencePOtoDO(iViewModel.Absence);

                        // Passes form to data access to update the database
                        _AbsenceDataAccess.UpdateAbsenceType(lAbsenceForm, userID);

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

        #endregion
    }
}