using OnshoreSDAttendanceTrackerNetDAL;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using OnshoreSDAttendanceTrackerNetBLL.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace OnshoreSDAttendanceTrackerNetBLL
{
    public static class AbsenceBusinessLogic
    {

        static AbsenceBusinessLogic()
        {
        }

        public static List<IAbsenceDO> CalculateUserPoints(List<IAbsenceDO> iAbsence)
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

        public static List<IAbsenceBO> DetermineEmployeeAbsenceStatus(List<IAbsenceBO> absences)
        {
           // var newList = new List<IAbsenceBO>();

            foreach (IAbsenceBO item in absences)
            {
                if (item.RunningTotal>=10)
                {
                    item.Status = "Pip+";
                }
                else if (item.RunningTotal >=8)
                {
                    item.Status = "Pip";
                }
                else if (item.RunningTotal >= 5)
                {
                    item.Status = "At-Risk";
                }
                else if (item.RunningTotal >= 0)
                {
                    item.Status = "Good Standing";
                }
            }

            return absences;

        }
    }


}

