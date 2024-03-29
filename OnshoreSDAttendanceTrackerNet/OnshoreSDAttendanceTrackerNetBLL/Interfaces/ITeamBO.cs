﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OnshoreSDAttendanceTrackerNetBLL.Models
{
    public interface ITeamBO
    {
        int TeamID { get; set; }
        string Name { get; set; }
        string Comment { get; set; }
        bool Active { get; set; }
        decimal RunningTotal { get; set; }
    }
}
