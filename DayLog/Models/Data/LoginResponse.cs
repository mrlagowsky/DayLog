using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        /// <summary>
        /// User's email address
        /// </summary>
        [DisplayName("Username")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")]
        [Required(ErrorMessage = "Please enter your email address!")]
        public string Username { get; set; }

        /// <summary>
        /// User Password Regex needs 1 Upper case, 1 Lower case, 1 Numeric, 1 Special, minimum 6
        /// </summary>
        [DisplayName("Password")]
        [RegularExpression(@"(?=^.{6,255}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*")]
        [Required(ErrorMessage = "The password field cannot be blank!")]
        public string Password { get; set; }

        /// <summary>
        /// Property to indicate whether the user is authenticated successfully
        /// </summary>
        public bool Authenticated { get; set; }

        /// <summary>
        /// Property to indicate whether the user has been found in the database
        /// </summary>
        public bool UserFound { get; set; }

        /// <summary>
        /// User's ID
        /// </summary>
        public int UserID { get; set; } 

        /// <summary>
        /// Base Constructor
        /// </summary>
        public LoginResponse()
        {
            UserID = 0;
        }
    }
}