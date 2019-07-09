using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using OnshoreSDAttendanceTrackerErrorLogger;

namespace OnshoreSDAttendanceTrackerNetDAL
{
    public class TeamDataAccess
    {
        private string conString = ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString;

        //public TeamDataAccess(string connString)
        //{
        //    conString = connString;
        //}
        //CREATe
        public string CreateNewTeam(ITeamDO newTeam, int userID)
        {
            string result;
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand com = new SqlCommand("sp_TeamAddNew", con))
                    {
                        try
                        {
                            com.CommandType = CommandType.StoredProcedure;
                            com.CommandTimeout = 35;

                            com.Parameters.Add(new SqlParameter("@TeamName", newTeam.Name));
                            com.Parameters.Add(new SqlParameter("@Comment", newTeam.Comment));
                            com.Parameters.Add(new SqlParameter("@CreatedUser", userID));
                            com.ExecuteNonQuery();

                            result = "Success";
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "CreateTeams", "nothing");
                            result = "fail";
                        }
                        finally
                        {
                            con.Close();
                            con.Dispose();
                            con.Dispose();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLogger.LogError(e, "CreateTeams", "nothing");
                result = "fail";
            }

            return result;
        }
        //READ
        public List<ITeamDO> GetAllTeams()
        {
            var teams = new List<ITeamDO>();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand com = new SqlCommand("sp_GetTeams", con))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandTimeout = 35;

                        con.Open();
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // TODO: Access the actual column instead of the using the index for preventing issues with extensibility
                                ITeamDO newTeam = new TeamDO();
                                newTeam.TeamID = reader.GetInt32(0);
                                newTeam.Name = reader.GetString(1);
                                newTeam.Comment = reader["Comment"].ToString();
                                var active = reader.GetInt32(3);
                                newTeam.Active = active != 0;
                                teams.Add(newTeam);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLogger.LogError(e, "GetAllTeams", "nothing");

            }
            return teams;
        }

        // Retrieves all teams assigned to a service manager
        public List<ITeamDO> GetAllSMTeams()
        {
            var teams = new List<ITeamDO>();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand com = new SqlCommand("sp_GetAllSMTeams", con))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandTimeout = 35;

                        con.Open();
                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // TODO: Access the actual column instead of the using the index for preventing issues with extensibility
                                ITeamDO newTeam = new TeamDO();
                                newTeam.TeamID = reader.GetInt32(0);
                                newTeam.Name = reader.GetString(1);
                                newTeam.Comment = reader["Comment"].ToString();
                                var active = reader.GetInt32(3);
                                newTeam.Active = active != 0;
                                teams.Add(newTeam);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLogger.LogError(e, "GetAllTeams", "nothing");

            }
            return teams;
        }

        // TODO: Find a better way if time allows
        //GetTeamByID
        public ITeamDO GetTeamNameByID(int teamID)
        {
            ITeamDO newTeam = new TeamDO();

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand com = new SqlCommand("sp_getTeamNameByID", con))
                    {
                        try
                        {
                            com.CommandType = CommandType.StoredProcedure;
                            com.CommandTimeout = 35;

                            com.Parameters.Add(new SqlParameter("@TeamID", teamID));
                            con.Open();
                            using (SqlDataReader reader = com.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    newTeam.Name = reader.GetString(0);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "GetTeamByID", "nothing");
                        }
                        finally
                        {
                            con.Close();
                            con.Dispose();
                            con.Dispose();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLogger.LogError(e, "GetTeamByID", "nothing");
            }

            return newTeam;
        }

        // Retrieves all SM teams
        public List<ITeamDO>GetAllSMTeamAbsencesByUserID (int userID)
        {
            var listOfTeams = new List<ITeamDO>();
            var newTeam = new TeamDO();
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetAllSMTeamsByUserID", connection))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 35;

                            command.Parameters.Add(new SqlParameter("@UserId", userID));
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    // TODO: Refactor to access column in SP
                                    newTeam.TeamID = (int)reader["TeamID"];
                                    newTeam.Name = reader["Name"].ToString();
                                    newTeam.RunningTotal = (int)reader["Points"];
                                    listOfTeams.Add(newTeam);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "GetAllSMTeamAbsencesByUserID", "nothing");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "GetAllSMTeamAbsencesByUserID", "nothing");
            }
            return listOfTeams;
        }

        //UPDATE
        public string UpdateTeam(ITeamDO newTeam, int userID)
        {
            string result;
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand com = new SqlCommand("sp_TeamUpdate", con))
                    {
                        try
                        {
                            com.CommandType = CommandType.StoredProcedure;
                            com.CommandTimeout = 35;

                            com.Parameters.Add(new SqlParameter("@TeamID", newTeam.TeamID));
                            com.Parameters.Add(new SqlParameter("@TeamName", newTeam.Name));
                            com.Parameters.Add(new SqlParameter("@Comment", newTeam.Comment));
                            com.Parameters.Add(new SqlParameter("@UpdatedUser", userID));
                            com.ExecuteNonQuery();

                            result = "Success";
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "UpdateTeams", "nothing");
                            result = "fail";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLogger.LogError(e, "UpdateTeams", "nothing");
                result = "fail";
            }

            return result;
        }

        public List<IUserDO> ViewUsersByTeamID(int teamId)
        {
            var listOfUsers = new List<IUserDO>();
            var newUser = new UserDO();
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    using (SqlCommand command = new SqlCommand("sp_ViewUsersByTeamID", connection))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 35;

                            command.Parameters.Add(new SqlParameter("TeamId", teamId));
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    // TODO: Change to access columns
                                    newUser.FirstName = reader.GetString(0);
                                    newUser.LastName = reader.GetString(1);
                                    newUser.Email = reader.GetString(2);
                                    var active = reader.GetInt32(3);
                                    newUser.Active = active != 0;
                                    listOfUsers.Add(newUser);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "ViewUsersByTeamID", "nothing");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "ViewUsersByTeamID", "nothing");
            }
            return listOfUsers;
        }

        // TODO: Implement correct DA call for retrieving full object for update

        //DELETe
        public string DeactivateTeam(int teamID)
        {
            string result;
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand com = new SqlCommand("sp_TeamDeleteByID", con))
                    {
                        try
                        {
                            com.CommandType = CommandType.StoredProcedure;
                            com.CommandTimeout = 35;

                            com.Parameters.Add(new SqlParameter("@TeamID", teamID));
                            com.ExecuteNonQuery();

                            result = "Success";
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "DeleteTeam", "nothing");
                            result = "fail";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLogger.LogError(e, "DeleteTeam", "nothing");
                result = "fail";
            }

            return result;
        }
    }
}
