using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Manager.Controller.Utils
{
    public static class TimeUtil
    {
        // Define the business's time zone (Eastern Standard Time)
        private static readonly TimeZoneInfo BusinessTimeZone;

        static TimeUtil()
        {
            try
            {
                // "Eastern Standard Time" is the ID for EST on Windows.
                BusinessTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            }
            catch (TimeZoneNotFoundException)
            {
                // Handle case where the time zone ID isn't found
                // This is a fallback, but "Eastern Standard Time" is standard on Windows
                BusinessTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/New_York") ?? TimeZoneInfo.Utc;
            }
        }

        /// <summary>
        /// Converts a time from the user's local time (e.g., from a DateTimePicker) 
        /// to Eastern Standard Time (EST) for database storage.
        /// </summary>
        /// <param name="localTime">The time in the user's local time zone.</param>
        /// <returns>The equivalent time in EST.</returns>
        public static DateTime ConvertToEST(DateTime localTime)
        {
            // Get the user's time zone captured at login
            TimeZoneInfo userTimeZone = UserSessions.CurrentUserTimeZone;

            // Use ConvertTime to handle all Daylight Saving Time (DST) logic automatically
            DateTime estTime = TimeZoneInfo.ConvertTime(localTime, userTimeZone, BusinessTimeZone);
            return estTime;
        }

        /// <summary>
        /// Converts a time from Eastern Standard Time (EST) (e.g., from the database)
        /// to the user's local time for display.
        /// </summary>
        /// <param name="estTime">The time stored in the database (in EST).</param>
        /// <returns>The equivalent time in the user's local time zone.</returns>
        public static DateTime ConvertToLocalTime(DateTime estTime)
        {
            // Get the user's time zone captured at login
            TimeZoneInfo userTimeZone = UserSessions.CurrentUserTimeZone;

            // Use ConvertTime to handle all Daylight Saving Time (DST) logic automatically
            DateTime localTime = TimeZoneInfo.ConvertTime(estTime, BusinessTimeZone, userTimeZone);
            return localTime;
        }
    }
}
