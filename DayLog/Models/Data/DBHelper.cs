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
                            response.UserFound = dr.GetBoolean(1);
                            response.UserID = dr.GetInt32(2);
                        }
                    }
                }
            }
            return response;
        }

        /// <summary>
        /// Method to try and register a new user in the system
        /// </summary>
        /// <param name="userEmail">User's email address</param>
        /// <param name="userFirstName">User's first name</param>
        /// <param name="userPassword">User's password</param>
        /// <returns>A true value for a successful registration and false for unsuccessful</returns>
        public bool TryRegister(string userEmail, string userFirstName, string userPassword)
        {
            //Local indicator for seeing the number of affected rows
            bool _successful = true;

            //Making a SQL connection to create a user
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand("usp_AddUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@LoginName", SqlDbType.NVarChar).Value = userEmail;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = userPassword;
                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = userFirstName;
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    //Assigning the values from the DataReader
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            _successful = dr.GetBoolean(0);
                        }
                    }
                }
            }
            //If the value is 0 it means the registration wasn't successful because the username already exists
            return _successful;
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

        /// <summary>
        /// Method for creating a new journal entry
        /// </summary>
        /// <param name="userID">We need the ID of the user who is making the entry</param>
        /// <param name="entryContent">We need the content of the entry</param>
        /// <param name="moodID">We need the mood ID for tracking</param>
        public bool CreateJournalEntry(int userID, string entryContent, MoodEnum moodID, bool ignore)
        {
            //Local indicator for seeing the number of affected rows
            int affected = 0;

            //Making a SQL connection to create a user
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand("usp_CreateNewJournalEntry", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                    cmd.Parameters.Add("@EntryContent", SqlDbType.NVarChar).Value = entryContent;
                    cmd.Parameters.Add("@MoodID", SqlDbType.Int).Value = (int)moodID;
                    cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@Ignore", SqlDbType.Bit).Value = ignore;

                    con.Open();

                    //ExecuteNonQuery will return the number of affected rows which we expect to be 1
                    affected = cmd.ExecuteNonQuery();
                }
            }
            //If the value is 0 it means the registration wasn't successful
            return affected == 1;
        }

        /// <summary>
        /// Method for retreiving a single journal entry for a particular user
        /// </summary>
        /// <param name="date">The date of the entry</param>
        /// <param name="userID">ID of the user this entry is needed for</param>
        /// <returns></returns>
        public EntryDetails GetSingleEntry(DateTime date, int userID)
        {
            //Local indicator for seeing the number of affected rows
            EntryDetails _entry = new EntryDetails();

            //Making a SQL connection to create a user
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetSingleEntry", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EntryDate", SqlDbType.DateTime).Value = date;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    //Assigning the values from the DataReader
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            _entry.EntryID = dr.GetInt32(0);
                            _entry.EntryContent = dr.GetString(1);
                            _entry.MoodID = (MoodEnum)dr.GetInt32(2);
                            _entry.CreatedDate = dr.GetDateTime(3);
                            _entry.Ignore = dr.GetBoolean(4);
                        }
                    }
                }
            }
            //Return the entry details
            return _entry;
        }
    }
}