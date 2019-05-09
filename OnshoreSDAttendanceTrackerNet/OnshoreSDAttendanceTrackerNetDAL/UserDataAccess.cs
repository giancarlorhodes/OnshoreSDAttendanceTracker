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
    public class UserDataAccess
    {
        private string _ConnectionString;

        public UserDataAccess(string iConnectionString)
        {
            _ConnectionString = iConnectionString;
        }

        public void CreateUser(IUserDO iUser)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString))
                {
                    using (SqlCommand comm = new SqlCommand("sp_MakeUser", conn))
                    {
                        try
                        {
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.CommandTimeout = 35;

                            comm.Parameters.AddWithValue("@CreatedByUserId", SqlDbType.Int).Value = iUser.UserID;
                            comm.Parameters.AddWithValue("@TeamId", SqlDbType.Int).Value = iUser.TeamID;
                            comm.Parameters.AddWithValue("@RoldeId", SqlDbType.Int).Value = iUser.RoleID_FK;
                            comm.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = iUser.Email;
                            comm.Parameters.AddWithValue("@FName", SqlDbType.VarChar).Value = iUser.FirstName;
                            comm.Parameters.AddWithValue("@LName", SqlDbType.VarChar).Value = iUser.LastName;

                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                     
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }


        public List<IUserDO> GetAllUsers()
        {
            var listOfDBUsers = new List<IUserDO>();
            SqlCommand getComm=null;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString))
                {

                    using (getComm = new SqlCommand("sp_GetUsers"))
                    {
                        getComm.CommandType = CommandType.StoredProcedure;
                        getComm.CommandTimeout = 35;
                        conn.Open();

                        using (SqlDataReader reader = getComm.ExecuteReader())
                        {
                            while (reader.Read())
                            {//UserID,FirstName,LastName,RoleID_FK as RoleID,Email
                                IUserDO user = new UserDO();
                                user.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                                user.FirstName = (string)reader["FirstName"];
                                user.LastName = (string)reader["LastName"];
                                user.RoleID_FK = reader.GetInt32(reader.GetOrdinal("RoleID_FK"));
                                user.Email = (string)reader["Email"];
                                user.TeamID = reader.GetInt32(reader.GetOrdinal("TeamID"));

                                listOfDBUsers.Add(user);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, getComm.CommandText);
            }

            return listOfDBUsers;
        }




    }
}
