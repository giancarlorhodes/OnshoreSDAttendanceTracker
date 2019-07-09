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
    [Authorize]
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
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            //Init Menus
            Session["MenuItems"]= HomeController.GetMenuItem(HttpContext.Session);
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserViewModel loginVM)
        {
            ActionResult oResponse = null;

            IUserBO returnUserBO = new UserBO();
            if ((returnUserBO = _userBLL.CheckUserLogin(loginVM.User.Email, loginVM.UserCred.UserPassword)) != null)
            {
                IUserPO _iUserPO = new UserPO();
                _iUserPO = Mapper.Map<IUserBO, IUserPO>(returnUserBO);
               
                FormsAuthentication.SetAuthCookie(_iUserPO.Email, false);
                Session["UserModel"] = _iUserPO;

                //Refresh Menus
                Session["MenuItems"] = HomeController.GetMenuItem(HttpContext.Session);
                oResponse = RedirectToAction("Dashboards", "Home");
            }
            else
            {
                oResponse = RedirectToAction("Shared", "Error");
            }

            return oResponse;
        }



        [HttpGet]
        [ValidateAntiForgeryToken]
        ///<summary>
        /// Gets form for creating a new User
        ///first, last, email, team, role(Admin, SM, TL)
        /// </summary>
        public ActionResult CreateUser()
        {
            ActionResult oResponse = null;

            // Ensure user is authenticated
            if (ModelState.IsValid)
            {
                List<ITeamDO> doTeamsList = _tda.GetAllTeams();
                List<TeamPO> teamsList = new List<TeamPO>();
                foreach(TeamDO teamDO in doTeamsList)
                {
                    TeamPO teamPO = Mapper.Map<TeamDO,TeamPO>(teamDO);
                    teamsList.Add(teamPO);
                }
                ViewBag.TeamsList = teamsList;

                //TODO: pull all roles names and Ids for dropDown List

                //refresh Menu for User
                Session["MenuItems"] = HomeController.GetMenuItem(HttpContext.Session);

                oResponse = RedirectToAction("Dashboards","Home");
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
        public ActionResult CreateUser(UserViewModel newUser)
        {
            ActionResult oResponse = null;

            // Ensure user is authenticated
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Map UserLogin properties from presentation to data objects
                        IUserDO newUserDO = Mapper.Map<IUserPO, IUserDO>(newUser.User);

                        // new User sent to UserCredDAL to add
                        _uda.CreateUser(newUserDO, newUser.TeamPO.TeamID);

                        oResponse = View("ViewUserByUserID", newUser);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex, "CreateUser", "Account");
                        newUser.ErrorMessage = "There was an issue with creating a new employee. Please try again. If the problem persists contact your IT department.";

                        oResponse = View(newUser);
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
        [ValidateAntiForgeryToken]
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
                    try
                    {
                    //TODO: pull userCred and Team for update

                        //stores data access user
                        IUserDO userDO = _uda.GetUserByID(userPO.UserID);

                        // Map userDO from DO to PO
                        userPO = Mapper.Map<IUserDO, IUserPO>(userDO);
                      
                        oResponse = View(userPO);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex, "ViewUserByUserID", "Account");
                        selectedUser.ErrorMessage = "The was an issue with retrieving the selected employee. Please try again. If the problem persists contact your IT department."; 

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
        ///<summary>
        /// Updates information for a User
        /// first,last, email, team, role  (Admin, SM,TL)
        /// </summary>
        public ActionResult UpdateUserInfo(UserViewModel userToUpdateVM)
        {
            ActionResult oResponse = null;

            // Ensure user is authenticated
                if (ModelState.IsValid)
                {
                    try
                    {
                    //TODO: Map userCred and Team for update

                        // Map user from presentation to data objects
                        UserDO userUpdateDO = Mapper.Map<UserPO, UserDO>(userToUpdateVM.User);

                        // Passes form to be updated
                        _uda.UpdateUser(userUpdateDO);

                        oResponse = View("ViewUserByUserID", userToUpdateVM.User.UserID);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex, "UpdateUserInfo", "Account");
                        userToUpdateVM.ErrorMessage = "The was an issue with updating employee information. Please try again. If the problem persists contact your IT department.";

                        oResponse = View(userToUpdateVM);
                    }
                }
                else
                {
                    oResponse = View(userToUpdateVM);
                }
            return oResponse;
        }


        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int userToDelId, int modifiedByUserId, int teamID)
        {
            _uda.RemoveUser(userToDelId, modifiedByUserId);

            return RedirectToAction("ViewUsersByTeamID", "Maint", teamID);
        }



        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordReset(int UserID)
        {
            UserViewModel userToUpdate = new UserViewModel();
            userToUpdate.User.UserID = UserID;

            return View("PasswordReset", userToUpdate);
        }

        [HttpPost]
        public ActionResult PasswordReset(UserViewModel userNewPasswordVM)
        {
            UserViewModel updatedUserPassword = new UserViewModel();
            updatedUserPassword.User.UserID = userNewPasswordVM.User.UserID;
            updatedUserPassword.UserCred.UserPassword = userNewPasswordVM.UserCred.UserPassword;

           

            return RedirectToAction("ViewUsers", "User");
        }



        [HttpGet]
        [ValidateAntiForgeryToken]
        ///<summary>
        /// Views all users by team
        /// </summary>
        public ActionResult ViewAllUsers()
        {
            ActionResult oResponse = null;
            UserViewModel ViewAllUsersVM = new UserViewModel();

            // Ensures authenticated
            if (ModelState.IsValid)
            {
                try
                {
                    // Calls GetAllUsers from DAL and stores in allUsersDO
                    List<IUserDO> allUsersDO = _uda.GetAllUsers();

                    foreach(IUserDO userDO in allUsersDO)
                    {
                        UserPO userPO = Mapper.Map<IUserDO, UserPO>(userDO);
                        ViewAllUsersVM.ListOfUserPO.Add(userPO);
                    }

                    oResponse = View(ViewAllUsersVM);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex, "ViewAllUsers", "Account");
                    ViewAllUsersVM.ErrorMessage = "There was an issue retrieving employees. Please try again. If the problem persists contact your IT department."; 
                }
            }
            else
            {
                oResponse = RedirectToAction("Index", "Home");
            }

            return oResponse;
        }


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