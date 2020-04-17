using System.Diagnostics;

namespace DayLog.Models.Logging
{
    /// <summary>
    /// Simple event logger class
    /// </summary>
    public class EventLogger : LogBase
    {
        public override void Log(string message)
        {
            EventLog eventLog = new EventLog("Error log");
            eventLog.Source = "IDGEventLog";
            eventLog.WriteEntry(message);
        }
    }
}