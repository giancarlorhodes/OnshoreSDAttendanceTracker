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
        private string _ConnectionString = (ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString);

        public UserDataAccess(string iConnectionString)
        {
            _ConnectionString = iConnectionString;
        }
        #region CreateUser
        public void CreateUser(IUserDO iUser)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand createComm = new SqlCommand("sp_MakeUser", conn))
                    {
                        try
                        {
                            createComm.CommandType = CommandType.StoredProcedure;
                            createComm.CommandTimeout = 35;

                            createComm.Parameters.AddWithValue("@CreatedByUserId", SqlDbType.Int).Value = iUser.UserID;
                            createComm.Parameters.AddWithValue("@TeamId", SqlDbType.Int).Value = iUser.TeamID;
                            createComm.Parameters.AddWithValue("@RoldeId", SqlDbType.Int).Value = iUser.RoleID_FK;
                            createComm.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = iUser.Email;
                            createComm.Parameters.AddWithValue("@FName", SqlDbType.VarChar).Value = iUser.FirstName;
                            createComm.Parameters.AddWithValue("@LName", SqlDbType.VarChar).Value = iUser.LastName;

                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "CreateUser", "nothing");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "CreateUser", "nothing");
            }

            finally
            {

            }
        }

        #endregion

        #region Gets

        public IUserDO GetUserByID(int iUserID)
        {
            IUserDO user = new UserDO();

            try
            {
                using (SqlConnection con = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand getUserComm = new SqlCommand("sp_GetUserById", con))
                    {
                        try
                        {
                            getUserComm.CommandType = CommandType.StoredProcedure;
                            getUserComm.CommandTimeout = 35;

                            getUserComm.Parameters.AddWithValue("@UserId", iUserID);
                            con.Open();

                            using (SqlDataReader reader = getUserComm.ExecuteReader())
                            {
                                user.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                                user.FirstName = (string)reader["FirstName"];
                                user.LastName = (string)reader["LastName"];
                                user.RoleID_FK = reader.GetInt32(reader.GetOrdinal("RoleID"));
                                user.Email = (string)reader["Email"];
                                user.TeamID = reader.GetInt32(reader.GetOrdinal("TeamID"));
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "GetUserByID", "nothing");
                        }
                        finally
                        {
                            con.Close();
                            con.Dispose();
                            getUserComm.Dispose();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "GetUserByID", "nothing");
            }

            return user;

        }





        public List<IUserDO> GetAllUsers()
        {
            var listOfDBUsers = new List<IUserDO>();
            SqlCommand getComm=null;
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {

                    using (getComm = new SqlCommand("sp_GetUsers"))
                    {
                        getComm.CommandType = CommandType.StoredProcedure;
                        getComm.CommandTimeout = 35;
                        conn.Open();

                        using (SqlDataReader reader = getComm.ExecuteReader())
                        {
                            while (reader.Read())
                            {
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
                ErrorLogger.LogError(ex, "GetAllUsers", "nothing");
            }

            return listOfDBUsers;
        }

        #endregion

        #region Updates
        //updates User info and takes in OldTeamID to update in SP where UserID & OldTeamID equal in TeamManagement table
        public void UpdateUser(IUserDO iUser, int OldTeamID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand updateComm = new SqlCommand("sp_UpdateUser", conn))
                    {
                        try
                        {
                            updateComm.Parameters.AddWithValue("@UserID", iUser.UserID);
                            updateComm.Parameters.AddWithValue("@ModifiedByUserId", iUser.UserID);
                            updateComm.Parameters.AddWithValue("@RoleId", iUser.RoleID_FK);
                            updateComm.Parameters.AddWithValue("@OldTeamId", OldTeamID);
                            updateComm.Parameters.AddWithValue("@NewTeamId", iUser.TeamID);
                            updateComm.Parameters.AddWithValue("@Email", iUser.Email);
                            updateComm.Parameters.AddWithValue("@FName", iUser.FirstName);
                            updateComm.Parameters.AddWithValue("@LName", iUser.LastName);

                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "UpdateUser", "nothing");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "UpdateUser", "nothing");
            }
        }

        #endregion

        #region Deletes
        public void RemoveUser(int userToDelID, int modifiedByUserID)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand deleteComm = new SqlCommand("sp_RemoveUserById", conn))
                    {
                        try
                        {
                            deleteComm.CommandType = CommandType.StoredProcedure;
                            deleteComm.CommandTimeout = 35;
                            deleteComm.Parameters.AddWithValue("@UserId", userToDelID);
                            deleteComm.Parameters.AddWithValue("@ModifiedByUserId ", modifiedByUserID);

                            deleteComm.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "UpdateUser", "nothing");
                        }
                        finally
                        {
                            conn.Close();
                            conn.Dispose();
                            deleteComm.Dispose();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "UpdateUser", "nothing");
            }
        }
        #endregion


    }
}
