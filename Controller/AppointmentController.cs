using Appointment_Manager.Controller.Utils;
using Appointment_Manager.Model.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Manager.Controller
{
    public class AppointmentController
    {
        /// <summary>
        /// Saves a new or updated appointment. Implements Req A.3.b (Exception Handling).
        /// </summary>
        public bool SaveAppointment(Dictionary<string, object> data, bool isUpdate)
        {
            using (MySqlConnection connection = DBConnection.GetNewConnection())
            {
                try
                {
                    connection.Open();
                    string query = isUpdate ? DBQueries.UpdateAppointmentQuery : DBQueries.InsertAppointmentQuery;

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        AddAppointmentParameters(command, data);

                        command.Parameters.AddWithValue("@LastUpdateBy", UserSessions.CurrentUserName);
                        if (isUpdate)
                        {
                            command.Parameters.AddWithValue("@AppointmentId", data["AppointmentId"]);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@CreatedBy", UserSessions.CurrentUserName);
                        }

                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving appointment: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Helper to add common parameters to the Save/Update command.
        /// </summary>
        private void AddAppointmentParameters(MySqlCommand command, Dictionary<string, object> data)
        {
            command.Parameters.AddWithValue("@CustomerId", data["CustomerId"]);
            command.Parameters.AddWithValue("@UserId", data["UserId"]);
            command.Parameters.AddWithValue("@Description", data["Description"]);
            command.Parameters.AddWithValue("@Location", data["Location"]);
            command.Parameters.AddWithValue("@Type", data["Type"]);
            command.Parameters.AddWithValue("@Start", (DateTime)data["Start"]);
            command.Parameters.AddWithValue("@End", (DateTime)data["End"]);

            // Handle optional/stubbed fields
            command.Parameters.AddWithValue("@Title", data.ContainsKey("Title") ? data["Title"] : "N/A");
            command.Parameters.AddWithValue("@Contact", data.ContainsKey("Contact") ? data["Contact"] : "N/A");
            command.Parameters.AddWithValue("@URL", data.ContainsKey("URL") ? data["URL"] : "N/A");
        }

        /// <summary>
        /// Checks for overlapping appointments for a consultant. Implements Req A.3.a.
        /// </summary>
        public bool CheckForOverlappingAppointments(int consultantId, DateTime startTimeEST, DateTime endTimeEST, string currentAppointmentId)
        {
            using (MySqlConnection connection = DBConnection.GetNewConnection())
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(DBQueries.CheckOverlapQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", consultantId);
                        command.Parameters.AddWithValue("@AppointmentId", string.IsNullOrEmpty(currentAppointmentId) ? "0" : currentAppointmentId);
                        command.Parameters.AddWithValue("@Start", startTimeEST);
                        command.Parameters.AddWithValue("@End", endTimeEST);

                        long count = (long)command.ExecuteScalar();
                        return count > 0; // If count > 0, an overlap was found
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error checking for overlap: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return true; // Fail safe: prevent saving if check fails
                }
            }
        }

        /// <summary>
        /// Deletes an appointment. Implements Req A.3.b (Exception Handling).
        /// </summary>
        public void DeleteAppointment(string appointmentId)
        {
            using (MySqlConnection connection = DBConnection.GetNewConnection())
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(DBQueries.DeleteAppointmentQuery, connection))
                    {
                        command.Parameters.AddWithValue("@AppointmentId", appointmentId);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting appointment: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Gets all appointments as filtering is handled by the calendar.
        /// </summary>
        public DataTable GetAppointments()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection connection = DBConnection.GetNewConnection())
            {
                // The query is now just the base query without any filters.
                string query = DBQueries.GetAppointmentsQuery + " ORDER BY start ASC;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(dt);

                        // Also convert these times to local for display
                        foreach (DataRow row in dt.Rows)
                        {
                            row["start"] = TimeUtil.ConvertToLocalTime((DateTime)row["start"]);
                            row["end"] = TimeUtil.ConvertToLocalTime((DateTime)row["end"]);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error getting all appointments: {ex.Message}");
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// Gets appointments for a specific day. Implements Req A.4 and A.5.
        /// </summary>
        public DataTable GetAppointmentsByDay(DateTime day)
        {
            DataTable dt = new DataTable();

            // Convert local day to EST window for database query
            DateTime startOfDayEST = TimeUtil.ConvertToEST(day.Date);
            DateTime endOfDayEST = TimeUtil.ConvertToEST(day.Date.AddDays(1));

            using (MySqlConnection connection = DBConnection.GetNewConnection())
            {
                using (MySqlCommand command = new MySqlCommand(DBQueries.GetAppointmentsByDayQuery, connection))
                {
                    command.Parameters.AddWithValue("@StartOfDay", startOfDayEST);
                    command.Parameters.AddWithValue("@EndOfDay", endOfDayEST);

                    try
                    {
                        connection.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(dt);

                        // --- Requirement A.5: Convert to Local Time ---
                        // Convert all 'start' and 'end' times from EST back to the user's local time for display.
                        foreach (DataRow row in dt.Rows)
                        {
                            row["start"] = TimeUtil.ConvertToLocalTime((DateTime)row["start"]);
                            row["end"] = TimeUtil.ConvertToLocalTime((DateTime)row["end"]);
                        }
                        // -----------------------------------------------
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error fetching daily appointments: " + ex.Message);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// Checks for upcoming appointments on login. Implements Req A.6.
        /// </summary>
        public void ValidateUpcomingAppointments()
        {
            DateTime nowUTC = DateTime.UtcNow;
            DateTime nowEST = TimeUtil.ConvertToESTFromUTC(nowUTC); // Convert current time to EST for DB comparison
            DateTime fifteenMinutesFromNowEST = nowEST.AddMinutes(15);

            using (MySqlConnection connection = DBConnection.GetNewConnection())
            {
                using (MySqlCommand command = new MySqlCommand(DBQueries.UpcomingAppointmentsQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserId", UserSessions.CurrentUserId);
                    command.Parameters.AddWithValue("@Now", nowEST);
                    command.Parameters.AddWithValue("@FifteenMinutesFromNow", fifteenMinutesFromNowEST);

                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // An appointment was found
                                DateTime startTime = (DateTime)reader["start"];
                                string customerName = reader["customerName"].ToString();

                                // Convert start time to local for the alert (Req A.5)
                                DateTime localStartTime = TimeUtil.ConvertToLocalTime(startTime);

                                string lang = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                                string title = (lang == "de") ? "Terminwarnung" : "Appointment Alert";
                                string message = (lang == "de") ?
                                    $"Sie haben einen bevorstehenden Termin mit {customerName} um {localStartTime:HH:mm} Uhr." :
                                    $"You have an upcoming appointment with {customerName} at {localStartTime:h:mm tt}.";

                                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error checking upcoming appointments: {ex.Message}");
                    }
                }
            }
        }

        // --- REPORTING METHODS (Requirement A.7) ---

        public DataTable GetAppointmentTypesByMonth(int month, int year)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection connection = DBConnection.GetNewConnection())
            {
                // This query is simple and doesn't need date filtering for this requirement
                using (MySqlCommand command = new MySqlCommand(DBQueries.ReportTypesBySelectedMonthQuery, connection))
                {

                    try
                    {
                        command.Parameters.AddWithValue("@Month", month);

                        connection.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error generating report: {ex.Message}");
                    }
                }
            }
            return dt;
        }

        public DataTable GetConsultantSchedules(string userName, string userId)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection connection = DBConnection.GetNewConnection())
            {
                using (MySqlCommand command = new MySqlCommand(DBQueries.ReportConsultantScheduleQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserId", Convert.ToInt32(userId));

                    try
                    {
                        connection.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(dt);

                        // Also convert these times to local for display
                        foreach (DataRow row in dt.Rows)
                        {
                            row["start"] = TimeUtil.ConvertToLocalTime((DateTime)row["start"]);
                            row["end"] = TimeUtil.ConvertToLocalTime((DateTime)row["end"]);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error generating report: {ex.Message}");
                    }
                }
            }
            return dt;
        }

        public DataTable GetAppointmentCountPerLocation()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection connection = DBConnection.GetNewConnection())
            {
                using (MySqlCommand command = new MySqlCommand(DBQueries.ReportLocationQuery, connection))
                {
                    try
                    {
                        connection.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error generating report: {ex.Message}");
                    }
                }
            }
            return dt;
        }
    }
}
