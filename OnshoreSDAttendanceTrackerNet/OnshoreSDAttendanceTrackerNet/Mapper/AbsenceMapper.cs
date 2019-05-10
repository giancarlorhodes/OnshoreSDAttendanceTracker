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
    public class AbsenceMapper : IAbsenceMapper
    {
        public List<IAttendanceBO> MapListOfDOsToListOfBOs(List<IAttendanceDO> attendanceDOs)
        {
            var listOfAttendanceBOs = new List<IAttendanceBO>();

            // Iterate through DOs
            foreach(IAttendanceDO entry in attendanceDOs)
            {
                var attendanceBO = MapAttendanceDOtoBO(entry);
                listOfAttendanceBOs.Add(attendanceBO);
            }

            return listOfAttendanceBOs;
        }

        public List<AbsencePO> MapListOfDOsToListOfPOs(List<IAttendanceDO> attendanceDOs)
        {
            var listOfAttendancePOs = new List<AbsencePO>();

            // Map each object in the list
            foreach(IAttendanceDO entry in attendanceDOs)
            {
                var attendancePO = MapAttendanceDOtoPO(entry);
                listOfAttendancePOs.Add(attendancePO);
            }

            return listOfAttendancePOs;
        }

        public AbsencePO MapAttendanceBOtoPO(AttendanceBO attendanceBO)
        {
            var oAttendance = new AbsencePO();
            oAttendance.AbsenceTypeID = attendanceBO.AbsenceTypeID;
            oAttendance.Name = attendanceBO.Name;
            oAttendance.Point = attendanceBO.Point;
            oAttendance.Active = attendanceBO.Active;
            oAttendance.TeamID_FK = attendanceBO.TeamID_FK;

            return oAttendance;
        }

        public IAttendanceBO MapAttendanceDOtoBO(IAttendanceDO attendanceDO)
        {
            IAttendanceBO oAttendance = new AttendanceBO();
            oAttendance.AbsenceTypeID = attendanceDO.AbsenceTypeID;
            oAttendance.Name = attendanceDO.Name;
            oAttendance.Point = attendanceDO.Point;
            oAttendance.Active = attendanceDO.Active;
            oAttendance.TeamID_FK = attendanceDO.TeamID_FK;

            return oAttendance;
        }

        public AbsencePO MapAttendanceDOtoPO(IAttendanceDO attendanceDO)
        {
            var oAttendance = new AbsencePO();
            oAttendance.AbsenceTypeID = attendanceDO.AbsenceTypeID;
            oAttendance.Name = attendanceDO.Name;
            oAttendance.Point = attendanceDO.Point;
            oAttendance.Active = attendanceDO.Active;
            oAttendance.TeamID_FK = attendanceDO.TeamID_FK;

            return oAttendance;
        }

        public IAttendanceDO MapAttendancePOtoDO(AbsencePO attendancePO)
        {
            IAttendanceDO oAttendance = new AbsenceDO();
            oAttendance.AbsenceTypeID = attendancePO.AbsenceTypeID;
            oAttendance.Name = attendancePO.Name;
            oAttendance.Point = attendancePO.Point;
            oAttendance.Active = attendancePO.Active;
            oAttendance.TeamID_FK = attendancePO.TeamID_FK;

            return oAttendance;
        }
    }
}