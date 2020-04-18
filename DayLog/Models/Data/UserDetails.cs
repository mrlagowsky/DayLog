using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayLog.Models.Data
{
    /// <summary>
    /// Class to capture the user registration details object
    /// </summary>
    public class UserDetails
    {
        /// <summary>
        /// User's email address
        /// </summary>
        [DisplayName("Email Address")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Please make sure you insert your email address correctly!")]
        [Required(ErrorMessage = "Please make sure your email address is not empty!")]
        public string Username { get; set; }

        /// <summary>
        /// User Password Regex needs 1 Upper case, 1 Lower case, 1 Numeric, 1 Special, minimum 6
        /// </summary>
        [DisplayName("Password")]
        [RegularExpression(@"(?=^.{6,255}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*")]
        [Required(ErrorMessage = "Make sure the password is at least 6 letters, contains one lower and one upper case and a special character")]
        public string Password { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        [Required(ErrorMessage = "You need to insert your name here")]
        [RegularExpression("/^[a-z ,.'-]+$/i", ErrorMessage = "This surely isn't a correct name!")]
        public string FirstName { get; set; }

        /// <summary>
        /// Property to indicate whether the user already exists in the database
        /// </summary>
        []
        public bool Exists { get; set; }

    }
}