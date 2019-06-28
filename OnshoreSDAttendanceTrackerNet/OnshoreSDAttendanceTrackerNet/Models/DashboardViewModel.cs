using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Models
{
    public class DashboardViewModel
    {
        public string EmployeeName { get; set; }

        public decimal Points { get; set; }

        public string Status { get; set; }
    }
}