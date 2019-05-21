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

namespace OnshoreSDAttendanceTrackerNet.Mapper
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

        public static AbsencePO MapAbsenceBOtoPO(AbsenceBO absenceBO)
        {
            var oAbsence = new AbsencePO();
            oAbsence.AbsenceTypeID = absenceBO.AbsenceTypeID;
            oAbsence.Name = absenceBO.Name;
            oAbsence.Point = absenceBO.Point;
            oAbsence.Active = absenceBO.Active;
            oAbsence.TeamID_FK = absenceBO.TeamID_FK;

            return oAbsence;
        }

        public static IAbsenceBO MapAbsenceDOtoBO(IAbsenceDO absenceDO)
        {
            IAbsenceBO oAbsence = new AbsenceBO();
            oAbsence.AbsenceTypeID = absenceDO.AbsenceTypeID;
            oAbsence.Name = absenceDO.Name;
            oAbsence.Point = absenceDO.Point;
            oAbsence.Active = absenceDO.Active;
            oAbsence.TeamID_FK = absenceDO.TeamID_FK;

            return oAbsence;
        }

        public static AbsencePO MapAbsenceDOtoPO(IAbsenceDO absenceDO)
        {
            var oAbsence = new AbsencePO();
            oAbsence.AbsenceTypeID = absenceDO.AbsenceTypeID;
            oAbsence.Name = absenceDO.Name;
            oAbsence.Point = absenceDO.Point;
            oAbsence.Active = absenceDO.Active;
            oAbsence.TeamID_FK = absenceDO.TeamID_FK;

            return oAbsence;
        }

        public static IAbsenceDO MapAbsencePOtoDO(AbsencePO absencePO)
        {
            IAbsenceDO oAbsence = new AbsenceDO();
            oAbsence.AbsenceTypeID = absencePO.AbsenceTypeID;
            oAbsence.Name = absencePO.Name;
            oAbsence.Point = absencePO.Point;
            oAbsence.Active = absencePO.Active;
            oAbsence.TeamID_FK = absencePO.TeamID_FK;

            return oAbsence;
        }
    }
}