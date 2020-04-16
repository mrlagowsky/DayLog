using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DayLog.Models.Data
{
    public class DayCardsHelper
    {
        //Getting the connection string out of the web.config file
        public string CONN_STR = ConfigurationManager.ConnectionStrings["dayLogConn"].ConnectionString; 
        public LoginResponse TryLogin(string username, string password)
        {
            //Create the response object and carry on the username
            LoginResponse response = new LoginResponse();
            response.LoginName = username;

            //Making a SQL connection to authenticate a user
            using (SqlConnection con = new SqlConnection(CONN_STR))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Login", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@LoginName", SqlDbType.NVarChar).Value = username;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;

                    con.Open();

                    //Populate the data reader
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        //Read the first row to find out whether the login was successful
                        while (dr.Read())
                        {
                            //When constructing the LoginResponse back, we don't need the password and username anymore so we don't populate those parameters
                            response.Authenticated = dr.GetBoolean(0);
                            response.User_Found = dr.GetBoolean(1);
                            response.UserID = dr.GetInt32(2);
                        }
                    }
                }
            }
            return response;
        }
    }
}