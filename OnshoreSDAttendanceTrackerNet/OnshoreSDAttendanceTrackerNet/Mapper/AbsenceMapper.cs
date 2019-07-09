using OnshoreSDAttendanceTrackerNet.Interfaces;
using OnshoreSDAttendanceTrackerNet.Models;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using OnshoreSDAttendanceTrackerNetBLL.Interfaces;
using OnshoreSDAttendanceTrackerNetBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.AutoMapper
{
    public class AbsenceMapper
    {
        public static List<IAbsenceBO> MapListOfDOsToListOfBOs(List<IAbsenceDO> absenceDOs)
        {
            var listOfAbsenceBOs = new List<IAbsenceBO>();

            // Iterate through DOs
            foreach(IAbsenceDO entry in absenceDOs)
            {
                var absenceBO = MapAbsenceDOtoBO(entry);
                listOfAbsenceBOs.Add(absenceBO);
            }

            return listOfAbsenceBOs;
        }

        public static List<AbsencePO> MapListOfDOsToListOfPOs(List<IAbsenceDO> absenceDOs)
        {
            var listOfAbsencePOs = new List<AbsencePO>();

            // Map each object in the list
            foreach(IAbsenceDO entry in absenceDOs)
            {
                var absencePO = MapAbsenceDOtoPO(entry);
                listOfAbsencePOs.Add(absencePO);
            }

            return listOfAbsencePOs;
        }

        public static List<DashboardViewModel> MapListOfPOsToListOfVMs(List<IAbsencePO> absencePOs)
        {
            var listOfAbsenceVMs = new List<DashboardViewModel>();

            // Map each object in the list
            foreach (IAbsencePO entry in absencePOs)
            {
                var absenceVM = new DashboardViewModel();
                absenceVM.EmployeeName = entry.Name;
                absenceVM.Points = entry.Point; //Need to change RunningTotal once Calc is finished
                absenceVM.Status = entry.Status;
                listOfAbsenceVMs.Add(absenceVM);
            }

            return listOfAbsenceVMs;
        }

        public static List<IAbsencePO> MapListOfBOsToListOfPOs(List<IAbsenceBO> absenceBOs)
        {
            var listOfAbsencePOs = new List<IAbsencePO>();

            // Map each object in the list
            foreach (IAbsenceBO entry in absenceBOs)
            {
                var absenceVM = MapAbsenceBOtoPO(entry);
                listOfAbsencePOs.Add(absenceVM);
            }

            return listOfAbsencePOs;
        }

        public static AbsencePO MapAbsenceBOtoPO(AbsenceBO absenceBO)
        {
            var oAbsence = new AbsencePO();
            oAbsence.AbsenceTypeID = absenceBO.AbsenceTypeID;
            oAbsence.Name = absenceBO.Name;
            oAbsence.TeamName = absenceBO.TeamName;
            oAbsence.Point = absenceBO.Point;
            oAbsence.Active = absenceBO.Active;
            oAbsence.TeamID_FK = absenceBO.TeamID_FK;
            oAbsence.AbsenceDate = absenceBO.AbsenceDate;
            oAbsence.AbsentUserID = absenceBO.AbsentUserID;
            oAbsence.Comments = absenceBO.Comments;
            oAbsence.RunningTotal = absenceBO.RunningTotal;
            oAbsence.TeamMgtID = absenceBO.TeamMgtID;
            oAbsence.Status = absenceBO.Status;

            return oAbsence;
        }
        public static AbsencePO MapAbsenceBOtoPO(IAbsenceBO absenceBO)
        {
            var oAbsence = new AbsencePO();
            oAbsence.AbsenceTypeID = absenceBO.AbsenceTypeID;
            oAbsence.Name = absenceBO.Name;
            oAbsence.TeamName = absenceBO.TeamName;
            oAbsence.Point = absenceBO.Point;
            oAbsence.Active = absenceBO.Active;
            oAbsence.TeamID_FK = absenceBO.TeamID_FK;
            oAbsence.AbsenceDate = absenceBO.AbsenceDate;
            oAbsence.AbsentUserID = absenceBO.AbsentUserID;
            oAbsence.Comments = absenceBO.Comments;
            oAbsence.RunningTotal = absenceBO.RunningTotal;
            oAbsence.TeamMgtID = absenceBO.TeamMgtID;
            oAbsence.Status = absenceBO.Status;
            oAbsence.EmployeeName = absenceBO.EmployeeName;

            return oAbsence;
        }

        public static IAbsenceBO MapAbsenceDOtoBO(IAbsenceDO absenceDO)
        {
            IAbsenceBO oAbsence = new AbsenceBO();
            oAbsence.AbsenceTypeID = absenceDO.AbsenceTypeID;
            oAbsence.Name = absenceDO.Name;
            oAbsence.TeamName = absenceDO.TeamName;
            oAbsence.Point = absenceDO.Point;
            oAbsence.Active = absenceDO.Active;
            oAbsence.TeamID_FK = absenceDO.TeamID_FK;
            oAbsence.AbsenceDate = absenceDO.AbsenceDate;
            oAbsence.AbsentUserID = absenceDO.AbsentUserID;
            oAbsence.Comments = absenceDO.Comments;
            oAbsence.RunningTotal = absenceDO.RunningTotal;
            oAbsence.TeamMgtID = absenceDO.TeamMgtID;
            oAbsence.EmployeeName = absenceDO.EmployeeName;
            return oAbsence;
        }

        public static AbsencePO MapAbsenceDOtoPO(IAbsenceDO absenceDO)
        {
            var oAbsence = new AbsencePO();
            oAbsence.AbsenceTypeID = absenceDO.AbsenceTypeID;
            oAbsence.Name = absenceDO.Name;
            oAbsence.TeamName = absenceDO.TeamName;
            oAbsence.Point = absenceDO.Point;
            oAbsence.Active = absenceDO.Active;
            oAbsence.TeamID_FK = absenceDO.TeamID_FK;
            oAbsence.AbsenceDate = absenceDO.AbsenceDate;
            oAbsence.AbsentUserID = absenceDO.AbsentUserID;
            oAbsence.Comments = absenceDO.Comments;
            oAbsence.RunningTotal = absenceDO.RunningTotal;
            oAbsence.TeamMgtID = absenceDO.TeamMgtID;
            oAbsence.EmployeeName = absenceDO.EmployeeName;

            return oAbsence;
        }

        public static IAbsenceDO MapAbsencePOtoDO(AbsencePO absencePO)
        {
            IAbsenceDO oAbsence = new AbsenceDO();
            oAbsence.AbsenceTypeID = absencePO.AbsenceTypeID;
            oAbsence.Name = absencePO.Name;
            oAbsence.TeamName = absencePO.TeamName;
            oAbsence.Point = absencePO.Point;
            oAbsence.Active = absencePO.Active;
            oAbsence.TeamID_FK = absencePO.TeamID_FK;
            oAbsence.AbsenceDate = absencePO.AbsenceDate;
            oAbsence.AbsentUserID = absencePO.AbsentUserID;
            oAbsence.Comments = absencePO.Comments;
            oAbsence.RunningTotal = absencePO.RunningTotal;
            oAbsence.TeamMgtID = absencePO.TeamMgtID;
            oAbsence.EmployeeName = absencePO.EmployeeName;
            return oAbsence;
        }
    }
}