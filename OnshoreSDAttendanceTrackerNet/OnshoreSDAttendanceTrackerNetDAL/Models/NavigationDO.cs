using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;

namespace OnshoreSDAttendanceTrackerNetDAL.Models
{
    public class NavigationDO : INavigationDO
    {
        public int NavigationID { get; set; }
        public string MenuItem { get; set; }
        public string URL { get; set; }
        public int RoleID { get; set; }
    }
}
