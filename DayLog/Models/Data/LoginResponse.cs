using DayLog.Models.HelperClasses;
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
        [DisplayName("Username")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "This username doesn't match our rules!")]
        [Required(ErrorMessage = "Username is missing!")]
        public string LoginName { get; set; }

        [DisplayName("Password")]
        //[RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{4,8}$", ErrorMessage = "That is definitely not a password here!")]
        [Required(ErrorMessage = "Password is missing!")]
        public string Password { get; set; }

        /// <summary>
        /// Boolean indicating whether the user is successfully logged in or not, all logic handled in the db
        /// </summary>
        public bool? Authenticated { get; set; }

        /// <summary>
        /// User's ID populated after a successful authentication
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Indicator for whether the user has been found or not
        /// </summary>
        public bool User_Found { get; set; }

        //Default Constructor
        public LoginResponse()
        {
            LoginName = String.Empty;
            Password = String.Empty;
        }

    }
}