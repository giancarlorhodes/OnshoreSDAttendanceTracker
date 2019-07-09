using System;
using System.Collections.Generic;
using System.Configuration;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using OnshoreSDAttendanceTrackerErrorLogger;


namespace OnshoreSDAttendanceTrackerNetDAL
{
    public class AbsenceDataAccess
    {
        private string _ConnectionString = (ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString);

       

        // Gets absences for a given team -- Check points for duplication
        public List<IAbsenceDO> GetAbsencesByTeamID(int iTeamID)
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
        public IAbsenceDO GetAbsenceByID(int pointBankID)
        {
            var iAbsence = new AbsenceDO();
            try
            {
                using(SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using(SqlCommand command = new SqlCommand("sp_GetAbsenceByPointBankID", connection))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 35;

                            command.Parameters.AddWithValue("@PointBankID", pointBankID);
                            connection.Open();

                            using(SqlDataReader reader = command.ExecuteReader())
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
        
        // Retrieves absences by UserID(SM, TL)
        public List<IAbsenceDO> GetAbsencesAssociatedWithUserID(int userID)
        {
            var absenceDOs = new List<IAbsenceDO>();

            try
            {
                using(SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using(SqlCommand command = new SqlCommand("sp_GetAbsencesByUserID", connection))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 35;

                            command.Parameters.AddWithValue("@UserID", userID);
                            connection.Open();

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    IAbsenceDO absenceType = new AbsenceDO();                                    
                                    absenceType.EmployeeName = reader["Employee"].ToString();
                                    absenceType.TeamName = reader["Team"].ToString();
                                    absenceType.Name = reader["Type"].ToString();
                                    absenceType.AbsenceDate = (DateTime)reader["AbsenceDate"];
                                    absenceType.Point = (decimal)reader["Point"];
                                    absenceType.AbsenceTypeID = (int)reader["AbsenceTypeID"];
                                    absenceType.AbsentUserID = (int)reader["UserID"];
                                    absenceType.TeamMgtID = (int)reader["TeamManagementID"];
                                    absenceType.PointBankID = (int)reader["PointBankID"];

                                    absenceDOs.Add(absenceType);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "GetAbsencesByUserID", "nothing");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "GetAbsencesByUserID", "nothing");
            }

            return absenceDOs;
        }


        #region AbsenceTypes

        public string CreateAbsenceType(IAbsenceDO absence, int userID)
        {
            string result;
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_TeamAddNew", connection))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 35;

                            command.Parameters.AddWithValue("@AbsenceTypeID",SqlDbType.Int).Value = absence.AbsenceTypeID;
                            command.Parameters.AddWithValue("@AbsenceName",SqlDbType.VarChar).Value = absence.Name;
                            command.Parameters.AddWithValue("@Point",SqlDbType.Decimal).Value = absence.Point;
                            command.Parameters.AddWithValue("@Active",SqlDbType.Int).Value = absence.Active;
                            command.Parameters.AddWithValue("@TeamID_FK",SqlDbType.Int).Value = absence.TeamID_FK;
                            command.Parameters.AddWithValue("@CreateDate",SqlDbType.DateTime).Value = absence.AbsenceDate;
                            command.Parameters.AddWithValue("@CreateUser_FK",SqlDbType.Int).Value = userID;
                            command.Parameters.AddWithValue("@ModifiedDate",SqlDbType.DateTime).Value = DateTime.Now;
                            command.Parameters.AddWithValue("@ModifiedUser_FK",SqlDbType.Int).Value = userID;
                            command.ExecuteNonQuery();

                            result = "Success";
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "CreateAbsenceType", "nothing");
                            result = "fail";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLogger.LogError(e, "CreateAbsenceType", "nothing");
                result = "fail";
            }

            return result;
        }

        public List<IAbsenceDO> GetAllAbsenceTypes()
        {
            var listOfAbsenceTypes = new List<IAbsenceDO>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    using (SqlCommand getComm = new SqlCommand("sp_GetAllAbsenceTypes", conn))
                    {
                        getComm.CommandType = CommandType.StoredProcedure;
                        getComm.CommandTimeout = 35;
                        conn.Open();

                        using (SqlDataReader reader = getComm.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IAbsenceDO absenceType = new AbsenceDO();
                                absenceType.AbsenceTypeID = (int)reader["AbsenceTypeID"];
                                absenceType.Name = reader["Name"].ToString();
                                var active = (int)reader["Active"];
                                absenceType.Active = active != 0;

                                listOfAbsenceTypes.Add(absenceType);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "GetAbsenceTypes", "nothing");
            }

            return listOfAbsenceTypes;
        }

        public IAbsenceDO UpdateAbsenceType(IAbsenceDO iAbsence, int ModifiedByUserID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand updateComm = new SqlCommand("sp_UpdateAttendanceTypeById", conn))
                    {
                        try
                        {
                            updateComm.Parameters.AddWithValue("@AttendanceTypeId", SqlDbType.Int).Value = iAbsence.AbsenceTypeID;
                            updateComm.Parameters.AddWithValue("@ModifiedByUserId", SqlDbType.Int).Value = ModifiedByUserID;
                            updateComm.Parameters.AddWithValue("@Name", SqlDbType.VarChar).Value = iAbsence.Name;
                            updateComm.Parameters.AddWithValue("@Point", SqlDbType.Decimal).Value = iAbsence.Point;

                            return iAbsence;
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "UpdateAbsenceType", "nothing");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "UpdateAbsenceType", "nothing");
            }
            return iAbsence;
        }

        public string DeactivateAbsenceType(int absenceTypeID)
        {
            string result;

            try
            {
                using(SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using(SqlCommand command = new SqlCommand("sp_DeactiveAbsenceType", connection))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 35;

                            command.Parameters.Add(new SqlParameter("@AbsenceTypeID", absenceTypeID));
                            command.ExecuteNonQuery();

                            result = "Success";
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "DeactivateAbsenceType", "nothing");
                            result = "fail";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "DeactivateAbsenceType", "nothing");
                result = "fail";
            }

            return result;
        }

        #endregion

    }
}
