using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayLog.Models.Logging
{
    /// <summary>
    /// Log helper class for general use across the application
    /// </summary>
    public static class LogHelper
    {
        private static LogBase logger = null;
        /// <summary>
        /// Log method extended to accept an enum pointing to the correct logging class
        /// </summary>
        /// <param name="target">Target logging place</param>
        /// <param name="message">Error message content</param>
        public static void Log(LogTarget target, string message)
        {
            switch (target)
            {
                case LogTarget.File:
                    logger = new FileLogger();
                    logger.Log(message);
                    break;
                case LogTarget.Database:
                    logger = new DBLogger();
                    logger.Log(message);
                    break;
                case LogTarget.EventLog:
                    logger = new EventLogger();
                    logger.Log(message);
                    break;
                default:
                    return;
            }
        }
    }
}