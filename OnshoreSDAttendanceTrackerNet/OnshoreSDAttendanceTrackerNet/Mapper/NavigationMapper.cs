using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnshoreSDAttendanceTrackerNet.Interfaces;
using OnshoreSDAttendanceTrackerNet.Models;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using OnshoreSDAttendanceTrackerNetBLL.Interfaces;
using OnshoreSDAttendanceTrackerNetBLL.Models;


namespace OnshoreSDAttendanceTrackerNet.AutoMapper
{
    public static class NavigationMapper
    {
        public static List<INavigationBO> MapListOfDOsToListOfBOs(List<INavigationDO> menuDOs)
        {
            var listOfMenuBOs = new List<INavigationBO>();

            // Iterate through DOs
            foreach (INavigationDO entry in menuDOs)
            {
                var MenuBO = MapMenuDOtoBO(entry);
                listOfMenuBOs.Add(MenuBO);
            }

            return listOfMenuBOs;
        }

        public static List<NavigationPO> MapListOfDOsToListOfPOs(List<INavigationDO> menuDOs)
        {
            var listOfMenuPOs = new List<NavigationPO>();

            // Map each object in the list
            foreach (INavigationDO entry in menuDOs)
            {
                var MenuPO = MapMenuDOtoPO(entry);
                listOfMenuPOs.Add(MenuPO);
            }

            return listOfMenuPOs;
        }

        public static List<INavigationPO> MapListOfBOsToListOfPOs(List<INavigationBO> menuDOs)
        {
            var listOfMenuPOs = new List<INavigationPO>();

            // Map each object in the list
            foreach (INavigationBO entry in menuDOs)
            {
                var MenuPO = MapMenuBOtoPO(entry);
                listOfMenuPOs.Add(MenuPO);
            }

            return listOfMenuPOs;
        }

        public static NavigationPO MapMenuBOtoPO(INavigationBO menuBO)
        {
            var oMenu = new NavigationPO();
            oMenu.NavigationID = menuBO.NavigationID;
            oMenu.MenuItem = menuBO.MenuItem;
            oMenu.URL = menuBO.URL;
            oMenu.RoleID = menuBO.RoleID;
            oMenu.ParentNavigationID = menuBO.ParentNavigationID;
            oMenu.Order = menuBO.Order;
            oMenu.HasChild = menuBO.HasChild;
            oMenu.Children = MapListOfBOsToListOfPOs(menuBO.Children);

            return oMenu;
        }

        public static INavigationBO MapMenuDOtoBO(INavigationDO menuDO)
        {
            INavigationBO oMenu = new NavigationBO();
            oMenu.NavigationID = menuDO.NavigationID;
            oMenu.MenuItem = menuDO.MenuItem;
            oMenu.URL = menuDO.URL;
            oMenu.RoleID = menuDO.RoleID;
            oMenu.ParentNavigationID = menuDO.ParentNavigationID;
            oMenu.Order = menuDO.Order;

            return oMenu;
        }

        public static NavigationPO MapMenuDOtoPO(INavigationDO menuDO)
        {
            var oMenu = new NavigationPO();
            oMenu.NavigationID = menuDO.NavigationID;
            oMenu.MenuItem = menuDO.MenuItem;
            oMenu.URL = menuDO.URL;
            oMenu.RoleID = menuDO.RoleID;
            oMenu.ParentNavigationID = menuDO.ParentNavigationID;
            oMenu.Order = menuDO.Order;

            return oMenu;
        }

        public static INavigationDO MapMenuPOtoDO(NavigationPO menuPO)
        {
            INavigationDO oMenu = new NavigationDO();
            oMenu.NavigationID = menuPO.NavigationID;
            oMenu.MenuItem = menuPO.MenuItem;
            oMenu.URL = menuPO.URL;
            oMenu.RoleID = menuPO.RoleID;
            oMenu.ParentNavigationID = menuPO.ParentNavigationID;
            oMenu.Order = menuPO.Order;

            return oMenu;
        }
    }
}