using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayLog.Models.Data
{
    /// <summary>
    /// System class used to store information needed for a successful user registration
    /// </summary>
    public class UserDetails
    {
        /// <summary>
        /// User login, stored as email 
        /// </summary>
        [DisplayName("Email Address")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "The Username doesn't match our rules! Please make sure it's an email, without spaces!")]
        [Required(ErrorMessage = "Email Address is missing!")]
        public string LoginName { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        [DisplayName("Password")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{4,8}$", ErrorMessage = "No Spaces! Between 8-16 Characters, one upper case, one lower case, one number!")]
        [Required(ErrorMessage = "Don't forget about the password!")]
        public string Password { get; set; }

        /// <summary>
        /// User's DOB
        /// </summary>
        [DisplayName("Date of Birth")]
        [Required(ErrorMessage = "We would like to know how old you are.")]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// User First Name
        /// </summary>
        [Required(ErrorMessage = "Please enter your first name")]
        [RegularExpression(@"([A-Za-z\-]+){3,18}")]
        public string FirstName { get; set; }

        /// <summary>
        /// User Last Name
        /// </summary>
        [Required(ErrorMessage = "Please enter your last name")]
        [RegularExpression(@"([A-Za-z\-]+){3,18}")]
        public string LastName { get; set; }

    }
}