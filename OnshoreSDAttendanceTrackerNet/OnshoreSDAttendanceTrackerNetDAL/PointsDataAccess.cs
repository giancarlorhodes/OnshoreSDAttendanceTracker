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
                                absence.Active = Convert.ToBoolean((int)reader["Active"]);
                                absence.TeamID_FK = reader.GetInt32(reader.GetOrdinal("TeamID"));
                                absence.AbsenceDate= Convert.ToDateTime(reader["AbsenceDate"]);
                                absence.AbsentUserID= reader.GetInt32(reader.GetOrdinal("UserID"));
                                absence.TeamMgtID = reader.GetInt32(reader.GetOrdinal("TeamMgtID"));
                                absence.EmployeeName = reader["EmployeeName"].ToString();
                                //Merged JC Method AR 7/9/19
                                absence.TeamName = reader["Team"].ToString();
                                absence.Name = reader["Type"].ToString();
                                absence.PointBankID = (int)reader["PointBankID"];
                                //------------
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
                                absence.Active = Convert.ToBoolean((int) reader["Active"]);
                                absence.TeamID_FK = reader.GetInt32(reader.GetOrdinal("TeamID"));
                                absence.AbsenceDate = Convert.ToDateTime(reader["AbsenceDate"]);
                                absence.AbsentUserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                                absence.Name = reader["EmployeeName"].ToString();
                                absence.TeamName = reader["TeamName"].ToString();
                                absence.TeamMgtID= reader.GetInt32(reader.GetOrdinal("TeamMgtID"));
                                absence.AbsenceType = reader["AbsenceType"].ToString();
                                absence.PointBankID = (long)reader["PointBankID"];

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

        public static void UpdateAbsenceInformation(IAbsenceDO iAbsence,int updatedBy)
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

        //AR 7/9/19 Merged from AbsenceTypeDA
        // Gets absences for a given team -- Check points for duplication
        public static List<IAbsenceDO> GetAbsencesByTeamID(int iTeamID)
        {
            List<IAbsenceDO> listAbsenceTypesByTeam = new List<IAbsenceDO>();

            try
            {
                using (SqlConnection con = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand getAbsenceTypesByTeamComm = new SqlCommand("sp_GetAbsencesByTeamID", con))
                    {
                        try
                        {
                            getAbsenceTypesByTeamComm.CommandType = CommandType.StoredProcedure;
                            getAbsenceTypesByTeamComm.CommandTimeout = 35;

                            getAbsenceTypesByTeamComm.Parameters.AddWithValue("@TeamId", iTeamID);
                            con.Open();

                            using (SqlDataReader reader = getAbsenceTypesByTeamComm.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    IAbsenceDO absence = new AbsenceDO();
                                    absence.PointBankID = (int)reader["PointBankID"];
                                    absence.EmployeeName = reader["EmployeeName"].ToString();
                                    absence.Name = reader["AbsenceType"].ToString();
                                    absence.Point = (decimal)reader["Point"];
                                    var active = (int)reader["Active"];
                                    absence.Active = active != 0;
                                    absence.TeamID_FK = (int)reader["TeamID_FK"];

                                    listAbsenceTypesByTeam.Add(absence);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "GetAbsencesByTeamID", "nothing");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "GetAbsencesByTeamID", "nothing");
            }

            return listAbsenceTypesByTeam;

        }

        // Gets selected absence by PointBankID
        public static IAbsenceDO GetAbsenceByID(int pointBankID)
        {
            var iAbsence = new AbsenceDO();
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetAbsenceByPointBankID", connection))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 35;

                            command.Parameters.AddWithValue("@PointBankID", pointBankID);
                            connection.Open();

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var absenceDO = new AbsenceDO();
                                    absenceDO.PointBankID = (int)reader["PointBankID"];
                                    absenceDO.EmployeeName = reader["Employee"].ToString();
                                    absenceDO.Name = reader["AbsenceType"].ToString();
                                    absenceDO.Point = (decimal)reader["Point"];
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "GetAbsenceByID", "nothing");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "GetAbsenceByID", "nothing");
            }
            return iAbsence;
        }

        
    }
}
