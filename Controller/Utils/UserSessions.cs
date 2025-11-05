using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Manager.Controller.Utils
{
    public static class UserSessions
    {
        public static int CurrentUserId { get; set; }
        public static string CurrentUserName { get; set; }

        public static TimeZoneInfo CurrentUserTimeZone { get; set; }
    }
}
