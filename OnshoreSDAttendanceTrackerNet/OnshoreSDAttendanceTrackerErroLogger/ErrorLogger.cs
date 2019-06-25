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
        private static string conString= ConfigurationManager.ConnectionStrings["DBContext"].ConnectionString;

        public static void LogError(Exception error, string location,string url)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand com = new SqlCommand("sp_LogError", con))
                {
                    con.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add(new SqlParameter("@ErrorMsg", error.InnerException==null?error.Message:error.InnerException.Message));
                    com.Parameters.Add(new SqlParameter("@SPName",location));
                    com.Parameters.Add(new SqlParameter("@ExceptionType", error.GetType().ToString()));
                    com.Parameters.Add(new SqlParameter("@ExceptionSource", "Application"));
                    com.Parameters.Add(new SqlParameter("@ExceptionURL", url));
                    com.ExecuteNonQuery();
                }
            }
        }
    }
}
