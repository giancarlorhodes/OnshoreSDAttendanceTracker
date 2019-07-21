using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using OnshoreSDAttendanceTrackerErrorLogger;

namespace OnshoreSDAttendanceTrackerNetDAL
{
    public class UserDataAccess
    {
        public string ConnectionParms { get; private set; } = ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString;
      
        #region CreateUser
        public bool CreateUser(IUserDO iUser, int TeamID)
        {
            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionParms))
                {
                    using (SqlCommand createComm = new SqlCommand("sp_MakeUser", conn))
                    {
                     
                            createComm.CommandType = CommandType.StoredProcedure;
                            createComm.CommandTimeout = 35;

                            createComm.Parameters.AddWithValue("@CreatedByUserId", SqlDbType.Int).Value = iUser.UserID;
                            createComm.Parameters.AddWithValue("@TeamId", SqlDbType.Int).Value = TeamID;
                            createComm.Parameters.AddWithValue("@RoleId", SqlDbType.Int).Value = iUser.RoleID_FK;
                            createComm.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = iUser.Email;
                            createComm.Parameters.AddWithValue("@FName", SqlDbType.VarChar).Value = iUser.FirstName;
                            createComm.Parameters.AddWithValue("@LName", SqlDbType.VarChar).Value = iUser.LastName;
                        conn.Open();
                        createComm.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "CreateUser", "nothing");
            }
            return result;
        }

        #endregion

        #region Gets

        public IUserDO GetUserByID(int iUserID)
        {
            IUserDO user = new UserDO();

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionParms))
                {
                    using (SqlCommand getUserComm = new SqlCommand("sp_GetUserById", con))
                    {

                            getUserComm.CommandType = CommandType.StoredProcedure;
                            getUserComm.CommandTimeout = 35;

                            getUserComm.Parameters.AddWithValue("@UserId", iUserID);

                            con.Open();

                            using (SqlDataReader reader = getUserComm.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    user.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));
                                    user.FirstName = (string)reader["FirstName"];
                                    user.LastName = (string)reader["LastName"];
                                    user.RoleID_FK = reader.GetInt32(reader.GetOrdinal("RoleID"));
                                    user.Email = (string)reader["Email"];
                                    // user.TeamID = reader.GetInt32(reader.GetOrdinal("TeamID"));
                                    //user.TeamManagementID = reader.GetInt32(reader.GetOrdinal("TeamManagementID"));
                                }
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

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionParms))
                {
                    using (SqlCommand getComm = new SqlCommand("sp_GetUsers"))
                    {
                        getComm.CommandType = CommandType.StoredProcedure;
                        getComm.CommandTimeout = 35;

                        getComm.Connection = conn;
                        conn.ConnectionString = ConnectionParms;
                        conn.Open();

                        using (SqlDataReader reader = getComm.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IUserDO user = new UserDO();
                                user.UserID = (int)reader["UserID"];
                                user.FirstName = (string)reader["FirstName"];
                                user.LastName = (string)reader["LastName"];
                                user.RoleID_FK = (int)reader["RoleID"];
                                user.Email = (string)reader["Email"];
                                user.Active = Convert.ToBoolean(reader["Active"]);
                                user.TeamID =(int) reader["TeamID"];
                                // user.NewTeamID = reader.GetInt32(reader.GetOrdinal("TeamID"));

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
        public void UpdateUser(IUserDO iUser)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionParms))
                {
                    using (SqlCommand updateComm = new SqlCommand("sp_UpdateUser", conn))
                    {
                        try
                        {
                            updateComm.Parameters.AddWithValue("@UserID", iUser.UserID);
                            updateComm.Parameters.AddWithValue("@ModifiedByUserId", iUser.UserID);
                            updateComm.Parameters.AddWithValue("@RoleId", iUser.RoleID_FK);
                            updateComm.Parameters.AddWithValue("@Email", iUser.Email);
                            updateComm.Parameters.AddWithValue("@FName", iUser.FirstName);
                            updateComm.Parameters.AddWithValue("@LName", iUser.LastName);  
                            // updateComm.Parameters.AddWithValue("@TeamId", iUser.TeamID);
                           // updateComm.Parameters.AddWithValue("@TeamManagementId", iUser.TeamManagementID);


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
                using (SqlConnection conn = new SqlConnection(ConnectionParms))
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

        public List<RoleDO> GetAllRoles()
        {
            List<RoleDO> listOfRoles = new List<RoleDO>();
            SqlCommand getComm = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionParms))
                {

                    using (getComm = new SqlCommand("sp_GetRoles"))
                    {
                        getComm.CommandType = CommandType.StoredProcedure;
                        getComm.CommandTimeout = 35;

                        getComm.Connection = conn;
                        conn.ConnectionString = ConnectionParms;
                        conn.Open();

                        using (SqlDataReader reader = getComm.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RoleDO role = new RoleDO();
                               role.RoleID = reader.GetInt32(reader.GetOrdinal("RoleID"));
                               role.RoleNameShort = (string)reader["RoleNameShort"];
                               role.RoleNameLong = (string)reader["RoleNameLong"];
                               role.Comment = (string)reader["Comment"];

                                listOfRoles.Add(role);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "GetAllUsers", "nothing");
            }
            return listOfRoles;
        }

    }
}
