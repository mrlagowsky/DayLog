using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DayLog.Models.Data
{

    /// <summary>
    /// Custom attribute to mark that a property of type bool must be false to pass the modelstate check
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class MustBeFalseAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool b = value != null && value is bool && !(bool)value;
            return b;
        }
    }
}