using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OnshoreSDAttendanceTrackerErrorLogger
{
    public static class ErrorLogger
    {
        //TODO:  Match DBContext to correct Web.config name for DB
        private static string conString;

        static ErrorLogger()
        {
            conString = ConfigurationManager.ConnectionStrings["OnshoreSDAttendanceTracker"].ConnectionString;
        }

        public static void LogError(Exception error, string location, string url)
        {
            string innerEx = "";
            if (error.InnerException != null)
            {
                innerEx = error.InnerException.Message.ToString();
            }
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand com = new SqlCommand("sp_LogError", con))
                {
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter("@ErrorMsg", innerEx));
                    com.Parameters.Add(new SqlParameter("@SPName", location));
                    com.Parameters.Add(new SqlParameter("@ExceptionType", error.GetType().ToString()));
                    com.Parameters.Add(new SqlParameter("@ExceptionSource", "Application"));
                    com.Parameters.Add(new SqlParameter("@ExceptionURL", url));
                    com.ExecuteNonQuery();
                }
            }
        }
    }
}
