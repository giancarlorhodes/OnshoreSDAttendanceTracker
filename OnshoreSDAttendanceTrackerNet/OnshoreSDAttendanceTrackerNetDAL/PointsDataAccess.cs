using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerErrorLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using OnshoreSDAttendanceTrackerNetDAL.Models;

namespace OnshoreSDAttendanceTrackerNetDAL
{
    public static class PointsDataAccess
    {
        private static string _ConnectionString;

        static PointsDataAccess()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString;
        }

        public static void AddAbsence(IAbsenceDO iAbsence,int createdBy )
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_PointsAddNew", conn))
                    {
                        try
                        {
                            
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 15;

                            command.Parameters.AddWithValue("@AbsenceTypeID", iAbsence.AbsenceTypeID);
                            command.Parameters.AddWithValue("@TeamID",iAbsence.TeamID_FK);
                            command.Parameters.AddWithValue("@AbsentUserID", iAbsence.AbsentUserID);
                            command.Parameters.AddWithValue("@Comments", iAbsence.Comments);
                            command.Parameters.AddWithValue("@CreateUserID", createdBy);
                            command.Parameters.AddWithValue("@AbsenceDate", iAbsence.AbsenceDate);

                            conn.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "AddAbsence", "nothing");
                        }
                        finally
                        {
                            conn.Close();
                            conn.Dispose();
                            command.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "AddAbsence", "");
            }
        }

        public static List<IAbsenceDO> ViewAbsencesByUserID(int userID)
        {
            var listOfAbsenceDOs = new List<IAbsenceDO>();

            try
            {
                using(SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    using(SqlCommand viewComm  = new SqlCommand("sp_GetPointsByUserID", conn))
                    {
                        viewComm.CommandType = CommandType.StoredProcedure;
                        viewComm.CommandTimeout = 35;
                        viewComm.Parameters.AddWithValue("@UserID", userID);
                        conn.Open();

                        using (SqlDataReader reader = viewComm.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IAbsenceDO absence = new AbsenceDO();

                                absence.AbsenceTypeID = reader.GetInt32(reader.GetOrdinal("AbsenceTypeID"));
                                absence.Comments = reader["Comment"].ToString();
                                absence.Point = Convert.ToDecimal( reader["Point"]);
                                absence.Active = reader.GetBoolean(reader.GetOrdinal("Active"));
                                absence.TeamID_FK = reader.GetInt32(reader.GetOrdinal("TeamID"));
                                absence.AbsenceDate= Convert.ToDateTime(reader["AbsenceDate"]);
                                absence.AbsentUserID= reader.GetInt32(reader.GetOrdinal("UserID"));
                                absence.Name= reader["EmployeeName"].ToString();
                                absence.TeamMgtID = reader.GetInt32(reader.GetOrdinal("TeamMgtID"));

                                listOfAbsenceDOs.Add(absence);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "ViewAbsencesByUserID", "nothing");
            }

            return listOfAbsenceDOs;
        }

        public static List<IAbsenceDO> ViewAllAbsences()
        {
            var listOfAbsenceDOs = new List<IAbsenceDO>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand viewComm = new SqlCommand("sp_GetAllPoints", conn))
                    {
                        viewComm.CommandType = CommandType.StoredProcedure;
                        viewComm.CommandTimeout = 35;
                        conn.Open();

                        using (SqlDataReader reader = viewComm.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IAbsenceDO absence = new AbsenceDO();

                                absence.AbsenceTypeID = reader.GetInt32(reader.GetOrdinal("AbsenceTypeID"));
                                absence.Comments = reader["Comment"].ToString();
                                absence.Point = Convert.ToDecimal(reader["Point"]);
                                absence.Active = reader.GetBoolean(reader.GetOrdinal("Active"));
                                absence.TeamID_FK = reader.GetInt32(reader.GetOrdinal("TeamID"));
                                absence.AbsenceDate = Convert.ToDateTime(reader["AbsenceDate"]);
                                absence.AbsentUserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                                absence.Name = reader["EmployeeName"].ToString();
                                absence.TeamMgtID= reader.GetInt32(reader.GetOrdinal("TeamMgtID"));

                                listOfAbsenceDOs.Add(absence);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "ViewAbsencesByUserID", "nothing");
            }

            return listOfAbsenceDOs;
        }

        public static void UdpateAbsenceInformation(IAbsenceDO iAbsence,int updatedBy)
        {

            try
            {
                using(SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    using(SqlCommand updateComm = new SqlCommand("sp_PointsUpdate", conn))
                    {
                        try
                        {
                            updateComm.CommandType = CommandType.StoredProcedure;
                            updateComm.CommandTimeout = 35;
                           
                            updateComm.Parameters.AddWithValue("@AbsenceTypeID", iAbsence.AbsenceTypeID);
                            updateComm.Parameters.AddWithValue("@@TeamMgtID", iAbsence.TeamMgtID);
                            updateComm.Parameters.AddWithValue("@AbsentUserID", iAbsence.AbsentUserID);
                            updateComm.Parameters.AddWithValue("@AbsenceDate",iAbsence.AbsenceDate);
                            updateComm.Parameters.AddWithValue("@UpdatedUserID", updatedBy);
                            updateComm.Parameters.AddWithValue("@Active", iAbsence.Active);
                             
                            conn.Open();
                            updateComm.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "UdpateAbsenceInformation", "nothing");
                        }
                        finally
                        {
                            conn.Close();
                            conn.Dispose();
                            updateComm.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "UdpateAbsenceInformation", "");
            }
        }
    }
}
