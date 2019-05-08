using OnshoreSDAttendanceTrackerNet.Interfaces;
using OnshoreSDAttendanceTrackerNet.Models;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using OnshoreSDAttendanceTracketNetBLL.Interfaces;
using OnshoreSDAttendanceTracketNetBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnshoreSDAttendanceTrackerNet.Mapper
{
    public class ExceptionMapper : IExceptionMapper
    {
        public ExceptionPO MapExceptionBOtoPO(ExceptionBO exceptionBO)
        {
            var oException = new ExceptionPO();
            oException.LogID = exceptionBO.LogID;
            oException.ExceptionMessage = exceptionBO.ExceptionMessage;
            oException.ExceptionType = exceptionBO.ExceptionType;
            oException.ExceptionSource = exceptionBO.ExceptionSource;
            oException.ExceptionURL = exceptionBO.ExceptionURL;
            oException.LogDate = exceptionBO.LogDate;

            return oException;
        }

        public IExceptionBO MapExceptionDOtoBO(IExceptionDO exceptionBO)
        {
            IExceptionBO oException = new ExceptionBO();
            oException.LogID = exceptionBO.LogID;
            oException.ExceptionMessage = exceptionBO.ExceptionMessage;
            oException.ExceptionType = exceptionBO.ExceptionType;
            oException.ExceptionSource = exceptionBO.ExceptionSource;
            oException.ExceptionURL = exceptionBO.ExceptionURL;
            oException.LogDate = exceptionBO.LogDate;

            return oException;
        }

        public ExceptionPO MapExceptionDOtoPO(IExceptionDO exceptionDO)
        {
            var oException = new ExceptionPO();
            oException.LogID = exceptionDO.LogID;
            oException.ExceptionMessage = exceptionDO.ExceptionMessage;
            oException.ExceptionType = exceptionDO.ExceptionType;
            oException.ExceptionSource = exceptionDO.ExceptionSource;
            oException.ExceptionURL = exceptionDO.ExceptionURL;
            oException.LogDate = exceptionDO.LogDate;

            return oException;
        }

        public IExceptionDO MapExceptionPOtoDO(ExceptionPO exceptionPO)
        {
            IExceptionDO oException = new ExceptionDO();
            oException.LogID = exceptionPO.LogID;
            oException.ExceptionMessage = exceptionPO.ExceptionMessage;
            oException.ExceptionType = exceptionPO.ExceptionType;
            oException.ExceptionSource = exceptionPO.ExceptionSource;
            oException.ExceptionURL = exceptionPO.ExceptionURL;
            oException.LogDate = exceptionPO.LogDate;

            return oException;
        }

        public List<IExceptionBO> MapListOfDOsToListOfBOs(List<IExceptionDO> exceptionDOs)
        {
            var listOfExcceptionPOs = new List<IExceptionBO>();

            // Iterate through DOs
            foreach(IExceptionDO entry in exceptionDOs)
            {
                var expectionPO = MapExceptionDOtoBO(entry);
                listOfExcceptionPOs.Add(expectionPO);
            }

            return listOfExcceptionPOs;
        }

        public List<ExceptionPO> MapListOfDOsToListOfPOs(List<IExceptionDO> exceptionDOs)
        {
            var listOfExceptionPOs = new List<ExceptionPO>();

            // Iterate through DOs
            foreach(IExceptionDO entry in exceptionDOs)
            {
                var exceptionPO = MapExceptionDOtoPO(entry);
                listOfExceptionPOs.Add(exceptionPO);
            }

            return listOfExceptionPOs;
        }
    }
}