namespace OnshoreSDAttendanceTrackerNetDAL
{
    using OnshoreSDAttendanceTrackerErrorLogger;
    using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
    using OnshoreSDAttendanceTrackerNetDAL.Models;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Company: Onshore Outsourcing, https://www.onshoreoutsourcing.com/
    /// Author: Giancarlo Rhodes
    /// Description: Database access methods for user credentials
    /// </summary>
    public class UserCredentialsDataAccess
    {

        private string _ConnectionString = (ConfigurationManager.
            ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString);

        #region Constructors

        public UserCredentialsDataAccess(string iConnectionString)
        {
            _ConnectionString = iConnectionString;
        }

        public UserCredentialsDataAccess() { }

        #endregion

        #region Add User Credentials
        public void CreateUserCredentials(IUserCredentialsDO iUserCredentials)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand createComm = new SqlCommand("sp_UserCredentialsAddOrEdit", conn))
                    {
                        try
                        {
                            createComm.CommandType = CommandType.StoredProcedure;
                            createComm.CommandTimeout = 35;
                            createComm.Parameters.AddWithValue("@CreatedByUserId", SqlDbType.Int).Value = iUserCredentials.UserCredentailsID;
                            createComm.Parameters.AddWithValue("@TeamId", SqlDbType.Int).Value = iUserCredentials.UserID_FK;
                            createComm.Parameters.AddWithValue("@parmUserPassword", SqlDbType.VarChar).Value = iUserCredentials.UserPassword;
                            createComm.Parameters.AddWithValue("@parmSalt", SqlDbType.VarChar).Value = iUserCredentials.Salt;

                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "CreateUserCredentials", "nothing");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "CreateUserCredentials", "nothing");
            }

            finally
            {

            }
        }

      


        #endregion

        #region Edit User Credentials
        public void EditUserCredentials(IUserCredentialsDO iUserCredentials)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand createComm = new SqlCommand("sp_UserCredentialsAddOrEdit", conn))
                    {
                        try
                        {
                            createComm.CommandType = CommandType.StoredProcedure;
                            createComm.CommandTimeout = 35;
                            createComm.Parameters.AddWithValue("@CreatedByUserId", SqlDbType.Int).Value = iUserCredentials.UserCredentailsID;
                            createComm.Parameters.AddWithValue("@TeamId", SqlDbType.Int).Value = iUserCredentials.UserID_FK;
                            createComm.Parameters.AddWithValue("@parmUserPassword", SqlDbType.VarChar).Value = iUserCredentials.UserPassword;
                            createComm.Parameters.AddWithValue("@parmSalt", SqlDbType.VarChar).Value = iUserCredentials.Salt;

                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "CreateUserCredentials", "nothing");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "CreateUserCredentials", "nothing");
            }

            finally
            {

            }
        }

        #endregion

        #region Get or check credentials

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<IUserLoginDO> GetAllUsersLogin()
        {

            List<IUserLoginDO> _list = new List<IUserLoginDO>();

            // TODO - implement database call here
            using (SqlConnection conn = new SqlConnection(_ConnectionString))
            {

                // create a sqlcommand 
                using (SqlCommand command = new SqlCommand("sp_GetAllUserLogin", conn))
                {

                    // details to the select command
                    command.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    // need a loop to get users from the database
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            UserLoginDO _userLoginDO = new UserLoginDO();
                            _userLoginDO.UserID = (int)reader["UserID"];
                            _userLoginDO.FirstName = (string)reader["FirstName"];
                            _userLoginDO.LastName = (string)reader["LastName"];
                            _userLoginDO.Password = (string)reader["Password"];
                            _userLoginDO.Salt = (string)reader["Salt"];
                            _userLoginDO.Email = (string)reader["Email"];
                            _userLoginDO.RoleID_FK = (int)reader["RoleID"];
                            _userLoginDO.RoleNameShort = (string)reader["RoleNameShort"];
                            _userLoginDO.RoleNameLong = (string)reader["RoleNameLong"];

                            //if ((string)reader["Role"] == RoleType.ADMINISTRATOR.ToString())
                            //{
                            //    _userDO.Role = RoleType.ADMINISTRATOR;
                            //}
                            //else
                            //{
                            //    _userDO.Role = RoleType.CUSTOMER;
                            //}
                            _list.Add(_userLoginDO);
                        }
                    }
                }
            }

            return _list;

        }


        // <summary>
        /// Company: Onshore Outsourcing, https://www.onshoreoutsourcing.com/
        /// Author: Giancarlo Rhodes
        /// Description: Database access methods for user credentials
        /// <param name="username"></param>
        /// <param name="userpassword"></param>
        /// <returns></returns>
        public IUserLoginDO GetUserLoginInformation(string username, string userpassword)
        {

            //// hard coding right now for testing only 
            //// remove this soon
            //IUserLoginDO _userLoginDO = new UserLoginDO();
            //_userLoginDO.UserID = 0;
            //_userLoginDO.FirstName = "system";
            //_userLoginDO.LastName ="system";
            //_userLoginDO.Password = "unhashedunsaltedsuperadminpassword";
            //_userLoginDO.Salt = "";
            //_userLoginDO.Email = "giancarlo.rhodes@onshoreoutsourcing.com";
            //_userLoginDO.RoleID_FK = 1;
            //_userLoginDO.RoleNameShort = "ADMIN";
            //_userLoginDO.RoleNameLong = "Administrator";

            //TODO GSR - make this a db call


            IUserLoginDO _userLoginDO =  new UserLoginDO();
            try
            {
                using (SqlConnection conn = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand viewComm = new SqlCommand("sp_UserCredentialCheckReturnUserInfo", conn))
                    {
                        viewComm.CommandType = CommandType.StoredProcedure;
                        viewComm.CommandTimeout = 35;
                        viewComm.Parameters.AddWithValue("@parmUserEmail", SqlDbType.VarChar).Value = username;
                        viewComm.Parameters.AddWithValue("@parmUserPassword", SqlDbType.VarChar).Value = userpassword;
                        conn.Open();

                        using (SqlDataReader reader = viewComm.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _userLoginDO.UserID = (int)reader["UserID"];
                                _userLoginDO.FirstName = (string)reader["FirstName"];
                                _userLoginDO.LastName = (string)reader["LastName"];
                                _userLoginDO.Password = (string)reader["UserPassword"];
                                _userLoginDO.Salt = (string)reader["Salt"];
                                _userLoginDO.Email = (string)reader["Email"];
                                _userLoginDO.RoleID_FK = (int)reader["RoleID"];
                                _userLoginDO.RoleNameShort = (string)reader["RoleNameShort"];
                                _userLoginDO.RoleNameLong = (string)reader["RoleNameLong"];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return _userLoginDO;
        }


        public  bool IsAutheticatedAgainstDatabase(string username, string userpassword)
        {
            var _user = this.GetUserLoginInformation(username, userpassword);

            if (_user.RoleID_FK != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion



    }
}
