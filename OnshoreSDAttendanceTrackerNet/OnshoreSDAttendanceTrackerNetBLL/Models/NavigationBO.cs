using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnshoreSDAttendanceTrackerNetBLL.Interfaces;

namespace OnshoreSDAttendanceTrackerNetBLL.Models
{
    public class NavigationBO:INavigationBO
    {
        public int NavigationID { get; set; }
        public string MenuItem { get; set; }
        public string URL { get; set; }
        public int RoleID { get; set; }
        public int ParentNavigationID { get; set; }
        public int Order { get; set; }

        public bool HasChild { get; set; }

        public List<INavigationBO> Children { get; set; }
    }
}
