using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DayLog.Models.Data
{
    /// <summary>
    /// Object to capture the information needed to authenticate a user
    /// </summary>
    public class LoginResponse
    {
        [DisplayName("Username")]
        [RegexStringValidator(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")]
        public string LoginName { get; set; }
        [DisplayName("Password")]
        [RegexStringValidator(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{4,8}$")]
        public string Password { get; set; }
        public bool Authenticated { get; set; }

        /// <summary>
        /// Constructor will populate the Authenticated property automatically
        /// </summary>
        /// <param name="loginName">Username</param>
        /// <param name="password">User Password</param>
        public LoginResponse(string loginName, string password)
        {
            //Getting the connection string out of the web.config file
            string _connStr = ConfigurationManager.ConnectionStrings["dayLogConn"].ConnectionString;

            //Making a SQL connection to authenticate a user
            using (SqlConnection con = new SqlConnection(_connStr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Login", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@LoginName", SqlDbType.NVarChar).Value = loginName;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Authenticated = dr.GetBoolean(0);
                        }
                    }
                }
            }
        }
    }
}