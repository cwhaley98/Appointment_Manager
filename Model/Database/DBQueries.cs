using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Manager.Model.Database
{
    public class DBQueries
    {
        // Parameterized query to select appointments that overlap with a specific 24-hour period.
        // @StartOfDay and @EndOfDay will be DateTime parameters passed.
        public static string GetAppointmentsByDayQuery
        {
            get
            {
                return @"
                    SELECT 
                        appointment.appointmentId,
                        appointment.customerId,
                        appointment.userId,
                        appointment.title,
                        appointment.description,
                        appointment.location,
                        appointment.contact,
                        appointment.type,
                        appointment.url,
                        appointment.start,
                        appointment.end,
                        customer.customerName,
                        user.userName
                    FROM 
                        appointment
                    INNER JOIN 
                        customer ON appointment.customerId = customer.customerId
                    INNER JOIN 
                        user ON appointment.userId = user.userId
                    WHERE 
                        appointment.start < @EndOfDay 
                        AND appointment.end > @StartOfDay
                    ORDER BY 
                        appointment.start ASC;
                ";
            }
        }
    }
}
