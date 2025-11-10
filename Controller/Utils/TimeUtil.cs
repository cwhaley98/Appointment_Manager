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
            // Use ConvertTime to handle all Daylight Saving Time (DST) logic automatically
            DateTime estTime = TimeZoneInfo.ConvertTime(localTime, TimeZoneInfo.Local, BusinessTimeZone);
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

        /// <summary>
        /// Converts a time from UTC to Eastern Standard Time (EST) for database storage.
        /// </summary>
        /// <param name="utcTime">The time in UTC.</param>
        /// <returns>The equivalent time in EST.</returns>
        public static DateTime ConvertToESTFromUTC(DateTime utcTime)
        {
            // This is the correct conversion:
            // Source Time Kind = Utc, Source Time Zone = TimeZoneInfo.Utc
            DateTime estTime = TimeZoneInfo.ConvertTime(utcTime, TimeZoneInfo.Utc, BusinessTimeZone);
            return estTime;
        }

        /// <summary>
        /// Converts a time from the user's local time to UTC for database storage.
        /// </summary>
        public static DateTime ConvertToUTC(DateTime localTime)
        {
            // Convert local time directly to UTC
            return TimeZoneInfo.ConvertTime(localTime, TimeZoneInfo.Local, TimeZoneInfo.Utc);
        }

        /// <summary>
        /// Converts a time from UTC (from the database) to the user's local time for display.
        /// </summary>
        public static DateTime ConvertFromUTC(DateTime utcTime)
        {
            TimeZoneInfo userTimeZone = UserSessions.CurrentUserTimeZone;
            // Convert UTC time back to the user's local time
            return TimeZoneInfo.ConvertTime(utcTime, TimeZoneInfo.Utc, userTimeZone);
        }

        /// <summary>
        /// Converts a time from EST to UTC.
        /// </summary>
        public static DateTime ConvertFromESTToUTC(DateTime estTime)
        {
            // Convert from EST to UTC
            return TimeZoneInfo.ConvertTime(estTime, BusinessTimeZone, TimeZoneInfo.Utc);
        }
    }
}
