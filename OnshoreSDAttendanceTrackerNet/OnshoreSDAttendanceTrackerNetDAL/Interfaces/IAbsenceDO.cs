﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OnshoreSDAttendanceTrackerNetDAL.Interfaces
{


    public interface IAbsenceDO
    {
        int AbsenceTypeID { get; set; }
        long PointBankID { get; set;}
        string TeamName { get; set; }
        string Name { get; set; }
        string Comments { get; set; }
        decimal Point { get; set; }
        bool Active { get; set; }
        int TeamID_FK { get; set; }
        int TeamMgtID { get; set; }
        int AbsentUserID { get; set; }
        DateTime AbsenceDate { get; set; }
        decimal RunningTotal { get; set; }
        string EmployeeName { get; set; }
        string AbsenceType { get; set; }
    }




}
