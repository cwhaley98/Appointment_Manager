using Appointment_Manager.Model.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Manager.Controller
{
    public class AppointmentController
    {
        #region Add/Update/Delete Appointments

        public void SaveAppointment(Dictionary<string, string> appointmentData, Dictionary<string, DateTime> startEndTime, bool isUpdate)
        {
            using (MySqlConnection connection = DBConnection.GetNewConnection())
            {
                try
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {

                        }
                    }
                }
            }
        }
        public DataTable GetAppointmentsByDay(DateTime day)
        {
            DateTime startOfDay = day.Date;
            DateTime endOfDay = startOfDay.AddDays(1); // Start of the NEXT day

            DataTable dt = new DataTable();

            using (MySqlConnection connection = DBConnection.GetNewConnection())
            {
                using (MySqlCommand GetAppointmentsCMD = new MySqlCommand(DBQueries.GetAppointmentsByDayQuery, connection))
                {
                    GetAppointmentsCMD.Parameters.AddWithValue("@StartOfDay", startOfDay);
                    GetAppointmentsCMD.Parameters.AddWithValue("@EndOfDay", endOfDay);

                    try
                    {
                        connection.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(GetAppointmentsCMD);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions (e.g., log them)
                        MessageBox.Show("Error retrieving daily appointments: " + ex.Message);
                    }
                }
            }

            return dt;
        }
    }
}
