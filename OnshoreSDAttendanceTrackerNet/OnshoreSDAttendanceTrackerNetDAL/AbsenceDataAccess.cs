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

        #region GetAbsenceTypes

        // TODO: Implement with correct SP this is calling Absence Types not absences
        // Retrieves all absences -- Check Points for duplication
        public List<IAbsenceDO> GetAllAbsences()
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
                                // TODO: Associate with columns
                                IAbsenceDO absenceType = new AbsenceDO();
                                absenceType.AbsenceTypeID = reader.GetInt32(0);
                                absenceType.Name = reader.GetString(1);
                                var active = reader.GetInt32(2);
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

        // Gets absences for a given team -- Check points for duplication
        public List<IAbsenceDO> GetAbsenceTypesByTeamID(int iTeamID)
        {
            List<IAbsenceDO> listAbsenceTypesByTeam = new List<IAbsenceDO>();

            try
            {
                using (SqlConnection con = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand getAbsenceTypesByTeamComm = new SqlCommand("sp_GetAttendanceTypesByTeamID", con))
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
                                    // TODO: Associate with column name SQL
                                    IAbsenceDO absenceType = new AbsenceDO();
                                    absenceType.AbsenceTypeID = reader.GetInt32(0);
                                    absenceType.EmployeeName = reader.GetString(1);
                                    absenceType.Name = reader.GetString(2);
                                    absenceType.Point = reader.GetDecimal(3);                                     
                                    var active = reader.GetInt32(4);
                                    absenceType.Active = active != 0;
                                    absenceType.TeamID_FK = reader.GetInt32(5);

                                    listAbsenceTypesByTeam.Add(absenceType);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "GetAbsenceTypesByTeamID", "nothing");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "GetAbsenceTypesByTeamID", "nothing");
            }

            return listAbsenceTypesByTeam;

        }

        // Gets selected absence by PointBankID -- Check points for duplication
        // TODO: Add PointBankID to the query
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
                                    // TODO: Associate with SQL columns
                                    var absenceDO = new AbsenceDO();
                                    absenceDO.PointBankID = reader.GetInt32(0);
                                    absenceDO.EmployeeName = reader.GetString(1);
                                    absenceDO.Name = reader.GetString(2);
                                    absenceDO.Point = reader.GetDecimal(3);
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

        #endregion GetAbsenceTypes


        #region UpdateAbsenceType

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
                            updateComm.Parameters.AddWithValue("@AttendanceTypeId", iAbsence.AbsenceTypeID);
                            updateComm.Parameters.AddWithValue("@ModifiedByUserId", ModifiedByUserID);
                            updateComm.Parameters.AddWithValue("@Name", iAbsence.Name);
                            updateComm.Parameters.AddWithValue("@Point", iAbsence.Point);

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
                                    // Associate with SQL columns
                                    IAbsenceDO absenceType = new AbsenceDO();                                    
                                    absenceType.EmployeeName = reader.GetString(0);
                                    absenceType.TeamName = reader.GetString(1);
                                    absenceType.Name = reader.GetString(2);
                                    absenceType.AbsenceDate = reader.GetDateTime(3);
                                    absenceType.Point = reader.GetDecimal(4);
                                    absenceType.AbsenceTypeID = reader.GetInt32(5);
                                    absenceType.AbsentUserID = reader.GetInt32(6);
                                    absenceType.TeamMgtID = reader.GetInt32(7);

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

        #endregion Absences

        #region AbsenceTypes
        // TODO: Add Absence Types for admininstrative actions for CRUD
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
                                // TODO: Associate with columns
                                IAbsenceDO absenceType = new AbsenceDO();
                                absenceType.AbsenceTypeID = reader.GetInt32(0);
                                absenceType.Name = reader.GetString(1);
                                var active = reader.GetInt32(2);
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
        #endregion

    }
}
