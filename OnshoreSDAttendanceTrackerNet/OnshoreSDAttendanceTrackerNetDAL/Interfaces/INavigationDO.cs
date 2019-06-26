using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnshoreSDAttendanceTrackerNetDAL.Interfaces
{
    public interface INavigationDO
    {
          int NavigationID { get; set; }
          string MenuItem { get; set; }
          string URL { get; set; }
          int RoleID { get; set; }
          int ParentNavigationID { get; set; }
          int Order { get; set; }
    }
}
