using System;
using System.IO;

namespace DayLog.Models.Logging
{
    public class FileLogger : LogBase
    {
        //Logging into the my documents folder
        private string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public override void Log(string message)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
        }
    }
}