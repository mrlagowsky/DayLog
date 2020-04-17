using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DayLog.Models.Logging
{
    /// <summary>
    /// Class for logging into the DB
    /// </summary>
    public class DBLogger : LogBase
    {
        //Connection string
        string connectionString = ConfigurationManager.ConnectionStrings["dayLogConn"].ConnectionString;

        /// <summary>
        /// Logging to the db
        /// </summary>
        /// <param name="message">Error message content</param>
        public override void Log(string message)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_CreateLog", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ExceptionMessage", SqlDbType.NVarChar).Value = message;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}