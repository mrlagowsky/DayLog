using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayLog.Models.Logging
{
    /// <summary>
    /// Enum for choosing the target for logging
    /// </summary>
    public enum LogTarget
    {
        File, Database, EventLog
    }
}