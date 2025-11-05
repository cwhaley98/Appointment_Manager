using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Manager.Controller.Utils
{
    public static class UserActivity
    {
        //Log user activity to a text file
        private static readonly string logFilePath = "user_activity_log.txt";

        public static void LogUserActivity(string username)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(logFilePath, true))
                {
                    string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: User '{username}' logged in.";
                    sw.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during logging
                Console.WriteLine($"Error logging user activity: {ex.Message}");
            }
        }
    }
}
