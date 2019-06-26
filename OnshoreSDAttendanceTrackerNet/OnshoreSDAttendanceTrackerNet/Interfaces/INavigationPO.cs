using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnshoreSDAttendanceTrackerNet.Interfaces
{
    public interface INavigationPO
    {
        int NavigationID { get; set; }
        string MenuItem { get; set; }
        string URL { get; set; }
        int RoleID { get; set; }
        int ParentNavigationID { get; set; }
        int Order { get; set; }
          bool HasChild { get; set; }
        List<INavigationPO> Children { get; set; }
    }
}
