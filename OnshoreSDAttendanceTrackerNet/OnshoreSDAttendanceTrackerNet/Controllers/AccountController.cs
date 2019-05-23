namespace OnshoreSDAttendanceTrackerNet.Controllers
{

    using OnshoreSDAttendanceTrackerNet.Common;
    using OnshoreSDAttendanceTrackerNet.Models;
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
        public ActionResult Login(UserLoginPO user)
        {


            // db call
            UserCredentialsDataAccess _ucda = new UserCredentialsDataAccess();
            IUserLoginDO _user = new UserLoginDO();

            if (_ucda.IsAutheticatedAgainstDatabase(user.UserEmail, user.UserPassword))
            {
                _user = _ucda.GetUserLoginInformation(user.UserEmail, user.UserPassword);
                FormsAuthentication.SetAuthCookie(user.UserEmail, false);
                UserLoginPO _u = new UserLoginPO();
                _u.UserEmail = _user.Email;
                _u.UserPassword = _user.Password;
                _u.UserRole = (RoleEnum)_user.RoleID_FK;
                _u.FirstName = _user.FirstName;
                _u.LastName = _user.LastName;
                Session["UserModel"] = _u;
                return RedirectToAction("Index", "Home");
            }
            else
            {

                return View();
            }
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