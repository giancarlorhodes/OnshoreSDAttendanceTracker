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

        #region CreateAbsence //creates an absence instance
        public void CreateAbsence(IAbsenceDO iAbsence, int createdByUserID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    // TODO: Need to fix issues with stored procedure decimal values
                    using (SqlCommand createComm = new SqlCommand("sp_MakeAttendanceTypeByUserId", conn))
                    {
                        try
                        {
                            createComm.CommandType = CommandType.StoredProcedure;
                            createComm.CommandTimeout = 35;

                            createComm.Parameters.AddWithValue("@TeamMgtId", SqlDbType.Int);                            
                            createComm.Parameters.AddWithValue("@Name", SqlDbType.Int).Value = iAbsence.Name;
                            createComm.Parameters.AddWithValue("@Point", SqlDbType.VarChar).Value = iAbsence.Point;
                            createComm.Parameters.AddWithValue("@CreatedByUserId", SqlDbType.Int).Value = createdByUserID;

                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "CreateAbsence", "nothing");
                        }

                        finally
                        {
                            conn.Close();
                            conn.Dispose();
                            createComm.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "CreateAbsence", "nothing");
            }

            finally
            {

            }
        }

        #endregion CreateAbsence



        #region GetAbsenceTypes

        public List<IAbsenceDO> GetAbsenceTypes()
        {
            var listOfAbsenceTypes = new List<IAbsenceDO>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    using (SqlCommand getComm = new SqlCommand("sp_GetAttendances"))
                    {
                        getComm.CommandType = CommandType.StoredProcedure;
                        getComm.CommandTimeout = 35;
                        conn.Open();

                        using (SqlDataReader reader = getComm.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IAbsenceDO absenceType = new AbsenceDO();
                                absenceType.AbsenceTypeID = reader.GetInt32(reader.GetOrdinal("AbsenceTypeID"));
                                absenceType.Name = (string)reader["Name"];
                                absenceType.Point = reader.GetInt32(reader.GetOrdinal("Point"));
                                absenceType.Active = (bool)reader["Active"];
                                absenceType.TeamID_FK = reader.GetInt32(reader.GetOrdinal("TeamID"));

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
                                IAbsenceDO absenceType = new AbsenceDO();
                                absenceType.AbsenceTypeID = reader.GetInt32(reader.GetOrdinal("AbsenceTypeID"));
                                absenceType.Name = (string)reader["Name"];
                                absenceType.Point = reader.GetInt32(reader.GetOrdinal("Point"));
                                absenceType.Active = (bool)reader["Active"];
                                absenceType.TeamID_FK = reader.GetInt32(reader.GetOrdinal("TeamID"));

                                listAbsenceTypesByTeam.Add(absenceType);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "GetAbsenceTypesByTeamID", "nothing");
                        }
                        finally
                        {
                            con.Close();
                            con.Dispose();
                            getAbsenceTypesByTeamComm.Dispose();
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

        public List<IAbsenceDO> GetAbsenceTypesForSMByTeamID(int userID, int teamID)
        {
            throw new NotImplementedException();
        }

        #endregion UpdateAbsenceType

    }
}
