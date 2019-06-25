namespace OnshoreSDAttendanceTrackerNet.Controllers
{
    using OnshoreSDAttendanceTrackerErrorLogger;
    using OnshoreSDAttendanceTrackerNet.Common;
    using OnshoreSDAttendanceTrackerNet.Models;
    using OnshoreSDAttendanceTrackerNetBLL;
    using OnshoreSDAttendanceTrackerNetBLL.Interfaces;
    using OnshoreSDAttendanceTrackerNetBLL.Models;
    using OnshoreSDAttendanceTrackerNetDAL;
    using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
    using OnshoreSDAttendanceTrackerNetDAL.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Threading;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Mvc;
    using System.Web.Security;
    using global::AutoMapper;
    using OnshoreSDAttendanceTrackerNet.Interfaces;


    /// <summary>
    /// Company: Onshore Outsourcing. https://www.onshoreoutsourcing.com/
    /// Author: Giancarlo Rhodes, Date: 5/18/2019
    /// Description: We could use this controller but mostly we need to use this to test
    /// how to get role and authetication, authorization logic
    /// The only anonymous request possible to the site should only be to login method.
    /// After that the login response should determin identity and role of the user and 
    /// save it to session.
    /// Need use form authetication
    /// https://www.youtube.com/watch?v=OOk-HF0bB_g
    /// </summary>
    public class AccountController : Controller
    {
        UserCredentialsDataAccess _ucda = new UserCredentialsDataAccess();
        UserDataAccess _uda = new UserDataAccess();
        TeamDataAccess _tda = new TeamDataAccess();
        UserBusinessLogic _userBLL = new UserBusinessLogic();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(UserLoginPO userPO)
        {
            ActionResult oResponse = null;
            // db call

            IUserLoginDO _dbUser = new UserLoginDO();
            IUserLoginBO _userBO = new UserLoginBO();
            IUserLoginPO _iUserPO = new UserLoginPO();
            UserViewModel userVM = new UserViewModel();

            _userBO.Email = userPO.Email;
            _userBO.Password = userPO.Password;



            if ((_userBO = _userBLL.CheckUserLogin(_userBO)) != null)
            {
                
                _iUserPO = Mapper.Map<IUserLoginBO, IUserLoginPO>(_userBO);
               
                FormsAuthentication.SetAuthCookie(_userBO.Email, false);


                userVM.User.UserID = _iUserPO.UserID;
                userVM.User.Email = _iUserPO.Email;
                userVM.User.RoleID_FK = _iUserPO.RoleID_FK;
                userVM.User.RoleName = _iUserPO.RoleNameLong;
                userVM.User.FirstName = _iUserPO.FirstName;
                userVM.User.LastName = _iUserPO.LastName;
                Session["UserModel"] = userVM.User;

                oResponse = View("ViewUserByUserID", userVM);
            }
            else
            {
                oResponse = RedirectToAction("Shared", "Error");
            }

            return oResponse;
        }



        //[HttpGet]
        /////<summary>
        ///// Gets form for creating a new team
        //first, last, email, team, role (Admin, SM, TL)
        ///// </summary>
        //public ActionResult AddTeam()
        //{
        //    ActionResult oResponse = null;
        //    var userPO = (UserPO)Session["UserModel"];

        //    // Ensure user is authenticated
        //    // TODO: Implement session checks after session has been handled
        //    if (userPO != null && userPO.RoleID_FK == 1)
        //    {
        //        var teamVM = new TeamViewModel();

        //        oResponse = View(teamVM);
        //    }
        //    else
        //    {
        //        // User doesn't have access, redirect
        //        oResponse = RedirectToAction("Index", "Home");
        //    }

        //    return oResponse;
        //}

        //[HttpPost]
        //// TODO: Uncomment when ready to touch OWASP
        //// [Authorize][ValidateAntiForgeryToken]
        /////<summary>
        ///// Sends request to database for creating a new team
        ///// </summary>
        //public ActionResult AddTeam(TeamViewModel iViewModel)
        //{
        //    ActionResult oResponse = null;
        //    var userPO = (UserPO)Session["UserModel"];

        //    // Ensure user is authenticated
        //    if (userPO.Email != null && userPO.RoleID_FK == 1)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                // Map Team properties from presentation to data objects
        //                ITeamDO lTeamForm = TeamMapper.MapTeamPOtoDO(iViewModel.Team);

        //                // Passes form to AddTeam method
        //                _TeamDataAccess.CreateNewTeam(lTeamForm, lTeamForm.TeamID);
        //                oResponse = RedirectToAction("ViewAllTeams", "Maint");
        //            }
        //            catch (Exception ex)
        //            {
        //                ErrorLogger.LogError(ex, "AddTeam", "Maint");
        //                iViewModel.ErrorMessage = ""; // TODO: Add meaningful message

        //                oResponse = View(iViewModel);
        //            }
        //        }
        //        else
        //        {
        //            oResponse = View(iViewModel);
        //        }
        //    }
        //    else
        //    {
        //        // User doesn't have access
        //        oResponse = RedirectToAction("Index", "Home");
        //    }

        //    return oResponse;
        //}






        [HttpGet]
        ///<summary>
        /// Retrieves user by userID
        /// </summary>
        public ActionResult ViewUserByUserID(UserViewModel loginUser)
        {
            ActionResult oResponse = null;
            UserViewModel selectedUser = new UserViewModel();
            IUserPO userPO = loginUser.User;

            if (ModelState.IsValid)
            {
                if (userPO.Email != null && userPO.RoleID_FK < 0 && userPO.RoleID_FK <= 2)
                {
                    try
                    {
                        //stores data access user
                        IUserDO userDO = _uda.GetUserByID(userPO.UserID);

                        // Map userDO from DO to PO
                        userPO = Mapper.Map<IUserDO, IUserPO>(userDO);
                      
                        oResponse = View(userPO);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex, "ViewUserByUserID", "Account");
                        selectedUser.ErrorMessage = "Cannot view user"; // TODO: Add meaningful message to the user

                        oResponse = View(selectedUser);
                    }
                }
                else
                {
                    oResponse = View(selectedUser);
                }
            }
            else
            {
                oResponse = View(selectedUser);
            }

            return oResponse;
        }




        [HttpPost]

        //[Authorize][ValidateAntiForgeryToken]
        ///<summary>
        /// Updates information for a User
        /// first,last, email, team, role  (Admin, SM,TL)
        /// </summary>
        public ActionResult UpdateUserInfo(UserViewModel userToUpdateVM)
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
                        // Map user from presentation to data objects
                        UserDO userUpdateDO = Mapper.Map<UserPO, UserDO>(userToUpdateVM.User);

                        // Passes form to be updated
                        _uda.UpdateUser(userUpdateDO);

                        oResponse = View("ViewUserByUserID", userToUpdateVM.User.UserID);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex, "UpdateUserInfo", "Account");
                        userToUpdateVM.ErrorMessage = "Cannot update user info"; // TODO Add meaningful message for user

                        oResponse = View(userToUpdateVM);
                    }
                }
                else
                {
                    oResponse = View(userToUpdateVM);
                }
            }

            return oResponse;
        }






        //[HttpGet]
        /////<summary>
        ///// Views all users by team
        ///// </summary>
        //public ActionResult ViewAllUsersByTeam()
        //{
        //    ActionResult oResponse = null;
        //    var ViewAllUsersVM = new UserViewModel();
        //    var userPO = (UserPO)Session["UserModel"];

        //    // Ensures authenticated
        //    if (userPO.Email != null && userPO.RoleID_FK == 1)
        //    {
        //        try
        //        {
        //            // Calls GetAllTeams from DAL and stores in allTeams
        //            List<ITeamDO> allTeams = _tda.GetAllTeams();

        //            // Maps from data objects to presentation objects.
        //            ViewAllTeamsVM.ListOfTeamPO = TeamMapper.MapListOfDOsToListOfPOs(allTeams);

        //            oResponse = View(ViewAllTeamsVM);
        //        }
        //        catch (Exception ex)
        //        {
        //            ErrorLogger.LogError(ex, "ViewAllTeams", "Maint");
        //            ViewAllTeamsVM.ErrorMessage = ""; // TODO: Add meaningful front end message
        //        }
        //    }
        //    else
        //    {
        //        oResponse = RedirectToAction("Index", "Home");
        //    }

        //    return oResponse;
        //}















        [HttpGet]
        public void LogOut()
        {

            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            FormsAuthentication.RedirectToLoginPage();

        }

    }
}