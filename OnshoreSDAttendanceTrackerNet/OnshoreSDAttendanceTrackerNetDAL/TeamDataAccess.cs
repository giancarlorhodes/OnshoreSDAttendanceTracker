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
using OnshoreSDAttendanceTrackerErroLogger;

namespace OnshoreSDAttendanceTrackerNetDAL
{
    public class TeamDataAccess
    {
        private string conString = ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString;

        public TeamDataAccess(string connString)
        {
            conString = connString;
        }
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
                            ITeamDO newTeam = new TeamDO();
                            newTeam.TeamID = reader.GetInt32(reader.GetOrdinal("TeamID"));
                            newTeam.Name = reader["Name"].ToString();
                            newTeam.Comment = reader["Comment"].ToString();
                            newTeam.Active = (bool)reader["Active"];
                            teams.Add(newTeam);
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
                                newTeam.Active = (bool)reader["Active"];
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
        //DELETe
        public string DeleteTeam( int teamID)
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
