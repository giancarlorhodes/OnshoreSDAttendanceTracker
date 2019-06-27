namespace OnshoreSDAttendanceTrackerNet.Controllers
{
    using OnshoreSDAttendanceTrackerNet.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
    using OnshoreSDAttendanceTrackerNetDAL.Models;
    using OnshoreSDAttendanceTrackerNetDAL;
    using OnshoreSDAttendanceTrackerNetBLL;
    using OnshoreSDAttendanceTrackerErrorLogger;
    using OnshoreSDAttendanceTrackerNet.Interfaces;
    using OnshoreSDAttendanceTrackerNet.Models;
    using OnshoreSDAttendanceTrackerNet.AutoMapper;

    [Authorize]
    public class HomeController : Controller
    {
        private NavigationDataAccess _menuDAO = new NavigationDataAccess();
        public ActionResult Index()
        { //Get Menu Items
         
            List<INavigationPO> menutItems = NavigationMapper.MapListOfBOsToListOfPOs(NavigationBusinessLogic.NavReOrder( NavigationMapper.MapListOfDOsToListOfBOs(_menuDAO.GetNavigationItemsByRoleID(1))));//loggedUSer.RoleID_FK
             Session["MenuItems"] = menutItems;
            
            return View();
        }

        public ActionResult AdminDashboard()
        {
            // Emp Name-> Points-> Status
            // if ((UserPO)Session["UserModel"] != null)
            // {
            //var loggedUSer =  (UserPO)Session["UserModel"];


            //  }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}