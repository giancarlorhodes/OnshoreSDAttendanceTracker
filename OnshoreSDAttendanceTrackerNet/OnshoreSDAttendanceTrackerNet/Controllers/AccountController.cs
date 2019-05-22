
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
    /// </summary>
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}