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
        //GetTeamByID
        public ITeamDO GetTeamByID(int teamID)
        {
            ITeamDO newTeam = new TeamDO();

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

                            com.Parameters.Add(new SqlParameter("@TeamID", teamID));
                            con.Open();
                            using (SqlDataReader reader = com.ExecuteReader())
                            {
                                newTeam.TeamID = reader.GetInt32(reader.GetOrdinal("TeamID"));
                                newTeam.Name = reader["Name"].ToString();
                                newTeam.Comment = reader["Comment"].ToString();
                                var active = reader.GetInt32(3);
                                newTeam.Active = active != 0;
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

        public List<ITeamDO> GetAllTeamsByID(int userID)
        {
            var listOfTeams = new List<ITeamDO>();
            var newTeam = new TeamDO();
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetAllTeamsByID", connection))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 35;

                            command.Parameters.Add(new SqlParameter("UserID", userID));
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                newTeam.TeamID = reader.GetInt32(reader.GetOrdinal("TeamID"));
                                newTeam.Name = reader["Name"].ToString();
                                newTeam.Comment = reader["Comments"].ToString();
                                var active = reader.GetInt32(3);
                                newTeam.Active = active != 0;
                                listOfTeams.Add(newTeam);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "GetAllTeamsByID", "nothing");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex, "GetAllTeamsByID", "nothing");
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
                                newUser.FirstName = reader["FirstName"].ToString();
                                newUser.LastName = reader["LastName"].ToString();
                                newUser.Email = reader["Email"].ToString();
                                newUser.Active = (bool)reader["Active"];
                                listOfUsers.Add(newUser);
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
                ErrorLogger.LogError(e, "DeleteTeam", "nothing");
                result = "fail";
            }

            return result;
        }
    }
}
