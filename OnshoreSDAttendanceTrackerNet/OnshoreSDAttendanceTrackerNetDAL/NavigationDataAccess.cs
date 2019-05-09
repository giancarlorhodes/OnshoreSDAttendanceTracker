using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using OnshoreSDAttendanceTrackerErrorLogger;
using System.Data;
using System.Data.SqlClient;

namespace OnshoreSDAttendanceTrackerNetDAL
{
    public class NavigationDataAccess
    {
        private string _conString = ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString;


        //READ
        public List<INavigationDO> GetNavigationItemsByRoleID(int roleID)
        {
            var menu = new List<INavigationDO>();

            try
            {
                using (SqlConnection con = new SqlConnection(_conString))
                {
                    using (SqlCommand com = new SqlCommand("sp_GetNavigationItemsByRoleID", con))
                    {
                        try
                        {
                            com.CommandType = CommandType.StoredProcedure;
                            com.CommandTimeout = 35;

                            com.Parameters.Add(new SqlParameter("@RoleID", roleID));

                            con.Open();
                            using (SqlDataReader reader = com.ExecuteReader())
                            {
                                INavigationDO newMenuItem = new NavigationDO();
                                newMenuItem.NavigationID = reader.GetInt32(reader.GetOrdinal("NavigationMenuID"));
                                newMenuItem.MenuItem = reader["MenuItem"].ToString();
                                newMenuItem.URL = reader["Url"].ToString();
                                newMenuItem.RoleID = reader.GetInt32(reader.GetOrdinal("RoleID"));
                                menu.Add(newMenuItem);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex, "GetNavigationItemsByRoleID", "nothing");
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
                ErrorLogger.LogError(e, "GetNavigationItemsByRoleID", "nothing");

            }
            return menu;
        }
    }
}
