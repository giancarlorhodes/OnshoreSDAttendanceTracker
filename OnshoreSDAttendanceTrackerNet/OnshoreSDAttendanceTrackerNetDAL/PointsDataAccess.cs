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
    public class PointsDataAccess
    {
        private string _ConnectionString;

        public PointsDataAccess(string iConnectionString)
        {
            _ConnectionString = iConnectionString;
        }

        public void AddPoints(IAttendanceDO iAttendance)
        {
            try
            {
                // TODO: Pass connection string when all layers are built
                using(SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_PointsUpdate", conn))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 15;

                            // SqlDbType followed by datatype whitelisting tells mvc what to expect for the parameter
                            command.Parameters.AddWithValue("@PointValue", SqlDbType.Int);
                            command.Parameters.AddWithValue("@AbsenceTypeID", SqlDbType.Int);
                            command.Parameters.AddWithValue("@TeamManagementID", SqlDbType.Int);
                            command.Parameters.AddWithValue("@CreateUserID", SqlDbType.Int);

                            conn.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
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
                ErrorLogger.LogError(ex, "AddPoints", "");
            }
        }

        public IAttendanceDO ViewPointsByID(int iAttendanceID)
        {
            var listOfAttendanceDOs = new AbsenceDO();

            try
            {
                using(SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[""].ConnectionString))
                {
                    using(SqlCommand viewComm  = new SqlCommand("sp_GetPointsByID", conn))
                    {
                        viewComm.CommandType = CommandType.StoredProcedure;
                        viewComm.CommandTimeout = 35;
                        conn.Open();

                        using (SqlDataReader reader = viewComm.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IAttendanceDO attendanceDO = new AbsenceDO();

                                attendanceDO.AbsenceTypeID = reader.GetInt32(0);
                                attendanceDO.Name = reader.GetString(1);
                                attendanceDO.Point = reader.GetDecimal(2);
                                attendanceDO.Active = reader.GetBoolean(3);
                                attendanceDO.TeamID_FK = reader.GetInt32(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return listOfAttendanceDOs;
        }

        public void UdpateAttendanceInformation(IAttendanceDO iAttendance)
        {
            var selectedAttendance = new AbsenceDO();

            try
            {
                using(SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[""].ConnectionString))
                {
                    using(SqlCommand updateComm = new SqlCommand("sp_PointsUpdate", conn))
                    {
                        try
                        {
                            updateComm.CommandType = CommandType.StoredProcedure;
                            updateComm.CommandTimeout = 35;

                            updateComm.Parameters.AddWithValue("@PointBankID", SqlDbType.Int);
                            updateComm.Parameters.AddWithValue("@@PointValue", SqlDbType.Int);
                            updateComm.Parameters.AddWithValue("@AbsenceTypeID", SqlDbType.Int);
                            updateComm.Parameters.AddWithValue("@TeamManagementID", SqlDbType.Int);
                            updateComm.Parameters.AddWithValue("@UpdateUserID", SqlDbType.Int);
                            updateComm.Parameters.AddWithValue("@Active", SqlDbType.Int);

                            var attendanceID = selectedAttendance.AbsenceTypeID;
                            var id = 0;

                            // Ensures ID is valid before executing request: Whitelists the expected ID
                            if (!int.TryParse(attendanceID.ToString(), out id))
                            {
                                throw new ApplicationException("Attendance ID was not an integer");
                            }

                            conn.Open();
                            updateComm.ExecuteNonQuery();
                        }
                        catch (Exception)
                        {
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
                ErrorLogger.LogError(ex, "UdpateAttendanceInformation", "");
            }
        }
    }
}
