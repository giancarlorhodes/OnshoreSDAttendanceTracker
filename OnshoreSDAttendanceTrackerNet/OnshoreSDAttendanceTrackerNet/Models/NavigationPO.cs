using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnshoreSDAttendanceTrackerNet.Interfaces;

namespace OnshoreSDAttendanceTrackerNet.Models
{
    public class NavigationPO:INavigationPO
    {
        public int NavigationID { get; set; }
        public string MenuItem { get; set; }
        public string URL { get; set; }
        public int RoleID { get; set; }
        public int ParentNavigationID { get; set; }
        public int Order { get; set; }

        public bool HasChild { get; set; }
        public List<INavigationPO> Children { get; set; }
    }
}