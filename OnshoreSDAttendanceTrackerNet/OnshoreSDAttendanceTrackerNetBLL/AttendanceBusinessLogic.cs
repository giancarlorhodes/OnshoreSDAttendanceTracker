using OnshoreSDAttendanceTrackerNetDAL;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace OnshoreSDAttendanceTrackerNetBLL
{
    public class AttendanceBusinessLogic
    {
        private PointsDataAccess _PointsDA;

        public AttendanceBusinessLogic()
        {
            string connection = ConfigurationManager.ConnectionStrings[""].ConnectionString;
            _PointsDA = new PointsDataAccess(connection);
        }

        public List<IAttendanceDO> CalculateUserPoints(List<IAttendanceDO> iAttendance)
        {
            // TODO: Need to add a way to pass the ID to this method based off of session
            var userPoints = new List<IAttendanceDO>();
            decimal runningTotal = 0;


            var grouping = from x in iAttendance
                           group x by x.Point into points
                           select points;

            foreach (IAttendanceDO user in grouping)
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
            var calculatedPoints = new List<IAttendanceDO>();
            foreach (var item in query)
            {
                var runningTotalList = new AttendanceDO() { RunningTotal = item.RunningTotal };
                calculatedPoints.Add(runningTotalList);
            }

            return calculatedPoints;
        }
    }


}

