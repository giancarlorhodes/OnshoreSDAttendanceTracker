using OnshoreSDAttendanceTrackerNetDAL;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace OnshoreSDAttendanceTrackerNetBLL
{
    public class AbsenceBusinessLogic
    {
        private PointsDataAccess _PointsDA;

        public AbsenceBusinessLogic()
        {
            string connection = ConfigurationManager.ConnectionStrings[""].ConnectionString;
            _PointsDA = new PointsDataAccess(connection);
        }

        public List<IAbsenceDO> CalculateUserPoints(List<IAbsenceDO> iAbsence)
        {
            // TODO: Need to add a way to pass the ID to this method based off of session
            var userPoints = new List<IAbsenceDO>();
            decimal runningTotal = 0;


            var grouping = from x in iAbsence
                           group x by x.Point into points
                           select points;

            foreach (IAbsenceDO user in grouping)
            {
                userPoints.Add(user);
            }

            var query = userPoints
                            .OrderBy(i => i.Point)
                            .Select(i =>
                            {
                                runningTotal += i.Point;
                                return new
                                {
                                    RunningTotal = runningTotal
                                };
                            });
            var calculatedPoints = new List<IAbsenceDO>();
            foreach (var item in query)
            {
                var runningTotalList = new AbsenceDO() { RunningTotal = item.RunningTotal };
                calculatedPoints.Add(runningTotalList);
            }

            return calculatedPoints;
        }
    }


}

