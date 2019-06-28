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
    using OnshoreSDAttendanceTrackerNetDAL;
    using OnshoreSDAttendanceTrackerNetBLL;
    using OnshoreSDAttendanceTrackerErrorLogger;
    using OnshoreSDAttendanceTrackerNet.Interfaces;
    using OnshoreSDAttendanceTrackerNet.Models;
    using OnshoreSDAttendanceTrackerNet.AutoMapper;
    using global::AutoMapper;

    [Authorize]
    public   class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        { //Get Menu Items
          

            return View();
        }

        public ActionResult Dashboards()
        {
            var curUser = (IUserPO)Session["UserModel"];
            if (curUser.RoleID_FK== (int)RoleEnum.Administrator || curUser.RoleID_FK == (int)RoleEnum.Service_Manager)
            {
                return RedirectToAction("AdminDashboard");
            }
            else if(curUser.RoleID_FK==(int)RoleEnum.Team_Lead)
            {
                return RedirectToAction("LeadDashboard");
            }
            return View("Index");
        }
        [HttpGet]
        public ActionResult AdminDashboard()
        {
            // Emp Name-> Points-> Status
            // if ((UserPO)Session["UserModel"] != null)
            // {
            //var loggedUSer =  (UserPO)Session["UserModel"];
            List<DashboardViewModel> emps = AbsenceMapper.MapListOfPOsToListOfVMs(AbsenceMapper.MapListOfBOsToListOfPOs(AbsenceBusinessLogic.DetermineEmployeeAbsenceStatus(AbsenceMapper.MapListOfDOsToListOfBOs( PointsDataAccess.ViewAllAbsences()))));
            //TODO: Implement AutoMapper Mapper.Map<List<IAbsenceBO>, List<DashboardViewModel>>(AbsenceBusinessLogic.DetermineEmployeeAbsenceStatus(Mapper.Map<List<IAbsenceDO>,List<IAbsenceBO>>( PointsDataAccess.ViewAllAbsences())));
            //  }
            return View(emps);
        }
        public ActionResult LeadDashboard()
        {
            // Emp Name-> Points-> Status
            // if ((UserPO)Session["UserModel"] != null)
            // {
            var loggedUSer =  (UserPO)Session["UserModel"];
            List<DashboardViewModel> emps = AbsenceMapper.MapListOfPOsToListOfVMs(AbsenceMapper.MapListOfBOsToListOfPOs(AbsenceBusinessLogic.DetermineEmployeeAbsenceStatus(AbsenceMapper.MapListOfDOsToListOfBOs(PointsDataAccess.ViewAbsencesByUserID(loggedUSer.UserID)))));
            //TODO: Implement AutoMapper Mapper.Map<List<IAbsenceBO>, List<DashboardViewModel>>(AbsenceBusinessLogic.DetermineEmployeeAbsenceStatus(Mapper.Map<List<IAbsenceDO>, List<IAbsenceBO>>(PointsDataAccess.ViewAbsencesByUserID(loggedUSer.UserID))));
            //  }
            return View(emps);
        }
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public static List<INavigationPO> GetMenuItem(HttpSessionStateBase session)
        {
            IUserPO curUser = new UserPO();
            curUser = GetCurrentUserID(session, curUser);
            List<INavigationPO> menutItems = NavigationMapper.MapListOfBOsToListOfPOs(NavigationBusinessLogic.NavReOrder(NavigationMapper.MapListOfDOsToListOfBOs(NavigationDataAccess.GetNavigationItemsByRoleID(curUser.RoleID_FK)),curUser.UserID));//loggedUSer.RoleID_FK
            //TODO: Implement AutoMapper List<INavigationPO> menutItems = Mapper.Map<List<INavigationBO>, List<INavigationPO>>(NavigationBusinessLogic.NavReOrder(menus,curUser.UserID));//loggedUSer.RoleID_FK
            return menutItems;
        }
        
        private static IUserPO GetCurrentUserID(HttpSessionStateBase session, IUserPO curUser)
        {
            if (session["UserModel"] != null)
            {
                curUser = (IUserPO)session["UserModel"];
            }
            else
            {
                curUser.RoleID_FK = -1;
            }

            return curUser;
        }
    }
}