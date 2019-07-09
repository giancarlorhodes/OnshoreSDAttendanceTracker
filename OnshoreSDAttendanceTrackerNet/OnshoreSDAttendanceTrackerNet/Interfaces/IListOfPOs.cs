using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnshoreSDAttendanceTrackerNet.Interfaces
{
    public interface IListOfPOs<T>
    {
        List<T> ListOfPos { get; set; }
    }
}
