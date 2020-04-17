using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DayLog.Models.Data
{
    public class DBHelper 
    {
        //Getting the connection string out of the web.config file
        private readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["dayLogConn"].ConnectionString;

        public DBHelper()
        {
            
        }

        /// <summary>
        /// Helper method to try and login the user
        /// </summary>
        /// <param name="username">Self explanatory</param>
        /// <param name="password">Self explanatory</param>
        /// <returns></returns>
        public LoginResponse TryLogin(string username, string password)
        {
            //Creating a new response object
            LoginResponse response = new LoginResponse();
            response.Username = username;

            //Making a SQL connection to authenticate a user
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Login", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@LoginName", SqlDbType.NVarChar).Value = username;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    //Assigning the values from the DataReader
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            response.Authenticated = dr.GetBoolean(0);
                            response.User_Found = dr.GetBoolean(1);
                            response.UserID = dr.GetInt32(2);
                        }
                    }
                }
            }
            return response;
        }

        /// <summary>
        /// Method that will check if the user needs to make a journal entry today, checked upon login
        /// </summary>
        /// <param name="userId">User ID is passed in</param>
        /// <returns>True if we need a card to be created today, and false if one already exists</returns>
        public bool IsCardNeeded(int userId)
        {
            bool _result = false;

            //Making a SQL connection to authenticate a user
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Check_CardNeededToday", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    //Assigning the values from the DataReader
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            _result = dr.GetBoolean(0);
                        }
                    }
                }
            }

            return _result;
        }
    }
}