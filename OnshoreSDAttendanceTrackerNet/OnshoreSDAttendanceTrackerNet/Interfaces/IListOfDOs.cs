using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnshoreSDAttendanceTrackerNet.Interfaces
{
    public interface IListOfDOs<T>
    {
        List<T> ListOfDos { get; set; }
    }
}
