using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnshoreSDAttendanceTrackerNetBLL.Models;
using OnshoreSDAttendanceTrackerNetBLL.Interfaces;


namespace OnshoreSDAttendanceTrackerNetBLL
{
    public static class NavigationBusinessLogic
    {

        public static List<INavigationBO> NavReOrder(List<INavigationBO> menu, int userID)
        {
            List<INavigationBO> orderedMenu = new List<INavigationBO>();
            IdentifySubMenus(menu);
            ReplaceNavURLs(menu, userID);

            foreach (INavigationBO menuItem in menu)
            {
                if (menuItem.HasChild)
                {
                   var child = (IEnumerable<INavigationBO>) menu.Where(e => e.ParentNavigationID == menuItem.NavigationID);

                    menuItem.Children.AddRange((IEnumerable<INavigationBO>)child);
                }
                orderedMenu.Add(menuItem);
            }

            return orderedMenu;
        }

        public static void ReplaceNavURLs(List<INavigationBO> menu, int userID)
        {
            // last / of string
            for (int i = 0; i < menu.Count(); i++)
            {
                if (menu[i].URL.Contains("{userID}"))
                {
                    //  menu[i].URL.Replace("{userID}", userID.ToString());
                    int lastChar = menu[i].URL.LastIndexOf('/') ;
                    menu[i].URL = menu[i].URL.Substring(0, lastChar) + "?userID=" + userID.ToString();
                }

            } 
        }

        private static void IdentifySubMenus(List<INavigationBO> menu)
        {
            foreach (NavigationBO menuItem in menu)
            {
                foreach (NavigationBO item in menu)
                {
                    if (menuItem.NavigationID == item.ParentNavigationID)
                    {
                        menuItem.HasChild = true;
                    }
                    menuItem.Children = new List<INavigationBO>();
                }
            }
        }
    }
}
