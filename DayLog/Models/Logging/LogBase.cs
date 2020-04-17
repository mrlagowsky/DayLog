using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayLog.Models.Logging
{
    /// <summary>
    /// Base class for logging across the system
    /// </summary>
    public abstract class LogBase
    {
        /// <summary>
        /// Generic Logging message
        /// </summary>
        /// <param name="message">Logging message content</param>
        public abstract void Log(string message);
    }
}