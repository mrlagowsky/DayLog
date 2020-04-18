using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayLog.Models.Data
{
    /// <summary>
    /// Enum to store the information about the user's mood
    /// </summary>
    public enum MoodEnum
    {
        Positive = 2,
        Okay = 1,
        Negative = 0
    }

    /// <summary>
    /// Class to hold the information about a single journal entry
    /// </summary>
    public class EntryDetails
    {
        /// <summary>
        /// ID number of the journal entry
        /// </summary>
        public int EntryID { get; set; }

        /// <summary>
        /// Content of the entry
        /// </summary>
        [Required(ErrorMessage = "You can't leave that field blank!")]
        [MinLength(20, ErrorMessage = "Your entry cannot be smaller than 20 characters")]
        public string EntryContent { get; set; }

        /// <summary>
        /// Indicator whether the entry describes a positive day or a negative one
        /// </summary>
        [Required(ErrorMessage = "You have to choose at least one!")]
        public MoodEnum MoodID { get; set; }

        /// <summary>
        /// Captures the date on which the entry was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}