﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OnshoreSDAttendanceTrackerNetDAL.Interfaces
{
    public interface ITeamDO
    {
        int TeamID { get; set; }
        string Name { get; set; }
        string Comment { get; set; }
        int Active { get; set; }
    }
}
